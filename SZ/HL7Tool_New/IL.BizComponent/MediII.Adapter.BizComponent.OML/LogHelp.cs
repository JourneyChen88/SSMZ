using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediII.Adapter.BizComponent.ORM
{
    public static class LogHelp
    {
        static object locker = new object();
        public static void SaveLogHL7(string msg)
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
                 DateTime.Now.Day, "_HL7.log");
                StreamWriter sw = new StreamWriter(LogAddress, true);
                sw.WriteLine(string.Format("[{0}] {1}", DateTime.Now.ToString(), msg));
                sw.Close();
            }

        }
   

     
        public static void SaveMonitorLog(string str, string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(str);
            sw.Close();
            fs.Close();
        }
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

        public static void WriteErrorLog(params string[] logs)
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
                 DateTime.Now.Day, "_Error.log");
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

