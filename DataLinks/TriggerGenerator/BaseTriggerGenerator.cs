namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text.RegularExpressions;
    using ICSSoft.STORMNET;

    public abstract class BaseTriggerGenerator: ITriggerGenerator
    {
        /// <summary>
        /// Некритичные ошибки
        /// </summary>
        protected List<string> Warnings = new List<string>();

        /// <summary>
        /// Тип объекта для которого генерируются триггеры
        /// </summary>
        protected Type RootType { get; private set; }

        /// <summary>
        /// Имя таблицы в базе геоданных
        /// </summary>
        protected string Layer { get; private set; }

        /// <summary>
        /// Очищать без ссылки
        /// </summary>
        protected bool ClearWithoutLink { get; private set; }

        /// <summary>
        /// Поля привязки
        /// </summary>
        protected Dictionary<string, string> LinkParams { get; private set; }
        
        /// <summary>
        /// Суффикс генерируемых триггеров
        /// </summary>
        protected string TriggerKey { get; private set; }

        /// <summary>
        /// Префикс генерируемых триггеров
        /// </summary>
        protected string TriggerPrefix { get; private set; }

        /// <summary>
        /// Права
        /// </summary>
        protected string Owner { get; private set; }

        /// <summary>
        /// Начальная часть имени слоя - схема или бд со схемой
        /// </summary>
        [Obsolete("Следует использовать полное имя таблицы в данных link.LayerTable")]
        protected string LayerPrefix { get; private set; }
        
        /// <summary>
        /// </summary>
        public BaseTriggerGenerator(string layerPrefix, string triggerPrefix, string owner = "")
        {
            Owner = owner;
            TriggerPrefix = triggerPrefix;
            LayerPrefix = layerPrefix;
        }

        public virtual void InitByDataLink(DataLink link)
        {
            if (link == null || link.DataLinkParameter == null || link.DataLinkParameter.Count == 0 ||
                !link.DataLinkParameter.Cast<DataLinkParameter>().Any(x => x.LinkField))
            {
                throw new Exception("Нельзя генерировать триггеры без указания настроек и полей связи");
            }
            ClearWithoutLink = link.ClearWithoutLink;
            Layer = $@"{LayerPrefix}{link.LayerTable}";

            RootType = TGHelper.ObjectType(link);

            LinkParams = new Dictionary<string, string>();
            foreach (var linkParam in link.DataLinkParameter.GetAllObjects().Cast<DataLinkParameter>().Where(p => p.LinkField))
            {
                if (!string.IsNullOrEmpty(linkParam.ObjectField))
                {
                    if (Information.CheckPropertyExist(RootType, linkParam.ObjectField))
                    {
                        var propertyType = Information.GetPropertyType(RootType, linkParam.ObjectField);
                        var databaseFieldName = propertyType.IsSubclassOf(typeof(DataObject))
                            ? Information.GetPropertyStorageName(RootType, linkParam.ObjectField, 0)
                            : Information.GetPropertyStorageName(RootType, linkParam.ObjectField);
                        LinkParams.Add(linkParam.LayerField, TGHelper.DataBaseHelper.Format(databaseFieldName));
                    }
                    else
                    {
                        LogService.LogWarn($"Поле {linkParam.ObjectField} не найдено в классе {RootType.FullName}");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(linkParam.Expression))
                    {
                        var expression = linkParam.Expression;
                        var match = Regex.Match(linkParam.Expression, @"@\b(\w+\.{0,1})+\b@");
                        while (match.Success)
                        {
                            var field = match.Value.Trim('@');
                            if (Information.CheckPropertyExist(RootType, field))
                            {
                                var propertyType = Information.GetPropertyType(RootType, field);
                                var databaseFieldName = propertyType.IsSubclassOf(typeof(DataObject))
                                    ? Information.GetPropertyStorageName(RootType, field, 0)
                                    : Information.GetPropertyStorageName(RootType, field);

                                expression = expression.Replace(match.Value, TGHelper.DataBaseHelper.Format(databaseFieldName));
                            }
                            else
                            {
                                LogService.LogWarn($"Поле {field} не найдено в классе {RootType.FullName}");
                            }

                            match = match.NextMatch();
                        }

                        LinkParams.Add(linkParam.LayerField, expression);
                    }
                }
            }

            TriggerKey = Regex.Replace(link.__PrimaryKey.ToString(), @"[^a-zA-Z]", string.Empty);
            if (TriggerKey.Length > 20) TriggerKey = TriggerKey.Substring(0, 20);
        }
                

        public abstract void GenerateTriggers(IDbCommand sqlCommand);

        public abstract void DropTriggers(IDbCommand sqlCommand);
    }
}
