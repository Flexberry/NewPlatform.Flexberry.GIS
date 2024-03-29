﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewPlatform.Flexberry.GIS
{
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business.Audit;


    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Layer metadata.
    /// </summary>
    // *** Start programmer edit section *** (LayerMetadata CustomAttributes)

    // *** End programmer edit section *** (LayerMetadata CustomAttributes)
    [AutoAltered()]
    [Caption("Layer metadata")]
    [AccessType(ICSSoft.STORMNET.AccessType.@this)]
    [View("AuditView", new string[] {
            "Name as \'Наименование\'",
            "Additionaldata as \'Дополнительные данные\'",
            "Creator as \'Создатель\'",
            "CreateTime as \'Время создания\'",
            "Editor as \'Редактор\'",
            "EditTime as \'Время редактирования\'"})]
    [AssociatedDetailViewAttribute("AuditView", "LinkMetadata", "AuditView", true, "", "", true, new string[] {
            ""})]
    [View("LayerMetadataE", new string[] {
            "Name as \'Наименование\'",
            "Description as \'Описание\'",
            "KeyWords as \'Ключевые слова\'",
            "Type as \'Тип\'",
            "Settings as \'Настройки\'",
            "Scale as \'Масштаб\'",
            "CoordinateReferenceSystem as \'Система координат\'",
            "BoundingBox as \'Граница\'",
			"Additionaldata as \'Дополнительные данные\'"})]
    [AssociatedDetailViewAttribute("LayerMetadataE", "LinkMetadata", "LinkMetadataD", true, "", "", true, new string[] {
            ""})]
    [View("LayerMetadataL", new string[] {
            "Name as \'Наименование\'",
            "Description as \'Описание\'",
            "Type as \'Тип\'"})]
    public class LayerMetadata : ICSSoft.STORMNET.DataObject, IDataObjectWithAuditFields
    {

        private string fName;

        private string fDescription;

        private string fKeyWords;

        private string fType;

        private string fSettings;

        private int fScale;

        private string fCoordinateReferenceSystem;

        private Microsoft.Spatial.Geography fBoundingBox;

		private string fAdditionaldata;

        private System.Nullable<System.DateTime> fCreateTime;

        private string fCreator;

        private System.Nullable<System.DateTime> fEditTime;

        private string fEditor;

        private NewPlatform.Flexberry.GIS.DetailArrayOfLinkMetadata fLinkMetadata;

        // *** Start programmer edit section *** (LayerMetadata CustomMembers)

        // *** End programmer edit section *** (LayerMetadata CustomMembers)


        /// <summary>
        /// Наименование слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Name CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Name CustomAttributes)
        [StrLen(255)]
        [NotNull()]
        public virtual string Name
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Name Get start)

                // *** End programmer edit section *** (LayerMetadata.Name Get start)
                string result = this.fName;
                // *** Start programmer edit section *** (LayerMetadata.Name Get end)

                // *** End programmer edit section *** (LayerMetadata.Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Name Set start)

                // *** End programmer edit section *** (LayerMetadata.Name Set start)
                this.fName = value;
                // *** Start programmer edit section *** (LayerMetadata.Name Set end)

                // *** End programmer edit section *** (LayerMetadata.Name Set end)
            }
        }

        /// <summary>
        /// Описание слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Description CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Description CustomAttributes)
        public virtual string Description
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Description Get start)

                // *** End programmer edit section *** (LayerMetadata.Description Get start)
                string result = this.fDescription;
                // *** Start programmer edit section *** (LayerMetadata.Description Get end)

                // *** End programmer edit section *** (LayerMetadata.Description Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Description Set start)

                // *** End programmer edit section *** (LayerMetadata.Description Set start)
                this.fDescription = value;
                // *** Start programmer edit section *** (LayerMetadata.Description Set end)

                // *** End programmer edit section *** (LayerMetadata.Description Set end)
            }
        }

        /// <summary>
        /// Ключевые слова имеющие отношение к слою или его тематике.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.KeyWords CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.KeyWords CustomAttributes)
        public virtual string KeyWords
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.KeyWords Get start)

                // *** End programmer edit section *** (LayerMetadata.KeyWords Get start)
                string result = this.fKeyWords;
                // *** Start programmer edit section *** (LayerMetadata.KeyWords Get end)

                // *** End programmer edit section *** (LayerMetadata.KeyWords Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.KeyWords Set start)

                // *** End programmer edit section *** (LayerMetadata.KeyWords Set start)
                this.fKeyWords = value;
                // *** Start programmer edit section *** (LayerMetadata.KeyWords Set end)

                // *** End programmer edit section *** (LayerMetadata.KeyWords Set end)
            }
        }

        /// <summary>
        /// Вычислимое поле для полнотекстового поиска ключевым словам, наименованию и описанию карты.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.AnyText CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.AnyText CustomAttributes)
        [ICSSoft.STORMNET.NotStored()]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.SQLDataService), "ISNULL(@Name@, \'\') + \' \' + ISNULL(@Description@, \'\') + \' \' + REPLACE(ISNULL(@KeyW" +
            "ords@, \'\'), \',\', \' \')")]
        [DataServiceExpression("ICSSoft.STORMNET.Business.MSSQLDataService, ICSSoft.STORMNET.Business.MSSQLDataService", "ISNULL(@Name@, \'\') + \' \' + ISNULL(@Description@, \'\') + \' \' + REPLACE(ISNULL(@KeyW" +
            "ords@, \'\'), \',\', \' \')")]
        [DataServiceExpression("ICSSoft.STORMNET.Business.PostgresDataService, ICSSoft.STORMNET.Business.PostgresDataService", "COALESCE(@Name@, \'\') || \' \' || COALESCE(@Description@, \'\') || \' \' || REPLACE(COAL" +
            "ESCE(@KeyWords@, \'\'), \',\', \' \')")]
        [DataServiceExpression("ICSSoft.STORMNET.Business.OracleDataService, ICSSoft.STORMNET.Business.PostgresDataService", "COALESCE(@Name@, \\\'\\\') || \\\' \\\' || COALESCE(@Description@, \\\'\\\') || \\\' \\\' || REPL" +
            "ACE(COALESCE(@KeyWords@, \\\'\\\'), \\\',\\\', \\\' \\\')")]
        public virtual string AnyText
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.AnyText Get)
                return $"{Name ?? string.Empty} {Description ?? string.Empty} {(KeyWords ?? string.Empty).Replace(",", " ")}";
                // *** End programmer edit section *** (LayerMetadata.AnyText Get)
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.AnyText Set)

                // *** End programmer edit section *** (LayerMetadata.AnyText Set)
            }
        }

        /// <summary>
        /// Тип слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Type CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Type CustomAttributes)
        [StrLen(255)]
        [NotNull()]
        public virtual string Type
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Type Get start)

                // *** End programmer edit section *** (LayerMetadata.Type Get start)
                string result = this.fType;
                // *** Start programmer edit section *** (LayerMetadata.Type Get end)

                // *** End programmer edit section *** (LayerMetadata.Type Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Type Set start)

                // *** End programmer edit section *** (LayerMetadata.Type Set start)
                this.fType = value;
                // *** Start programmer edit section *** (LayerMetadata.Type Set end)

                // *** End programmer edit section *** (LayerMetadata.Type Set end)
            }
        }

        /// <summary>
        /// Настройки слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Settings CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Settings CustomAttributes)
        public virtual string Settings
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Settings Get start)

                // *** End programmer edit section *** (LayerMetadata.Settings Get start)
                string result = this.fSettings;
                // *** Start programmer edit section *** (LayerMetadata.Settings Get end)

                // *** End programmer edit section *** (LayerMetadata.Settings Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Settings Set start)

                // *** End programmer edit section *** (LayerMetadata.Settings Set start)
                this.fSettings = value;
                // *** Start programmer edit section *** (LayerMetadata.Settings Set end)

                // *** End programmer edit section *** (LayerMetadata.Settings Set end)
            }
        }

        /// <summary>
        /// Масштаб или точность данных слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Scale CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Scale CustomAttributes)
        public virtual int Scale
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Scale Get start)

                // *** End programmer edit section *** (LayerMetadata.Scale Get start)
                int result = this.fScale;
                // *** Start programmer edit section *** (LayerMetadata.Scale Get end)

                // *** End programmer edit section *** (LayerMetadata.Scale Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Scale Set start)

                // *** End programmer edit section *** (LayerMetadata.Scale Set start)
                this.fScale = value;
                // *** Start programmer edit section *** (LayerMetadata.Scale Set end)

                // *** End programmer edit section *** (LayerMetadata.Scale Set end)
            }
        }

        /// <summary>
        /// Система координат слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.CoordinateReferenceSystem CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.CoordinateReferenceSystem CustomAttributes)
        [StrLen(255)]
        public virtual string CoordinateReferenceSystem
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Get start)

                // *** End programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Get start)
                string result = this.fCoordinateReferenceSystem;
                // *** Start programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Get end)

                // *** End programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Set start)

                // *** End programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Set start)
                this.fCoordinateReferenceSystem = value;
                // *** Start programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Set end)

                // *** End programmer edit section *** (LayerMetadata.CoordinateReferenceSystem Set end)
            }
        }

        /// <summary>
        /// Границы слоя.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.BoundingBox CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.BoundingBox CustomAttributes)
        public virtual Microsoft.Spatial.Geography BoundingBox
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.BoundingBox Get start)

                // *** End programmer edit section *** (LayerMetadata.BoundingBox Get start)
                Microsoft.Spatial.Geography result = this.fBoundingBox;
                // *** Start programmer edit section *** (LayerMetadata.BoundingBox Get end)

                // *** End programmer edit section *** (LayerMetadata.BoundingBox Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.BoundingBox Set start)

                // *** End programmer edit section *** (LayerMetadata.BoundingBox Set start)
                this.fBoundingBox = value;
                // *** Start programmer edit section *** (LayerMetadata.BoundingBox Set end)

                // *** End programmer edit section *** (LayerMetadata.BoundingBox Set end)
            }
        }
        
        /// <summary>
        /// Additionaldata.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Additionaldata CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Additionaldata CustomAttributes)
        public virtual string Additionaldata
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Additionaldata Get start)

                // *** End programmer edit section *** (LayerMetadata.Additionaldata Get start)
                string result = this.fAdditionaldata;
                // *** Start programmer edit section *** (LayerMetadata.Additionaldata Get end)

                // *** End programmer edit section *** (LayerMetadata.Additionaldata Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Additionaldata Set start)

                // *** End programmer edit section *** (LayerMetadata.Additionaldata Set start)
                this.fAdditionaldata = value;
                // *** Start programmer edit section *** (LayerMetadata.Additionaldata Set end)

                // *** End programmer edit section *** (LayerMetadata.Additionaldata Set end)
            }
        }
        /// <summary>
        /// Время создания объекта.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.CreateTime CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.CreateTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> CreateTime
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.CreateTime Get start)

                // *** End programmer edit section *** (LayerMetadata.CreateTime Get start)
                System.Nullable<System.DateTime> result = this.fCreateTime;
                // *** Start programmer edit section *** (LayerMetadata.CreateTime Get end)

                // *** End programmer edit section *** (LayerMetadata.CreateTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.CreateTime Set start)

                // *** End programmer edit section *** (LayerMetadata.CreateTime Set start)
                this.fCreateTime = value;
                // *** Start programmer edit section *** (LayerMetadata.CreateTime Set end)

                // *** End programmer edit section *** (LayerMetadata.CreateTime Set end)
            }
        }

        /// <summary>
        /// Создатель объекта.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Creator CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Creator CustomAttributes)
        [StrLen(255)]
        public virtual string Creator
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Creator Get start)

                // *** End programmer edit section *** (LayerMetadata.Creator Get start)
                string result = this.fCreator;
                // *** Start programmer edit section *** (LayerMetadata.Creator Get end)

                // *** End programmer edit section *** (LayerMetadata.Creator Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Creator Set start)

                // *** End programmer edit section *** (LayerMetadata.Creator Set start)
                this.fCreator = value;
                // *** Start programmer edit section *** (LayerMetadata.Creator Set end)

                // *** End programmer edit section *** (LayerMetadata.Creator Set end)
            }
        }

        /// <summary>
        /// Время последнего редактирования объекта.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.EditTime CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.EditTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> EditTime
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.EditTime Get start)

                // *** End programmer edit section *** (LayerMetadata.EditTime Get start)
                System.Nullable<System.DateTime> result = this.fEditTime;
                // *** Start programmer edit section *** (LayerMetadata.EditTime Get end)

                // *** End programmer edit section *** (LayerMetadata.EditTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.EditTime Set start)

                // *** End programmer edit section *** (LayerMetadata.EditTime Set start)
                this.fEditTime = value;
                // *** Start programmer edit section *** (LayerMetadata.EditTime Set end)

                // *** End programmer edit section *** (LayerMetadata.EditTime Set end)
            }
        }

        /// <summary>
        /// Последний редактор объекта.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.Editor CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.Editor CustomAttributes)
        [StrLen(255)]
        public virtual string Editor
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.Editor Get start)

                // *** End programmer edit section *** (LayerMetadata.Editor Get start)
                string result = this.fEditor;
                // *** Start programmer edit section *** (LayerMetadata.Editor Get end)

                // *** End programmer edit section *** (LayerMetadata.Editor Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.Editor Set start)

                // *** End programmer edit section *** (LayerMetadata.Editor Set start)
                this.fEditor = value;
                // *** Start programmer edit section *** (LayerMetadata.Editor Set end)

                // *** End programmer edit section *** (LayerMetadata.Editor Set end)
            }
        }

        /// <summary>
        /// Layer metadata.
        /// </summary>
        // *** Start programmer edit section *** (LayerMetadata.LinkMetadata CustomAttributes)

        // *** End programmer edit section *** (LayerMetadata.LinkMetadata CustomAttributes)
        public virtual NewPlatform.Flexberry.GIS.DetailArrayOfLinkMetadata LinkMetadata
        {
            get
            {
                // *** Start programmer edit section *** (LayerMetadata.LinkMetadata Get start)

                // *** End programmer edit section *** (LayerMetadata.LinkMetadata Get start)
                if ((this.fLinkMetadata == null))
                {
                    this.fLinkMetadata = new NewPlatform.Flexberry.GIS.DetailArrayOfLinkMetadata(this);
                }
                NewPlatform.Flexberry.GIS.DetailArrayOfLinkMetadata result = this.fLinkMetadata;
                // *** Start programmer edit section *** (LayerMetadata.LinkMetadata Get end)

                // *** End programmer edit section *** (LayerMetadata.LinkMetadata Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (LayerMetadata.LinkMetadata Set start)

                // *** End programmer edit section *** (LayerMetadata.LinkMetadata Set start)
                this.fLinkMetadata = value;
                // *** Start programmer edit section *** (LayerMetadata.LinkMetadata Set end)

                // *** End programmer edit section *** (LayerMetadata.LinkMetadata Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// Представление для аудита.
            /// </summary>
            public static ICSSoft.STORMNET.View AuditView
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("AuditView", typeof(NewPlatform.Flexberry.GIS.LayerMetadata));
                }
            }

            /// <summary>
            /// Представление для форм редактирования.
            /// </summary>
            public static ICSSoft.STORMNET.View LayerMetadataE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("LayerMetadataE", typeof(NewPlatform.Flexberry.GIS.LayerMetadata));
                }
            }

            /// <summary>
            /// Представление для списковых форм.
            /// </summary>
            public static ICSSoft.STORMNET.View LayerMetadataL
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("LayerMetadataL", typeof(NewPlatform.Flexberry.GIS.LayerMetadata));
                }
            }
        }

        /// <summary>
        /// Audit class settings.
        /// </summary>
        public class AuditSettings
        {

            /// <summary>
            /// Включён ли аудит для класса.
            /// </summary>
            public static bool AuditEnabled = true;

            /// <summary>
            /// Использовать имя представления для аудита по умолчанию.
            /// </summary>
            public static bool UseDefaultView = false;

            /// <summary>
            /// Включён ли аудит операции чтения.
            /// </summary>
            public static bool SelectAudit = false;

            /// <summary>
            /// Имя представления для аудирования операции чтения.
            /// </summary>
            public static string SelectAuditViewName = "AuditView";

            /// <summary>
            /// Включён ли аудит операции создания.
            /// </summary>
            public static bool InsertAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции создания.
            /// </summary>
            public static string InsertAuditViewName = "AuditView";

            /// <summary>
            /// Включён ли аудит операции изменения.
            /// </summary>
            public static bool UpdateAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции изменения.
            /// </summary>
            public static string UpdateAuditViewName = "AuditView";

            /// <summary>
            /// Включён ли аудит операции удаления.
            /// </summary>
            public static bool DeleteAudit = true;

            /// <summary>
            /// Имя представления для аудирования операции удаления.
            /// </summary>
            public static string DeleteAuditViewName = "AuditView";

            /// <summary>
            /// Путь к форме просмотра результатов аудита.
            /// </summary>
            public static string FormUrl = "";

            /// <summary>
            /// Режим записи данных аудита (синхронный или асинхронный).
            /// </summary>
            public static ICSSoft.STORMNET.Business.Audit.Objects.tWriteMode WriteMode = ICSSoft.STORMNET.Business.Audit.Objects.tWriteMode.Synchronous;

            /// <summary>
            /// Максимальная длина сохраняемого значения поля (если 0, то строка обрезаться не будет).
            /// </summary>
            public static int PrunningLength = 0;

            /// <summary>
            /// Показывать ли пользователям в изменениях первичные ключи.
            /// </summary>
            public static bool ShowPrimaryKey = false;

            /// <summary>
            /// Сохранять ли старое значение.
            /// </summary>
            public static bool KeepOldValue = true;

            /// <summary>
            /// Сжимать ли сохраняемые значения.
            /// </summary>
            public static bool Compress = false;

            /// <summary>
            /// Сохранять ли все значения атрибутов, а не только изменяемые.
            /// </summary>
            public static bool KeepAllValues = false;
        }
    }
}
