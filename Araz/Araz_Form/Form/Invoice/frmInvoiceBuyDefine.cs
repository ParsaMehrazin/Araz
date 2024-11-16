using Araz_ViewModel;
using DevExpress.CodeParser;
using DevExpress.DashboardCommon.Viewer;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting.Native;
using DevExpress.XtraSplashScreen;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using ViewModel.ViewModels;
//using static DevExpress.Data.Utils.AsyncDownloader<Value>.LifeTime;

namespace Araz_Form
{
    public partial class frmInvoiceBuyDefine : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int _mod = -1;
        Int64 pkinvoice = -1;
        string select = "";
        string where = "";
        decimal _percent = 0;
        decimal _discount = 0;
        decimal _amount = 0;
        decimal totalPrice = 0;
        bool hasError = false;
        bool _isSave = false;
        List<View_Product> product = new List<View_Product>();
        List<View_InvoiceBuyNumber> _invoiceBuyNumbers = new List<View_InvoiceBuyNumber>();
        View_InvoiceBuyNumber invoice;
        View_Product products;

        public frmInvoiceBuyDefine(int mod, View_InvoiceBuyNumber invoicebuynumber)
        {


            InitializeComponent();
            FillData();
            _mod = mod;
            invoice = invoicebuynumber;
            if (_mod == 1)
            {
                if (!ModOne())
                {
                    this.hasError = true;
                }
            }
            else if (_mod == 2)
            {
                if (!ModTwo(invoice))
                {
                    this.hasError = true;
                }
            }
            else if (mod == 3)
            {
                if (CommonTools.AskQuestion($" آیا از حذف {invoice.fkPersonID} مطمئن هستید؟ "))
                {
                    this.pkinvoice = invoice.pkInvoiceBuyID;
                    //btnSaveProduct_Click(null, null);
                }
            }
            else
            {
                this.Close();
            }

            ApplyPermission();
        }

        public void FillData()
        {
            CommonTools.Loading(true);
            cmbPersonList.Properties.DataSource = DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person", "").ToList();
            select = "SELECT  TOP 1   *   FROM dbo.View_InvoiceBuy1403 AS i";
            where = "WHERE (SELECT MAX(CAST(InvoiceBuyNumber AS INT)) FROM dbo.InvoiceBuy1403 ) = i.InvoiceBuyNumber";
            invoice = DARepository.GetAllFromView<View_InvoiceBuyNumber>(select, where).ToList().FirstOrDefault();
            txtInvoiceBuyNumber.Text = invoice.ComputedInvoiceBuyNumber.ToString() + (Convert.ToInt32(invoice.InvoiceBuyNumber) + 1).ToString();


            CommonTools.Loading();
        }
        public void ApplyPermission()
        {
            CommonTools.Loading(true);

            CommonTools.Loading();
        }

        public bool ModOne()
        {
            try
            {
                this.Text = "ثبت فاکتور خرید";

                dtpLoadingDate.GeorgianDate = DateTime.Now;

                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool ModTwo(View_InvoiceBuyNumber model)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {

                return false;
            }
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
                        product.Add(
                            new View_Product()
                            {
                                pkProductID = item.pkProductID,
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

        private void gvProduct_CustomDrawRowIndicator(
            object sender,
            DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvProduct_CellValueChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
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

        private void gvProduct_FocusedColumnChanged(
            object sender,
            DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
        }

        public void Total()
        {
            totalPrice = 0;
            for (int i = 0; i < gvProduct.DataRowCount; i++)
            {
                totalPrice += Convert.ToDecimal(gvProduct.GetRowCellValue(i, "AllPriceBuy"));
            }
            if (!string.IsNullOrEmpty(txtPercent.Text))
                txtPrice.Text = (totalPrice - ((totalPrice * _percent) / 100)).ToString("N0");
            else if (!string.IsNullOrEmpty(txtdiscount.Text))
                txtPrice.Text = (totalPrice - _discount).ToString("N0");
            else
                txtPrice.Text = totalPrice.ToString("N0");
            _amount = Convert.ToDecimal(txtPrice.Text);
            txtPrice.Text = txtPrice.Text + " ریال";
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {
        }

        private void txtPercent_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPercent.Text))
            {
                txtdiscount.Enabled = false;

                _percent = decimal.Parse(txtPercent.Text);
            }
            else
            {
                txtdiscount.Enabled = true;
                _percent = 0;
            }
            Total();
        }

        private void txtdiscount_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtdiscount.Text))
            {
                txtPercent.Enabled = false;
                _discount = decimal.Parse(txtdiscount.Text);
            }
            else
            {
                txtPercent.Enabled = true;
                _discount = 0;
            }

            Total();
        }

        private void btnSaveAndPrint_Click(object sender, EventArgs e)
        {
            ErrorProvider.ClearErrors();

           

            if (_mod != 3)
            {
                if (string.IsNullOrEmpty(cmbPersonList.Text) || cmbPersonList.EditValue == null)
                    ErrorProvider.SetError(cmbPersonList, "لطفا یک شخص را انتخاب کنید ");

                if (!dtpLoadingDate.GeorgianDate.HasValue || dtpLoadingDate.GeorgianDate.Value == null )
                {
                    ErrorProvider.SetError(dtpLoadingDate, "تاریخ را انتخاب کنید ");
                    return;
                }

                if (gcProduct.DataSource  == null)
                
                    CommonTools.ShowMessage( "لیست محصولات نمیتواند خالی باشد");
                else 
                    gcProduct.DataSource = product;
                //if (string.IsNullOrEmpty(txtBarCode.Text) || txtBarCode.Text == "")
                //    ErrorProvider.SetError(txtBarCode, "نمیتواند خالی باشد");

                //if (string.IsNullOrEmpty(cmbType.Text) || cmbType.EditValue == null)
                //    ErrorProvider.SetError(cmbType, "لطفا یک سمت را انتخاب کنید ");

                //if (string.IsNullOrEmpty(txtCountOne.Text) || txtCountOne.Text == "")
                //    ErrorProvider.SetError(txtCountOne, "نمیتواند خالی باشد");

                //if (string.IsNullOrEmpty(txtBuy.Text) || txtBuy.Text == "")
                //    ErrorProvider.SetError(txtBuy, "نمیتواند خالی باشد");

                //if (string.IsNullOrEmpty(txtSell.Text) || txtSell.Text == "")
                //    ErrorProvider.SetError(txtSell, "نمیتواند خالی باشد");
            }

            if (ErrorProvider.HasErrors)
            {
                return;
            }

            CommonTools.Loading(true);
            //BaseRepositoryResponseViewModel res = null;


            //res = DARepository.ExcuteOperationalSP_New("dbo", "CrudProduct",
            //   new ServiceOperatorParameter() { Name = "mod", Value = _mod },
            //   new ServiceOperatorParameter() { Name = "pkInvoiceBuyID", Value = _mod == 1 ? "-1" : this.pkinvoice.ToString() },
            //   new ServiceOperatorParameter() { Name = "fkFinnantialYear", Value = (cmbNameGroup2.EditValue as View_Product) == null ? -1 : (cmbNameGroup2.EditValue as View_Product).pkGroup2 },
            //   new ServiceOperatorParameter() { Name = "InvoiceBuyNumber", Value = string.IsNullOrEmpty(txtBarCode.Text) ? "" : txtBarCode.Text },
            //   new ServiceOperatorParameter() { Name = "PurchaseInvoiceNumber", Value = string.IsNullOrEmpty(txtProductName.Text) ? "" : txtProductName.Text },
            //   new ServiceOperatorParameter() { Name = "fkPersonID", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "fkProductID", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "fkPrice", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "DateInvoice", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "BuyInvoice", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "Buyquantity", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "Description", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "EditToken", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "InsertUser", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "InsertDate", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
            //   new ServiceOperatorParameter() { Name = "InsertIP", Value = string.IsNullOrEmpty(txtCountOne.Text) ? 0 : Convert.ToInt16(txtCountOne.Text) });







            CommonTools.Loading();

            //if (CommonTools.ShowMessage(res))
            //{
            //    this._isSave = true;
            //  //  FillDataProduct();
            //   // ClearProduct();
            //    this.Close();
            //}
            //else
            //{
            //    this._isSave = false;
            //    this.hasError = true;
            //}
            return;
        }
    }
}