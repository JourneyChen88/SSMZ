using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediII.Net.Common
{
    public class MLLPHelper
    {
       public const char c1 = (char)11;   //  VT,数据块结束字符
       const char c2 = (char)28;   //  EB,FS,数据块结束字符
       public const char c3 = (char)13;   //  CR,数据块结束字符

        //头
        public static string LEADING = new string(c1, 1);
        //尾
        public static string TAILING = new string(c2, 1) + new string(c3, 1);

        public static string AddMLLP(string source)
        {
            return LEADING + source + TAILING;
        }

        public static string TrimMLLP(string source)
        {
            try
            {
                return source.Substring(1, source.IndexOf(TAILING));
            }
            catch
            {
                return source;
            }
        }
    }
}
