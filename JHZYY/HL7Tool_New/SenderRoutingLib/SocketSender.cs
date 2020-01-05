using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace SenderRoutingLib
{
    public class SocketSender
    {
        public string Send(string msg, string Ip, int port)
        {
            string ackContet = string.Empty;
            string content = MediII.Net.Common.MLLPHelper.AddMLLP(ReplaceNewLine(msg));
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] readByte = new byte[SocketClientInfo.BufferSize];
            try
            {
                client.Connect(Ip, port);
                //MediII.Common.LogHelper.LogInfo("Socket连接成功");
                client.Send(bytes);
                //MediII.Common.LogHelper.LogInfo(string.Format("消息发送:{0}", content));
                client.Receive(readByte);
                string ack = System.Text.Encoding.UTF8.GetString(readByte);
                ackContet = MediII.Net.Common.MLLPHelper.TrimMLLP(ack);
                //MediII.Common.LogHelper.LogInfo(string.Format("消息接收:{0}", ack));
            }
            catch (SocketException ex)
            {
                return ex.Message;
            }
            finally
            {
                if (client.Connected)
                {
                    client.Shutdown(System.Net.Sockets.SocketShutdown.Send);
                    client.Close();
                }
            }
            return ackContet;
        }
        public string SendMsg(string msg)
        {
            string ackContet = string.Empty;
            string content = MediII.Net.Common.MLLPHelper.AddMLLP(ReplaceNewLine(msg));
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(content);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            byte[] readByte = new byte[SocketClientInfo.BufferSize];
            try
            {
                client.Connect(SocketClientInfo.Address, SocketClientInfo.Port);
                //MediII.Common.LogHelper.LogInfo("Socket连接成功");
                client.Send(bytes);
                //MediII.Common.LogHelper.LogInfo(string.Format("消息发送:{0}", content));
                client.Receive(readByte);
                string ack = System.Text.Encoding.UTF8.GetString(readByte);
                ackContet = MediII.Net.Common.MLLPHelper.TrimMLLP(ack.Trim());
                //MediII.Common.LogHelper.LogInfo(string.Format("消息接收:{0}", ack));
            }
            catch (SocketException ex)
            {
                return ex.Message;
            }
            finally
            {
                if (client.Connected)
                {
                    client.Shutdown(System.Net.Sockets.SocketShutdown.Send);
                    client.Close();
                }
            }
            return ackContet;
        }

        string ReplaceNewLine(string oleString)
        {
            char[] strArr = oleString.ToCharArray();
            StringBuilder sb = new StringBuilder(oleString.Length);
            foreach (char cr in strArr)
            {
                if (cr == (char)10)
                    sb.Append((char)13);
                else
                    sb.Append(cr);
            }
            return sb.ToString();
        }
    }
}
