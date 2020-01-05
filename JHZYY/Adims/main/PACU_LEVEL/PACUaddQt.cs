using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace main.PACU_LEVEL
{
    public partial class PACUaddQt : Form
    {
        int mzjldid;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<adims_MODEL.mzqt> mzqt;
        ArrayList sss = new ArrayList();
        DateTime dtStart = new DateTime();
        public PACUaddQt(List<adims_MODEL.mzqt> mzqt1, int id)
        {
            mzjldid = id;
            mzqt = mzqt1;
            InitializeComponent();
        }
        public PACUaddQt(DateTime dtStart1, int id)
        {
            mzjldid = id;
            dtStart = dtStart1;
            InitializeComponent();
        }

        private void BindQtList()
        {
            sss.Clear();
            DataTable dtYDY = bll.select_qtPACU(mzjldid);
            dataGridView1.DataSource = dtYDY.DefaultView;
            for (int i = 0; i < dtYDY.Rows.Count; i++)
            {
                if (!sss.Contains(dtYDY.Rows[i]["qtname"].ToString()))
                {
                    sss.Add(dtYDY.Rows[i]["qtname"].ToString());
                }
            }
        }
        private void PACUaddQt_Load(object sender, EventArgs e)
        {
            BindQtList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.mzqt mz1 = new adims_MODEL.mzqt();
                mz1.Qtname = comboBox1.Text;
                mz1.Yl = Convert.ToDouble(textBox1.Text);
                mz1.Dw = comboBox2.Text;
                mz1.Sysj = dtStart;
                mz1.Bz = 1;
                int m = bll.addqtPACU(mzjldid, mz1);
                if (m > 0) 
                    BindQtList();
                else 
                    MessageBox.Show("添加失败");
            }
            else MessageBox.Show("用量不能为空");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (dataGridView1.SelectedRows[0].Cells["flags"].Value.ToString()=="1")
                {
                    int p = bll.endqtPACU(mzjldid, DateTime.Now, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    BindQtList();
                }
                else MessageBox.Show("气体用药已经结束。");                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int q = bll.delqtPACU(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                BindQtList();
                if (q != 1)
                { MessageBox.Show("删除失败"); }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender, e);
        }

    }

}