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
        public double PriceSell { get; set; }
        public double PriceBuy {  get; set; }    
        public long? Invoice {  get; set; }
        public double CountBuy { get; set; }
        public double CountSell { get; set; }

    }
}
