using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_Utility;

namespace main
{
    public partial class addSqyy : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        UserControl u;
        int mzjldid;
        public addSqyy(WindowsFormsControlLibrary5.UserControl1 o,int mzid)
        {
            u = o;
            mzjldid = mzid;
            InitializeComponent();
        }
        public addSqyy(UserControl o, int mzid)
        {
            u = o;
            mzjldid = mzid;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim() != "" && tbName.Text.Trim() != "")
            {
                int m = bll.InsertIntoSQYY(mzjldid, tbName.Text.Trim(), comboBox1.Text.Trim(), textBox2.Text.Trim(), comboBox2.Text.Trim());
                if (m > 0) DataBind();
            }
            else
                MessageBox.Show("药品名字和药品剂量不能为空");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                tbName.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==1)
            {
               
                int m = bll.deleteSQYY(mzjldid, dataGridView1.SelectedRows[0].Cells["id"].Value.ToString());
                if (m > 0)
                {
                    DataBind();
                }
                
               
            }
        }
        private void DataBind()
        {
            string ws = "";
            DataTable dt = bll.GetsqyyUse(mzjldid);            
            dataGridView1.DataSource = dt.DefaultView;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
               string  ss = dt.Rows[i]["ypName"].ToString()
                          + " " + dt.Rows[i]["yl"].ToString()
                          + "" + dt.Rows[i]["dw"].ToString()
                          + " " + dt.Rows[i]["yyfs"].ToString();
               if (ws == "")
                {
                    ws = ss;
                }
                else
                {
                    ws = ws + " + " + ss;  
                }
            }
            u.Text = ws;

        }
        /// <summary>
        /// 注入方式
        /// </summary>
        private void BindZRFS()
        {
            DataTable dtdw = dal.GetAllYD_ZRFS();
            comboBox1.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.comboBox1.Items.Add(dtdw.Rows[i][0]);
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        private void BindDW()
        {
            DataTable dtdw = dal.GetAllDW();
            comboBox2.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.comboBox2.Items.Add(dtdw.Rows[i][0]);
            }
        }
        private void sqyy_Load(object sender, EventArgs e)
        {
            BindZRFS();
            BindDW();
            DataBind();
            BindYaoPin();
        }
        adims_DAL.PacuDal dall = new adims_DAL.PacuDal();
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("术前用药", tbName.Text.Trim());
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            tbName.Text = listBox1.Text.ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string[] ss = listBox2.SelectedItem.ToString().Split(' ');
            //textBox1.Text = ss[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int m = bll.InsertIntoSQYY(mzjldid, "鲁米那", "肌注", "0.1", "mg");
            int n = bll.InsertIntoSQYY(mzjldid, "阿托品", "肌注", "0.5", "mg");
            if (m > 0 || n > 0)
            {
                DataBind();
            }
        }
      
    }
}
