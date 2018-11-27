using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace code1111
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BarCode.BarCodeEvent += new BardCodeHooK.BardCodeDeletegate(BarCode_BarCodeEvent);

        }
        BardCodeHooK BarCode = new BardCodeHooK();


        private void Form1_Load(object sender, EventArgs e)
        {
            BarCode.Start();
        }
        private delegate void ShowInfoDelegate(BardCodeHooK.BarCodes barCode);
        private void ShowInfo(BardCodeHooK.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate(ShowInfo), new object[] { barCode });
            }
            else
            {               
                textBox1.Text = barCode.KeyName;
                
                
                textBox2.Text = barCode.VirtKey.ToString();
                textBox3.Text = barCode.ScanCode.ToString();
                textBox4.Text = barCode.Ascll.ToString();

               // textBox6.Text = barCode.IsValid ? barCode.BarCode : "";//是否为扫描枪输入，如果为true则是 否则为键盘输入


                label7.Text += barCode.Chr;

                
                
                //textBox6.Text = "";
                //MessageBox.Show(barCode.IsValid.ToString());
               
            }

        }

        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }
        //C#中判断扫描枪输入与键盘输入 

        private DateTime _dt = DateTime.Now;  //定义一个成员函数用于保存每次的时间点
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            DateTime tempDt = DateTime.Now;          //保存按键按下时刻的时间点
            TimeSpan ts = tempDt.Subtract(_dt);     //获取时间间隔
            if (ts.Milliseconds > 50)                           //判断时间间隔，如果时间间隔大于50毫秒，则将TextBox清空
                textBox5.Text = "";
            _dt = tempDt;
        }

        

        void BarCode_BarCodeEvent(BardCodeHooK.BarCodes barCode)
        {
            
            ShowInfo(barCode);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
           
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            BarCode.Stop();
        }

      

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length > 0)
            {
                MessageBox.Show("条码长度：" + textBox6.Text.Length + "\n条码内容：" + textBox6.Text, "系统提示");
            }
        }

        private void textBox6_TextChanged_1(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox6.Text);   
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            BardCodeHooK.BarCodes barCode = new BardCodeHooK.BarCodes() ;
            ShowInfo(barCode);
        }

     

       


    }
}
