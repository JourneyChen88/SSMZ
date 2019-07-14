using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Adims_Utility;

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
            //XmlHelper help = new XmlHelper();
            //string filename = Application.StartupPath + @"\SysSetting.xml";
            var remotepath = ConfigurationManager.AppSettings["EMR"];
            string http = remotepath + userNo;
            webBrowser1.Navigate(http);
        }
    }
}
