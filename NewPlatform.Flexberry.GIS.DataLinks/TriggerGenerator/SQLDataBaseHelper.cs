using System;
using System.Collections.Generic;
using System.Linq;
using ICSSoft.STORMNET;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    /// <summary>
    /// Хелпер для языка SQL
    /// </summary>
    public class SQLDataBaseHelper : DataBaseHelper
    {
        public SQLDataBaseHelper(string spatialIdField):  base(spatialIdField)
        {
        }

        public override string Format(string identifier)
        {
            return identifier;
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
                    chain.Joins[joinKey].TableName == insertedTableName
                        ? "inserted"
                        : chain.Joins[joinKey].FullTableName,
                    chain.Joins[joinKey].Alias,
                    chain.Joins[joinKey].PrimaryKey,
                    chain.Alias,
                    joinKey,
                    Environment.NewLine,
                    chain.Joins[joinKey].TableName == insertedTableName
                        ? "INNER JOIN"
                        : "LEFT JOIN");
                result += FromStatement(insertedTableName, chain.Joins[joinKey]);
            }

            return result;
        }

        /// <summary>
        /// </summary>
        public override string DropTriggersCommand(string triggerKey, string triggerPrefix)
        {
            string dropTemplate =
                              @"
                                DECLARE @query varchar(200)
                                DECLARE trig_cursor CURSOR FOR
                                    SELECT 'DROP TRIGGER ' + sys.schemas.name + '.' + sys.triggers.name
                                    FROM
	                                    sys.triggers,
	                                    sys.objects,
	                                    sys.schemas
                                    WHERE
	                                    sys.triggers.parent_id = sys.objects.object_id
	                                    AND sys.objects.schema_id = sys.schemas.schema_id
                                        AND sys.triggers.name LIKE '{1}%{0}'

                                OPEN trig_cursor
                                SET NOCOUNT ON
                                WHILE 0 = 0
                                BEGIN
            	                    FETCH NEXT FROM trig_cursor INTO @query

            	                    IF  @@FETCH_STATUS <> 0
            		                    BREAK

            	                    execute(@query)
                                END

                                CLOSE trig_cursor
                                DEALLOCATE trig_cursor";

            return string.Format(dropTemplate, triggerKey, triggerPrefix);
        }

        /// <summary>
        /// </summary>
        public override string DropSpatialTriggersCommand(string triggerName)
        {
            var procedureExec = string.Format(
                    @"IF OBJECT_ID('{0}', 'P') IS NOT NULL BEGIN
                        EXECUTE {0};
                        DROP PROCEDURE {0}; 
                    END",
                    triggerName) + Environment.NewLine;

            return procedureExec;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForSpatialRecordsCount(string layer, string triggerPrefix, string triggerName, Dictionary<string, string> linkParams, Chain chain, string owner = "")
        {
            string result = $@"CREATE TRIGGER {triggerPrefix}{triggerName}{Environment.NewLine}";
            result += $"ON {layer}{Environment.NewLine}";
            result += $"AFTER INSERT, UPDATE AS BEGIN{Environment.NewLine}";
            result += $"IF {string.Join(" OR ", linkParams.Select(link => $"UPDATE({link.Key})").ToArray())} BEGIN {Environment.NewLine}";
            result += $"UPDATE {chain.FullTableName}{Environment.NewLine}";

            var linkColumns = string.Join(", ", linkParams.Select(link => link.Key).ToArray());
            result += $@"SET [SpatialRecordsCount] =
			                (CASE
				                WHEN((SpatialRecordsCount IS NULL) OR (SpatialRecordsCount < tbl.cnt)) THEN 0
				                ELSE SpatialRecordsCount - tbl.cnt
			                 END)
		                FROM
			                (SELECT COUNT(*) cnt, {linkColumns} FROM DELETED GROUP BY {linkColumns}) tbl WHERE ";

            result += string.Join(" AND ", linkParams.Select(link => $"tbl.{link.Key}={link.Value}").ToArray()) + Environment.NewLine;

            result += $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            result += $@"SET [SpatialRecordsCount] = ISNULL([SpatialRecordsCount], 0) + tbl.cnt
            	  FROM (SELECT COUNT(*) cnt, {linkColumns} FROM INSERTED GROUP BY {linkColumns}) tbl WHERE ";
            result += string.Join(" AND ", linkParams.Select(link => $"tbl.{link.Key}={link.Value}").ToArray()) + Environment.NewLine;
            result += "END END";
            return result;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForSpatialRecordsCountOnDelete(string layer, string triggerName,
            Dictionary<string, string> linkParams, Chain chain, string owner = "")
        {
            string result = $@"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            result += $"ON {layer}{Environment.NewLine}";
            result += "AFTER DELETE AS BEGIN" + Environment.NewLine;
            result += $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            var linkColumns = string.Join(", ", linkParams.Select(link => link.Key).ToArray());
            result += $@"SET [SpatialRecordsCount] =
			                (CASE
				                WHEN((SpatialRecordsCount IS NULL) OR (SpatialRecordsCount < tbl.cnt)) THEN 0
				                ELSE SpatialRecordsCount - tbl.cnt
			                 END)
		                FROM
			                (SELECT COUNT(*) cnt, {linkColumns} FROM DELETED GROUP BY {linkColumns}) tbl WHERE ";
            result += $"{string.Join(" AND ", linkParams.Select(link => $"tbl.{link.Key}={link.Value}").ToArray())}{Environment.NewLine}";
            result += "END";
            return result;
        }

        /// <summary>
        /// </summary>
        public override string InitialUpdateCommandForSpatialRecordsCount(string layer, Dictionary<string, string> linkParams, Chain chain, string owner = "")
        {
            string result = $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            result += @"SET SpatialRecordsCount = ISNULL(SpatialRecordsCount, 0) + spatialTable.cnt FROM ";
            string innerSelect = "SELECT COUNT(*) cnt, ";
            string fields = string.Join(", ", linkParams.Select(links => links.Key).ToArray()) + Environment.NewLine;
            innerSelect += fields;
            innerSelect += "FROM " + layer + Environment.NewLine;
            innerSelect += "GROUP BY " + fields + Environment.NewLine;
            result += "(" + innerSelect + ") spatialTable" + Environment.NewLine;
            result += "WHERE" + Environment.NewLine;
            result += string.Join(" AND ", linkParams.Select(links => $"spatialTable.{links.Key}={links.Value}").ToArray());
            return result;
        }

        /// <summary>
        /// </summary>
        public override string CreateStoredProcedureForUnjoinSpatialCounter(string layer, string procedureName,
            Dictionary<string, string> linkParams, Chain chain, string owner = "")
        {
            string result = string.Empty;
            result += $"CREATE PROCEDURE {procedureName} AS BEGIN{Environment.NewLine}";
            result += $"UPDATE {chain.FullTableName}{Environment.NewLine}";
            result += @"SET SpatialRecordsCount =
                            (CASE
				                WHEN((SpatialRecordsCount IS NULL) OR (SpatialRecordsCount - spatialTable.cnt <= 0)) THEN 0
				                ELSE SpatialRecordsCount - spatialTable.cnt
			                 END)" + Environment.NewLine;
            string innerSelect = "SELECT COUNT(*) cnt, ";
            string fields = string.Join(", ", linkParams.Select(links => links.Key).ToArray()) + Environment.NewLine;
            innerSelect += fields;
            innerSelect += "FROM " + layer + Environment.NewLine;
            innerSelect += "GROUP BY " + fields + Environment.NewLine;
            result += "FROM (" + innerSelect + ") spatialTable" + Environment.NewLine;
            result += "WHERE" + Environment.NewLine;
            result += string.Join(" AND ", linkParams.Select(links => $"spatialTable.{links.Key}={links.Value}").ToArray()) + Environment.NewLine;
            result += "END";
            return result;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForUpdateSpatialObject(string layer, string triggerName, Dictionary<string, string> linkParams, bool clearWithoutLink, Chain chain, List<MapExpressionField> expressionFields, Dictionary<string, string> simpleFields, string owner = "")
        {
            string result = $@"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            result += $"ON {layer}{Environment.NewLine}";
            result += "AFTER INSERT, UPDATE AS BEGIN" + Environment.NewLine;
            result += $"IF {string.Join(" OR ", linkParams.Select(link => $"UPDATE({link.Key})").ToArray())} BEGIN{Environment.NewLine}";
            result += $"UPDATE layer SET{Environment.NewLine}";

            var updates = expressionFields.Select(field => $"{field.LayerField} = {chain.ReplacePath(field.Paths, field.Expression)}").ToList();
            updates.AddRange(simpleFields.Select(field => field.Key + " = " + chain.ReplacePath(field.Value)).ToArray());

            result += string.Join(", ", updates) + Environment.NewLine;

            result += $"FROM {layer} layer {(clearWithoutLink ? "LEFT" : "INNER")} JOIN {chain.FullTableName} {chain.Alias} ON ";
            result += string.Join(" AND ",
                linkParams.Select(links => $"layer.{links.Key} = {chain.Alias}.{links.Value}")
                           .ToArray()) + Environment.NewLine;
            result += FromStatement("", chain);
            result += $"INNER JOIN inserted ON inserted.{SpatialIdField} = layer.{SpatialIdField} ";
            result += "END END";
            return result;
        }

        /// <summary>
        /// </summary>
        public override string CommandTextForTriggerForInsertSpatialObject(string layer, string triggerName, Dictionary<string, string> linkParams, 
            bool clearWithoutLink, Chain chain, List<MapExpressionField> expressionFields, Dictionary<string, string> simpleFields, string owner = "")
        {
            string result =
                $@"
CREATE TRIGGER {triggerName}
    ON {layer}
    AFTER INSERT 
AS 
BEGIN
    SELECT 
        ins.{SpatialIdField}, 
        NEWID() AS newid
    INTO #tmp
    FROM inserted AS ins
        LEFT JOIN deleted AS del ON ins.{SpatialIdField} = del.{SpatialIdField}
    WHERE del.{SpatialIdField} IS NULL

    INSERT INTO {chain.FullTableName}
    (
        [primaryKey],
        {string.Join($", {Environment.NewLine}        ", 
            simpleFields.Select(field => field.Key))}
    )
    SELECT
        [primaryKey] = #tmp.newid,
        {string.Join($", {Environment.NewLine}        ", 
            simpleFields.Select(field => $"{field.Key} = {field.Value}"))}
    FROM inserted AS ins
        INNER JOIN #tmp ON ins.{SpatialIdField} = #tmp.{SpatialIdField}

    UPDATE layer SET DataObjectKey = #tmp.newid
    FROM {layer} AS layer 
        INNER JOIN #tmp ON layer.{SpatialIdField} = #tmp.{SpatialIdField}

    DROP TABLE #tmp
END";
            return result;
        }

        /// <summary>
        /// </summary>
        public override string Update(MapField field, string setCommand, string layer, Dictionary<string, string> linkParams)
        {
            string updateCommand = "UPDATE layer" + Environment.NewLine;
            updateCommand += setCommand;
            updateCommand += $"FROM {layer} layer INNER JOIN {field.Chain.FullTableName} {field.Chain.Alias} ON ";
            updateCommand += string.Join(" AND ", linkParams.Select(flink => $"layer.{flink.Key} = {field.Chain.Alias}.{flink.Value}").ToArray()) + Environment.NewLine;
            updateCommand += FromStatement("", field.Chain);
            return updateCommand;
        }

        /// <summary>
        /// </summary>
        public override string DeleteTrigger(string layer, string triggerName, Type rootType,
            Dictionary<string, string> linkParams, List<MapExpressionField> expressionFields, Dictionary<string, MapSimpleField> simpleFields, string owner = "")
        {
            var triggerCommand = string.Empty;
            triggerCommand += $@"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $@"ON {Information.GetClassStorageName(rootType)} AFTER DELETE AS BEGIN{Environment.NewLine}";

            triggerCommand += "UPDATE layer SET" + Environment.NewLine;
            var updatedFieldsByTrigger = new List<string>();

            foreach (var simpleField in simpleFields.Values) updatedFieldsByTrigger.AddRange(simpleField.LayerObjectFields.Keys);
            updatedFieldsByTrigger.AddRange(expressionFields.Select(field => field.LayerField));
            updatedFieldsByTrigger.AddRange(linkParams.Select(link => link.Key));

            triggerCommand += string.Join(", " + Environment.NewLine, updatedFieldsByTrigger.Select(field => field + " = NULL").ToArray());

            triggerCommand += " FROM deleted ";
            triggerCommand += " INNER JOIN " + layer + " layer ON ";
            triggerCommand += string.Join(" AND ", linkParams.Select(flink => "layer." + flink.Key + "= deleted." + flink.Value).ToArray()) + Environment.NewLine;
            triggerCommand += "END";

            return triggerCommand;
        }

        /// <summary>
        /// </summary>
        public override string UpdateTrigger(string layer, string triggerName, Type rootType,
            Dictionary<string, string> linkParams, string fromTableName, MapField field, string setCommand, string owner = "")
        {
            string triggerCommand = $"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $"ON {fromTableName} AFTER INSERT, UPDATE AS BEGIN{Environment.NewLine}";
            triggerCommand += field.Chain.UpdatedFields(fromTableName).Any()
                ? "IF " +
                  string.Join(" OR ",
                      field.Chain.UpdatedFields(fromTableName)
                          .Select(fld => $"UPDATE({fld})")
                          .ToArray()) + " BEGIN" + Environment.NewLine
                : "";
            triggerCommand += "UPDATE layer" + Environment.NewLine;
            triggerCommand += setCommand;
            triggerCommand +=
                $"FROM {layer} layer INNER JOIN {(field.Chain.TableName == fromTableName ? "inserted" : field.Chain.FullTableName)} {field.Chain.Alias} ON ";

            triggerCommand += string.Join(
                " AND ",
                linkParams.Select(
                    flink =>
                        $"layer.{flink.Key}={field.Chain.Alias}.{flink.Value}").
                                  ToArray()) + Environment.NewLine;

            triggerCommand += FromStatement(fromTableName, field.Chain) + Environment.NewLine;
            triggerCommand += "END" + Environment.NewLine;
            triggerCommand += "END";

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
