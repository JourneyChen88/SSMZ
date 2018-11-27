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
    public partial class modify_password : Form
    {
        public modify_password()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        user_info customer = new user_info();
        private void button1_Click(object sender, EventArgs e)
        {
            string old_password = old_txt.Text.ToString().Trim();
            string new_password1 = new1_txt.Text.ToString().Trim();
            string new_password2 = new2_txt.Text.ToString().Trim();
            if (old_password != Program.customer.password)
            {
                MessageBox.Show("旧密码输入错误，你没有修改权限","警告");
                old_txt.Text = "";
                return;
            }
            if(new_password1!=new_password2)
            {
                MessageBox.Show("两次输入的新密码不一样", "警告");
                new1_txt.Text = "";
                new2_txt.Text = "";
                return;
            }
            Program.customer.password = new_password1;
            bll.Update_user(Program.customer);
            MessageBox.Show("恭喜，密码修改成功", "信息");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
