using adims_DAL.Dics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsControlLibrary5;

namespace main
{
    public partial class SelectMZYSandHushi : Form
    {
        UserDal _UserDal = new UserDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int Type = 0;
        WindowsFormsControlLibrary5.UserControl1 _UC;
        UserControl UC;
        public SelectMZYSandHushi(int type,UserControl o)
        {
            Type = type;
            UC = o;
            InitializeComponent();
        }
        public SelectMZYSandHushi(int type, WindowsFormsControlLibrary5.UserControl1 o)
        {
            Type = type;
            UC = o;
            InitializeComponent();
        }

        private void SelectYSorHS_Load(object sender, EventArgs e)
        {
            if (Type == 1)
            {
                groupBox1.Text = "医生列表";
            }
            else
                groupBox1.Text = "护士列表";
            dataGridView1.DataSource = _UserDal.GetUserByType(Type);
            DataGridViewColumn column0 = new DataGridViewCheckBoxColumn();
            column0.HeaderText = "是否添加";
            column0.Name = "IsAdd";
            dataGridView1.Columns.Add(column0);
            dataGridView1.Columns[0].ReadOnly = true;
            if (Type == 1)
            {
                DataGridViewColumn column1 = new DataGridViewCheckBoxColumn();
                column1.HeaderText = "是否主麻";
                column1.Name = "IsZM";
                dataGridView1.Columns.Add(column1);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = "";
            string zm_name = "";
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
                if (Type == 1)
                {
                    DataGridViewCheckBoxCell checkCells = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[2];
                    Boolean flags = Convert.ToBoolean(checkCell.Value);
                    if (flags == true)
                    {
                        zm_name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    }
                }

            }
            UC.Text = Name;
            this.Close();
        }
    }
}