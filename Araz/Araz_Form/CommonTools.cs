﻿using Araz_Form;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System.Diagnostics;
using System.Windows.Forms;
using ViewModel.ViewModels;

namespace Utilities
{
    public class CommonTools
    {
        public static void Loading(bool start = false)
        {
            if (start)
            {
                if (SplashScreenManager.Default == null || !SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.ShowForm(typeof(WaitingForm));
            }
            else
            {
                if (SplashScreenManager.Default != null && SplashScreenManager.Default.IsSplashFormVisible)
                    SplashScreenManager.CloseForm();
            }
        }
    }
}