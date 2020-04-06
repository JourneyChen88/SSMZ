using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SenderRoutingWin
{
    public class AnalysisXML
    {
        public static List<MessageNode> Analysis(string FilePath)
        {
            List<MessageNode> messageList = null;
            try
            {
                messageList = new List<MessageNode>();
                if (File.Exists(FilePath))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(FilePath);
                    XmlElement element = doc.DocumentElement;
                    foreach (XmlNode xn in element.ChildNodes)
                    {
                        if (xn.HasChildNodes)
                        {
                            MessageNode msgNode = new MessageNode();
                            foreach (XmlNode _xn in xn.ChildNodes)
                            {
                                if (_xn.HasChildNodes)
                                {
                                    List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
                                    Dictionary<string, string> dic = new Dictionary<string, string>();
                                    AnalysisNode(_xn, dic);
                                    switch (_xn.Name)
                                    {
                                        case "PID":
                                            msgNode.PIDList = dic;
                                            break;
                                        case "PV1":
                                            msgNode.PV1List = dic;
                                            break;
                                        case "ORC":
                                            msgNode.ORCList = new List<Dictionary<string, string>>();
                                            msgNode.ORCList.Add(dic);
                                            break;
                                        case "OBR":
                                            msgNode.OBRList = new List<Dictionary<string, string>>();
                                            msgNode.OBRList.Add(dic);
                                            break;
                                    }
                                }
                            }
                            messageList.Add(msgNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //捕捉错误  
            }
            return messageList;
        }

        private static void AnalysisNode(XmlNode node, Dictionary<string, string> dic)
        {
            foreach (XmlNode xn in node.ChildNodes)
            {
                if (xn.HasChildNodes)
                {
                    AnalysisNode(xn, dic);
                }
                else
                {
                    dic[xn.ParentNode.Name] = xn.InnerText;
                }
            }
        }
    }
}
