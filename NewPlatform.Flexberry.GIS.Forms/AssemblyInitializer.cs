namespace NewPlatform.Flexberry.GIS.Forms
{
    using Web.Routing;

    public class AssemblyInitializer
    {
        public static void Initialize()
        {
            // Регистрируем типы страницы, соответствующие кастомным значениям в перечислении
            // Порядок важен!! Сначала страница -e, потом -new!!!
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-mapobjectsetting-e"), typeof(MapObjectSettingE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-mapobjectsetting-new"), typeof(MapObjectSettingE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-mapobjectsetting-l"), typeof(MapObjectSettingL).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-maplayer-e"), typeof(MapLayerE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-maplayer-new"), typeof(MapLayerE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-maplayer-l"), typeof(MapLayerL).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-map-e"), typeof(MapE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-map-new"), typeof(MapE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-map-l"), typeof(MapL).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-layermetadata-e"), typeof(LayerMetadataE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-layermetadata-new"), typeof(LayerMetadataE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-layermetadata-l"), typeof(LayerMetadataL).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-datalink-e"), typeof(DataLinkE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-datalink-new"), typeof(DataLinkE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-layerlink-e"), typeof(LayerLinkE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-layerlink-new"), typeof(LayerLinkE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-cswconnection-e"), typeof(CswConnectionE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-cswconnection-new"), typeof(CswConnectionE).FullName);
            DynamicPageRoute.AddDynamicPage(DynamicPageIdentifiers.Add("gis-cswconnection-l"), typeof(CswConnectionL).FullName);
        }
    }
}
