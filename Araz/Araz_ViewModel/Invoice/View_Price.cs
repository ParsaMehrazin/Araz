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
        public float PriceSell { get; set; }
        public float PriceBuy {  get; set; }    
        public long? Invoice {  get; set; }
 
    }
}
