using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main.PACU_LEVEL
{
    public partial class PACU_AddYyao : Form
    {
        adims_DAL.PACU_DAL apro = new adims_DAL.PACU_DAL();
        int mzjldid;
        List<adims_MODEL.tsyy> tsyyList;
        public PACU_AddYyao(List<adims_MODEL.tsyy> tsyy1, int id)
        {
            mzjldid = id;
            tsyyList = tsyy1;
            InitializeComponent();

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                tbName1.Text = listBox1.SelectedItem.ToString();
        }
        private void datagridBind()
        {
            DataTable dt = apro.GetPACUyy(mzjldid);

            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

            }
        }
        private void PACU_AddYyao_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            datagridBind();
        }
        
        adims_DAL.PACU_DAL pdal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = pdal.GetAdims_YaoPinByType("特殊用药");
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            tbYl1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName1.Text == "")
            {
                MessageBox.Show("药名不能为空。");
                return;
            }
            if (tbYl1.Text == "")
            {
                MessageBox.Show("用量不能为空。");
                return;
            }
            else 
            {
                adims_MODEL.tsyy ts = new adims_MODEL.tsyy();
                ts.D = DateTime.Now;
                if (tbName2.Text.Trim() != "" && tbYl2.Text != "")
                {
                    ts.Name = tbName2.Text.Trim() + " " + tbYl2.Text.Trim()
                        + cmbDW2.Text.Trim() + "+" + tbName1.Text.Trim();
                }
                else
                    ts.Name = tbName1.Text.Trim();
                ts.Yl =  float.Parse(tbYl1.Text);
                ts.Dw = cmbDW1.Text;
                ts.Yyfs = comboBox2.Text;
               // tsyyList.Add(ts);
                int m = apro.addPACUyy(mzjldid, ts);
                if (m != 1)
                { MessageBox.Show("错误"); }
                datagridBind();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView1.SelectedCells.Count == 1)
                {
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    int i = apro.deletePACUyy(ID);
                    //foreach (adims_MODEL.tsyy ts in tsyyList)
                    //{
                    //    if (ts.Name == dataGridView1.CurrentRow.Cells[2].Value.ToString())
                    //    {
                    //        tsyyList.Remove(ts);
                    //    }
                    //    break;
                    //}
                    if (i == 0)
                    {
                        MessageBox.Show("特殊用药失败");
                    }
                    datagridBind();
                }
                else
                    MessageBox.Show("请选择已使用的特殊用药！");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e);
        }

        private void listBox1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (tbName1.Text.Trim() == "")
                {
                    tbName1.Text = listBox1.SelectedItem.ToString();
                }
                else
                    tbName2.Text = listBox1.SelectedItem.ToString();
            }
                
        }
    }
}
