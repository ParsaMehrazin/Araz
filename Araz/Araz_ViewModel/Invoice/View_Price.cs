using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araz_ViewModel
{
    public class View_Price
    {
        public long pkPriceID { get; set; }
        public long? fkProductID { get; set; }
        public decimal PriceSell { get; set; }
        public decimal PriceBuy {  get; set; }    
        public long? Invoice {  get; set; }
        public decimal CountBuy { get; set; }
        public decimal CountSell { get; set; }

    }
}
