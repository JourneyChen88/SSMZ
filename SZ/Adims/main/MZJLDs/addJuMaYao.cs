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
        List<adims_MODEL.jtytsx> szyy1;
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
        public addJuMaYao(List<adims_MODEL.jtytsx> jys, int m)
        {
            mzjldid = m;
            //type = t;
            szyy1 = jys;
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
            DataTable dt = dal.GetAdims_YaoPinByType("局麻药");
            listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }

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
            if (sss.Count < 3 || (sss.Count == 3 && sss.Contains(textBox1.Text.Trim())))
            {
                if (textBox2.Text != "")
                {
                    adims_MODEL.jtytsx jtytsx = new adims_MODEL.jtytsx();
                    jtytsx.Bz = 1;
                    jtytsx.Name = textBox1.Text.Trim();
                    jtytsx.Jl = Convert.ToDouble(textBox2.Text);
                    jtytsx.Cxyy = checkBox1.Checked;
                    jtytsx.Dw = comboBox2.Text;
                    jtytsx.Zrfs = comboBox1.Text;
                    //jtytsx.Ytlx = type;
                    jtytsx.Kssj = DateTime.Now;
                    int q = bll.addjt(mzjldid, jtytsx);
                    if (q > 0)
                    { BindJMYList();}
                }

                else MessageBox.Show("剂量不能为空");
            }
            else MessageBox.Show("局麻药数量超标");
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                textBox1.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e); 
        }

        #endregion

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.textBox1.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            this.textBox2.Text = dataGridView1.CurrentRow.Cells["JL"].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells["dw"].Value.ToString();
        }

      
    }
}
