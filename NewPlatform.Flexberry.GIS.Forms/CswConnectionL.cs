namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using Web.Routing;
    using Web.Page;
    using System.Web.Routing;
    using ICSSoft.STORMNET.Web.AjaxControls;

    class CswConnectionL : DynamicBaseListForm
    {
        /// <summary>
        /// </summary>
        public CswConnectionL() : base(CswConnection.Views.CswConnectionL, DynamicPageIdentifiers.Get("gis-cswconnection-e"))
        {
            PageHeader = "CSW соединение";
        }

        /// <summary>
        /// Обработчик события инициализации формы.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnInit(EventArgs e)
        {
            Wolv.LimitEditorClassCaption = "CSW соединение";
            Wolv.EditPage = GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-cswconnection-new")), new RouteValueDictionary());
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            new WolvSettApplyer().SettingsApply(Wolv);
            RightHelper.ApplyRightsOnWolv(Wolv, typeof(CswConnection));
        }
    }
}