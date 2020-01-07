using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;
using adims_BLL;

namespace main.权限管理
{
    public partial class Add_users : Form
    {
        public Add_users()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet users = new DataSet();

        private void Add_users_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bll.selectUser();
            DataSet pj_set = new DataSet();
            DataTable pj_table = new DataTable();
            pj_set = bll.Select_PJ();
            pj_table = pj_set.Tables[0];
            foreach (DataRow myrow in pj_table.Rows)
            {
                cmbPosition.Items.Add(myrow[1]);
            }            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim().IsNullOrEmpty())
            {
                MessageBox.Show("请填写真实姓名！");
                tbName.Focus();
            }else if (tbUid.Text.Trim()=="")
            {
                MessageBox.Show("请填写登陆账号！");
                tbUid.Focus();
            }else if(tbPwd.Text.Trim()=="")
            {
                MessageBox.Show("请填写登陆密码！");
                tbPwd.Focus();
            }
            else
            {
                user_info user = new user_info();
                user.user_name = tbName.Text.Trim();
                DataTable dt = bll.selectCount_user(user.user_name);
                int i = Convert.ToInt32(dt.Rows[0][0]);
                if (i > 0)
                {
                    MessageBox.Show("用户名不能重复添加！");
                }
                else
                {
                    user.uid = tbUid.Text.Trim();
                    user.password = tbPwd.Text.Trim();
                    user.position = cmbPosition.Text.Trim();
                    if (cmbType.Text.Trim() == "管理员")
                    {
                        user.type = 0;
                    }
                    else if (cmbType.Text.Trim() == "医师")
                    {
                        user.type = 1;
                    }
                    else
                    {
                        user.type = 2;
                    }
                    user.HisCode = txtHis.Text.Trim();
                    bll.Add_user(user);
                    DataTable  users= bll.selectUser();
                    dataGridView1.DataSource = users;
                    MessageBox.Show("用户添加成功！");
                }
            }
        }
        bool update_flag = true;
        private void button2_Click(object sender, EventArgs e)
        { 
                if (dataGridView1.SelectedCells.Count == 0) 
                { MessageBox.Show("请选择要修改的行", "警告"); return; }
                user_info user = new user_info();
                user.uid = tbUid.Text.Trim();
                user.user_name = tbName.Text.Trim();
                user.password = tbPwd.Text.Trim();
                user.position = cmbPosition.Text.Trim();
                int id1 = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                if (cmbType.Text.Trim() == "管理员")
                {
                    user.type = 0;
                }
                else if (cmbType.Text.Trim() == "医师")
                {
                    user.type = 1;
                }
                else
                {
                    user.type = 2;
                }
                user.HisCode = txtHis.Text.Trim();
                bll.Update_user(user,id1);
                dataGridView1.DataSource = bll.selectUser();
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) 
            { MessageBox.Show("请选择要删除的用户", "警告"); return; }
            else if (MessageBox.Show("您真的要删除吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int row_index = dataGridView1.SelectedCells[0].RowIndex;
                user_info user = new user_info();
                user.uid = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
                bll.Delete_user(user);
                DataTable users = bll.selectUser();
                dataGridView1.DataSource = users;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
               tbUid.Text=dataGridView1.CurrentRow.Cells[1].Value.ToString();
               tbPwd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
               tbName.Text=dataGridView1.CurrentRow.Cells[3].Value.ToString();
               cmbPosition.Text=dataGridView1.CurrentRow.Cells[4].Value.ToString();
               cmbType.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
             
            }
        }
    }
}
