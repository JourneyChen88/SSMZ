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
using main.OrgBusinessManage;

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
            int month = DateTime.Now.Month; 
            int year = DateTime.Now.Year;
            //初始化年第一天
            dateTimePicker1.Text=year+"/1/1";
            //初始化月份第一天
            dateTimePicker1.Text=year+"/"+month+"/1";
           

            //string sqlWhere = " 1=1 ";
            //System.Data.DataTable dt = adimsController.GetMzjldList(sqlWhere);
            //BindDataGridView();

            DataTable dt1 = new DataTable();
            dt1 = opr.GetAllMZYS();
           
            if ( dt1.Rows.Count>0)
            {
                for (int i = 0; i <dt1.Rows.Count; i++)
                {
                    tvTJFS.Nodes[0].Nodes.Add(dt1.Rows[i][0].ToString());
                }
            }
           

            DataTable dt2 = new DataTable();
            dt2 = opr.GetSSJname();
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    tvTJFS.Nodes[1].Nodes.Add(dt2.Rows[i][0].ToString());
                    
                }
            }

            DataTable dt3 = new DataTable();
            dt3 = opr.GetKShiname();
            if (dt3.Rows.Count > 0)
            {
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    tvTJFS.Nodes[2].Nodes.Add(dt3.Rows[i][0].ToString());

                }
            }

            DataTable dt5 = new DataTable();
            dt5 = opr.GetMZname();
            tvTJFS.Nodes[4].Nodes.Clear();
            tvTJFS.Nodes[4].Nodes.Add("全身麻醉");
            if (dt5.Rows.Count > 0)
            {
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    tvTJFS.Nodes[4].Nodes.Add(dt5.Rows[i][0].ToString());
                }
            }

            tvTJFS.Nodes[3].Nodes.Add("1");
            tvTJFS.Nodes[3].Nodes.Add("2");
            tvTJFS.Nodes[3].Nodes.Add("3");
            tvTJFS.Nodes[3].Nodes.Add("4");
            tvTJFS.Nodes[3].Nodes.Add("5");
            tvTJFS.Nodes[3].Nodes.Add("E");

            //    }
            //}
        }

        private void BindDataGridView()
        {
            string d1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string d2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            System.Data.DataTable dt = opr.GetMzjldList2(d1, d2);
            dataGridView1.DataSource = dt.DefaultView;
            this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count - 1).ToString();
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.Level == 1)
            {
                string TreenodeText = e.Node.Text;
                string TreenodeName = e.Node.Name;
                System.Data.DataTable dt = opr.GetByYSName(TreenodeText, dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                dataGridView1.DataSource = dt.DefaultView;
                this.toolStripStatusLabel1.Text = "当前手术总量：" + (dataGridView1.Rows.Count).ToString();
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            string where1 = tvTJFS.SelectedNode.Text.ToString();
            conditon = where1;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fmzystj f = new Fmzystj();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fkstj f = new Fkstj();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FSSJTJ f = new FSSJTJ();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BindDataGridView();
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Fmzhstj f1 = new Fmzhstj();
            f1.ShowDialog();
        }

        
    }
}
