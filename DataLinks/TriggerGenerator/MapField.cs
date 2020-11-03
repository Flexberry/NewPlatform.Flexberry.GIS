using System;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    public abstract class MapField
    {
        public Chain Chain { get; protected set; }        

        protected MapField(Type type, string prefix = "")
        {            
            Chain = new Chain(type, prefix);
        }
    }
}
