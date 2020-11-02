using System.Configuration;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using ICSSoft.STORMNET;

    /// <summary>
    /// </summary>
    public class BaseSpatialTriggerGenerator : BaseTriggerGenerator
    {
        /// <summary>
        /// </summary>
        protected List<MapExpressionField> ExpressionFields = new List<MapExpressionField>();
        
        /// <summary>
        /// </summary>
        protected Dictionary<string, string> SimpleFields = new Dictionary<string, string>();
        
        /// <summary>
        /// </summary>
        protected Chain Chain;

        /// <summary>
        /// </summary>
        protected string ProcedureName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        protected bool GenerateCreateObjectTrigger { get; private set; }

        /// <summary>
        /// Общая часть имени атрибутивной таблицы которая участвует в триггере(схема или бд.схема)
        /// </summary>
        protected string BasePrefix { get; private set; }

        /// <summary>
        /// Схема создаваемых функций
        /// </summary>
        protected string FunctionSchema { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layerPrefix">Общая имени слоя на который генерируются триггеры, может быть (схема или бд.схема)</param>
        /// <param name="basePrefix">Общая часть имени атрибутивной таблицы которая участвует в триггере (схема или бд.схема)</param>
        /// <param name="triggerPrefix">Общая начальная часть имени генерируемыех триггеров</param>
        /// <param name="functionSchema">Схема, в которой будут созданы тригерные функции</param>
        /// <param name="owner">Поменять владельца созданных объектов на указанного</param>
        public BaseSpatialTriggerGenerator(string layerPrefix, string basePrefix, string triggerPrefix, string functionSchema = "", string owner = "") : base(layerPrefix, triggerPrefix, owner)
        {
            BasePrefix = basePrefix;
            FunctionSchema = functionSchema;
        }

        public override void InitByDataLink(DataLink link)
        {
            base.InitByDataLink(link);

            var hasher = System.Security.Cryptography.MD5.Create();
            var hashdata = hasher.ComputeHash(System.Text.Encoding.Default.GetBytes($"procedure_unjoin_{TriggerKey}"));
            ProcedureName = string.Concat(hashdata.Select(b => b.ToString("x2")).ToArray());

            Chain = new Chain(RootType, BasePrefix);

            GenerateCreateObjectTrigger = link.CreateObject;

            foreach (var p in link.DataLinkParameter.Cast<DataLinkParameter>().Where(p => !p.LinkField))
            {
                if (!string.IsNullOrEmpty(p.ObjectField) && string.IsNullOrEmpty(p.Expression))
                {
                    Chain.Add(p.ObjectField);

                    if (!SimpleFields.ContainsKey(p.LayerField))
                        SimpleFields.Add(p.LayerField, p.ObjectField);
                }
                else
                {
                    var expressionField = new MapExpressionField(RootType, p.LayerField, p.Expression);
                    ExpressionFields.Add(expressionField);
                    foreach (var path in expressionField.Paths)
                    {
                        Chain.Add(path);
                    }
                }
            }
        }

        /// <summary>Сгенерировать триггеры в текущей команде Sql</summary>
        /// <param name="sqlCommand">sql command с открытым соединением и, если требуется, транзакцией.</param>
        public override void GenerateTriggers(IDbCommand sqlCommand)
        {
            var sql = TGHelper.DataBaseHelper.CommandTextForTriggerForSpatialRecordsCount(Layer, TriggerPrefix, $"_upd_SOC_{TriggerKey}", LinkParams, Chain, Owner, FunctionSchema);
            if (!string.IsNullOrEmpty(sql))
            {
                sqlCommand.CommandText = sql;
                LogService.LogDebug(sql);
                sqlCommand.ExecuteNonQuery();
                LogService.LogDebug("done");
            }

            sql = TGHelper.DataBaseHelper.CommandTextForTriggerForSpatialRecordsCountOnDelete(Layer, $"{TriggerPrefix}_del_SOC_{TriggerKey}", LinkParams, Chain, Owner, FunctionSchema);
            if (!string.IsNullOrEmpty(sql))
            {
                sqlCommand.CommandText = sql;
                LogService.LogDebug(sql);
                sqlCommand.ExecuteNonQuery();
                LogService.LogDebug("done");
            }

            var ignoreInitial = (ConfigurationManager.AppSettings["ignoreInitial"] ?? "") == "1";
            if (!ignoreInitial)
            {
                sql = TGHelper.DataBaseHelper.InitialUpdateCommandForSpatialRecordsCount(Layer, LinkParams, Chain, Owner);
                if (!string.IsNullOrEmpty(sql))
                {
                    sqlCommand.CommandText = sql;
                    LogService.LogDebug(sql);
                    sqlCommand.ExecuteNonQuery();
                    LogService.LogDebug("done");
                }
            }
            else LogService.LogDebug($"Игнорируем заполнение SpatialRecordsCount для таблицы {Chain.FullTableName}");
            
            sql = TGHelper.DataBaseHelper.CommandTextForTriggerForUpdateSpatialObject(Layer, $"{TriggerPrefix}_USO_{TriggerKey}", LinkParams, ClearWithoutLink, Chain, ExpressionFields, SimpleFields, Owner, FunctionSchema);
            if (!string.IsNullOrEmpty(sql))
            {
                sqlCommand.CommandText = sql;
                LogService.LogDebug(sql);
                sqlCommand.ExecuteNonQuery();
                LogService.LogDebug("done");
            }

            if (GenerateCreateObjectTrigger)
            {
                sql = TGHelper.DataBaseHelper.CommandTextForTriggerForInsertSpatialObject(Layer,
                    $"{TriggerPrefix}_ISO_{TriggerKey}", LinkParams, ClearWithoutLink, Chain, ExpressionFields,
                    SimpleFields, Owner, FunctionSchema);
                if (!string.IsNullOrEmpty(sql))
                {
                    sqlCommand.CommandText = sql;
                    LogService.LogDebug(sql);
                    sqlCommand.ExecuteNonQuery();
                    LogService.LogDebug("done");
                }
            }

            sql = TGHelper.DataBaseHelper.CreateStoredProcedureForUnjoinSpatialCounter(Layer, $"{FunctionSchema}{TriggerPrefix}_unjn_{ProcedureName}", LinkParams, Chain, Owner);
            if (!string.IsNullOrEmpty(sql))
            {
                sqlCommand.CommandText = sql;
                LogService.LogDebug(sql);
                sqlCommand.ExecuteNonQuery();
                LogService.LogDebug("done");
            }
        }

        /// <summary>Удалить триггеры из указанной БД</summary>
        /// <param name="sqlCommand">sql command с открытым соединением и, если требуется, транзакцией.</param>
        public override void DropTriggers(IDbCommand sqlCommand)
        {
            var sql = TGHelper.DataBaseHelper.DropSpatialTriggersCommand($"{TriggerPrefix}_unjn_{ProcedureName}", FunctionSchema);
            sqlCommand.CommandText = sql;
            LogService.LogDebug(sql);
            sqlCommand.ExecuteNonQuery();
            LogService.LogDebug("done");

            sql = TGHelper.DataBaseHelper.DropTriggersCommand(TriggerKey, (FunctionSchema ?? "") + TriggerPrefix);
            sqlCommand.CommandText = sql;
            LogService.LogDebug(sql);
            sqlCommand.ExecuteNonQuery();
            LogService.LogDebug("done");
        }
    }
}
