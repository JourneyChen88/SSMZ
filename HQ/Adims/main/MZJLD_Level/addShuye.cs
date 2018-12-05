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
    public partial class addShuye : Form
    {
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();      
        ArrayList syList = new ArrayList();
        int mzjldID;
        DateTime ksTime;
        public addShuye(DateTime dt,int id)
        {           
            ksTime = dt;
            mzjldID = id;
            InitializeComponent();
        }       
        public addShuye( int id)
        {
            ksTime = DateTime.Now;
            mzjldID = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (syList.Count < 7 || (syList.Count == 7 && syList.Contains(cmbName.Text.Trim())))
            {
                if (tbYL.Text != "" && cmbName.Text.Trim() != "")
                {
                    adims_MODEL.Yongyao sy1 = new adims_MODEL.Yongyao();
                    sy1.Bz = 1;
                    sy1.YpType = 4;
                    sy1.Name = cmbName.Text;
                    sy1.Yl = Convert.ToDouble(tbYL.Text);
                    sy1.Dw = cmbDW.Text;
                    sy1.Yyfs = cmbZRFS.Text;
                    sy1.Cxyy = cbCXYY.Checked;
                    sy1.KsTime = ksTime;
                    int m = 0;
                    if (cbCXYY.Checked)
                    {
                        sy1.Bz = 1;
                        m = bll.addYongyaoList1(mzjldID, sy1);
                    }
                    else
                    {
                        sy1.Bz = 2; sy1.JsTime = sy1.KsTime;
                        m = bll.addYongyaoList2(mzjldID, sy1);
                    }
                   
                    if (m > 0)
                        BindShuyeList();
                    else MessageBox.Show(sy1.Name + "—添加失败请重试！");
                }
                else
                {
                    MessageBox.Show("剂量、液体名不能为空");
                }
            }
            else MessageBox.Show("输液用量超标，请到其他用药出添加");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
                {
                    int m = bll.endYongyaoList(mzjldID, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindShuyeList();
                }
                else
                    MessageBox.Show("输液已经结束！");
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    int q = bll.deleteYaopinList(mzjldID, id);
                    if (q > 0)
                        BindShuyeList();
                }
            }
            else
                MessageBox.Show("请选择需要删除的液体！");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("输液", cmbName.Text.Trim());
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindShuyeList()
        {
            DataTable dtSy = _YongyaoListDal.GetYongyaoList(mzjldID, (int)EnumCreator.YongyaoType.输液);//4输液
            dataGridView1.DataSource = dtSy.DefaultView;
            syList.Clear();
            for (int i = 0; i < dtSy.Rows.Count; i++)
            {
                if (!syList.Contains(dtSy.Rows[i]["name"].ToString()))
                {
                    syList.Add(dtSy.Rows[i]["name"].ToString());
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
        private void addshuye_Load(object sender, EventArgs e)
        {
            BindDW();
            BindZRFS();
            BindYaoPin();
            BindShuyeList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);
        }

        private void cmbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        private void cmbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByType("输液",cmbName.Text.Trim());
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
            
        }
    }
}
