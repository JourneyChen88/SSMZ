using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main
{
    public partial class HuShiTJ : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public HuShiTJ()
        {
            InitializeComponent();
        }

        private void HuShiTJ_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            int month = DateTime.Now.Month;
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text = year + "/1/1";
            //初始化月份第一天
            dateTimePicker1.Text = year + "/" + month + "/1";
            DGVBind();
        }
        private void DGVBind() 
        {
            DataTable dt = dal.SelectHushiTJ(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DGVBind();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level == 0)
            {
                string TreenodeText = e.Node.Text;
                string TreenodeName = e.Node.Name;
                System.Data.DataTable dt = dal.HuShiName(TreenodeName, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                dataGridView1.DataSource = dt.DefaultView;
                this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            }
        }
    }
}
