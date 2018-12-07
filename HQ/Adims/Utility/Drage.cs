using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.Ports;
namespace adims_Utility
{
   public  class Drage
    {
       public SerialPort p=new SerialPort();
        
        List<adims_MODEL.point> Spo2;
        public Drage(List<adims_MODEL.point> spo2)
        {
            p.BaudRate = 19200;
            p.DtrEnable = true;
            p.RtsEnable = true;
            p.DataReceived += new SerialDataReceivedEventHandler(p_DataReceived);
            Spo2 = spo2;
           
        }

        void p_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(100);
            // ser.MonitorRecord MonitorRecord = new ser.MonitorRecord();
            int spo2 = 0, pulse = 0, nibps = 0, nibpd = 0, nibpm = 0, arts = 0, artd = 0, artm = 0, hr = 0, ico2 = 0, rrc = 0;
            double etco2 = 0;
            int fff = 0;

            byte[] q = new byte[500];
            p.Read(q, 0, p.BytesToRead);
            /*   foreach (byte w in q)
            {
                // textBox1.Text = textBox1.Text + ((char)w).T1oString();
                s = s + ((int)w).ToString()+" ";    
               }
             */
            //textBox3.Text = s;
            for (int i = 0; i < q.Length - 3; i++)
            {
                if (q[i] == 1 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    //MonitorRecord.HRValue = m;
                    hr = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "hr:" + m.ToString();
                }

                if (q[i] == 100 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.SpO2Value = m;
                    spo2 = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "Spo2:" + m.ToString();
                }
                if (q[i] == 101 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.PulseValue = m;
                    pulse = m;
                    fff++;
                    //  textBox4.Text = textBox4.Text + "Pulse:" + m.ToString();
                }
                if (q[i] == 91 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.NIBPSValue = m;
                    nibps = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "NIBPS:" + m.ToString();
                }
                if (q[i] == 92 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    //MonitorRecord.NIBPDValue = m;
                    nibpd = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "NIBPD:" + m.ToString();
                }
                if (q[i] == 93 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.NIBPMValue = m;
                    nibpm = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "NIBPM:" + m.ToString();
                }
                if (q[i] == 32 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    arts = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "IBP1S:" + m.ToString();
                }
                if (q[i] == 33 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    artd = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "IBP1D:" + m.ToString();
                }
                if (q[i] == 34 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    artm = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "IBP1M:" + m.ToString();
                }
                if (q[i] == 103 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; double m = 0, n = 10;
                    while (q[j] != 0 && q[j] != 46)
                    { m = m * 10 + q[j] - 48; j++; }
                    if (q[j] == 46)
                    {
                        j++;
                        while (q[j] != 0)
                        {
                            m = m + ((double)q[j]) / n;
                            n = n * 10;
                            j++;
                        }
                    }
                    i = j;
                    // MonitorRecord.ETCO2Value = m;
                    etco2 = m;
                    fff++;
                    //textBox4.Text = textBox4.Text + "ETCO2:" + m.ToString();
                }
                if (q[i] == 105 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.ICO2Value = m;
                    ico2 = m;
                    fff++;

                    //textBox4.Text = textBox4.Text + "ICO2:" + m.ToString();
                }
                if (q[i] == 104 && q[i + 1] == 0 && q[i + 2] != 0 && q[i + 3] > 47 && q[i + 3] < 58)
                {
                    int j = i + 2; int m = 0;
                    while (q[j] != 0)
                    { m = m * 10 + q[j] - 48; j++; }
                    i = j;
                    // MonitorRecord.RRCValue = m;
                    rrc = m;
                    fff++;
                    // textBox4.Text = textBox4.Text + "RRC:" + m.ToString();
                }
            }
            if (fff > 0)
            {
                adims_MODEL.point point1 = new adims_MODEL.point();
                point1.D = DateTime.Now;
                point1.Lx = 1;
                point1.V = spo2;
                Spo2.Add(point1);
                /* MonitorRecord.MonitorCode = "test75";
                 webser.SendRawData(MonitorRecord);
                 */
        /*        cmd.CommandText = "insert into MonitorRecord(HR,SpO2,Pulse,NIBPS,NIBPD,NIBPM,ARTS,ARTD,ARTM,ETCO2,ICO2,RRC,RecordTime) values(" + hr + "," + spo2 + "," + pulse + "," + nibps + "," + nibpd + "," + nibpm + "," + arts + "," + artd + "," + artm + "," + etco2 + "," + ico2 + "," + rrc + ",'" + DateTime.Now + "')";
                int fa = cmd.ExecuteNonQuery();
                if (fa != 1)
                { MessageBox.Show("错误"); }
         */
            }

        }


    }
}
