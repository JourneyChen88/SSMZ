using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using Adims_Utility;

namespace main
{
    public partial class ElectrocardiogramForm : Form
    {
        string patId = "";
        public ElectrocardiogramForm(string patid)
        {
            patId = patid;
            InitializeComponent();
        }
        private void EHRForm_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = 0;
            XmlHelper help = new XmlHelper();
            string filename = Application.StartupPath + @"\SysSetting.xml";
            var remotepath = help.GetXmlNodeInnerText(filename, "Electrocardiogram");
            string http = remotepath + patId;
            webBrowser1.Navigate(http);
        }
    }
}
