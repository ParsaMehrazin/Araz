using Araz_ViewModel;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
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
//using static DevExpress.Data.Utils.AsyncDownloader<Value>.LifeTime;

namespace Araz_Form.Form.Invoice
{
    public partial class frmInvoiceBuyDefine : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        decimal _percent = 0;
        List<View_Product> product = new List<View_Product>();
        //View_Product products = new View_Product();
        View_Product products;
        public frmInvoiceBuyDefine()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }

        public void FillData()
        {
            cmbPersonList.Properties.DataSource = DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person", "").ToList();
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {

            frmSearchProduct frm = new frmSearchProduct(false);

            frm.ShowDialog();


            if (frm.SelectedProducts != null && frm.SelectedProducts.Count > 0)
            {
                foreach (var item in frm.SelectedProducts)
                {
                    if (!product.Any(p => p.pkProductID == item.pkProductID))
                    {
                        product.Add(new View_Product()
                        {
                            pkProductID = -1,
                            parentProductID = item.parentProductID,
                            pkGroup1 = item.pkGroup1,
                            ParentGroup1 = item.ParentGroup1,
                            NameGroup1 = item.NameGroup1,
                            pkGroup2 = item.pkGroup2,
                            ParentGroup2 = item.ParentGroup2,
                            NameGroup2 = item.NameGroup2,
                            BarCode = item.BarCode,
                            ProductName = item.ProductName,
                            fkTypeID = item.fkTypeID,
                            TypeName = item.TypeName,
                            PriceSell = item.PriceSell,
                            PriceBuy = item.PriceBuy,
                            PriceSellOne = item.PriceSellOne,
                            PriceBuyOne = item.PriceBuyOne,
                            CountSell = item.CountSell,
                            CountBuy = item.CountBuy,
                            AllPriceSell = item.AllPriceSell,
                            AllPriceBuy = item.AllPriceBuy,
                            Count = item.Count,
                            CountOne = item.CountOne,
                            pkPriceID = item.pkPriceID

                        });
                    }
                }

                gcProduct.DataSource = null;
                gcProduct.DataSource = product;

            }
            Total();
        }

        private void gvProduct_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvProduct_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            CommonTools.Loading(true);

            GridView view = sender as GridView;

            if (e.Column.FieldName == "Count" || e.Column.FieldName == "PriceBuy")
            {
                var column5ValueObj = view.GetFocusedRowCellValue("Count");
                var column6ValueObj = view.GetFocusedRowCellValue("PriceBuy");

                if (column5ValueObj != null && column6ValueObj != null)
                {

                    if (decimal.TryParse(column5ValueObj.ToString(), out decimal column5Value) &&
                        decimal.TryParse(column6ValueObj.ToString(), out decimal column6Value))
                    {
                        decimal result = column5Value * column6Value;
                        view.SetRowCellValue(e.RowHandle, "AllPriceBuy", result);
                    }
                }
            }
            Total();
            CommonTools.Loading();
        }

        private void gvProduct_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {

        }
        public void Total()
        {
            decimal totalPrice = 0;

            for (int i = 0; i < gvProduct.DataRowCount; i++)
            {
                totalPrice += Convert.ToDecimal(gvProduct.GetRowCellValue(i, "AllPriceBuy"));
            }
            if (_percent.ToString() != null || _percent > 0)
                txtPrice.Text = (totalPrice - ((totalPrice * _percent) / 100)).ToString("N0");
            else
                txtPrice.Text = totalPrice.ToString("N0");

        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPercent_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPercent != null && txtPercent.Text != "")
                _percent = decimal.Parse(txtPercent.Text);
            else
                _percent = 0;
            Total();
        }
    }
}