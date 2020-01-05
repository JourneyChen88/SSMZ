using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;

namespace main
{
    public partial class MZZJ_Query : Form
    {
        public MZZJ_Query()
        {
            InitializeComponent();
        }
        AdimsController acon = new AdimsController();
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlWhere = "";
            sqlWhere += " and mzkssj ='" + dtStart.Value + "'";
            if (tbPatID.Text != "") sqlWhere += " and  adims_otypesetting.patid='" + tbPatID.Text + "' ";
            if (tbMzjldID.Text != "") sqlWhere += " and adims_mzzongjie.mzjldID='" + tbMzjldID.Text + "'  ";
            if (tbName.Text != "") sqlWhere += " and patname='" + tbName.Text + "'  ";
            if (tbSex.Text != "") sqlWhere += " and patsex='" + tbSex.Text + "'  ";
            StringBuilder Builder1 = new StringBuilder();
            
            Builder1.AppendFormat(" and mzys like '%{0}%'  ", tbMZYS.Text);
           
            if (tbMZYS.Text != "") sqlWhere += Builder1.ToString();
            DataTable dt = acon.query_mzzongjie(sqlWhere);
               
            dgvMzjld.DataSource = dt.DefaultView;
        }
    }
}
