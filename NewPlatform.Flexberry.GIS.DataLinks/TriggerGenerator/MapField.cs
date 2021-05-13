using System;

namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    public abstract class MapField
    {
        public Chain Chain { get; protected set; }        

        protected MapField(Type type)
        {            
            Chain = new Chain(type, "");
        }
    }
}
