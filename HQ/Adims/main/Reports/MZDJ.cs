using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL.Dics;

namespace main
{
    public partial class MZDJ : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        private ComboBox cmbMZFF = new ComboBox();
        DataDicDal _DataDicDal = new DataDicDal();
        adims_BLL.AdimsController BLL = new adims_BLL.AdimsController();
        public MZDJ()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addMZDJ_Load(object sender, EventArgs e)
        {
            BindDGV();
            DataTable dtMZFF = _DataDicDal.GetMazuiFangfaAll();
            BindMZFF(dtMZFF);
            cmbMZFF.Visible = false;
            cmbMZFF.SelectedIndexChanged += new EventHandler(cmbMZFF_SelectedIndexChanged);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            dataGridView1.Controls.Add(cmbMZFF);
        }
        /// <summary>
        /// 绑定dgv
        /// </summary>
        private void BindDGV() 
        {
            DataTable dt = bll.Get_mzdj(dtVisitDate.Value.Date.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt.DefaultView;
        }
        private void cmbMZFF_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = cmbMZFF.SelectedItem.ToString();
            cmbMZFF.Visible = false;
        }
        /// <summary>
        /// 绑定麻醉方式
        /// </summary>
        /// <param name="dt1"></param>
        private void BindMZFF(DataTable dt1)
        {
            cmbMZFF.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZFF.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbMZFF.Items.Add("");
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
                if (dataGridView1.Rows.Count > 0)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (dataGridView1.CurrentCell.Value.ToString() == "")
                        {
                            MessageBox.Show("时间不能为空，请重新修改");
                            return;
                        }
                    }                   
                    string ID = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    string RoomName = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
                    string Value_Info = dataGridView1.CurrentCell.Value.ToString();
                    bll.Updatemzdj_Data(ID, RoomName, Value_Info);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("时间输入格式不正确！\n输入格式为yyyy-MM-dd HH:mm");
                BindDGV();
            }
        }
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                //DataTable dts = bll.Get_mzdjs();
                //int count =Convert.ToInt32(dts.Rows[0][0].ToString());
                string dtTime = dtVisitDate.Value.Date.ToString("yyyy-MM-dd ")+DateTime.Now.ToString("HH:mm");
                bll.Insert_mzdj(dtTime);
                BindDGV();
            }
            else if (dataGridView1.Rows.Count > 0)
            {
                if (dataGridView1.CurrentCell.IsInEditMode == false)
                {
                    //DataTable dts = bll.Get_mzdjs();
                    //int count = Convert.ToInt32(dts.Rows[0][0].ToString());
                    string dtTime = dtVisitDate.Value.Date.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm");
                    bll.Insert_mzdj(dtTime);
                    BindDGV();
                }
                else MessageBox.Show("单元格内不能为编辑状态！");
            }
        }
        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                 string idno = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                 int rs = bll.delete_mzdj(idno);
                 if (rs>0)
                 {
                     BindDGV();
                 }
            }
        }

        private void dtVisitDate_ValueChanged(object sender, EventArgs e)
        {
            BindDGV();
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Select_MZTJ f1 = new Select_MZTJ();
            f1.ShowDialog();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                int ColIndex = dataGridView1.CurrentCell.ColumnIndex;
                if (ColIndex==4)
                {
                     cmbMZFF.Visible = false;
                    cmbMZFF.Visible = false;
                    cmbMZFF.Visible = false;
                    Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);
                    cmbMZFF.Left = rect.Left;
                    cmbMZFF.Top = rect.Top;
                    cmbMZFF.Width = rect.Width;
                    cmbMZFF.Height = rect.Height;
                    cmbMZFF.Visible = true;
                    cmbMZFF.Text = "";
                    cmbMZFF.DroppedDown = true;
                }
            }
         
        }
    }
}
