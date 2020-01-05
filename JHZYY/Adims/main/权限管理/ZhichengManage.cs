using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_MODEL;

namespace main.权限管理
{
    public partial class ZhichengManage : Form
    {
        DataSet pos_set = new DataSet();
        AdimsController bll = new AdimsController(); 
        public ZhichengManage()
        {
            InitializeComponent();
        }

        private void maintenance_Load(object sender, EventArgs e)
        {
            pos_set = bll.Select_PJ();
            dataGridView1.DataSource = pos_set.Tables[0];

        }

        private void button1_Click(object sender, EventArgs e)
        {
            position_jurisdiction pj = new position_jurisdiction();
            pj.position = position_txt.Text; 
            bll.Add_position(pj);
            pos_set = bll.Select_PJ();
            dataGridView1.DataSource = pos_set.Tables[0];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) { MessageBox.Show("请选择要删除的岗位", "警告"); return; }
            else if (MessageBox.Show("您真的要删除吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int i = dataGridView1.SelectedCells[0].RowIndex;
                position_jurisdiction pj = new position_jurisdiction();
                pj.ID = (int)dataGridView1.Rows[i].Cells[0].Value;
                bll.Delete_PJ(pj);
                bll.Delete_user_position(dataGridView1.Rows[i].Cells[1].Value.ToString());
                pos_set = bll.Select_PJ();
                dataGridView1.DataSource = pos_set.Tables[0];
            }
        }

        int flag = 0;
        int i = 0;
        public  struct cell_pos
        {
           public int row_index;
           public int col_index;
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (flag == 0)
            {                        
                int count = dataGridView1.SelectedCells.Count;
                i = dataGridView1.SelectedCells[count-1].RowIndex;
                //   dataGridView1.Rows[i].Cells[1].Value = null;
                List<cell_pos> mylist=new List<cell_pos>();
                for (int j = 0; j < count; j++)
                {   cell_pos mycell=new cell_pos();
                    mycell.row_index = dataGridView1.SelectedCells[j].RowIndex;
                    mycell.col_index = dataGridView1.SelectedCells[j].ColumnIndex;
                    mylist.Add(mycell);
                }
                for (int j = 0; j < count; j++) dataGridView1.Rows[mylist[j].row_index].Cells[mylist[j].col_index].Selected = false; 
                dataGridView1.Rows[i].Cells[1].Selected = true;
                position_txt.Text="";
                position_txt.Focus();
                button2.Text = "保存";
                flag = 1;
            }
            else 
            {
                position_jurisdiction pj = new position_jurisdiction();
                pj.ID = (int)dataGridView1.Rows[i].Cells[0].Value;
                if (position_txt.Text == "") { MessageBox.Show("请填写职务名称", "警告"); return; }
                pj.position = position_txt.Text;
                bll.Update_position(pj);
                bll.Update_user_position(dataGridView1.Rows[i].Cells[1].Value.ToString(), position_txt.Text);                
                button2.Text = "修改";
                flag = 0;
                pos_set = bll.Select_PJ();
                dataGridView1.DataSource = pos_set.Tables[0];
            }         
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}