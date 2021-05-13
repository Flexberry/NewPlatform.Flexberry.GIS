namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System.Web.Routing;

    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.Web.Tools;
    using ICSSoft.STORMNET.Web.AjaxControls;
    
    
    using NewPlatform.Flexberry.Web.Page;
    using NewPlatform.Flexberry.Web.Routing;

    using NewPlatform.Flexberry.GIS.DataLinks.TriggerGenerator;

    internal class MapObjectSettingL : DynamicBaseListForm
    {
        /// <summary>
        /// </summary>
        public MapObjectSettingL() : base(MapObjectSetting.Views.MapObjectSettingL, DynamicPageIdentifiers.Get("gis-mapobjectsetting-e"))
        {
            PageHeader = "Настройка объекта";
        }

        /// <summary>
        /// Обработчик события инициализации формы.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnInit(EventArgs e)
        {
            Wolv.LimitEditorClassCaption = "Настройка объекта";
            Wolv.EditPage = GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-mapobjectsetting-new")), new RouteValueDictionary());
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            var type = typeof(DataLinkE);
            AttachWebResource(type, "scripts.jquery.gis.js");
            PageContentManager.AttachJavaScriptCode("$('[id$=_generate]').bind('click', function () { generateTriggersOnListClick(); });", true);

            new WolvSettApplyer().SettingsApply(Wolv);
            RightHelper.ApplyRightsOnWolv(Wolv, typeof(MapObjectSetting));
            
            Wolv.AddImageButton("generate", "ics-wolv-toolbar-button generate-triggers", "Сгенерировать все триггеры", GenerateClick);
        }
        
        private void GenerateClick(LinkButton sender, ToolBarBtnEventArgs eventargs)
        {
            var errors = TriggerGenerator.Generate(DataServiceProvider.DataService.Query<DataLink>(DataLink.Views.DataLinkE.Name));
            PageContentManager.AttachJavaScriptCode($"jAlert('<b>Генерация триггеров завершена.</b>" + (errors.Any() ? "<br/><br/><b>Ошибки в процессе выполнения:</b><br/>" + string.Join("<br />", errors): "") + "', '');", true);
        }
    }
}
