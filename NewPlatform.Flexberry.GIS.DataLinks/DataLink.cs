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
    // *** Start programmer edit section *** (Using statements)
    using System.Data;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using NewPlatform.Flexberry.GIS.TriggerGenerator;
    using Unity;

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// DataLink.
    /// </summary>
    // *** Start programmer edit section *** (DataLink CustomAttributes)

    // *** End programmer edit section *** (DataLink CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.@this)]
    [View("DataLinkD", new string[] {
            "MapObjectSetting",
            "LayerTable as \'Таблица в геоданных\'",
            "CreateObject as \'Триггер на создание\'"}, Hidden = new string[] {
            "MapObjectSetting"})]
    [View("DataLinkE", new string[] {
            "MapObjectSetting",
            "LayerTable as \'Таблица в геоданных\'",
            "ClearWithoutLink as \'Очищать, если нет данных\'",
            "CreateObject as \'Триггер на создание\'"}, Hidden = new string[] {
            "MapObjectSetting"})]
    [AssociatedDetailViewAttribute("DataLinkE", "DataLinkParameter", "DataLinkParameterD", true, "", "", true, new string[] {
            ""})]
    public class DataLink : ICSSoft.STORMNET.DataObject
    {

        private bool fClearWithoutLink = false;

        private string fLayerTable;

        private bool fCreateObject = false;

        private NewPlatform.Flexberry.GIS.DetailArrayOfDataLinkParameter fDataLinkParameter;

        private NewPlatform.Flexberry.GIS.MapObjectSetting fMapObjectSetting;

        // *** Start programmer edit section *** (DataLink CustomMembers)

        /// <summary>
        /// Создать/обновить триггеры в атрибутивной БД
        /// </summary>
        public void UpdateAttributiveTriggers(IDbCommand sqlCommand)
        {
            try
            {
                var attributiveTriggerGenerator = UnityFactory.GetContainer().Resolve<ITriggerGenerator>("AttributiveTriggerGenerator");
                attributiveTriggerGenerator.InitByDataLink(this);
                attributiveTriggerGenerator.DropTriggers(sqlCommand);
                attributiveTriggerGenerator.GenerateTriggers(sqlCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Ошибка генерации триггеров атрибутивной БД для соединения со слоем {0}", LayerTable), exception);
            }
        }

        /// <summary>
        /// Создать/обновить триггеры в геоданных
        /// </summary>
        public void UpdateSpatialTriggers(IDbCommand sqlCommand)
        {
            try
            {
                var spatialTriggerGenerator = UnityFactory.GetContainer().Resolve<ITriggerGenerator>("SpatialTriggerGenerator");
                spatialTriggerGenerator.InitByDataLink(this);
                spatialTriggerGenerator.DropTriggers(sqlCommand);
                spatialTriggerGenerator.GenerateTriggers(sqlCommand);
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Ошибка генерации триггеров геоданных для слоя {0}", LayerTable), exception);
            }
        }

        // *** End programmer edit section *** (DataLink CustomMembers)


        /// <summary>
        /// ClearWithoutLink.
        /// </summary>
        // *** Start programmer edit section *** (DataLink.ClearWithoutLink CustomAttributes)

        // *** End programmer edit section *** (DataLink.ClearWithoutLink CustomAttributes)
        public virtual bool ClearWithoutLink
        {
            get
            {
                // *** Start programmer edit section *** (DataLink.ClearWithoutLink Get start)

                // *** End programmer edit section *** (DataLink.ClearWithoutLink Get start)
                bool result = this.fClearWithoutLink;
                // *** Start programmer edit section *** (DataLink.ClearWithoutLink Get end)

                // *** End programmer edit section *** (DataLink.ClearWithoutLink Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DataLink.ClearWithoutLink Set start)

                // *** End programmer edit section *** (DataLink.ClearWithoutLink Set start)
                this.fClearWithoutLink = value;
                // *** Start programmer edit section *** (DataLink.ClearWithoutLink Set end)

                // *** End programmer edit section *** (DataLink.ClearWithoutLink Set end)
            }
        }

        /// <summary>
        /// LayerTable.
        /// </summary>
        // *** Start programmer edit section *** (DataLink.LayerTable CustomAttributes)

        // *** End programmer edit section *** (DataLink.LayerTable CustomAttributes)
        [StrLen(255)]
        [NotNull()]
        public virtual string LayerTable
        {
            get
            {
                // *** Start programmer edit section *** (DataLink.LayerTable Get start)

                // *** End programmer edit section *** (DataLink.LayerTable Get start)
                string result = this.fLayerTable;
                // *** Start programmer edit section *** (DataLink.LayerTable Get end)

                // *** End programmer edit section *** (DataLink.LayerTable Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DataLink.LayerTable Set start)

                // *** End programmer edit section *** (DataLink.LayerTable Set start)
                this.fLayerTable = value;
                // *** Start programmer edit section *** (DataLink.LayerTable Set end)

                // *** End programmer edit section *** (DataLink.LayerTable Set end)
            }
        }

        /// <summary>
        /// CreateObject.
        /// </summary>
        // *** Start programmer edit section *** (DataLink.CreateObject CustomAttributes)

        // *** End programmer edit section *** (DataLink.CreateObject CustomAttributes)
        public virtual bool CreateObject
        {
            get
            {
                // *** Start programmer edit section *** (DataLink.CreateObject Get start)

                // *** End programmer edit section *** (DataLink.CreateObject Get start)
                bool result = this.fCreateObject;
                // *** Start programmer edit section *** (DataLink.CreateObject Get end)

                // *** End programmer edit section *** (DataLink.CreateObject Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DataLink.CreateObject Set start)

                // *** End programmer edit section *** (DataLink.CreateObject Set start)
                this.fCreateObject = value;
                // *** Start programmer edit section *** (DataLink.CreateObject Set end)

                // *** End programmer edit section *** (DataLink.CreateObject Set end)
            }
        }

        /// <summary>
        /// DataLink.
        /// </summary>
        // *** Start programmer edit section *** (DataLink.DataLinkParameter CustomAttributes)

        // *** End programmer edit section *** (DataLink.DataLinkParameter CustomAttributes)
        public virtual NewPlatform.Flexberry.GIS.DetailArrayOfDataLinkParameter DataLinkParameter
        {
            get
            {
                // *** Start programmer edit section *** (DataLink.DataLinkParameter Get start)

                // *** End programmer edit section *** (DataLink.DataLinkParameter Get start)
                if ((this.fDataLinkParameter == null))
                {
                    this.fDataLinkParameter = new NewPlatform.Flexberry.GIS.DetailArrayOfDataLinkParameter(this);
                }
                NewPlatform.Flexberry.GIS.DetailArrayOfDataLinkParameter result = this.fDataLinkParameter;
                // *** Start programmer edit section *** (DataLink.DataLinkParameter Get end)

                // *** End programmer edit section *** (DataLink.DataLinkParameter Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DataLink.DataLinkParameter Set start)

                // *** End programmer edit section *** (DataLink.DataLinkParameter Set start)
                this.fDataLinkParameter = value;
                // *** Start programmer edit section *** (DataLink.DataLinkParameter Set end)

                // *** End programmer edit section *** (DataLink.DataLinkParameter Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.GIS.MapObjectSetting.
        /// </summary>
        // *** Start programmer edit section *** (DataLink.MapObjectSetting CustomAttributes)

        // *** End programmer edit section *** (DataLink.MapObjectSetting CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "MapObjectSetting"})]
        public virtual NewPlatform.Flexberry.GIS.MapObjectSetting MapObjectSetting
        {
            get
            {
                // *** Start programmer edit section *** (DataLink.MapObjectSetting Get start)

                // *** End programmer edit section *** (DataLink.MapObjectSetting Get start)
                NewPlatform.Flexberry.GIS.MapObjectSetting result = this.fMapObjectSetting;
                // *** Start programmer edit section *** (DataLink.MapObjectSetting Get end)

                // *** End programmer edit section *** (DataLink.MapObjectSetting Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (DataLink.MapObjectSetting Set start)

                // *** End programmer edit section *** (DataLink.MapObjectSetting Set start)
                this.fMapObjectSetting = value;
                // *** Start programmer edit section *** (DataLink.MapObjectSetting Set end)

                // *** End programmer edit section *** (DataLink.MapObjectSetting Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "DataLinkD" view.
            /// </summary>
            public static ICSSoft.STORMNET.View DataLinkD
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("DataLinkD", typeof(NewPlatform.Flexberry.GIS.DataLink));
                }
            }

            /// <summary>
            /// "DataLinkE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View DataLinkE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("DataLinkE", typeof(NewPlatform.Flexberry.GIS.DataLink));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of DataLink.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfDataLink CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfDataLink CustomAttributes)
    public class DetailArrayOfDataLink : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.GIS.DetailArrayOfDataLink members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.GIS.DetailArrayOfDataLink members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type DataLink by index.
        /// </summary>
        /// <summary>
        /// Adds object with type DataLink.
        /// </summary>
        public DetailArrayOfDataLink(NewPlatform.Flexberry.GIS.MapObjectSetting fMapObjectSetting) :
                base(typeof(DataLink), ((ICSSoft.STORMNET.DataObject)(fMapObjectSetting)))
        {
        }

        public NewPlatform.Flexberry.GIS.DataLink this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.GIS.DataLink)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.GIS.DataLink dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}
