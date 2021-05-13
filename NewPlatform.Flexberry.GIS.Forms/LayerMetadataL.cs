namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using Web.Routing;
    using Web.Page;
    using System.Web.Routing;
    using ICSSoft.STORMNET.Web.AjaxControls;

    class LayerMetadataL : DynamicBaseListForm
    {
        /// <summary>
        /// </summary>
        public LayerMetadataL() : base(LayerMetadata.Views.LayerMetadataL, DynamicPageIdentifiers.Get("gis-layermetadata-e"))
        {
            PageHeader = "Метаданные слоев";
        }

        /// <summary>
        /// Обработчик события инициализации формы.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnInit(EventArgs e)
        {
            Wolv.LimitEditorClassCaption = "Метаданные слоев";
            Wolv.EditPage = GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-layermetadata-new")), new RouteValueDictionary());
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            new WolvSettApplyer().SettingsApply(Wolv);
            RightHelper.ApplyRightsOnWolv(Wolv, typeof(LayerMetadata));
        }
    }
}