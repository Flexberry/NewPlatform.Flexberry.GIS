namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using ICSSoft.STORMNET;

    /// <summary>
    /// </summary>
    public class BaseAttributiveTriggerGenerator : BaseTriggerGenerator
    {
        /// <summary>
        /// Общая часть имени атрибутивной таблицы, которая участвует в триггере(схема или бд.схема)
        /// </summary>
        protected string BasePrefix { get; private set; }

        /// <summary>
        /// Схема создаваемых функций
        /// </summary>
        protected string FunctionSchema { get; private set; }

        /// <summary>
        /// </summary>
        protected List<MapExpressionField> ExpressionFields = new List<MapExpressionField>();
        /// <summary>
        /// </summary>
        protected Dictionary<string, MapSimpleField> SimpleFields = new Dictionary<string, MapSimpleField>();

        /// <summary>
        /// </summary>
        /// <param name="layerPrefix">Общая имени слоя на который генерируются триггеры, может быть (схема или бд.схема)</param>
        /// <param name="basePrefix">Общая часть имени атрибутивной таблицы которая участвует в триггере (схема или бд.схема)</param>
        /// <param name="triggerPrefix">Общая начальная часть имени генерируемыех триггеров</param>
        /// <param name="functionSchema">Схема, в которой будут созданы тригерные функции</param>
        /// <param name="owner">Поменять владельца созданных объектов на указанного</param>
        public BaseAttributiveTriggerGenerator(string layerPrefix, string basePrefix, string triggerPrefix, string functionSchema = "", string owner = "") :
            base(layerPrefix, triggerPrefix, owner)
        {
            BasePrefix = basePrefix;
            FunctionSchema = functionSchema;
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
                        SimpleFields.Add(path, new MapSimpleField(RootType, p.LayerField, p.ObjectField, BasePrefix));
                    }
                    else
                    {
                        SimpleFields[path].Add(p.LayerField, p.ObjectField);
                    }
                }
                else
                {
                    var expressionField = new MapExpressionField(RootType, p.LayerField, p.Expression, BasePrefix);
                    ExpressionFields.Add(expressionField);
                }
            }
        }

        private List<string> GenerateAtributiveDatabaseTriggers()
        {
            var result = new List<string>();
            if (LinkParams.Count == 0) return result;

            var ignoreInitial = (ConfigurationManager.AppSettings["ignoreInitial"] ?? "") == "1";
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

                    result.Add(TGHelper.DataBaseHelper.UpdateTrigger(Layer, $"{TriggerPrefix}_u{hashName}_a{TriggerKey}", RootType, LinkParams, fromTableName, simpleField, TGHelper.DataBaseHelper.SetStatement(simpleField), Owner, FunctionSchema));
                }

                if (!ignoreInitial) result.Add(TGHelper.DataBaseHelper.Update(simpleField, TGHelper.DataBaseHelper.SetStatement(simpleField), Layer, LinkParams));
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

                    result.Add(TGHelper.DataBaseHelper.UpdateTrigger(Layer, $"{TriggerPrefix}_u{hashName}_e{TriggerKey}", RootType, LinkParams, fromTableName, field, TGHelper.DataBaseHelper.SetStatement(field), Owner, FunctionSchema));
                }

                if (!ignoreInitial)  result.Add(TGHelper.DataBaseHelper.Update(field, TGHelper.DataBaseHelper.SetStatement(field), Layer, LinkParams));
            }

            result.Add(TGHelper.DataBaseHelper.DeleteTrigger(Layer, $"{TriggerPrefix}_d{TriggerKey}", RootType, LinkParams, ExpressionFields, SimpleFields, Owner, FunctionSchema));

            return result;
        }

        /// <summary>Сгенерировать триггеры в текущей команде Sql</summary>
        /// <param name="sqlCommand">sql command с открытым соединением и, если требуется, транзакцией.</param>
        public override void GenerateTriggers(IDbCommand sqlCommand)
        {
            foreach (var attributiveDatabaseTrigger in GenerateAtributiveDatabaseTriggers())
            {
                sqlCommand.CommandText = attributiveDatabaseTrigger;
                LogService.LogDebug(attributiveDatabaseTrigger);
                sqlCommand.ExecuteNonQuery();
                LogService.LogDebug("done");
            }
        }

        /// <summary>Удалить триггеры из указанной БД</summary>
        /// <param name="sqlCommand">sql command с открытым соединением и, если требуется, транзакцией.</param>
        public override void DropTriggers(IDbCommand sqlCommand)
        {
            var sql = TGHelper.DataBaseHelper.DropTriggersCommand(TriggerKey, (FunctionSchema ?? "") + TriggerPrefix);

            sqlCommand.CommandText = sql;
            LogService.LogDebug(sql);
            sqlCommand.ExecuteNonQuery();
            LogService.LogDebug("done");
        }
    }
}