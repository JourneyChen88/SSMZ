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
    public partial class SelectMZFF : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        WindowsFormsControlLibrary5.UserControl1 u;
        int mzjldid;
        public SelectMZFF(WindowsFormsControlLibrary5.UserControl1 o,int mzid)
        {
            u = o;
            mzjldid = mzid;
            InitializeComponent();
        }
        public SelectMZFF(WindowsFormsControlLibrary5.UserControl1 o)
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
                        if (qs == "")
                        { qs = s; }
                        else
                        qs = qs + "+"+s;
                    }
                    u.Controls[0].Text = qs;
                    int n;
                    n = bll.xgmzff(mzjldid, qs);
                    if (n != 1)
                        MessageBox.Show("error" + n.ToString());
                    
                }
            
            }
        }

        private void sqzd_Load(object sender, EventArgs e)
        {
            DataTable dt = bll.GetAllmzzuifangfa();
            if (dt.Rows.Count>0)
            {
                listBox1.Items.Clear();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i][0].ToString());
            }
            if(u.Controls[0].Text!="")
                listBox2.Items.AddRange(u.Controls[0].Text.Split('+'));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string qs = "";
            if (listBox2.SelectedIndex != -1)
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            foreach (string s in listBox2.Items)
            {
                if (qs == "")
                { qs = s; }
                else
                    qs = qs + "+" + s;
            }
            int n;
            n = bll.xgmzff(mzjldid, qs);
            if (n != 1)
                MessageBox.Show("error" + n.ToString());
            u.Controls[0].Text = qs;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
