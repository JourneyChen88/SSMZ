using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace adims_BLL
{
    public class ValidationRegex
    {
        public static string pattern; //正则表达式字符串

        /// <summary>
        /// 公共方法
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>Bool</returns>
        private static bool PublicMethod(string pattern, string validteString)
        {
            Regex reg = new Regex(pattern);
            Match m = reg.Match(validteString);
            return m.Success;
        }

        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidteData(string validteString)
        {
            if (string.IsNullOrEmpty(validteString)) return true;
            pattern = "^[+]?\\d+$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证浮点数
        /// </summary>
        /// <param name="validteString"></param>
        /// <returns></returns>
        public static bool ValidteDouble(string validteString)
        {
            if (string.IsNullOrEmpty(validteString)) return true;
            pattern = "^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证正负整数
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidatePlusMinus(string validteString)
        {
            pattern = "^([-+]?\\d+)(\\.\\d+)?$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证字母和数字
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool VadidateDataLetter(string validteString)
        {
            pattern = "^[a-zA-Z0-9]+$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证中文字符
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidateChineseChar(string validteString)
        {
            pattern = "^[\u4e00-\u9fa5]+$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证时间-【H:mm:ss】
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidateTime(string validteString)
        {
            pattern = "^[0-9]{1,2}:[0-9]{2}(:[0-9]{2})?$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证固定电话
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidateTel(string validteString)
        {
            pattern = "^([0-9]{3,4}-)?[0-9]{7,8}$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidatePhone(string validteString)
        {
            pattern = "^(13|15|18)[0-9]{9}$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidateEmail(string validteString)
        {
            pattern = "^\\w+([+-.]\\w+)*@\\w+([-.]\\w+)*$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        /// 验证邮编
        /// </summary>
        /// <param name="validteString">待验证字符串</param>
        /// <returns>bool</returns>
        public static bool ValidatePostCode(string validteString)
        {
            pattern = "^\\d{6}$";
            return PublicMethod(pattern, validteString);
        }

        /// <summary>
        ///   验证输入字符串为网络地址
        /// </summary>
        /// <param name="P_str_naddress"> 输入字符串 </param>
        /// <returns> 返回一个bool类型的值 </returns>
        public bool validateNAddress(string P_str_naddress)
        {
            return Regex.IsMatch(P_str_naddress, @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

    }
}
