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

namespace main.PrivigeManage
{
    public partial class Add_users : Form
    {
        adims_DAL.DataManageClass dmc = new adims_DAL.DataManageClass();
        adims_DAL.DB2help his = new adims_DAL.DB2help();
        public Add_users()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataSet users = new DataSet();

        private void Add_users_Load(object sender, EventArgs e)
        {
            if (!UserFunction.PingHost(Program.Globals.SeverIp))
            {
                MessageBox.Show("网络中断");
                return;
            }
            dgvUsers.DataSource = dmc.select_user();
            string condiction = "and inputstr like '" + tbPinyin.Text.Trim() + "%'";
            dataGridView1.DataSource = his.GetHisUser(condiction);
            //DataSet pj_set = new DataSet();
            //DataTable pj_table = new DataTable();
            //pj_set = bll.Select_PJ();
            //pj_table = pj_set.Tables[0];
            //foreach (DataRow myrow in pj_table.Rows)
            //{
            //    cmbPosition.Items.Add(myrow[1]);
            //}
            //cmbPosition.SelectedIndex = 0;
            //cmbType.SelectedIndex = 0;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Trim() == "")
            {
                MessageBox.Show("请填写真实姓名！");
                tbUserName.Focus();
            }
            else if (tbUid.Text.Trim() == "")
            {
                MessageBox.Show("请填写登陆账号！");
                tbUid.Focus();
            }
            else if (tbPassword.Text.Trim() == "")
            {
                MessageBox.Show("请填写登陆密码！");
                tbPassword.Focus();
            }
            else if (tbUserNo.Text.Trim() == "")
            {
                MessageBox.Show("请填写用户编号！");
                tbUserNo.Focus();
            }
            else if (cmbPosition.Text.Trim() == "")
            {
                MessageBox.Show("请填写职称！");
                cmbPosition.Focus();
            }
            else if (cmbType.Text.Trim() == "")
            {
                MessageBox.Show("请填写分类！");
                cmbType.Focus();
            }
            else
            {
                user_info user = new user_info();
                user.userno = tbUserNo.Text.Trim();
                user.user_name = tbUserName.Text.Trim();
                user.uid = tbUid.Text.Trim();
                user.password = tbPassword.Text.Trim();
                user.position = cmbPosition.Text.Trim();
                if (cmbType.Text.Trim() == "管理员")
                    user.type = 0;
                else if (cmbType.Text.Trim() == "医师")
                    user.type = 1;
                else user.type = 2;
                DataTable dt = dmc.selectCount_user(user.userno);
                int i = Convert.ToInt32(dt.Rows[0][0]);
                int flag = 0;
                if (i > 0)
                    flag = dmc.Update_user(user);
                else
                    flag = dmc.Add_user(user);
                if (flag > 0)
                    MessageBox.Show("保存成功！");
            }

        }
        bool update_flag = true;
        private void button2_Click(object sender, EventArgs e)
        {
            //if (update_flag)
            //{
            //    if (dataGridView1.SelectedCells.Count == 0) { MessageBox.Show("请选择要修改的行", "警告"); return; }
            //    int row_index = dataGridView1.SelectedCells[0].RowIndex;
            //    uid_txt.Text = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
            //    uid_txt.ReadOnly = true;
            //    password_txt.Text = dataGridView1.Rows[row_index].Cells[1].Value.ToString();
            //    name_txt.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
            //    position_com.Text = dataGridView1.Rows[row_index].Cells[3].Value.ToString();
            //    button2.Text = "保存";
            //    update_flag = false;
            //}
            //else
            //{
            //    user_info user = new user_info();
            //    user.uid = uid_txt.Text.Trim();
            //    user.user_name = name_txt.Text.Trim();
            //    user.password = password_txt.Text.Trim();
            //    user.position = position_com.Text.Trim();
            //    if (user.position == "管理员")
            //    {
            //        user.type = 0;
            //    }
            //    else if (user.position == "副主任医师" || user.position == "主治医师" || user.position == "住院医师")
            //    {
            //        user.type = 1;
            //    }
            //    else
            //    {
            //        user.type = 2;
            //    }
            //    bll.Update_user(user);
            //    users = bll.select_user();
            //    dataGridView1.DataSource = users.Tables[0];
            //    button2.Text = "修改";
            //    update_flag = true;
            //    uid_txt.ReadOnly = false;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedCells.Count == 0) { MessageBox.Show("请选择要删除的用户", "警告"); return; }
            //else if (MessageBox.Show("您真的要删除吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    int row_index = dataGridView1.SelectedCells[0].RowIndex;
            //    user_info user = new user_info();
            //    user.uid = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
            //    bll.Delete_user(user);
            //    users = bll.select_user();
            //    dataGridView1.DataSource = users.Tables[0];
            //}
        }

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                string condiction = "and inputstr like '" + tbPinyin.Text.Trim().ToUpper() + "%'";
                DataTable dt1 = his.GetHisUser(condiction);
                dataGridView1.DataSource = dt1;
                tbPinyin.Text = "";
            }

        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                tbUserNo.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                tbUserName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                cmbPosition.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                //tbUserNo.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
