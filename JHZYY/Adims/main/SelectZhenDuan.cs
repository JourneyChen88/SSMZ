using Adims_Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class SelectZhenDuan : Form
    {
        adims_DAL.HisDB_Help hdal = new adims_DAL.HisDB_Help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        WindowsFormsControlLibrary5.UserControl1 u;
        string patid;
        int mzjldid;
        public SelectZhenDuan(WindowsFormsControlLibrary5.UserControl1 o, int mzid)
        {
            u = o;
            mzjldid = mzid;
            InitializeComponent();
        }

        public SelectZhenDuan(WindowsFormsControlLibrary5.UserControl1 o, string pid)
        {
            u = o;
            patid = pid;
            InitializeComponent();
        }
        public SelectZhenDuan(WindowsFormsControlLibrary5.UserControl1 o)
        {
            u = o;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0; string qs = "";
            if (listBox1.SelectedIndex != -1)
            {
                foreach (string s in listBox2.Items)
                {
                    if (s == listBox1.Items[listBox1.SelectedIndex].ToString())
                        i = 1;
                }
                if (i == 1)
                    MessageBox.Show("已添加");
                else
                {
                    listBox2.Items.Add(listBox1.Items[listBox1.SelectedIndex]);
                    foreach (string s in listBox2.Items)
                    {
                        if (qs.IsNullOrEmpty())
                        { qs = s; }
                        else
                            qs = qs + "+" + s;
                    }                    
                    u.Controls[0].Text = qs;
                }

            }
        }

        private void sqzd_Load(object sender, EventArgs e)
        {
            if (u.Controls[0].Text != "")
                listBox2.Items.AddRange(u.Controls[0].Text.Split('+'));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string qs = "";
            if (listBox2.SelectedIndex != -1)
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            foreach (string s in listBox2.Items)
            {
                if (qs.IsNullOrEmpty())
                { qs = s; }
                else
                    qs = qs + "+" + s;
            }           
            u.Controls[0].Text = qs;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void tbSXname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        private void tbSXname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {  
                string IPaddress = "132.147.160.60";
                bool flag = adims_BLL.DataValid.PingHost(IPaddress, 1000);
                if (flag == false)
                    MessageBox.Show("网络未连接，请检查网络");
                else
                {
                    DataTable dt = hdal.GetHisZhenduan(tbSXname.Text.Trim());
                    listBox1.Items.Clear();
                    foreach (DataRow dr in dt.Rows)
                    {
                        listBox1.Items.Add(dr[0].ToString());
                    }
                }
            }
        }

     
    }
}
