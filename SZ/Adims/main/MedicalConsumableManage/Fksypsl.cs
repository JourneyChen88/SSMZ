using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.MedicalConsumableManage
{
    public partial class Fksypsl : Form
    {
        adims_BLL.AdimsController bill = new adims_BLL.AdimsController();
        public Fksypsl()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Fksypsl_Load(object sender, EventArgs e)
        {
            DataSet medComSta_set = new DataSet();
            medComSta_set=bill.ypxhtj_table();
            dataGridView1.DataSource = medComSta_set.Tables[0];

        }

       
    }
}
