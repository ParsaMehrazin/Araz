using Araz_Form.Form.Product;
using Araz_ViewModel;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Customization;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utilities;

namespace Araz_Form
{
    public partial class frmSearchProduct : DevExpress.XtraEditors.XtraForm
    {
        
        public List<View_Product> SelectedProducts = new List<View_Product>();
        View_Product product = new View_Product();
        private bool singleSelect = false;
        string changes = "";
        public frmSearchProduct(bool SingleSelect , string model )
        {
            InitializeComponent();       
            FillData();        
            changes = model;    
            singleSelect = SingleSelect;
            if (singleSelect)
            {
                this.Text += " ( تک انتخابی )";
                gridColumnSelect.Visible = false;
               lcgSelectedProduct.Expanded = false;
               lcgSelectedProduct.ExpandButtonVisible = false;
            }
            else
            {
                this.Text += " ( چند انتخابی )";
                gridColumnSelect.Visible = true;
               lcgSelectedProduct.Expanded = true;
               lcgSelectedProduct.ExpandButtonVisible = true;
            }
        }
        private void FillData()
        {
            var select = "";
            var where = " Where 1 = 1 ";
            select = "SELECT DISTINCT(NameGroup2),NameGroup1,pkGroup1,pkGroup2 FROM dbo.View_Product";
            where = "WHERE pkGroup1 = ParentGroup2 OR pkGroup1=1";
            cmbGroup.Properties.DataSource = DARepository.GetAllFromView<View_Product>(select, where).ToList();
       
        }


        string sell = "";
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            CommonTools.Loading(true);          
            var grp = (cmbGroup.EditValue as View_Product);
            string where = " WHERE 1 = 1 ";
            string select = "";
            if (changes == "Sell")
                 sell = " AND Count <> 0 ";
            else
                sell = "";
            if (grp != null)
            {
                select = "SELECT DISTINCT(ProductName),* FROM dbo.View_Product";
                where = "Where ParentProductID = " + grp.pkGroup2 + sell;
            }
            if (!string.IsNullOrEmpty(txtProductName.Text))          
                where = "Where ParentProductID = " + grp.pkGroup2 + " AND ProductName LIKE '%" + txtProductName.Text + "%'" +  sell;
            gcProductList.DataSource = DARepository.GetAllFromView<View_Product>(select,where).ToList();
            CommonTools.Loading();
        }

        private void gvProductList_DoubleClick(object sender, EventArgs e)
        {
            var t = gvProductList.GetFocusedRow() as View_Product;
            if (t != null)
            {
                SelectedProducts.Clear();
                SelectedProducts.Add(t);
                this.Close();
            }
        }

        private void btnClearSelection_Click(object sender, EventArgs e)
        {
            SelectedProducts.Clear();
            gcSelection.DataSource = null;
        }

         


        private void treeListLookUpEdit1TreeList_CustomDrawNodeIndicator_1(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {
            if (e.Node.Id >= 0)
                e.Info.DisplayText = (e.Node.Id + 1).ToString();
        }

        private void gvProductList_CustomDrawRowIndicator_1(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }   
     

        private void btnSelectProductGrid_Click(object sender, EventArgs e)
        {

            //var t = gvProductList.GetFocusedRow() as View_Product;
            //if (t != null)
            //{
            //    if (singleSelect)
            //        SelectedProducts.Clear();  
            //        SelectedProducts.Add(t);
            //        gcSelection.DataSource = null;
            //        gcSelection.DataSource = SelectedProducts;
                
            //}

            var t = gvProductList.GetFocusedRow() as View_Product;
            if (t != null)
            {
                if (singleSelect)
                    SelectedProducts.Clear();
            
                if (!SelectedProducts.Contains(t))
                {
                    SelectedProducts.Add(t);
                }

                gcSelection.DataSource = null;
                gcSelection.DataSource = SelectedProducts;
            }





        }
        private void btnAddProduct_Click(object sender, EventArgs e)
        {

            frmProductDefine frm = new frmProductDefine(1, null);           
                frm.ShowDialog();             
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SelectedProducts.Clear();
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var t = gvSelection.GetFocusedRow() as View_Product;
            if (t != null)
            {
                SelectedProducts.Remove(t);
                gcSelection.DataSource = null;
                gcSelection.DataSource = SelectedProducts;
            }
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            var t = gvProductList.GetFocusedRow() as View_Product;
            if (t != null)
            {
                if (singleSelect)
                    SelectedProducts.Clear();
                if (!SelectedProducts.Any(p => p.pkProductID == t.pkProductID))
                    SelectedProducts.Add(t);
                this.Close();
            }
        }

        private void cmbGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbGroup.EditValue as View_Product != null)
                btnRefresh_Click(null, null);
        }
  

        private void gvSelection_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void allselect_Click(object sender, EventArgs e)
        {            
                var allProducts = gvProductList.DataSource as List<View_Product>;

                if (allProducts != null)
                {
                    foreach (var product in allProducts)
                    {
                        if (!SelectedProducts.Any(p => p.pkProductID == product.pkProductID))
                        {
                            if (singleSelect)
                                SelectedProducts.Clear();

                            if (!SelectedProducts.Contains(product))
                            {
                                SelectedProducts.Add(product);
                            }

                            gcSelection.DataSource = null;
                            gcSelection.DataSource = SelectedProducts;
                        }
                    }
                }     
        }
    }
}

    
