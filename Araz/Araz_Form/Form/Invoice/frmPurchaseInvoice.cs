using Araz_Form.Form.Account;
using Araz_ViewModel;
using DevExpress.XtraBars;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace Araz_Form
{
    public partial class frmPurchaseInvoice : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmPurchaseInvoice()
        {
            InitializeComponent();
            FillData();
        }

        public void FillData()
        {
            CommonTools.Loading(true);
            gcProductList.DataSource = DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person", "");
            CommonTools.Loading();  
        }

        private void gvProductList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmRolePerson frm = new frmRolePerson();            
            frm.ShowDialog();

        }
    }
}