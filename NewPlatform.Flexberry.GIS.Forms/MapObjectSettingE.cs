namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using System.Linq;
    using System.Web.Routing;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using ICSSoft.STORMNET.Web.Tools;
    using ICSSoft.STORMNET.Web.Tools.Monads;

    using NewPlatform.Flexberry.Web.Page;
    using NewPlatform.Flexberry.Web.Routing;

    using NewPlatform.Flexberry.GIS.DataLinks.TriggerGenerator;

    internal class MapObjectSettingE : DynamicBaseEditForm<MapObjectSetting>
    {
        private WebObjectListView ctrlDataLink;
        private WebObjectListView ctrlLayerLink;
        private MasterEditorAjaxLookUp ctrlDefaultMap;
        private ImageButton GenerateBtn;

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public MapObjectSettingE() : base(MapObjectSetting.Views.MapObjectSettingE.Name)
        {
            PageHeader = "Настройка объекта";

            ctrlDataLink = new WebObjectListView() { ID = "ctrlDataLink", View = DataLink.Views.DataLinkD };
            ctrlLayerLink = new WebObjectListView() { ID = "ctrlLayerLink", View = LayerLink.Views.LayerLinkL };
        }

        /// <summary>
        /// Инициализирует контролы, которые должны присутствовать на форме редактирования пользователя (в 
        /// том числе и те, которые будут обрабатываться <see cref="WebBinder"/>.
        /// </summary>
        protected void InitializeFormControls()
        {
            var parentDiv = AddParentDiv();
            
            var clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlTypeNameLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Тип объекта", EnableViewState = false });
            clearfix.Controls.Add(new TextBox {ID = "ctrlTypeName", CssClass = Web.Page.Constants.DescTxtCssClass});

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlTitleLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Отображаемое название", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlTitle", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlListFormLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Списковая форма", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlListForm", CssClass = Web.Page.Constants.DescTxtCssClass});

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlEditFormLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Форма редактирования", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlEditForm", CssClass = Web.Page.Constants.DescTxtCssClass});

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlDefaultMapLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Карта по умолчанию", EnableViewState = false });
            ctrlDefaultMap =
                new MasterEditorAjaxLookUp()
                {
                    ID = "ctrlDefaultMap",
                    MasterViewName = Map.Views.MapL.Name,
                }.WithParent(clearfix);

            parentDiv.Controls.Add(new ScriptManager());

            new HtmlGenericControl("br").WithParent(parentDiv);
            var fsDL= new HtmlGenericControl("fieldset").WithParent(parentDiv);
            new HtmlGenericControl("legend") {InnerText = "Данные для генерации триггеров" }.WithParent(fsDL);
            ctrlDataLink.WithParent(fsDL);

            new HtmlGenericControl("br").WithParent(parentDiv);
            var fsLL = new HtmlGenericControl("fieldset").WithParent(parentDiv);
            new HtmlGenericControl("legend") { InnerText = "Данные для связи с картой" }.WithParent(fsLL);
            ctrlLayerLink.WithParent(fsLL);

            // кнопка
            if (SaveBtn != null)
            {
                var toolbar = SaveBtn.Parent;

                var generate = new ImageButton
                {
                    ID = "GenerateTriggerBtn",
                    SkinID = "GenerateBtn",
                    AlternateText = "Генерировать триггеры",
                    ValidationGroup = (SaveBtn).ValidationGroup
                };

                GenerateBtn =
                    generate.WithParent<ImageButton>(toolbar)
                        .Do<ImageButton>((Action<ImageButton>)(x => x.Click += GenerateClick));
            }
        }

        /// <summary>
        /// Метод, выполняющийся при инициализации формы. В нем динамически создаются
        /// контролы формы.
        /// </summary>
        /// <param name="e">Аргументы события.</param>
        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            InitializeFormControls();
        }

        /// <summary>
        /// Здесь лучше всего писать бизнес-логику, оперируя только объектом данных.
        /// </summary>
        protected override void PreApplyToControls()
        {
            if (DataObject == null) DataObject = new MapObjectSetting();

            if (RightHelper.CheckReadOnlyForm(DataObject.GetType(), DataObject.GetStatus())) ReadOnly = true;
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице, которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            base.PostApplyToControls();

            Page.Validate();

            ctrlDataLink.LimitFunction = LangDef.GetFunction(LangDef.funcEQ,
                new VariableDef(LangDef.GuidType, Information.ExtractPropertyPath<DataLink>(x => x.MapObjectSetting)),
                DataObject.__PrimaryKey.ToString());

            ctrlDataLink.EditPage = FormUrlHelper.UpdateParam("SPK", DataObject.__PrimaryKey.ToString(), GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-datalink-new")), new RouteValueDictionary()));
            var routingForDataLinkEditPage = (DynamicPageRoute)RouteTable.Routes[DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-datalink-e"))];
            ctrlDataLink.EditPageBuilder = buildParams =>
            {
                var routes = new RouteValueDictionary { { WebParamController.DataObjectPrimaryKey, buildParams.PrimaryKey } };
                var virtualPath = routingForDataLinkEditPage.GetVirtualPath(Request.RequestContext, routes);
                return virtualPath?.VirtualPath;
            };
            
            ctrlLayerLink.LimitFunction = LangDef.GetFunction(LangDef.funcEQ,
                new VariableDef(LangDef.GuidType, Information.ExtractPropertyPath<LayerLink>(x => x.MapObjectSetting)),
                DataObject.__PrimaryKey.ToString());

            ctrlLayerLink.EditPage = FormUrlHelper.UpdateParam("SPK", DataObject.__PrimaryKey.ToString(), GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-layerlink-new")), new RouteValueDictionary()));
            var routingForLayerLinkEditPage = (DynamicPageRoute)RouteTable.Routes[DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-layerlink-e"))];
            ctrlLayerLink.EditPageBuilder = buildParams =>
            {
                var routes = new RouteValueDictionary { { WebParamController.DataObjectPrimaryKey, buildParams.PrimaryKey } };
                var virtualPath = routingForLayerLinkEditPage.GetVirtualPath(Request.RequestContext, routes);
                return virtualPath?.VirtualPath;
            };
            
            new WolvSettApplyer().SettingsApply(ctrlDataLink);
            new WolvSettApplyer().SettingsApply(ctrlLayerLink);

            RightHelper.ApplyRightsOnWolv(ctrlDataLink, typeof(DataLink));
            RightHelper.ApplyRightsOnWolv(ctrlLayerLink, typeof(LayerLink));
            
            ctrlDataLink.Operations.LimitEdit = false;
            ctrlLayerLink.Operations.LimitEdit = false;
        }

        private void GenerateClick(object sender, ImageClickEventArgs imageClickEventArgs)
        {
            var errors = TriggerGenerator.Generate(
                DataServiceProvider.DataService
                    .Query<DataLink>(DataLink.Views.DataLinkE.Name)
                    .Where(dl => dl.MapObjectSetting.__PrimaryKey.ToString() == DataObject.__PrimaryKey.ToString()));

            PageContentManager.AttachJavaScriptCode($"jAlert('<b>Генерация триггеров завершена.</b>" + (errors.Any() ? "<br/><br/><b>Ошибки в процессе выполнения:</b><br/>" + string.Join("<br />", errors) : "") + "', '');", true);
        }
    }
}