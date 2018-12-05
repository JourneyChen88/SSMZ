using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using adims_Utility;
using adims_DAL;

namespace main
{
    public partial class addShuxue : Form

    {
        YaopinDal _YaopinDal = new YaopinDal();
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.PacuDal dal = new adims_DAL.PacuDal();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        ArrayList syList = new ArrayList();
        int mzjldID;
        DateTime ksTime;      
        public addShuxue(DateTime dt, int id)
        {
            mzjldID = id;
            ksTime = dt;
            InitializeComponent();
        }
        public addShuxue(int id)
        {
            mzjldID = id;
            ksTime = DateTime.Now;
            InitializeComponent();
        }
        
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("输血", cmbName.Text.Trim());
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindShuxueList()
        {
            DataTable dtSX = _YongyaoListDal.GetYongyaoList(mzjldID, (int)EnumCreator.YongyaoType.输血);//5输血
            dataGridView1.DataSource = dtSX.DefaultView;
            syList.Clear();
            for (int i = 0; i < dtSX.Rows.Count; i++)
            {
                if (!syList.Contains(dtSX.Rows[i]["name"].ToString()))
                {
                    syList.Add(dtSX.Rows[i]["name"].ToString());
                }
            }
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
            cmbZRFS.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.cmbZRFS.Items.Add(dtdw.Rows[i][0]);
            }
        }
        private void addshuxue_Load(object sender, EventArgs e)
        {
            BindDW();
            BindZRFS();
            BindYaoPin();
            BindShuxueList();
          
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (syList.Count < 4 || (syList.Count == 4 && syList.Contains(cmbName.Text.Trim())))
            {
                if (tbYL.Text != "" && cmbName.Text.Trim() != "")
                {
                    adims_MODEL.Yongyao sx1 = new adims_MODEL.Yongyao();
                    sx1.Bz = 1;
                    sx1.YpType = 5;
                    sx1.Name = cmbName.Text;
                    sx1.Yl = Convert.ToDouble(tbYL.Text);
                    sx1.Dw = cmbDW.Text;
                    sx1.Yyfs = cmbZRFS.Text;
                    sx1.Cxyy = cbCXYY.Checked;
                    sx1.KsTime = ksTime;
                    sx1.Hxb = txthxb.Text;
                    sx1.Xuejiang = txtxuejiang.Text;
                    sx1.Quanxue = txtquanxue.Text;
                    int m = 0;
                    if (cbCXYY.Checked)
                    {
                        sx1.Bz = 1;
                        m = bll.addYongyaoList1(mzjldID, sx1);
                    }
                    else
                    {
                        sx1.Bz = 2; sx1.JsTime = sx1.KsTime;
                        m = bll.addYongyaoList2(mzjldID, sx1);
                    }
                    if (m > 0)
                        BindShuxueList();
                    else MessageBox.Show(sx1.Name + "—添加失败请重试！");
                }
                else MessageBox.Show("剂量、输血名不能为空");
            }
            else MessageBox.Show("输血标记超标，请到其他用药处添加。");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
                {
                    int m = bll.endYongyaoList(mzjldID, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindShuxueList();
                }
                else
                    MessageBox.Show("输血已经结束！");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int q = bll.deleteYaopinList(mzjldID, id);
                if (q > 0)
                    BindShuxueList();
            }            
            else MessageBox.Show("请选择需要删除的血液！");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        private void cmbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByType("输血", cmbName.Text.Trim());
                cmbName.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbName.Items.Add(dr[1].ToString());
                }
                cmbName.Text = "";
                cmbName.DroppedDown = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbName.Text = dataGridView1.SelectedRows[0].Cells["name"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["yl"].Value.ToString();
            cmbZRFS.Text = dataGridView1.SelectedRows[0].Cells["yyfs"].Value.ToString();
            cmbDW.Text = dataGridView1.SelectedRows[0].Cells["dw"].Value.ToString();
            txthxb.Text = dataGridView1.SelectedRows[0].Cells["hxb"].Value.ToString();
            txtquanxue.Text = dataGridView1.SelectedRows[0].Cells["quanxue"].Value.ToString();
            txtxuejiang.Text = dataGridView1.SelectedRows[0].Cells["xuejiang"].Value.ToString();
        }

        private void txthxb_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void txtquanxue_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void txtxuejiang_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }
       
    }
}
