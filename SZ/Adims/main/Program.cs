using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using adims_MODEL;
using adims_BLL;
using main.MZJLDs;
using main.MedicalConsumableManage;
using main.Statistics;

namespace main
{
    static class Program
    {
        public static string yh;
        public static string zhanghao;
        public static user_info customer = new user_info();
        public static bool[] jurisdiction;
        public static List<string> muluz = new List<string>();
        public static GlobalsModel Globals = new GlobalsModel();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // new AfterVisit_SZ().ShowDialog();
            // new mzjldEdit( "170821134429SZ00794344001","手术间1",DateTime.Now, 13121).ShowDialog();

            LogIn loginForm = new LogIn();
            loginForm.ShowDialog();
            if (loginForm.DialogResult == DialogResult.OK)
            {
                //new ConsumableManageForm().ShowDialog();

                Application.Run(new MainForm());
            }
        }
    }
}
