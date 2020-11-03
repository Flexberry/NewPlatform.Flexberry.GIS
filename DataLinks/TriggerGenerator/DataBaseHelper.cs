namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System;
    using System.Collections.Generic;

    public abstract class DataBaseHelper
    {
        private DataBaseHelper()
        {
        }

        public DataBaseHelper(string spatialIdField)
        {
            SpatialIdField = spatialIdField;
        }

        protected string SpatialIdField;

        /// <summary>
        /// Формат экранирования свойства
        /// </summary>
        /// <returns></returns>
        public abstract string Format(string identifier);

        /// <summary>
        /// Формирование блока From по таблицам цепочки
        /// </summary>
        /// <param name="insertedTableName"></param>
        /// <param name="chain"></param>
        /// <param name="join"></param>
        /// <returns></returns>
        public abstract string FromStatement(string insertedTableName, Chain chain);

        /// <summary>
        /// Формирование команды удаления триггеров
        /// </summary>
        /// <param name="triggerKey"></param>
        /// <param name="triggerPrefix"></param>
        /// <returns></returns>
        public abstract string DropTriggersCommand(string triggerKey, string triggerPrefix);

        /// <summary>
        /// Формирование команды удаления дополнительных опций для геоданных
        /// </summary>
        /// <param name="triggerName"></param>
        /// <param name="functionSchema"></param>
        /// <returns></returns>
        public abstract string DropSpatialTriggersCommand(string triggerName, string functionSchema);

        /// <summary>
        /// </summary>
        public abstract string CommandTextForTriggerForSpatialRecordsCount(string layer, string triggerPrefix, string triggerName, Dictionary<string, string> linkParams, Chain chain, string owner = "", string functionSchema = "");
        
        /// <summary>
        /// </summary>
        public abstract string CommandTextForTriggerForSpatialRecordsCountOnDelete(string layer, string triggerName, Dictionary<string, string> linkParams, Chain chain, string owner = "", string functionSchema = "");

        /// <summary>
        /// </summary>
        public abstract string InitialUpdateCommandForSpatialRecordsCount(string layer, Dictionary<string, string> linkParams, Chain chain, string owner = "");

        /// <summary>
        /// </summary>
        public abstract string CreateStoredProcedureForUnjoinSpatialCounter(string layer, string procedureName, Dictionary<string, string> linkParams, Chain chain, string owner = "");
        
        /// <summary>
        /// </summary>
        public abstract string CommandTextForTriggerForUpdateSpatialObject(string layer, string triggerName, 
            Dictionary<string, string> linkParams, bool clearWithoutLink, Chain chain,
            List<MapExpressionField> expressionFields, Dictionary<string, string> simpleFields, string owner = "", string functionSchema = "");

        /// <summary>
        /// </summary>
        public abstract string CommandTextForTriggerForInsertSpatialObject(string layer, string triggerName,
            Dictionary<string, string> linkParams, bool clearWithoutLink, Chain chain,
            List<MapExpressionField> expressionFields, Dictionary<string, string> simpleFields, string owner = "", string functionSchema = "");

        /// <summary>
        /// </summary>
        public abstract string Update(MapField field, string setCommand, string layer, Dictionary<string, string> linkParams);

        /// <summary>
        /// </summary>
        public abstract string DeleteTrigger(string layer, string triggerName, Type rootType,
            Dictionary<string, string> linkParams, List<MapExpressionField> expressionFields,
            Dictionary<string, MapSimpleField> simpleFields, string owner = "", string functionSchema = "");

        /// <summary>
        /// </summary>
        public abstract string UpdateTrigger(string layer, string triggerName, Type rootType,
            Dictionary<string, string> linkParams, string fromTableName, MapField field, string setCommand, string owner = "", string functionSchema = "");

        /// <summary>
        /// </summary>
        public abstract string SetStatement(MapSimpleField field);

        /// <summary>
        /// </summary>
        public abstract string SetStatement(MapExpressionField field);
    }
}
