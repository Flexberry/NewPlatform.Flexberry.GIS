namespace NewPlatform.Flexberry.GIS.Forms
{
    using System.Web.Routing;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.FunctionalLanguage;
    using Web.Routing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using ICSSoft.STORMNET.Web.Tools;
    using Web.Page;
    using System.Web.UI.HtmlControls;

    internal class MapE : DynamicBaseEditForm<Map>
    {
        private WebObjectListView ctrlMapLayer;
        private HtmlGenericControl fsML;

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public MapE() : base(Map.Views.MapE.Name)
        {
            PageHeader = "Карта";
            
            ctrlMapLayer = new WebObjectListView() { ID = "ctrlMapLayer", View = MapLayer.Views.MapLayerD };
        }
        
        /// <summary>
        /// Инициализирует контролы, которые должны присутствовать на форме редактирования пользователя (в 
        /// том числе и те, которые будут обрабатываться <see cref="WebBinder"/>.
        /// </summary>
        protected void InitializeFormControls()
        {
            var parentDiv = AddParentDiv();

            var clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlNameLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Наименование", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlName", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix.Controls.Add(new Label { ID = "ctrlDescriptionLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Описание", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlDescription", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix.Controls.Add(new Label { ID = "ctrlKeyWordsLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Ключевые слова", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlKeyWords", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlLatLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Широта", EnableViewState = false });
            clearfix.Controls.Add(new TextBox() { ID = "ctrlLat", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlLngLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Долгота", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlLng", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlZoomLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Зум", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlZoom", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlPublicLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Общая", EnableViewState = false });
            clearfix.Controls.Add(new CheckBox { ID = "ctrlPublic", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix.Controls.Add(new Label { ID = "ctrlScaleLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Масштаб", EnableViewState = false });
            clearfix.Controls.Add(new AlphaNumericTextBox { ID = "ctrlScale", CssClass = Web.Page.Constants.DescTxtCssClass, Type = AlphaNumericType.Numeric });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlCoordinateReferenceSystemLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Система координат", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlCoordinateReferenceSystem", CssClass = Web.Page.Constants.DescTxtCssClass });

            parentDiv.Controls.Add(new ScriptManager());

            new HtmlGenericControl("br").WithParent(parentDiv);
            fsML = new HtmlGenericControl("fieldset").WithParent(parentDiv);
            new HtmlGenericControl("legend") { InnerText = "Слои карты" }.WithParent(fsML);
            ctrlMapLayer.WithParent(fsML);
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
            if (DataObject == null) DataObject = new Map();
            if (RightHelper.CheckReadOnlyForm(DataObject.GetType(), DataObject.GetStatus())) ReadOnly = true;
            if (IsObjectPrototyped) DataObject.MapLayer.Clear();
            if (IsObjectCreated) fsML.Visible = false;
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице, которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            Page.Validate();
            
            ctrlMapLayer.LimitFunction = LangDef.GetFunction(LangDef.funcEQ,
                new VariableDef(LangDef.GuidType, Information.ExtractPropertyPath<MapLayer>(x => x.Map)),
                DataObject.__PrimaryKey.ToString());

            ctrlMapLayer.EditPage = FormUrlHelper.UpdateParam("MPK", DataObject.__PrimaryKey.ToString(), GetRouteUrl(DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-maplayer-new")), new RouteValueDictionary()));
            var routingForLayerLinkEditPage = (DynamicPageRoute)RouteTable.Routes[DynamicPageRoute.GetRouteName(DynamicPageIdentifiers.Get("gis-maplayer-e"))];
            ctrlMapLayer.EditPageBuilder = buildParams =>
            {
                var routes = new RouteValueDictionary { { WebParamController.DataObjectPrimaryKey, buildParams.PrimaryKey } };
                var virtualPath = routingForLayerLinkEditPage.GetVirtualPath(Request.RequestContext, routes);
                return virtualPath?.VirtualPath;
            };

            new WolvSettApplyer().SettingsApply(ctrlMapLayer);

            RightHelper.ApplyRightsOnWolv(ctrlMapLayer, typeof(MapLayer));

            ctrlMapLayer.Operations.LimitEdit = false;
        }
    }
}
