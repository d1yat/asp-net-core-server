using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using AspNetCoreDashboardBackend.Models;

namespace AspNetCoreDashboardBackend.Configuration;

public class ObjectDataSourceConfigurator {
    public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage) {
        var dataItems = JsonResultClass.GetDataItems();
        foreach (var dataItem in dataItems) {
            // Registers an Object data source.
            string? datasourceName = dataItem.Name;
            DashboardObjectDataSource objDataItem = new DashboardObjectDataSource(datasourceName);
            objDataItem.DataId = dataItem.Id;
            storage.RegisterDataSource(datasourceName, objDataItem.SaveToXml());

            configurator.DataLoading += DataLoading;
        }
    }

    private static void DataLoading(object sender, DataLoadingWebEventArgs e) {
        string? id = e.DataId;
        var itemDetailsList = JsonResultClass.GetItemDetails(id);
        e.Data = itemDetailsList;
    }
}

