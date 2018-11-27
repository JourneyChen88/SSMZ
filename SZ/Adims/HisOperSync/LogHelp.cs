using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InsertSysLogTool
{
    public static class LogHelp
    {
        static object locker = new object();
        public static void WriteLog(params string[] logs)
        {
            lock (locker)
            {
                string LogAddress = Environment.CurrentDirectory + "\\Log";
                if (!Directory.Exists(LogAddress))
                {
                    Directory.CreateDirectory(LogAddress);
                }
                LogAddress = string.Concat(LogAddress, "\\",
                 DateTime.Now.Year, '-', DateTime.Now.Month, '-',
                 DateTime.Now.Day, ".log");
                StreamWriter sw = new StreamWriter(LogAddress, true);
                foreach (string log in logs)
                {
                    sw.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString(), log));
                }
                sw.Close();
            }
        }
    }
}
