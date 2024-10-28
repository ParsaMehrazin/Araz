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

namespace Araz_Form.Form
{
    public partial class frmInvoiceList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmInvoiceList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }

        public void FillData()
        {
            cmbPersonList.Properties.DataSource = DARepository.GetAllFromView<View_Person>("SELECT * FROM dbo.View_Person", "").ToList();
        }

        private void btnBuyInvoice_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
    }
}