using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adims_Utility
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 根据数字顺序替换字母--- 123-->abc
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string ReplaceNumToLetter(int index)
        {

            string[] arr = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            if (index < 26)
            {
                return arr[index];
            }
            else
            {
                return "$";
            }


        }

        /// <summary>
        /// 按长度分割字符串，汉字按一个字符算
        /// </summary>
        /// <param name="SourceString"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static ArrayList SplitLength(string SourceString, int Length)
        {
            //List<string> DestString = new List<string>();
            ArrayList list = new ArrayList();
            for (int i = 0; i < SourceString.Trim().Length; i += Length)
            {
                if ((SourceString.Trim().Length - i) >= Length)
                    list.Add(SourceString.Trim().Substring(i, Length));
                else
                    list.Add(SourceString.Trim().Substring(i, SourceString.Trim().Length - i));
            }
            return list;
        }
    }
}
