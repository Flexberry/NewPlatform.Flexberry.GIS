namespace NewPlatform.Flexberry.GIS
{
    using System.Linq;
    using ICSSoft.Services;
    using ICSSoft.STORMNET.Business;
    using ICSSoft.STORMNET.Business.LINQProvider;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Unity;

    [TestClass]
    public class TriggerTests
    {

        [TestMethod]
        // Интеграционный тест для отладки, не использовать в CI - зависает
        public void GenerateAttributiveTest()
        {
            var dataobject = new IIS.ISOGD.Здание();
            var dataService = UnityFactory.GetContainer().Resolve<IDataService>();
            var dataLinks = dataService.Query<DataLink>(DataLink.Views.DataLinkE.Name).Where(x => x.LayerTable == "geo.Здание");

            /*var errors = DataLinks.TriggerGenerator.TriggerGenerator.Generate(dataLinks);
             if(errors.Any())
             {
                 throw new Exception(string.Join(Environment.NewLine, errors));
             }
             */
        }
    }
}
