using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

using WindowsFormsControlLibrary5;
using System.Net.NetworkInformation;
using System.Threading;

using System.Runtime.InteropServices;
namespace main
{
    public partial class MzjldSelet : Form
    {
        #region <<Members>>

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        public int mzjldid = 0;

        #endregion
        string operAddress = "";
        admin_T_SQL at = new admin_T_SQL();//
        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public MzjldSelet()
        {
            InitializeComponent();
        }

        #endregion

        #region <<Events>>
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reserverdaValue);
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Smzjld_Load(object sender, EventArgs e)
        {
            operAddress = Program.customer.yiyuanType;
            BindNewPatInfo();
        }
        public void BindNewPatInfo()
        {
            lbMzs.Text = Program.customer.user_name;
            dataGridView1.DataSource = bll.xssbr1(dtDATE.Value.ToString("yyyy-MM-dd"), operAddress).DefaultView;
            dataGridView1_CellClick(null, null);
        }
        public void BindOldPatInfo()
        {
            lbMzs.Text = Program.customer.user_name;
            dataGridView1.DataSource = bll.xssbr2(dtDATE.Value.ToString("yyyy-MM-dd"), operAddress).DefaultView;
            dataGridView1_CellClick(null, null);
        }
        public bool PingHost(string Address, int TimeOut = 1000)
        {
            using (System.Net.NetworkInformation.Ping PingSender = new System.Net.NetworkInformation.Ping())
            {
                PingOptions Options = new PingOptions();
                Options.DontFragment = true;
                string Data = "test";
                byte[] DataBuffer = Encoding.ASCII.GetBytes(Data);
                PingReply Reply = PingSender.Send(Address, TimeOut, DataBuffer, Options);
                if (Reply.Status == IPStatus.Success)
                    return true;
                return false;
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
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["病人编号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    DateTime odate = DateTime.Parse(dtDATE.Value.Date.ToString("yyyy-MM-dd"));
                    string time = dtDATE.Value.Date.ToString("yyyy-MM-dd");
                    DataTable dt = new DataTable();

                    #region 打开麻醉记录单
                    dt = bll.selectSinglemzjld1(patid, time);
                    if (dt.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("此病人当日已麻醉记录单存在，是否新建？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel)
                        {
                            rdOld.Checked = true;
                            return;
                        }
                        else
                        {
                            mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, odate, 0);
                            mzjld1.ShowDialog();
                            this.Close();

                        }
                    }
                    else
                    {
                        mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, odate, 0);
                        mzjld1.ShowDialog();
                        this.Close();

                    }
                    #endregion
                }
                else if (this.rdOld.Checked == true)
                {

                    //int mzid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["病人编号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    DateTime odate = DateTime.Parse(dtDATE.Value.ToShortDateString());
                    mzjldid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["麻醉编号"].Value);

                    mzjldEdit mzjld1 = new mzjldEdit(patid, oroom, odate, mzjldid);
                    mzjld1.ShowDialog();
                    this.Close();

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
                    dt = bll.xssbr1(dtDATE.Value.ToString("yyyy-MM-dd"), operAddress);
                    dataGridView1.DataSource = dt.DefaultView;
                    if (dt.Rows.Count != 0)
                    {
                        this.lbOroomName.Text = dataGridView1.SelectedCells[0].Value.ToString();
                    }
                }
                else if (rdOld.Checked)
                {
                    dt = bll.xssbr2(dtDATE.Value.ToString("yyyy-MM-dd"), operAddress);
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
                BindOldPatInfo();
            }
            dataGridView1_CellClick(null, null);
        }

        private void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked)
            {
                BindNewPatInfo();
            }
            dataGridView1_CellClick(null, null);
        }

        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id;
            if (rdOld.Checked == true)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["麻醉编号"].Value.ToString());
                    if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        int a = Convert.ToInt32(at.select_zt(id, dtDATE.Value.ToString("yyyy-MM-dd")));
                        if (a == 0)
                        {
                            int i = at.delectadmin_mzjld(id);
                            if (i > 0)
                                BindOldPatInfo();
                            else
                                MessageBox.Show("删除失败，请重试！!");
                        }
                        else
                        {
                            MessageBox.Show("此条信息有效记录不能执行删除！！");
                            #region MyRegion
                            //if (MessageBox.Show("此条信息记录过了确定删除吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            //{
                            //    int i = at.delectadmin_JS_xin(id);
                            //    if (i > 0)
                            //    {
                            //        MessageBox.Show("删除成功");
                            //        dataGridView1.DataSource = bll.xssbr2(dtDATE.Value.ToString("yyyy-MM-dd")).DefaultView;
                            //    }
                            //}
                            #endregion
                        }
                    }
                }
            }
            else
                MessageBox.Show("请选择旧麻醉记录单删除！！");
        }

        private void updateOroomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.rdNew.Checked == true)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("确定要修改手术间？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        string patid = dataGridView1.SelectedRows[0].Cells["病人编号"].Value.ToString();
                        UpdateOroom f1 = new UpdateOroom(patid, dtDATE.Value.ToString("yyyy-MM-dd"));
                        f1.ShowDialog();
                        rdNew.Checked = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("正在进行手术或已完成手术，不能更换手术间！！");
                rdNew.Checked = true;
            }
        }


    }
}
