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
    /// FavoriteFeature.
    /// </summary>
    // *** Start programmer edit section *** (FavoriteFeature CustomAttributes)

    // *** End programmer edit section *** (FavoriteFeature CustomAttributes)
    [AutoAltered()]
    [Caption("FavoriteFeatures")]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("AuditView", new string[] {
            "CreateTime",
            "Creator",
            "EditTime",
            "Editor",
            "ObjectKey",
            "ObjectLayerKey",
            "UserKey"})]
    public class FavoriteFeature : ICSSoft.STORMNET.DataObject
    {

        private System.Nullable<System.DateTime> fCreateTime;

        private string fCreator;

        private System.Nullable<System.DateTime> fEditTime;

        private string fEditor;

        private System.Guid fObjectKey;

        private System.Guid fObjectLayerKey;

        private string fUserKey;

        // *** Start programmer edit section *** (FavoriteFeature CustomMembers)

        // *** End programmer edit section *** (FavoriteFeature CustomMembers)


        /// <summary>
        /// Время создания объекта.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.CreateTime CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.CreateTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> CreateTime
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.CreateTime Get start)

                // *** End programmer edit section *** (FavoriteFeature.CreateTime Get start)
                System.Nullable<System.DateTime> result = this.fCreateTime;
                // *** Start programmer edit section *** (FavoriteFeature.CreateTime Get end)

                // *** End programmer edit section *** (FavoriteFeature.CreateTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.CreateTime Set start)

                // *** End programmer edit section *** (FavoriteFeature.CreateTime Set start)
                this.fCreateTime = value;
                // *** Start programmer edit section *** (FavoriteFeature.CreateTime Set end)

                // *** End programmer edit section *** (FavoriteFeature.CreateTime Set end)
            }
        }

        /// <summary>
        /// Создатель объекта.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.Creator CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.Creator CustomAttributes)
        [StrLen(255)]
        public virtual string Creator
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.Creator Get start)

                // *** End programmer edit section *** (FavoriteFeature.Creator Get start)
                string result = this.fCreator;
                // *** Start programmer edit section *** (FavoriteFeature.Creator Get end)

                // *** End programmer edit section *** (FavoriteFeature.Creator Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.Creator Set start)

                // *** End programmer edit section *** (FavoriteFeature.Creator Set start)
                this.fCreator = value;
                // *** Start programmer edit section *** (FavoriteFeature.Creator Set end)

                // *** End programmer edit section *** (FavoriteFeature.Creator Set end)
            }
        }

        /// <summary>
        /// Время последнего редактирования объекта.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.EditTime CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.EditTime CustomAttributes)
        public virtual System.Nullable<System.DateTime> EditTime
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.EditTime Get start)

                // *** End programmer edit section *** (FavoriteFeature.EditTime Get start)
                System.Nullable<System.DateTime> result = this.fEditTime;
                // *** Start programmer edit section *** (FavoriteFeature.EditTime Get end)

                // *** End programmer edit section *** (FavoriteFeature.EditTime Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.EditTime Set start)

                // *** End programmer edit section *** (FavoriteFeature.EditTime Set start)
                this.fEditTime = value;
                // *** Start programmer edit section *** (FavoriteFeature.EditTime Set end)

                // *** End programmer edit section *** (FavoriteFeature.EditTime Set end)
            }
        }

        /// <summary>
        /// Последний редактор объекта.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.Editor CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.Editor CustomAttributes)
        [StrLen(255)]
        public virtual string Editor
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.Editor Get start)

                // *** End programmer edit section *** (FavoriteFeature.Editor Get start)
                string result = this.fEditor;
                // *** Start programmer edit section *** (FavoriteFeature.Editor Get end)

                // *** End programmer edit section *** (FavoriteFeature.Editor Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.Editor Set start)

                // *** End programmer edit section *** (FavoriteFeature.Editor Set start)
                this.fEditor = value;
                // *** Start programmer edit section *** (FavoriteFeature.Editor Set end)

                // *** End programmer edit section *** (FavoriteFeature.Editor Set end)
            }
        }

        /// <summary>
        /// Ключ объекта.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.ObjectKey CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.ObjectKey CustomAttributes)
        [NotNull()]
        public virtual System.Guid ObjectKey
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.ObjectKey Get start)

                // *** End programmer edit section *** (FavoriteFeature.ObjectKey Get start)
                System.Guid result = this.fObjectKey;
                // *** Start programmer edit section *** (FavoriteFeature.ObjectKey Get end)

                // *** End programmer edit section *** (FavoriteFeature.ObjectKey Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.ObjectKey Set start)

                // *** End programmer edit section *** (FavoriteFeature.ObjectKey Set start)
                this.fObjectKey = value;
                // *** Start programmer edit section *** (FavoriteFeature.ObjectKey Set end)

                // *** End programmer edit section *** (FavoriteFeature.ObjectKey Set end)
            }
        }

        /// <summary>
        /// Ключ слоя.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.ObjectLayerKey CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.ObjectLayerKey CustomAttributes)
        [NotNull()]
        public virtual System.Guid ObjectLayerKey
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.ObjectLayerKey Get start)

                // *** End programmer edit section *** (FavoriteFeature.ObjectLayerKey Get start)
                System.Guid result = this.fObjectLayerKey;
                // *** Start programmer edit section *** (FavoriteFeature.ObjectLayerKey Get end)

                // *** End programmer edit section *** (FavoriteFeature.ObjectLayerKey Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.ObjectLayerKey Set start)

                // *** End programmer edit section *** (FavoriteFeature.ObjectLayerKey Set start)
                this.fObjectLayerKey = value;
                // *** Start programmer edit section *** (FavoriteFeature.ObjectLayerKey Set end)

                // *** End programmer edit section *** (FavoriteFeature.ObjectLayerKey Set end)
            }
        }

        /// <summary>
        /// Ключ пользователя.
        /// </summary>
        // *** Start programmer edit section *** (FavoriteFeature.UserKey CustomAttributes)

        // *** End programmer edit section *** (FavoriteFeature.UserKey CustomAttributes)
        [StrLen(50)]
        [NotNull()]
        public virtual string UserKey
        {
            get
            {
                // *** Start programmer edit section *** (FavoriteFeature.UserKey Get start)

                // *** End programmer edit section *** (FavoriteFeature.UserKey Get start)
                string result = this.fUserKey;
                // *** Start programmer edit section *** (FavoriteFeature.UserKey Get end)

                // *** End programmer edit section *** (FavoriteFeature.UserKey Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (FavoriteFeature.UserKey Set start)

                // *** End programmer edit section *** (FavoriteFeature.UserKey Set start)
                this.fUserKey = value;
                // *** Start programmer edit section *** (FavoriteFeature.UserKey Set end)

                // *** End programmer edit section *** (FavoriteFeature.UserKey Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "AuditView" view.
            /// </summary>
            public static ICSSoft.STORMNET.View AuditView
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("AuditView", typeof(NewPlatform.Flexberry.GIS.FavoriteFeature));
                }
            }
        }
    }
}
