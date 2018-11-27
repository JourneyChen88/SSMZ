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
    public partial class PACU_AddSJ : Form
    { adims_DAL.PACU_DAL apro = new adims_DAL.PACU_DAL();
        int mzjldid;
        List<adims_MODEL.szsj> sjList;
        public PACU_AddSJ(List<adims_MODEL.szsj> tsyy1, int id)
        {
            mzjldid = id;
            sjList = tsyy1;
            InitializeComponent();
          
        }
        public PACU_AddSJ()
        {
            InitializeComponent();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                textBox1.Text = listBox1.SelectedItem.ToString();
        }
        private void datagridBind()
        {
            DataTable dt = apro.GetPACUsj(mzjldid);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

            }
        }
        private void PACU_AddSJ_Load(object sender, EventArgs e)
        {
            datagridBind();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.szsj s = new adims_MODEL.szsj();
                s.D = dateTimePicker1.Value;
                s.Name = textBox1.Text;
                sjList.Add(s);
                //int i = apro.addPACUsj(mzjldid, s);
                //if (i > 0)
                //{
                //    MessageBox.Show("事件添加成功");
                //}
                //datagridBind();
                //this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView1.SelectedCells.Count == 1)
                {
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    int i = apro.deletePACUsj(ID);
                    foreach (adims_MODEL.szsj mz in sjList)
                    {
                        if (mz.Name == dataGridView1.CurrentRow.Cells[2].Value.ToString())
                        {
                            sjList.Remove(mz);
                        }
                        break;
                    }

                    if (i > 0)
                    {
                        MessageBox.Show("事件删除成功");
                    }
                    datagridBind();
                    this.Close();
                }
                else
                    MessageBox.Show("请选择事件！");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
