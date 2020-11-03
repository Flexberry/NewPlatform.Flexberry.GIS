using System;
using System.Collections.Generic;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    public class MapSimpleField: MapField
    {
        /// <summary>
        /// </summary>
        public Dictionary<string, string> LayerObjectFields { get; private set; }
        
        /// <summary>
        /// </summary>
        public MapSimpleField(Type type, string layerField, string objectPath, string prefix = "") : base(type, prefix)
        {
            LayerObjectFields = new Dictionary<string, string>();
            Add(layerField, objectPath);
        }

        public void Add(string layerField, string objectPath)
        {
            Chain.Add(objectPath);

            if (!LayerObjectFields.ContainsKey(layerField))
            {
                LayerObjectFields.Add(layerField, objectPath);
            }
            else throw new Exception();
        }
    }
}