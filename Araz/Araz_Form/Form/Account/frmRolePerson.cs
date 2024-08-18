using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Araz_Form.Form.Account
{
    public partial class frmRolePerson : DevExpress.XtraEditors.XtraForm
    {
        public frmRolePerson()
        {
            InitializeComponent();
            FillData();

        }
        private void FillData()
        {
           // treeListLookUpEdit1.Properties.DataSource = d;
           // treeListLookUpEdit1.Properties.DisplayMember = "CarName";
           // treeListLookUpEdit1.Properties.ValueMember = "Id";
            //treeListLookUpEdit1.Properties.TreeList.ParentFieldName = "ParentId";
        }
    }
}