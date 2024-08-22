using System;
using System.Collections.Generic;

namespace AspNetCoreDashboardBackend.Models;

public class Invoices {
    static Random rnd = new Random();

    public static List<InvoiceItem> CreateData() {
        List<InvoiceItem> data = new List<InvoiceItem>();
data.Add(new InvoiceItem { Country = "Germany", City = "Aachen", ProductName = "Raclette Courdavault", OrderDate = GenerateOrderDate(), Quantity = 30, Discount = 0, ExtendedPrice = 1650, Freigth = 149.47, UnitPrice = 55 });
        data.Add(new InvoiceItem { Country = "Germany", City = "Berlin", ProductName = "Raclette Courdavault", OrderDate = GenerateOrderDate(), Quantity = 15, Discount = 0, ExtendedPrice = 825, Freigth = 69.53, UnitPrice = 55 });
        data.Add(new InvoiceItem { Country = "Germany", City = "Brandenburg", ProductName = "Raclette Courdavault", OrderDate = GenerateOrderDate(), Quantity = 61, Discount = 0, ExtendedPrice = 2959, Freigth = 42.33, UnitPrice = 99 });
        // ...
        return data;
    }

    static DateTime GenerateOrderDate() {
        int startYear = DateTime.Today.Year - 3;
        int endYear = DateTime.Today.Year;
        return new DateTime(rnd.Next(startYear, endYear), rnd.Next(1, 13), rnd.Next(1, 29));
    }
}

public class InvoiceItem {
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? ProductName { get; set; }
    public DateTime OrderDate { get; set; }
    public int Quantity { get; set; }
    public double Discount { get; set; }
    public double ExtendedPrice { get; set; }
    public double Freigth { get; set; }
    public double UnitPrice { get; set; }
}

