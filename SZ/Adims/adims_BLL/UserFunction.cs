using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace adims_BLL
{
    public static class UserFunction
    {
        /// <summary>  
        /// 按字符串长度切分成数组  
        /// </summary>  
        /// <param name="str">原字符串</param>  
        /// <param name="separatorCharNum">切分长度</param>  
        /// <returns>字符串数组</returns>  
        public static List<string> SplitByLen(string str, int separatorCharNum)
        {
            List<string> strList = new List<string>();
            if (string.IsNullOrEmpty(str) || str.Length <= separatorCharNum)
            {
                strList.Add(str);
                return strList;
            }
            string tempStr = str;

            int iMax = Convert.ToInt32(Math.Ceiling(str.Length / (separatorCharNum * 1.0)));//获取循环次数  
            for (int i = 1; i <= iMax; i++)
            {
                string currMsg = tempStr.Substring(0, tempStr.Length > separatorCharNum ? separatorCharNum : tempStr.Length);
                strList.Add(currMsg);
                if (tempStr.Length > separatorCharNum)
                {
                    tempStr = tempStr.Substring(separatorCharNum, tempStr.Length - separatorCharNum);
                }
            }
            return strList;
        }
        public static DataTable removeEmptyRow(DataTable dt)//删除datatable空白行
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool rowdataisnull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {

                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        rowdataisnull = false;
                    }

                }
                if (rowdataisnull)
                {
                    removelist.Add(dt.Rows[i]);
                }

            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            }
            return dt;
        }
        public static DataTable GetDataFromExcel(string fileType, string filePath, string sheetName)
        {


            using (DataSet ds = new DataSet())
            {
                string strCon = string.Format("Provider=Microsoft.ACE.OLEDB.{0}.0;" +
                                       "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                       "data source={3};",
                                       (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), "Yes", filePath);
                using (OleDbConnection myConn = new OleDbConnection(strCon))
                {
                    //打开连接
                    if (myConn.State == ConnectionState.Broken || myConn.State == ConnectionState.Closed)
                    {
                        myConn.Open();
                    }

                    System.Data.DataTable schemaTable = myConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    //获取Excel的Sheet名称
                    string strCom = " SELECT * FROM [" + sheetName + "$]";

                    using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                    {
                        myCommand.Fill(ds);
                    }
                    if (ds == null || ds.Tables.Count <= 0) return null;
                    return ds.Tables[0];
                }
            }
        }
        public static void NoKeyboard(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// 仅输入数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Text_Value_Limit(object sender, KeyPressEventArgs e)
        {

            if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
            {
                if (e.KeyChar == '.')
                {
                    if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                        e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            else
            {
                if (e.KeyChar <= 31)
                {
                    e.Handled = false;
                }
                else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                {
                    if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 4)
                        e.Handled = true;
                }
            }

        }

  
        public static void Text_Value_decimal(object sender, KeyPressEventArgs e)
        {

            if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }


        }
        public static int ToInt32(string str)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(str);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
        public static void SaveLogHL7(string msg)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(Application.StartupPath + @"\SendHL7message.txt"))
                {
                    string str = DateTime.Now.ToString() + Environment.NewLine + msg;
                    sw.WriteLine(str);
                    sw.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        public static int ToInt32(object obj)
        {
            int i = 0;
            try
            {
                i = Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
            return i;
        }
        public static double ToDouble(object obj)
        {
            double i = 0.0;
            try
            {
                i = Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return 0.0;
            }
            return i;
        }

        public static bool PingHost(string Address, int TimeOut = 1000)
        {
            using (System.Net.NetworkInformation.Ping PingSender = new System.Net.NetworkInformation.Ping())
            {
                PingOptions Options = new PingOptions();
                Options.DontFragment = true;
                string Data = "test";
                byte[] DataBuffer = Encoding.ASCII.GetBytes(Data);
                PingReply Reply = PingSender.Send(Address, TimeOut, DataBuffer, Options);
                if (Reply.Status == IPStatus.Success)
                    return true;
                return false;
            }
        }
    }
}
