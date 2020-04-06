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
    public partial class addHSDJ : Form
    {
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        ComboBox cmbSF = new ComboBox();
        public addHSDJ()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addHSDJ_Load(object sender, EventArgs e)
        {
            BindKS();
            BindDaoRu();
            BindDGV();
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            cmbSF.Visible = false;
            cmbSF.DropDownStyle = ComboBoxStyle.DropDown;
            cmbSF.Items.Add("是");
            cmbSF.Items.Add("否");           
            dataGridView1.Controls.Add(cmbSF);
            cmbSF.SelectedIndexChanged += new EventHandler(cmbSF_SelectedIndexChanged);
            cmbSF.MouseLeave += new EventHandler(cmbSF_MouseLeave);

        }
        private void cmbSF_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmbSF.SelectedItem.ToString();
            cmbSF.Visible = false;
        }
        private void cmbSF_MouseLeave(object sender, EventArgs e)
        {
            cmbSF.Visible = false;
        }
        /// <summary>
        /// 导入信息
        /// </summary>
        private void BindDaoRu()
        {
            DataTable dt1 = dal.GetHSDJ(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            List<string> addHS = new List<string>();
            int result = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                addHS.Clear();
                addHS.Add(dr["Odate"].ToString());
                addHS.Add(dr["Oroom"].ToString());
                addHS.Add(dr["Patbedno"].ToString());
                addHS.Add(dr["Patname"].ToString());
                addHS.Add(dr["PatZhuYuanID"].ToString());
                addHS.Add(dr["Patage"].ToString());
                addHS.Add(dr["Patsex"].ToString());
                addHS.Add(dr["Oname"].ToString());
                addHS.Add(dr["OS"].ToString() + " " + dr["OS1"].ToString() + " " + dr["OS2"].ToString() + " " + dr["OS3"].ToString() + " " + dr["OS4"].ToString());
                addHS.Add(dr["AP1"].ToString() + " " + dr["AP2"].ToString() + " " + dr["AP3"].ToString());
                addHS.Add(dr["ON1"].ToString() + " " + dr["ON2"].ToString());
                addHS.Add(dr["SN1"].ToString() + " " + dr["SN2"].ToString());
                if (dr["IsJizhen"].ToString() == "1")
                {
                    addHS.Add("是");
                }
                else
                {
                    addHS.Add("否");
                }
                addHS.Add(dr["Patdpm"].ToString());
                DataTable dt = dal.SelectHSDJs(dr["PatZhuYuanID"].ToString(), dr["Odate"].ToString());

                if (dt.Rows.Count == 0)
                    result = dal.InsertHSDj(addHS);
            }
        }
        /// <summary>
        /// 导入信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndaoru_Click(object sender, EventArgs e)
        {
            BindDaoRu();
            BindDGV();
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void BindDGV()
        {
            DataTable dts = dal.SelectHSDJ(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            if (dts.Rows.Count == 0)
            {
                BindDaoRu();
            }
            DataTable ds = new DataTable();
            if (cmbKS.Text == "")
            {
                ds = dal.SelectHSDJ(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            }
            else
            {
                ds = dal.SelectHSDJ(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"), cmbKS.Text);
            }
            dataGridView1.DataSource = ds.DefaultView;

        }
        /// <summary>
        /// 绑定科室
        /// </summary>
        private void BindKS()
        {
            DataTable dt = dal.SelectHSDJKS();
            cmbKS.DataSource = dt;
            cmbKS.DisplayMember = "Patdpm";
        }
        /// <summary>
        /// 改变时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            BindDGV();
        }
        /// <summary>
        /// 改变科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKS_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDGV();
        }
        /// <summary>
        /// 打印登记表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                PrintHSDj.Print(dataGridView1, "昌吉州人民医院手术登记本", "", 1);
            }

        }
        /// <summary>
        ///修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    string ID = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                    if (ID != "")
                    {
                        string RoomName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
                        string Value_Info = dataGridView1.CurrentCell.Value.ToString();
                        int res = dal.UpdateHSDJ(ID, RoomName, Value_Info, dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("修改失败");
                BindDGV();
            }
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int ColIndex = dataGridView1.CurrentCell.ColumnIndex;
                if (ColIndex == 11)
                {
                    SelectMZYSandHushi F1 = new SelectMZYSandHushi(1, txtMZYS);
                    F1.ShowDialog();
                    dataGridView1.CurrentCell.Value = F1.Strdata;
                }
                else if (ColIndex == 12)
                {
                    SelectMZYSandHushi F1 = new SelectMZYSandHushi(2, txtMZYS);
                    F1.ShowDialog();
                    dataGridView1.CurrentCell.Value = F1.Strdata;
                }
                else if (ColIndex == 13)
                {
                    SelectMZYSandHushi F1 = new SelectMZYSandHushi(2, txtMZYS);
                    F1.ShowDialog();
                    dataGridView1.CurrentCell.Value = F1.Strdata;
                }
                else if (ColIndex == 15)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmbSF.Left = rect.Left; cmbSF.Top = rect.Top;
                    cmbSF.Width = rect.Width; cmbSF.Height = rect.Height;
                    cmbSF.Visible = true; cmbSF.Text = "";
                }
                else if (ColIndex == 16)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmbSF.Left = rect.Left; cmbSF.Top = rect.Top;
                    cmbSF.Width = rect.Width; cmbSF.Height = rect.Height;
                    cmbSF.Visible = true; cmbSF.Text = "";
                }
                else if (ColIndex == 17)
                {
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmbSF.Left = rect.Left; cmbSF.Top = rect.Top;
                    cmbSF.Width = rect.Width; cmbSF.Height = rect.Height;
                    cmbSF.Visible = true; cmbSF.Text = "";
                }
                else
                {
                    cmbSF.Visible = false;
                }
            }
          

        }
        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 删除一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                if (DialogResult.OK==MessageBox.Show("是否删除！","提示",MessageBoxButtons.OKCancel,MessageBoxIcon.Question))
                {
                    string id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    int reault = dal.DeleteHSDJ(id);
                    if (reault>0)
                    {
                        BindDGV();
                    }
                    else
                    {
                        MessageBox.Show("删除失败");
                    }
                }
              
              

            }
        }
       
    }
}
