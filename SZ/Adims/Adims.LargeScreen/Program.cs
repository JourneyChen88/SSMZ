using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using adims_MODEL;
using Adims.LargeScreenWaitZone;

namespace Adims.LargeScreenWaitZone
{
    static class Program
    {
        public static string yh;
        public static string zhanghao;
        public static user_info customer = new user_info();
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
            Application.Run(new CurrentOperationInfo());
        }
    }
}
