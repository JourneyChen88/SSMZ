using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main
{
    public partial class LsyzForm : Form
    {
        string mzjldID;
        string PatID;
        private ComboBox cmbXHHS = new ComboBox();
        private ComboBox cmbMZYS = new ComboBox();
        adims_DAL.Flows.OperScheduleDal _PaibanDal = new adims_DAL.Flows.OperScheduleDal();
        adims_DAL.Flows.LsyzDal _LsyzDal = new adims_DAL.Flows.LsyzDal();
        adims_DAL.Dics.DataDicDal _DataDicDal = new adims_DAL.Dics.DataDicDal();
        public LsyzForm(string mzid, string patid)
        {
            mzjldID = mzid;
            PatID = patid;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int f = 0;
        }
        private void BindMZYS(DataTable dt1)
        {
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZYS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbMZYS.Items.Add("");
        }
        private void BindXHHS(DataTable dt1)
        {
            cmbXHHS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbXHHS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbXHHS.Items.Add("");
        }
        private void BindPatInfo()
        {
            DataTable dt = _PaibanDal.GetPaibanByPatId(PatID);
            tbPatName.Text = dt.Rows[0]["Patname"].ToString();
            tbKeshi.Text = dt.Rows[0]["patdpm"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            cmbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNo.Text = dt.Rows[0]["Patbedno"].ToString();
            //tbShoushuName.Text = dt.Rows[0]["Oname"].ToString();
            //tbSZZD.Text = dt.Rows[0]["Pattmd"].ToString();
            //cmbOroom.Text = dt.Rows[0]["Oroom"].ToString();
            //tbMingzu.Text = dt.Rows[0]["PatNation"].ToString();
            //cmbMZFF.Text = dt.Rows[0]["Amethod"].ToString();
        }
        
        private void cmbXHHS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvYizhu.CurrentCell.Value = cmbXHHS.SelectedItem.ToString();
            cmbXHHS.Visible = false;
        }

        private void cmbMZYS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvYizhu.CurrentCell.Value = cmbMZYS.SelectedItem.ToString();
            cmbMZYS.Visible = false;
        }
        private void LsyzForm_Load(object sender, EventArgs e)
        {
            try
            {
                BindPatInfo();
                DataTable dtMZYS = _DataDicDal.GetAllMZYS();
                BindMZYS(dtMZYS); cmbMZYS.Visible = false;
                DataTable dtXHHS = _DataDicDal.GetAll_hushi();
                BindXHHS(dtXHHS); cmbXHHS.Visible = false;
                cmbMZYS.SelectedIndexChanged += new EventHandler(cmbMZYS_SelectedIndexChanged);
                cmbXHHS.SelectedIndexChanged += new EventHandler(cmbXHHS_SelectedIndexChanged);
                dgvYizhu.Controls.Add(cmbMZYS);
                dgvYizhu.Controls.Add(cmbXHHS);
                dgvYizhuBind();
                dgvYizhu.CellValueChanged += new DataGridViewCellEventHandler(dgvYizhu_CellValueChanged);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
          
        }
        private void dgvYizhu_CellValueChanged(object sender, EventArgs e)
        {
            if (dgvYizhu.Rows.Count > 0)
            {
                int id = 0;
                id = int.Parse(dgvYizhu.CurrentRow.Cells["xuhao"].Value.ToString());
                if (id != 0)
                {
                    string DataType = dgvYizhu.Columns[dgvYizhu.CurrentCell.ColumnIndex].Name;
                    string DataValue = dgvYizhu.CurrentCell.Value.ToString();
                    int flag = _LsyzDal.updateLSYZ(id, DataType, DataValue);
                    if (flag > 0)
                    { }
                    else
                        MessageBox.Show("修改失败！");

                }
            }
        }
        private void dgvYizhuBind()
        {
            DataTable dt = _LsyzDal.SelectLSYZ(mzjldID);
            dgvYizhu.DataSource = dt.DefaultView;
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            flag = _LsyzDal.InsertLSYZ(mzjldID, DateTime.Now.ToString("MM-dd"), DateTime.Now.ToString("HH:mm"));
            if (flag > 0)
                dgvYizhuBind();
            else
                MessageBox.Show("添加失败！");
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvYizhu.SelectedCells.Count == 1)
            {
                int id = int.Parse(dgvYizhu.CurrentRow.Cells["xuhao"].Value.ToString());

                if (MessageBox.Show(" 您确定要删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int flag = 0;
                    flag = _LsyzDal.deleteLSYZ(id);
                    if (flag > 0)
                        dgvYizhuBind();
                    else
                        MessageBox.Show("删除失败！");

                }
            }

        }

        private void zxyzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvYizhu.SelectedCells.Count == 1)
            {
                int id = int.Parse(dgvYizhu.CurrentRow.Cells["xuhao"].Value.ToString());
                string Date = DateTime.Now.ToString("MM-dd");
                string Time = DateTime.Now.ToString("HH:mm");
                int flag = 0;
                flag = _LsyzDal.updateLSYZdatetime(id, Date, Time);
                if (flag > 0)
                    dgvYizhuBind();
                else
                    MessageBox.Show("删除失败！");
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //this.printPreviewDialog1.Document = printDocument1;
            //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            //    printDocument1.Print();
            //printPreviewDialog1.PrintPreviewControl.Zoom = 1.0;
            //printDocument1.DefaultPageSettings.PaperSize =
            //         new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            if (dgvYizhu.Rows.Count > 0)
            {
                PrintDGV2.Print(dgvYizhu, "昌吉回族自治州人民医院\n  手术室临时医嘱单",
                    "科别：" + tbKeshi.Text + "  床号：" + tbBedNo.Text + "  姓名：" + tbPatName.Text + "  年龄：" + tbAge.Text + " 岁  性别：" + cmbSex.Text + "  住院号：" + tbZhuyuanID.Text, 1);
            }
        }

        private void dgvYizhu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvYizhu.Rows.Count > 0)
            {
                int ColIndex = dgvYizhu.CurrentCell.ColumnIndex;
                if (ColIndex == 5)
                {
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvYizhu.GetCellDisplayRectangle(dgvYizhu.CurrentCell.ColumnIndex, dgvYizhu.CurrentCell.RowIndex, false);
                    cmbMZYS.Left = rect.Left;
                    cmbMZYS.Top = rect.Top;
                    cmbMZYS.Width = rect.Width;
                    cmbMZYS.Height = rect.Height;
                    cmbMZYS.Visible = true;
                    cmbMZYS.Text = "";
                    cmbMZYS.DroppedDown = true;
                }

                else if (ColIndex == 6)
                {
                    cmbMZYS.Visible = false;
                    Rectangle rect = dgvYizhu.GetCellDisplayRectangle(dgvYizhu.CurrentCell.ColumnIndex, dgvYizhu.CurrentCell.RowIndex, false);
                    cmbXHHS.Left = rect.Left;
                    cmbXHHS.Top = rect.Top;
                    cmbXHHS.Width = rect.Width;
                    cmbXHHS.Height = rect.Height;
                    cmbXHHS.Visible = true;
                    cmbXHHS.Text = "";
                    cmbXHHS.DroppedDown = true;
                }
                else
                {
                    cmbXHHS.Visible = false;
                    cmbMZYS.Visible = false;
                }
            }

        }
    }
}
