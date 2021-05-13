using System;
using System.Collections.Generic;
using System.Linq;
using ICSSoft.STORMNET;
using ICSSoft.STORMNET.Business;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    /// <summary>
    /// Хелпер для языка PGSQL
    /// </summary>
    public class PGDataBaseHelper : DataBaseHelper
    {
        public PGDataBaseHelper(string spatialIdField):  base(spatialIdField)
        {
        }

        public override string Format(string identifier)
        {
            return PostgresDataService.PrepareIdentifier(identifier);
        }

        /// <summary>
        /// </summary>
        public override string FromStatement(string insertedTableName, Chain chain)
        {
            string result = string.Empty;
            foreach (string joinKey in chain.Joins.Keys)
            {
                result += string.Format(
                    "{6} {0} {1} ON {1}.{2}= {3}.{4}{5}",
                    chain.Joins[joinKey].FullTableName,
                    chain.Joins[joinKey].Alias,
                    chain.Joins[joinKey].PrimaryKey,
                    chain.Alias,
                    joinKey,
                    Environment.NewLine,
                    "LEFT JOIN");
                result += FromStatement(insertedTableName, chain.Joins[joinKey]);
            }

            return result;
        }

        /// <summary>
        /// </summary>
        public override string DropTriggersCommand(string triggerKey, string triggerPrefix)
        {
            return
                $@"do $$
                begin
                EXECUTE (coalesce((
                    select string_agg(x, ' ')
                    from
                        (select 'DROP TRIGGER ' || pg_trigger.tgname || ' ON ' || pg_namespace.nspname || '.' || pg_class.relname || '; DROP FUNCTION ' || pg_proc.proname || '();' x
                         from pg_trigger, pg_proc, pg_class, pg_namespace
                         where pg_proc.oid = pg_trigger.tgfoid
                                and pg_trigger.tgrelid = pg_class.oid
                                    and pg_class.relnamespace = pg_namespace.oid
                                        and pg_trigger.tgname like '{triggerPrefix}%{triggerKey}') t), '')
                    );
                end;
            $$;";
        }

        /// <summary>
        /// </summary>
        public override string DropSpatialTriggersCommand(string triggerName)
        {
            var procedureExec = string.Format(
                @"do $$
                    begin
                        IF exists(select * from pg_proc where proname = '{0}') THEN
                            PERFORM {0}();
                            DROP FUNCTION {0}();
                        END IF;
                  end $$;", triggerName) + Environment.NewLine;

            return procedureExec;
        }

        private string FunctionForTriggerForSpatialRecordsCount(string layer, string triggerName, Dictionary<string, string> linkParams, Chain chain, string owner, string type)
        {
            string triggerCommand = $"CREATE FUNCTION {triggerName}_f() RETURNS TRIGGER AS $${Environment.NewLine}";
            triggerCommand += $"BEGIN{Environment.NewLine}";

            var linkColumns = string.Join(", ", linkParams.Select(link => link.Key).ToArray());

            if (type == "u")
            {
                triggerCommand += $"UPDATE {chain.FullTableName}{Environment.NewLine}";
                triggerCommand += $"SET SpatialRecordsCount = (CASE WHEN((SpatialRecordsCount IS NULL) OR (SpatialRecordsCount < tbl.cnt)) THEN 0 ELSE SpatialRecordsCount - tbl.cnt END){Environment.NewLine}";
                triggerCommand += $"FROM (SELECT COUNT(*) cnt, {linkColumns} FROM {layer} layer WHERE layer.{SpatialIdField} = OLD.{SpatialIdField} GROUP BY {linkColumns}) tbl{Environment.NewLine}";
                triggerCommand += $"WHERE {string.Join(" AND ", linkParams.Select(link => $"tbl.{link.Key} = {link.Value}").ToArray())};{Environment.NewLine}";
            }

            triggerCommand += $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            triggerCommand += $"SET SpatialRecordsCount = coalesce(SpatialRecordsCount, 0) + tbl.cnt{Environment.NewLine}";
            triggerCommand += $"FROM (SELECT COUNT(*) cnt, {linkColumns} FROM {layer} layer WHERE layer.{SpatialIdField} = NEW.{SpatialIdField} GROUP BY {linkColumns}) tbl{Environment.NewLine}";
            triggerCommand += $"WHERE {string.Join(" AND ", linkParams.Select(link => $"tbl.{link.Key}={link.Value}").ToArray())};{Environment.NewLine}";

            triggerCommand += $"RETURN NULL;{Environment.NewLine}";
            triggerCommand += $"END;{Environment.NewLine}";
            triggerCommand += $"$$ LANGUAGE plpgsql;{Environment.NewLine}{Environment.NewLine}";

            triggerCommand += $"CREATE TRIGGER {triggerName}{Environment.NewLine}";

            triggerCommand += type == "u"
                ? $"AFTER UPDATE OF {string.Join(", ", linkParams.Select(link => link.Key).ToArray())} ON {layer}{Environment.NewLine}"
                : $"AFTER INSERT ON {layer}{Environment.NewLine}";

            triggerCommand += $"FOR EACH ROW{Environment.NewLine}";
            triggerCommand += $"EXECUTE PROCEDURE {triggerName}_f();{Environment.NewLine}";
            if (!string.IsNullOrEmpty(owner)) triggerCommand += $"ALTER FUNCTION {triggerName}_f() OWNER TO {owner};";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForSpatialRecordsCount(string layer, string triggerPrefix, string triggerName, Dictionary<string, string> linkParams, Chain chain, string owner)
        {
            return FunctionForTriggerForSpatialRecordsCount(layer, $"{triggerPrefix}_i{triggerName}", linkParams, chain, owner, "i") +
                   Environment.NewLine +
                   FunctionForTriggerForSpatialRecordsCount(layer, $"{triggerPrefix}_u{triggerName}", linkParams, chain, owner, "u");
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForSpatialRecordsCountOnDelete(string layer, string triggerName,
            Dictionary<string, string> linkParams, Chain chain, string owner)
        {
            string triggerCommand = $"CREATE FUNCTION {triggerName}_f() RETURNS TRIGGER AS $${Environment.NewLine}";
            triggerCommand += $"BEGIN{Environment.NewLine}";

            triggerCommand += $"UPDATE {chain.FullTableName}{Environment.NewLine}";

            var linkColumns = string.Join(", ", linkParams.Select(link => link.Key).ToArray());
            triggerCommand += $"SET SpatialRecordsCount = (CASE WHEN((SpatialRecordsCount IS NULL) OR (SpatialRecordsCount < tbl.cnt)) THEN 0 ELSE SpatialRecordsCount - tbl.cnt END){Environment.NewLine}";
		    triggerCommand += $"FROM (SELECT COUNT(*) cnt, {linkColumns} FROM {layer} layer WHERE layer.{SpatialIdField} = OLD.{SpatialIdField} GROUP BY {linkColumns}) tbl{Environment.NewLine}";
            triggerCommand += $"WHERE {string.Join(" AND ", linkParams.Select(link => $"tbl.{link.Key}={link.Value}").ToArray())};{Environment.NewLine}";

            triggerCommand += $"RETURN NULL;{Environment.NewLine}";
            triggerCommand += $"END;{Environment.NewLine}";
            triggerCommand += $"$$ LANGUAGE plpgsql;{Environment.NewLine}{Environment.NewLine}";

            triggerCommand += $"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $"AFTER DELETE ON {layer}{Environment.NewLine}";
            triggerCommand += $"FOR EACH ROW{Environment.NewLine}";
            triggerCommand += $"EXECUTE PROCEDURE {triggerName}_f();{Environment.NewLine}";
            if (!string.IsNullOrEmpty(owner)) triggerCommand += $"ALTER FUNCTION {triggerName}_f() OWNER TO {owner};";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string InitialUpdateCommandForSpatialRecordsCount(string layer, Dictionary<string, string> linkParams, Chain chain, string owner)
        {
            string result = $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            result += $"SET SpatialRecordsCount = coalesce(SpatialRecordsCount, 0) + spatialTable.cnt{Environment.NewLine}";

            var fields = string.Join(", ", linkParams.Select(links => links.Key).ToArray());
            result += $"FROM (SELECT COUNT(*) cnt, {fields} FROM {layer} GROUP BY {fields}) spatialTable{Environment.NewLine}";
            result += $"WHERE{Environment.NewLine}";
            result += string.Join(" AND ", linkParams.Select(links => $"spatialTable.{links.Key} = {links.Value}").ToArray()) + ";";
            return result;
        }

        /// <summary>
        /// </summary>
        public override string CreateStoredProcedureForUnjoinSpatialCounter(string layer, string procedureName, Dictionary<string, string> linkParams, Chain chain, string owner)
        {
            string result = $"CREATE FUNCTION {procedureName}() RETURNS void AS $${Environment.NewLine}";
            result += $"BEGIN{Environment.NewLine}";

            result += $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            result += $"SET SpatialRecordsCount = (CASE WHEN((SpatialRecordsCount IS NULL) OR (SpatialRecordsCount - stbl.cnt <= 0)) THEN 0 ELSE SpatialRecordsCount - stbl.cnt END){Environment.NewLine}";

            var fields = string.Join(", ", linkParams.Select(links => links.Key).ToArray());

            result += $"FROM (SELECT COUNT(*) cnt, {fields} FROM {layer} GROUP BY {fields}) stbl{Environment.NewLine}";
            result += $"WHERE {Environment.NewLine}";
            result += $"{string.Join(" AND ", linkParams.Select(links => $"stbl.{links.Key} = {links.Value}").ToArray())};{Environment.NewLine}";

            result += $"END;{Environment.NewLine}";
            result += $"$$ LANGUAGE plpgsql;{Environment.NewLine}";

            if (!string.IsNullOrEmpty(owner)) result += $"ALTER FUNCTION {procedureName}() OWNER TO {owner};";

            return result;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForUpdateSpatialObject(string layer, string triggerName, Dictionary<string, string> linkParams, bool clearWithoutLink, Chain chain, List<MapExpressionField> expressionFields, Dictionary<string, string> simpleFields, string owner)
        {
            string triggerCommand = $"CREATE FUNCTION {triggerName}_f() RETURNS TRIGGER AS $${Environment.NewLine}";
            triggerCommand += $"BEGIN{Environment.NewLine}";

            triggerCommand += $"UPDATE {layer} l{Environment.NewLine}SET{Environment.NewLine}";

            var updates = expressionFields.Select(field => $"{field.LayerField} = {chain.ReplacePath(field.Paths, field.Expression)}").ToList();
            updates.AddRange(simpleFields.Select(field => field.Key + " = " + chain.ReplacePath(field.Value)).ToArray());

            triggerCommand += string.Join($",{Environment.NewLine}", updates) + Environment.NewLine;

            triggerCommand += $"FROM {layer} layer {(clearWithoutLink ? "LEFT" : "INNER")} JOIN {chain.FullTableName} {chain.Alias} ON ";
            triggerCommand += $"{string.Join(" AND ", linkParams.Select(links => $"layer.{links.Key} = {chain.Alias}.{links.Value}").ToArray())}{Environment.NewLine}";

            triggerCommand += FromStatement("", chain);

            triggerCommand += $"WHERE NEW.{SpatialIdField} = layer.{SpatialIdField} AND l.{SpatialIdField} = layer.{SpatialIdField};{Environment.NewLine}";

            triggerCommand += $"RETURN NULL;{Environment.NewLine}";
            triggerCommand += $"END;{Environment.NewLine}";
            triggerCommand += $"$$ LANGUAGE plpgsql;{Environment.NewLine}{Environment.NewLine}";

            triggerCommand += $"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $"AFTER INSERT OR UPDATE OF {string.Join(", ", linkParams.Select(link => link.Key).ToArray())} ON {layer}{Environment.NewLine}";
            triggerCommand += $"FOR EACH ROW{Environment.NewLine}";
            triggerCommand += $"EXECUTE PROCEDURE {triggerName}_f();{Environment.NewLine}";
            if (!string.IsNullOrEmpty(owner)) triggerCommand += $"ALTER FUNCTION {triggerName}_f() OWNER TO {owner};";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForInsertSpatialObject(string layer, string triggerName, Dictionary<string, string> linkParams, bool clearWithoutLink, Chain chain, List<MapExpressionField> expressionFields, Dictionary<string, string> simpleFields, string owner)
        {
            // Игнорируем записи для мастеровых свойств.
            var insertFields = simpleFields.Where(f => !f.Value.Contains("."));

            string triggerCommand = $@"
CREATE FUNCTION {triggerName}_f() RETURNS TRIGGER AS $$
BEGIN
    IF (TG_OP = 'INSERT' AND NEW.DataObjectKey IS NULL) THEN
        NEW.DataObjectKey := uuid_generate_v4()::varchar;

        INSERT INTO {chain.FullTableName}
        (
            primaryKey,
            {string.Join($", {Environment.NewLine}        ",
                insertFields.Select(field => field.Value))}
        )
        SELECT
            NEW.DataObjectKey::UUID,
            {string.Join($", {Environment.NewLine}        ",
                insertFields.Select(field => $"NEW.{field.Key}"))};


    END IF;

    RETURN NEW;
END
$$ LANGUAGE plpgsql;
CREATE TRIGGER {triggerName}
  BEFORE INSERT
  ON {layer}
  FOR EACH ROW
  EXECUTE PROCEDURE {triggerName}_f();
";

            if (!string.IsNullOrEmpty(owner))
                triggerCommand += $"ALTER FUNCTION {triggerName}_f() OWNER TO {owner};";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string Update(MapField field, string setCommand, string layer, Dictionary<string, string> linkParams)
        {
            string updateCommand = $"UPDATE {layer} layer" + Environment.NewLine;
            updateCommand += setCommand;
            updateCommand += $"FROM {field.Chain.FullTableName} {field.Chain.Alias} ";
            updateCommand += FromStatement("", field.Chain);
            updateCommand += "WHERE ";
            updateCommand += string.Join(" AND ", linkParams.Select(flink => $"layer.{flink.Key} = {field.Chain.Alias}.{flink.Value}").ToArray()) + ";" + Environment.NewLine;

            return updateCommand;
        }

        /// <summary>
        /// </summary>
        public override string DeleteTrigger(string layer, string triggerName, Type rootType,
            Dictionary<string, string> linkParams, List<MapExpressionField> expressionFields,
            Dictionary<string, MapSimpleField> simpleFields, string owner = "")
        {
            var triggerCommand = string.Empty;
            triggerCommand += $@"CREATE FUNCTION {triggerName}_f() RETURNS TRIGGER AS $${Environment.NewLine}";
            triggerCommand += $"BEGIN{Environment.NewLine}";

            triggerCommand += $"UPDATE {layer} layer {Environment.NewLine}SET {Environment.NewLine}";

            var updatedFieldsByTrigger = new List<string>();

            foreach (var simpleField in simpleFields.Values) updatedFieldsByTrigger.AddRange(simpleField.LayerObjectFields.Keys);
            updatedFieldsByTrigger.AddRange(expressionFields.Select(field => field.LayerField));
            updatedFieldsByTrigger.AddRange(linkParams.Select(link => link.Key));

            triggerCommand += $"{string.Join(", " + Environment.NewLine, updatedFieldsByTrigger.Select(field => field + " = NULL").ToArray())}{Environment.NewLine}";

            triggerCommand += $"WHERE {string.Join(" AND ", linkParams.Select(flink => "layer." + flink.Key + " = OLD." + flink.Value).ToArray())}; {Environment.NewLine}";

            triggerCommand += $"RETURN NULL;{Environment.NewLine}";
            triggerCommand += $"END;{Environment.NewLine}";
            triggerCommand += $"$$ LANGUAGE plpgsql;{Environment.NewLine}{Environment.NewLine}";

            triggerCommand += $"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $"AFTER DELETE ON {Information.GetClassStorageName(rootType)}{Environment.NewLine}";
            triggerCommand += $"FOR EACH ROW{Environment.NewLine}";
            triggerCommand += $"EXECUTE PROCEDURE {triggerName}_f();{Environment.NewLine}";
            if (!string.IsNullOrEmpty(owner)) triggerCommand += $"ALTER FUNCTION {triggerName}_f() OWNER TO {owner};";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string UpdateTrigger(string layer, string triggerName, Type rootType,
            Dictionary<string, string> linkParams, string fromTableName, MapField field, string setCommand, string owner)
        {
            string triggerCommand = $"CREATE FUNCTION {triggerName}_f() RETURNS TRIGGER AS $${Environment.NewLine}";
            triggerCommand += $"BEGIN{Environment.NewLine}";
            triggerCommand += $"UPDATE {layer} layer{Environment.NewLine}";
            triggerCommand += setCommand;

            triggerCommand += $"FROM {field.Chain.FullTableName} {field.Chain.Alias} ";
            triggerCommand += $"{FromStatement(fromTableName, field.Chain)}{Environment.NewLine}";

            triggerCommand += "WHERE ";

            var tableAlias = field.Chain.TableName == fromTableName ? "NEW" : field.Chain.Alias;
            triggerCommand += string.Join(" AND ",
                linkParams.Select(flink => $"layer.{flink.Key} = {tableAlias}.{flink.Value}").ToArray()) + Environment.NewLine;

            var fromTableChain = field.Chain.FindChain(fromTableName);
            triggerCommand += $" AND {fromTableChain.Alias}.{fromTableChain.PrimaryKey} = NEW.{fromTableChain.PrimaryKey};{Environment.NewLine}";

            triggerCommand += $"RETURN NULL;{Environment.NewLine}";
            triggerCommand += $"END{Environment.NewLine}";
            triggerCommand += $"$$ LANGUAGE plpgsql;{Environment.NewLine}{Environment.NewLine}";

            triggerCommand += $"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $"AFTER INSERT OR UPDATE";
            triggerCommand += field.Chain.UpdatedFields(fromTableName).Any() ? $" OF {string.Join(", ", field.Chain.UpdatedFields(fromTableName).Select(fld => fld).ToArray())}" : "";
            triggerCommand += $" ON {fromTableName}{Environment.NewLine}";

            triggerCommand += $"FOR EACH ROW{Environment.NewLine}";
            triggerCommand += $"EXECUTE PROCEDURE {triggerName}_f();{Environment.NewLine}";
            if (!string.IsNullOrEmpty(owner)) triggerCommand += $"ALTER FUNCTION {triggerName}_f() OWNER TO {owner};";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string SetStatement(MapSimpleField field)
        {
            return $"SET {string.Join(", ", field.LayerObjectFields.Select(fmap => fmap.Key + " = " + field.Chain.ReplacePath(fmap.Value)).ToArray()) + Environment.NewLine}";
        }

        /// <summary>
        /// </summary>
        public override string SetStatement(MapExpressionField field)
        {
            return $"SET {field.LayerField}={field.Chain.ReplacePath(field.Paths, field.Expression)}{Environment.NewLine}";
        }
    }
}
