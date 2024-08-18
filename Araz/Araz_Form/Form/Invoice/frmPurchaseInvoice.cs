using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Araz_Form
{
    public partial class frmPurchaseInvoice : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmPurchaseInvoice()
        {
            InitializeComponent();
        }

        public void FillData()
        {

        }

        private void gvProductList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }
        


    }
}