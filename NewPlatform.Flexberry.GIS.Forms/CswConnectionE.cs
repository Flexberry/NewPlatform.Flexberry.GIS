namespace NewPlatform.Flexberry.GIS.Forms
{
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using ICSSoft.STORMNET.Web.Tools;
    using Web.Page;

    internal class CswConnectionE : DynamicBaseEditForm<CswConnection>
    {
        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public CswConnectionE() : base(CswConnection.Views.CswConnectionE.Name)
        {
            PageHeader = "CSW соединение";
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
            clearfix.Controls.Add(new Label { ID = "ctrlUrlLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Url", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlUrl", CssClass = Web.Page.Constants.DescTxtCssClass });
    
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
            if (DataObject == null) DataObject = new CswConnection();
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
