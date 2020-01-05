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
using System.Collections;

namespace main
{
    public partial class addJuMaYao : Form
    {

        //添加局部麻醉药
        #region <<Members>>

        
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
       // List<adims_MODEL.jtytsx> szyy1;
        ArrayList sss = new ArrayList();
        int  mzjldid;

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jys"></param>
        /// <param name="t"></param>
        /// <param name="m"></param>       
        public addJuMaYao(int mzid)
        {
            mzjldid = mzid;
            //type = t;           
            InitializeComponent();
        }
        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///  
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("局麻药", tbName.Text.Trim());
            listYaopin.Items.Clear();
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
            DataTable dt = dal.GetAdims_DW();
            cmbDW.DataSource = dt;
            cmbDW.ValueMember = "id";
            cmbDW.DisplayMember = "name";
        }
        private void BindJMYList()
        {
            sss.Clear();
            DataTable dtYDY = bll.select_JMY(mzjldid);

            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["name"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["name"].ToString());
                }
            }
        }
        private void addszyy_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindJMYList();
            BindDW();
           
         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==1)
            {
                int q = bll.endjt(mzjldid, DateTime.Now, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                BindJMYList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int q = bll.deljt(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    if (q > 0)
                    {
                        BindJMYList();
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的局麻药");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sss.Count < 2 || (sss.Count == 2 && sss.Contains(tbName.Text.Trim())))
            {
                if (tbName.Text.Trim() != "" && tbJL.Text.Trim() != "")
                {
                    adims_MODEL.jtytsx jtytsx = new adims_MODEL.jtytsx();
                    jtytsx.Bz = 1;
                    jtytsx.Name = tbName.Text.Trim();
                    jtytsx.Jl = Convert.ToDouble(tbJL.Text.Trim());
                    jtytsx.Cxyy = cbCXYY.Checked;
                    jtytsx.Dw = cmbDW.Text;
                    jtytsx.Zrfs = cmbZRFS.Text;
                    //jtytsx.Ytlx = type;
                    jtytsx.Kssj = DateTime.Now;
                    int q = bll.addjt(mzjldid, jtytsx);
                    if (q > 0)
                    { BindJMYList();}
                }
                else MessageBox.Show("药名，剂量不能为空");
            }
            else MessageBox.Show("局麻药数量超标");
        }
        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listYaopin.SelectedIndex != -1)
            {
                tbName.Text = listYaopin.SelectedItem.ToString();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataValid.Text_Value_Limit(sender, e); 
        }

        #endregion

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = dal.GetAdims_YaoPinByType("局麻药", tbPinyin.Text.Trim());
                listYaopin.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    listYaopin.Items.Add(dr[1].ToString());
                }
                tbName.Text = "";
            }
        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {//禁用空格键和回车键
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

      
    }
}
