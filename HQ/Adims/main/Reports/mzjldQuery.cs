using adims_DAL.Dics;
using adims_Utility;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class mzjldQuery : Form
    {
        public mzjldQuery()
        {
            InitializeComponent();
        }
        DataDicDal _DataDicDal = new DataDicDal();
        adims_DAL.OperStatisticsDal _OperStatisticsDal = new adims_DAL.OperStatisticsDal();
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlWhere = "";
            sqlWhere += " b.odate between'" + dtStart.Value + "' And  '" + dtEND.Value + "' ";
            if (tbMzjldID.Text != "") sqlWhere += " and  A.id='" + tbMzjldID.Text + "' ";
            if (tbPatID.Text != "") sqlWhere += " and A.patid='" + tbPatID.Text.Trim() + "'  ";
            if (tbAgeStart.Text != "") sqlWhere += " and patage>='" + tbAgeStart.Text + "' ";
            if (tbAgeEnd.Text != "") sqlWhere += " and patage<='" + tbAgeEnd.Text + "'  ";
            if (tbName.Text != "") sqlWhere += " and patname='" + tbName.Text + "'  ";
            if (cmbSex.Text != "") sqlWhere += " and patsex='" + cmbSex.Text + "'  ";
            StringBuilder Builder1 = new StringBuilder();
            StringBuilder Builder2 = new StringBuilder();
            Builder1.AppendFormat(" and mzys like '%{0}%'  ", cmbMZYS.Text);
            Builder2.AppendFormat(" and ssys like '%{0}%'  ", cmbSSYS.Text);
            if (cmbMZYS.Text != "") sqlWhere += Builder1.ToString();
            if (cmbSSYS.Text != "") sqlWhere += Builder2.ToString();
            if (tbSSMC.Text != "") sqlWhere += " and shoushufs='" + tbSSMC.Text + "'  ";
            if (cmbTiWei.Text != "") sqlWhere += " and A.tw='" + cmbTiWei.Text + "'  ";
            if (cmbKeShi.Text != "") sqlWhere += " and patdpm='" + cmbKeShi.Text + "'  ";
            if (tbSQZD.Text != "") sqlWhere += " and pattmd='" + tbSQZD.Text + "'  ";
            //if (tbSZZD.Text != "") sqlWhere += " and szzd='" + tbSZZD.Text + "'  ";
            if (cmbMZFF.Text != "") sqlWhere += " and Mazuifs='" + cmbMZFF.Text + "' ";
            //if (cmbMZXG.Text != "") sqlWhere += " and mzxg='" + cmbMZXG.Text + "'  ";

            DataTable dt = _OperStatisticsDal.GetMzjldByCondition(sqlWhere);
            string danwei = "  条";
            labResult.Text = dt.Rows.Count.ToString() + danwei;
            dgvMzjld.DataSource = dt.DefaultView;
            if (dt.Rows.Count == 0) MessageBox.Show("当前查询条件无手术信息！");
        }

        private void mzjldQuery_Load(object sender, EventArgs e)
        {
            dtStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            this.WindowState = FormWindowState.Maximized;
            Bind_KeShi();
            Bind_MZYS();
            Bind_SSYS();
            BindMZFF();
            BindTiwei();
        }

        public void Bind_KeShi()
        {
            cmbKeShi.Items.Clear();
            DataTable dt = _DataDicDal.Select_Keshi();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbKeShi.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        public void Bind_MZYS()
        {
            cmbMZYS.Items.Clear();
            DataTable dt = _DataDicDal.GetUserByType((int)EnumCreator.UserType.麻醉医生);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        public void Bind_SSYS()
        {
            cmbSSYS.Items.Clear();
            DataTable dt = _DataDicDal.Select_SSYS();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbSSYS.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        public void BindMZFF()
        {
            cmbMZFF.Items.Clear();
            DataTable dt = _DataDicDal.GetMazuiFangfaAll();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZFF.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        public void BindTiwei()
        {
            cmbTiWei.Items.Clear();
            DataTable dt = _DataDicDal.GetTiwei();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbTiWei.Items.Add(dt.Rows[i][0].ToString());
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

        }

        private void dgvMzjld_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvMzjld.SelectedRows[0].Cells[0].Value.ToString()!="")
            //{
            //   string mzid = dgvMzjld.SelectedRows[0].Cells["mzjldid"].Value.ToString();
            //   String patid = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["patid"].Value);
            //   mzjldEdit mzjld1 = new mzjldEdit(mzid, patid);
            //   mzjld1.ShowDialog();
            //}
        }

        private void dgvMzjld_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                dgvMzjld.ContextMenuStrip = conMenu;
                conMenu.Show(dgvMzjld, new Point(e.X, e.Y));
            }

        }


        private void MZZJDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.Rows.Count > 0)
            {
                string mzid = dgvMzjld.SelectedRows[0].Cells["mzjldid"].Value.ToString();
                string patid = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["patid"].Value);
                string date = dgvMzjld.SelectedRows[0].Cells["otime"].Value.ToString();
                AnesthesiaSummary form1 = new AnesthesiaSummary(mzid, patid, date);
                form1.ShowDialog();
            }
        }

        private void MZJLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.SelectedRows.Count == 1)
            {
                int mzID = Convert.ToInt32(dgvMzjld.SelectedRows[0].Cells["mzjldid"].Value);
                String patID = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["patid"].Value);
                String oroom = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["oroom"].Value);
                string dttime = dgvMzjld.SelectedRows[0].Cells["otime"].Value.ToString();
                DateTime dt = Convert.ToDateTime((Convert.ToDateTime(dgvMzjld.SelectedRows[0].Cells["otime"].Value)).ToShortDateString());
                MzjldEdit mzjld1 = new MzjldEdit(patID, oroom, dt, mzID, true);
                mzjld1.ShowDialog();
            }

        }

        private void SHZTDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.Rows.Count > 0)
            {
                string mzID = dgvMzjld.CurrentRow.Cells["mzjldid"].Value.ToString();
                string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
                string date = dgvMzjld.SelectedRows[0].Cells["otime"].Value.ToString();
                NurseRecord_HQ form1 = new NurseRecord_HQ(mzID, patID, date);
                form1.ShowDialog();
            }
        }

        private void PACUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.Rows.Count > 0)
            {
                string mzID = dgvMzjld.CurrentRow.Cells["mzjldid"].Value.ToString();
                string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
                string date = dgvMzjld.SelectedRows[0].Cells["otime"].Value.ToString();
                PACU_HQ F = new PACU_HQ(mzID, patID, date);
                F.ShowDialog();
            }
        }
        /// <summary>
        /// 麻醉医师术后访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_MZYSSHFS_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.Rows.Count > 0)
            {
                int mzjldId = Convert.ToInt32(dgvMzjld.CurrentRow.Cells["mzjldId"].Value.ToString());
                string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
                string date = Convert.ToDateTime(dgvMzjld.CurrentRow.Cells["otime"].Value).ToString("yyyy-MM-dd");
                AfterVisit_HQ f2 = new AfterVisit_HQ(mzjldId, patID);
                f2.Show();
            }
        }
        /// <summary>
        ///  护士术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TSMI_HS_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.Rows.Count > 0)
            {
                string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
                string date = Convert.ToDateTime(dgvMzjld.CurrentRow.Cells["otime"].Value).ToString("yyyy-MM-dd");
                BeforeVisit_HS_HQ f2 = new BeforeVisit_HS_HQ(patID, date);
                f2.Show();
            }
        }
        /// <summary>
        /// 麻醉医师术前访视
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_MZYSSQFS_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.Rows.Count > 0)
            {
                string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
                string date = Convert.ToDateTime(dgvMzjld.CurrentRow.Cells["otime"].Value).ToString("yyyy-MM-dd");
                BeforeVisit_HQ f2 = new BeforeVisit_HQ(patID, date);
                f2.Show();
            }
        }


    }
}
