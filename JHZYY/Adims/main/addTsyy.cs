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
    public partial class addTsyy : Form
    {
        #region <<Members>>

        int mzjldid;
        List<adims_MODEL.tsyy> tsyy;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        AdimsProvider apro = new AdimsProvider();
        PACU_DAL Pdal = new PACU_DAL();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t"></param>
        public addTsyy(List<adims_MODEL.tsyy> tsyy1,int id)
        {
            mzjldid = id;
            tsyy = tsyy1;
            InitializeComponent();
          
        }
        public addTsyy(int id)
        {
            mzjldid = id;           
            InitializeComponent();
        }
       
        #region <<Events>>

        

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listYaopin.SelectedIndex != -1)
                tbName.Text = listYaopin.SelectedItem.ToString();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//添加
        {
            if (tbName.Text.Trim() != "" && tbYl.Text.Trim() != "")
           {
                adims_MODEL.tsyy ts = new adims_MODEL.tsyy();
                ts.D = DateTime.Now;                
                ts.Name = tbName.Text.Trim();
                ts.Yl = Convert.ToDouble(tbYl.Text.Trim());
                ts.Dw = cmbDW1.Text.Trim();
                ts.Yyfs = cmbYYFS1.Text.Trim();
                int m = bll.addtsyy(mzjldid, ts);
                if (m>0)
                {
                    datagridBind();
                }
            }
           else
               MessageBox.Show("药品名和用量不能为空！"); 
        }
        private void button3_Click(object sender, EventArgs e)//删除
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    int j = bll.deleteTSYY(ID);
                    if (j>0)
                    {
                         datagridBind();
                    }
                }              
            }
            else
                MessageBox.Show("请选择已使用的特殊用药！"); 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataValid.Text_Value_Limit(sender, e); 
        }

        #endregion

        private void datagridBind()
        {
            
            DataTable dt = apro.GetTSYY(mzjldid);
            dataGridView1.DataSource = dt.DefaultView;
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType(tbPinyin.Text.Trim());
            this.listYaopin.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listYaopin.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        /// <summary>
        /// 绑定用量单位
        /// </summary>
        private void BindDW()
        {
            DataTable dt = Pdal.GetAdims_DW();
            cmbDW1.DataSource = dt;
            cmbDW1.ValueMember = "id";
            cmbDW1.DisplayMember = "name";
        }
        private void addtsyy_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            datagridBind();
            BindDW();
            BindFS();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbYl.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            
        }

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = dal.GetAdims_YaoPinByType(tbPinyin.Text.Trim());
                listYaopin.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                    listYaopin.Items.Add(dr[1].ToString());
                {
                }
                tbName.Text = "";
            }
        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }
        private void BindFS()
        {
            DataTable dt = dal.GetAdims_fs();
            cmbYYFS1.DataSource = dt;
            cmbYYFS1.ValueMember = "id";
            cmbYYFS1.DisplayMember = "name";
        }
       
    }
}
