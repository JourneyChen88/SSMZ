using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class addshuye : Form
    {

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<adims_MODEL.shuye> SYlist;
        int mzjldID;
        DateTime ksTime;
        public addshuye(List<adims_MODEL.shuye> list,DateTime dt,int id)
        {
            SYlist = list;
            ksTime = dt;
            mzjldID = id;
            InitializeComponent();
        }
        public addshuye( DateTime dt, int id)
        {           
            ksTime = dt;
            mzjldID = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && cmbName.Text.Trim() != "")
            {
                adims_MODEL.shuye jtytsx = new adims_MODEL.shuye();
                jtytsx.Bz = 1;
                jtytsx.Name = cmbName.Text;
                jtytsx.Jl = Convert.ToDouble(textBox1.Text);
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
            DataTable dt = dal.GetAdims_YaoPinByType("输液");
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindShuyeList()
        {
            DataTable dtSX = bll.selectSY1(mzjldID);
            dataGridView1.DataSource = dtSX.DefaultView;

        }
        private void addshuye_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuyeList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }
    }
}
