using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.ViewModels;

namespace Araz_ViewModel
{
    public class View_InvoiceSell : BaseLogResponseViewModel
    {
        public long pkInvoiceSellID { get; set; }
        public long fkFinnantialYear { get; set; }
        public long FinnantialYear { get; set; }
        public string InvoiceSellNumber { get; set; }
        public string PurchaseInvoiceNumber { get; set; }
        public string ComputedInvoiceSellNumber { get; set; }
        public long fkPersonID { get; set; }
        public long fkProductID { get; set; }
        public long fkPrice { get; set; }
        public double? SellInvoice { get; set; }
        public double? Sellquantity { get; set; }
        public string Description { get; set; }
        public int percentdiscount { get; set; }
        public double? discountamount { get; set; }
    }
}