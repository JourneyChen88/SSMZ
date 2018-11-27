using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using main.UserSecurity;
using adims_DAL;
namespace main
{
    public partial class mzjldQuery : Form
    {
        public mzjldQuery()
        {
            InitializeComponent();
        }
        admin_T_SQL at = new admin_T_SQL();
        AdimsController acon = new AdimsController();
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlWhere = "";
            sqlWhere += " and isValid='1' and otime between'" + dtStart.Value + "' And  '" + dtEND.Value + "' ";
            if (tbMzjldID.Text != "") sqlWhere += " and  M.id='" + tbMzjldID.Text + "' ";
            if (tbPatID.Text != "") sqlWhere += " and M.patid='" + tbPatID.Text + "'  ";
            if (this.tbZhuyuanID.Text != "") sqlWhere += " and O.patZhuyuanID='" + tbZhuyuanID.Text + "'  ";
            if (tbAgeStart.Text != "") sqlWhere += " and patage>='" + tbAgeStart.Text + "' ";
            if (tbAgeEnd.Text != "") sqlWhere += " and patage<='" + tbAgeEnd.Text + "'  ";
            if (tbName.Text != "") sqlWhere += " and patname='" + tbName.Text + "'  ";
            if (cmbSex.Text != "") sqlWhere += " and patsex='" + cmbSex.Text + "'  ";
            if (cmbMZYS.Text != "") sqlWhere += string.Format(" and mzys like '%{0}%'  ", cmbMZYS.Text);
            if (cmbSSYS.Text != "") sqlWhere += string.Format(" and ssys like '%{0}%'  ", cmbSSYS.Text);
            if (cmbMZXG.Text != "") sqlWhere += " and mzxg = '" + cmbMZXG.Text + "'";
            if (cmbTiWei.Text != "") sqlWhere += " and M.tw='" + cmbTiWei.Text + "'";
            if (cmbKeShi.Text != "") sqlWhere += " and patdpm = '" + cmbKeShi.Text + "'";
            if (tbSQZD.Text != "") sqlWhere += " and Pattmd  like '%" + tbSQZD.Text + "%'";
            if (tbSZZD.Text != "") sqlWhere += " and szzd  like '%" + tbSZZD.Text + "%'";
            if (tbSSMC.Text != "") sqlWhere += " and ssss like '%" + tbSSMC.Text + "%'";
            if (cmbMZFF.Text != "") sqlWhere += " and amethod like '%" + cmbMZFF.Text + "%'";
            if (cbEyeOper.Checked) sqlWhere += " and EyeOper = '1' ";
            else sqlWhere += " and EyeOper = '0' ";
            if (this.cbIsJizhen.Checked) sqlWhere += " and M.ASAE = '1' ";
            else sqlWhere += " and M.ASAE = '0' ";

            if (this.cmbBRQX.Text.Trim() != "") sqlWhere += " and M.BRQX = '" + cmbBRQX.Text.Trim() + "' ";

            DataTable dt = acon.query_mzjld(sqlWhere);
            string danwei = "  条";
            labResult.Text = dt.Rows.Count.ToString() + danwei;
            dgvMzjld.DataSource = dt.DefaultView;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("当前查询条件无手术信息！");
            }
        }

        private void mzjldQuery_Load(object sender, EventArgs e)
        {
            dtStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.WindowState = FormWindowState.Maximized;
            Bind_KeShi();
            Bind_MZYS();
            Bind_SSYS();
            Bind_amethod();
            Bind_TW();
        }
        /// <summary>
        /// 科室
        /// </summary>
        public void Bind_KeShi()
        {
            cmbKeShi.Items.Clear();
            DataTable dt = acon.select_keshi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbKeShi.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        /// <summary>
        /// 体位
        /// </summary>
        public void Bind_TW()
        {
            cmbTiWei.Items.Clear();
            DataTable dt = at.Select_TW();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbTiWei.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        /// <summary>
        /// 麻醉方式
        /// </summary>
        public void Bind_amethod()
        {
            cmbMZFF.Items.Clear();
            DataTable dt = at.Select_amethod();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZFF.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        /// <summary>
        /// 麻醉医生
        /// </summary>
        public void Bind_MZYS()
        {
            cmbMZYS.Items.Clear();
            DataTable dt = at.Select_MZYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        /// <summary>
        /// 手术医生
        /// </summary>
        public void Bind_SSYS()
        {
            cmbSSYS.Items.Clear();
            DataTable dt = at.Select_SSYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbSSYS.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbMzjldID.Text = "";
            tbPatID.Text = "";
            tbAgeStart.Text = "";
            tbAgeEnd.Text = "";
            tbName.Text = "";
            cmbSex.Text = "";
            cmbMZYS.Text = "";
            cmbSSYS.Text = "";
            cmbMZXG.Text = "";
            tbSSMC.Text = "";
            cmbTiWei.Text = "";
            cmbKeShi.Text = "";
            tbSQZD.Text = "";
            tbSZZD.Text = "";
            cmbMZFF.Text = "";
            cmbBRQX.Text = "";
            cbEyeOper.Checked = false;
            cbIsJizhen.Checked = false;

        }

        //private void dgvMzjld_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvMzjld.SelectedRows[0].Cells[0].Value.ToString()!="")
        //    {
        //       string mzid = dgvMzjld.SelectedRows[0].Cells[1].Value.ToString();
        //       String patid = Convert.ToString(dgvMzjld.SelectedRows[0].Cells[0].Value);
        //       mzjldEdit mzjld1 = new mzjldEdit(mzid, patid);
        //       mzjld1.ShowDialog();
        //    }
        //}

        //private void dgvMzjld_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{

        //        if (e.Button == MouseButtons.Right)
        //        {
        //            dgvMzjld.ContextMenuStrip = conMenu;
        //            conMenu.Show(dgvMzjld, new Point(e.X, e.Y));
        //        }

        //}


        private void MZZJDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mzID = dgvMzjld.CurrentRow.Cells[1].Value.ToString();
            string patID = dgvMzjld.CurrentRow.Cells[0].Value.ToString();
            MZZJ_SZ form1 = new MZZJ_SZ(mzID, patID);
            form1.ShowDialog();
        }

        private void MZJLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.SelectedRows.Count == 1)
            {
                int mzID = Convert.ToInt32(dgvMzjld.SelectedRows[0].Cells["mzid"].Value);
                String patID = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["patid"].Value);
                String oroom = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["oroom"].Value);
                string dttime = dgvMzjld.SelectedRows[0].Cells["otime"].Value.ToString();
                DateTime dt = Convert.ToDateTime((Convert.ToDateTime(dgvMzjld.SelectedRows[0].Cells["otime"].Value)).ToShortDateString());
                mzjldEdit mzjld1 = new mzjldEdit(patID, oroom, dt, mzID, true);
                mzjld1.ShowDialog();
            }
        }

        private void SHZTDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mzjldid = dgvMzjld.CurrentRow.Cells[0].Value.ToString();
            string patID = dgvMzjld.CurrentRow.Cells[1].Value.ToString();
            string patNAME = dgvMzjld.CurrentRow.Cells[2].Value.ToString();
            string patSEX = dgvMzjld.CurrentRow.Cells[3].Value.ToString();
            string patAGE = dgvMzjld.CurrentRow.Cells[4].Value.ToString();
            MZH_ZhengTong f2 = new MZH_ZhengTong(mzjldid, patID);
            f2.ShowDialog();
            //string mzID = dgvMzjld.CurrentRow.Cells[1].Value.ToString();
            //string patID = dgvMzjld.CurrentRow.Cells[0].Value.ToString();
            //MZH_ZhengTong form1 = new MZH_ZhengTong(mzID, patID);
            //form1.ShowDialog();
        }

        private void PACUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patID = dgvMzjld.CurrentRow.Cells["病人编号"].Value.ToString();
            string mzID = dgvMzjld.CurrentRow.Cells["麻醉编号"].Value.ToString();
            PACU_SZ pacuform = new PACU_SZ(patID, mzID);
            pacuform.ShowDialog();
        }

        private void SHSFToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string mzjldid = dgvMzjld.CurrentRow.Cells[0].Value.ToString();
            string patID = dgvMzjld.CurrentRow.Cells[1].Value.ToString();
            AfterVisit_SZ f2 = new AfterVisit_SZ(mzjldid, patID);
            f2.ShowDialog();
            //string mzID = dgvMzjld.CurrentRow.Cells[1].Value.ToString();
            //string patID = dgvMzjld.CurrentRow.Cells[0].Value.ToString();
            //AfterVisit_JDZY F = new AfterVisit_JDZY(mzID, patID);
            //F.ShowDialog();
        }

        private void SQFSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string patID = dgvMzjld.CurrentRow.Cells[1].Value.ToString();
            BeforeVisit_YS f2 = new BeforeVisit_YS(patID);
            f2.ShowDialog();
        }

        private void tbAgeStart_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e);
        }

        private void tbAgeEnd_KeyPress(object sender, KeyPressEventArgs e)
        {
            UserFunction.Text_Value_Limit(sender, e);
        }


    }
}
