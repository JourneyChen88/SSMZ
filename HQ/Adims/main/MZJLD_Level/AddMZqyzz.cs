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
    public partial class AddMZqyzz : Form
    {
        WindowsFormsControlLibrary5.UserControl1 u;
        string Temp;
        public AddMZqyzz(WindowsFormsControlLibrary5.UserControl1 o, string temp)
        {
            u = o;
            Temp = temp;
            InitializeComponent();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            if (ckqyzz1.Checked)
            {
                str += " " + "颈丛";
            }
            if (ckqyzz2.Checked)
            {
                str += " " + "一点";
            }
            if (ckqyzz3.Checked)
            {
                str += " " + "两点";
            }
            if (ckqyzz4.Checked)
            {
                str += " " + "三点";
            }
            if (ckqyzz5.Checked)
            {
                str += " " + "单侧";
            }
            if (ckqyzz6.Checked)
            {
                str += " " + "双侧";
            }
            if (ckqyzz7.Checked)
            {
                str += " " + "臂丛";
            }
            if (ckqyzz8.Checked)
            {
                str += " " + "肌沟";
            }
            if (ckqyzz9.Checked)
            {
                str += " " + "锁上";
            }
            if (ckqyzz10.Checked)
            {
                str += " " + "锁下";
            }
            if (ckqyzz11.Checked)
            {
                str += " " + "腋路";
            }
            if (ckqyzz12.Checked)
            {
                str += " 局部静脉";
            }
            u.Text = str;
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMZqyzz_Load(object sender, EventArgs e)
        {
            if (Temp.Contains("颈丛")) ckqyzz1.Checked = true;
            if (Temp.Contains("一点")) ckqyzz2.Checked = true;
            if (Temp.Contains("两点")) ckqyzz3.Checked = true;
            if (Temp.Contains("三点")) ckqyzz4.Checked = true;
            if (Temp.Contains("单侧")) ckqyzz5.Checked = true;
            if (Temp.Contains("双侧")) ckqyzz6.Checked = true;
            if (Temp.Contains("臂丛")) ckqyzz7.Checked = true;
            if (Temp.Contains("肌沟")) ckqyzz8.Checked = true;
            if (Temp.Contains("锁上")) ckqyzz9.Checked = true;
            if (Temp.Contains("锁下")) ckqyzz10.Checked = true;
            if (Temp.Contains("腋路")) ckqyzz11.Checked = true;
            if (Temp.Contains("局部静脉")) ckqyzz12.Checked = true;
           
        }
    }
}
