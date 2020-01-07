using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;



namespace ListenerRoutingLib
{
    /// <summary>
    /// HL7解析器
    /// </summary>
    public static class HL7ToXmlConverter
    {
        private static XmlDocument _xmlDoc;

        /// <summary>
        /// 把HL7信息转成XML形式
        /// 分隔顺序 \n,|,~,^,&
        /// </summary>
        /// <param name="sHL7">HL7字符串</param>
        /// <returns></returns>
        public static string ConvertToXml(string sHL7)
        {
            _xmlDoc = ConvertToXmlObject(sHL7);
            return _xmlDoc.OuterXml;
        }
        public static OperDicModel ToOperDic(string message)
        {
            //把HL7分成段
            if (message.Contains("Z8A|"))
            {
                message = message.Replace("Z8A|", "\nZ8A|");
            }
            string[] sHL7Lines = message.Split('\n');
            OperDicModel pb = new OperDicModel();

            int lastIndex = sHL7Lines.Length - 1;
            // 判断开始
            if (sHL7Lines[lastIndex].StartsWith("Z8A|"))
            {
                string[] sList = sHL7Lines[lastIndex].Split('|');
                //string[] nameList = sList[18].Split('^');
                pb.OperCode = sList[1];
                pb.OperName = sList[2];
                pb.Operlevel = sList[4].Split('^')[1];
                string[] quickList = sList[6].Split('~');
                pb.QuickInput = quickList[0].Split('^')[1];
                pb.CutType = sList[19];
            }
            #region HIS平台提供信息解析
            //for (int i = 0; i < sHL7Lines.Length; i++)
            //{
            //    // 判断开始
            //    if (sHL7Lines[i].StartsWith("Z8A|"))
            //    {
            //        string[] sList = sHL7Lines[i].Split('|');
            //        string[] nameList = sList[18].Split('^');
            //        pb.OperCode = nameList[0];
            //        pb.OperName = nameList[1];
            //        pb.Operlevel = sList[4].Split('^')[1];
            //        string[] quickList = sList[6].Split('~');
            //        pb.QuickInput = quickList[0].Split('^')[1];
            //        pb.CutType = sList[19];


            //    }

            //}

            #endregion



            return pb;
        }
        public static OTypesetting toDataBae(string sHL7)
        {
            //把HL7分成段

            string Old = sHL7;
            if (Old.Contains("ARQ|"))
            {
                Old = Old.Replace("ARQ|", "\nARQ|");
            }
            if (Old.Contains("PID|"))
            {
                Old = Old.Replace("PID|", "\nPID|");
            }
            if (Old.Contains("PV1|"))
            {
                Old = Old.Replace("PV1|", "\nPV1|");
            }
            if (Old.Contains("AIS|"))
            {
                Old = Old.Replace("AIS|", "\nAIS|");
            }
            if (Old.Contains("OBX|1|"))
            {
                Old = Old.Replace("OBX|1|", "\nOBX|1|");
            }
            if (Old.Contains("OBX|2|"))
            {
                Old = Old.Replace("OBX|2|", "\nOBX|2|");
            }
            if (Old.Contains("AIP|"))
            {
                Old = Old.Replace("AIP|", "\nAIP|");
            }
            if (Old.Contains("DG1|"))
            {
                Old = Old.Replace("DG1|", "\nDG1|");
            }
            if (Old.Contains("RGS|"))
            {
                Old = Old.Replace("RGS|", "\nRGS|");
            }

            string[] sHL7Lines = Old.Split('\n');
            OTypesetting pb = new OTypesetting();

            #region HIS平台提供信息解析
            for (int i = 0; i < sHL7Lines.Length; i++)
            {

                // 判断是否空行
                if (sHL7Lines[i].Contains("ARQ|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    pb.PatID = sList[1].Replace("^", "");
                    string[] dateList = sList[11].Split('^');
                    DateTime dtOdate = DateTime.ParseExact(dateList[0].Replace("^", "").Trim().Substring(0, 8), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                    pb.Odate = dtOdate;
                    DateTime dtApplyDate = DateTime.ParseExact(dateList[1].Replace("^", ""), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    pb.ApplyDate = dtApplyDate;
                    string[] nameList = sList[5].Split('^');
                    if (nameList.Length>1)
                    {
                        pb.Amethod = nameList[1];
                    }
                   

                }
                if (sHL7Lines[i].Contains("PID|"))
                {
                    pb.PidInfo = sHL7Lines[i];
                    string[] sList = sHL7Lines[i].Split('|');
                   // pb.PatientNo = sList[2].Split('^')[0];
                    string[] idList = sList[3].Split('~');
                    pb.PatID = idList[0].Split('^')[0];
                    pb.PatZhuYuanID = idList[1].Split('^')[0];
                    pb.CardID = idList[2].Split('^')[0];

                    string[] nameList = sList[5].Split('^');
                    pb.Patname = nameList[1];

                    string str = sList[7].Trim().Substring(0, 8);
                    DateTime now = DateTime.Today;
                    DateTime bday = DateTime.ParseExact(str, "yyyyMMdd", null);
                    int age = now.Year - bday.Year;
                    pb.Patage = age.ToString();
                    string[] sexList = sList[8].Split('^');
                    pb.Patsex = sexList[0];
                    if (pb.Patsex == "M")
                    {
                        pb.Patsex = "男";
                    }
                    else
                    {
                        pb.Patsex = "女";
                    }
                    //pb.patMinZu = sList[22].Split('^')[1];

                }

                if (sHL7Lines[i].Contains("OBX|1|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    pb.PatWeight = sList[5];
                }

                if (sHL7Lines[i].Contains("OBX|2|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    pb.PatHeight = sList[5];
                }
                if (sHL7Lines[i].Contains("PV1|"))
                {
                    pb.Pv1Info = sHL7Lines[i];
                    string[] sList = sHL7Lines[i].Split('|');
                    //if (sList[2] == "I")
                    //{
                    //    pb.IsZhuYuan = "1";
                    //}
                    //else if (sList[2] == "O")
                    //{
                    //    pb.IsZhuYuan = "0";
                    //}
                    //else
                    //{
                    //    pb.IsZhuYuan = "0";
                    //    pb.asae = 1;
                    //}

                    string[] bedList = sList[3].Split('^');
                    pb.Patbedno = bedList[4];
                    string[] dpmList = bedList[3].Split('&');

                    pb.Patdpm = dpmList[1];
               
                    //pb.applyID = sList[19];



                }
                if (sHL7Lines[i].Contains("DG1|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    pb.Pattmd = sList[4];
                }
                if (sHL7Lines[i].Contains("AIS|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    string[] nameList = sList[3].Split('^');
                    //pb.OperNo = nameList[0];
                    pb.Oname = nameList[1];
                }

                if (sHL7Lines[i].Contains("AIP|1|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    string[] osList = sList[3].Split('^');
                    try
                    {
                        //pb.OsNo = osList[0];
                        pb.OS = osList[2];
                    }
                    catch (Exception)
                    {
                       // pb.OsNo = string.Empty;
                        pb.OS = string.Empty;
                    }
                   
                }
                if (sHL7Lines[i].Contains("AIP|7|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    string[] osList = sList[3].Split('^');
                    try
                    {
                        //pb.OS1 = osList[0];
                        pb.OS1 = osList[2];
                    }
                    catch (Exception)
                    {
                        //pb.OS1 = string.Empty;
                        pb.OS1 = string.Empty;
                    }


                }
                if (sHL7Lines[i].Contains("AIP|8|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    string[] osList = sList[3].Split('^');
                    try
                    {
                      //  pb.OS2 = osList[0];
                        pb.OS2 = osList[2];
                    }
                    catch (Exception)
                    {
                        //pb.OS2 = string.Empty;
                        pb.OS2 = string.Empty;
                    }
                }

                if (sHL7Lines[i].Contains("AIP|9|"))
                {
                    string[] sList = sHL7Lines[i].Split('|');
                    string[] osList = sList[3].Split('^');
                    try
                    {
                       // pb.OS3 = osList[0];
                        pb.OS3 = osList[2];
                    }
                    catch (Exception)
                    {
                        //pb.OS3 = string.Empty;
                        pb.OS3 = string.Empty;
                    }

                }

            }
            #endregion



            return pb;
        }
        public static XmlDocument ConvertToXmlObject(string sHL7)
        {
            _xmlDoc = CreateXmlDoc();

            //把HL7分成段
            string[] sHL7Lines = sHL7.Split('\n');


            //去掉XML的关键字
            for (int i = 0; i < sHL7Lines.Length; i++)
            {
                sHL7Lines[i] = Regex.Replace(sHL7Lines[i], @"[^-~]", "");
            }

            for (int i = 0; i < sHL7Lines.Length; i++)
            {

                // 判断是否空行
                if (sHL7Lines[i].Contains("ARQ|"))
                {
                    string sHL7Line = sHL7Lines[i];

                    //通过/r 或/n 回车符分隔
                    string[] sFields = HL7ToXmlConverter.GetMessgeFields(sHL7Line);

                    // 为段（一行）创建第一级节点
                    XmlElement el = _xmlDoc.CreateElement(sFields[0]);
                    _xmlDoc.DocumentElement.AppendChild(el);

                    // 循环每一行
                    for (int a = 0; a < sFields.Length; a++)
                    {
                        // 为字段创建第二级节点
                        XmlElement fieldEl = _xmlDoc.CreateElement(sFields[0] + "." + a.ToString());

                        //是否包括HL7的连接符
                        if (sFields[a] != @"^~\&")
                        {//0:如果这一行有任何分隔符



                            //通过~分隔
                            string[] sComponents = HL7ToXmlConverter.GetRepetitions(sFields[a]);
                            if (sComponents.Length > 1)
                            {//1:如果可以分隔
                                for (int b = 0; b < sComponents.Length; b++)
                                {
                                    XmlElement componentEl = _xmlDoc.CreateElement(sFields[0] + "." + a.ToString() + "." + b.ToString());

                                    //通过&分隔 
                                    string[] subComponents = GetSubComponents(sComponents[b]);
                                    if (subComponents.Length > 1)
                                    {//2.如果有字组，一般是没有的。。。
                                        for (int c = 0; c < subComponents.Length; c++)
                                        {
                                            //修改了一个错误
                                            string[] subComponentRepetitions = GetComponents(subComponents[c]);
                                            if (subComponentRepetitions.Length > 1)
                                            {
                                                for (int d = 0; d < subComponentRepetitions.Length; d++)
                                                {
                                                    XmlElement subComponentRepEl = _xmlDoc.CreateElement(sFields[0] + "." + a.ToString() + "." + b.ToString() + "." + c.ToString() + "." + d.ToString());
                                                    subComponentRepEl.InnerText = subComponentRepetitions[d];
                                                    componentEl.AppendChild(subComponentRepEl);
                                                }
                                            }
                                            else
                                            {
                                                XmlElement subComponentEl = _xmlDoc.CreateElement(sFields[0] + "." + a.ToString() + "." + b.ToString() + "." + c.ToString());
                                                subComponentEl.InnerText = subComponents[c];
                                                componentEl.AppendChild(subComponentEl);

                                            }
                                        }
                                        fieldEl.AppendChild(componentEl);
                                    }
                                    else
                                    {//2.如果没有字组了，一般是没有的。。。
                                        string[] sRepetitions = HL7ToXmlConverter.GetComponents(sComponents[b]);
                                        if (sRepetitions.Length > 1)
                                        {
                                            XmlElement repetitionEl = null;
                                            for (int c = 0; c < sRepetitions.Length; c++)
                                            {
                                                repetitionEl = _xmlDoc.CreateElement(sFields[0] + "." + a.ToString() + "." + b.ToString() + "." + c.ToString());
                                                repetitionEl.InnerText = sRepetitions[c];
                                                componentEl.AppendChild(repetitionEl);
                                            }
                                            fieldEl.AppendChild(componentEl);
                                            el.AppendChild(fieldEl);
                                        }
                                        else
                                        {
                                            componentEl.InnerText = sComponents[b];
                                            fieldEl.AppendChild(componentEl);
                                            el.AppendChild(fieldEl);
                                        }
                                    }
                                }
                                el.AppendChild(fieldEl);
                            }
                            else
                            {//1:如果不可以分隔，可以直接写节点值了。
                                fieldEl.InnerText = sFields[a];
                                el.AppendChild(fieldEl);
                            }

                        }
                        else
                        {//0:如果不可以分隔，可以直接写节点值了。
                            fieldEl.InnerText = sFields[a];
                            el.AppendChild(fieldEl);
                        }
                    }
                }
            }



            return _xmlDoc;
        }

        /// <summary>
        /// 通过|分隔 字段
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetMessgeFields(string s)
        {
            return s.Split('|');
        }

        /// <summary>
        /// 通过^分隔 组字段
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetComponents(string s)
        {
            return s.Split('^');
        }

        /// <summary>
        /// 通过&分隔 子分组组字段
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetSubComponents(string s)
        {
            return s.Split('&');
        }

        /// <summary>
        /// 通过~分隔 重复
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string[] GetRepetitions(string s)
        {
            return s.Split('~');
        }

        /// <summary>
        /// 创建XML对象
        /// </summary>
        /// <returns></returns>
        private static XmlDocument CreateXmlDoc()
        {
            XmlDocument output = new XmlDocument();
            XmlElement rootNode = output.CreateElement("HL7Message");
            output.AppendChild(rootNode);
            return output;
        }

        public static string GetText(XmlDocument xmlObject, string path)
        {
            XmlNode node = xmlObject.DocumentElement.SelectSingleNode(path);
            if (node != null)
            {
                return node.InnerText;
            }
            else
            {
                return null;
            }
        }

        public static string GetText(XmlDocument xmlObject, string path, int index)
        {
            XmlNodeList nodes = xmlObject.DocumentElement.SelectNodes(path);
            if (index <= nodes.Count)
            {
                return nodes[index].InnerText;
            }
            else
            {
                return null;
            }


        }

        public static String[] GetTexts(XmlDocument xmlObject, string path)
        {
            XmlNodeList nodes = xmlObject.DocumentElement.SelectNodes(path);
            String[] arr = new String[nodes.Count];
            int index = 0;
            foreach (XmlNode node in nodes)
            {
                arr[index++] = node.InnerText;
            }
            return arr;

        }

    }

}
