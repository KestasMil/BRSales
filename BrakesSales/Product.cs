using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMailRead
{
    public class Product
    {
        //Property always false by default. Need to be set after creation if needed.
        //This property is set by comparing two emails data, not based on what email says.
        public string IsNewProduct { get; set; } = "No";
        //Properties based on email color codes in product table.
        public string Weighted { get; set; } = "No";
        public string NewItem { get; set; } = "No";
        public string ReducedPrice { get; set; } = "No";
        //Product info to be set on object creation.
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime BBE_Date { get; set; }
        public string Temp { get; set; }
        public string Depot { get; set; }
        public double SalePrice { get; set; }
        public bool VAT { get; set; }
        public string SaleSystem { get; set; }
        public string CompareTheMarket { get; set; }
        public string LinkToProduct { get; set; }

        public Product(string code, string description, DateTime bBE_Date, string temp, string depot, double salePrice, bool vAT, string saleSystem, string compareTheMarket, string linkToProduct)
        {
            Code = code;
            Description = description;
            BBE_Date = bBE_Date;
            Temp = temp;
            Depot = depot;
            SalePrice = salePrice;
            VAT = vAT;
            SaleSystem = saleSystem;
            CompareTheMarket = compareTheMarket;
            LinkToProduct = linkToProduct;
        }
    }
}
