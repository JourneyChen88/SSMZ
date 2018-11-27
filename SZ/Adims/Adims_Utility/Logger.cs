using System;
using System.Reflection;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace Adims_Utility
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Logger
    {
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="exception">异常类</param>
        public static void WriteErrorLog(Exception exception)
        {
            try
            {
                DeleteLogInfo();
                string fileName = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                if (!Directory.Exists(fileName))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(fileName);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                fileName += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(exception, fileName);
            }
            catch { }
        }
        /// <summary>
        /// 数据库错误创建文本日志
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">sql参数</param>
        public static void WriteErrorLog(Exception exception, string sqlStr, params SqlParameter[] param)
        {
            try
            {
                DeleteLogInfo();
                string fileName = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                if (!Directory.Exists(fileName))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(fileName);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                fileName += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(exception, fileName, sqlStr, param);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="logString">异常类</param>
        public static void WriteErrorLog(string logString)
        {
            try
            {
                DeleteLogInfo();
                string fileName = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                if (!Directory.Exists(fileName))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(fileName);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                fileName += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                MethodBase methodName = new StackTrace().GetFrame(1).GetMethod();
                WriteLogInfo(methodName, logString, fileName);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="exception">异常类</param>
        /// <param name="logType">日志类型</param>
        public static void WriteErrorLog(Exception exception, LogType logType)
        {
            try
            {
                DeleteLogInfo();
                string fileName = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                switch (logType)
                {
                    case LogType.Error:
                        fileName = Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs"), "Error");
                        break;
                    case LogType.Message:
                        fileName = Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs"), "LogFiles");
                        break;
                }
                if (!Directory.Exists(fileName))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(fileName);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                fileName += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(exception, fileName);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="logString">异常类</param>
        /// <param name="logType">日志类型</param>
        public static void WriteLog(string logString, LogType logType)
        {
            try
            {
                DeleteLogInfo();
                string fileName = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                switch (logType)
                {
                    case LogType.Error:
                        fileName = Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs"), "Error");
                        break;
                    case LogType.Message:
                        fileName = Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs"), "LogFiles");
                        break;
                }
                if (!Directory.Exists(fileName))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(fileName);
                }
                MethodBase methodName = new StackTrace().GetFrame(1).GetMethod();
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                fileName += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(methodName, logString, fileName);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="folderPath">日志文件目录</param>
        /// <param name="exception">异常类</param>
        public static void WriteErrorLog(string folderPath, Exception exception)
        {
            try
            {
                DeleteLogInfo();
                if (!Directory.Exists(folderPath))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(folderPath);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                folderPath += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(exception, folderPath);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="folderPath">日志目标文件夹</param>
        /// <param name="fileName">日志文件名称</param>
        /// <param name="logString">日志内容</param>
        public static void WriteLog(string folderPath, string fileName, string logString)
        {
            try
            {
                if (!Directory.Exists(folderPath))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(folderPath);
                }
                fileName = Path.Combine(folderPath, fileName);
                if (!File.Exists(fileName))
                    File.Create(fileName).Close();
                StreamWriter sw = null;
                try
                {
                    using (sw = new StreamWriter(fileName, true, Encoding.Default))
                    {
                        //sw.WriteLine("*****************************************【"
                        //              + DateTime.Now.ToLongTimeString()
                        //              + "】*****************************************");
                        if (!string.IsNullOrEmpty(logString))
                        {
                            sw.WriteLine(logString);
                        }
                        else
                        {
                            sw.WriteLine("Exception is empty");
                        }
                        sw.WriteLine();
                        sw.Dispose();
                        sw.Close();
                    }
                }
                catch
                {
                }
                finally
                {
                    //3、关闭流
                    sw.Close();
                }
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="logString">异常类</param>
        public static void WriteLog(string logString)
        {
            try
            {
                DeleteLogInfo();
                string folderPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                if (!Directory.Exists(folderPath))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(folderPath);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                folderPath += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(logString, folderPath);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="exception">异常类</param>
        public static void WriteLog(Exception exception)
        {
            try
            {
                DeleteLogInfo();
                string folderPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
                if (!Directory.Exists(folderPath))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(folderPath);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                folderPath += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                WriteLogInfo(exception, folderPath);
            }
            catch { }
        }
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="folderPath">日志文件目录</param>
        /// <param name="exception">异常类</param>
        public static void WriteLog(string folderPath, string exception)
        {
            try
            {
                DeleteLogInfo();
                if (!Directory.Exists(folderPath))
                {
                    //创建日志文件夹
                    Directory.CreateDirectory(folderPath);
                }
                //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
                folderPath += "\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                MethodBase methodName = new StackTrace().GetFrame(1).GetMethod();
                WriteLogInfo(methodName, exception, folderPath);
            }
            catch { }
        }
        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="exception">异常类</param>
        /// <param name="fileName">日志文件存放路径</param>
        private static void WriteLogInfo(Exception exception, string fileName)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    sw.WriteLine("*****************************************【"
                                  + DateTime.Now.ToLongTimeString()
                                  + "】*****************************************");
                    if (exception != null)
                    {
                        sw.WriteLine("【实例的运行时类型】" + exception.GetType());
                        sw.WriteLine("【导致异常的应用程序或对象名称】" + exception.Source.Trim());
                        sw.WriteLine("【引发异常的方法】" + exception.TargetSite);
                        sw.WriteLine("【异常信息】" + exception.Message.Trim());
                        sw.WriteLine("【堆栈】" + exception.StackTrace.Trim());
                    }
                    else
                    {
                        sw.WriteLine("Exception is NULL");
                    }
                    sw.WriteLine();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {
            }
            finally
            {
                //3、关闭流
                sw.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception">异常类</param>
        /// <param name="fileName">日志文件存放路径</param>
        /// <param name="sqlStr">出错SQL语句</param>
        /// <param name="param">sql参数</param>
        private static void WriteLogInfo(Exception exception, string fileName, string sqlStr, params SqlParameter[] param)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    sw.WriteLine("*****************************************【"
                                  + DateTime.Now.ToLongTimeString()
                                  + "】*****************************************");
                    if (exception != null)
                    {
                      
                       
                        sw.WriteLine("【实例的运行时类型】" + exception.GetType());
                        sw.WriteLine("【导致异常的应用程序或对象名称】" + exception.Source.Trim());
                        sw.WriteLine("【引发异常的方法】" + exception.TargetSite);
                        sw.WriteLine("【异常信息】" + exception.Message.Trim());
                        sw.WriteLine("【异常SQL语句】" + sqlStr.Trim());
                        sw.WriteLine("【SQL参数】" + param.ToArray().ToString());
                        sw.WriteLine("【堆栈】" + exception.StackTrace.Trim());
                    }
                    else
                    {
                        sw.WriteLine("Exception is NULL");
                    }
                    sw.WriteLine();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {
            }
            finally
            {
                //3、关闭流
                sw.Close();
            }
        }
        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="methodName">调用者</param>
        /// <param name="logString">异常类</param>
        /// <param name="fileName">日志文件存放路径</param>
        private static void WriteLogInfo(MethodBase methodName, string logString, string fileName)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    sw.WriteLine("*****************************************【"
                                  + DateTime.Now.ToLongTimeString()
                                  + "】*****************************************");
                    if (logString != null)
                    {
                      
                       
                        sw.WriteLine("【实例的运行时类型】" + logString.GetType());
                        sw.WriteLine("【导致异常的应用程序或对象名称】" + methodName.Module.ToString());
                        sw.WriteLine("【引发异常的方法】" + methodName.Name);
                        sw.WriteLine("【异常信息】" + logString);
                        sw.WriteLine("【堆栈】" + methodName.ReflectedType.Namespace);
                    }
                    else
                    {
                        sw.WriteLine("Exception is NULL");
                    }
                    sw.WriteLine();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {
            }
            finally
            {
                //3、关闭流
                sw.Close();
            }
        }
        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="logString">异常类</param>
        /// <param name="fileName">日志文件存放路径</param>
        private static void WriteLogInfo(string logString, string fileName)
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    sw.WriteLine("*****************************************【"
                                  + DateTime.Now.ToLongTimeString()
                                  + "】*****************************************");
                    if (!string.IsNullOrEmpty(logString))
                    {
                      
                       
                        sw.WriteLine("【内容】" + logString);
                    }
                    else
                    {
                        sw.WriteLine("Exception is empty");
                    }
                    sw.WriteLine();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {
            }
            finally
            {
                //3、关闭流
                sw.Close();
            }
        }
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="logString">The log string.</param>
        public static void WriteOperationLog(string logString)
        {
            DeleteLogInfo();
            string folderPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config"), "Logs");
            if (!Directory.Exists(folderPath))
            {
                //创建日志文件夹
                Directory.CreateDirectory(folderPath);
            }
            string fileName = Path.Combine(folderPath, DateTime.Now.ToString("操作日志yyyyMMdd") + ".log");
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            //发生异常每天都创建一个单独的日志文件[*.log],每天的错误信息都在这一个文件里。方便查找
            StreamWriter sw = null;
            try
            {
                using (sw = new StreamWriter(fileName, true, Encoding.Default))
                {
                    if (!string.IsNullOrEmpty(logString))
                    {
                        sw.Write(DateTime.Now.ToLongTimeString() + "【内容】" + logString);
                    }
                    else
                    {
                        sw.WriteLine("Exception is empty");
                    }
                    sw.WriteLine();
                    sw.Dispose();
                    sw.Close();
                }
            }
            catch
            {
            }
            finally
            {
                //3、关闭流
                sw.Close();
            }
        }
        /// <summary>
        /// 删除30天前日志文件
        /// </summary>
        private static void DeleteLogInfo()
        {
            try
            {
                string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WK_Framework_Config\\Logs");
                string[] strFiles = System.IO.Directory.GetFiles(strPath);
                //遍历所有文件
                foreach (string strFile in strFiles)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(strFile);
                    //删除文件
                    if ((DateTime.Now - file.CreationTime).Days > 30)
                    {
                        file.Delete();
                    }
                }
            }
            catch { }
        }
    
     
    
     
    
     
        /// <summary>
        /// 消息类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 
            /// </summary>
            Error = 1,
            /// <summary>
            /// 
            /// </summary>
            Message = 0
        };
 
     
    }
}
