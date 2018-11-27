using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.知识库
{
    public partial class ZhiShiKu : Form
    {
        public ZhiShiKu()
        {
            InitializeComponent();
        }

        private void ZhiShiKu_Load(object sender, EventArgs e)
        {
            string chmPath = System.IO.Path.Combine(Application.StartupPath, "zhishiku.CHM");
            System.Diagnostics.Process.Start(chmPath); 
        }
    }
}
