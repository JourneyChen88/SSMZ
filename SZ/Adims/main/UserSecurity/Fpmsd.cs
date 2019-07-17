using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;

namespace main.UserSecurity
{
    public partial class Fpmsd : Form
    {
        public Fpmsd()
        {
            InitializeComponent();
        }
        Keys presskey;
        user_info Customer = new user_info();
        private void Fpmsd_Load(object sender, EventArgs e)
        {
            button1.Text = "屏幕已被" + Program.Customer.user_name + "锁定";
            Customer.uid = Program.Customer.uid;

        }
        bool flag = false;
        private void button2_Click(object sender, EventArgs e)
        {
            LogIn newlogin = new LogIn();
            newlogin.ShowDialog();
            if (newlogin.DialogResult == DialogResult.OK)
            {
                if (Program.Customer.uid == Customer.uid) { flag = true; this.Close(); }
                else
                {
                    MessageBox.Show("登录用户与锁定用户不一致", "警告");
                }
            }
        }

        private void Fpmsd_KeyDown(object sender, KeyEventArgs e)
        {
            presskey = e.KeyCode;
            /*   if ((e.KeyCode == Keys.F4) && (e.Alt == true))
               {
                   e.Handled = true;
               }
            */    //另一种屏蔽Alt+F4的有效方法
        }

        private void Fpmsd_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    if (presskey == Keys.F4)
                    {
                        e.Cancel = true;
                    }
                } 
             */    //一种屏蔽Alt+F4的有效方法
            if (!flag) e.Cancel = true;
        }
    }
}
