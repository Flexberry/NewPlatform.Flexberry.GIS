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
    using System;
    using System.Xml;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business.Audit;
    using ICSSoft.STORMNET.Business.Audit.Objects;
    
    
    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// Map.
    /// </summary>
    // *** Start programmer edit section *** (Map CustomAttributes)

    // *** End programmer edit section *** (Map CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.@this)]
    [View("AuditView", new string[] {
            "Name as \'Наименование\'",
            "Creator as \'Создатель\'",
            "CreateTime as \'Время создания\'",
            "Editor as \'Редактор\'",
            "EditTime as \'Время редактирования\'"})]
    [View("Map", new string[] {
            "Name as \'Наименование\'",
            "Lat as \'Широта\'",
            "Lng as \'Долгота\'",
            "Zoom as \'Зум\'",
            "Public as \'Общая\'",
            "CoordinateReferenceSystem as \'Система координат\'"}, Hidden=new string[] {
            "Lat",
            "Lng",
            "Zoom",
            "Public",
            "CoordinateReferenceSystem"})]
    [View("MapE", new string[] {
            "Name as \'Наименование\'",
            "Description as \'Описание\'",
            "KeyWords as \'Ключевые слова\'",
            "Lat as \'Широта\'",
            "Lng as \'Долгота\'",
            "Zoom as \'Зум\'",
            "Public as \'Общая\'",
            "Scale as \'Масштаб\'",
            "CoordinateReferenceSystem as \'Система координат\'",
            "BoundingBox as \'Граница\'",
            "Picture as \'Изображение\'"})]
    [AssociatedDetailViewAttribute("MapE", "MapLayer", "MapLayerD", true, "", "", true, new string[] {
            ""})]
    [View("MapL", new string[] {
            "Name as \'Наименование\'",
            "Lat as \'Широта\'",
            "Lng as \'Долгота\'",
            "Zoom as \'Зум\'",
            "Public as \'Общая\'",
            "Picture as \'Изображение\'"})]
    public class Map : ICSSoft.STORMNET.DataObject, NewPlatform.Flexberry.GIS.IPublicOwner, IDataObjectWithAuditFields
    {
        
        private System.Nullable<System.DateTime> fCreateTime;
        
        private string fCreator;
        
        private System.Nullable<System.DateTime> fEditTime;
        
        private string fEditor;
        
        private string fName;
        
        private string fDescription;
        
        private string fKeyWords;
        
        private double fLat;
        
        private double fLng;
        
        private double fZoom;
        
        private bool fPublic;
        
        private int fScale;
        
        private string fCoordinateReferenceSystem;
        
        private Microsoft.Spatial.Geography fBoundingBox;
        
        private string fOwner;
        
        private string fPicture;
        
        private NewPlatform.Flexberry.GIS.DetailArrayOfMapLayer fMapLayer;
        
        // *** Start programmer edit section *** (Map CustomMembers)

        // *** End programmer edit section *** (Map CustomMembers)

        
        /// <summary>
        /// Время создания объекта.
        /// </summary>
        // *** Start programmer edit section *** (Map.CreateTime CustomAttributes)

        // *** End programmer edit section *** (Map.CreateTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> CreateTime
        {
            get
            {
                // *** Start programmer edit section *** (Map.CreateTime Get start)

                // *** End programmer edit section *** (Map.CreateTime Get start)
                System.Nullable<System.DateTime> result = this.fCreateTime;
                // *** Start programmer edit section *** (Map.CreateTime Get end)

                // *** End programmer edit section *** (Map.CreateTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.CreateTime Set start)

                // *** End programmer edit section *** (Map.CreateTime Set start)
                this.fCreateTime = value;
                // *** Start programmer edit section *** (Map.CreateTime Set end)

                // *** End programmer edit section *** (Map.CreateTime Set end)
            }
        }
        
        /// <summary>
        /// Создатель объекта.
        /// </summary>
        // *** Start programmer edit section *** (Map.Creator CustomAttributes)

        // *** End programmer edit section *** (Map.Creator CustomAttributes)
        [StrLen(255)]
        public virtual string Creator
        {
            get
            {
                // *** Start programmer edit section *** (Map.Creator Get start)

                // *** End programmer edit section *** (Map.Creator Get start)
                string result = this.fCreator;
                // *** Start programmer edit section *** (Map.Creator Get end)

                // *** End programmer edit section *** (Map.Creator Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Creator Set start)

                // *** End programmer edit section *** (Map.Creator Set start)
                this.fCreator = value;
                // *** Start programmer edit section *** (Map.Creator Set end)

                // *** End programmer edit section *** (Map.Creator Set end)
            }
        }
        
        /// <summary>
        /// Время последнего редактирования объекта.
        /// </summary>
        // *** Start programmer edit section *** (Map.EditTime CustomAttributes)

        // *** End programmer edit section *** (Map.EditTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> EditTime
        {
            get
            {
                // *** Start programmer edit section *** (Map.EditTime Get start)

                // *** End programmer edit section *** (Map.EditTime Get start)
                System.Nullable<System.DateTime> result = this.fEditTime;
                // *** Start programmer edit section *** (Map.EditTime Get end)

                // *** End programmer edit section *** (Map.EditTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.EditTime Set start)

                // *** End programmer edit section *** (Map.EditTime Set start)
                this.fEditTime = value;
                // *** Start programmer edit section *** (Map.EditTime Set end)

                // *** End programmer edit section *** (Map.EditTime Set end)
            }
        }
        
        /// <summary>
        /// Последний редактор объекта.
        /// </summary>
        // *** Start programmer edit section *** (Map.Editor CustomAttributes)

        // *** End programmer edit section *** (Map.Editor CustomAttributes)
        [StrLen(255)]
        public virtual string Editor
        {
            get
            {
                // *** Start programmer edit section *** (Map.Editor Get start)

                // *** End programmer edit section *** (Map.Editor Get start)
                string result = this.fEditor;
                // *** Start programmer edit section *** (Map.Editor Get end)

                // *** End programmer edit section *** (Map.Editor Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Editor Set start)

                // *** End programmer edit section *** (Map.Editor Set start)
                this.fEditor = value;
                // *** Start programmer edit section *** (Map.Editor Set end)

                // *** End programmer edit section *** (Map.Editor Set end)
            }
        }
        
        /// <summary>
        /// Наименование карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Name CustomAttributes)

        // *** End programmer edit section *** (Map.Name CustomAttributes)
        [StrLen(255)]
        [NotNull()]
        public virtual string Name
        {
            get
            {
                // *** Start programmer edit section *** (Map.Name Get start)

                // *** End programmer edit section *** (Map.Name Get start)
                string result = this.fName;
                // *** Start programmer edit section *** (Map.Name Get end)

                // *** End programmer edit section *** (Map.Name Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Name Set start)

                // *** End programmer edit section *** (Map.Name Set start)
                this.fName = value;
                // *** Start programmer edit section *** (Map.Name Set end)

                // *** End programmer edit section *** (Map.Name Set end)
            }
        }
        
        /// <summary>
        /// Описание карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Description CustomAttributes)

        // *** End programmer edit section *** (Map.Description CustomAttributes)
        public virtual string Description
        {
            get
            {
                // *** Start programmer edit section *** (Map.Description Get start)

                // *** End programmer edit section *** (Map.Description Get start)
                string result = this.fDescription;
                // *** Start programmer edit section *** (Map.Description Get end)

                // *** End programmer edit section *** (Map.Description Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Description Set start)

                // *** End programmer edit section *** (Map.Description Set start)
                this.fDescription = value;
                // *** Start programmer edit section *** (Map.Description Set end)

                // *** End programmer edit section *** (Map.Description Set end)
            }
        }
        
        /// <summary>
        /// Ключевые слова имеющие отношение к карте или её тематике.
        /// </summary>
        // *** Start programmer edit section *** (Map.KeyWords CustomAttributes)

        // *** End programmer edit section *** (Map.KeyWords CustomAttributes)
        public virtual string KeyWords
        {
            get
            {
                // *** Start programmer edit section *** (Map.KeyWords Get start)

                // *** End programmer edit section *** (Map.KeyWords Get start)
                string result = this.fKeyWords;
                // *** Start programmer edit section *** (Map.KeyWords Get end)

                // *** End programmer edit section *** (Map.KeyWords Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.KeyWords Set start)

                // *** End programmer edit section *** (Map.KeyWords Set start)
                this.fKeyWords = value;
                // *** Start programmer edit section *** (Map.KeyWords Set end)

                // *** End programmer edit section *** (Map.KeyWords Set end)
            }
        }
        
        /// <summary>
        /// Вычислимое поле для полнотекстового поиска ключевым словам, наименованию и описанию карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.AnyText CustomAttributes)

        // *** End programmer edit section *** (Map.AnyText CustomAttributes)
        [ICSSoft.STORMNET.NotStored()]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.MSSQLDataService, ICSSoft.STORMNET.Business.MSSQLDataService), "ISNULL(@Name@, \'\') + \' \' + ISNULL(@Description@, \'\') + \' \' + REPLACE(ISNULL(@KeyW" +
            "ords@, \'\'), \',\', \' \')")]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.OracleDataService, ICSSoft.STORMNET.Business.MSSQLDataService), "COALESCE(@Name@, \\\'\\\') || \\\' \\\' || COALESCE(@Description@, \\\'\\\') || \\\' \\\' || REPL" +
            "ACE(COALESCE(@KeyWords@, \\\'\\\'), \\\',\\\', \\\' \\\')")]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.PostgresDataService, ICSSoft.STORMNET.Business.PostgresDataService), "COALESCE(@Name@, \'\') || \' \' || COALESCE(@Description@, \'\') || \' \' || REPLACE(COAL" +
            "ESCE(@KeyWords@, \'\'), \',\', \' \')")]
        [DataServiceExpression(typeof(ICSSoft.STORMNET.Business.SQLDataService), "ISNULL(@Name@, \'\') + \' \' + ISNULL(@Description@, \'\') + \' \' + REPLACE(ISNULL(@KeyW" +
            "ords@, \'\'), \',\', \' \')")]
        public virtual string AnyText
        {
            get
            {
                // *** Start programmer edit section *** (Map.AnyText Get)
                return null;
                // *** End programmer edit section *** (Map.AnyText Get)
            }
            set
            {
                // *** Start programmer edit section *** (Map.AnyText Set)

                // *** End programmer edit section *** (Map.AnyText Set)
            }
        }
        
        /// <summary>
        /// Широта центра карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Lat CustomAttributes)

        // *** End programmer edit section *** (Map.Lat CustomAttributes)
        public virtual double Lat
        {
            get
            {
                // *** Start programmer edit section *** (Map.Lat Get start)

                // *** End programmer edit section *** (Map.Lat Get start)
                double result = this.fLat;
                // *** Start programmer edit section *** (Map.Lat Get end)

                // *** End programmer edit section *** (Map.Lat Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Lat Set start)

                // *** End programmer edit section *** (Map.Lat Set start)
                this.fLat = value;
                // *** Start programmer edit section *** (Map.Lat Set end)

                // *** End programmer edit section *** (Map.Lat Set end)
            }
        }
        
        /// <summary>
        /// Долгота центра карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Lng CustomAttributes)

        // *** End programmer edit section *** (Map.Lng CustomAttributes)
        public virtual double Lng
        {
            get
            {
                // *** Start programmer edit section *** (Map.Lng Get start)

                // *** End programmer edit section *** (Map.Lng Get start)
                double result = this.fLng;
                // *** Start programmer edit section *** (Map.Lng Get end)

                // *** End programmer edit section *** (Map.Lng Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Lng Set start)

                // *** End programmer edit section *** (Map.Lng Set start)
                this.fLng = value;
                // *** Start programmer edit section *** (Map.Lng Set end)

                // *** End programmer edit section *** (Map.Lng Set end)
            }
        }
        
        /// <summary>
        /// Зум карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Zoom CustomAttributes)

        // *** End programmer edit section *** (Map.Zoom CustomAttributes)
        public virtual double Zoom
        {
            get
            {
                // *** Start programmer edit section *** (Map.Zoom Get start)

                // *** End programmer edit section *** (Map.Zoom Get start)
                double result = this.fZoom;
                // *** Start programmer edit section *** (Map.Zoom Get end)

                // *** End programmer edit section *** (Map.Zoom Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Zoom Set start)

                // *** End programmer edit section *** (Map.Zoom Set start)
                this.fZoom = value;
                // *** Start programmer edit section *** (Map.Zoom Set end)

                // *** End programmer edit section *** (Map.Zoom Set end)
            }
        }
        
        /// <summary>
        /// Флаг общедоступности карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Public CustomAttributes)

        // *** End programmer edit section *** (Map.Public CustomAttributes)
        [NotNull()]
        public virtual bool Public
        {
            get
            {
                // *** Start programmer edit section *** (Map.Public Get start)

                // *** End programmer edit section *** (Map.Public Get start)
                bool result = this.fPublic;
                // *** Start programmer edit section *** (Map.Public Get end)

                // *** End programmer edit section *** (Map.Public Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Public Set start)

                // *** End programmer edit section *** (Map.Public Set start)
                this.fPublic = value;
                // *** Start programmer edit section *** (Map.Public Set end)

                // *** End programmer edit section *** (Map.Public Set end)
            }
        }
        
        /// <summary>
        /// Масштаб или точность данных карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.Scale CustomAttributes)

        // *** End programmer edit section *** (Map.Scale CustomAttributes)
        public virtual int Scale
        {
            get
            {
                // *** Start programmer edit section *** (Map.Scale Get start)

                // *** End programmer edit section *** (Map.Scale Get start)
                int result = this.fScale;
                // *** Start programmer edit section *** (Map.Scale Get end)

                // *** End programmer edit section *** (Map.Scale Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Scale Set start)

                // *** End programmer edit section *** (Map.Scale Set start)
                this.fScale = value;
                // *** Start programmer edit section *** (Map.Scale Set end)

                // *** End programmer edit section *** (Map.Scale Set end)
            }
        }
        
        /// <summary>
        /// Система координат карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.CoordinateReferenceSystem CustomAttributes)

        // *** End programmer edit section *** (Map.CoordinateReferenceSystem CustomAttributes)
        [StrLen(255)]
        public virtual string CoordinateReferenceSystem
        {
            get
            {
                // *** Start programmer edit section *** (Map.CoordinateReferenceSystem Get start)

                // *** End programmer edit section *** (Map.CoordinateReferenceSystem Get start)
                string result = this.fCoordinateReferenceSystem;
                // *** Start programmer edit section *** (Map.CoordinateReferenceSystem Get end)

                // *** End programmer edit section *** (Map.CoordinateReferenceSystem Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.CoordinateReferenceSystem Set start)

                // *** End programmer edit section *** (Map.CoordinateReferenceSystem Set start)
                this.fCoordinateReferenceSystem = value;
                // *** Start programmer edit section *** (Map.CoordinateReferenceSystem Set end)

                // *** End programmer edit section *** (Map.CoordinateReferenceSystem Set end)
            }
        }
        
        /// <summary>
        /// Границы карты.
        /// </summary>
        // *** Start programmer edit section *** (Map.BoundingBox CustomAttributes)

        // *** End programmer edit section *** (Map.BoundingBox CustomAttributes)
        public virtual Microsoft.Spatial.Geography BoundingBox
        {
            get
            {
                // *** Start programmer edit section *** (Map.BoundingBox Get start)

                // *** End programmer edit section *** (Map.BoundingBox Get start)
                Microsoft.Spatial.Geography result = this.fBoundingBox;
                // *** Start programmer edit section *** (Map.BoundingBox Get end)

                // *** End programmer edit section *** (Map.BoundingBox Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.BoundingBox Set start)

                // *** End programmer edit section *** (Map.BoundingBox Set start)
                this.fBoundingBox = value;
                // *** Start programmer edit section *** (Map.BoundingBox Set end)

                // *** End programmer edit section *** (Map.BoundingBox Set end)
            }
        }
        
        /// <summary>
        /// Owner.
        /// </summary>
        // *** Start programmer edit section *** (Map.Owner CustomAttributes)

        // *** End programmer edit section *** (Map.Owner CustomAttributes)
        [StrLen(255)]
        public virtual string Owner
        {
            get
            {
                // *** Start programmer edit section *** (Map.Owner Get start)

                // *** End programmer edit section *** (Map.Owner Get start)
                string result = this.fOwner;
                // *** Start programmer edit section *** (Map.Owner Get end)

                // *** End programmer edit section *** (Map.Owner Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Owner Set start)

                // *** End programmer edit section *** (Map.Owner Set start)
                this.fOwner = value;
                // *** Start programmer edit section *** (Map.Owner Set end)

                // *** End programmer edit section *** (Map.Owner Set end)
            }
        }
        
        /// <summary>
        /// Picture.
        /// </summary>
        // *** Start programmer edit section *** (Map.Picture CustomAttributes)

        // *** End programmer edit section *** (Map.Picture CustomAttributes)
        [StrLen(255)]
        public virtual string Picture
        {
            get
            {
                // *** Start programmer edit section *** (Map.Picture Get start)

                // *** End programmer edit section *** (Map.Picture Get start)
                string result = this.fPicture;
                // *** Start programmer edit section *** (Map.Picture Get end)

                // *** End programmer edit section *** (Map.Picture Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.Picture Set start)

                // *** End programmer edit section *** (Map.Picture Set start)
                this.fPicture = value;
                // *** Start programmer edit section *** (Map.Picture Set end)

                // *** End programmer edit section *** (Map.Picture Set end)
            }
        }
        
        /// <summary>
        /// Map.
        /// </summary>
        // *** Start programmer edit section *** (Map.MapLayer CustomAttributes)

        // *** End programmer edit section *** (Map.MapLayer CustomAttributes)
        public virtual NewPlatform.Flexberry.GIS.DetailArrayOfMapLayer MapLayer
        {
            get
            {
                // *** Start programmer edit section *** (Map.MapLayer Get start)

                // *** End programmer edit section *** (Map.MapLayer Get start)
                if ((this.fMapLayer == null))
                {
                    this.fMapLayer = new NewPlatform.Flexberry.GIS.DetailArrayOfMapLayer(this);
                }
                NewPlatform.Flexberry.GIS.DetailArrayOfMapLayer result = this.fMapLayer;
                // *** Start programmer edit section *** (Map.MapLayer Get end)

                // *** End programmer edit section *** (Map.MapLayer Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (Map.MapLayer Set start)

                // *** End programmer edit section *** (Map.MapLayer Set start)
                this.fMapLayer = value;
                // *** Start programmer edit section *** (Map.MapLayer Set end)

                // *** End programmer edit section *** (Map.MapLayer Set end)
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
                    return ICSSoft.STORMNET.Information.GetView("AuditView", typeof(NewPlatform.Flexberry.GIS.Map));
                }
            }
            
            /// <summary>
            /// Представление для прочих форм.
            /// </summary>
            public static ICSSoft.STORMNET.View Map
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("Map", typeof(NewPlatform.Flexberry.GIS.Map));
                }
            }
            
            /// <summary>
            /// Представление для форм редактирования.
            /// </summary>
            public static ICSSoft.STORMNET.View MapE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("MapE", typeof(NewPlatform.Flexberry.GIS.Map));
                }
            }
            
            /// <summary>
            /// Представление для списковых форм.
            /// </summary>
            public static ICSSoft.STORMNET.View MapL
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("MapL", typeof(NewPlatform.Flexberry.GIS.Map));
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
