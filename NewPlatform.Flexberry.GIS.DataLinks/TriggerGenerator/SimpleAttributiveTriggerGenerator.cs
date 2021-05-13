namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class SimpleAttributiveTriggerGenerator : ITriggerGenerator
    {
        /// <summary>
        /// Тип объекта для которого генерируются триггеры
        /// </summary>
        public Type RootType { get; private set; }

        /// <summary>
        /// Префикс генерируемых триггеров
        /// </summary>
        public string TriggerPrefix { get; private set; }

        /// <summary>
        /// Суффикс генерируемых триггеров
        /// </summary>
        public string TriggerKey { get; private set; }

        /// <summary>
        /// Имя таблицы в базе геоданных
        /// </summary>
        public string Layer { get; private set; }

        public readonly Dictionary<string, MapSimpleField> SimpleFields = new Dictionary<string, MapSimpleField>();

        public SimpleAttributiveTriggerGenerator(string triggerPrefix)
        {
            TriggerPrefix = triggerPrefix;
        }

        public void DropTriggers(IDbCommand sqlCommand)
        {
            var sql = TGHelper.DataBaseHelper.DropTriggersCommand(TriggerKey, TriggerPrefix);

            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();
        }

        private string TriggerCommand(string triggerName, string fromTableName, MapSimpleField field)
        {
            string triggerCommand = $"CREATE TRIGGER {triggerName}{Environment.NewLine}";
            triggerCommand += $"ON {fromTableName} AFTER INSERT, UPDATE AS BEGIN{Environment.NewLine}";
            triggerCommand += field.Chain.UpdatedFields(fromTableName).Any()
                ? "IF " +
                  string.Join(" OR ",
                      field.Chain.UpdatedFields(fromTableName)
                          .Select(fld => $"UPDATE({fld})")
                          .ToArray()) + " BEGIN" + Environment.NewLine
                : " BEGIN" + Environment.NewLine;
            triggerCommand += "INSERT INTO SyncDataObjectKey(primaryKey, tablename, objectkey, changetime)" + Environment.NewLine;
            triggerCommand += $"SELECT NEWID(), '{Layer}', {field.Chain.Alias}.primaryKey, GETDATE() ";
            triggerCommand +=
                $"FROM {(field.Chain.TableName == fromTableName ? "inserted" : field.Chain.FullTableName)} {field.Chain.Alias} ";
            triggerCommand += TGHelper.DataBaseHelper.FromStatement(fromTableName, field.Chain) + Environment.NewLine;
            triggerCommand += "END" + Environment.NewLine;
            triggerCommand += "END;";


            return triggerCommand;
        }

        public void GenerateTriggers(IDbCommand sqlCommand)
        {
            foreach (var keyValuePair in SimpleFields.Where(x => x.Value != null))
            {
                var simpleField = keyValuePair.Value;

                var triggerName = simpleField.Chain.TableName + "_" + string.Join("", keyValuePair.Key.Split('.').ToList().Where(x => !string.IsNullOrEmpty(x)).Select(x => "_" + x));

                var fromTableNames = simpleField.Chain.FromTableNames;
                for (int i = 0; i < fromTableNames.Count; i++)
                {
                    string fromTableName = fromTableNames[i];

                    var hasher = System.Security.Cryptography.MD5.Create();
                    var hashdata = hasher.ComputeHash(System.Text.Encoding.Default.GetBytes($"{triggerName}_{i}"));
                    var hashName = string.Concat(hashdata.Select(b => b.ToString("x2")).ToArray());

                    sqlCommand.CommandText = TriggerCommand($"{TriggerPrefix}_u{hashName}_a{TriggerKey}", fromTableName, simpleField);
                    sqlCommand.ExecuteNonQuery();
                }                
            }

            var chain = new Chain(RootType, "");
            sqlCommand.CommandText = "INSERT INTO SyncDataObjectKey(primaryKey, tablename, objectkey, changetime)" + Environment.NewLine
                                    + $"SELECT NEWID(), '{Layer}', primaryKey, GETDATE() FROM {chain.FullTableName};";
            sqlCommand.ExecuteNonQuery();
        }

        private void AddSimpleField(string layerField, string objectfield)
        {
            var path = objectfield.LastIndexOf('.') > -1 ? objectfield.Substring(0, objectfield.LastIndexOf('.')) : "";
            if (!SimpleFields.Keys.Contains(path))
            {
                SimpleFields.Add(path, new MapSimpleField(RootType, layerField, objectfield));
            }
            else
            {
                SimpleFields[path].Add(layerField, objectfield);
            }
        }

        public void InitByDataLink(DataLink link)
        {
            Layer = $@"{link.LayerTable}";
            RootType = TGHelper.ObjectType(link);
            TriggerKey = Regex.Replace(link.__PrimaryKey.ToString(), @"[^a-zA-Z]", string.Empty);
            if (TriggerKey.Length > 20) TriggerKey = TriggerKey.Substring(0, 20);

            foreach (var p in link.DataLinkParameter.Cast<DataLinkParameter>().Where(p => !p.LinkField))
            {
                if (!string.IsNullOrEmpty(p.ObjectField) && string.IsNullOrEmpty(p.Expression))
                {
                    AddSimpleField(p.LayerField, p.ObjectField);
                }
                else
                {
                    var match = Regex.Match(p.Expression, @"@(.+?)@");
                    int i = 0;
                    while (match.Success)
                    {                        
                        AddSimpleField(p.LayerField + i++, match.Groups[1].Value);
                        match = match.NextMatch();
                    }
                }
            }
        }
    }
}
