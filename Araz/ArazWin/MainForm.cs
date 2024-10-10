using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using System;
using System.Windows.Forms;
using Araz_Form;
using Araz_Form.Form.Account;
using Araz_Form.Form.Region;

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

        #endregion


        #region ■■■■■ rpCommerce ■■■■■  

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
         
        #endregion


        #endregion

        #region ■■■■■ Main Form Event ■■■■■          

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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


        private void BuyButton_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
        }

        private void btnPerson_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPersonList frm = new frmPersonList();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnProvince_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmRegionList frm = new frmRegionList();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}