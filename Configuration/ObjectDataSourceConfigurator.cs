using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using AspNetCoreDashboardBackend.Models;

namespace AspNetCoreDashboardBackend.Configuration;

public class ObjectDataSourceConfigurator {
    public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage) {
        var dataItems = JsonResultClass.GetDataItems();
        foreach (var dataItem in dataItems) {
            string? dataSourceName = dataItem.Name;
            DashboardObjectDataSource objDataItem = new DashboardObjectDataSource(dataSourceName);
            objDataItem.DataId = dataItem.Id;
            storage.RegisterDataSource(dataSourceName, objDataItem.SaveToXml());
        }

        configurator.DataLoading += DataLoading;
    }

    private static void DataLoading(object sender, DataLoadingWebEventArgs e) {
        string? id = e.DataId;
        if (!string.IsNullOrEmpty(id)) {
            var itemDetailsList = JsonResultClass.GetItemDetails(datasourceId: id);
            e.Data = itemDetailsList;
        }
    }
}

