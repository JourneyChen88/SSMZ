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
    public partial class AddMZCG : Form
    {
        UserControl u;
        string Temp;
        public AddMZCG(UserControl o, string temp)
        {
            u = o;
            Temp = temp;
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddMZCG_Load(object sender, EventArgs e)
        {
          
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string str = "";
            if (ckcg1.Checked)
            {
                str += " " + "清醒"; 
            }
            if (ckcg2.Checked)
            {
                str += " " + "诱导";
            }
            if (ckcg3.Checked)
            {
                str += " " + "经口";
            }
            if (ckcg4.Checked)
            {
                str += " " + "左";
            }
            if (ckcg5.Checked)
            {
                str += " " + "右";
            }
            if (ckcg6.Checked)
            {
                str += " " + "双腔";
            }
            if (ckcg7.Checked)
            {
                str += " " + "左";
            }
            if (ckcg8.Checked)
            {
                str += " " + "右";
            }
            if (ckcg9.Checked)
            {
                str += " " + "纤维支气管镜";
            }
            if (ckcg10.Checked)
            {
                str += " " + "气管切开";
            }
            if (ckcg11.Checked)
            {
                str += " " + "喉罩";
            }
            if (ckcg12.Checked)
            {
                str += " 代" + txtH.Text+"号咽喉导管";
            }
            u.Text = str;
        }
    }
}
