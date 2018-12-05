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
using adims_Utility;
using adims_DAL;

namespace main
{
    public partial class addYdyao : Form
    {

        //添加全麻药，吸入药物
        #region <<Members>>

        YaopinDal _YaopinDal = new YaopinDal();
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        List<adims_MODEL.mzyt> mzyt;
        ArrayList sss = new ArrayList();
        int mzjldID;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mzyt1"></param>
        public addYdyao(int id, List<adims_MODEL.mzyt> mzyt1)
        {
            mzjldID = id;
            mzyt = mzyt1;
            InitializeComponent();
        }
        public addYdyao(int id)
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
        adims_DAL.PacuDal dal = new adims_DAL.PacuDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("全麻药", tbPinyin.Text.Trim());
            this.listYaopin.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listYaopin.Items.Add(dt.Rows[i]["ypname"]);
            }
            BindDW();
            BindZRFS();
        }
        /// <summary>
        /// 单位
        /// </summary>
        private void BindDW()
        {
            DataTable dtdw = DAL.GetAllDW();
            cmbDW.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.cmbDW.Items.Add(dtdw.Rows[i][0]);
            }
        }
        /// <summary>
        /// 注入方式
        /// </summary>
        private void BindZRFS()
        {
            DataTable dtdw = DAL.GetAllYD_ZRFS();
            cmbYYFS.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.cmbYYFS.Items.Add(dtdw.Rows[i][0]);
            }
        }
        private void BindYdyList()
        {
            sss.Clear();
            DataTable dtYDY = _YongyaoListDal.GetYongyaoList(mzjldID, (int)EnumCreator.YongyaoType.诱导药);//2指诱导药
            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["name"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["name"].ToString());
                }
            }
        }
       
        private void addyt_Load(object sender, EventArgs e)
        {
            DataTable dt = _YaopinDal.GetYaopinBagAll();//绑定用药包
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
            if (listYaopin.SelectedIndex != -1)
                tbName.Text = listYaopin.SelectedItem.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sss.Count < 20 || (sss.Count == 20 && sss.Contains(tbName.Text.Trim())))
            {
                if (string.IsNullOrEmpty(tbName.Text.Trim()))
                {
                    MessageBox.Show("药名称不能为空！");
                    tbName.Focus();
                    return;
                }
                else if (string.IsNullOrEmpty(tbYL.Text.Trim()))
                {
                    MessageBox.Show("用量不能为空！");
                    tbYL.Focus();
                    return;
                }
                else
                {
                    int m = 0;
                    adims_MODEL.Yongyao ydy = new adims_MODEL.Yongyao();
                    ydy.YpType = 2;
                    ydy.Name = tbName.Text.Trim();
                    ydy.Yl = Convert.ToDouble(tbYL.Text.Trim());
                    ydy.Dw = cmbDW.Text.Trim();
                    ydy.Yyfs = cmbYYFS.Text.Trim();
                    ydy.Cxyy = cbCXYY.Checked;
                    ydy.KsTime = DateTime.Now;
                    if (cbCXYY.Checked)
                    {
                        ydy.Bz = 1;
                        m = bll.addYongyaoList1(mzjldID, ydy);
                    }
                    else
                    {
                        ydy.Bz = 2; ydy.JsTime = ydy.KsTime;
                        m = bll.addYongyaoList2(mzjldID, ydy);
                    }

                    if (m > 0)
                    {
                        tbName.Text = "";
                        BindYdyList();
                    }
                    else MessageBox.Show(ydy.Name + "—添加失败请重试！");
                }
            }
            else MessageBox.Show("全麻药区域标记数超标，请添加到其他用药。");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
                {
                    int m = bll.endYongyaoList(mzjldID, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindYdyList();
                }
                else
                    MessageBox.Show("用药已经结束！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int m = bll.deleteYaopinList(mzjldID, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
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
            TextValueLimit.Text_Value_Limit(sender, e);
        }

        #endregion

        private void btnBagUse_Click(object sender, EventArgs e)
        {
            DataTable dt = _YaopinDal.GetYaoPinBagByBagName(this.cmbBagName.Text.Trim());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sss.Count < 20 || (sss.Count == 20 && sss.Contains(dt.Rows[i]["ypname"].ToString())))
                {
                    int m = 0;
                    adims_MODEL.Yongyao ydy = new adims_MODEL.Yongyao();
                    ydy.YpType = 2;
                    ydy.Name = dt.Rows[i]["ypname"].ToString();
                    ydy.Yl = Convert.ToDouble(dt.Rows[i]["yl"]);
                    ydy.Dw = Convert.ToString(dt.Rows[i]["dw"]);
                    ydy.Yyfs = Convert.ToString(dt.Rows[i]["zrff"]);
                    int cxyy = Convert.ToInt32(dt.Rows[i]["cxyy"]);
                    ydy.KsTime = DateTime.Now;
                    if (cxyy == 1)
                    {
                        ydy.Cxyy = true;
                        ydy.Bz = 1;
                        m = bll.addYongyaoList1(mzjldID, ydy);
                    }
                    else
                    {
                        ydy.Cxyy = false;
                        ydy.Bz = 2;
                        ydy.JsTime = ydy.KsTime;
                        m = bll.addYongyaoList2(mzjldID, ydy);
                    }
                    if (m > 0 && !sss.Contains(ydy.Name))
                    {
                        sss.Add(ydy.Name);
                    }
                }
                else MessageBox.Show("全麻药区域标记数超标," + dt.Rows[i]["ypname"].ToString() + " 未添加成功！");
            }
            BindYdyList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName.Text = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["yl"].Value.ToString();
            cmbYYFS.Text = dataGridView1.SelectedRows[0].Cells["yyfs"].Value.ToString();
            if (Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["cxyy"].Value) == 1)
            {
                cbCXYY.Checked = true;
            }
            else cbCXYY.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int m = _YongyaoListDal.UpdateYDY_yl(tbYL.Text.Trim(), Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value));
                if (m > 0)
                    BindYdyList();
            }
        }

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByType("全麻药", tbPinyin.Text.Trim());
                listYaopin.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    listYaopin.Items.Add(dr[1].ToString());
                }
                tbName.Text = "";
            }
        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }

    }
}
