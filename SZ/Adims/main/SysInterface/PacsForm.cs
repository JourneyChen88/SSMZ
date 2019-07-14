using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using Adims_Utility;

namespace main
{
    public partial class PacsForm : Form
    {
        string carid = "";
        public PacsForm(string cardno)
        {
            carid = cardno;
            InitializeComponent();
        }
        private void yxbl_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = 0;
            //XmlHelper help = new XmlHelper();
            //string filename = Application.StartupPath + @"\SysSetting.xml";

            var remotepath = ConfigurationManager.AppSettings["PACS"];
            //help.GetXmlNodeInnerText(filename, "PACS");
            string http = remotepath + carid;
            webBrowser1.Navigate(http);
        }
    }
}
