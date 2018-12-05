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
    public partial class PACU_AddYyao : Form
    {
        adims_DAL.PacuDal apro = new adims_DAL.PacuDal();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
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
                textBox1.Text = listBox1.SelectedItem.ToString();
        }
        private void datagridBind()
        {
            DataTable dt = apro.GetPACUyy(mzjldid);
           
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;

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
        /// <summary>
        /// 用药方式
        /// </summary>
        private void BindZRFS()
        {
            DataTable dtdw = DAL.GetAllYD_ZRFS();
            comboBox2.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.comboBox2.Items.Add(dtdw.Rows[i][0]);
            }
        }
        private void PACU_AddYyao_Load(object sender, EventArgs e)
        {
            datagridBind();
            BindDW();
            BindZRFS();
            BindYaoPin();
        }
        adims_DAL.PacuDal dal = new adims_DAL.PacuDal();
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByPinyin(tbPinyin.Text.Trim());
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                adims_MODEL.tsyy ts = new adims_MODEL.tsyy();
                ts.D = DateTime.Now;
                ts.Name = textBox1.Text;
                ts.Yl = Convert.ToInt32(textBox2.Text);
                ts.Dw = cmbDW.Text;
                ts.Yyfs = comboBox2.Text;
                tsyyList.Add(ts);
                int m = apro.addPACUyy(mzjldid, ts);
                if (m != 1)
                { MessageBox.Show("错误"); }
                datagridBind();
                this.Close();
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
                    foreach (adims_MODEL.tsyy ts in tsyyList)
                    {
                        if (ts.Name == dataGridView1.CurrentRow.Cells[2].Value.ToString())
                        {
                            tsyyList.Remove(ts);
                        }
                        break;
                    }
                    if (i > 0)
                    {
                        MessageBox.Show("特殊用药删除成功");
                    }
                    datagridBind();
                    this.Close();
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
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void listBox1_Click_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByPinyin(tbPinyin.Text.Trim());
                listBox1.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    listBox1.Items.Add(dr[1].ToString());
                }
                textBox1.Text = "";
            }
        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }
    }
}
