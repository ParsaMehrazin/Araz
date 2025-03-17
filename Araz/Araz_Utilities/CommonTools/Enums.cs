using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Enums
    {
  



        public static int GetFinancialYearNumber(int year)
        {
            var financialYears = new Dictionary<int, int>
            {
                 { 1403, 1 },
                 { 1404, 2 },
                 { 1405, 3 },
                 { 1406, 4 },
                 { 1407, 5 },
                 { 1408, 6 },
                 { 1409, 7 },
                 { 1410, 8 },
                 { 1411, 9 },
                 { 1412, 10 },
                 { 1413, 11 },
                 { 1414, 12 },
                 { 1415, 13 },
                 { 1416, 14 },
                 { 1417, 15 },
                 { 1418, 16 },
                 { 1419, 17 },
                 { 1420, 18 },
                 { 1421, 19 }
            };
            return financialYears.ContainsKey(year) ? financialYears[year] : -1;
        }
    }

}
