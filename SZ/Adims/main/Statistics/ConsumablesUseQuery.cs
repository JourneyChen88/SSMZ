using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_MODEL;

namespace main.Statistics
{
    public partial class ConsumablesUseQuery : Form
    {
        ConsumablesUseDal dal = new ConsumablesUseDal();
        //int MzjldId;
        //string PatId;
        public ConsumablesUseQuery()
        {
            InitializeComponent();

        }

        private void BindCombox()
        {
            DataTable dt = dal.GetKeshi();
            foreach (DataRow dr in dt.Rows)
            {
                cmbKeshi.Items.Add(dr[0]);
            }
        }
        private void BindGridView()
        {
            ConsumablesUseGetInput input = new ConsumablesUseGetInput();
            input.BeginTime = dtStart.Value.Date;
            input.EndTime = dtEnd.Value.Date.AddDays(1);
            input.PatZhuyuanId = tbZhuyuanID.Text.Trim();
            input.PatDpm = cmbKeshi.Text;
            input.PatName = tbPatName.Text.Trim();
            DataTable dt = dal.GetConsumablesUseList(input);
            this.dgvUseList.DataSource = dt;
            var rows = dt.Select(" isCost=1");
            double sum = 0.0;
            foreach (var dr in rows)
            {
                sum += Convert.ToDouble(dr["Dosage"].ToString()) * Convert.ToDouble(dr["Price"].ToString());
            }
            tbSum.Text = String.Format("{0:F}", sum);
        }


        private void ConsumablesUseForm_Load(object sender, EventArgs e)
        {
            BindGridView();
            BindCombox();
        }



        private void tbDosage_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.UserFunction.Text_Value_Limit(sender, e);
        }


        private void btnQuery_Click(object sender, EventArgs e)
        {

            BindGridView();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsumablesUseQueryByOrg f = new ConsumablesUseQueryByOrg();
            f.ShowDialog();
        }
    }
}
