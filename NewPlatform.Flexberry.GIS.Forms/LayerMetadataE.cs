namespace NewPlatform.Flexberry.GIS.Forms
{
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ICSSoft.STORMNET.Web.Tools;
    using Web.Page;
    using ICSSoft.STORMNET.Web.AjaxControls;

    internal class LayerMetadataE : DynamicBaseEditForm<LayerMetadata>
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public LayerMetadataE() : base(LayerMetadata.Views.LayerMetadataE.Name)
        {
            PageHeader = "Метаданные слоя";
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

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlDescriptionLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Описание", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlDescription", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlKeyWordsLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Ключевые слова", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlKeyWords", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlTypeLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Тип", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlType", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlCoordinateReferenceSystemLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Система координат", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlCoordinateReferenceSystem", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlScaleLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Масштаб", EnableViewState = false });
            clearfix.Controls.Add(new AlphaNumericTextBox { ID = "ctrlScale", CssClass = Web.Page.Constants.DescTxtCssClass, Type = AlphaNumericType.Numeric });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlSettingsLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Настройки", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlSettings", CssClass = Web.Page.Constants.DescTxtCssClass });

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

        protected override void PreApplyToControls()
        {
            if (DataObject == null) DataObject = new LayerMetadata();
            if (RightHelper.CheckReadOnlyForm(DataObject.GetType(), DataObject.GetStatus())) ReadOnly = true;
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
