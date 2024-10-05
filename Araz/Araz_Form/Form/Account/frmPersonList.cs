using Araz_ViewModel;
using DevExpress.XtraBars;
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
    public partial class frmPersonList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmPersonList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }
        public void FillData ()
        {
            gcPersonList.DataSource = DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person","").ToList();
        }
        private void btnAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmPersonDefine frm = new frmPersonDefine(1, null);
            frm.ShowDialog();
        }
    }
}