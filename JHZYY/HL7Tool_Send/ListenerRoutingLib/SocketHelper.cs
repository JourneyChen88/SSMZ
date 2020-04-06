using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using MediII.Common;

namespace MediII.Adapter.ListenerRouting
{
    public class SocketHelper
    {
        public class StateObject
        {
            public Socket workSocket = null;

            public byte[] buffer = null;
 
            public StringBuilder sb = new StringBuilder();
            public List<byte> list =new List<byte>();
            public int offset = 0;
        }

        private static ManualResetEvent Done = new ManualResetEvent(false);

        /// <summary>
        /// 监听配置
        /// </summary>
        public static ListenerInfo ConfigInfo { get; set; }

        /// <summary>
        /// Socke连接
        /// </summary>
        private static Socket serverSocket = null;

        public static bool IsRuning
        {
            get
            {
                return serverSocket == null ? false :
                    serverSocket.LingerState.Enabled;
            }
        }

        /// <summary>
        /// 启动监听
        /// </summary>
        public static void BeginListen()
        {
            IPAddress localAddress = IPAddress.Any;
            SocketType sockType = SocketType.Stream;
            ProtocolType sockProtocol = ProtocolType.Tcp;

            try
            {
                IPEndPoint localEndPoint = new IPEndPoint(localAddress, ConfigInfo.LocalPort);
                serverSocket = new Socket(localAddress.AddressFamily, sockType, sockProtocol);

                serverSocket.Bind(localEndPoint);
                serverSocket.Listen(ConfigInfo.MaxConnections);

                while (true)
                {
                    //侦听到一个新传入的连接
                    Done.Reset();//将状态设为非终止
                    serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), serverSocket);
                    Done.WaitOne();
                }
            }
            catch (SocketException ex)
            {
                //LogHelper.LogError(ex, LogCatagories.Sock);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex, LogCatagories.Sock);
            }
        }

        static void AcceptCallBack(IAsyncResult ar)//ar表示异步操作的状态。
        {
            Socket socket;    //用于接受来自客户端的请求连接
            Socket handler = null;
            try
            {
                Done.Set();//设为终止
                socket = (Socket)ar.AsyncState; //获取状态
                handler = socket.EndAccept(ar);

                StateObject stateObject = new StateObject();
                stateObject.buffer = new byte[ConfigInfo.BufferSize];
                stateObject.workSocket = handler;
                handler.BeginReceive(stateObject.buffer, 0, ConfigInfo.BufferSize, 0, new AsyncCallback(ReceiveCallBack), stateObject);
            }
            catch (SocketException ex)
            {
                //LogHelper.LogError(ex, LogCatagories.Sock);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex, LogCatagories.Sock);
            }
        }

        static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket handler = null;
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                handler = state.workSocket;
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    for (int i = 0; i < bytesRead; i++)
                        state.list.Add(state.buffer[i]);

                    string content = Encoding.UTF8.GetString(state.buffer, 0, bytesRead);
                    string endString = new string((char)28, 1) + new string((char)13, 1);
                    if (content.IndexOf(endString) > -1 || (content == new string((char)13, 1)))
                    {
                        if (MessageReviced != null)
                        {
                            byte[] bs = state.list.ToArray();
                            MessageReviced(null, new MessageRevicedEventArgs()
                            {
                                Contents = bs,
                                SocketHandler = handler
                            });
                        };
                    }
                    else
                    {
                        // Not all data received. Get more.
                        handler.BeginReceive(state.buffer, 0, ConfigInfo.BufferSize, 0,
                        new AsyncCallback(ReceiveCallBack), state);
                    }
                }
                else
                {
                    //LogHelper.LogError("没有接受到数据", LogCatagories.Sock);
                }
            }
            catch (SocketException ex)
            {
                //LogHelper.LogError(ex, LogCatagories.Sock);
            }
            catch (Exception ex)
            {
                //LogHelper.LogError(ex, LogCatagories.Sock);
            }
        }

        /// <summary>
        /// 停止监听
        /// </summary>
        public static void StopListen()
        {
            if (serverSocket.LingerState.Enabled)
            {
                serverSocket.Shutdown(SocketShutdown.Both);
            }
            serverSocket.Close();
            serverSocket = null;
        }

        /// <summary>
        /// 回复
        /// </summary>
        public static void SendAck(string ackMessage, Socket handler)
        {
            try
            {
                if (handler.Connected)
                {
                    Byte[] byteSend = Encoding.UTF8.GetBytes(ackMessage);
                    handler.SendTimeout = 6000;
                    handler.BeginSend(byteSend, 0, byteSend.Length, 0, new AsyncCallback(SendCallBack), handler);

                }
                else
                {
                    LogHelper.LogError(string.Format("连接已关闭,回复消息内容是:{0}",ackMessage), LogCatagories.Sock);
                }
            }
            catch (SocketException ex)
            {
                LogHelper.LogError(ex, LogCatagories.Sock);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex, LogCatagories.Sock);
            }
        }

        static void SendCallBack(IAsyncResult ar)
        {
            Socket handler = (Socket)ar.AsyncState;
            try
            {
                handler.EndSend(ar);
            }
            catch (SocketException ex)
            {
                LogHelper.LogError(ex, LogCatagories.Sock);
            }
            finally
            {
                handler.Shutdown(SocketShutdown.Send);
                //LingerOption option = new LingerOption(true, 60);
                //handler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, true);
                //handler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.NoDelay, true);
                handler.Close();
            }
        }

        /// <summary>
        /// 消息到达事件
        /// </summary>
        public static event EventHandler<MessageRevicedEventArgs> MessageReviced;
    }
}
