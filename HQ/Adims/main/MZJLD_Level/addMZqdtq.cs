using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace main
{
    public partial class addMZqdtq : Form
    {
        UserControl u;
        string Temp;
        public addMZqdtq(UserControl o, string temp)
        {
            u = o;
            Temp = temp;
            InitializeComponent();
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMZqdtq_Load(object sender, EventArgs e)
        {
            if (Temp.Contains("简易面罩")) ckqdtq1.Checked = true;
            if (Temp.Contains("鼻塞管")) ckqdtq2.Checked = true;
            if (Temp.Contains("口咽通气道")) ckqdtq3.Checked = true;
            if (Temp.Contains("鼻咽通气道")) ckqdtq4.Checked = true;
            if (Temp.Contains("鼻导管")) ckqdtq5.Checked = true;
            if (Temp.Contains("T型管")) ckqdtq6.Checked = true;
            if (Temp.Contains("贝因回路")) ckqdtq7.Checked = true;
            if (Temp.Contains("循环紧闭")) ckqdtq8.Checked = true;
            if (Temp.Contains("给氧")) ckqdtq9.Checked = true;
            if (Temp.Contains("自主")) ckqdtq10.Checked = true;
            if (Temp.Contains("间歇正压")) ckqdtq11.Checked = true;
            if (Temp.Contains("辅助呼吸")) ckqdtq12.Checked = true;
            if (Temp.Contains("SIMV")) ckqdtq13.Checked = true;
            if (Temp.Contains("peep"))
            {
                string[] newArr = Regex.Split(Temp, "peep", RegexOptions.IgnoreCase);

                if (newArr.Length>1)
                {
                    if (newArr[1].Contains("cmH20"))
                    {
                        linePEEP.Text = newArr[1].Replace("cmH20", "");
                    }
                   
                }
            }


        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            if (ckqdtq1.Checked)
            {
                str += " " + "简易面罩";
            }
            if (ckqdtq2.Checked)
            {
                str += " " + "鼻塞管";
            }
            if (ckqdtq3.Checked)
            {
                str += " " + "口咽通气道";
            }
            if (ckqdtq4.Checked)
            {
                str += " " + "鼻咽通气道";
            }
            if (ckqdtq5.Checked)
            {
                str += " " + "鼻导管";
            }
            if (ckqdtq6.Checked)
            {
                str += " " + "T型管";
            }
            if (ckqdtq7.Checked)
            {
                str += " " + "贝因回路";
            }
            if (ckqdtq8.Checked)
            {
                str += " " + "循环紧闭";
            }
            if (ckqdtq9.Checked)
            {
                str += " " + "给氧";
            }
            if (ckqdtq10.Checked)
            {
                str += " " + "自主";
            }
            if (ckqdtq11.Checked)
            {
                str += " " + "间歇正压";
            }
            if (ckqdtq12.Checked)
            {
                str += " 辅助呼吸";
            }
            if (ckqdtq13.Checked)
            {
                str += " SIMV";
            }
            if (!string.IsNullOrEmpty(linePEEP.Text.Trim()))
            {
                str += " peep" + linePEEP.Text.Trim() + "cmH2O";
            }

            u.Text = str;
        }

        private void lineBox1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ckqdtq13_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
