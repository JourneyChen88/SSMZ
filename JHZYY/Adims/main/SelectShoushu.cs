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
    public partial class SelectShoushu : Form
    {
        adims_DAL.HisDB_Help hdal = new adims_DAL.HisDB_Help();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        WindowsFormsControlLibrary5.UserControl1 u;
        string patid;
        public SelectShoushu(WindowsFormsControlLibrary5.UserControl1 o, string pid)
        {
            u = o;
            patid = pid;
            InitializeComponent();
        }
        public SelectShoushu(WindowsFormsControlLibrary5.UserControl1 o)
        {
            u = o;
            InitializeComponent();
        }
        private void osel_Load(object sender, EventArgs e)
        {
            DataTable dt = bll.Osel("");
            dgvShoushu.DataSource = dt.DefaultView;
            dgvShoushu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = bll.Osel(tbName.Text);
            dgvShoushu.DataSource = dt.DefaultView;
        }



        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                string IPaddress = "132.147.160.60";
                bool flag = adims_BLL.DataValid.PingHost(IPaddress, 1000);
                if (flag == false)
                    MessageBox.Show("网络未连接，请检查网络");
                else
                {
                    DataTable dt = hdal.GetHisShoushu(tbName.Text.Trim());
                    dgvShoushu.DataSource = dt;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                string qs = "";
                int j = 1;
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                foreach (string s in listBox2.Items)
                {
                    qs = qs + j.ToString() + "." + s + " ";
                    j++;
                }
                u.Controls[0].Text = qs;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvShoushu.SelectedRows.Count > 0)
            {
                int i = 0;
                string qs = "";
                foreach (string s in listBox2.Items)
                {
                    if (s == dgvShoushu.SelectedCells[0].Value.ToString())
                        i = 1;
                }
                if (i == 1)
                    MessageBox.Show("已添加");
                else
                {
                    int j = 1;
                    listBox2.Items.Add(dgvShoushu.SelectedCells[0].Value.ToString());
                    foreach (string s in listBox2.Items)
                    {
                        qs = qs + j.ToString() + "." + s + " ";
                        j++;
                    }
                    u.Controls[0].Text = qs;

                }
            }
        }
    }
}
