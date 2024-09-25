using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Araz_ViewModel
{
    public class View_Role
    {
        public long? pkRoleID { get; set; }         //-----آی دی سمت 
        public long? ParentRole { get; set; }       //-----
        public string RoleName { get; set; }         //-----سمت
      
    }
}
