using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace main
{
    public partial class EMRform : Form
    {
        string userNo;
        public EMRform(string userNo1)
        {
            userNo = userNo1;
            InitializeComponent();
        }

        private void EMRform_Load(object sender, EventArgs e)
        {
            string http = "http://10.0.100.55:9080/jsszhemr/jsp/login.action?doctor.user_password=88888&hisbs=true&doctor.user_name=" + userNo;
            webBrowser1.Navigate(http);
        }
    }
}
