using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ListenerRoutingWin
{
    static class Program
    {
        public static string _PaibanTable;
        public static string _NewOperApply;
        public static string _UpdateOperApply;
        public static string _CancelOperApply;
        public static string _AcceptTitleOperDic;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ListenerRoutingWin());
        }
    }
}
