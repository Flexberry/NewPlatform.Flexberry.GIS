namespace NewPlatform.Flexberry.GIS.TriggerGenerator
{
    using System.Data;
    using System.Linq;

    public class SimpleSpatialTriggerGenerator : ITriggerGenerator
    {
        private string layerTable;

        private string triggerName;

        private string triggerPrefix;

        public SimpleSpatialTriggerGenerator(string triggerPrefix)
        {
            this.triggerPrefix = triggerPrefix;
        }

        public void DropTriggers(IDbCommand sqlCommand)
        {
            string sql = $@"DO $$ BEGIN 
                                IF EXISTS (SELECT 1 FROM pg_trigger t WHERE t.tgrelid = '{layerTable}'::regclass and tgname = '{triggerName}')
	                            THEN BEGIN
                                    DROP TRIGGER {triggerName} ON {layerTable};

                                    INSERT INTO geo.syncdataobjectkey(tablename, objectkey, olddok)
                                        SELECT '{layerTable}', primaryKey, DataObjectKey::uuid
                                        FROM {layerTable}
                                        WHERE DataObjectKey is not null;
                                    END;
                                END IF;
                           END$$";

            if (!string.IsNullOrEmpty(sql))
            {
                sqlCommand.CommandText = sql;
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void GenerateTriggers(IDbCommand sqlCommand)
        {
            string sql =
                $@"CREATE TRIGGER {triggerName}
    AFTER INSERT OR UPDATE OR DELETE ON {layerTable}
    FOR EACH ROW    
    EXECUTE PROCEDURE geo.syncdataobjectkey_trigger();
    
    INSERT INTO geo.syncdataobjectkey(tablename, objectkey, newdok) 
    SELECT '{layerTable}', primaryKey, DataObjectKey::uuid
    FROM {layerTable}
    WHERE DataObjectKey is not null;";

            if (!string.IsNullOrEmpty(sql))
            {
                sqlCommand.CommandText = sql;
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void InitByDataLink(DataLink datalink)
        {
            layerTable = datalink.LayerTable;

            var hasher = System.Security.Cryptography.MD5.Create();
            var hashdata = hasher.ComputeHash(System.Text.Encoding.Default.GetBytes($"{layerTable}_syncdok"));
            triggerName = triggerPrefix + string.Concat(hashdata.Select(b => b.ToString("x2")).ToArray());
        }
    }
}
