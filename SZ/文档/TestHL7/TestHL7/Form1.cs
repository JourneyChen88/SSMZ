using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace TestHL7
{
    public partial class Form1 : Form
    {
        private ThreadStart start;
        private Thread listenThread;
        static private bool m_bListening = false;
        private System.Net.IPAddress MyIP;
        private TcpListener listener;
        private String msg;
        //初始化监听器
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            start = new ThreadStart(startListen);
            listenThread = new Thread(start);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string ValidateDataFormatString2(object value, double decimalshift, bool rounddata)
        {
            int val = Convert.ToInt32(value);
            double dval = (Convert.ToDouble(value)) * decimalshift;
            if (rounddata)
                dval = Math.Round(dval, 1);

            string str = dval.ToString();



            return str;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string title1 = @"MSH|^~\&|IIWeb|IIWeb|MediII|MediII|";
            string datetime1 = DateTime.Now.ToString("yyyyMMddHHmmss");
            string title2 = @"SRM^S01^SRM_S01|";
            //string llphl7message = Convert.ToChar(11).ToString() + hl7message + 
            //    Convert.ToChar(28).ToString() + Convert.ToChar(13).ToString();
            MessageBox.Show(Convert.ToChar(11).ToString());
            MessageBox.Show(Convert.ToChar(28).ToString());
            MessageBox.Show(Convert.ToChar(13).ToString());
            string temp = tbAdress.Text;
            
            SendHl7 send = new SendHl7();
            ////string strHL7="MSH|^~|IIWeb|IIWeb|MediII|MediII|20140120153141||MFN^Z8C^MFN_Z1A|b10bc8d45485433b9b08c961baf8db85|P|2.4MFI|Z8C||UPD|||ALMFE|MAD|D99E305CF3C700A2E0430AC70A0100A2||D99E305CF62A00A2E0430AC70A0100A2|STZ1A|21|介入手术|0|21|1|1|1^JRSS~2^WTRS~3^8824|DBA^^超级用户|20130405222358||0";
            //int port=int.Parse(tbPort.Text.Trim());
            //send.SendHL7(tbAdress.Text.Trim(),port,"fuck");



            //MyIP = System.Net.IPAddress.Parse(tbAdress.Text.Trim());
            //listener = new TcpListener(MyIP, int.Parse(tbPort.Text.Trim()));
            //if (m_bListening)
            //{
            //    m_bListening = false;
            //    //label4.Text = "Server Idle";
            //}
            //else
            //{
            //    m_bListening = true;
            //    listenThread.Start();
            //    // label4.Text = "Server Listening";
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (m_bListening)
            {
                m_bListening = false;
                listenThread.Abort();//强制结束线程运行
                listenThread = null; ////在.NET中，有GC垃圾回收，如果这里不赋为null，有可能老半天也不回收内存，甚至永远这么占着，导致内存泄漏。
            }
        }
        private void startListen()
        {
            //textBox1.Text = "listening";
            listener.Start();
            //接收数据
            while (m_bListening)
            {
                //测试是否有数据
                try
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream ns = client.GetStream();
                    //StreamReader sr = new StreamReader(ns);//流读写器
                    //字组处理
                    byte[] bytes = new byte[1024];
                    int bytesread = ns.Read(bytes, 0, bytes.Length);
                    msg = Encoding.Default.GetString(bytes, 0, bytesread);
                    //显示
                    //MessageBox.Show(msg);


                    ShowInfo();
                    ns.Close();
                    client.Close();
                    //清场

                }
                catch (Exception re)
                {
                    MessageBox.Show(re.Message);
                }

            }
            listener.Stop();
            //
        }
        private void ShowInfo()
        {
            rtbInfo.Text = msg;
        }

    }

}

