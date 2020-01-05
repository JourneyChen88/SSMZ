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
    public partial class pingfenbiaozhun : Form
    {
        public int count = 0;
        public pingfenbiaozhun()
        {
            InitializeComponent();
        }
        int getcheckboxvalue(CheckBox chbox, int value)
        {
            if (chbox.Checked)
                return value;
            else
                return 0;
        }
        private void btSubmit_Click(object sender, EventArgs e)
        {
            count += getcheckboxvalue(checkBox1, 0);
            count += getcheckboxvalue(checkBox2, 1);
            count += getcheckboxvalue(checkBox3, 2);

            count += getcheckboxvalue(checkBox6, 0);
            count += getcheckboxvalue(checkBox5, 1);
            count += getcheckboxvalue(checkBox4, 2);

            count += getcheckboxvalue(checkBox9, 0);
            count += getcheckboxvalue(checkBox8, 1);
            count += getcheckboxvalue(checkBox7, 2);

            count += getcheckboxvalue(checkBox12, 0);
            count += getcheckboxvalue(checkBox11, 1);
            count += getcheckboxvalue(checkBox10, 2);

            count += getcheckboxvalue(checkBox15, 0);
            count += getcheckboxvalue(checkBox14, 1);
            count += getcheckboxvalue(checkBox13, 2);
            this.Close();
        }
    }
}
