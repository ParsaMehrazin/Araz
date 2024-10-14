using Araz_ViewModel;
using DevExpress.DashboardWin.Bars;
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

namespace Araz_Form.Form.Account
{
    public partial class frmSearchPerson : DevExpress.XtraEditors.XtraForm
    {
        List<View_Person> selectedperson = new List<View_Person>();
        bool singleselect;
        public frmSearchPerson()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }
        public void FillData()
        {
            this.singleselect = singleselect;
            gridColumn1.Visible = !singleselect;
            gcPersonList.DataSource =DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person", "").ToList();

        }

        private void gvPersonList_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.RowHandle >= 0)
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
        }

        private void gvPersonList_DoubleClick(object sender, EventArgs e)
        {
            var t = gvPersonList.GetFocusedRow() as View_Person;
            if (t != null)
            {
                this.selectedperson.Clear();
                this.selectedperson.Add(t);
                this.Close();
            }
        }

        private void frmSearchPerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((gvPersonList.DataSource as List<View_Person>) != null && (gvPersonList.DataSource as List<View_Person>).Count > 0)
            {
                if (this.singleselect == false)
                {
                    var i = gvPersonList.FocusedRowHandle;
                    gvPersonList.FocusedRowHandle = i == 0 ? 1 : 0;  // برای گرفتن تیک آخر کاربر این خط نیاز است 
                    this.selectedperson.Clear();
                    foreach (var item in (gvPersonList.DataSource as List<View_Person>).Where(p => p.Selected).ToList())
                        this.selectedperson.Add(item);
                }
            }
        }
    }
}