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
    
    
    // *** Start programmer edit section *** (Using statements)

    // *** End programmer edit section *** (Using statements)


    /// <summary>
    /// MapObjectSetting.
    /// </summary>
    // *** Start programmer edit section *** (MapObjectSetting CustomAttributes)

    // *** End programmer edit section *** (MapObjectSetting CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.@this)]
    [View("MapObjectSetting", new string[] {
            "*",
            "DefaultMap"}, Hidden=new string[] {
            "*"})]
    [View("MapObjectSettingE", new string[] {
            "TypeName as \'Тип\'",
            "Title as \'Отображаемое имя\'",
            "ListForm as \'Списковая форма\'",
            "EditForm as \'Форма редактирования\'",
            "MultEditForm as \'Форма группового создания\'",
            "DefaultMap as \'Карта по умолчанию\'",
            "DefaultMap.Name"}, Hidden=new string[] {
            "DefaultMap.Name"})]
    [MasterViewDefineAttribute("MapObjectSettingE", "DefaultMap", ICSSoft.STORMNET.LookupTypeEnum.Standard, "", "Name")]
    [View("MapObjectSettingL", new string[] {
            "TypeName as \'Тип\'",
            "ListForm as \'Списковая форма\'",
            "EditForm as \'Форма редактирования\'",
            "MultEditForm as \'Форма группового создания\'"})]
    public class MapObjectSetting : ICSSoft.STORMNET.DataObject
    {
        
        private string fTypeName;
        
        private string fListForm;
        
        private string fEditForm;
        
        private string fTitle;
        
        private string fMultEditForm;
        
        private NewPlatform.Flexberry.GIS.Map fDefaultMap;
        
        // *** Start programmer edit section *** (MapObjectSetting CustomMembers)

        // *** End programmer edit section *** (MapObjectSetting CustomMembers)

        
        /// <summary>
        /// TypeName.
        /// </summary>
        // *** Start programmer edit section *** (MapObjectSetting.TypeName CustomAttributes)

        // *** End programmer edit section *** (MapObjectSetting.TypeName CustomAttributes)
        [StrLen(255)]
        public virtual string TypeName
        {
            get
            {
                // *** Start programmer edit section *** (MapObjectSetting.TypeName Get start)

                // *** End programmer edit section *** (MapObjectSetting.TypeName Get start)
                string result = this.fTypeName;
                // *** Start programmer edit section *** (MapObjectSetting.TypeName Get end)

                // *** End programmer edit section *** (MapObjectSetting.TypeName Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (MapObjectSetting.TypeName Set start)

                // *** End programmer edit section *** (MapObjectSetting.TypeName Set start)
                this.fTypeName = value;
                // *** Start programmer edit section *** (MapObjectSetting.TypeName Set end)

                // *** End programmer edit section *** (MapObjectSetting.TypeName Set end)
            }
        }
        
        /// <summary>
        /// ListForm.
        /// </summary>
        // *** Start programmer edit section *** (MapObjectSetting.ListForm CustomAttributes)

        // *** End programmer edit section *** (MapObjectSetting.ListForm CustomAttributes)
        [StrLen(255)]
        public virtual string ListForm
        {
            get
            {
                // *** Start programmer edit section *** (MapObjectSetting.ListForm Get start)

                // *** End programmer edit section *** (MapObjectSetting.ListForm Get start)
                string result = this.fListForm;
                // *** Start programmer edit section *** (MapObjectSetting.ListForm Get end)

                // *** End programmer edit section *** (MapObjectSetting.ListForm Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (MapObjectSetting.ListForm Set start)

                // *** End programmer edit section *** (MapObjectSetting.ListForm Set start)
                this.fListForm = value;
                // *** Start programmer edit section *** (MapObjectSetting.ListForm Set end)

                // *** End programmer edit section *** (MapObjectSetting.ListForm Set end)
            }
        }
        
        /// <summary>
        /// EditForm.
        /// </summary>
        // *** Start programmer edit section *** (MapObjectSetting.EditForm CustomAttributes)

        // *** End programmer edit section *** (MapObjectSetting.EditForm CustomAttributes)
        [StrLen(255)]
        public virtual string EditForm
        {
            get
            {
                // *** Start programmer edit section *** (MapObjectSetting.EditForm Get start)

                // *** End programmer edit section *** (MapObjectSetting.EditForm Get start)
                string result = this.fEditForm;
                // *** Start programmer edit section *** (MapObjectSetting.EditForm Get end)

                // *** End programmer edit section *** (MapObjectSetting.EditForm Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (MapObjectSetting.EditForm Set start)

                // *** End programmer edit section *** (MapObjectSetting.EditForm Set start)
                this.fEditForm = value;
                // *** Start programmer edit section *** (MapObjectSetting.EditForm Set end)

                // *** End programmer edit section *** (MapObjectSetting.EditForm Set end)
            }
        }
        
        /// <summary>
        /// Title.
        /// </summary>
        // *** Start programmer edit section *** (MapObjectSetting.Title CustomAttributes)

        // *** End programmer edit section *** (MapObjectSetting.Title CustomAttributes)
        [StrLen(255)]
        public virtual string Title
        {
            get
            {
                // *** Start programmer edit section *** (MapObjectSetting.Title Get start)

                // *** End programmer edit section *** (MapObjectSetting.Title Get start)
                string result = this.fTitle;
                // *** Start programmer edit section *** (MapObjectSetting.Title Get end)

                // *** End programmer edit section *** (MapObjectSetting.Title Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (MapObjectSetting.Title Set start)

                // *** End programmer edit section *** (MapObjectSetting.Title Set start)
                this.fTitle = value;
                // *** Start programmer edit section *** (MapObjectSetting.Title Set end)

                // *** End programmer edit section *** (MapObjectSetting.Title Set end)
            }
        }
        
        /// <summary>
        /// MultEditForm.
        /// </summary>
        // *** Start programmer edit section *** (MapObjectSetting.MultEditForm CustomAttributes)

        // *** End programmer edit section *** (MapObjectSetting.MultEditForm CustomAttributes)
        [StrLen(255)]
        public virtual string MultEditForm
        {
            get
            {
                // *** Start programmer edit section *** (MapObjectSetting.MultEditForm Get start)

                // *** End programmer edit section *** (MapObjectSetting.MultEditForm Get start)
                string result = this.fMultEditForm;
                // *** Start programmer edit section *** (MapObjectSetting.MultEditForm Get end)

                // *** End programmer edit section *** (MapObjectSetting.MultEditForm Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (MapObjectSetting.MultEditForm Set start)

                // *** End programmer edit section *** (MapObjectSetting.MultEditForm Set start)
                this.fMultEditForm = value;
                // *** Start programmer edit section *** (MapObjectSetting.MultEditForm Set end)

                // *** End programmer edit section *** (MapObjectSetting.MultEditForm Set end)
            }
        }
        
        /// <summary>
        /// MapObjectSetting.
        /// </summary>
        // *** Start programmer edit section *** (MapObjectSetting.DefaultMap CustomAttributes)

        // *** End programmer edit section *** (MapObjectSetting.DefaultMap CustomAttributes)
        [PropertyStorage(new string[] {
                "DefaultMap"})]
        public virtual NewPlatform.Flexberry.GIS.Map DefaultMap
        {
            get
            {
                // *** Start programmer edit section *** (MapObjectSetting.DefaultMap Get start)

                // *** End programmer edit section *** (MapObjectSetting.DefaultMap Get start)
                NewPlatform.Flexberry.GIS.Map result = this.fDefaultMap;
                // *** Start programmer edit section *** (MapObjectSetting.DefaultMap Get end)

                // *** End programmer edit section *** (MapObjectSetting.DefaultMap Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (MapObjectSetting.DefaultMap Set start)

                // *** End programmer edit section *** (MapObjectSetting.DefaultMap Set start)
                this.fDefaultMap = value;
                // *** Start programmer edit section *** (MapObjectSetting.DefaultMap Set end)

                // *** End programmer edit section *** (MapObjectSetting.DefaultMap Set end)
            }
        }
        
        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {
            
            /// <summary>
            /// "MapObjectSetting" view.
            /// </summary>
            public static ICSSoft.STORMNET.View MapObjectSetting
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("MapObjectSetting", typeof(NewPlatform.Flexberry.GIS.MapObjectSetting));
                }
            }
            
            /// <summary>
            /// "MapObjectSettingE" view.
            /// </summary>
            public static ICSSoft.STORMNET.View MapObjectSettingE
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("MapObjectSettingE", typeof(NewPlatform.Flexberry.GIS.MapObjectSetting));
                }
            }
            
            /// <summary>
            /// "MapObjectSettingL" view.
            /// </summary>
            public static ICSSoft.STORMNET.View MapObjectSettingL
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("MapObjectSettingL", typeof(NewPlatform.Flexberry.GIS.MapObjectSetting));
                }
            }
        }
    }
}