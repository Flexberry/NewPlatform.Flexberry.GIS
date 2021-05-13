namespace NewPlatform.Flexberry.GIS.Forms
{
    using System.Web.Routing;
    using Web.Routing;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using ICSSoft.STORMNET.Web.Tools;
    using Web.Page;
    using System.Linq;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;

    internal class MapLayerE : DynamicBaseEditForm<MapLayer>
    {
        private MasterEditorAjaxLookUp ctrlParent;
        private MasterEditorAjaxLookUp ctrlMap;

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public MapLayerE() : base(MapLayer.Views.MapLayerE.Name)
        {
            PageHeader = "Слой карты";
        }
        
        /// <summary>
        /// Инициализирует контролы, которые должны присутствовать на форме редактирования пользователя (в 
        /// том числе и те, которые будут обрабатываться <see cref="WebBinder"/>.
        /// </summary>
        protected void InitializeFormControls()
        {
            var parentDiv = AddParentDiv();

            var clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlMapLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Карта", EnableViewState = false });
            ctrlMap = new MasterEditorAjaxLookUp() { ID = "ctrlMap" }.WithParent(clearfix);

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlNameLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Наименование", EnableViewState = false });
            clearfix.Controls.Add(new TextBox {ID = "ctrlName", CssClass = Web.Page.Constants.DescTxtCssClass});

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlDescriptionLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Описание", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlDescription", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlKeyWordsLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Ключевые слова", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlKeyWords", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlTypeLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Тип", EnableViewState = false });
            clearfix.Controls.Add(new TextBox {ID = "ctrlType", CssClass = Web.Page.Constants.DescTxtCssClass});
    
            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlVisibilityLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Видимость", EnableViewState = false });
            clearfix.Controls.Add(new CheckBox {ID = "ctrlVisibility", CssClass = Web.Page.Constants.DescTxtCssClass});
    
            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlSettingsLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Настройки", EnableViewState = false });
            clearfix.Controls.Add(new TextBox {ID = "ctrlSettings", CssClass = Web.Page.Constants.DescTxtCssClass});
    
            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlCoordinateReferenceSystemLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Система координат", EnableViewState = false });
            clearfix.Controls.Add(new TextBox {ID = "ctrlCoordinateReferenceSystem", CssClass = Web.Page.Constants.DescTxtCssClass});

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlScaleLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Масштаб", EnableViewState = false });
            clearfix.Controls.Add(new AlphaNumericTextBox { ID = "ctrlScale", CssClass = Web.Page.Constants.DescTxtCssClass, Type = AlphaNumericType.Numeric });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlIndexLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Индекс", EnableViewState = false });
            clearfix.Controls.Add(new TextBox {ID = "ctrlIndex", CssClass = Web.Page.Constants.DescTxtCssClass});

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlParentLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Родительский слой", EnableViewState = false });
            ctrlParent = new MasterEditorAjaxLookUp() {ID = "ctrlParent"}.WithParent(clearfix);

            parentDiv.Controls.Add(new ScriptManager());
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
            if (DataObject == null) DataObject = new MapLayer();
            if (RightHelper.CheckReadOnlyForm(DataObject.GetType(), DataObject.GetStatus())) ReadOnly = true;
            
            if (DataObject.GetStatus() == ObjectStatus.Created && !string.IsNullOrEmpty(Request["MPK"]))
            {
                var pk = Request["MPK"];
                DataObject.Map =
                    DataServiceProvider.DataService.Query<Map>(Map.Views.MapE.Name)
                        .FirstOrDefault(x => x.__PrimaryKey.ToString().Equals(pk));
            }

            ctrlParent.MasterViewName = MapLayer.Views.MapLayerL.Name;
            ctrlMap.MasterViewName = Map.Views.MapL.Name;
        }

        /// <summary>
        /// Здесь лучше всего изменять свойства контролов на странице, которые не обрабатываются WebBinder.
        /// </summary>
        protected override void PostApplyToControls()
        {
            Page.Validate();
        }
    }
}