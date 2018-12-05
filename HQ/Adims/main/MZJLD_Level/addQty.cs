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
    public partial class addQty : Form
    {
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
        int mzjldid;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        List<adims_MODEL.mzyt> mzyt;
        //List<adims_MODEL.mzqt> mzqt;
        ArrayList sss = new ArrayList();
        public addQty( int id)
        {
            mzjldid = id;           
            InitializeComponent();
        }
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        adims_DAL.PacuDal _PacuDal = new adims_DAL.PacuDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByType("气体药", cmbName.Text.Trim());
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
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
        private void BindQtList()
        {
            sss.Clear();
            DataTable dtYDY = _YongyaoListDal.GetYongyaoList(mzjldid, (int)EnumCreator.YongyaoType.气体药);//1指气体药
            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["name"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["name"].ToString());
                }
            }
        }
        private void addqt_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindQtList();
            BindDW();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sss.Count < 3 || (sss.Count == 3 &&sss.Contains(cmbName.Text.Trim())))
            {
                if (cmbName.Text != "" && tbYL.Text.Trim() != "")
                {
                    adims_MODEL.Yongyao mz1 = new adims_MODEL.Yongyao();
                    mz1.Name = cmbName.Text.Trim();
                    mz1.Yl = Convert.ToDouble(tbYL.Text);
                    mz1.Dw = cmbDW.Text;
                    mz1.KsTime = DateTime.Now;
                    mz1.Bz = 1;
                    mz1.YpType = 1;
                    int m = bll.addYongyaoList1(mzjldid, mz1);
                    if (m > 0)
                    {
                        BindQtList();
                    }
                }
                else
                {
                    MessageBox.Show("用量和药名不能为空");
                }
            }
            else MessageBox.Show("气体数量超标！");           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString() == "1")
            {
                int p = bll.endYongyaoList(mzjldid,Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                BindQtList();
            }
            else MessageBox.Show("气体用药已经结束。");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count ==1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int q = bll.deleteYaopinList(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                   if (q > 0)
                   {
                       BindQtList();
                   }
                }
            }
            else
                MessageBox.Show("请选择需要删除的气体药！");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e);

        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            cmbName.Text = dataGridView1.SelectedRows[0].Cells["Names"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["yl"].Value.ToString();           
            cmbDW.Text = dataGridView1.SelectedRows[0].Cells["dw"].Value.ToString();
        }

        private void cmbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        private void cmbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByType("气体药", cmbName.Text.Trim());
                cmbName.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    cmbName.Items.Add(dr[1].ToString());
               }
                cmbName.Text = "";
                cmbName.DroppedDown = true;
            }
        }   


    }

}
