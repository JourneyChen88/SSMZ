
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adims_Utility
{
    public static class TypeExtension
    {
        public static DataTable ToDataTable(DataRow[] rows)
        {
            if (rows == null || rows.Length == 0) return null;
            DataTable tmp = rows[0].Table.Clone();  // 复制DataRow的表结构  
            foreach (DataRow row in rows)
                tmp.Rows.Add(row.ItemArray);  // 将DataRow添加到DataTable中  
            return tmp;
        }
        /// <summary>
        /// 验证是否为空。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool StringToBoolean(this string s)
        {
            bool flg = false;
            if (!Boolean.TryParse(s, out flg)) return false;
            return flg;
        }
        /// <summary>
        /// 强制转换为数字，如果失败则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ConvertToInt32(object obj)
        {
            int result = 0;
            try
            {
                result = Convert.ToInt32(obj);
                return result;
            }
            catch (Exception ex)
            {

                return result;
            }

        }
        /// <summary>
        /// 强制转换为数字，如果失败则返回1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ConvertToInt32Default1(object obj)
        {
            int result = 1;
            try
            {
                result = Convert.ToInt32(obj);
                return result;
            }
            catch (Exception ex)
            {

                return result;
            }

        }
        /// <summary>
        /// 强制装换位Double ,失败则返回10.0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ConvertToDouble(object obj)
        {
            double result = 10.0;
            try
            {
                result = Convert.ToDouble(obj);
                return result;
            }
            catch (Exception ex)
            {

                return result;
            }

        }
        public static string ConvertToString(object obj)
        {
            string result = string.Empty;
            try
            {
                result = Convert.ToString(obj);
                return result;
            }
            catch (Exception ex)
            {

                return result;
            }

        }

        public static bool ConvertToBoolen(object obj)
        {
            bool result = false;
            try
            {
                result = Convert.ToBoolean(obj);
                return result;
            }
            catch (Exception ex)
            {

                return result;
            }
        }


        #region FontConverter字体转换。
        static FontConverter _FontConverter = new FontConverter();


        /// <summary>
        /// 利用 FontConverter对象将Font对象转换成String字符串
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public static string ToStringFontConverter(this System.Drawing.Font f)
        {
            return _FontConverter.ConvertToInvariantString(f);
        }
        #endregion

        #region String Color对象转换
        /*  注释
         *  string color = System.Drawing.ColorTranslator.ToHtml(colorEdit1.Color); //将Color对象转为String对象。如#AABBCC这种
         *  Color c = System.Drawing.ColorTranslator.FromHtml(sender.ToString());//将#AABBCC这种字符串对象转为Color对象。
         */

        /// <summary>
        ///  #AABBCC这种字符串对象转为Color对象
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Color ToColorFromHtml(this string s)
        {
            return System.Drawing.ColorTranslator.FromHtml(s);
        }

        /// <summary>
        /// 将Color对象转为String对象。如#AABBCC这种
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ToFromHtml(this Color c)
        {
            return System.Drawing.ColorTranslator.ToHtml(c);
        }



        #endregion

        /// <summary>
        ///  验证Table数据dt != null && dt.Columns.Count != 0 && dt.Rows.Count != 0
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            if (dt != null && dt.Columns.Count != 0 && dt.Rows.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string ToStringY(this DateTime dt)
        {

            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static bool GetIListIsNull(IList<object> list)
        {
            if (list == null || list.Count == 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 强制转化为日期，不符合时转为现在时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object obj)
        {
            DateTime res = DateTime.Now;

            if (obj == null)
            {
                res = DateTime.Now;
            }
            else
            {
                try
                {
                    res = Convert.ToDateTime(obj.ToString());
                }
                catch (Exception)
                {
                    res = DateTime.Now;
                }
            }
            return res;

        }
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
        public static int ToIntDefalt1(this object strGuid)
        {
            int res = 1;
            if (strGuid == null)
            {
                res = 1; ;
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
        public static Guid ToGuid(this string strGuid)
        {
            if (strGuid == null)
            {
                return Guid.Empty;
            }
            else
            {
                return new Guid(strGuid);
            }

        }
        /// <summary>
        /// 判断字符串时候空或者null 或者GUID EMPTY
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static bool IsNullOrEmptyOrGuidEmpty(this object strGuid)
        {
            if (strGuid == null)
            {
                return true;
            }
            if (strGuid.ToString() == string.Empty)
            {
                return true;
            }
            if (strGuid.ToString() == Guid.Empty.ToString())
            {
                return true;
            }
            return false;
        }

        public static Guid ToGuid(this object strGuid)
        {
            if (strGuid == null)
            {
                return Guid.Empty;
            }
            else
            {
                return new Guid(strGuid.ToString());
            }

        }
        public static string ToStringUpper(this object obj)
        {
            if (obj != null)
            {
                return obj.ToString().ToUpper();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToStringLower(this object obj)
        {
            if (obj != null)
            {
                return obj.ToString().ToLower();
            }
            else
            {
                return string.Empty;
            }
        }

        public static string ToStringUpper(this Guid strGuid)
        {
            return strGuid.ToString().ToUpper();
        }
        public static string ToStringUpper(this Guid? strGuid)
        {
            if (strGuid != null)
            {
                return strGuid.Value.ToString().ToUpper();
            }
            else
            {
                return string.Empty;
            }

        }
        public static Guid? ToGuidNullable(this string strGuid)
        {
            if (string.IsNullOrEmpty(strGuid))
            {
                return null;
            }
            else
            {
                return new Guid(strGuid);
            }
        }



        ///// <summary>
        ///// List集合转String
        ///// </summary>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //public static string ToStringFromList(this List<string> values)
        //{
        //    string stringValue = string.Empty;
        //    foreach (string val in values)
        //    {
        //        stringValue += val + ConstString.SEPARATE_COMMA;
        //    }
        //    if (stringValue != string.Empty)
        //    {
        //        stringValue = stringValue.Substring(0, stringValue.Length - 1);
        //    }
        //    return stringValue;
        //}

        /// <summary>
        ///String转List集合
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static List<string> ToListFromString(this string values)
        {
            List<string> list = new List<string>(values.Split(','));
            return list;
        }
    }
}

