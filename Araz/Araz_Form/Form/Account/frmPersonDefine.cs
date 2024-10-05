using Araz_ViewModel;
using Araz_ViewModel.Region;
using DevExpress.DashboardWin.Bars;
using DevExpress.XtraEditors;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using ViewModel.ViewModels;

namespace Araz_Form.Form
{
    public partial class frmPersonDefine : DevExpress.XtraEditors.XtraForm
    {
        public bool _isSave = false;
        public bool error = false;
        int _mod = -1;
        private List<string> _Sex;
        View_Person _Person = new View_Person();
        List<View_Role> _Role = new List<View_Role>();
        List<View_City> _City = new List<View_City>();
        List<View_Province> _Country = new List<View_Province>();
        public frmPersonDefine(int mod, View_Person model)
        {
            CommonTools.Loading(true);
            _mod = mod;
            _Person = model;
            InitializeComponent();
            LoadSex();
            FillData();         
            if (_mod == 1)
            {
                if (!modOne())
                {
                    this.error = true;
                }
            }
            if (_mod == 2)
            {
                if (!modTwo())
                {
                    this.error = true;
                    this.Close();
                }
            }
            else if (mod == 3)
                btnSave_Click(null, null);
            else
            {
                this.error = true;
                this.Close();
            }

            CommonTools.Loading();
        }
        private void FillData()
        {
            cmbRole.Properties.DataSource = DARepository.GetAllFromView<View_Role>("SELECT * FROM dbo.View_Role", "").ToList();
            cmbProvince.Properties.DataSource = DARepository.GetAllFromView<View_Province>("SELECT * FROM dbo.View_Province", "").ToList();
            cmbEducation.Properties.DataSource = DARepository.GetAllFromView<View_Education>("SELECT * FROM dbo.View_Education", "").ToList();
            cmbCity.EditValue = null;
            cmbSex.Properties.DataSource = _Sex.ToList();
        }
        private void LoadSex()
        {
            _Sex = new List<string>
        {
            "خانم", "آقا"
        };
        }

        public bool modOne()
        {
            try
            {
                this.Text = "ثبت اشخاص جدید";
                return true;
            }
            catch
            {

                return false;
            }
        }
        public bool modTwo()
        {
            try
            {
                this.Text = "ویرایش شخص ";
                cmbRole.EditValue = (cmbRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == _Person.fkRoleID).FirstOrDefault();
                txtName.Text = _Person.PersonName;
                cmbSex.Text = _Person.PersonLastName;
                cmbSex.EditValue = _Sex.Where(p => p == _Person.Sex.ToString()).FirstOrDefault();
                txtAge.Text = _Person.PersonAge.ToString();
                //dtpAgeDate.GeorgianDate = _Person.AgeDate;
                cmbEducation.EditValue = (cmbEducation.Properties.DataSource as List<View_Education>).Where(p => p.pkEducationID == _Person.fkEducationID).FirstOrDefault();
                txtNationalCode.Text = _Person.NationalCode;
                layoutControlItem2.Text = _Person.Mobile;
                txtTel.Text = _Person.Tel;
                txtPostalCode.Text = _Person.PostalCode;
                txtEmail.Text = _Person.Email;               
                cmbProvince.EditValue = (cmbProvince.Properties.DataSource as List<View_Province>).Where(p => p.pkCityID == _Person.fkProvinceID).FirstOrDefault();
                cmbCity.EditValue = (cmbCity.Properties.DataSource as List<View_City>).Where(p => p.pkCityID == _Person.fkCityID).FirstOrDefault();
                txtAddress.Text = _Person.Address;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void Clear()
        {
            cmbRole.EditValue = null;
            txtName.Text = "";
            cmbSex.Text = "";
            cmbSex.EditValue = null;
            txtAge.Text = "";
            cmbEducation.EditValue = null;
            txtNationalCode.Text = "";
            layoutControlItem2.Text = "";
            txtTel.Text = "";
            txtPostalCode.Text = "";
            txtEmail.Text = "";
            cmbProvince.EditValue = null;
            cmbCity.EditValue = null;
            txtAddress.Text = "";
        }


        public bool PersonSave()
        {
            ErrorProvider.ClearErrors();

            if (string.IsNullOrEmpty(cmbRole.Text) || cmbRole.EditValue == null)
                ErrorProvider.SetError(cmbRole, "لطفا یک سمت را انتخاب کنید ");

            if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
                ErrorProvider.SetError(txtName, "نمیتواند خالی باشد");

            if (string.IsNullOrEmpty(cmbSex.Text) || cmbSex.Text == "")
                ErrorProvider.SetError(cmbSex, "نمیتواند خالی باشد");

            if (txtMobile.Text != "")
                if (txtMobile.Text.Length != 11)
                    ErrorProvider.SetError(txtMobile, " موبایل باید 11 رقمی باشد");

            if (txtNationalCode.Text != "")
                if (txtNationalCode.Text.Length != 10)
                    ErrorProvider.SetError(txtNationalCode, "کد ملی باید 10 رقمی باشد");

            if (txtPostalCode.Text != "")
                if (txtPostalCode.Text.Length != 10)
                    ErrorProvider.SetError(txtPostalCode, "کد پستی باید 10 رقمی باشد");


            if (ErrorProvider.HasErrors)
            {
                return false;
            }
            CommonTools.Loading(true);
            BaseRepositoryResponseViewModel res = null;

            res = DARepository.ExcuteOperationalSP_New("dbo", "CrudPerson",
             new ServiceOperatorParameter() { Name = "mod", Value = _mod },
             new ServiceOperatorParameter() { Name = "pkPersonID", Value = _mod == 1 || _Person.pkPersonID == null ? "-1" : _Person.pkPersonID.ToString() },
             new ServiceOperatorParameter() { Name = "fkRoleID", Value = (cmbRole.EditValue as View_Role) == null ? -1 : (cmbRole.EditValue as View_Role).pkRoleID },
             new ServiceOperatorParameter() { Name = "PersonName", Value = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text },
             new ServiceOperatorParameter() { Name = "PersonLastName", Value = string.IsNullOrEmpty(cmbSex.Text) ? "" : cmbSex.Text },
             new ServiceOperatorParameter() { Name = "Sex", Value = cmbSex.EditValue == null ? "" : cmbSex.EditValue.ToString() },
            //  new ServiceOperatorParameter() { Name = "LoadingDate", Value = dtpLoadingDate.GeorgianDate == null ? "" : dtpLoadingDate.GeorgianDate.Value.ToString("yyyy-MM-dd") },
             new ServiceOperatorParameter() { Name = "PersonAge", Value = string.IsNullOrEmpty(txtAge.Text) ? (short?)null : Convert.ToInt16(txtAge.Text) },
             new ServiceOperatorParameter() { Name = "fkEducationID", Value = (cmbEducation.EditValue as View_Education) == null ? -1 : (cmbEducation.EditValue as View_Education).pkEducationID },
             new ServiceOperatorParameter() { Name = "NationalCode", Value = string.IsNullOrEmpty(txtNationalCode.Text) ? "" : txtNationalCode.Text },
             new ServiceOperatorParameter() { Name = "Mobile", Value = string.IsNullOrEmpty(layoutControlItem2.Text) ? "" : layoutControlItem2.Text },
             new ServiceOperatorParameter() { Name = "Tel", Value = string.IsNullOrEmpty(txtTel.Text) ? "" : txtTel.Text },
             new ServiceOperatorParameter() { Name = "PostalCode", Value = string.IsNullOrEmpty(txtPostalCode.Text) ? "" : txtPostalCode.Text },
             new ServiceOperatorParameter() { Name = "Email", Value = string.IsNullOrEmpty(txtEmail.Text) ? "" : txtEmail.Text },
             new ServiceOperatorParameter() { Name = "fkProvinceID", Value = (cmbProvince.EditValue as View_Province) == null ? -1 : (cmbProvince.EditValue as View_Province).pkCityID },
             new ServiceOperatorParameter() { Name = "fkCityID", Value = (cmbCity.EditValue as View_City) == null ? -1 : (cmbCity.EditValue as View_City).pkCityID },
             new ServiceOperatorParameter() { Name = "Address", Value = string.IsNullOrEmpty(txtAddress.Text) ? "" : txtAddress.Text },
             new ServiceOperatorParameter() { Name = "EditToken", Value = _mod == 1 && _Person == null ? "" : _Person.EditToken },
             // new ServiceOperatorParameter() { Name = "InsertUser", Value = Utilities.ControlUnit.LoggedUser.pkUser },
             new ServiceOperatorParameter() { Name = "InsertUser", Value = 14125 },
             new ServiceOperatorParameter() { Name = "InsertIP", Value = "193.168.1.1" });

            CommonTools.Loading();

            if (CommonTools.ShowMessage(res))
            {
                this._isSave = true;
                this.Close();
            }
            else
            {
                this._isSave = false;
            }

            return false;
        }
   

        private void btnSave_Click(object sender, EventArgs e)
        {
            PersonSave();
        }

        private void cmbProvince_EditValueChanged(object sender, EventArgs e)
        {
            var _Country = cmbProvince.EditValue as View_Province;
            if (_Country != null)
                cmbCity.Properties.DataSource = DARepository.GetAllFromView<View_City>("SELECT * FROM dbo.View_City", "where PerentCityID = " + _Country.pkCityID).ToList();
            else 
                cmbCity.EditValue=null;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Clear();
            this.Close();
        }
    }
}