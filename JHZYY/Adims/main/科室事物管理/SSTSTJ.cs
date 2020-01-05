using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_BLL;
using main.科室事物管理;

namespace main
{
    public partial class SSTSTJ : Form
    {
        public SSTSTJ()
        {
            InitializeComponent();
        }
        adims_DAL.AdimsProvider opr = new adims_DAL.AdimsProvider();
        AdimsController adimsController = new AdimsController();
        string conditon = null;

        private void SSTSTJ_Load(object sender, EventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.TransparencyKey = this.BackColor;
            this.WindowState = FormWindowState.Maximized;
            int month = DateTime.Now.Month; 
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text=year+"/1/1";
            //初始化月份第一天
            dateTimePicker1.Text=year+"/"+month+"/1";
           

            //string sqlWhere = " 1=1 ";
            //System.Data.DataTable dt = adimsController.GetMzjldList(sqlWhere);
            BindDataGridView();
            DataTable dt0  = opr.GetAllMZYS();
            
            if (dt0.Rows.Count > 0)
            {
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    treeView1.Nodes[0].Nodes.Add(dt0.Rows[i][0].ToString());
                }
            }
            
            DataTable dt1 = new DataTable();
            dt1 = opr.GetSSJname();
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    treeView1.Nodes[1].Nodes.Add(dt1.Rows[i][0].ToString());
                    
                }
            }

            DataTable dt2 = opr.GetKShiname();
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    treeView1.Nodes[2].Nodes.Add(dt2.Rows[i][0].ToString());
                }
            }
            List<string> list = new List<string>();
            list.Add("Ⅰ");
            list.Add("Ⅱ");
            list.Add("Ⅲ");
            list.Add("Ⅳ");
            list.Add("Ⅴ");
            //DataTable dt3 = opr.GetKShiname();
            if (list.Count > 0)
            {
                for (int i = 0; i <list.Count; i++)
                {
                    treeView1.Nodes[3].Nodes.Add(list[i].ToString());
                }
            }
            DataTable dt4 = opr.GetMZname();            
            if (dt4.Rows.Count > 0)
            {
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    treeView1.Nodes[4].Nodes.Add(dt4.Rows[i][0].ToString());
                }
            }
            DataTable dt5 = opr.GetAll_hushi();

            if (dt5.Rows.Count > 0)
            {
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    treeView1.Nodes[5].Nodes.Add(dt5.Rows[i][0].ToString());
                }
            }
            DataTable dt6 = opr.GetAll_QKDJ();

            if (dt6.Rows.Count > 0)
            {
                for (int i = 0; i < dt6.Rows.Count; i++)
                {
                    treeView1.Nodes[6].Nodes.Add(dt6.Rows[i][0].ToString());
                }
            }
            DataTable dt7 = opr.GetAll_SSLB();

            if (dt7.Rows.Count > 0)
            {
                for (int i = 0; i < dt7.Rows.Count; i++)
                {
                    treeView1.Nodes[7].Nodes.Add(dt7.Rows[i][0].ToString());
                }
            }
        }

        private void BindDataGridView()
        {
            System.Data.DataTable dt = opr.GetMzjldList2(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            TreeNode node = e.Node;
            if (node.Level == 1)
            {
                string CXname = e.Node.Parent.Name;//需要查询字段
                string Lname = e.Node.Parent.Text;
                if (Lname == "切口等级")
                {
                    string TreenodeText = e.Node.Text;
                    string TreenodeName = e.Node.Name;
                    System.Data.DataTable dt = opr.GetByYSName(TreenodeText, CXname, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    dataGridView1.DataSource = dt.DefaultView;
                    this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
                }
                else if (Lname == "手术类别")
                {
                    string TreenodeText = e.Node.Text;
                    string TreenodeName = e.Node.Name;
                    System.Data.DataTable dt = opr.GetByYSName(TreenodeText, CXname, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    dataGridView1.DataSource = dt.DefaultView;
                    this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
                }
                else if (Lname == "手术持续时间")
                {
                    string TreenodeText = e.Node.Text;
                     System.Data.DataTable dt;
                    if (TreenodeText=="3小时内")
                    {
                        dt = opr.GetByShiJian(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));

                    }
                    else
                    {
                        dt = opr.GetByShiJian2(dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    }
                    dataGridView1.DataSource = dt.DefaultView;
                    this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
                }
                else
                {
                    string TreenodeText = e.Node.Text;
                    string TreenodeName = e.Node.Name;
                    System.Data.DataTable dt = opr.GetByYSName(TreenodeText, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                    dataGridView1.DataSource = dt.DefaultView;
                    this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
                }

            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string where1 = treeView1.SelectedNode.Text.ToString();
            conditon = where1;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fmzystj F1 = new Fmzystj();
            F1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fkstj F1 = new Fkstj();
            F1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FSSJTJ F1 = new FSSJTJ();
            F1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindDataGridView();
            
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            BindDataGridView();

        }
        /// <summary>
        /// 手术室护士统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            HuShiTJ f1 = new HuShiTJ();
            f1.Show();
        }

     
        
    }
}
