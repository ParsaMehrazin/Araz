using DevExpress.XtraSplashScreen;

namespace Araz_Form
{
    public partial class frmMainDashboard : DevExpress.XtraEditors.XtraForm
    {
        public frmMainDashboard()
        {
            InitializeComponent();

            if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible)
                SplashScreenManager.ShowForm(typeof(WaitingForm));

            ApplyPermission();
            FillData();

            if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                SplashScreenManager.CloseForm();
        }

        void ApplyPermission()
        {

        }

        void FillData()
        {

        }

    }
}