using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;

namespace main
{
    public partial class addMZPM : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController(); 

        int mzjldid;
        DateTime mzpmTime = new DateTime();
        List<mzpingmian> mzpm = new List<mzpingmian>();
        public addMZPM(List<mzpingmian> PINGMIAN, DateTime dt, int ID)
        {
            mzjldid = ID;
            mzpmTime = dt;
            mzpm = PINGMIAN;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbMZPM.Text.Trim() != "")
            {
                adims_MODEL.mzpingmian q = new adims_MODEL.mzpingmian();
                q.mzpmName = cmbMZPM.Text.Trim()+cmbName2.Text.Trim();
                q.D = mzpmTime;
                int i = bll.addmzpm(mzjldid, q);
                if (i != 1)
                {
                    MessageBox.Show("添加失败，请重试！");
                }
                q.Id = bll.GETmzpm_ID(mzjldid, q);
                mzpm.Add(q);

                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataBind()
        {
            DataTable dt = bll.GETALLmzpm(mzjldid);
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int q = 0;
                foreach (adims_MODEL.mzpingmian mz in mzpm)
                {
                    if (mz.Id == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value))
                    {
                        q = bll.delmzpm(mzjldid, mz);
                        mzpm.Remove(mz);
                    }
                    if (q == 1)
                        break;
                }
            }
            dataBind();
        }

        private void addMZPM_Load(object sender, EventArgs e)
        {
            dataBind();
        }
    }
}
