using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SenderRoutingWin
{
    public class MessageNode
    {
        public Dictionary<string, string> PIDList { get; set; }
        public Dictionary<string, string> PV1List { get; set; }
        public List<Dictionary<string, string>> ORCList { get; set; }
        public List<Dictionary<string, string>> OBRList { get; set; }
        public MessageNode()
        {
        }
        public MessageNode(Dictionary<string, string> pidList, Dictionary<string, string> pv1List, List<Dictionary<string, string>> orcList, List<Dictionary<string, string>> obrList)
        {
            PIDList = pidList;
            PV1List = pv1List;
            ORCList = orcList;
            OBRList = obrList;
        }
    }
}