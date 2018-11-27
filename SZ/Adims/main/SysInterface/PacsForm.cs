using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

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
            string http = "http://pacs.js-szyy.com/webserver/examAction!prepareRptFromForeign.action?name=his&pass=aGlz&patiId=" + carid;
            webBrowser1.Navigate(http);
        }
    }
}
