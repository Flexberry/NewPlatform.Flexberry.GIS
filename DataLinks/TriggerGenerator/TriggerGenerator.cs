namespace NewPlatform.Flexberry.GIS.DataLinks.TriggerGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using ICSSoft.Services;
    using ICSSoft.STORMNET;
    using ICSSoft.STORMNET.Business;
    using Unity;

    public class TriggerGenerator
    {
        static IDbConnection OpenConnection(IDataService dataservice)
        {
            IDbConnection result = null;
            IDataService srv = null;

            srv = dataservice ?? DataServiceProvider.DataService;

            result = ((SQLDataService)srv).GetConnection();
            result.Open();
            return result;
        }

        private static object lockObject = new object();

        public static List<string> Generate(IEnumerable<DataLink> datalinks)
        {
            var errors = new List<string>();

            lock (lockObject)
            {
                var attributiveDataService = UnityFactory.CreateContainer().Resolve<IDataService>("AttributiveDS");
                var sdeDataService = UnityFactory.CreateContainer().Resolve<IDataService>("SpatialDS");

                foreach (var datalink in datalinks)
                {
                    var error = string.Empty;
                    if (string.IsNullOrEmpty(datalink.LayerTable)) error = "слой геоданных";
                    if (datalink.DataLinkParameter == null || datalink.DataLinkParameter.Count == 0
                        || !datalink.DataLinkParameter.Cast<DataLinkParameter>().Any(x => x.LinkField))
                        error = "параметры/поля для связи";

                    if (!string.IsNullOrEmpty(error))
                    {
                        errors.Add($"'Генерация по настройке {datalink.__PrimaryKey} невозможна: не указан(ы) {error}");
                        continue;
                    }

                    using (var dbConnection = OpenConnection(attributiveDataService))
                    using (var sqlTransaction = dbConnection.BeginTransaction())
                    {

                        using (var sqlCommand = dbConnection.CreateCommand())
                        {
                            sqlCommand.Transaction = sqlTransaction;
                            sqlCommand.CommandTimeout = 0;
                            try
                            {
                                datalink.UpdateAttributiveTriggers(sqlCommand);
                                sqlTransaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                LogService.LogError(ex);
                                sqlTransaction.Rollback();
                                errors.Add($"Настройка {datalink.__PrimaryKey}: {ex.Message} {ex.InnerException?.Message}");
                            }
                        }
                    }

                    using (var sdeConnection = OpenConnection(sdeDataService))
                    {
                        var sqlTransaction = sdeConnection.BeginTransaction();
                        var sqlCommand = sdeConnection.CreateCommand();
                        sqlCommand.Transaction = sqlTransaction;
                        sqlCommand.CommandTimeout = 0;
                        try
                        {
                            datalink.UpdateSpatialTriggers(sqlCommand);
                            sqlTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            LogService.LogError(ex);
                            sqlTransaction.Rollback();
                            errors.Add($"Настройка {datalink.__PrimaryKey}: {ex.Message} {ex.InnerException?.Message}");
                        }
                    }
                }
            }

            return errors;
        }
    }
}
