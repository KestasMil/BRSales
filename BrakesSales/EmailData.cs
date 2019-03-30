using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMailRead
{
    class EmailData
    {
        public List<Product> Products { get; set; }
        public DateTime? DateReceived { get; set; }

        public EmailData(List<Product> products, DateTime? dateReceived)
        {
            Products = products;
            DateReceived = dateReceived;
        }
    }
}
