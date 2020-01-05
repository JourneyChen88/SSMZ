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
    public partial class mzjld_Select : Form
    {
        #region <<Members>>
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public int mzjldid = 0;

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public mzjld_Select()
        {
            
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Smzjld_Load(object sender, EventArgs e)
        {
           
            try
            {
                lbMzs.Text = Program.customer.user_name;
                dataGridView1.DataSource = bll.xssbr1(dtDATE.Value.ToString("yyyy-MM-dd")).DefaultView;
                dataGridView1_CellClick(null, null);
                if (Program.customer.position == "主任医师" || Program.customer.position == "副主任医师" || Program.customer.position == "管理员")
                {
                    DeleteMZJLDmenu.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (this.rdNew.Checked == true)
                {
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["住院号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    string odate = dtDATE.Value.ToString("yyyy-MM-dd");

                    DataTable dt = new DataTable();
                    dt = bll.selectSinglemzjld(patid, odate);
                    if (dt.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("此病人当日已麻醉记录单存在，是否新建？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                            return;
                        else
                        {
                            this.Close();
                            mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, DateTime.Parse(odate), 0);
                            mzjld1.ShowDialog();
                            
                        }
                    }
                    else
                    {
                        this.Close();
                        mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, DateTime.Parse(odate), 0);
                        mzjld1.ShowDialog();
                        
                    }
                }

                else if (this.rdOld.Checked == true)
                {
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["住院号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    DateTime odate = DateTime.Parse(dtDATE.Value.ToShortDateString());
                    mzjldid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["麻醉编号"].Value);
                    this.Close();
                    mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, odate, mzjldid);
                    mzjld1.ShowDialog();                   
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        private void dtDATE_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                lbMzs.Text = Program.customer.user_name;
                DataTable dt;
                if (rdNew.Checked)
                {
                    dt = bll.xssbr1(dtDATE.Value.ToString("yyyy-MM-dd"));
                    dataGridView1.DataSource = dt.DefaultView;
                    if (dt.Rows.Count != 0)
                    {
                        this.lbOroomName.Text = dataGridView1.SelectedCells[0].Value.ToString();
                    }
                }
                else if (rdOld.Checked)
                {
                    dt = bll.xssbr2(dtDATE.Value.ToString("yyyy-MM-dd"));
                    dataGridView1.DataSource = dt.DefaultView;
                    if (dt.Rows.Count != 0)
                    {
                        this.lbOroomName.Text = dataGridView1.SelectedCells[0].Value.ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                this.lbOroomName.Text = dataGridView1.SelectedCells[0].Value.ToString();
            }


        }

        private void rdOld_CheckedChanged(object sender, EventArgs e)
        {
            if (rdOld.Checked)
            {
                DataTable dt = bll.xssbr2(dtDATE.Value.ToString("yyyy-MM-dd"));
                dataGridView1.DataSource = dt.DefaultView;
            }
            dataGridView1_CellClick(null, null);
        }

        private void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked)
            {
                dataGridView1.DataSource = bll.xssbr1(dtDATE.Value.ToString("yyyy-MM-dd")).DefaultView;
            }
            dataGridView1_CellClick(null, null);
        }

        private void DeleteMZJLDmenu_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (rdOld.Checked == true)
                {
                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["麻醉编号"].Value.ToString());
                    if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        int i = dal.delectadmin_JS_xin(id);
                        if (i > 0)
                        {
                            MessageBox.Show("删除成功");
                            dataGridView1.DataSource = bll.xssbr2(dtDATE.Value.ToString("yyyy-MM-dd")).DefaultView;
                        }
                    }
                }
                else MessageBox.Show("请选择“继续旧麻醉单”，然后再删除误添加的麻醉记录");

            }
        }
        /// <summary>
        /// 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (this.rdNew.Checked == true)
                {
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["住院号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    string odate = dtDATE.Value.ToString("yyyy-MM-dd");

                    DataTable dt = new DataTable();
                    dt = bll.selectSinglemzjld(patid, odate);
                    if (dt.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("此病人当日已麻醉记录单存在，是否新建？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                            return;
                        else
                        {
                            this.Close();
                            mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, DateTime.Parse(odate), 0);
                            mzjld1.ShowDialog();

                        }
                    }
                    else
                    {
                        this.Close();
                        mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, DateTime.Parse(odate), 0);
                        mzjld1.ShowDialog();

                    }
                }
                else if (this.rdOld.Checked == true)
                {
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["住院号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    DateTime odate = DateTime.Parse(dtDATE.Value.ToShortDateString());
                    mzjldid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["麻醉编号"].Value);
                    this.Close();
                    mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, odate, mzjldid);
                    mzjld1.ShowDialog();
                }
            }
        }
       
      
    }
}
