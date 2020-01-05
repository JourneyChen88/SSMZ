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
        int mzjldID;
        string PatID;
        private ComboBox cmbXHHS = new ComboBox();
        private ComboBox cmbMZYS = new ComboBox();
        private ComboBox cmbyztype = new ComboBox();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public LsyzForm(int mzid, string patid)
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
            DataTable dt = new DataTable();
            dt = dal.GetALLPAIBANyz(PatID);
            tbPatName.Text = dt.Rows[0]["Patname"].ToString();
            tbKeshi.Text = dt.Rows[0]["patdpm"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            cmbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["patid"].ToString();
            tbBedNo.Text = dt.Rows[0]["Patbedno"].ToString();
            cmbSex.Enabled = false;
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
            BindPatInfo();
          //  BindYZbao();//医嘱包
            
            DataTable dtMZYS = dal.GetAllMZYS();
            BindMZYS(dtMZYS); cmbMZYS.Visible = false;
            DataTable dtXHHS = dal.GetAll_hushi();
            BindXHHS(dtXHHS); cmbXHHS.Visible = false;
            DataTable dtYZ = dal.GetAllYZtype();
            Bindnametype(dtYZ); cmbyztype.Visible = false;
            cmbMZYS.SelectedIndexChanged += new EventHandler(cmbMZYS_SelectedIndexChanged);
            cmbXHHS.SelectedIndexChanged += new EventHandler(cmbXHHS_SelectedIndexChanged);
            cmbyztype.SelectedIndexChanged += new EventHandler(cmbyztype_SelectedIndexChanged);
            dgvYizhu.Controls.Add(cmbMZYS);
            dgvYizhu.Controls.Add(cmbXHHS);
            dgvYizhu.Controls.Add(cmbyztype);
            dgvYizhuBind();
            dgvYizhu.CellValueChanged += new DataGridViewCellEventHandler(dgvYizhu_CellValueChanged);
        }
        /// <summary>
        /// 医嘱名称
        /// </summary>
        private void Bindnametype(DataTable dt1)
        {
            cmbyztype.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbyztype.Items.Add(dt1.Rows[i][1]);
            }
            this.cmbyztype.Items.Add("");
        }
        private void cmbyztype_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvYizhu.CurrentCell.Value = cmbyztype.SelectedItem.ToString();
            cmbyztype.Visible = false;
        }
        /// <summary>
        /// 绑定医嘱包
        /// </summary>
        private void BindYZbao()
        {
            DataTable dt = dal.GetAllYZBao();
            cmbYZbao.DataSource = dt;
            cmbYZbao.ValueMember = "id";
            cmbYZbao.DisplayMember = "name";
        }
        private void dgvYizhu_CellValueChanged(object sender, EventArgs e)
        {
            if (dgvYizhu.Rows.Count > 0)
            {
                int id = 0;
                if (dgvYizhu.CurrentCell.ColumnIndex==0)
                {
                    return;
                }
                id = int.Parse(dgvYizhu.CurrentRow.Cells["xuhao"].Value.ToString());
                if (id != 0)
                {
                    string DataType = dgvYizhu.Columns[dgvYizhu.CurrentCell.ColumnIndex].Name;
                    string DataValue = dgvYizhu.CurrentCell.Value.ToString();
                    int flag = dal.updateLSYZ(id, DataType, DataValue);
                    if (flag > 0)
                    { }
                    else
                        MessageBox.Show("修改失败！");

                }
            }
        }
        private void dgvYizhuBind()
        {
            DataTable dt = dal.SelectLSYZ(mzjldID);
            if (dt.Rows.Count == 0)
            {
                DataTable dt2 = dal.Selectshuye(mzjldID);
                DataTable dte = dal.SelectLSYZ(mzjldID);
                dgvYizhu.DataSource = dte.DefaultView;
            }
            else
            {
                dgvYizhu.DataSource = dt.DefaultView;
            }

        }
        private void dgvYizhuBind1()
        {
            DataTable dt = dal.SelectLSYZ(mzjldID);
           dgvYizhu.DataSource = dt.DefaultView;
            
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            flag = dal.InsertLSYZ(mzjldID,PatID, DateTime.Now.ToString("MM-dd"), DateTime.Now.ToString("HH:mm"));
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
                    flag = dal.deleteLSYZ(id,mzjldID);
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
                flag = dal.updateLSYZdatetime(id, Date, Time);
                if (flag > 0)
                    dgvYizhuBind();
                else
                    MessageBox.Show("删除失败！");
            }

        }
        /// <summary>
        /// 使用医嘱包
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQUE_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = dal.SelectLSYZBao(cmbYZbao.Text.Trim());
                Dictionary<string, string> dis = new Dictionary<string, string>();
                foreach (DataRow ds in dt.Rows)
                {
                    dis.Clear();
                    dis.Add("mzjldid", mzjldID.ToString());
                    dis.Add("patid", PatID);
                    dis.Add("klDate", ds["klDate"].ToString());
                    dis.Add("klTime", ds["klTime"].ToString());
                    dis.Add("yizhu", ds["yizhu"].ToString());
                    dis.Add("yisheng", ds["yisheng"].ToString());
                    dis.Add("zxDate", ds["zxDate"].ToString());
                    dis.Add("zxTime", ds["zxTime"].ToString());
                    dis.Add("Remark", ds["Remark"].ToString());
                    int flg = dal.InsertLSYZ(dis);
                }
                dgvYizhuBind();
            }
            catch (Exception ex)
            {

                MessageBox.Show("操作失败！");
            }
        }

        //private void btnPrint_Click(object sender, EventArgs e)
        //{

         
        //}

        private void dgvYizhu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvYizhu.Rows.Count > 0)
            {
                int ColIndex = dgvYizhu.CurrentCell.ColumnIndex;
                if (ColIndex == 5)
                {
                    cmbXHHS.Visible = false;
                    cmbMZYS.Visible = false;
                    cmbyztype.Visible = false;
                    Rectangle rect = dgvYizhu.GetCellDisplayRectangle(dgvYizhu.CurrentCell.ColumnIndex, dgvYizhu.CurrentCell.RowIndex, false);
                    cmbyztype.Left = rect.Left;
                    cmbyztype.Top = rect.Top;
                    cmbyztype.Width = rect.Width;
                    cmbyztype.Height = rect.Height;
                    cmbyztype.Visible = true;
                    cmbyztype.Text = "";
                    cmbyztype.DroppedDown = true;
                }
                if (ColIndex == 6)
                {

                    cmbXHHS.Visible = false;
                    cmbMZYS.Visible = false;
                    cmbyztype.Visible = false;
                    Rectangle rect = dgvYizhu.GetCellDisplayRectangle(dgvYizhu.CurrentCell.ColumnIndex, dgvYizhu.CurrentCell.RowIndex, false);
                    cmbMZYS.Left = rect.Left;
                    cmbMZYS.Top = rect.Top;
                    cmbMZYS.Width = rect.Width;
                    cmbMZYS.Height = rect.Height;
                    cmbMZYS.Visible = true;
                    cmbMZYS.Text = "";
                    cmbMZYS.DroppedDown = true;
                }

                //else if (ColIndex == 7)
                //{
                //    cmbXHHS.Visible = false;
                //    cmbMZYS.Visible = false;
                //    cmbyztype.Visible = false;
                //    Rectangle rect = dgvYizhu.GetCellDisplayRectangle(dgvYizhu.CurrentCell.ColumnIndex, dgvYizhu.CurrentCell.RowIndex, false);
                //    cmbXHHS.Left = rect.Left;
                //    cmbXHHS.Top = rect.Top;
                //    cmbXHHS.Width = rect.Width;
                //    cmbXHHS.Height = rect.Height;
                //    cmbXHHS.Visible = true;
                //    cmbXHHS.Text = "";
                //    cmbXHHS.DroppedDown = true;
                //}
                else
                {
                    //cmbXHHS.Visible = false;
                    cmbMZYS.Visible = false;
                }
            }

        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //printDocument1.DefaultPageSettings.PaperSize =
            //           new System.Drawing.Printing.PaperSize("K16", 740, 1020);  
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
            dgvRowCount = dgvYizhu.Rows.Count;
            rowindex = 0;
            Fzrows = 0;
        }
        int dgvRowCount = 0;
        int rowindex = 0;
        int Fzrows = 0;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Pen ptp = Pens.Black;//普通画笔
            Pen pblue2 = new Pen(Brushes.Blue);
            pblue2.Width = 2;
            Pen pblack = new Pen(Brushes.Black);
            Pen pblack2 = new Pen(Brushes.Black, 2);
            Font ptzt = new Font("新宋体", 9);//普通字体
            Font ptzt2 = new Font("黑体", 9, FontStyle.Bold);//加粗十号
            Font ptzt3 = new Font("新宋体", 14, FontStyle.Bold);//加粗14号
            int y = 30; int x = 40; int y1 = 0;
            string title1 = "新 疆 医 科 大 学 第 五 附 属 医 院";
            e.Graphics.DrawString(title1, ptzt3, Brushes.Black, new Point(x + 180, y));
            string title2 = "临   时   医   嘱   单";
            y = y + 40;
            e.Graphics.DrawString(title2, ptzt3, Brushes.Black, new Point(x + 240, y));
            y = y + 40; y1 = y + 15;
            e.Graphics.DrawString("科室：" + tbKeshi.Text, ptzt, Brushes.Black, new Point(x, y));
            e.Graphics.DrawLine(Pens.Black, x + 30, y1, x + 150, y1);
            e.Graphics.DrawString("床号：" + tbBedNo.Text, ptzt, Brushes.Black, new Point(x + 160, y));
            e.Graphics.DrawLine(Pens.Black, x + 190, y1, x + 230, y1);
            e.Graphics.DrawString("姓名：" + tbPatName.Text, ptzt, Brushes.Black, new Point(x + 240, y));
            e.Graphics.DrawLine(Pens.Black, x + 270, y1, x + 360, y1);
            e.Graphics.DrawString("年龄：" + tbAge.Text, ptzt, Brushes.Black, new Point(x + 370, y));
            e.Graphics.DrawLine(Pens.Black, x + 400, y1, x + 460, y1);
            e.Graphics.DrawString("性别：" + cmbSex.Text, ptzt, Brushes.Black, new Point(x + 470, y));
            e.Graphics.DrawLine(Pens.Black, x + 500, y1, x + 560, y1);
            e.Graphics.DrawString("住院号：" + tbZhuyuanID.Text.Trim(), ptzt, Brushes.Black, new Point(x + 570, y));
            e.Graphics.DrawLine(Pens.Black, x + 610, y1, x + 730, y1);
            y = y + 40; y1 = y + 7;
            int Y2 =40 + 35 * 25;
            e.Graphics.DrawLine(pblack2, x + 0, y, x + 670, y);
            e.Graphics.DrawLine(pblack2, x + 0, y, x + 0, y + Y2);
            e.Graphics.DrawString("开立\n日期", ptzt2, Brushes.Black, new Point(x + 5, y1));
            e.Graphics.DrawLine(Pens.Black, x + 40, y, x + 40, y + Y2);
            e.Graphics.DrawString("开立\n时间", ptzt2, Brushes.Black, new Point(x + 45, y1));
            e.Graphics.DrawLine(Pens.Black, x + 80, y, x + 80, y + Y2);
            e.Graphics.DrawString("医嘱内容", ptzt2, Brushes.Black, new Point(x + 240, y1 + 10));
            e.Graphics.DrawLine(Pens.Black, x + 440, y, x + 440, y + Y2);
            e.Graphics.DrawString("医生签字", ptzt2, Brushes.Black, new Point(x + 445, y1 + 10));
            e.Graphics.DrawLine(Pens.Black, x + 500, y, x + 500, y + Y2);
            e.Graphics.DrawString("执行时间", ptzt2, Brushes.Black, new Point(x + 505, y1 + 10));
            e.Graphics.DrawLine(Pens.Black, x + 580, y, x + 580, y + Y2);
            e.Graphics.DrawString("执行者签字", ptzt2, Brushes.Black, new Point(x + 585, y1+10));
            //e.Graphics.DrawLine(Pens.Black, x + 600, y, x + 600, y + Y2);
            //e.Graphics.DrawString("执行\n时间", ptzt2, Brushes.Black, new Point(x + 605, y1));
            e.Graphics.DrawLine(Pens.Black, x + 670, y, x + 670, y + Y2);
            //e.Graphics.DrawString("执行者签字", ptzt2, Brushes.Black, new Point(x + 660, y1 + 10));
            //e.Graphics.DrawLine(pblack2, x + 760, y, x + 760, y + Y2);

            y = y + 40; y1 = y + 5;
            for (int i = 0; i <= 35; i++)
            {
                if (i == 35)
                    e.Graphics.DrawLine(pblack2, x + 0, y + i * 25, x + 670, y + i * 25);
                else
                    e.Graphics.DrawLine(Pens.Black, x + 0, y + i * 25, x + 670, y + i * 25);

            }
            for (int i = Fzrows; i < dgvYizhu.Rows.Count; i++)
            {
               
                e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[3].Value.ToString(), ptzt, Brushes.Black, new Point(x + 3, y1 ));
               
                e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[4].Value.ToString(), ptzt, Brushes.Black, new Point(x + 43, y1));              
                e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[5].Value.ToString(), ptzt, Brushes.Black, new Point(x + 90, y1));               
                e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[6].Value.ToString(), ptzt, Brushes.Black, new Point(x + 445, y1 ));
                e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[7].Value.ToString() +" "+ dgvYizhu.Rows[Fzrows].Cells[8].Value.ToString(), ptzt, Brushes.Black, new Point(x + 505, y1));               
                e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[9].Value.ToString(), ptzt, Brushes.Black, new Point(x + 583, y1 ));               
                //e.Graphics.DrawString(dgvYizhu.Rows[Fzrows].Cells[9].Value.ToString(), ptzt, Brushes.Black, new Point(x + 603, y1 ));              
              
                y1 = y1 + 25;
                rowindex++;
                if (rowindex % 35 == 0 && rowindex > 34)
                {
                    e.HasMorePages = true;
                    y = 0; y1 = 0;
                    Fzrows++;
                    break;
                }

                Fzrows++;
            }
        }
        /// <summary>
        /// 删除选择项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvYizhu.Rows.Count>0)
            {               
           
            int flag = 0;   
             int flag2 = 0;
                for (int i = 0; i < dgvYizhu.Rows.Count; i++)
                {
                    if ((bool)dgvYizhu.Rows[i].Cells["CoDelete"].EditedFormattedValue == true)
                    {
                        int id = Convert.ToInt32(dgvYizhu.Rows[i].Cells["xuhao"].Value);
                         flag2 = dal.deleteLSYZ(id,mzjldID);
                        if (flag2 > 0)
                            flag++;
                    }
                }
                if (flag > 0)
                {
                    dgvYizhuBind1();
                }
                else
                {
                    MessageBox.Show("请选择你要删除的医嘱！");
                }
            }
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click_1(object sender, EventArgs e)
        {

            if (dgvYizhu.Rows.Count > 0)
            {
                this.printPreviewDialog1.Document = printDocument1;
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    printDocument1.Print();
                printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
                printDocument1.DefaultPageSettings.PaperSize =
                         new System.Drawing.Printing.PaperSize("A4", 820, 1160);
                //PrintDGV2.Print(dgvYizhu, "昌吉回族自治州人民医院\n  手术室临时医嘱单",
                //    "科别：" + tbKeshi.Text + "  床号：" + tbBedNo.Text + "  姓名："
                // + tbPatName.Text + "  年龄：" + tbAge.Text + " 岁  性别：" + cmbSex.Text + "  住院号：" + tbZhuyuanID.Text, 1);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show("保存成功");
            //try
            //{
            //    List<string> LSYZ = new List<string>();
            //    for (int i = 0; i < dgvYizhu.Rows.Count; i++)
            //    {
            //        int id = Convert.ToInt32(dgvYizhu.Rows[i].Cells["xuhao"].Value);
            //        DataTable flag2 = dal.SelectLSYZ(id, mzjldID);
            //        if (flag2.Rows.Count > 0)
            //        { }
            //        else
            //        {
            //            LSYZ.Clear();
                      

            //        }


            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("保存失败");
            //}     
           
           
        }

       

    
    }
}
