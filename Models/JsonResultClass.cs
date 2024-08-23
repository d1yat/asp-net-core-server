using System.Data;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AspNetCoreDashboardBackend.Models;

public class JsonResultClass {
    public static List<DataItem> GetDataItems() {
        using (var httpClient = new HttpClient()) {
            string apiUrl = "https://bosbi.beonesolution.com/api";
            string queryParams = "q_paging=10&q_category_id=1edad1e8-a187-6974-8a71-b38140175139&q_company=Bara Indah Sinergi&q_name=Sales&q_code=ds1edf2d44e6956fc883b8fa163e42aa00&q_type=All Company";

            var responseMessage = httpClient.PostAsync($"{apiUrl}/v1/datasource/?{queryParams}", null).Result;

            List<DataItem> dataItems = new List<DataItem>();

            if (responseMessage.IsSuccessStatusCode) {
                var jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var jsonNode = JsonNode.Parse(jsonResponse);

                JsonArray? dataArray = jsonNode?["data"]?.AsArray();
                if (dataArray != null) {
                    foreach (JsonNode? dataItemNode in dataArray) {
                        DataItem dataItem = new DataItem() {
                            Id = dataItemNode?["id"]?.ToString(),
                            Name = dataItemNode?["name"]?.ToString()
                        };

                        dataItems.Add(dataItem);
                    }
                } else {
                    Console.WriteLine("Data array not found.");
                }
            } else {
                Console.WriteLine($"Request failed with status code: {responseMessage.StatusCode}");
            }

            return dataItems;
        }
    }

    public static List<ItemDetails> GetItemDetails() {
        using (var httpClient = new HttpClient()) {
            string apiUrl = "https://bosbi.beonesolution.com/api";
            string queryParams = "1edf2cf3-59dc-6c40-a7b5-fa163e42aa00/show?company_id=1edc3415-71ce-68aa-87f5-fa163e42aa00";

            var responseMessage = httpClient.PostAsync($"{apiUrl}/v1/datasource/{queryParams}", null).Result;

            List<ItemDetails> itemDetailsList = new List<ItemDetails>();

            if (responseMessage.IsSuccessStatusCode) {
                var jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                var jsonRootNode = JsonNode.Parse(jsonResponse);

                string? itemDataArray = jsonRootNode?["data"]?["datas"]?.ToString();
                if (!string.IsNullOrEmpty(itemDataArray)) {
                    var options = new JsonSerializerOptions {
                        PropertyNameCaseInsensitive = true
                    };

                    // Deserialisasi JSON menjadi List<ItemDetails>
                    List<ItemDetails>? itemList = JsonSerializer.Deserialize<List<ItemDetails>>(itemDataArray, options);
                    if (itemList != null) {
                        itemDetailsList.AddRange(itemList);
                    } else {
                        Console.WriteLine("Failed to deserialize item data.");
                    }
                } else {
                    Console.WriteLine("Item data array not found.");
                }
            } else {
                Console.WriteLine($"Request failed with status code: {responseMessage.StatusCode}");
            }

            return itemDetailsList;
        }
    }
}

