namespace NewPlatform.Flexberry.GIS.Forms
{
	using System;
    using System.Linq;
    using System.Web.UI.HtmlControls;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using System.Web.UI;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Web.Tools;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using System.Web.UI.WebControls;
    using Web.Page;

    internal class LayerLinkE : DynamicBaseEditForm<LayerLink>
    {
        private AjaxGroupEdit ctrlLinkParameter;
        private MasterEditorAjaxLookUp ctrlLayer;

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public LayerLinkE() : base(LayerLink.Views.LayerLinkEF.Name)
        {
            PageHeader = "Связь с картой";
        }

        /// <summary>
        /// Инициализирует контролы, которые должны присутствовать на форме редактирования пользователя (в 
        /// том числе и те, которые будут обрабатываться <see cref="WebBinder"/>.
        /// </summary>
        protected void InitializeFormControls()
        {
            var parentDiv = AddParentDiv();
            parentDiv.Controls.Add(new ScriptManager());

            var clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlAllowShowLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Показывать", EnableViewState = false });
            clearfix.Controls.Add(new CheckBox { ID = "ctrlAllowShow", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlLayerLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Слой карты", EnableViewState = false });
            ctrlLayer =
                new MasterEditorAjaxLookUp()
                {
                    ID = "ctrlLayer",
                    MasterViewName = MapLayer.Views.MapLayerL.Name
                }.WithParent(clearfix);

            new HtmlGenericControl("br").WithParent(parentDiv);
            var fs = new HtmlGenericControl("fieldset").WithParent(parentDiv);
            new HtmlGenericControl("legend") { InnerText = "Параметры" }.WithParent(fs);
            ctrlLinkParameter = new AjaxGroupEdit() { ID = "ctrlParameters" }.WithParent(fs);
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
            if (DataObject == null) DataObject = new LayerLink();
            if (RightHelper.CheckReadOnlyForm(DataObject.GetType(), DataObject.GetStatus())) ReadOnly = true;
            if (DataObject.GetStatus() == ObjectStatus.Created && !string.IsNullOrEmpty(Request["SPK"]))
            {
                var pk = Request["SPK"];
                DataObject.MapObjectSetting =
                    DataServiceProvider.DataService.Query<MapObjectSetting>(
                        MapObjectSetting.Views.MapObjectSettingE.Name)
                        .FirstOrDefault(x => x.__PrimaryKey.ToString().Equals(pk));
            }
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