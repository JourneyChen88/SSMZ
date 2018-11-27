using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using System.Collections;

namespace main
{
    public partial class addYDyao : Form
    {

        //添加诱导药，吸入药
        #region <<Members>>
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<adims_MODEL.mzyt> mzyt;
        ArrayList sss = new ArrayList();
        int mzjldID;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mzyt1"></param>
        public addYDyao(int id, List<adims_MODEL.mzyt> mzyt1)
        {
            mzjldID = id;
            mzyt = mzyt1;
            InitializeComponent();
        }
        public addYDyao(int id)
        {
            mzjldID = id;
            InitializeComponent();
        }

        #endregion



        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("诱导药");
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }

        private void BindYdyList()
        {
            sss.Clear();
            DataTable dtYDY = bll.selectYDY(mzjldID);
            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["ydyname"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["ydyname"].ToString());
                }
            }
        }
        private void addyt_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.GetAdims_ALLBagName();//绑定用药包
            cmbBagName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbBagName.Items.Add(dt.Rows[i][0]);
            }
            BindYaoPin();//绑定待选列表
            BindYdyList();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                tbName.Text = listBox1.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sss.Count < 11 || (sss.Count == 11 && sss.Contains(tbName.Text.Trim())))
            {
                if (tbYL.Text.Trim() != "" && tbName.Text.Trim() != "")
                {
                    int m = 0;
                    adims_MODEL.mzyt yt1 = new adims_MODEL.mzyt();
                    yt1.Ytname = tbName.Text.Trim();
                    yt1.Yl = Convert.ToDouble(tbYL.Text.Trim());
                    yt1.Dw = comboBox2.Text.Trim();
                    yt1.Yyfs = cmbYYFS.Text.Trim();
                    yt1.Cxyy = cbCXYY.Checked;
                    yt1.Sysj = DateTime.Now;
                    if (cbCXYY.Checked)
                    {
                        yt1.Bz = 1;
                        yt1.Dw = yt1.Dw;
                        m = bll.addyt1(mzjldID, yt1);
                    }
                    else
                    {
                        yt1.Bz = 2;
                        yt1.Jssj = yt1.Sysj;
                        m = bll.addyt2(mzjldID, yt1);
                    }
                    if (m > 0)
                    {
                        BindYdyList();

                    }
                }

                else MessageBox.Show("用量或诱导药名称不能为空");
            }
            else MessageBox.Show("诱导药标记数超标，请添加到特殊用药");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
                {
                    int m = bll.endyt(mzjldID, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindYdyList();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int m = bll.delyt(mzjldID, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    if (m > 0)
                    {
                        BindYdyList();
                    }

                }
                else
                    MessageBox.Show("请选择已使用的诱导药！");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e);
        }

        #endregion

        private void btnBagUse_Click(object sender, EventArgs e)
        {
            DataTable dt = dal.GetYongYaoBag(this.cmbBagName.Text.Trim());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sss.Count < 11 || (sss.Count == 11 && sss.Contains(dt.Rows[i]["ypname"].ToString())))
                {
                    int m = 0;
                    adims_MODEL.mzyt yt1 = new adims_MODEL.mzyt();
                    yt1.Ytname = dt.Rows[i]["ypname"].ToString();
                    yt1.Yl = Convert.ToDouble(dt.Rows[i]["yl"]);
                    yt1.Dw = Convert.ToString(dt.Rows[i]["dw"]);
                    yt1.Yyfs = Convert.ToString(dt.Rows[i]["zrff"]);
                    int cxyy = Convert.ToInt32(dt.Rows[i]["cxyy"]);
                    yt1.Sysj = DateTime.Now;
                    if (cxyy == 1)
                    {
                        yt1.Cxyy = true;
                        yt1.Bz = 1;
                        m = bll.addyt1(mzjldID, yt1);
                    }
                    else
                    {
                        yt1.Cxyy = false;
                        yt1.Bz = 2;
                        yt1.Jssj = yt1.Sysj;
                        m = bll.addyt2(mzjldID, yt1);
                    }
                    if (m > 0 && !sss.Contains(yt1.Ytname))
                    {
                        sss.Add(yt1.Ytname);
                    }
                }
                else
                {
                    MessageBox.Show("诱导药标记数超标," + dt.Rows[i]["ypname"].ToString() + " 未添加成功！");
                }
            }
            BindYdyList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName.Text = dataGridView1.SelectedRows[0].Cells["ypname"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["yl"].Value.ToString();
            cmbYYFS.Text = dataGridView1.SelectedRows[0].Cells["yyfs"].Value.ToString();
            if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["cxyy"].Value) == 1)
            {
                cbCXYY.Checked = true;
            }
            else
                cbCXYY.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int m = bll.xgydYL(tbYL.Text.Trim(), Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value));
                if (m > 0)
                    BindYdyList();
            }
        }
    }
}
