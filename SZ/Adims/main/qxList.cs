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
    public partial class qxList : Form
    {
        adims_DAL.AdimsProvider apro = new adims_DAL.AdimsProvider();
        int dr, dc;
        DataGridView dgv;
        int rows;
        public qxList(DataGridView d, int r, int c)
        {
            dgv = d;
            dr = r;
            dc = c;
            InitializeComponent();
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=null&&textBox1.Text!=null)
            {
                DataTable dt1 = apro.GetIsInQxmc(textBox1.Text);
                if (dt1.Rows.Count>0)
                {
                    MessageBox.Show("器械不能重复添加！");
                }
                else
                {
                    int i = apro.InsertQxmc(textBox1.Text, textBox2.Text);
                    if (i > 0)
                    {
                        BindDateGridView();
                        MessageBox.Show("器械添加成功！");
                    }
                    else
                    {
                        MessageBox.Show("器械添加失败，请重试！");
                    }
                        
                }
                
            }
            else
                MessageBox.Show("器械名称或缩写不能为空");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                string qxmc = dataGridView1.Rows[rows].Cells[1].Value.ToString();
                int i = apro.Deleteqxmc(qxmc);
                if (i > 0)
                {
                    BindDateGridView();
                    MessageBox.Show("器械删除成功！");
                }
                else
                {
                    MessageBox.Show("器械删除失败，请重试！");
                }
            }
            else
                MessageBox.Show("请选择需要删除的器械");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rows = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[rows].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rows].Cells[2].Value.ToString();
        }

        private void qxList_Load(object sender, EventArgs e)
        {
            BindDateGridView();
        }

        private void BindDateGridView()
        {
            DataTable dt = apro.GetallQxmc();
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
            this.Close();
        }
       
    }
}
