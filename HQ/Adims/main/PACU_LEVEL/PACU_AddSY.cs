using adims_Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.PACU_LEVEL
{
    public partial class PACU_AddSY : Form
    {

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        List<adims_MODEL.shuye> SYlist;
        int mzjldid;
        DateTime ksTime;
        public PACU_AddSY( DateTime dt, int id)
        {
           
            ksTime = dt;
            mzjldid = id;
            InitializeComponent();
        }
        adims_DAL.PacuDal dal = new adims_DAL.PacuDal();
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
            DataTable dtSX = bll.selectSY_pacu(mzjldid);
            dataGridView1.DataSource = dtSX.DefaultView;
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
        private void PACU_AddSY_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuyeList();
            BindDW();
            BindZRFS();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""||cmbName.Text.Trim() != "")
            {
                adims_MODEL.shuye jtytsx = new adims_MODEL.shuye();
                jtytsx.Bz = 1;
                jtytsx.Name = cmbName.Text;
                jtytsx.Jl = Convert.ToInt32(textBox1.Text);
                jtytsx.Dw = cmbDW.Text;
                jtytsx.Zrfs = cmbZRFS.Text;
                jtytsx.Kssj = ksTime;
                jtytsx.Cxyy = cbCXYY.Checked;             
                int m = 0;
                if (cbCXYY.Checked)
                {
                    jtytsx.Bz = 1;
                    m = bll.addshuyePACU(mzjldid, jtytsx);
                }
                else
                {
                    jtytsx.Bz = 2; jtytsx.Jssj = jtytsx.Kssj; 
                    m = bll.addshuyePACU2(mzjldid, jtytsx);
                }
                if (m > 0)
                    BindShuyeList();
                else MessageBox.Show(jtytsx.Name + "—添加失败请重试！");
                //int q = bll.addshuyePACU(mzjldid, jtytsx);
                //if (q > 0)
                //    BindShuyeList();
                //else
                //    MessageBox.Show("添加失败，请重试。");
            }
            else
            {
                MessageBox.Show("剂量、液体名称不能为空！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
                {
                    int m = bll.endshuyePACUs(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindShuyeList();
                }
                else
                    MessageBox.Show("输液已经结束！");
            }        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int q = bll.delshuyePACU(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                if (q>0)
                {
                    MessageBox.Show("删除成功！");
                    BindShuyeList(); 
                }
               
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbName.Text =dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells["jl"].Value.ToString();
            cmbDW.Text = dataGridView1.CurrentRow.Cells["dw"].Value.ToString();
            cmbZRFS.Text = dataGridView1.CurrentRow.Cells["zrff"].Value.ToString();
        }
    }
}
