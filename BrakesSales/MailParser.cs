using HtmlAgilityPack;
using Limilabs.Client.IMAP;
using Limilabs.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMailRead
{
    class MailParser
    {
        private string _server;
        private string _user;
        private string _password;

        public MailParser(string server, string user, string password)
        {
            _server = server;
            _user = user;
            _password = password;
        }

        private List<Product> getListOfProducts(IMail email)
        {
            List<Product> productsList = new List<Product>();
            //Get content of email as html string.
            string body = email.GetBodyAsHtml();
            //forwarded emails get extra html element which gets removed if present.
            body = body.Replace("<tbody>", "").Replace("</tbody>", "");
            //Create html document object.
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(body);
            //Retrieve html of products table from email body.
            var productsTable = htmlDoc.DocumentNode.SelectSingleNode("//table[@class=\"stats_table\"]");
            //Create nodes collection of table rows.
            var products = productsTable.ChildNodes.Where(node => node.Name == "tr").ToList();
            //Remove first node as it stores headings of the table.
            products.RemoveAt(0);
            //Create objects of 'Product' class for each node (node is a row of the products table).
            foreach (var item in products)
            {
                //Individual cells or a row node.
                var cells = item.ChildNodes.Where(node => node.Name == "td").ToList();
                //Create object based on cells data.
                DateTime dt = DateTime.ParseExact(cells[2].InnerHtml, "dd/MM/yyyy", null);
                bool vat = cells[6].InnerHtml.Contains("Yes") ? true : false;
                string linkToProduct = cells[9].SelectSingleNode("a").GetAttributeValue("href", "NONE");
                Product prod = new Product(cells[0].InnerHtml, cells[1].InnerHtml, dt, cells[3].InnerHtml, cells[4].InnerHtml, double.Parse(cells[5].InnerHtml.Substring(1)), vat, cells[7].InnerHtml, cells[8].InnerHtml, linkToProduct);
                //Set other properties based on color codes.
                string bgcolour = item.GetAttributeValue("bgcolor", "");
                switch (bgcolour)
                {
                    case "#95CAFF":
                        prod.NewItem = "Yes";
                        break;
                    case "#FF6347":
                        prod.ReducedPrice = "Yes";
                        break;
                    case "#FFFF33":
                        prod.Weighted = "Yes";
                        break;
                    default:
                        break;
                }
                //Add to products list.
                productsList.Add(prod);
            }
            //Return products.
            return productsList;
        }

        public EmailData getEmailData(int emailId)
        {
            DateTime? emailDate = null;
            List<Product> productsList = new List<Product>();
            using (Imap imap = new Imap())
            {
                try
                {
                    //connect to mail service and select "inbox" folder.
                    imap.ConnectSSL(_server);
                    imap.Login(_user, _password);
                    imap.SelectInbox();
                    //get list of emails from staff sales.
                    List<long> uidList = imap.Search(Expression.Subject("Premier Park Staff Sales"));
                    //Inverse emails list so at index 0 we have newest email.
                    uidList.Reverse();
                    //Retrieve email by emailId.
                    IMail email = new MailBuilder().CreateFromEml(imap.GetMessageByUID(uidList[emailId]));
                    //Get date.
                    emailDate = email.Date;
                    //Get products list.
                    productsList = getListOfProducts(email);
                    //Close connection.
                    imap.Close();
                } catch (Exception e) {}
            }
            return new EmailData(productsList, emailDate);
        }


    }
}
