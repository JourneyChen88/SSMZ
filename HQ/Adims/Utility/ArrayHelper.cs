using System;
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

            string[] arr = new string[] {  "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            if (index < 26)
            {
                return arr[index];
            }
            else
            {
                return "$";
            }


        }
    }
}
