using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YahooMailRead;

namespace BrakesSales
{
    public partial class Index : System.Web.UI.Page
    {
        private static EmailData eData;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Get data from email
            MailParser mailParser = new MailParser("imap.mail.yahoo.com", "kesmilbox123@yahoo.com", "PASSWORD_PLACEHOLDER");
            EmailData emailData = mailParser.getEmailData(0);
            eData = emailData;

            //Show error if could not get the data.
            if (eData.DateReceived == null) { 
                Response.Write("<b>ERROR OCCURRED. RELOAD PAGE NOW, OR TRY AGAIN LATER.</b>");
                Response.End();
                return;
            }

            //Get two emails compared and IsNewProduct property set.
            EmailData oldEmailData = mailParser.getEmailData(1);
            SetIsNewProduct(emailData.Products, oldEmailData.Products);

            //Setup GridView1 so it can be modified by javascript sorting library.
            GridView1.UseAccessibleHeader = true;
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            //Set date lable.
            Con1.InnerText = String.Format("{0:dddd, dd/MM/yyy 'at' hh:mm tt}", eData.DateReceived);
            //Set products on sale.
            Con2.InnerText = eData.Products.Count.ToString();
            //New products today counter.
            Con3.InnerText = eData.Products.Where(p => p.IsNewProduct == "Yes").Count().ToString();
        }

        //Function for datagrid to bind.
        public List<Product> GetProducts()
        {
            return eData.Products;
        }

        //Matching two lists of products and identify which product is new by settings it's isNewProduct property to "Yes"
        static void SetIsNewProduct(List<Product> currentProducts, List<Product> previousProducts)
        {
            foreach (var prod in currentProducts)
            {
                if (!previousProducts.Exists(p => p.Code == prod.Code))
                {
                    prod.IsNewProduct = "Yes";
                }
            }
        }
    }
}