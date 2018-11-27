using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MediII.Net.Common
{

    public class LogTxt
    {
        //private static string m_LogDirPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\";
        private static string m_LogDirPath = string.Empty;
        private static object m_Lock = new object();

        #region 写出错信息
        /// <summary>
        /// 写出错信息
        /// </summary>
        /// <param name="strFuntiongName">
        /// 简单说明
        /// </param>
        /// <param name="ex">
        /// 异常
        /// </param>
        public static void WriteError(string Title, string LogMSG, EventLogEntryType LogType)
        {
            lock (m_Lock)
            {
                m_LogDirPath = AppDomain.CurrentDomain.BaseDirectory.Replace("/", "\\");
                if (m_LogDirPath.EndsWith("\\"))
                {
                    //m_LogDirPath = m_LogDirPath.Substring(0, m_LogDirPath.LastIndexOf("\\"));
                    //m_LogDirPath = m_LogDirPath.Substring(0, m_LogDirPath.LastIndexOf("\\"));
                    m_LogDirPath = m_LogDirPath + @"Log\";
                }
                else
                {
                    //m_LogDirPath = m_LogDirPath.Substring(0, m_LogDirPath.LastIndexOf("\\"));
                    m_LogDirPath = m_LogDirPath + @"\Log\";
                }
                if (Directory.Exists(m_LogDirPath) == false)
                {
                    Directory.CreateDirectory(m_LogDirPath);
                }
                Worker.WriteLogTxt(m_LogDirPath, Title, LogMSG, LogType);
            }
        }
        #endregion
    }

    class Worker
    {
        public static void WriteLogTxt(string FilePath, string Title, string LogMSG, EventLogEntryType LogType)
        {
            if (Directory.Exists(FilePath) == false)
            {
                Directory.CreateDirectory(FilePath);
            }
            //每半个小时创建一个文件
            string serverPath = string.Empty;
            serverPath = String.Format("{0}{1}_{2:yyyyMMddHH}.log", FilePath, LogType, DateTime.Now);
          
            if (!string.IsNullOrEmpty(Title))
            {
                LogMSG = String.Format("{0}\r\n{1}", Title, LogMSG);
            }
            LogMSG = String.Format("发生日期：{0:yyyy-MM-dd HH:mm:ss:fff}\r\n{1}", DateTime.Now, LogMSG);
            using (StreamWriter sw = new StreamWriter(serverPath, true))
            {
                sw.WriteLine(LogMSG);
                sw.WriteLine();
                sw.Flush();
                sw.Close();
            }
        }
    }
}