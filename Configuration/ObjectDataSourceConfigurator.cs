using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using AspNetCoreDashboardBackend.Models;

namespace AspNetCoreDashboardBackend.Configuration;

public class ObjectDataSourceConfigurator {
    public static void ConfigureDataSource(DashboardConfigurator configurator, DataSourceInMemoryStorage storage) {
        // Registers an Object data source.
        DashboardObjectDataSource objDataItem = new DashboardObjectDataSource("Object Data Item");
        objDataItem.DataId = "objectDataItem";
        storage.RegisterDataSource("objDataItem", objDataItem.SaveToXml());

        // Registers an Object data source.
        DashboardObjectDataSource objItemDetails = new DashboardObjectDataSource("Object Item Details");
        objItemDetails.DataId = "objectItemDetails";
        storage.RegisterDataSource("objItemDetails", objItemDetails.SaveToXml());

        configurator.DataLoading += DataLoading;

    }
    private static void DataLoading(object sender, DataLoadingWebEventArgs e) {
        if (e.DataId == "objectDataItem") {
            e.Data = JsonResultClass.GetDataItems();
        }
        if (e.DataId == "objectItemDetails") {
            e.Data = JsonResultClass.GetItemDetails();
        }
    }
}

