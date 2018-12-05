///************************************
///Updated By        : Senvi
///************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;  
using System.Threading;

namespace main
{
    public partial class mzjldList : Form
    {
        #region <<Members>>

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();

        #endregion

        #region <<Constructors>>

        /// <summary>
        /// 构造函数
        /// </summary>
        public mzjldList()
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
        private void mzjldList_Load(object sender, EventArgs e)
        {
            try
            {   

                dateEdit1.DateTime = new DateTime(1800,1,1);
                dateEdit2.DateTime = DateTime.Now;
                DataTable doctors = new DataTable();
                doctors = bll.select_doctors();
                gridLookUpEdit1.Properties.DataSource = doctors;  //数据源
                gridLookUpEdit1.Properties.DisplayMember = "姓名"; //绑定Text显示的字段源名称
                gridLookUpEdit1.Properties.ValueMember = "编号"; //绑定Value字段源名称
                gridLookUpEdit2.Properties.DataSource = doctors;  //数据源
                gridLookUpEdit2.Properties.DisplayMember = "姓名"; //绑定Text显示的字段源名称
                gridLookUpEdit2.Properties.ValueMember = "编号"; //绑定Value字段源名称
                sex_txt.Properties.Items.Add(new ImageComboBoxItem("男"));
                sex_txt.Properties.Items.Add(new ImageComboBoxItem("女"));
                DataTable mzxg = new DataTable();
                mzxg = adims_DAL.AdimsProvider.GetData1("mazuixiaoguo", "麻醉效果");
                for (int i = 0; i < mzxg.Rows.Count; i++)
                    imageComboBoxEdit1.Properties.Items.Add(new ImageComboBoxItem(mzxg.Rows[i][1]));
                btnSearch_Click(null, null);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {  //if(this.lookupedit.editvalue==null ||this.lookupedit.editvalue.tostring()=="nulltext")
                string sqlWhere = "";
                sqlWhere += " and mzkssj between'" + dateEdit1.DateTime + "' And  '"+dateEdit2.DateTime+"' ";
                if (binganhao_txt.Text != "") sqlWhere += " and  adims_otypesetting.id='" + binganhao_txt.Text + "' ";
                if (patid_txt.Text != "") sqlWhere += " and adims_mzjld.patid='" + patid_txt.Text + "'  ";
                if (age1_txt.Text != "") sqlWhere += " and patage>='" + age1_txt.Text + "' ";
                if (age2_txt.Text != "") sqlWhere += " and patage<='" + age2_txt.Text + "'  ";
                if (name_txt.Text != "") sqlWhere += " and patname='" + name_txt.Text + "'  ";
                if (sex_txt.Text != "") sqlWhere += " and patsex='" + sex_txt.Text + "'  ";
                if (hbz_txt.Text != "") sqlWhere += " and hbz='" + hbz_txt.Text + "'  ";
                StringBuilder Builder1 = new StringBuilder();
                StringBuilder Builder2 = new StringBuilder();
                Builder2.AppendFormat(" and mzys like '%{0}%'  ", gridLookUpEdit2.Text);
                Builder1.AppendFormat(" and ssys like '%{0}%'  ", gridLookUpEdit1.Text);
                if (gridLookUpEdit2.Text.Trim() != "") sqlWhere += Builder2.ToString();
                if (imageComboBoxEdit1.Text != "") sqlWhere += " and mzxg='" + imageComboBoxEdit1.Text + "'  ";
                if (gridLookUpEdit1.Text.Trim() != "") sqlWhere += Builder1.ToString();
                if (ssmc_txt.Text != "") sqlWhere += " and ssmc='" + ssmc_txt.Text + "'  ";
                if (tw_txt.Text != "") sqlWhere += " and tw='" + tw_txt.Text + "'  ";
                if (sqyy_txt.Text != "") sqlWhere += " and sqyy='" + sqyy_txt.Text + "'  ";
                if (jhxm_txt.Text != "") sqlWhere += " and jhxm='" + jhxm_txt.Text + "'  ";
                if (mzfa_txt.Text != "") sqlWhere += " and mzfa='" + mzfa_txt.Text + "'  ";
                if (szzd_txt.Text != "") sqlWhere += " and szzd='" + szzd_txt.Text + "' ";
              //  DataTable dt = bll.GetMzjldList(sqlWhere);
                DataTable dt = bll.query_mzjld(sqlWhere);
                grdMzjld.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "出现异常，请检查！");
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            binganhao_txt.Text = "";
            patid_txt.Text = "";
            age1_txt.Text = "";
            age2_txt.Text = "";
            name_txt.Text = "";
            sex_txt.Text = "";
            hbz_txt.Text = "";
            gridLookUpEdit2.Text = "";
            imageComboBoxEdit1.Text = "";
            gridLookUpEdit1.Text= "";
            ssmc_txt.Text = "";
            tw_txt.Text = "";
            sqyy_txt.Text = "";
            jhxm_txt.Text = "";
            mzfa_txt.Text = "";
            szzd_txt.Text = "";
            btnSearch_Click(null,null);
        }

        /// <summary>
        /// grdvMzjld 双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdvMzjld_DoubleClick(object sender, EventArgs e)
        {
            //string mzjldID = Convert.ToString(grdvMzjld.GetFocusedRowCellValue("MID"));
            //if (string.IsNullOrEmpty(mzjldID)) return;
            //int patID = Convert.ToInt32(grdvMzjld.GetFocusedRowCellValue("patid"));
            //mzjldEdit mzjldxs = new mzjldEdit(mzjldID, patID);
            //mzjldxs.Show();
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdvMzjld_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }

        #endregion

        #region <<Methods>>

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void BindLookUpEdit()
        {

        }

        #endregion

        private void grdMzjld_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //string MZJLD_ID = grdMzjld. ToString();
            //mzjldEdit mzjldform = new mzjldEdit(MZJLD_ID, Convert.ToInt32(dataGridView1.SelectedCells[0].Value));
            //mzjldform.ShowDialog();
        }
    }
}
