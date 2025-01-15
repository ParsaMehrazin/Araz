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

namespace ArazWin
{
    public partial class frmLoginAraz : DevExpress.XtraEditors.XtraForm
    {
        long pkPerson =0;
        public frmLoginAraz()
        {

            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading(false);
        }

        public void FillData()
        {

            cmbFinnantialYear.Properties.DataSource = DARepository.GetAllFromView<View_FinnantialYear>("SELECT * FROM View_FinnantialYear", "").ToList();

        }

        private void gridLookUpEdit1View_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void cmbFinnantialYear_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbFinnantialYear != null)
            {                
                cmbRole.Properties.DataSource = DARepository.GetAllFromView<View_Role>("SELECT * FROM dbo.View_Role", "Where pkRoleID > 2").ToList();
            }
            else
            {
                CommonTools.ShowMessage("");
            }


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var person = cmbPerson.EditValue as View_Person;

            if ((cmbFinnantialYear.EditValue as View_FinnantialYear).FinnantialYear <= DateTime.Now.ToPersianYear())
            {
                if (person != null && txtPassword.Text != "")
                {
                    bool ispass = IdentityHelper.VerifyHashedPassword(person.Password, txtPassword.Text);
                    if (ispass)
                    {
                        this.Hide();
                        pkPerson = person.pkPersonID;
                        MainForm frm = new MainForm(pkPerson);
                        
                        frm.Show();
                    }
                    else
                    {
                        CommonTools.ShowMessage("نام کاربری یا رمز عبور اشتباه است");
                    }
                }
                else
                {
                    CommonTools.ShowMessage("نام کاربری یا رمز عبور اشتباه است");
                }
            }
            else
            {
                CommonTools.ShowMessage("سال مالی اشتباه است");
            }
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void cmbRole_EditValueChanged(object sender, EventArgs e)
        {
            var role = cmbRole.EditValue as View_Role;
            if (cmbRole != null)
            {
                cmbPerson.Properties.DataSource = DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person", "WHERE fkRoleID ="+ role.pkRoleID).ToList();
            }
            else
            {
                CommonTools.ShowMessage("");
            }
        }
    }
}