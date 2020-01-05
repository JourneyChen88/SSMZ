using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace adims_BLL
{
   public class TextValueLimit
    {
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
        public static void Int_Limit(object sender, KeyPressEventArgs e)
        {
            if ((((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

        }
        /// <summary>
        /// 判断日期格式是否有效
        /// </summary>
        /// <param name="objString"></param>
        /// <returns></returns>
        public static bool IsDate(String objString)
        {
            Regex myReg = new Regex(@"\b(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})\b");
            return myReg.IsMatch(objString);
        }

        public static int ConvertToInt(object objString)
        {
            int x = 0;
            try
            {
                x = Convert.ToInt32(objString);
                return x;
            }
            catch (Exception)
            {

                return 0;
            }

        }
    }
}
