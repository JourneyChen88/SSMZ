using adims_DAL.Flows;
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
    public partial class OperImplant : Form
    {

        OperImplantDal _SszrwDal = new OperImplantDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        string _PatId, Odate;
        private ComboBox cmbCJ = new ComboBox();
        public OperImplant(string patid, string date)
        {
            _PatId = patid;
            Odate = date;
            InitializeComponent();
        }

        private void addSSSZR_Load(object sender, EventArgs e)
        {
            try
            {
                BindPatInfo();
                BindDGV();
                DataTable dtSF = bll.GetCJ();
                BindCJ(dtSF);
                cmbCJ.Visible = false;
                this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
                cmbCJ.SelectedIndexChanged += new EventHandler(cmbCJ_SelectedIndexChanged);
                dataGridView1.Controls.Add(cmbCJ);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cmbCJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmbCJ.SelectedItem.ToString();
            cmbCJ.Visible = false;
        }
        private void BindCJ(DataTable dt1)
        {
            cmbCJ.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbCJ.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbCJ.Items.Add("");
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void BindDGV()
        {
            DataTable dt = _SszrwDal.GetOperImplant(_PatId);
            dataGridView1.DataSource = dt.DefaultView;
        }
        /// <summary>
        /// 基本信息
        /// </summary>
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = _PaibanDal.GetPaibanByPatId(_PatId);
            if (dt.Rows.Count > 0)
            {
                tbPatname.Text = dt.Rows[0]["Patname"].ToString();
                tbAge.Text = dt.Rows[0]["Patage"].ToString();
                cmbSex.Text = dt.Rows[0]["Patsex"].ToString();
                tbkeshi.Text = dt.Rows[0]["patdpm"].ToString();
                tbZhuyuanID.Text = dt.Rows[0]["Patzhuyuanid"].ToString();
                txtSSMC.Text = dt.Rows[0]["oname"].ToString();
                txtZD.Text = dt.Rows[0]["OS"].ToString();
                txtYZ.Text = dt.Rows[0]["OS1"].ToString();
                lbl.Text = dt.Rows[0]["OS2"].ToString();
                txtQH.Text = dt.Rows[0]["on1"].ToString() + " " + dt.Rows[0]["on2"].ToString();
                txtXH.Text = dt.Rows[0]["sn1"].ToString() + " " + dt.Rows[0]["sn2"].ToString();
            }

        }
        /// <summary>
        /// 编辑dgv的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                string RoomName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
                string Value_Info = dataGridView1.CurrentCell.Value.ToString();
                _SszrwDal.UpdateOperImplant(ID, RoomName, Value_Info);

            }
            catch (Exception)
            {
                MessageBox.Show("操作错误！");
                BindDGV();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int res = _SszrwDal.InsertOperImplant(_PatId, Odate);
            if (res > 0)
            {
                BindDGV();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                string id = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                int iss = _SszrwDal.DeleteOperImplant(id);
                if (iss > 0)
                {
                    BindDGV();
                }
            }
        }

        private void btnDY_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("K16", 740, 1020);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font zt8 = new Font(new FontFamily("新宋体"), 8);
            Font zt9 = new Font(new FontFamily("新宋体"), 9);
            Font ht8 = new Font(new FontFamily("黑体"), 8);
            Font ht9 = new Font(new FontFamily("黑体"), 9);
            Font zt14 = new Font(new FontFamily("新宋体"), 10);
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            Pen ptp = new Pen(Brushes.Black);
            Pen pb2 = new Pen(Brushes.Black, 2);
            int x = 50, y = 40, y1 = y + 15;
            string title1 = "红桥医院手术室植入物登记表";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 170, y));
            y = y + 30;
            if (dataGridView1.Rows.Count > 0)
            {
                e.Graphics.DrawString(Convert.ToDateTime(dataGridView1.Rows[0].Cells["Column1"].Value).ToString("yyyy年MM月dd日"), ht8, Brushes.Black, new Point(x + 530, y + 5));
            }
            else
            {
                e.Graphics.DrawString("    年   月   日", ht8, Brushes.Black, new Point(x + 530, y + 5));
            }

            y = y + 25; int ZH_Y = y;
            e.Graphics.DrawLine(ptp, x, y, x + 660, y);//画横线          
            e.Graphics.DrawString("姓名：" + tbPatname.Text, ht8, Brushes.Black, new Point(x + 10, y + 5));
            e.Graphics.DrawLine(ptp, x + 110, y, x + 110, y + 25);
            e.Graphics.DrawString("性别：" + cmbSex.Text.Trim(), ht8, Brushes.Black, new Point(x + 120, y + 5));
            e.Graphics.DrawLine(ptp, x + 220, y, x + 220, y + 25);
            e.Graphics.DrawString("年龄：" + this.tbAge.Text.Trim() + "岁", ht8, Brushes.Black, new Point(x + 230, y + 5));
            e.Graphics.DrawLine(ptp, x + 340, y, x + 340, y + 25);
            e.Graphics.DrawString("科别：" + this.tbkeshi.Text.Trim(), ht8, Brushes.Black, new Point(x + 350, y + 5));
            e.Graphics.DrawLine(ptp, x + 520, y, x + 520, y + 25);
            e.Graphics.DrawString("住院号：" + this.tbZhuyuanID.Text.Trim(), ht8, Brushes.Black, new Point(x + 530, y + 5));
            y = y + 25;
            e.Graphics.DrawLine(ptp, x, y, x + 660, y);//画横线
            e.Graphics.DrawString("手术名称：" + txtSSMC.Text, ht8, Brushes.Black, new Point(x + 10, y + 5));
            y = y + 25;
            e.Graphics.DrawLine(ptp, x, y, x + 660, y);
            e.Graphics.DrawLine(ptp, x + 220, y, x + 220, y + 25 * 14);
            e.Graphics.DrawLine(ptp, x + 520, y, x + 520, y + 25 * 14);
            e.Graphics.DrawLine(ptp, x + 370, y, x + 370, y + 25 * 14);
            e.Graphics.DrawString("植入物名称", ht8, Brushes.Black, new Point(x + 80, y + 5));
            e.Graphics.DrawString("厂家", ht8, Brushes.Black, new Point(x + 270, y + 5));
            e.Graphics.DrawString("型号", ht8, Brushes.Black, new Point(x + 430, y + 5));
            e.Graphics.DrawString("数量", ht8, Brushes.Black, new Point(x + 570, y + 5));
            y = y + 25; int YY = y; int YYY = y;
            for (int i = 0; i < 14; i++)
            {
                e.Graphics.DrawLine(ptp, x, YY, x + 660, YY);
                YY = YY + 25;
            }
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                 e.Graphics.DrawString(dataGridView1.Rows[i].Cells["ZW_name"].Value.ToString(), ht8, Brushes.Black, new Point(x + 10, y + 5));
                 e.Graphics.DrawString(dataGridView1.Rows[i].Cells["ZW_CJ"].Value.ToString(), ht8, Brushes.Black, new Point(x + 230, y + 5));
                 e.Graphics.DrawString(dataGridView1.Rows[i].Cells["ZW_XH"].Value.ToString(), ht8, Brushes.Black, new Point(x + 380, y + 5));
                 e.Graphics.DrawString(dataGridView1.Rows[i].Cells["ZW_SL"].Value.ToString(), ht8, Brushes.Black, new Point(x + 530, y + 5));
                 y = y + 25;
            }
            YYY = YYY + 25 * 12;
            e.Graphics.DrawString("主刀：" + txtZD.Text, ht8, Brushes.Black, new Point(x + 10, YYY + 5));
            e.Graphics.DrawLine(ptp, x + 110, YYY, x + 110, YYY + 25);
            e.Graphics.DrawString("一助：" + txtYZ.Text, ht8, Brushes.Black, new Point(x + 120, YYY + 5));
            //e.Graphics.DrawLine(ptp, x + 220, YYY, x + 220, YYY + 25);
            e.Graphics.DrawString("二助：" + lbl.Text, ht8, Brushes.Black, new Point(x + 230, YYY + 5));            
            //e.Graphics.DrawLine(ptp, x + 370, YYY, x + 370, YYY + 25);
            e.Graphics.DrawString("器护：" + txtQH.Text, ht8, Brushes.Black, new Point(x + 380, YYY + 5));
            e.Graphics.DrawString("巡回：" + txtXH.Text, ht8, Brushes.Black, new Point(x + 530, YYY + 5));
            YYY = YYY + 25;
            e.Graphics.DrawLine(ptp, x , ZH_Y, x , YYY);
            e.Graphics.DrawLine(ptp, x+660, ZH_Y, x+660, YYY);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int ColIndex = dataGridView1.CurrentCell.ColumnIndex;
                if (ColIndex == 2)
                {
                    cmbCJ.Visible = false;
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmbCJ.Left = rect.Left;
                    cmbCJ.Top = rect.Top;
                    cmbCJ.Width = rect.Width;
                    cmbCJ.Height = rect.Height;
                    cmbCJ.Visible = true;
                    cmbCJ.Text = "";
                    cmbCJ.DroppedDown = true;
                }
                else
                {
                    cmbCJ.Visible = false;
                }
            }
        }

    }
}
