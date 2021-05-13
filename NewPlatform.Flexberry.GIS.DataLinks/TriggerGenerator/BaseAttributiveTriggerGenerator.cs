using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    /// <summary>
    /// </summary>
    public class BaseAttributiveTriggerGenerator : BaseTriggerGenerator
    {
        /// <summary>
        /// </summary>
        protected List<MapExpressionField> ExpressionFields = new List<MapExpressionField>();
        /// <summary>
        /// </summary>
        protected Dictionary<string, MapSimpleField> SimpleFields = new Dictionary<string, MapSimpleField>();

        /// <summary>
        /// </summary>
        public BaseAttributiveTriggerGenerator(string layerPrefix, string triggerPrefix, string owner = "") :
            base(layerPrefix, triggerPrefix, owner)
        {
        }

        public override void InitByDataLink(DataLink link)
        {
            base.InitByDataLink(link);

            foreach (var p in link.DataLinkParameter.Cast<DataLinkParameter>().Where(p => !p.LinkField))
            {
                if (!string.IsNullOrEmpty(p.ObjectField) && string.IsNullOrEmpty(p.Expression))
                {
                    var path = p.ObjectField.LastIndexOf('.') > -1 ? p.ObjectField.Substring(0, p.ObjectField.LastIndexOf('.')) : "";
                    if (!SimpleFields.Keys.Contains(path))
                    {
                        SimpleFields.Add(path, new MapSimpleField(RootType, p.LayerField, p.ObjectField));
                    }
                    else
                    {
                        SimpleFields[path].Add(p.LayerField, p.ObjectField);
                    }
                }
                else
                {
                    var expressionField = new MapExpressionField(RootType, p.LayerField, p.Expression);
                    ExpressionFields.Add(expressionField);
                }
            }
        }

        private List<string> GenerateAtributiveDatabaseTriggers()
        {
            var result = new List<string>();
            if (LinkParams.Count == 0) return result;

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

                    result.Add(TGHelper.DataBaseHelper.UpdateTrigger(Layer, $"{TriggerPrefix}_u{hashName}_a{TriggerKey}", RootType, LinkParams, fromTableName, simpleField, TGHelper.DataBaseHelper.SetStatement(simpleField), Owner));
                }

                result.Add(TGHelper.DataBaseHelper.Update(simpleField, TGHelper.DataBaseHelper.SetStatement(simpleField), Layer, LinkParams));
            }

            foreach (var field in ExpressionFields)
            {
                var fromTableNames = field.Chain.FromTableNames;

                for (int i = 0; i < fromTableNames.Count; i++)
                {
                    string fromTableName = fromTableNames[i];

                    var hasher = System.Security.Cryptography.MD5.Create();
                    var hashdata = hasher.ComputeHash(System.Text.Encoding.Default.GetBytes($"{field.LayerField}{field.Expression}_{i}"));
                    var hashName = string.Concat(hashdata.Select(b => b.ToString("x2")).ToArray());

                    result.Add(TGHelper.DataBaseHelper.UpdateTrigger(Layer, $"{TriggerPrefix}_u{hashName}_e{TriggerKey}", RootType, LinkParams, fromTableName, field, TGHelper.DataBaseHelper.SetStatement(field), Owner));
                }

                result.Add(TGHelper.DataBaseHelper.Update(field, TGHelper.DataBaseHelper.SetStatement(field), Layer, LinkParams));
            }

            result.Add(TGHelper.DataBaseHelper.DeleteTrigger(Layer, $"{TriggerPrefix}_d{TriggerKey}", RootType, LinkParams, ExpressionFields, SimpleFields, Owner));

            return result;
        }

        /// <summary>Сгенерировать триггеры в текущей команде Sql</summary>
        /// <param name="sqlCommand">sql command с открытым соединением и, если требуется, транзакцией.</param>
        public override void GenerateTriggers(IDbCommand sqlCommand)
        {
            foreach (var attributiveDatabaseTrigger in GenerateAtributiveDatabaseTriggers())
            {
                sqlCommand.CommandText = attributiveDatabaseTrigger;
                sqlCommand.ExecuteNonQuery();
            }
        }

        /// <summary>Удалить триггеры из указанной БД</summary>
        /// <param name="sqlCommand">sql command с открытым соединением и, если требуется, транзакцией.</param>
        public override void DropTriggers(IDbCommand sqlCommand)
        {
            var sql = TGHelper.DataBaseHelper.DropTriggersCommand(TriggerKey, TriggerPrefix);

            sqlCommand.CommandText = sql;
            sqlCommand.ExecuteNonQuery();
        }
    }
}