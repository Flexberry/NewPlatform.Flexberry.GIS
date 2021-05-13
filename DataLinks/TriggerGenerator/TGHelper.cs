using System;
using ICSSoft.Services;
using ICSSoft.STORMNET;
using ICSSoft.STORMNET.Business;
using Unity;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    public class TGHelper
    {
        static TGHelper()
        {
            try
            {
                DataBaseHelper = UnityFactory.GetContainer().Resolve<DataBaseHelper>("DataBaseHelper");
                ChainHelper = UnityFactory.GetContainer().Resolve<DataBaseHelper>("ChainHelper");
                DataService = UnityFactory.GetContainer().Resolve<IDataService>();
            }
            catch (Exception ex)
            {
                LogService.LogError(ex.Message);
                throw new Exception("Неверно сконфигурирован Unity", ex);
            }
        }

        public static DataBaseHelper DataBaseHelper { get; private set; }

        public static DataBaseHelper ChainHelper { get; private set; }

        public static IDataService DataService { get; private set; }

        public static Type ObjectType(DataLink layerLink)
        {
            if (layerLink.MapObjectSetting == null)
            {
                throw new Exception($"Не указана настройка объекта");
            }

            DataService.LoadObject(layerLink.MapObjectSetting);

            if (string.IsNullOrEmpty(layerLink.MapObjectSetting.TypeName))
            {
                throw new Exception($"Не указан тип объекта");
            }

            var type = Type.GetType(layerLink.MapObjectSetting.TypeName);

            if (type == null)
            {
                throw new Exception($"Тип {layerLink.MapObjectSetting.TypeName} отсутствует в сборке");
            }

            return type;
        }
    }
}
