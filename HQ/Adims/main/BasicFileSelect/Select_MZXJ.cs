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
    public partial class Select_MZXJ : Form
    {
         adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        TextBox UC;
        public Select_MZXJ(TextBox o)
        {
            UC = o;
            InitializeComponent();
        }

        private void Select_MZXJ_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DAL.GetAllYD_MZXJ();
            DataGridViewColumn column0 = new DataGridViewCheckBoxColumn();
            column0.HeaderText = "是否添加";
            column0.Name = "IsAdd";
            dataGridView1.Columns.Add(column0);
            dataGridView1.Columns[0].ReadOnly = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = "";
            int count = dataGridView1.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[1];
                Boolean flag = Convert.ToBoolean(checkCell.Value);
                if (flag == true)
                {
                    if (Name == "")
                        Name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    else
                        Name = Name + "、" + dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
            }
            UC.Text = Name;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
