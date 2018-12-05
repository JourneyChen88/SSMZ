using adims_DAL.Flows;
using adims_Utility;
using System;
using System.Data;
using System.Windows.Forms;

namespace main
{
    public partial class Mzjld_Select : Form
    {
        #region <<Members>>
      
        MzjldDal _MzjldDal = new MzjldDal();
        OperScheduleDal _PaibanDal = new OperScheduleDal();
        public int mzjldid = 0;

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public Mzjld_Select()
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
                BindNewOperInfo();
                dataGridView1_CellClick(null, null);
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
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["病人编号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    string odate = dtDATE.Value.ToString("yyyy-MM-dd");

                    DataTable dt = new DataTable();
                    dt = _MzjldDal.GetMzjldByPatId(patid);
                    if (dt.Rows.Count > 0)
                    {
                        DialogResult result = MessageBox.Show("此病人当日麻醉记录单已存在，是否新建？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                            return;
                        else
                        {
                            this.Close();
                            MzjldEdit mzjld1 = new MzjldEdit(patid, oroom, DateTime.Parse(odate), 0);
                            mzjld1.Show();
                        }
                    }
                    else
                    {
                        this.Close();
                        MzjldEdit mzjld1 = new MzjldEdit(patid, oroom, DateTime.Parse(odate), 0);
                        mzjld1.Show();
                    }
                }

                else if (this.rdOld.Checked == true)
                {
                    string patid = Convert.ToString(dataGridView1.SelectedRows[0].Cells["病人编号"].Value);
                    string oroom = dataGridView1.SelectedRows[0].Cells["手术间名称"].Value.ToString();
                    DateTime odate = DateTime.Parse(dtDATE.Value.ToShortDateString());
                    mzjldid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["麻醉编号"].Value);
                    this.Close();
                    MzjldEdit mzjld1 = new MzjldEdit(patid, oroom, odate, mzjldid);
                    mzjld1.Show();
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
                    dt = BindNewOperInfo();
                    if (dt.Rows.Count != 0)
                    {
                        this.lbOroomName.Text = dataGridView1.SelectedCells[0].Value.ToString();
                    }
                }
                else if (rdOld.Checked)
                {
                    dt = _MzjldDal.GetMzjldByOtime(dtDATE.Value.ToString("yyyy-MM-dd"));
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
        /// <summary>
        /// 绑定新建排班
        /// </summary>
        /// <returns></returns>
        private DataTable BindNewOperInfo()
        {
            DataTable dt = _PaibanDal.GetPaibanByOdateAndOstate(dtDATE.Value.ToString("yyyy-MM-dd"), (int)EnumCreator.Ostate.已排班);
            dataGridView1.DataSource = dt.DefaultView;
            return dt;
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
                dataGridView1.DataSource = this._MzjldDal.GetMzjldByOtime(dtDATE.Value.ToString("yyyy-MM-dd")).DefaultView;
            }
            dataGridView1_CellClick(null, null);
        }

        private void rdNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNew.Checked)
            {
                DataTable dt = _PaibanDal.GetPaibanByOdateAndOstate(dtDATE.Value.ToString("yyyy-MM-dd"), (int)EnumCreator.Ostate.已排班);
                dataGridView1.DataSource = dt.DefaultView;
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

                        int i = _MzjldDal.DeleteMzjldById(id);
                        if (i > 0)
                        {
                            MessageBox.Show("删除成功");
                            dataGridView1.DataSource = _MzjldDal.GetMzjldByOtime(dtDATE.Value.ToString("yyyy-MM-dd")).DefaultView;
                        }
                    }
                }
                else MessageBox.Show("请选择“继续旧麻醉单”，然后再删除误添加的麻醉记录");

            }
        }
    }
}
