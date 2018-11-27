using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_MODEL;

namespace main
{
    public partial class addSqyy : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        WindowsFormsControlLibrary5.UserControl1 u;
        int mzjldid;
        public addSqyy(WindowsFormsControlLibrary5.UserControl1 o,int mzid)
        {
            u = o;
            mzjldid = mzid;
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbYl.Text.Trim() != "" && tbYpName.Text.Trim() != "")
            {
                //DataTable dt = bll.GetsqyyUse(mzjldid);
                //if (dt.Rows.Count>0)
                //{
                SqyyUse yy = new SqyyUse();
                yy.MzjldId = mzjldid;
                yy.YpName = tbYpName.Text.Trim();
                yy.Yyfs = cmbYyfs.Text.Trim();
                yy.Yl = tbYl.Text.Trim();
                yy.Dw = cmbDw.Text.Trim();
                yy.UseTime = dtUseTime.Value;
                int m = bll.InsertIntoSQYY(yy);
                    if (m > 0)
                    {
                        DataBind();
                    }
                //}               
            }
            else
                MessageBox.Show("药品名字和药品剂量不能为空");
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                tbYpName.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
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
            u.Controls[0].Text = ws;

        }
        private void sqyy_Load(object sender, EventArgs e)
        {
            DataBind();
            BindYaoPin();
            this.dtUseTime.Format = DateTimePickerFormat.Custom;
            this.dtUseTime.CustomFormat = "yyyy-MM-dd HH:mm";
        }
        adims_DAL.PACU_DAL dall = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dall.GetAdims_YaoPinByType("术前用药");
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            tbYpName.Text = listBox1.Text.ToString();
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
            UserFunction.Text_Value_Limit(sender, e); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqyyUse yy = new SqyyUse();
            yy.MzjldId = mzjldid;
            yy.YpName = "鲁米那";
            yy.Yyfs = "肌注";
            yy.Yl = "0.1";
            yy.Dw = "g";
            yy.UseTime = DateTime.Now;

            SqyyUse yy2 = new SqyyUse();
            yy2.MzjldId = mzjldid;
            yy2.YpName = "鲁米那";
            yy2.Yyfs = "肌注";
            yy2.Yl = "0.1";
            yy2.Dw = "g";
            yy2.UseTime = DateTime.Now;
            int m = bll.InsertIntoSQYY(yy);
            int n = bll.InsertIntoSQYY(yy2);
            if (m > 0 || n > 0)
            {
                DataBind();
            }
        }
      
    }
}
