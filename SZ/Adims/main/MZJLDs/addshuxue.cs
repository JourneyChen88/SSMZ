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
    public partial class addshuxue : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<adims_MODEL.shuxue> SXlist;
        int mzjldID;
        DateTime ksTime;
        public addshuxue(List<adims_MODEL.shuxue> list, DateTime dt, int id)
        {
            SXlist = list;
            mzjldID = id;
            ksTime = dt;
            InitializeComponent();
        }
        public addshuxue(DateTime dt, int id)
        {
            mzjldID = id;
            ksTime = dt;
            InitializeComponent();
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("输血");
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindShuxueList()
        {
            DataTable dtSX = bll.selectSX1(mzjldID);
            dataGridView1.DataSource = dtSX.DefaultView;
           
        }

        private void addshuxue_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuxueList();
          
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != ""&&cmbName.Text.Trim() != "")
            {
                    adims_MODEL.shuxue jtytsx = new adims_MODEL.shuxue();

                    jtytsx.Bz = 1;
                    jtytsx.Name = cmbName.Text;
                    jtytsx.Jl = Convert.ToDouble(textBox1.Text);
                    jtytsx.Dw = cmbDW.Text;
                    jtytsx.Zrfs = cmbZRFS.Text;                    
                    //jtytsx.Ytlx = type;
                    jtytsx.Kssj = ksTime;
                    int q = bll.addshuxue(mzjldID, jtytsx);
                    if (q > 0)
                        BindShuxueList();
                
            }
            else
            {
                MessageBox.Show("剂量、输血名不能为空");
            }
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
            
            else
                MessageBox.Show("请选择需要删除的血液！");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
