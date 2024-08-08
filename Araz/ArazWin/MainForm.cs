using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using System;
using System.Windows.Forms;
using Araz_Form;

namespace ArazWin
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private Timer timer;
        private bool VersionUpdated = true;

        public MainForm()
        {
            InitializeComponent();

            if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible)
                SplashScreenManager.ShowForm(typeof(WaitingForm));

            ApplyPermission();
            CheckDevMode();

            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                SplashScreenManager.CloseForm();
        }



        #region ■■■■■ Forms ■■■■■ 


        #region ■■■■■ rpInitialInformation ■■■■■ 
        private void btnEmployeeList_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmEmployeeList frm = new frmEmployeeList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnCustomerList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmCustomerList frm = new frmCustomerList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnRoleList_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmRoleList>().Any())
            //{
            //    frmRoleList frm = new frmRoleList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmRoleList>().FirstOrDefault().Focus();
        }

        private void btnObjectAccess_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmObjectAccessList>().Any())
            //{
            //    frmObjectAccessList frm = new frmObjectAccessList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmObjectAccessList>().FirstOrDefault().Focus();
        }

        private void btnCountryList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmCountryList>().Any())
            //{
            //    frmCountryList frm = new frmCountryList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmCountryList>().FirstOrDefault().Focus();
        }

        private void btnStateList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmStateList>().Any())
            //{
            //    frmStateList frm = new frmStateList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmStateList>().FirstOrDefault().Focus();
        }

        private void btnCityList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmCityList>().Any())
            //{
            //    frmCityList frm = new frmCityList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmCityList>().FirstOrDefault().Focus();
        }

        #endregion

        #region ■■■■■ rpCommerce ■■■■■ 

        private void btnLeadList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmLeadList frm = new frmLeadList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        //function formatNumber(input)
        //{
        //    // حذف هر چیزی جز اعداد
        //    var numericInput = input.replace(/\D / g, '');
        //    // اعمال قرمت سه رقم سه رقم
        //    var formattedInput = numericInput.replace(/\B(?= (\d{ 3})+(? !\d))/ g, ',');
        //    return formattedInput;
        //}

        //// مثال استفاده
        //var inputNumber = '1234567';
        //var formattedNumber = formatNumber(inputNumber);
        //console.log(formattedNumber); // 1,234,567


        private void btnProductGroupList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmGroupList frm = new frmGroupList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnProductList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmProductList frm = new frmProductList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnGroupPropertyValueList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //    frmGroupPropertyValue frm = new frmGroupPropertyValue();
            //    frm.MdiParent = this;
            //    frm.Show();
        }

        private void btnGroupPropertyList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmGroupPropertyList frm = new frmGroupPropertyList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnPropertyValueList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmPropertyValueList frm = new frmPropertyValueList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnPropertyList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmPropertyList frm = new frmPropertyList();
            //frm.MdiParent = this;
            //frm.Show();
        }


        #endregion

        #region ■■■■■ rpReport ■■■■■ 
        #endregion

        #region ■■■■■ rpFinancial ■■■■■ 

        private void btnFinancialReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmFinancialReport frm = new frmFinancialReport();
            //frm.MdiParent = this;
            //frm.Show();
        }

        #endregion


        #region ■■■■■ rpSetting ■■■■■ 

        private void btnOptionList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmSysOptionList>().Any())
            //{
            //    frmSysOptionList frm = new frmSysOptionList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmSysOptionList>().FirstOrDefault().Focus();
        }

        private void btnSettingList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (!Application.OpenForms.OfType<frmSysSettingList>().Any())
            //{
            //    frmSysSettingList frm = new frmSysSettingList();
            //    frm.MdiParent = this;
            //    frm.Show();
            //}
            //else
            //    Application.OpenForms.OfType<frmSysSettingList>().FirstOrDefault().Focus();
        }

        #endregion

        #region Ribbon
        private void btnTicketDefine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmTicketDefine frm = new frmTicketDefine();
            //frm.ShowDialog();
        }
        #endregion


        #endregion

        #region ■■■■■ Main Form Event ■■■■■

        private void btnMyProfile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmEmployeeDefine frm = new frmEmployeeDefine(2, ControlUnit.LoggedUser, true);
            //frm.ShowDialog();
        }

        private void lblUserFullName_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmEmployeeDefine frm = new frmEmployeeDefine(2, ControlUnit.LoggedUser, true);
            //frm.ShowDialog();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void txtVersion_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void Mainfrom_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 60000;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();

            GetDashboardData();
        }

        void GetDashboardData()
        {      
            frmMainDashboard frm = new frmMainDashboard();
            frm.MdiParent = this;
            frm.ControlBox = false;
            frm.Show();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            //GetNotifications();
        }

        void CheckDevMode()
        {

        }

        void ApplyPermission()
        {
            //var role = DARepository.GetAllEmployeeRole(Utilities.ControlUnit.LoggedUser.UsrVcCode).ToList();
            //if (role != null && !role.Any(p => p.fkRole == (int)Utilities.Enums.Roles.Admin))
            //{
            //    var list = MainRibbonControl.Items.Where(c => c.RibbonStyle == DevExpress.XtraBars.Ribbon.RibbonItemStyles.All ||
            //        c.RibbonStyle == DevExpress.XtraBars.Ribbon.RibbonItemStyles.Default ||
            //        c.RibbonStyle == DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large ||
            //        c.RibbonStyle == DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText ||
            //        c.RibbonStyle == DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText).ToList();
            //    var access = DARepository.GetAllEmployeeObjectAccess(Utilities.ControlUnit.LoggedUser.UsrVcCode);
            //    foreach (var item in list)
            //        item.Enabled = access.Any(p => p.ObjectAccessName == item.Name && p.isInsideForm == false);
            //}
        }

        private void xtraTabbedMdiManager_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            pictureEdit.Visible = false;
        }

        private void xtraTabbedMdiManager_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (this.xtraTabbedMdiManager.Pages.Count == 0)
                pictureEdit.Visible = true;
        }

        private void MainRibbonControl_Merge(object sender, RibbonMergeEventArgs e)
        {
            RibbonControl parentRRibbon = sender as RibbonControl;
            RibbonControl childRibbon = e.MergedChild;
            parentRRibbon.MergeRibbon(childRibbon);
            if (parentRRibbon.MergedPages.Count > 0)
                parentRRibbon.SelectedPage = parentRRibbon.MergedPages[0];
        }



        #endregion

        private void btnInputEvidenceList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmInputEvidenceList frm = new frmInputEvidenceList();
            //frm.MdiParent = this;
            //frm.Show();

        }

        private void btnInputEvidenceDefine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmInputEvidenceDefine frm = new frmInputEvidenceDefine();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void btnLeadList23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnSeperationProduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //    frmSeperationProductList frm = new frmSeperationProductList();
            //    frm.MdiParent = this;
            //    frm.Show();
        }

        private void btnOutputEvidenceList_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmOutputEvidenceList frm = new frmOutputEvidenceList();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void BuyButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPurchaseInvoice frm = new frmPurchaseInvoice();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}