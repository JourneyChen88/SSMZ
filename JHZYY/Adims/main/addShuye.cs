using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace main
{
    public partial class addShuye : Form
    {

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
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
            if (syList.Count < 8 || (syList.Count == 8 && syList.Contains(cmbName.Text.Trim())))
            {
                if (tbYL.Text != "" && cmbName.Text.Trim() != "")
                {
                    adims_MODEL.shuye jtytsx = new adims_MODEL.shuye();
                    jtytsx.Bz = 1;
                    jtytsx.Name = cmbName.Text;
                    jtytsx.Jl = Convert.ToDouble(tbYL.Text);
                    jtytsx.Dw = cmbDW.Text;
                    jtytsx.Zrfs = cmbZRFS.Text;
                    jtytsx.Kssj = ksTime;
                    int q = bll.addshuye(mzjldID, jtytsx);
                    if (q > 0)
                        BindShuyeList();
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
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int q = bll.endshuye(mzjldID, id);
                if (q > 0)
                    BindShuyeList();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    int q = bll.delshuye(mzjldID, id);
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
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("输液", cmbName.Text.Trim());
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
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
        private void BindFS()
        {
            DataTable dt = dal.GetAdims_fs();
            cmbZRFS.DataSource = dt;
            cmbZRFS.ValueMember = "id";
            cmbZRFS.DisplayMember = "name";
        }
        private void BindShuyeList()
        {
            DataTable dtSy = bll.selectSY1(mzjldID);
            dataGridView1.DataSource = dtSy.DefaultView;
            syList.Clear();
            for (int i = 0; i < dtSy.Rows.Count; i++)
            {
                if (!syList.Contains(dtSy.Rows[i]["shuyename"].ToString()))
                {
                    syList.Add(dtSy.Rows[i]["shuyename"].ToString());
                }
            }
        }
        private void addshuye_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuyeList();
            BindDW();
            BindFS(); 
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void cmbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        private void cmbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = dal.GetAdims_YaoPinByType("输液",cmbName.Text.Trim());
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
            cmbName.Text = dataGridView1.SelectedRows[0].Cells["shuyename"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["jl"].Value.ToString();
            cmbZRFS.Text = dataGridView1.SelectedRows[0].Cells["zrfs"].Value.ToString();
            cmbDW.Text = dataGridView1.SelectedRows[0].Cells["dw"].Value.ToString();
            
        }
    }
}
