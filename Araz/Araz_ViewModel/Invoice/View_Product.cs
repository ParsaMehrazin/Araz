using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araz_ViewModel
{
    public class View_Product
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
        public float? PriceSell { get; set; }
        public float? PriceBuy { get; set; }
        public float? PriceSellOne { get; set; }
        public float? PriceBuyOne { get; set; }
        public float? CountSell { get; set; }
        public float? CountBuy { get; set; }
        public float? AllPriceSell { get; set; }
        public float? AllPriceBuy { get;set; }
        public float? Count {  get; set; }
        public float? CountOne { get; set; }
        public long? pkPriceID { get; set; }
        public string Invoice { get; set; }
    }
}


  