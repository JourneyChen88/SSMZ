using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using adims_MODEL;
using adims_BLL;
using main.科室事物管理;

namespace main
{
    static class Program
    {
        public static string yh;
        public static string zhanghao;
        public static UserInfo customer = new UserInfo();
        public static bool[] jurisdiction;
        public static List<string> muluz = new List<string>();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //FlowStatusQuery fsq = new FlowStatusQuery();
            //Application.Run(fsq);
            //MzjldEdit mzjld1 = new MzjldEdit("22107", "手术间1", DateTime.Now, 3508);
            //Application.Run(mzjld1);


            login loginForm = new login();
            loginForm.ShowDialog();
            if (loginForm.DialogResult == DialogResult.OK)
            {
                Application.Run(new AppMainForm());
            }
        }
    }
}
