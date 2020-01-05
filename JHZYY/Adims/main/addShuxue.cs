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
    public partial class addShuxue : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
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
            DataTable dt = dal.GetAdims_YaoPinByType("输血", cmbName.Text.Trim());
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
        private void BindShuxueList()
        {
            DataTable dtSX = bll.selectSX1(mzjldID);
            dataGridView1.DataSource = dtSX.DefaultView;
            syList.Clear();
            for (int i = 0; i < dtSX.Rows.Count; i++)
            {
                if (!syList.Contains(dtSX.Rows[i]["shuxuename"].ToString()))
                {
                    syList.Add(dtSX.Rows[i]["shuxuename"].ToString());
                }
            }
        }

        private void addshuxue_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuxueList();
            BindDW();
            BindFS();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (syList.Count < 4 || (syList.Count == 4 && syList.Contains(cmbName.Text.Trim())))
            {
                if (tbYL.Text != "" && cmbName.Text.Trim() != "")
                {
                    adims_MODEL.shuxue jtytsx = new adims_MODEL.shuxue();
                    jtytsx.Bz = 1;
                    jtytsx.Name = cmbName.Text;
                    jtytsx.Jl = Convert.ToDouble(tbYL.Text);
                    jtytsx.Dw = cmbDW.Text;
                    jtytsx.Zrfs = cmbZRFS.Text;
                    //jtytsx.Ytlx = type;
                    jtytsx.Kssj = ksTime;
                    int q = bll.addshuxue(mzjldID, jtytsx);
                    if (q > 0)
                        BindShuxueList();
                }
                else MessageBox.Show("剂量、输血名不能为空");
            }
            else MessageBox.Show("输血标记超标，请到其他用药处添加。");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id=Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int q = bll.endshuxue(mzjldID, id);
                if (q > 0)
                    BindShuxueList();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                int q = bll.delshuxue(mzjldID, id);
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
                DataTable dt = dal.GetAdims_YaoPinByType("输血", cmbName.Text.Trim());
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
            cmbName.Text = dataGridView1.SelectedRows[0].Cells["shuxuename"].Value.ToString();
            tbYL.Text = dataGridView1.SelectedRows[0].Cells["jl"].Value.ToString();
            cmbZRFS.Text = dataGridView1.SelectedRows[0].Cells["zrfs"].Value.ToString();
            cmbDW.Text = dataGridView1.SelectedRows[0].Cells["dw"].Value.ToString();
        }
       
    }
}
