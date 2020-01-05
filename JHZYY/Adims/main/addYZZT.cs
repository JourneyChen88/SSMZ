using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using main.数据维护;
using main.科室事物管理;

namespace main
{
    public partial class addYZZT : Form
    {
        private ComboBox cmbXHHS = new ComboBox();
        private ComboBox cmbMZYS = new ComboBox();
        private ComboBox cmbyztype = new ComboBox();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        public addYZZT()
        {
            InitializeComponent();
        }
        private void addYZZT_Load(object sender, EventArgs e)
        {
            BindYZbao();//绑定医嘱包
            
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
            dgvYizhu.CellValueChanged += new DataGridViewCellEventHandler(dgvYizhu_CellValueChanged);
            dgvYizhuBind();
        }
        /// <summary>
        /// 绑定DGV
        /// </summary>
        private void dgvYizhuBind()
        {
            DataTable dt = dal.SelectLSYZBao(this.cmbYZbao.Text.Trim().ToString());
            dgvYizhu.DataSource = dt.DefaultView;
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
        /// <summary>
        /// 医师
        /// </summary>
        /// <param name="dt1"></param>
        private void BindMZYS(DataTable dt1)
        {
            cmbMZYS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbMZYS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbMZYS.Items.Add("");
        }
        /// <summary>
        /// 护士
        /// </summary>
        /// <param name="dt1"></param>
        private void BindXHHS(DataTable dt1)
        {
            cmbXHHS.Items.Clear();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                this.cmbXHHS.Items.Add(dt1.Rows[i][0]);
            }
            this.cmbXHHS.Items.Add("");
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
            
        private void cmbyztype_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvYizhu.CurrentCell.Value = cmbyztype.SelectedItem.ToString();
            cmbyztype.Visible = false;
        }

        /// <summary>
        /// 基础数据维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            DateSetup maintenance = new DateSetup();
            maintenance.Show();
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
                    int flag = dal.updateLSYZbao(id, DataType, DataValue);
                    if (flag > 0)
                    { }
                    else
                        MessageBox.Show("修改失败！");

                }
            }
        }

        private void dgvYizhu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvYizhu.Rows.Count > 0)
            {
                int ColIndex = dgvYizhu.CurrentCell.ColumnIndex;
                if (ColIndex == 4)
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
                else if (ColIndex == 5)
                {
                    cmbXHHS.Visible = false;
                    cmbMZYS.Visible = false;
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
                    cmbXHHS.Visible = false;
                    cmbMZYS.Visible = false;
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
                    cmbyztype.Visible = false;
                }
            }
        }
        /// <summary>
        /// 响应刷新按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShua_Click(object sender, EventArgs e)
        {
            BindYZbao();
            DataTable dtYZ = dal.GetAllYZtype();
            Bindnametype(dtYZ);
        }
        /// <summary>
        /// 录入执行日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void zxyzToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dgvYizhu.SelectedCells.Count == 1)
            {
                int id = int.Parse(dgvYizhu.CurrentRow.Cells["xuhao"].Value.ToString());
                string Date = DateTime.Now.ToString("MM-dd");
                string Time = DateTime.Now.ToString("HH:mm");
                int flag = 0;
                flag = dal.updateLSYZbaodatetime(id, Date, Time);
                if (flag > 0) 
                dgvYizhuBind();
                else
                    MessageBox.Show("修改失败！");


            }
        }
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int flag = 0;
            flag = dal.InsertLSYZBao(DateTime.Now.ToString("MM-dd"), DateTime.Now.ToString("HH:mm"), this.cmbYZbao.SelectedValue.ToString());
            if (flag > 0)
            dgvYizhuBind();
            else
                MessageBox.Show("添加失败！");
        }
        /// <summary>
        /// 删除此行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvYizhu.SelectedCells.Count == 1)
            {
                int id = int.Parse(dgvYizhu.CurrentRow.Cells["xuhao"].Value.ToString());

                if (MessageBox.Show(" 您确定要删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int flag = 0;
                    flag = dal.deleteLSYZBao(id);
                    if (flag > 0) 
                    dgvYizhuBind();
                    else
                        MessageBox.Show("删除失败！");

                }
            }
        }

        private void cmbYZbao_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvYizhuBind();
        }




    }
}
