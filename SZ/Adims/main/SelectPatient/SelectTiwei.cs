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
    public partial class SelectTiwei : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        WindowsFormsControlLibrary5.UserControl1 u;
        int mzjldid;
        public SelectTiwei(WindowsFormsControlLibrary5.UserControl1 o,int mzid)
        {
            u = o;
            mzjldid = mzid;
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
                            qs = qs + "," + s;
                    }
                    int n;
                    n = bll.xgtw(mzjldid, qs);

                    if (n != 1)
                        MessageBox.Show("error" + n.ToString());
                    u.Controls[0].Text = qs;
                }

            }
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
                    qs = qs + "," + s;
            }
            int n;
            n = bll.xgtw(mzjldid, qs);

            if (n != 1)
                MessageBox.Show("error" + n.ToString());
            u.Controls[0].Text = qs;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sqzd_Load(object sender, EventArgs e)
        {
            if (u.Controls[0].Text != "")
                listBox2.Items.AddRange(u.Controls[0].Text.Split(','));
        }


    }
}
