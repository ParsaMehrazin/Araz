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
using System.Windows;
using System.Xml.Linq;
using Utilities;
using ViewModel.ViewModels;
using static DevExpress.Data.Helpers.SyncHelper.ZombieContextsDetector;

namespace Araz_Form.Form
{
    public partial class frmRoleDefine : DevExpress.XtraEditors.XtraForm
    {
        Int64 pkroleId = -1;
        Int64 parentrole = -1;
        public bool _isSave = false;
        public bool hasError = false;
        int _mod = -1;
        View_Role roles = new View_Role();
        List<View_Role> _Role = new List<View_Role>();

        public frmRoleDefine(int mod , View_Role model)
        {
            CommonTools.Loading(true);
            InitializeComponent();
            _mod = mod;
            roles = model;
            FillData();
            if (_mod == 1)
            {
                if (!modOne())
                {
                    this.hasError = true;
                    this.Close();
                }
            }
            else if (_mod == 2)
            {
                if (!modTwo())
                {
                    this.hasError = true;
                    this.Close();
                }
            }
            else if (mod == 3)
            {
                if (CommonTools.AskQuestion($" آیا از حذف {roles.RoleName} مطمئن هستید؟ "))
                {
                    this.pkroleId = roles.pkRoleID;
                    btnSave_Click(null, null);
                }
            }
            else
            {
                this.Close();
            }
            CommonTools.Loading();
        }

        public void FillData()
        {
            cmbPersonRole.Properties.DataSource = DARepository.GetAllFromView<View_Role>("select * from dbo.View_Role","Where pkRoleId <> 2").ToList();
           
        }

        public bool modOne()
        {
            try
            {
                CommonTools.Loading(true); 
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

        public bool modTwo()
        {
            try
            {
                CommonTools.Loading(true);     
                this.Text = "ویرایش شخص جدید";
                this.pkroleId = roles.pkRoleID;
                if ( roles != null && roles.ParentRole!=0)
                    cmbPersonRole.EditValue = (cmbPersonRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == roles.ParentRole).FirstOrDefault();
               else
                    cmbPersonRole.EditValue = (cmbPersonRole.Properties.DataSource as List<View_Role>).Where(p => p.pkRoleID == 1).FirstOrDefault();
                txtRole.Text = roles.RoleName;                
                CommonTools.Loading();
                return true;
            }
            catch
            {
                return false;
            }
        } 

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
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
             new ServiceOperatorParameter() { Name = "ParentRole", Value = parentrole },
            new ServiceOperatorParameter() { Name = "RoleName", Value = string.IsNullOrEmpty(txtRole.Text) ? "" : txtRole.Text });

            CommonTools.Loading();

            if (CommonTools.ShowMessage(res))
            {
                this._isSave = true;
                this.Close();
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
            if (cmbPersonRole.EditValue.ToString() != "-----تمام سمت ها-----")
                parentrole = (cmbPersonRole.Properties.DataSource as List<View_Role>).FirstOrDefault().pkRoleID;
        }
    }
}