using Araz_ViewModel;
using DevExpress.XtraEditors;
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
using ViewModel.ViewModels;
using static Utilities.Enums;

namespace Araz_Form.Form.Product
{
    public partial class frmProductDefine : DevExpress.XtraEditors.XtraForm
    {
        string select = "";
        string where = "";
        int _mod = -1;
        Int64 CountSell = 0;
        Int64 CountBuy = 0;
        string parentproduct = "";
        Int64 pkproductid = -1;
        string invoice = "-1";
        Int64 pkPriceID = -1;
        bool _isSave = false;
        bool hasError = false;
        View_Price _price = new View_Price();
        View_Product product = new View_Product();
        List<View_Type> type = new List<View_Type>();
        public frmProductDefine(int mod , View_Product model)
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillDataProduct();
            _mod = mod;
            product = model;
            if (_mod == 1)
            {
                if (!ModOne())
                {
                    this.hasError = true;
                    this.Close();
                }
            }
            else if (_mod == 2)
            {
                if (!ModTwo(product))
                {
                    this.hasError = true;
                    this.Close();
                }
            }
            else if (mod == 3)
            {
                if (CommonTools.AskQuestion($" آیا از حذف {product.ProductName} مطمئن هستید؟ "))
                {
                    this.pkproductid = product.pkProductID;
                    btnSaveProduct_Click(null, null);
                }
            }
            else
            {
                this.Close();
            }
            CommonTools.Loading();
        }
        public void FillDataProduct()
        {
            cmbNameGroup1.Properties.DataSource = DARepository.GetAllFromView<View_Product>("SELECT DISTINCT(NameGroup1),pkGroup1,ParentGroup1 FROM dbo.View_Product ", "").ToList();
            cmbType.Properties.DataSource = DARepository.GetAllFromView<View_Type>("SELECT * FROM dbo.View_Type ", "").ToList();
        }
        public bool ModOne()
        {
            try
            {
                this.Text = "ثبت محصول جدید";
                _mod = 1;
                CountSell = 0;
                CountBuy = 0;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ModTwo(View_Product model)
        {
            try
            {
                this.Text = "ویرایش محصول جدید";
                product = model;
                pkproductid = product.pkProductID;
                pkPriceID = Convert.ToInt64(product.pkPriceID);
                _mod = 2;
                cmbNameGroup1.EditValue = (cmbNameGroup1.Properties.DataSource as List<View_Product>).Where(p => p.pkGroup1 == product.pkGroup1).FirstOrDefault();
                cmbNameGroup2.EditValue = (cmbNameGroup2.Properties.DataSource as List<View_Product>).Where(p => p.pkGroup2 == product.pkGroup2).FirstOrDefault();
                txtProductName.Text = product.ProductName;
                txtBarCode.Text = product.BarCode;
                cmbType.EditValue = (cmbType.Properties.DataSource as List<View_Type>).Where(p => p.pkTypeID == product.fkTypeID).FirstOrDefault();
                txtCountOne.Text = product.CountOne.ToString();
                txtBuy.Text = product.PriceBuyOne.ToString();
                txtSell.Text = product.PriceSellOne.ToString();
                CountSell = Convert.ToInt64(product.CountSell);
                CountBuy = Convert.ToInt64(product.CountBuy);
                invoice = product.Invoice == null ? "-1" : product.Invoice;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void ClearProduct()
        {
            cmbNameGroup1.EditValue = null;       
            txtProductName.Text = "";
            txtBarCode.Text = "";
            cmbType.EditValue = null;
            txtCountOne.Text = "";
            txtBuy.Text = "";
            txtSell.Text = "";
            CountSell = 0;
            CountBuy = 0;
        }


        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            ErrorProvider.ClearErrors();

            if (_mod != 3)
            {
                if (string.IsNullOrEmpty(cmbNameGroup1.Text) || cmbNameGroup1.EditValue == null)
                    ErrorProvider.SetError(cmbNameGroup1, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(cmbNameGroup2.Text) || cmbNameGroup2.EditValue == null)
                    ErrorProvider.SetError(cmbNameGroup2, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(txtProductName.Text) || txtProductName.Text == "")
                    ErrorProvider.SetError(txtProductName, "نمیتواند خالی باشد");

                if (string.IsNullOrEmpty(txtBarCode.Text) || txtBarCode.Text == "")
                    ErrorProvider.SetError(txtBarCode, "نمیتواند خالی باشد");

                if (string.IsNullOrEmpty(cmbType.Text) || cmbType.EditValue == null)
                    ErrorProvider.SetError(cmbType, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(txtCountOne.Text) || txtCountOne.Text == "")
                    ErrorProvider.SetError(txtCountOne, "نمیتواند خالی باشد");

                if (string.IsNullOrEmpty(txtBuy.Text) || txtBuy.Text == "")
                    ErrorProvider.SetError(txtBuy, "نمیتواند خالی باشد");

                if (string.IsNullOrEmpty(txtSell.Text) || txtSell.Text == "")
                    ErrorProvider.SetError(txtSell, "نمیتواند خالی باشد");
            }

            if (ErrorProvider.HasErrors)
            {
                return;
            }

            CommonTools.Loading(true);
            BaseRepositoryResponseViewModel res = null;
            BaseRepositoryResponseViewModel res1 = null;

            res = DARepository.ExcuteOperationalSP_New("dbo", "CrudProduct",
               new ServiceOperatorParameter() { Name = "mod", Value = _mod },
               new ServiceOperatorParameter() { Name = "pkProductID", Value = _mod == 1 ? "-1" : this.pkproductid.ToString() },
               new ServiceOperatorParameter() { Name = "parentProductID", Value = (cmbNameGroup2.EditValue as View_Product) == null ? -1 : (cmbNameGroup2.EditValue as View_Product).pkGroup2 },
               new ServiceOperatorParameter() { Name = "BarCode", Value = string.IsNullOrEmpty(txtBarCode.Text) ? "" : txtBarCode.Text },
               new ServiceOperatorParameter() { Name = "ProductName", Value = string.IsNullOrEmpty(txtProductName.Text) ? "" : txtProductName.Text },
               new ServiceOperatorParameter() { Name = "fkTypeID", Value = (cmbType.EditValue as View_Type) == null ? -1 : (cmbType.EditValue as View_Type).pkTypeID },
               new ServiceOperatorParameter() { Name = "CountOne", Value = string.IsNullOrEmpty(txtCountOne.Text) ? 0 : Convert.ToInt16(txtCountOne.Text) });

            res1 = DARepository.ExcuteOperationalSP_New("dbo", "CrudPrice",
               new ServiceOperatorParameter() { Name = "mod", Value = _mod },
               new ServiceOperatorParameter() { Name = "pkPriceID", Value = _mod == 1 ? "-1" : this.pkPriceID.ToString() },
               new ServiceOperatorParameter() { Name = "fkProductID", Value = _mod == 1 ? res.Result.ToString() : this.pkproductid.ToString() },
               new ServiceOperatorParameter() { Name = "PriceSell", Value = string.IsNullOrEmpty(txtSell.Text) ? "" : txtSell.Text },
               new ServiceOperatorParameter() { Name = "PriceBuy", Value = string.IsNullOrEmpty(txtBuy.Text) ? "" : txtBuy.Text },
               new ServiceOperatorParameter() { Name = "Invoice", Value = invoice },
               new ServiceOperatorParameter() { Name = "CountSell", Value = CountSell },
               new ServiceOperatorParameter() { Name = "CountBuy", Value = CountBuy });

            CommonTools.Loading();

            if (CommonTools.ShowMessage(res))
            {
                this._isSave = true;
                FillDataProduct();
                ClearProduct();
                this.Close();
            }
            else
            {
                this._isSave = false;
                this.hasError = true;
            }
            return;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            ClearProduct();
            this.Close();
        }

        private void cmbNameGroup1_EditValueChanged(object sender, EventArgs e)
        {
            var item = cmbNameGroup1.EditValue as View_Product;
            if (item != null)
            {
                var select = "SELECT DISTINCT(NameGroup2),pkGroup2,ParentGroup2 FROM dbo.View_Product";
                var where = "WHERE ParentGroup2 = " + item.pkGroup1;
                cmbNameGroup2.Properties.DataSource = DARepository.GetAllFromView<View_Product>(select, where).ToList();
                cmbNameGroup2.EditValue = null;
            }
        }
    }
}