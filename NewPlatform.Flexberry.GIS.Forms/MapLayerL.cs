namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using Web.Page;
    using System.Web.Routing;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using Web.Routing;

    internal class MapLayerL : DynamicBaseListForm
    {
        /// <summary>
        /// </summary>
        public MapLayerL() : base(MapLayer.Views.MapLayerL, DynamicPageIdentifiers.Get("gis-maplayer-e"))
        {
            PageHeader = "Слои карты";
        }

        /// <summary>
        /// Обработчик события инициализации формы.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnInit(EventArgs e)
        {
            Wolv.LimitEditorClassCaption = "Слои карты";
            Wolv.EditPage = GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-maplayer-new")), new RouteValueDictionary());
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            new WolvSettApplyer().SettingsApply(Wolv);
            RightHelper.ApplyRightsOnWolv(Wolv, typeof(MapLayer));
        }
    }
}