using Araz_Form.Form.Account;
using Araz_ViewModel;
using DevExpress.CodeParser;
using DevExpress.Utils.VisualEffects;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
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

namespace Araz_Form.Form.Region
{
    public partial class frmRegionList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Int64 pkcityId = -1;
        //   string parentcity = null;
        public bool _isSave = false;
        public bool hasError = false;
        int _mod = -1;
        View_City _city = new View_City();
        public frmRegionList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }
        public void FillData()
        {

            CommonTools.Loading(true);
            cmbProvince.Properties.DataSource = DARepository.GetAllFromView<View_City>("SELECT DISTINCT(ProvinceName) , ProvinceID FROM dbo.View_City", "WHERE ParentProvinceID IS NULL ORDER BY (ProvinceName)").ToList();
            cmbProvince.EditValue=null;
            cmbflProvince.EditValue = null;
            cmbflProvince.Enabled = false;
            CommonTools.Loading();
        }

        private void gvCityList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var _city = cmbProvince.EditValue as View_City;
            if (_city != null)
            {

                var select = "SELECT * FROM dbo.View_City";
                var where = "where PerentCityID = " + _city.ProvinceID;
                gcCityList.DataSource = DARepository.GetAllFromView<View_City>(select, where).ToList();
            }
        }

        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            modOneCity();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gvCityList.GetFocusedRow() as View_City;
            if (item != null)
            {
                modThreeCity(item);
            }
            else
                CommonTools.ShowMessage("لطفا یک شهر رو انتخاب کنید ");
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gvCityList.GetFocusedRow() as View_City;
            if (item != null)
            {
                modTwoCity(item);
            }
            else
                CommonTools.ShowMessage("لطفا یک شهر رو انتخاب کنید ");
        }

        private void cmbProvince_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var item = cmbProvince.EditValue as View_City;
            if (e.Button.Kind == ButtonPredefines.Plus)
            {


                lbCity.Text = "استان";
                _mod = 1;
                fpCityDefine.OwnerControl = gcCityList;
                fpCityDefine.ShowBeakForm(Control.MousePosition);
                cmbflProvince.EditValue = null;
            }
            else if (item != null)
            {
                if (e.Button.Kind == ButtonPredefines.Glyph)
                {
                    lbCity.Text = "استان";
                    _mod = 2;
                    _city = item;
                    txtCity.Text = _city.ProvinceName;
                    pkcityId = Convert.ToInt64(_city.ProvinceID);
                    fpCityDefine.OwnerControl = gcCityList;
                    fpCityDefine.ShowBeakForm(Control.MousePosition);
                    cmbflProvince.EditValue = null;
                    cmbflProvince.Enabled = false;

                }
                else if (e.Button.Kind == ButtonPredefines.Delete)
                {
                    _city = item;
                    if (CommonTools.AskQuestion($"آیا از حذف استان {_city.ProvinceName} مطمئن هستید ؟"))
                    {
                        _mod = 3;                       
                        pkcityId = Convert.ToInt64(_city.ProvinceID);
                        btnSave_Click(null, null);
                        FillData();
                        cmbflProvince.EditValue = null;
                        cmbflProvince.Enabled = false;
                    }
                }
            }
            else
                CommonTools.ShowMessage("لطفا یک استان رو انتخاب کنید ");

        }
        public void FillMod()
        {
            cmbflProvince.Properties.DataSource = DARepository.GetAllFromView<View_City>("SELECT DISTINCT(ProvinceName) , ProvinceID FROM dbo.View_City", "WHERE ParentProvinceID IS NULL").ToList();
            cmbflProvince.Enabled = true;
        }

        public bool modOneCity()
        {
            try
            {
                CommonTools.Loading(true);
                // _city = model;
                _mod = 1;
                fpCityDefine.OwnerControl = gcCityList;
                fpCityDefine.ShowBeakForm(Control.MousePosition);
                FillMod();
                lcProvince.Text = "ثبت شهر جدید";

                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool modTwoCity(View_City model)
        {
            try
            {
                CommonTools.Loading(true);
                _city = model;
                _mod = 2;
                pkcityId = Convert.ToInt64(_city.CityID);
                fpCityDefine.OwnerControl = gcCityList;
                fpCityDefine.ShowBeakForm(Control.MousePosition);
                FillMod();
                lcProvince.Text = "ویرایش نام شهر ";
                txtCity.Text = _city.CityName;
                cmbflProvince.EditValue = (cmbflProvince.Properties.DataSource as List<View_City>).Where(p => p.ProvinceID == _city.ProvinceID).FirstOrDefault();
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool modThreeCity(View_City model)
        {
            try
            {
                CommonTools.Loading(true);
                _city = model;
                _mod = 3;
                pkcityId = Convert.ToInt64(_city.CityID);
                if (CommonTools.AskQuestion($"آیا از حذف شهر {_city.CityName} مطمئن هستید ؟"))
                {
                    btnSave_Click(null, null);
                }
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Clear()
        {
            cmbflProvince.EditValue = null;

            txtCity.Text = null;
            lbCity.Text = "شهر";

        }

        private void btnflExit_Click(object sender, EventArgs e)
        {
            fpCityDefine.HideBeakForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ErrorProvider.ClearErrors();

            if (_mod != 3)
            {
                if (cmbflProvince.Enabled == true)
                {
                    if (string.IsNullOrEmpty(cmbflProvince.Text) || cmbflProvince.EditValue == null)
                        ErrorProvider.SetError(cmbflProvince, "لطفا یک استان را انتخاب کنید ");
                }
                if (string.IsNullOrEmpty(txtCity.Text) || txtCity.Text == "")
                    ErrorProvider.SetError(txtCity, "نمیتواند خالی باشد");
            }

            if (ErrorProvider.HasErrors)
            {
                return;
            }
            CommonTools.Loading(true);
            BaseRepositoryResponseViewModel res = null;

            res = DARepository.ExcuteOperationalSP_New("dbo", "CrudRegion",
             new ServiceOperatorParameter() { Name = "mod", Value = _mod },
             new ServiceOperatorParameter() { Name = "pkCityID", Value = _mod == 1 ? "-1" : this.pkcityId.ToString() },
             new ServiceOperatorParameter() { Name = "PerentCityID", Value = (cmbflProvince.EditValue as View_City) == null ? -1 : (cmbflProvince.EditValue as View_City).ProvinceID },
             new ServiceOperatorParameter() { Name = "CityName", Value = string.IsNullOrEmpty(txtCity.Text) ? "" : txtCity.Text });

            CommonTools.Loading();

            if (CommonTools.ShowMessage(res))
            {
                this._isSave = true;
                btnflExit_Click(null, null);
                if (lbCity.Text == "شهر")
                    btnRefresh_Click(null, null);
                else
                    FillData();
                Clear();
            }
            else

            {
                this._isSave = false;
                this.hasError = true;
            }

            return;
        }

        private void btnGridDelete_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            btnDelete_ItemClick(null, null);
        }

        private void btnGridEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }
    }
}