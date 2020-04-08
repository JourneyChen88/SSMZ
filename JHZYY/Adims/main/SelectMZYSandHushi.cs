using Adims_Utility;
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

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int Type = 0;
        private string strdata;//勾选的医生和护士

        public string Strdata
        {
            get { return strdata; }
            set { strdata = value; }
        }

        WindowsFormsControlLibrary5.UserControl1 UC;
        Dictionary<string, string> dic;
        public SelectMZYSandHushi(int type, WindowsFormsControlLibrary5.UserControl1 o, Dictionary<string, string> _dic = null)
        {
            dic = _dic;
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
            dataGridView1.DataSource = bll.selectUserName(Type);
            DataGridViewColumn column0 = new DataGridViewCheckBoxColumn();
            column0.HeaderText = "是否添加";
            column0.Name = "IsAdd";
            dataGridView1.Columns.Add(column0);
            dataGridView1.Columns[0].ReadOnly = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Name = "";
            if (dic != null)
            {
                dic.Clear();
            }
            else
            {
                dic = new Dictionary<string, string>();
            }

            int count = dataGridView1.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[2];
                if (Convert.ToBoolean(checkCell.Value))
                {
                    dic.Add(dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[0].Value.ToString());
                    if (Name.IsNullOrEmpty())
                        Name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    else
                        Name = Name + "、" + dataGridView1.Rows[i].Cells[0].Value.ToString();
                }
            }
            UC.Controls[0].Text = Name;
            strdata = Name;
            this.Close();
        }
    }
}