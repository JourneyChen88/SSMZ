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

namespace main
{
    public partial class addtsyy : Form
    {
        #region <<Members>>

        int mzjldid;
        List<adims_MODEL.tsyy> tsyy;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        AdimsProvider apro = new AdimsProvider();
        PACU_DAL Pdal = new PACU_DAL();
        
        public addtsyy(List<adims_MODEL.tsyy> tsyy1,int id)
        {
            mzjldid = id;
            tsyy = tsyy1;
            InitializeComponent();
          
        }
        public addtsyy(int id)
        {
            mzjldid = id;           
            InitializeComponent();

        }


        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t"></param>
        public addtsyy(List<adims_MODEL.tsyy> t)
        {
            tsyy = t;
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1&&tbName1.Text.Trim()=="")
                tbName1.Text = listBox1.SelectedItem.ToString();
            else
                tbName2.Text = listBox1.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//添加
        {
            if (tbName1.Text == "")
            {
                MessageBox.Show("药名不能为空。");
                return;
            }
             if (tbYl1.Text == "")
            {
                MessageBox.Show("用量不能为空。");
                return;
            }
             else
           {
                adims_MODEL.tsyy ts = new adims_MODEL.tsyy();
                ts.D = DateTime.Now;
                if (tbName2.Text.Trim() != "" && tbYl2.Text != "")
                {
                    ts.Name = tbName2.Text.Trim() + " " + tbYl2.Text.Trim()
                        + cmbDW2.Text.Trim() + "+" + tbName1.Text.Trim();
                }
                else
                    ts.Name = tbName1.Text.Trim();
                ts.Yl = float.Parse(tbYl1.Text.Trim());
                ts.Dw = cmbDW1.Text.Trim();
                ts.Yyfs = cmbYYFS1.Text.Trim();               
                tsyy.Add(ts);
                int m = bll.addtsyy(mzjldid, ts);
                if (m>0)
                {
                    datagridBind();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)//删除
        {
            if (dataGridView1.SelectedCells.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);                    
                    int j = 0;
                    foreach (adims_MODEL.tsyy ts in tsyy)
                    {
                        if (ts.Name == dataGridView1.CurrentRow.Cells[2].Value.ToString())
                        {
                            tsyy.Remove(ts);
                            int i = bll.deleteTSYY(ID);
                            j++;
                        }
                        if (j > 0)
                        {
                            datagridBind();
                            break; 
                        }                            
                    }
                }              
            }
            else
                MessageBox.Show("请选择已使用的特殊用药！"); 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e); 
        }

        #endregion

        private void datagridBind()
        {
            
            DataTable dt = apro.GetTSYY(mzjldid);            
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt.DefaultView;

            }
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("特殊用药");
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void addtsyy_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            datagridBind();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbYl1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
        }

       
    }
}
