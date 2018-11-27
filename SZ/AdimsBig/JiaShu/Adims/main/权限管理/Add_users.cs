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
            users = bll.select_user();
            dataGridView1.DataSource = users.Tables[0];
            DataSet pj_set = new DataSet();
            DataTable pj_table = new DataTable();
            pj_set = bll.Select_PJ();
            pj_table = pj_set.Tables[0];
            foreach (DataRow myrow in pj_table.Rows)
            {
                position_com.Items.Add(myrow[1]);
            }
            position_com.SelectedIndex = 0;
            cmbposition_comType.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (name_txt.Text.Trim() == "")
            {
                MessageBox.Show("请填写真实姓名！");
                name_txt.Focus();
            }else if (uid_txt.Text.Trim()=="")
            {
                MessageBox.Show("请填写登陆账号！");
                uid_txt.Focus();
            }else if(password_txt.Text.Trim()=="")
            {
                MessageBox.Show("请填写登陆！");
                password_txt.Focus();
            }
            else
            {
                user_info user = new user_info();
                user.user_name = name_txt.Text.Trim();
                DataTable dt = bll.selectCount_user(user.user_name);
                int i = Convert.ToInt32(dt.Rows[0][0]);
                if (i > 0)
                {
                    MessageBox.Show("用户名不能重复添加！");
                }
                else
                {
                    user.uid = uid_txt.Text.Trim();
                    user.password = password_txt.Text.Trim();
                    user.position = position_com.Text.Trim();
                    if (user.position == "管理员")
                    {
                        user.type = 0;
                    }
                    else if (user.position == "副主任医师" || user.position == "主治医师" || user.position == "住院医师")
                    {
                        user.type = 1;
                    }
                    else
                    {
                        user.type = 2;
                    }
                    bll.Add_user(user);
                    users = bll.select_user();
                    dataGridView1.DataSource = users.Tables[0];
                    MessageBox.Show("用户名添加成功！");
                }
            }


        }
        bool update_flag = true;
        private void button2_Click(object sender, EventArgs e)
        {
            /*if (uid_txt.Text == "") { MessageBox.Show("用户账号不能为空", "警告"); return; }
            user_info user = new user_info();
            user.uid = uid_txt.Text;
            int row_count = dataGridView1.Rows.Count;
            int i=0;
            for (i = 0; i < row_count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == user.uid)
                {
                    dataGridView1.Rows[i].Selected = true; break;
                }
            }
            if (i == row_count) { MessageBox.Show("该用户账号不存在", "警告"); return; }
            user.password = password_txt.Text;
            user.user_name = name_txt.Text;
            user.position = position_com.Text;
            bll.Update_user(user);
            users = bll.select_user();
            dataGridView1.DataSource = users.Tables[0];*/
            //一种修改方式
            if (update_flag)
            {
                if (dataGridView1.SelectedCells.Count == 0) { MessageBox.Show("请选择要修改的行", "警告"); return; }
                int row_index = dataGridView1.SelectedCells[0].RowIndex;
                uid_txt.Text = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
                uid_txt.ReadOnly = true;
                password_txt.Text = dataGridView1.Rows[row_index].Cells[1].Value.ToString();
                name_txt.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
                position_com.Text = dataGridView1.Rows[row_index].Cells[3].Value.ToString();
                button2.Text = "保存";
                update_flag = false;
            }
            else
            {
                user_info user = new user_info();
                user.uid = uid_txt.Text.Trim();
                user.user_name = name_txt.Text.Trim();
                user.password = password_txt.Text.Trim();
                user.position = position_com.Text.Trim();
                if (user.position == "管理员")
                {
                    user.type = 0;
                }
                else if (user.position == "副主任医师" || user.position == "主治医师" || user.position == "住院医师")
                {
                    user.type = 1;
                }
                else
                {
                    user.type = 2;
                }
                bll.Update_user(user);
                users = bll.select_user();
                dataGridView1.DataSource = users.Tables[0];
                button2.Text = "修改";
                update_flag = true;
                uid_txt.ReadOnly = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) { MessageBox.Show("请选择要删除的用户", "警告"); return; }
            else if (MessageBox.Show("您真的要删除吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int row_index = dataGridView1.SelectedCells[0].RowIndex;
                user_info user = new user_info();
                user.uid = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
                bll.Delete_user(user);
                users = bll.select_user();
                dataGridView1.DataSource = users.Tables[0];
            }
        }
    }
}
