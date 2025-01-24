using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace Araz_ViewModel
{
    public class View_InvoiceBuy : BaseLogResponseViewModel
    {
        public long pkInvoiceBuyID { get; set; }
        public long fkFinnantialYear { get; set; }
        public long FinnantialYear { get; set; }
        public string InvoiceBuyNumber { get; set; }
        public string PurchaseInvoiceNumber { get; set; }
        public string ComputedInvoiceBuyNumber { get; set; }
        public long fkPersonID { get; set; }
        public long fkProductID { get; set; }
        public long fkPrice { get; set; }
        public double? BuyInvoice { get; set; }
        public double? Buyquantity { get; set; }
        public string Description { get; set; }
        public int percentdiscount { get; set; }    
        public double? discountamount { get; set; }
    }
}
