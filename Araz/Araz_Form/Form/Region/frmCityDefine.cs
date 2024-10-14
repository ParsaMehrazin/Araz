using Araz_ViewModel;
using DevExpress.DashboardCommon.Native.DashboardRestfulService;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
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
using System.Xml.Linq;
using Utilities;
using ViewModel.ViewModels;

namespace Araz_Form.Form.Region
{
    public partial class frmCityDefine : DevExpress.XtraEditors.XtraForm
    {

        Int64 pkCityId = -1;
        string cityname = null;       
        public bool _isSave = false;
        public bool hasError = false;
        int _mod = -1;  
        View_City _City = new View_City();

        public frmCityDefine(int mod , View_City model)
        {
            CommonTools.Loading(true);
            InitializeComponent();
            _mod = mod;
            _City = model;            
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
                modThree();    
                if (CommonTools.AskQuestion($" آیا از حذف {cityname} مطمئن هستید؟ "))
                {                    
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
            cmbProvince.Properties.DataSource= DARepository.GetAllFromView<View_City>("SELECT DISTINCT(ProvinceName) , ProvinceID FROM dbo.View_City", "WHERE ParentProvinceID IS NULL").ToList();
        }

        public bool modOne()
        {
            try
            {
                this.Text = "ثبت نام شهر";
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool modTwo() 
        {
            try
            {           
                    this.Text = "ویرایش نام شهر";
                    txtCity.Text = _City.CityName;
                    cmbProvince.EditValue = (cmbProvince.Properties.DataSource as List<View_City>).Where(p => p.ProvinceID == _City.ProvinceID).FirstOrDefault();
                    pkCityId = Convert.ToInt64(_City.CityID);
            
                    //this.Text = "ویرایش نام استان";
                    //cmbProvince.EditValue = (cmbProvince.Properties.DataSource as List<View_City>).Where(p => p.ProvinceID == _City.ProvinceID).FirstOrDefault();
                    //cityname = _City.ProvinceName;
                    //pkCityId = Convert.ToInt64(_City.ProvinceID);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public bool modThree()
        {
            try
            {
                    cityname = _City.CityName;
                    pkCityId = Convert.ToInt64(_City.CityID);
                
               
                    //cityname = _City.ProvinceName;
                    //pkCityId = Convert.ToInt64(_City.ProvinceID);
               
                return true;
            }
            catch (Exception)
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
                 if (string.IsNullOrEmpty(cmbProvince.Text) || cmbProvince.EditValue == null)
                        ErrorProvider.SetError(cmbProvince, "لطفا یک استان را انتخاب کنید ");
               
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
             new ServiceOperatorParameter() { Name = "pkCityID", Value = _mod == 1 ? "-1" : this.pkCityId.ToString() },
             new ServiceOperatorParameter() { Name = "PerentCityID", Value = (cmbProvince.EditValue as View_City) == null ? -1 : (cmbProvince.EditValue as View_City).ProvinceID },
             new ServiceOperatorParameter() { Name = "CityName", Value = string.IsNullOrEmpty(txtCity.Text) ? "" : txtCity.Text });

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
    }
}