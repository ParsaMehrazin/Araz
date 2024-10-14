using Araz_Form.Form.Account;
using Araz_ViewModel;
using DevExpress.CodeParser;
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
    public partial class frmProductList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string select = "";
        string where = "";
        public frmProductList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }

        public void FillData()
        {
           
            cmbNameGroup1.Properties.DataSource = DARepository.GetAllFromView<View_Product>("SELECT DISTINCT(NameGroup1),pkGroup1,ParentGroup1 FROM dbo.View_Product ", "");
           
        }

        private void gvProductList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
         
        }

        private void cmbNameGroup1_EditValueChanged(object sender, EventArgs e)
        {
            var item = cmbNameGroup1.EditValue as View_Product;
            if (item != null)
            {
                var select = "SELECT DISTINCT(NameGroup2),pkGroup2,ParentGroup2 FROM dbo.View_Product";
                var where = "WHERE ParentGroup2 = " + item.pkGroup1;
                cmbNameGroup2.Properties.DataSource = DARepository.GetAllFromView<View_Product>(select, where).ToList();
                
            }   
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var item = cmbNameGroup1.EditValue as View_Product;
            var item2 = cmbNameGroup2.EditValue as View_Product;
            if (item == null || item2 == null || cmbNameGroup2.Text=="")
            {                
                select = "SELECT * FROM dbo.View_Product";
                where = "" ;
            }
            else if (item != null && item2 != null)
            {
                select = "SELECT * FROM dbo.View_Product";
                where = "WHERE ParentProductID = " + item2.pkGroup2;
            }
            

            gcProductList.DataSource = DARepository.GetAllFromView<View_Product>(select, where).ToList();
        }
    }
}