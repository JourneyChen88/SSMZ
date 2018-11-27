using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;
using WindowsFormsControlLibrary5;
namespace main
{
    public partial class showPaiBan : Form
    {
        DB2help db2 = new DB2help();
        AdimsController bll = new AdimsController();
        AdimsProvider DAL = new AdimsProvider();
        public showPaiBan()
        {
            InitializeComponent();

        }

        private void showPaiBan_Load(object sender, EventArgs e)
        {
            DataGridViewRow row = this.JSdgv.RowTemplate;
            row.Height = 75;
            string Minute;
            if (DateTime.Now.Minute<=9)
            {
                Minute = "0" + DateTime.Now.Minute;
            }
            else
            {
                Minute = "" + DateTime.Now.Minute;
            }
            this.label3.Text =  DateTime.Now.Year + "年" + DateTime.Now.Month + "月" + DateTime.Now.Day + "日 " + DateTime.Now.Hour + ":" + Minute + "           " + "请耐心等候...";
            paibanDataBind();
            this.timer1.Start();
            this.timer2.Start();
        }
        private void paibanDataBind()
        {
            DataTable dt = new DataTable();            
            dt = DAL.GetPAIBAN(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            JSdgv.DataSource = dt.DefaultView; 
        }
        /// <summary>
        /// 纵向滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            this.label3.Left -= 1;
            if (this.label3.Right < 0)
            {
                this.label3.Left = this.Width;
            }
            Application.DoEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
