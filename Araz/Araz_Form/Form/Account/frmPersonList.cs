using Araz_ViewModel;
using DevExpress.CodeParser;
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
using System.Windows;
using System.Windows.Forms;
using Utilities;
using ViewModel.ViewModels;
using static Utilities.Enums;




namespace Araz_Form
{
    public partial class frmPersonList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Int64 pkpersonId = -1;
        Int64 pkroleId = -1;
        Int64 parentrole = -1;
        public bool _isSave = false;
        public bool hasError = false;
        int _mod = -1;
        View_Role roles = new View_Role();
        private List<string> _Sex;
        View_Person _Person = new View_Person();
        List<View_Role> _Role = new List<View_Role>();
        List<View_City> _City = new List<View_City>();

        public frmPersonList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            LoadSex();
            FillData();
            CommonTools.Loading();
        }

        public void FillData()
        {
            cmbRole.Properties.DataSource = DARepository.GetAllFromView<View_Role>("SELECT * FROM dbo.View_Role", "").ToList();
            cmbRole.EditValue = (cmbRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == 1).FirstOrDefault();
        }
        public void FillData_Person()
        {
            cmbRolePerson.Properties.DataSource = DARepository.GetAllFromView<View_Role>("SELECT * FROM dbo.View_Role", "").ToList();
            cmbProvince.Properties.DataSource = DARepository.GetAllFromView<View_City>("SELECT DISTINCT(ProvinceName) , ProvinceID FROM dbo.View_City", "WHERE ParentProvinceID IS NULL").ToList();
            cmbEducation.Properties.DataSource = DARepository.GetAllFromView<View_Education>("SELECT * FROM dbo.View_Education", "").ToList();
            cmbCity.EditValue = null;
            cmbSex.Properties.DataSource = _Sex.ToList();
        }

        private void LoadSex()
        {
            _Sex = new List<string>
        {
            "آقا", "خانم"
        };
        }
        private bool IsPersianLetter(char c)
        {
            return (c >= '\u0600' && c <= '\u06FF') || c == ' ';
        }

        #region FillMod Person 
        public bool modOne()
        {
            try
            {
                CommonTools.Loading(true);
                FillData_Person();
                _mod = 1;
                fpPersonDefine.OwnerControl = gcPersonList;
                fpPersonDefine.ShowBeakForm(Control.MousePosition);
                lcgPersonDefine.Text = "ثبت شخص جدید";
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool modTwo(View_Person model)
        {
            try
            {
                CommonTools.Loading(true);
                FillData_Person();
                _mod = 2;
                _Person = model;
                fpPersonDefine.OwnerControl = gcPersonList;
                fpPersonDefine.ShowBeakForm(Control.MousePosition);
                lcgPersonDefine.Text = "ویرایش شخص جدید";
                this.pkpersonId = _Person.pkPersonID;
                cmbRolePerson.EditValue = (cmbRolePerson.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == _Person.fkRoleID).FirstOrDefault();
                txtName.Text = _Person.PersonName;
                txtLastName.Text = _Person.PersonLastName;
                cmbSex.EditValue = _Sex.Where(p => p == _Person.Sex).FirstOrDefault();
                cmbSex.EditValue = _Person.Sex == null ? "" : _Sex.Where(p => p == _Person.Sex.ToString()).FirstOrDefault();
                txtAge.Text = _Person.PersonAge.ToString();
                txtAgeDate.Text = (_Person.AgeDate == null) ? "" : _Person.AgeDate.ToString();
                cmbEducation.EditValue = (cmbEducation.Properties.DataSource as List<View_Education>).Where(p => p.pkEducationID == _Person.fkEducationID).FirstOrDefault();
                txtNationalCode.Text = _Person.NationalCode;
                txtMobile.Text = _Person.Mobile;
                txtTel.Text = _Person.Tel;
                txtPostalCode.Text = _Person.PostalCode;
                txtEmail.Text = _Person.Email;
                cmbProvince.EditValue = (cmbProvince.Properties.DataSource as List<View_City>).Where(p => p.ProvinceID == _Person.fkProvinceID).FirstOrDefault();
                cmbCity.EditValue = (cmbProvince.EditValue == null) ? null : (cmbCity.Properties.DataSource as List<View_City>).Where(p => p.CityID == _Person.fkCityID).FirstOrDefault();
                txtAddress.Text = _Person.Address;
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region button ribbon
        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            modOne();
            if (_isSave)
                btnRefresh_Click(null, null);
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gvPersonList.GetFocusedRow() as View_Person;
            if (item != null)
            {
                modTwo(item);
                if (_isSave)
                    btnRefresh_Click(null, null);
            }
            else
                CommonTools.ShowMessage("ردیفی برای ویرایش انتخاب نشده ");
        }

        private void btnExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            this.Close();
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = gvPersonList.GetFocusedRow() as View_Person;
            if (item != null)
            {
                if (CommonTools.AskQuestion($" آیا از حذف {item.Sex} {item.FullName} مطمئن هستید؟ "))
                {
                    pkpersonId = item.pkPersonID;
                    _mod = 3;
                    btnSave_Click(null, null);
                    btnRefresh_Click(null, null);
                }
            }
            else
                CommonTools.ShowMessage("ردیفی برای حذف انتخاب نشده ");

        }
        #endregion

        private void gvPersonList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var _Roles = cmbRole.EditValue as View_Role;
            if (_Roles != null)
            {
                var where = "";
                var select = "SELECT * FROM dbo.View_Person";
                if (_Roles.pkRoleID > 3)
                    where = "WHERE fkRoleID = " + _Roles.pkRoleID;
                else if (_Roles.pkRoleID == 2)
                    where = "WHERE fkRoleID <>  " + 4;
                gcPersonList.DataSource = DARepository.GetAllFromView<View_Person>(select, where).ToList();
            }
        }
        public void ClearPerson()
        {
            txtName.Text = "";
            txtLastName.Text = "";
            cmbSex.Text = "";
            cmbSex.EditValue = null;
            txtAge.Text = "";
            txtAgeDate.Text = "";
            cmbEducation.EditValue = null;
            txtNationalCode.Text = "";
            txtMobile.Text = "";
            txtTel.Text = "";
            txtPostalCode.Text = "";
            txtEmail.Text = "";
            cmbProvince.EditValue = null;
            cmbCity.EditValue = null;
            txtAddress.Text = "";
        }

        #region Button Grid Person

        private void btnPrintGridList_Click(object sender, EventArgs e)
        {

        }

        private void btnGridDelete_Click(object sender, EventArgs e)
        {
            btnDelete_ItemClick(null, null);
        }

        private void btnGridEdit_Click(object sender, EventArgs e)
        {
            btnEdit_ItemClick(null, null);
        }

        #endregion

        #region Code Flayout Role
        private void cmbRole_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            var item = cmbRole.EditValue as View_Role;
            if (e.Button.Kind == ButtonPredefines.Plus)
            {
                FillDataRole();
                modOneRole();                
                if (this._isSave)
                    FillData();
            }
            else if (e.Button.Kind == ButtonPredefines.Glyph)
            {
                if (item != null)
                {
                    if (item.pkRoleID > 2)
                    {
                        FillDataRole();
                        modTwoRole(item);
                        if (this._isSave)
                            FillData();                        
                    }
                    else
                        CommonTools.ShowMessage("این سمت رو نمی توانید ویرایش کنید  ");
                   
                }
            }
            if (e.Button.Kind == ButtonPredefines.Delete)
            {
                if (item.pkRoleID > 2)
                {
                    if (roles != null)
                    {
                        if (CommonTools.AskQuestion($"  آیا از حذف سمت {item.RoleName} مطمئن هستید؟ "))
                        {
                            this.pkroleId = item.pkRoleID;
                            _mod = 3;
                            btnSaveRole_Click(null, null);
                        }
                        if (this._isSave)
                            FillData();
                    }
                    else
                        CommonTools.ShowMessage("لطفا یک سمت رو انتخاب کنید ");
                }

                else
                    CommonTools.ShowMessage("این سمت رو نمی توانید حذف کنید  ");
            }
            
          

        }

        public void FillDataRole()
        {
            cmbPersonRole.Properties.DataSource = DARepository.GetAllFromView<View_Role>("select * from dbo.View_Role", "").ToList();

        }

        public bool modOneRole()
        {
            try
            {
                CommonTools.Loading(true);
                _mod = 1;
                
                fpRoleDefine.OwnerControl = gcPersonList;
                fpRoleDefine.ShowBeakForm(Control.MousePosition);
                this.Text = "ثبت شخص جدید";
                cmbPersonRole.EditValue = (cmbPersonRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == 1).FirstOrDefault();
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool modTwoRole(View_Role model)
        {
            try
            {
                CommonTools.Loading(true);
                _mod = 2;
                var _Role = model;
                fpRoleDefine.OwnerControl = gcPersonList;
                fpRoleDefine.ShowBeakForm(Control.MousePosition);
                this.Text = "ویرایش شخص جدید";
                this.pkroleId = _Role.pkRoleID;                
                if (_Role.ParentRole != null && _Role.ParentRole >0 )
                    cmbPersonRole.EditValue = (cmbPersonRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == _Role.ParentRole).FirstOrDefault();
                else
                    cmbPersonRole.EditValue = (cmbPersonRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == 1).FirstOrDefault();
                txtRole.Text = _Role.RoleName;
               
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnSaveRole_Click(object sender, EventArgs e)
        {
            ErrorProvider.ClearErrors();

            if (_mod != 3)
            {
                if (string.IsNullOrEmpty(cmbPersonRole.Text) || cmbPersonRole.EditValue == null)
                    ErrorProvider.SetError(cmbPersonRole, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(txtRole.Text) || txtRole.Text == "")
                    ErrorProvider.SetError(txtRole, "نمیتواند خالی باشد");
            }

            if (ErrorProvider.HasErrors)
            {
                return;
            }

            CommonTools.Loading(true);
            BaseRepositoryResponseViewModel res = null;

            res = DARepository.ExcuteOperationalSP_New("dbo", "CrudRole",
             new ServiceOperatorParameter() { Name = "mod", Value = _mod },
             new ServiceOperatorParameter() { Name = "pkRoleID", Value = _mod == 1 ? "-1" : this.pkroleId.ToString() },
             new ServiceOperatorParameter() { Name = "ParentRole", Value =( parentrole == 0) ? parentrole : (cmbPersonRole.EditValue as View_Role).ParentRole },
            new ServiceOperatorParameter() { Name = "RoleName", Value = string.IsNullOrEmpty(txtRole.Text) ? "" : txtRole.Text });

            CommonTools.Loading();

            if (CommonTools.ShowMessage(res))
            {
                this._isSave = true;
                FillData();
                fpRoleDefine.HideBeakForm();
            }
            else
            {
                this._isSave = false;
                this.hasError = true;
            }
            return;
        }

        private void cmbPersonRole_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPersonRole.EditValue != "-----تمام سمت ها-----")
                parentrole = (cmbPersonRole.Properties.DataSource as List<View_Role>).FirstOrDefault().pkRoleID;
        }

        private void btnExitRole_Click(object sender, EventArgs e)
        {
            fpRoleDefine.HideBeakForm();
        }
        #endregion

        private void btnExitPerson_Click(object sender, EventArgs e)
        {
            fpPersonDefine.HideBeakForm();

            ClearPerson();
        }
        #region Crud Person
        private void btnSave_Click(object sender, EventArgs e)
        {
            ErrorProvider.ClearErrors();

            if (_mod != 3)
            {
                if (string.IsNullOrEmpty(cmbRolePerson.Text) || cmbRolePerson.EditValue == null)
                    ErrorProvider.SetError(cmbRolePerson, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(txtName.Text) || txtName.Text == "")
                    ErrorProvider.SetError(txtName, "نمیتواند خالی باشد");

                if (string.IsNullOrEmpty(txtLastName.Text) || txtLastName.Text == "")
                    ErrorProvider.SetError(txtLastName, "نمیتواند خالی باشد");


                if (string.IsNullOrEmpty(cmbEducation.Text) || cmbEducation.EditValue == null)
                    ErrorProvider.SetError(cmbEducation, "لطفا یک سمت را انتخاب کنید ");

                if (string.IsNullOrEmpty(cmbProvince.Text) || cmbProvince.EditValue == null)
                    ErrorProvider.SetError(cmbProvince, "لطفا یک سمت را انتخاب کنید ");

                if ((cmbProvince.EditValue as View_City) != null)
                    if ((cmbCity.EditValue as View_City) == null)
                        ErrorProvider.SetError(cmbCity, "نمیتواند خالی باشد");

                if (txtMobile.Text != "")
                    if (txtMobile.Text.Length != 11)
                        ErrorProvider.SetError(txtMobile, " موبایل باید 11 رقمی باشد");

                if (txtNationalCode.Text != "")
                    if (txtNationalCode.Text.Length != 10)
                        ErrorProvider.SetError(txtNationalCode, "کد ملی باید 10 رقمی باشد");

                if (txtPostalCode.Text != "")
                    if (txtPostalCode.Text.Length != 10)
                        ErrorProvider.SetError(txtPostalCode, "کد پستی باید 10 رقمی باشد");
            }

            if (ErrorProvider.HasErrors)
            {
                return;
            }
            CommonTools.Loading(true);
            BaseRepositoryResponseViewModel res = null;

            res = DARepository.ExcuteOperationalSP_New("dbo", "CrudPerson",
             new ServiceOperatorParameter() { Name = "mod", Value = _mod },
             new ServiceOperatorParameter() { Name = "pkPersonID", Value = _mod == 1 ? "-1" : this.pkpersonId.ToString() },
             new ServiceOperatorParameter() { Name = "fkRoleID", Value = (cmbRolePerson.EditValue as View_Role) == null ? -1 : (cmbRolePerson.EditValue as View_Role).pkRoleID },
             new ServiceOperatorParameter() { Name = "PersonName", Value = string.IsNullOrEmpty(txtName.Text) ? "" : txtName.Text },
             new ServiceOperatorParameter() { Name = "PersonLastName", Value = string.IsNullOrEmpty(txtLastName.Text) ? "" : txtLastName.Text },
             new ServiceOperatorParameter() { Name = "Sex", Value = cmbSex.EditValue == null ? "آقا" : cmbSex.EditValue },
             new ServiceOperatorParameter() { Name = "PersonAge", Value = "" },
             new ServiceOperatorParameter() { Name = "AgeDate", Value = "" },
             new ServiceOperatorParameter() { Name = "fkEducationID", Value = (cmbEducation.EditValue as View_Education) == null ? -1 : (cmbEducation.EditValue as View_Education).pkEducationID },
             new ServiceOperatorParameter() { Name = "NationalCode", Value = string.IsNullOrEmpty(txtNationalCode.Text) ? "" : txtNationalCode.Text },
             new ServiceOperatorParameter() { Name = "Mobile", Value = string.IsNullOrEmpty(txtMobile.Text) ? "" : txtMobile.Text },
             new ServiceOperatorParameter() { Name = "Tel", Value = string.IsNullOrEmpty(txtTel.Text) ? "" : txtTel.Text },
             new ServiceOperatorParameter() { Name = "PostalCode", Value = string.IsNullOrEmpty(txtPostalCode.Text) ? "" : txtPostalCode.Text },
             new ServiceOperatorParameter() { Name = "Email", Value = string.IsNullOrEmpty(txtEmail.Text) ? "" : txtEmail.Text },
             new ServiceOperatorParameter() { Name = "fkProvinceID", Value = (cmbProvince.EditValue as View_City) == null ? -1 : (cmbProvince.EditValue as View_City).ProvinceID },
             new ServiceOperatorParameter() { Name = "fkCityID", Value = (cmbCity.EditValue as View_City) == null ? -1 : (cmbCity.EditValue as View_City).CityID },
             new ServiceOperatorParameter() { Name = "Address", Value = string.IsNullOrEmpty(txtAddress.Text) ? "" : txtAddress.Text },
             new ServiceOperatorParameter() { Name = "EditToken", Value = "" }, //Value = _mod == 1 && _Person == null ? "" : _Person.EditToken
                                                                                // new ServiceOperatorParameter() { Name = "InsertUser", Value = Utilities.ControlUnit.LoggedUser.pkUser },
             new ServiceOperatorParameter() { Name = "InsertUser", Value = 14125 },
             new ServiceOperatorParameter() { Name = "InsertIP", Value = "193.168.1.1" });

            CommonTools.Loading();

            if (CommonTools.ShowMessage(res))
            {
                this._isSave = true;
                fpPersonDefine.HideBeakForm();
                btnRefresh_Click(null, null);
                ClearPerson();
            }
            else

            {
                this._isSave = false;
                this.hasError = true;
            }

            return;
        }
        #endregion

        private void cmbProvince_EditValueChanged(object sender, EventArgs e)
        {
            var _province = cmbProvince.EditValue as View_City;
            if (_province != null)
            {
                var select = "SELECT * FROM dbo.View_City";
                var where = "where PerentCityID = " + _province.ProvinceID;
                cmbCity.Properties.DataSource = DARepository.GetAllFromView<View_City>(select, where).ToList();
                cmbCity.EditValue = null;
            }
        }

        private void btnPrintList_ItemClick(object sender, ItemClickEventArgs e)
        {
           
        }
    }
}