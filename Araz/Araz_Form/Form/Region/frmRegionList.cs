using Araz_ViewModel;
using DevExpress.CodeParser;
using DevExpress.Utils.VisualEffects;
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

namespace Araz_Form.Form.Region
{
    public partial class frmRegionList : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmRegionList()
        {
            CommonTools.Loading(true);
            InitializeComponent();
            FillData();
            CommonTools.Loading();
        }
        public void FillData()
        {
            cmbProvince.Properties.DataSource = DARepository.GetAllFromView<View_City>("SELECT DISTINCT(ProvinceName) , ProvinceID FROM dbo.View_City", "WHERE ParentProvinceID IS NULL");
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
                var where = "where PerentCityID = " + _city.ProvinceID ;
                gcCityList.DataSource = DARepository.GetAllFromView<View_City>(select, where).ToList();
            }
        }
    }
}