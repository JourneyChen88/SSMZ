using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_Utility
{
    public static class TypeExtension
    {
        /// <summary>
        /// 强制转化日期格式为yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeLong(this DateTime timeStamp)
        {
            DateTime dtime = Convert.ToDateTime(timeStamp.ToString("F"));

            return dtime;
        }
        /// <summary>
        /// 强制转为时间，默认为当前时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object str)
        {
            DateTime res = ToDateTimeLong(DateTime.Now);
            if (str == null)
            {
                res = ToDateTimeLong(DateTime.Now);
            }
            else
            {
                try
                {
                    res = Convert.ToDateTime(str);
                }
                catch (Exception)
                {
                    res = ToDateTimeLong(DateTime.Now);
                }
            }
            return res;

        }
        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtime = DateTime.ParseExact(timeStamp, "yyyyMMdd", null);

            return dtime;
        }
        /// <summary>
        /// 是否为空或空字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object str)
        {
            try
            {
                if (str == null)
                {
                    return true;
                }
                else if (str.ToString() == "" || str.ToString() == string.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return true;
            }

        }

        /// <summary>
        ///  判断是否不为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns> true 非空，false 空</returns>
        public static bool IsNotNullOrEmpty(this object str)
        {
            try
            {
                if (str == null)
                {
                    return false;
                }
                else if (str.ToString() == "" || str.ToString() == string.Empty)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool ToBoolean(this object str)
        {
            bool res = false;
            try
            {
                res = Convert.ToBoolean(str);
            }
            catch (Exception)
            {
                res = false;
            }
            return res;

        }
        /// <summary>
        /// 强制转string,默认为空字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToStringForce(this object str)
        {
            string res = "";
            if (IsNullOrEmpty(str))
            {
                res = "";
            }
            else
            {
                try
                {
                    res = str.ToString();
                }
                catch (Exception)
                {
                    res = "";
                }
            }
            return res;

        }

        /// <summary>
        /// 强制转为两位小数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToStringF2(this object str)
        {
            string res = "0.00";
            if (IsNullOrEmpty(str))
            {
                res = "0.00";
            }
            else
            {
                try
                {
                    res = str.ToDecimal().ToString("F2");
                }
                catch (Exception)
                {
                    res = "0.00";
                }
            }
            return res;

        }

        /// <summary>
        /// 强制转为Guid,默认为 000000000-0000-0000-0000000000000
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object strGuid)
        {
            Guid res = new Guid();
            if (strGuid == null)
            {
                res = new Guid();
            }
            else
            {
                try
                {
                    res = Guid.Parse(strGuid.ToString());
                }
                catch (Exception)
                {
                    res = new Guid();
                }
            }
            return res;

        }
        /// <summary>
        /// 强制转化为整形，默认为0
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static int ToInt32(this object strGuid)
        {
            int res = 0;
            if (strGuid == null)
            {
                res = 0; ;
            }
            else
            {
                try
                {
                    res = Convert.ToInt32(strGuid.ToString());
                }
                catch (Exception)
                {
                    res = 0;
                }
            }
            return res;

        }
        /// <summary>
        /// 强制装换为Long ,默认为0
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static long ToInt64(this object strGuid)
        {
            long res = 0;
            if (strGuid == null)
            {
                res = 0;
            }
            else
            {
                try
                {
                    res = Convert.ToInt64(strGuid.ToString());
                }
                catch (Exception)
                {
                    res = 0;
                }
            }
            return res;

        }
        /// <summary>
        /// 强制转化为decimal ,默认为0M
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object strGuid)
        {
            decimal res = 0M;
            if (strGuid == null)
            {
                res = 0M;
            }
            else
            {
                try
                {
                    res = Convert.ToDecimal(strGuid);
                }
                catch (Exception)
                {
                    res = 0M;
                }
            }
            return res;

        }

        public static double ToDouble(this object strGuid)
        {
            double res = 0;
            if (strGuid == null)
            {
                res = 0;
            }
            else
            {
                try
                {
                    res = Convert.ToDouble(strGuid);
                }
                catch (Exception)
                {
                    res = 0;
                }
            }
            return res;

        }
        /// <summary>
        /// 强制转为整形，默认为1
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static int ToIntDefalt1(this object strGuid)
        {
            int res = 1;
            if (strGuid == null)
            {
                res = 1;
            }
            else
            {
                try
                {
                    res = Convert.ToInt32(strGuid.ToString());
                }
                catch (Exception)
                {
                    res = 1;
                }
            }
            return res;

        }

    }
}
