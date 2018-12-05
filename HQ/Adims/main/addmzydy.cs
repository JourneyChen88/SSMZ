using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;

namespace main
{
    public partial class addmzydy : Form
    {
        #region <<Members>>

        AdimsProvider apro = new AdimsProvider();
        AdimsController acon = new AdimsController();
        ListBox listboxt;
        WindowsFormsControlLibrary5.UserControl1 u;
        int mzjldid;
        #endregion

        #region <<Constructors>>
      

        /// <summary>
        /// 构造函数2
        /// </summary>
        /// <param name="lb"></param>
       
        public addmzydy(ListBox lb,int mzid)
        {
            listboxt = lb;
            mzjldid = mzid;
           
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                int i = acon.InsertIntoYDY(mzjldid, textBox1.Text, comboBox1.Text, textBox2.Text, comboBox2.Text,DateTime.Now);
                //int shuliang = dataGridView1.RowCount;
                //listboxt.Items.Add(shuliang+"." + textBox1.Text + "   " + textBox2.Text + comboBox2.Text);
                //listboxt.Tag = shuliang;
                if (i>0)
                {
                    MessageBox.Show("诱导药添加成功");
                }
                datagridBind();
                listboxtBind();

              
            }
            else
            MessageBox.Show("药品名称和用量不能为空！");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (dataGridView1.SelectedCells.Count == 1)
                {

                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    int i = acon.deleteYDYUse(ID);

                    if (i > 0)
                    {
                        MessageBox.Show("诱导药删除成功");

                    }
                    datagridBind();
                    listboxtBind();
                }
                else
                    MessageBox.Show("请选择已使用的诱导药！");
            }

        }

      

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        #endregion

        
        #region<<绑定用药数据>>


        private void listboxtBind()
        {
            listboxt.Items.Clear();
            DataTable dt = apro.GetMzydyUSE(mzjldid);
            if (dt.Rows.Count != 0)
            {
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    listboxt.Items.Add(i + 1 + "." + dt.Rows[i][2] + "   " + dt.Rows[i][4] + dt.Rows[i][5]);
                
                }
                
            }

        }
        private void datagridBind()
        {
            DataTable dt = apro.GetMzydyUSE(mzjldid);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                
            }

        }

        #endregion

        private void addmzydy_Load(object sender, EventArgs e)
        {
            this.Text = "111";
            datagridBind();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }
    }
}
