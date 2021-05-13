namespace NewPlatform.Flexberry.GIS.Forms
{
    using System;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using ICSSoft.STORMNET.Web.AjaxControls;
    using ICSSoft.STORMNET.Web.Tools;
    using ICSSoft.STORMNET.Web.Tools.Monads;
    using NewPlatform.Flexberry.GIS.DataLinks.TriggerGenerator;
    using NewPlatform.Flexberry.Web.Page;

    internal class DataLinkE : DynamicBaseEditForm<DataLink>
    {
        private AjaxGroupEdit ctrlDataLinkParameter;
        private ImageButton GenerateBtn;

        public static object LockObject = new object();

        /// <summary>
        /// Конструктор формы.
        /// </summary>
        public DataLinkE() : base(DataLink.Views.DataLinkE.Name)
        {
            PageHeader = "Связь с картой";
        }

        /// <summary>Загрузка и инициализация клиентских скриптов.</summary>
        protected override void LoadScripts()
        {
            base.LoadScripts();

            if (ReadOnly) return;

            var type = typeof(DataLinkE);
            AttachWebResource(type, "scripts.jquery.gis.js");
            PageContentManager.AttachJavaScriptCode($"$('#{GenerateBtn.ClientID}').bind('click', function () {{ return generateTriggersClick(); }});", true);
        }

        /// <summary>
        /// Инициализирует контролы, которые должны присутствовать на форме редактирования пользователя (в 
        /// том числе и те, которые будут обрабатываться <see cref="WebBinder"/>.
        /// </summary>
        protected void InitializeFormControls()
        {
            var parentDiv = AddParentDiv();

            var clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlLayerTableLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Слой геоданных", EnableViewState = false });
            clearfix.Controls.Add(new TextBox { ID = "ctrlLayerTable", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlClearWithoutLinkLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Очищать при отсутствии связи", EnableViewState = false });
            clearfix.Controls.Add(new CheckBox { ID = "ctrlClearWithoutLink", CssClass = Web.Page.Constants.DescTxtCssClass });

            clearfix = AddClearFix(parentDiv);
            clearfix.Controls.Add(new Label { ID = "ctrlCreateObjectLabel", CssClass = Web.Page.Constants.DescLblCssClass, Text = "Триггер на создание", EnableViewState = false });
            clearfix.Controls.Add(new CheckBox { ID = "ctrlCreateObject", CssClass = Web.Page.Constants.DescTxtCssClass });

            parentDiv.Controls.Add(new ScriptManager());

            new HtmlGenericControl("br").WithParent(parentDiv);
            var fs = new HtmlGenericControl("fieldset").WithParent(parentDiv);
            new HtmlGenericControl("legend") { InnerText = "Параметры" }.WithParent(fs);
            ctrlDataLinkParameter = new AjaxGroupEdit { ID = "ctrlDataLinkParameter" }.WithParent(fs);

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

        private void GenerateClick(object sender, ImageClickEventArgs imageClickEventArgs)
        {
            var errors = TriggerGenerator.Generate(new[] { DataObject });

            if (errors.Any())
            {
                PageContentManager.AttachJavaScriptCode($"jAlert('Генерация невозможна, ошибки<br/>: {string.Join("<br />", errors)}', '');", true);
                return;
            }

            PageContentManager.AttachJavaScriptCode($"jAlert('Триггеры успешно сгенерированы', '');", true);
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
            if (DataObject == null) DataObject = new DataLink();
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
