using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araz_ViewModel
{
    public class View_Product : View_InvoiceBuy
    {
        public bool Selected { get; set; }
        public long pkGroup1 { get; set; }
        public long? ParentGroup1 { get; set; }
        public string NameGroup1 { get; set; }
        public long pkGroup2 { get; set; }
        public long? ParentGroup2 { get; set; }
        public string NameGroup2 { get; set; }
        public long pkProductID { get; set; }
        public long? parentProductID { get; set; }
        public string BarCode { get; set; }
        public string ProductName { get; set; }
        public long? fkTypeID { get; set; }
        public string TypeName { get; set; }
        public double? PriceSell { get; set; }
        public double? PriceBuy { get; set; }
        public double? PriceSellOne { get; set; }
        public double? PriceBuyOne { get; set; }
        public double? CountSell { get; set; }
        public double? CountBuy { get; set; }
        public double? AllPriceSell { get; set; }
        public double? AllPriceBuy { get;set; }
        public double? Count {  get; set; }
        public double? CountOne { get; set; }
        public long? pkPriceID { get; set; }
        public string Invoice { get; set; }
        public long pkPersonID { get; set; }    

    }
}


  