using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using main.用户安全;

namespace main
{
    public partial class mzjldQuery : Form
    {
        public mzjldQuery()
        {
            InitializeComponent();
        }
        AdimsController dal = new AdimsController();
        adims_BLL.AdimsController bll = new AdimsController();
        private void button2_Click(object sender, EventArgs e)
        {
            string sqlWhere = "";
            sqlWhere += " otime between'" + dtStart.Value + "' And  '" + dtEND.Value + "' ";
            if (tbdmzg.Text != "") sqlWhere += " and  C.qitacaozuo like'%" + tbdmzg.Text + "%' ";
            if (tbPatID.Text != "") sqlWhere += " and B.patid='" + tbPatID.Text.Trim() + "'  ";
            if (tbAgeStart.Text != "") sqlWhere += " and patage>=" + tbAgeStart.Text.Trim() + " ";
            if (tbAgeEnd.Text != "") sqlWhere += " and patage<= " + tbAgeEnd.Text.Trim() + "   ";
            if (tbName.Text != "") sqlWhere += " and patname='" + tbName.Text + "'  ";
            if (cmbSex.Text != "") sqlWhere += " and patsex='" + cmbSex.Text + "'  ";
            StringBuilder Builder1 = new StringBuilder();
            StringBuilder Builder2 = new StringBuilder();
            Builder1.AppendFormat(" and A.mzys like '%{0}%'  ", cmbMZYS.Text);
            Builder2.AppendFormat(" and ssys like '%{0}%'  ", cmbSSYS.Text);
            if (cmbMZYS.Text != "") sqlWhere += Builder1.ToString();
            if (cmbSSYS.Text != "") sqlWhere += Builder2.ToString();
            if (tbSSMC.Text != "") sqlWhere += " and shoushufs='" + tbSSMC.Text + "'  ";
            if (cmbjmzg.Text != "") sqlWhere += " and C.qitacaozuo like'%" + cmbjmzg.Text + "%'  ";
            if (cmbKeShi.Text != "") sqlWhere += " and patdpm  like '%" + cmbKeShi.Text + "%'  ";
            if (tbSQZD.Text != "") sqlWhere += " and pattmd='" + tbSQZD.Text + "'  ";
            if (comasa.Text != "") sqlWhere += " and A.ASA='" + comasa.Text + "'  ";
            if (cmbMZFF.Text != "") sqlWhere += " and Mazuifs like '%" + cmbMZFF.Text + "%' ";
            if (cmbisjizhen.Text != "") sqlWhere += " and A.isjizhen='" + cmbisjizhen.Text + "'  ";
            if (combxztx.Text != "") sqlWhere += " and c.shuxuejl like'%" + combxztx.Text + "%'  ";
            if (comboxytx.Text != "") sqlWhere += " and c.shuxuejl like'%" + comboxytx.Text + "%'  ";
            DataTable dt = dal.query_mzjld(sqlWhere);
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
            //Bind_SSYS();
            BindMZFF();
            //BindTiwei();
        }

        public void Bind_KeShi()
        {
           cmbKeShi.Items.Clear();
           DataTable dt = dal.select_keshi();
           for (int i = 0; i < dt.Rows.Count; i++)
			{
                cmbKeShi.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        public void Bind_MZYS()
        {
            cmbMZYS.Items.Clear();
            DataTable dt = bll.GetMZYS(1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZYS.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        public void Bind_SSYS()
        {
            cmbSSYS.Items.Clear();
            DataTable dt = dal.select_shoushuyisheng();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbSSYS.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        public void BindMZFF()
        {
            cmbMZFF.Items.Clear();
            DataTable dt = dal.selectMazuifangfa();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbMZFF.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        public void BindTiwei()
        {
            cmbjmzg.Items.Clear();
            DataTable dt = dal.selectTiwei();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbjmzg.Items.Add(dt.Rows[i][0].ToString());
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            tbdmzg.Text = "";
            tbPatID.Text = "";
            tbAgeStart.Text = "";
            tbAgeEnd.Text = "";
            tbName.Text = "";
            cmbSex.Text = "";
            cmbMZYS.Text = "";
            cmbSSYS.Text = "";
            cmbisjizhen.Text = "";
            tbSSMC.Text = "";
            cmbjmzg.Text = "";
            cmbKeShi.Text = "";
            tbSQZD.Text = "";
            tbasafj.Text = "";
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
            string mzid = dgvMzjld.SelectedRows[0].Cells["mzjldid"].Value.ToString();
            String patid = Convert.ToString(dgvMzjld.SelectedRows[0].Cells["patid"].Value);
            MZZJ_CJ form1 = new MZZJ_CJ(mzid, patid);
            form1.ShowDialog();
        }

        private void MZJLDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvMzjld.SelectedRows.Count==1)
            {
                int mzID = Convert.ToInt32(dgvMzjld.SelectedRows[0].Cells["mzjldid"].Value);
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
            string mzID = dgvMzjld.CurrentRow.Cells["mzjldid"].Value.ToString();
            string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
            NurseRecord_CJ form1 = new NurseRecord_CJ(mzID, patID);
            form1.ShowDialog();
        }

        private void PACUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mzID = dgvMzjld.CurrentRow.Cells["mzjldid"].Value.ToString();
            string patID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
            PACU F = new PACU(mzID, patID);
            F.ShowDialog();
        }

        private void SHSFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patID = dgvMzjld.CurrentRow.Cells["mzjldid"].Value.ToString();
            string mzID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
            string Odate = dgvMzjld.CurrentRow.Cells["otime"].Value.ToString();
            BeforeVisit_YS F = new BeforeVisit_YS(patID, Odate);
            F.ShowDialog();
        }

        private void SQFSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string patID = dgvMzjld.CurrentRow.Cells["mzjldid"].Value.ToString();
            string mzID = dgvMzjld.CurrentRow.Cells["patid"].Value.ToString();
            string Odate = dgvMzjld.CurrentRow.Cells["otime"].Value.ToString();
            BeforeVisit_YS F = new BeforeVisit_YS(patID, Odate);
            F.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridviewShowToExcel(dgvMzjld);
        }

        public void DataGridviewShowToExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count == 0) return;
            try
            {
                //建立Excel对象   
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                //excel.Application.Workbooks.Add(true);
                if (excel == null)
                {
                    MessageBox.Show("Excel 无法启动");
                    return;
                }
                Microsoft.Office.Interop.Excel.Workbook xlBook = excel.Workbooks.Add(true);
                Microsoft.Office.Interop.Excel.Worksheet xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets[1];
                int cols = dgv.Rows[0].Cells.Count;
                int rows = dgv.Rows.Count;
                Microsoft.Office.Interop.Excel.Range range = null;

                // 列头
                for (int k = 0; k < cols; k++)
                {
                    excel.Cells[1, k + 1] = dgv.Columns[k].HeaderText;
                    range = excel.Cells[1, k + 1];
                    range.Font.Bold = true;
                    range.Interior.ColorIndex = 15;
                    range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    range.EntireColumn.AutoFit();
                }
                //填充数据  
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        if (dgv[j, i].ValueType == typeof(string))
                            excel.Cells[i + 2, j + 1] = "'" + dgv[j, i].Value.ToString();
                        else
                            excel.Cells[i + 2, j + 1] = dgv[j, i].Value.ToString();
                    }
                }
                excel.Visible = true;
            }
            catch
            {
                MessageBox.Show("导出Execl出现异常,请检查!");
            }
        }


    }
}
