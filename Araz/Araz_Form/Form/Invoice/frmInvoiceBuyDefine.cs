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
        double _percent = 0;
        double _discount = 0;
        double _amount = 0;
        double totalPrice = 0;
        
        int invoicebuyNumber = 0;
        int fkfinancialYearNumber = 0;
      bool hasError = false;
        bool _isSave = false;
        List<View_Product> product = new List<View_Product>();
        View_InvoiceBuyNumber _invoiceBuyNumbers = new View_InvoiceBuyNumber();
        View_InvoiceBuyNumber invoice;
      //  View_Product products;

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
            invoicebuyNumber = Convert.ToInt16(invoice.InvoiceBuyNumber) + 1;
            txtInvoiceBuyNumber.Text = invoice.ComputedInvoiceBuyNumber.ToString() + (invoicebuyNumber).ToString();


            _invoiceBuyNumbers = DARepository.GetAllFromView<View_InvoiceBuyNumber>("SELECT MAX(pkInvoiceBuyID) AS pkInvoiceBuyID FROM dbo.InvoiceBuy1403", "").FirstOrDefault();
            pkinvoice = _invoiceBuyNumbers.pkInvoiceBuyID + 1;

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
                                pkPriceID = item.pkPriceID,
                                percentdiscount =item.percentdiscount,
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
            Int16 persenttotal = 0;
            if (e.Column.FieldName == "Count" || e.Column.FieldName == "PriceBuy" || e.Column.FieldName == "percentdiscount")
            {
                var column5ValueObj = view.GetFocusedRowCellValue("Count");
                var column6ValueObj = view.GetFocusedRowCellValue("PriceBuy");
                var column7ValueObj = view.GetFocusedRowCellValue("percentdiscount");

                if (column5ValueObj != null && column6ValueObj != null)
                {
                    if (decimal.TryParse(column5ValueObj.ToString(), out decimal column5Value) &&
                        decimal.TryParse(column6ValueObj.ToString(), out decimal column6Value) && 
                        decimal.TryParse(column7ValueObj.ToString(), out decimal column7Value))
                    {                               
                            decimal result = (column5Value * column6Value)-(column5Value * column6Value) * (column7Value / 100);
                            view.SetRowCellValue(e.RowHandle, "AllPriceBuy", result);

                        for (int i = 0; i < gvProduct.DataRowCount; i++)
                        {
                             persenttotal += Convert.ToInt16(gvProduct.GetRowCellValue(i, "percentdiscount"));
                        }
                        if (persenttotal > 0)
                        { 
                                txtdiscount.Enabled = false;
                                txtPercent.Enabled = false;
                        }
                        else
                        {
                                txtdiscount.Enabled = true;
                                txtPercent.Enabled = true;
                        }
                        
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
                totalPrice += Convert.ToDouble(gvProduct.GetRowCellValue(i, "AllPriceBuy"));
            }
            if (!string.IsNullOrEmpty(txtPercent.Text))
                txtPrice.Text = (totalPrice - ((totalPrice * _percent) / 100)).ToString("N0");
            else if (!string.IsNullOrEmpty(txtdiscount.Text))
                txtPrice.Text = (totalPrice - _discount).ToString("N0");
            else
                txtPrice.Text = totalPrice.ToString("N0");
            _amount = Convert.ToDouble(txtPrice.Text);
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

                _percent = Convert.ToDouble(txtPercent.Text);
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
                _discount = Convert.ToDouble(txtdiscount.Text);
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
            var dtList = DARepository.ListToDataTable(new List<View_InvoiceBuyNumber>());
            

            if (_mod != 3)
            {
                if (string.IsNullOrEmpty(cmbPersonList.Text) || cmbPersonList.EditValue == null)
                    ErrorProvider.SetError(cmbPersonList, "لطفا یک شخص را انتخاب کنید ");

                if (!dtpLoadingDate.GeorgianDate.HasValue || dtpLoadingDate.GeorgianDate.Value == null)
                {

                    CommonTools.ShowMessage("تاریخ نمی تواند خالی باشد");
                    return;
                    }
                    else
                { 
                    int year = Convert.ToInt16(dtpLoadingDate.GeorgianDate.Value.ToPersianYear());
                int financialYearNumber = Enums.GetFinancialYearNumber(year);

                if (financialYearNumber != -1)
                    fkfinancialYearNumber = financialYearNumber;
                else
                {
                    CommonTools.ShowMessage($"سال {year} در داده‌ها موجود نیست.");
                }
                }



                if (gcProduct.DataSource  == null)
                
                    CommonTools.ShowMessage( "لیست محصولات نمیتواند خالی باشد");
                else 
                     product = gcProduct.DataSource as List<View_Product>;
              
                }

    


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


            if (ErrorProvider.HasErrors)
            {
                return;
            }
            CommonTools.Loading(true);
            BaseRepositoryResponseViewModel res = null;
            BaseRepositoryResponseViewModel res1 = null;
            foreach (var productgrid in product)
            {           

                res = DARepository.ExcuteOperationalSP_New("dbo", "CrudInvoiceBuy",
                   new ServiceOperatorParameter() { Name = "mod", Value = _mod },
                   new ServiceOperatorParameter() { Name = "pkInvoiceBuyID", Value = _mod == 1 ? "-1" : this.pkinvoice.ToString() },
                   new ServiceOperatorParameter() { Name = "fkFinnantialYear", Value = fkfinancialYearNumber },
                   new ServiceOperatorParameter() { Name = "InvoiceBuyNumber", Value = invoicebuyNumber.ToString() },
                   new ServiceOperatorParameter() { Name = "PurchaseInvoiceNumber", Value = string.IsNullOrEmpty(txtInvoiceBuyNumber.Text) ? "" : txtInvoiceBuyNumber.Text },
                   new ServiceOperatorParameter() { Name = "fkPersonID", Value = (cmbPersonList.EditValue as View_Person) == null ? -1 : (cmbPersonList.EditValue as View_Person).pkPersonID },
                   new ServiceOperatorParameter() { Name = "fkProductID", Value = productgrid.pkProductID },
                   new ServiceOperatorParameter() { Name = "fkPrice", Value = productgrid.pkPriceID },
                   new ServiceOperatorParameter() { Name = "DateInvoice", Value = dtpLoadingDate.GeorgianDate.Value },
                   new ServiceOperatorParameter() { Name = "BuyInvoice", Value = productgrid.PriceBuy },
                   new ServiceOperatorParameter() { Name = "Buyquantity", Value = productgrid.AllPriceBuy },
                   new ServiceOperatorParameter() { Name = "Description", Value = string.IsNullOrEmpty(productgrid.Description) ? "" : productgrid.Description },
                   new ServiceOperatorParameter() { Name = "EditToken", Value = "" },
                   new ServiceOperatorParameter() { Name = "InsertUser", Value = 12345 },
                   //new ServiceOperatorParameter() { Name = "InsertDate", Value = new DateTime() },
                   new ServiceOperatorParameter() { Name = "InsertIP", Value = "193.168.2.5" },
                   new ServiceOperatorParameter() { Name = "percentdiscount", Value = string.IsNullOrEmpty(txtPercent.Text) ? productgrid.percentdiscount.ToString() : txtPercent.Text },
                   new ServiceOperatorParameter() { Name = "discountamount", Value = string.IsNullOrEmpty(txtdiscount.Text) ? "0" : txtdiscount.Text });



                res1 = DARepository.ExcuteOperationalSP_New("dbo", "CrudPrice",
                   new ServiceOperatorParameter() { Name = "mod", Value = _mod },
                   new ServiceOperatorParameter() { Name = "pkPriceID", Value = _mod == 1 ? "-1" : productgrid.pkPriceID.ToString() },
                   new ServiceOperatorParameter() { Name = "fkProductID", Value = productgrid.pkProductID.ToString() },
                   new ServiceOperatorParameter() { Name = "PriceSell", Value =  0 },
                   new ServiceOperatorParameter() { Name = "PriceBuy", Value =  productgrid.PriceBuy },
                   new ServiceOperatorParameter() { Name = "Invoice", Value = pkinvoice.ToString() },
                   new ServiceOperatorParameter() { Name = "CountSell", Value = 0 },
                   new ServiceOperatorParameter() { Name = "CountBuy", Value = productgrid.Count });


                CommonTools.Loading();
            }
                if (CommonTools.ShowMessage(res))
                {
                    this._isSave = true;
                    //  FillDataProduct();
                    // ClearProduct();    
                }
                else
                {
                    this._isSave = false;
                    this.hasError = true;
                }
            }
            //return;
        }
    
}