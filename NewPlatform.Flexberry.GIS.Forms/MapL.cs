namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using Web.Page;
    using Web.Routing;
    using System.Web.Routing;
    using ICSSoft.STORMNET.Web.AjaxControls;

    internal class MapL : DynamicBaseListForm
    {
        /// <summary>
        /// </summary>
        public MapL() : base(Map.Views.MapL, DynamicPageIdentifiers.Get("gis-map-e"))
        {
            PageHeader = "Карты";
        }

        /// <summary>
        /// Обработчик события инициализации формы.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnInit(EventArgs e)
        {
            Wolv.LimitEditorClassCaption = "Карты";
            Wolv.EditPage = GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-map-new")), new RouteValueDictionary());
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            new WolvSettApplyer().SettingsApply(Wolv);
            RightHelper.ApplyRightsOnWolv(Wolv, typeof(Map));
        }
    }
}