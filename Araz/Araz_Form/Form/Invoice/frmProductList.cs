using Araz_Form.Form.Account;
using Araz_ViewModel;
using DevExpress.CodeParser;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting.Native;
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

namespace Araz_Form
{
    public partial class frmProductList : DevExpress.XtraBars.Ribbon.RibbonForm
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

        public frmProductList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }

        public void FillData()
        {

            cmbNameGroup1.Properties.DataSource = DARepository.GetAllFromView<View_Product>("SELECT DISTINCT(NameGroup1),pkGroup1,ParentGroup1 FROM dbo.View_Product ", "").ToList();

        }
        public void FillDataProduct()
        {
            cmbflNameGroup1.Properties.DataSource = DARepository.GetAllFromView<View_Product>("SELECT DISTINCT(NameGroup1),pkGroup1,ParentGroup1 FROM dbo.View_Product ", "").ToList();
            cmbType.Properties.DataSource = DARepository.GetAllFromView<View_Type>("SELECT * FROM dbo.View_Type ", "").ToList();
        }
        public void FillDataGroup()
        {
            
            cmbflGroupName.Properties.DataSource = DARepository.GetAllFromView<View_Product>("SELECT DISTINCT(NameGroup1),pkGroup1,ParentGroup1 FROM dbo.View_Product ", "").ToList();
        }

        public bool ModOne()
        {
            try
            {
                lcfrmProductDefine.Text = "ثبت محصول جدید";
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
                lcfrmProductDefine.Text = "ویرایش محصول جدید";
                product = model;
                pkproductid = product.pkProductID;
                pkPriceID = Convert.ToInt64(product.pkPriceID);
                _mod = 2;
                cmbflNameGroup1.EditValue = (cmbflNameGroup1.Properties.DataSource as List<View_Product>).Where(p => p.pkGroup1 == product.pkGroup1).FirstOrDefault();
                cmbflNameGroup2.EditValue = (cmbflNameGroup2.Properties.DataSource as List<View_Product>).Where(p => p.pkGroup2 == product.pkGroup2).FirstOrDefault();
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

        public bool ModOneGroup1()
        {
            try
            {
                _mod = 1;
                fpGroup.OwnerControl = gcProductList;
                fpGroup.ShowBeakForm(Control.MousePosition);               
                cmbflGroupName.Enabled = false;
                txtGroupLabel.Text = "گروه اصلی";
                lcGroup.Text = "ثبت گروه اصلی جدید";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ModTwoGroup1(View_Product model)
        {
            try
            {
                _mod = 2;
                fpGroup.OwnerControl = gcProductList;
                fpGroup.ShowBeakForm(Control.MousePosition);
                product = model;
                pkproductid = product.pkGroup1;
                txtGroup.Text = product.NameGroup1;
                cmbflGroupName.Enabled = false;
                txtGroupLabel.Text = "گروه اصلی";
                lcGroup.Text = "ویرایش گروه اصلی ";
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool ModOneGroup2()
        {
            try
            {
                _mod = 1;
                fpGroup.OwnerControl = gcProductList;
                fpGroup.ShowBeakForm(Control.MousePosition);
                //cmbflGroupName.Properties.DataSource = DARepository.GetAllFromView<View_Product>("SELECT DISTINCT(NameGroup1),pkGroup1,ParentGroup1 FROM dbo.View_Product ", "").ToList();
                txtGroupLabel.Text = "گروه فرعی";
                lcGroup.Text = "ثبت گروه فرعی جدید";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ModTwoGroup2(View_Product model ,View_Product model1)
        {
            try
            {
                _mod = 2;
                fpGroup.OwnerControl = gcProductList;
                fpGroup.ShowBeakForm(Control.MousePosition);
                product = model;
                cmbflGroupName.EditValue = (cmbflGroupName.Properties.DataSource as List<View_Product>).Where(p => p.pkGroup1 == product.pkGroup1).FirstOrDefault();
                pkproductid = model1.pkGroup2;
                txtGroup.Text = model1.NameGroup2;               
                txtGroupLabel.Text = "گروه فرعی";
                lcGroup.Text = "ثبت گروه فرعی جدید";
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            fpProductDefine.OwnerControl = gcProductList;
            fpProductDefine.ShowBeakForm(Control.MousePosition);
            FillDataProduct();
            ModOne();
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var item = cmbNameGroup1.EditValue as View_Product;
            var item2 = cmbNameGroup2.EditValue as View_Product;
            if (item == null || item2 == null || cmbNameGroup2.Text == "" || item.pkGroup1 == 1)
            {
                select = "SELECT DISTINCT(ProductName),* FROM dbo.View_Product";
                where = "WHERE parentProductID > (SELECT MAX(pkProductID) FROM dbo.Product WHERE parentProductID IS NULL )";
            }
            else if (item != null && item2 != null)
            {
                select = "SELECT DISTINCT(ProductName),* FROM dbo.View_Product";
                where = "WHERE ParentProductID = " + item2.pkGroup2;
            }
            gcProductList.DataSource = DARepository.GetAllFromView<View_Product>(select, where).ToList();
            cmbNameGroup1.EditValue = null;
            cmbNameGroup2.EditValue = null;
        }
        public void ClearProduct()
        {
            cmbflNameGroup1.EditValue = null;
            cmbflNameGroup2.EditValue = null;
            txtProductName.Text = "";
            txtBarCode.Text = "";
            cmbType.EditValue = null;
            txtCountOne.Text = "";
            txtBuy.Text = "";
            txtSell.Text = "";
            CountSell = 0;
            CountBuy = 0;
        }
        public void ClearGroup()
        {
            txtGroup.Text = "";
            cmbflGroupName.EditValue = null;
            cmbflNameGroup2.EditValue = null;
            cmbflGroupName.Enabled = true;
        }

        private void gvProductList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void cmbflNameGroup1_EditValueChanged(object sender, EventArgs e)
        {
            var item = cmbflNameGroup1.EditValue as View_Product;
            if (item != null)
            {
                var select = "SELECT DISTINCT(NameGroup2),pkGroup2,ParentGroup2 FROM dbo.View_Product";
                var where = "WHERE ParentGroup2 = " + item.pkGroup1;
                cmbflNameGroup2.Properties.DataSource = DARepository.GetAllFromView<View_Product>(select, where).ToList();
                cmbflNameGroup2.EditValue = null;
            }
        }

        private void btnFlExit_Click(object sender, EventArgs e)
        {
            ClearProduct();
            fpProductDefine.HideBeakForm();
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gvProductList.GetFocusedRow() as View_Product;
            if (item != null)
            {
                FillDataProduct();
                ModTwo(item);
                fpProductDefine.OwnerControl = gcProductList;
                fpProductDefine.ShowBeakForm(Control.MousePosition);

            }
            else
                CommonTools.ShowMessage("ردیفی برای ویرایش انتخاب نشده");
        }

        private void btnGridEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        private void btnGridPrint_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void btnGridDelete_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            btnDelete_ItemClick(null, null);
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            ErrorProvider.ClearErrors();

            if (_mod != 3)
            {
                if (string.IsNullOrEmpty(cmbflNameGroup1.Text) || cmbflNameGroup1.EditValue == null)
                    ErrorProvider.SetError(cmbflNameGroup1, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(cmbflNameGroup2.Text) || cmbflNameGroup2.EditValue == null)
                    ErrorProvider.SetError(cmbflNameGroup2, "لطفا یک سمت را انتخاب کنید ");

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
               new ServiceOperatorParameter() { Name = "parentProductID", Value = (cmbflNameGroup2.EditValue as View_Product) == null ? -1 : (cmbflNameGroup2.EditValue as View_Product).pkGroup2 },
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
                FillData();
                btnRefresh_Click(null, null);
                fpProductDefine.HideBeakForm();
            }
            else
            {
                this._isSave = false;
                this.hasError = true;
            }
            return;
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void cmbNameGroup1_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ClearGroup();
            var item = cmbNameGroup1.EditValue as View_Product;
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                ModOneGroup1();
                if (this._isSave)
                    FillData();
            }
           
           else if (item != null)
            {
                if (e.Button.Kind == ButtonPredefines.Glyph)
                {
                    ModTwoGroup1(item);
                    if (this._isSave)
                        FillData();
                }
                if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    if (CommonTools.AskQuestion($" آیا از حذف {item.NameGroup1} مطمئن هستید؟ "))
                    {
                        this.pkproductid = item.pkGroup1;
                        _mod = 3;
                        btnSaveGroup_Click(null, null);
                    }
                    if (this._isSave)
                        FillData();
                }
            }
            else
                CommonTools.ShowMessage("لطفا یک گروه اصلی رو انتخاب کنید ");            
        }

            private void btnExitGroup_Click(object sender, EventArgs e)
            {
                fpGroup.HideBeakForm();
                ClearGroup();
            }

            private void btnSaveGroup_Click(object sender, EventArgs e)
            {
                if (_mod != 3)
                {
                    if (string.IsNullOrEmpty(txtGroup.Text) || txtGroup.Text == "")
                        ErrorProvider.SetError(txtGroup, "نمیتواند خالی باشد");
                }

                if (ErrorProvider.HasErrors)
                {
                    return;
                }

                CommonTools.Loading(true);
                BaseRepositoryResponseViewModel res = null;

                res = DARepository.ExcuteOperationalSP_New("dbo", "CrudProductGroup",
                   new ServiceOperatorParameter() { Name = "mod", Value = _mod },
                   new ServiceOperatorParameter() { Name = "pkProductID", Value = _mod == 1 ? "-1" : this.pkproductid.ToString() },
                   new ServiceOperatorParameter() { Name = "parentProductID", Value = (cmbflGroupName.EditValue as View_Product) == null ? -1 : (cmbflGroupName.EditValue as View_Product).pkGroup2 },
                   new ServiceOperatorParameter() { Name = "BarCode", Value = -1 },
                   new ServiceOperatorParameter() { Name = "ProductName", Value = string.IsNullOrEmpty(txtGroup.Text) ? "" : txtGroup.Text },
                   new ServiceOperatorParameter() { Name = "fkTypeID", Value = -1 },
                   new ServiceOperatorParameter() { Name = "CountOne", Value = -1 });

                CommonTools.Loading();

                if (CommonTools.ShowMessage(res))
                {
                    this._isSave = true;
                    FillData();
                    ClearGroup();
                    btnRefresh_Click(null, null);
                    fpGroup.HideBeakForm();
                }
                else
                {
                    this._isSave = false;
                    this.hasError = true;
                }
                return;
            }

        private void cmbNameGroup2_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            View_Product item2 =new View_Product();
            var item = cmbNameGroup1.EditValue as View_Product;
            if (cmbNameGroup2 != null) 
             item2 = cmbNameGroup2.EditValue as View_Product;
            else 
            item2=null;

            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                ClearGroup();
                FillDataGroup();
                ModOneGroup2();
                if (this._isSave)
                    FillData();
            }            
           else if ( item != null && item2 != null  )
            {
                if (e.Button.Kind == ButtonPredefines.Glyph)
                {
                    ClearGroup();
                    FillDataGroup();
                    ModTwoGroup2(item , item2);
                    if (this._isSave)
                        FillData();
                }
               if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    if (CommonTools.AskQuestion($" آیا از حذف {item2.NameGroup2} مطمئن هستید؟ "))
                    {
                        this.pkproductid = item2.pkGroup2;
                        _mod = 3;
                        btnSaveGroup_Click(null, null);
                    }
                    if (this._isSave)
                        FillData();
                }
            }
            else
                CommonTools.ShowMessage("لطفا یک گروه فرعی رو انتخاب کنید ");
        }
    }
}