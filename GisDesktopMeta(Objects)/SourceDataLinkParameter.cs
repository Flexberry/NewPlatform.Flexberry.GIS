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
    /// SourceDataLinkParameter.
    /// </summary>
    // *** Start programmer edit section *** (SourceDataLinkParameter CustomAttributes)

    // *** End programmer edit section *** (SourceDataLinkParameter CustomAttributes)
    [AutoAltered()]
    [AccessType(ICSSoft.STORMNET.AccessType.none)]
    [View("SourceDataLinkParameterD", new string[] {
            "Link",
            "ObjectField as \'Поле в объекте\'",
            "LayerField as \'Поле в таблице геоданных\'",
            "QueryKey as \'Ключ запроса\'"}, Hidden = new string[] {
            "Link"})]
    public class SourceDataLinkParameter : ICSSoft.STORMNET.DataObject
    {

        private string fObjectField;

        private string fLayerField;

        private string fQueryKey;

        private NewPlatform.Flexberry.GIS.SourceDataLink fLink;

        // *** Start programmer edit section *** (SourceDataLinkParameter CustomMembers)

        // *** End programmer edit section *** (SourceDataLinkParameter CustomMembers)


        /// <summary>
        /// ObjectField.
        /// </summary>
        // *** Start programmer edit section *** (SourceDataLinkParameter.ObjectField CustomAttributes)

        // *** End programmer edit section *** (SourceDataLinkParameter.ObjectField CustomAttributes)
        [StrLen(255)]
        public virtual string ObjectField
        {
            get
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.ObjectField Get start)

                // *** End programmer edit section *** (SourceDataLinkParameter.ObjectField Get start)
                string result = this.fObjectField;
                // *** Start programmer edit section *** (SourceDataLinkParameter.ObjectField Get end)

                // *** End programmer edit section *** (SourceDataLinkParameter.ObjectField Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.ObjectField Set start)

                // *** End programmer edit section *** (SourceDataLinkParameter.ObjectField Set start)
                this.fObjectField = value;
                // *** Start programmer edit section *** (SourceDataLinkParameter.ObjectField Set end)

                // *** End programmer edit section *** (SourceDataLinkParameter.ObjectField Set end)
            }
        }

        /// <summary>
        /// LayerField.
        /// </summary>
        // *** Start programmer edit section *** (SourceDataLinkParameter.LayerField CustomAttributes)

        // *** End programmer edit section *** (SourceDataLinkParameter.LayerField CustomAttributes)
        [StrLen(255)]
        public virtual string LayerField
        {
            get
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.LayerField Get start)

                // *** End programmer edit section *** (SourceDataLinkParameter.LayerField Get start)
                string result = this.fLayerField;
                // *** Start programmer edit section *** (SourceDataLinkParameter.LayerField Get end)

                // *** End programmer edit section *** (SourceDataLinkParameter.LayerField Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.LayerField Set start)

                // *** End programmer edit section *** (SourceDataLinkParameter.LayerField Set start)
                this.fLayerField = value;
                // *** Start programmer edit section *** (SourceDataLinkParameter.LayerField Set end)

                // *** End programmer edit section *** (SourceDataLinkParameter.LayerField Set end)
            }
        }

        /// <summary>
        /// QueryKey.
        /// </summary>
        // *** Start programmer edit section *** (SourceDataLinkParameter.QueryKey CustomAttributes)

        // *** End programmer edit section *** (SourceDataLinkParameter.QueryKey CustomAttributes)
        [StrLen(255)]
        public virtual string QueryKey
        {
            get
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.QueryKey Get start)

                // *** End programmer edit section *** (SourceDataLinkParameter.QueryKey Get start)
                string result = this.fQueryKey;
                // *** Start programmer edit section *** (SourceDataLinkParameter.QueryKey Get end)

                // *** End programmer edit section *** (SourceDataLinkParameter.QueryKey Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.QueryKey Set start)

                // *** End programmer edit section *** (SourceDataLinkParameter.QueryKey Set start)
                this.fQueryKey = value;
                // *** Start programmer edit section *** (SourceDataLinkParameter.QueryKey Set end)

                // *** End programmer edit section *** (SourceDataLinkParameter.QueryKey Set end)
            }
        }

        /// <summary>
        /// мастеровая ссылка на шапку NewPlatform.Flexberry.GIS.SourceDataLink.
        /// </summary>
        // *** Start programmer edit section *** (SourceDataLinkParameter.Link CustomAttributes)

        // *** End programmer edit section *** (SourceDataLinkParameter.Link CustomAttributes)
        [Agregator()]
        [NotNull()]
        [PropertyStorage(new string[] {
                "Link"})]
        public virtual NewPlatform.Flexberry.GIS.SourceDataLink Link
        {
            get
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.Link Get start)

                // *** End programmer edit section *** (SourceDataLinkParameter.Link Get start)
                NewPlatform.Flexberry.GIS.SourceDataLink result = this.fLink;
                // *** Start programmer edit section *** (SourceDataLinkParameter.Link Get end)

                // *** End programmer edit section *** (SourceDataLinkParameter.Link Get end)
                return result;
            }
            set
            {
                // *** Start programmer edit section *** (SourceDataLinkParameter.Link Set start)

                // *** End programmer edit section *** (SourceDataLinkParameter.Link Set start)
                this.fLink = value;
                // *** Start programmer edit section *** (SourceDataLinkParameter.Link Set end)

                // *** End programmer edit section *** (SourceDataLinkParameter.Link Set end)
            }
        }

        /// <summary>
        /// Class views container.
        /// </summary>
        public class Views
        {

            /// <summary>
            /// "SourceDataLinkParameterD" view.
            /// </summary>
            public static ICSSoft.STORMNET.View SourceDataLinkParameterD
            {
                get
                {
                    return ICSSoft.STORMNET.Information.GetView("SourceDataLinkParameterD", typeof(NewPlatform.Flexberry.GIS.SourceDataLinkParameter));
                }
            }
        }
    }

    /// <summary>
    /// Detail array of SourceDataLinkParameter.
    /// </summary>
    // *** Start programmer edit section *** (DetailArrayDetailArrayOfSourceDataLinkParameter CustomAttributes)

    // *** End programmer edit section *** (DetailArrayDetailArrayOfSourceDataLinkParameter CustomAttributes)
    public class DetailArrayOfSourceDataLinkParameter : ICSSoft.STORMNET.DetailArray
    {

        // *** Start programmer edit section *** (NewPlatform.Flexberry.GIS.DetailArrayOfSourceDataLinkParameter members)

        // *** End programmer edit section *** (NewPlatform.Flexberry.GIS.DetailArrayOfSourceDataLinkParameter members)


        /// <summary>
        /// Construct detail array.
        /// </summary>
        /// <summary>
        /// Returns object with type SourceDataLinkParameter by index.
        /// </summary>
        /// <summary>
        /// Adds object with type SourceDataLinkParameter.
        /// </summary>
        public DetailArrayOfSourceDataLinkParameter(NewPlatform.Flexberry.GIS.SourceDataLink fSourceDataLink) :
                base(typeof(SourceDataLinkParameter), ((ICSSoft.STORMNET.DataObject)(fSourceDataLink)))
        {
        }

        public NewPlatform.Flexberry.GIS.SourceDataLinkParameter this[int index]
        {
            get
            {
                return ((NewPlatform.Flexberry.GIS.SourceDataLinkParameter)(this.ItemByIndex(index)));
            }
        }

        public virtual void Add(NewPlatform.Flexberry.GIS.SourceDataLinkParameter dataobject)
        {
            this.AddObject(((ICSSoft.STORMNET.DataObject)(dataobject)));
        }
    }
}
