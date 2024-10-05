using Araz_Form;
using DevExpress.CodeParser;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using DevExpress.XtraSplashScreen;
using System;
using System.Diagnostics;
using System.Drawing;
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

        public static bool ShowMessage(BaseRepositoryResponseViewModel model, int showMessageTime = 2)
        {
            //Always =2;
            //False=0;
            // True=1;
            if (model.Success)
            {
                if (string.IsNullOrEmpty(model.msg))
                {
                    XtraMessageBox.Show("پیغامی یافت نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (model.Result < 1)
                {
                    if (showMessageTime == 0 || showMessageTime == 2)
                        XtraMessageBox.Show(model.msg, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                else
                    if (showMessageTime == 1 || showMessageTime == 2)
                    XtraMessageBox.Show(model.msg, "اطلاعیه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                XtraMessageBox.Show(model.msg, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ShowMessage(string msg, int showMessageTime = 2)
        {

            //Always =2;
            //False=0;
            // True=1;
            if (string.IsNullOrEmpty(msg))
            {
                XtraMessageBox.Show("پیغامی یافت نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (msg.Contains("@"))
            {
                var split = msg.Split('@');
                if (split[0] == "0" || split[0] == "-1")
                {
                    if (showMessageTime == 0 || showMessageTime == 2)
                        XtraMessageBox.Show(split[1], "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
                else
                    if (showMessageTime == 1 || showMessageTime == 2)
                    XtraMessageBox.Show(split[1], "اطلاعیه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                XtraMessageBox.Show(msg, "اطلاعیه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }

        public static bool AskQuestion(string msg = " آیا از حذف مطمئن هستید ؟")
        {
            if (XtraMessageBox.Show(msg, "سوال", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return false;
            else
                return true;
        }

        public static void ExportToExcel(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            //if (!Utilities.ControlUnit.Role.Any(c => c.fkRole == 35))
            //{
            //    XtraMessageBox.Show("شما این دسترسی را ندارید", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                if (saveFileDialog.FileName != string.Empty)
                {
                    gv.ExportToXlsx(saveFileDialog.FileName);
                    if (XtraMessageBox.Show("خروجی با موفقیت انجام شد . آیا مایل به باز کردن فایل مربوطه هستید ؟", "Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        Process.Start(saveFileDialog.FileName);
                }
        }
    }
}
