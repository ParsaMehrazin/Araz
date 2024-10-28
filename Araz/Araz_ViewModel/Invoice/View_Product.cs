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
        public decimal? PriceSell { get; set; }
        public decimal? PriceBuy { get; set; }
        public decimal? PriceSellOne { get; set; }
        public decimal? PriceBuyOne { get; set; }
        public decimal? CountSell { get; set; }
        public decimal? CountBuy { get; set; }
        public decimal? AllPriceSell { get; set; }
        public decimal? AllPriceBuy { get;set; }
        public decimal? Count {  get; set; }
        public decimal? CountOne { get; set; }
        public long? pkPriceID { get; set; }
        public string Invoice { get; set; }
    }
}


  