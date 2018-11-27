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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "显示对话框";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //启动定时器
            timer1.Interval = 3000;
            timer1.Start();
            // 显示对话框
            MessageBox.Show("3秒钟，这个对话框后自动关闭！",
                "自动关闭的对话框",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information);
        }

       
    }
}
