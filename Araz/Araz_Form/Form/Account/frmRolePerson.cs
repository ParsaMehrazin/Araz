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

namespace Araz_Form.Form.Account
{
    public partial class frmRolePerson : DevExpress.XtraEditors.XtraForm
    {
        public frmRolePerson()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }
        private void FillData()
        {           
            cmbRole.Properties.DataSource = DARepository.GetAllFromView<View_Role>("SELECT * FROM dbo.View_Role", "").ToList();
          
        }
    }
}