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
using adims_Utility;

namespace main
{
    public partial class addJmy : Form
    {

        //添加局部麻醉药
        #region <<Members>>   
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
    adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
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
        public addJmy(int mzid)
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
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("局麻药", tbName.Text.Trim());
            listYaopin.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listYaopin.Items.Add(dt.Rows[i]["ypname"]);
            }

        }
        private void BindJMYList()
        {
            sss.Clear();
            DataTable dtYDY = _YongyaoListDal.GetYongyaoList(mzjldid, (int)EnumCreator.YongyaoType.诱导药);//3指局麻药
            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["name"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["name"].ToString());
                }
            }
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
                    int q = bll.deleteYaopinList(mzjldid,Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
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
                    adims_MODEL.Yongyao jmy = new adims_MODEL.Yongyao();
                    jmy.Bz = 1;
                    jmy.YpType = 3;
                    jmy.Name = tbName.Text.Trim();
                    jmy.Yl = Convert.ToDouble(tbJL.Text.Trim());
                    jmy.Cxyy = cbCXYY.Checked;
                    jmy.Dw = cmbDW.Text;
                    jmy.Yyfs = cmbZRFS.Text;
                    jmy.KsTime = DateTime.Now;
                    int q = bll.addYongyaoList2(mzjldid, jmy);
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
            TextValueLimit.Text_Value_Limit(sender, e); 
        }

        #endregion

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByType("局麻药", tbPinyin.Text.Trim());
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

        private void addJmy_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindJMYList();  
        }

      
    }
}
