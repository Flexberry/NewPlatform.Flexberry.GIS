namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System.Data;

    public interface ITriggerGenerator
    {
        void GenerateTriggers(IDbCommand sqlCommand);

        void DropTriggers(IDbCommand sqlCommand);

        void InitByDataLink(DataLink datalink);
    }
}
