using System.Data;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AspNetCoreDashboardBackend.Models;

public class JsonResultClass {
    public static DataTable CreateDataAsync() {
        using (var httpClient = new HttpClient()) {
            string host = "https://bosbi.beonesolution.com/api";
            string query = "q_paging=10&q_category_id=1edad1e8-a187-6974-8a71-b38140175139&q_company=Bara Indah Sinergi&q_name=Sales&q_code=ds1edf2d44e6956fc883b8fa163e42aa00&q_type=All Company";

            var response = httpClient.PostAsync($"{host}/v1/datasource/?{query}", null).Result;

            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Phone", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Logo", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Desc", typeof(string));

            if (response.IsSuccessStatusCode) {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var jsonNode = JsonNode.Parse(jsonString);

                JsonArray? data = jsonNode?["data"]?["data"]?.AsArray();
                if (data != null) {
                    foreach (JsonNode? d in data) {
                        JsonArray? companies = d?["companies"]?.AsArray();
                        if (companies != null) {
                            foreach (var company in companies) {
                                string? name = company?["name"]?.ToString();
                                string? phone = company?["phone"]?.ToString();
                                string? email = company?["email"]?.ToString();
                                string? address = company?["address"]?.ToString();
                                string? logo = company?["logo"]?.ToString();
                                string? status = company?["status"]?.ToString();
                                string? desc = company?["desc"]?.ToString();

                                var objectCompany = new Company()
                                {
                                    Name = name,
                                    Phone = phone,
                                    Email = email,
                                    Address = address,
                                    Logo = logo,
                                    Status = status,
                                    Desc = desc
                                };

                                table.Rows.Add(name, phone, email, address, logo, status, desc);
                            }
                        }
                    }
                } else {
                    Console.WriteLine("Data node not found.");
                }
            } else {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }

            return table;
        }
    }
}
