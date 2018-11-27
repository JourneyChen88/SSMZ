using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SenderRoutingWin
{
    public class KeyValue
    {
        public string key { get; set; }
        public string value { get; set; }

        public KeyValue(string Key,string Value)
        {
            key = Key;
            value = Value;
        }
    }
}
