using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_BLL;

namespace main
{
    public partial class MZYYJF : Form
    {
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        adims_DAL.HisDB_Help HisHelp = new adims_DAL.HisDB_Help();
        PACU_DAL pacu = new PACU_DAL();
        DataTable dtGG;
        private ComboBox cmbLBs = new ComboBox();
        private ComboBox cmbKS = new ComboBox();
        private ComboBox cmbXMMC = new ComboBox();
        private ComboBox cmbGG = new ComboBox();
        private ComboBox cmbDW = new ComboBox();
        bool isGG = false;
        bool isXM = false;
        int rows = 0;
        public MZYYJF()
        {
            InitializeComponent();
        }
        string PGID, MZID;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mzid"></param>
        /// <param name="patid"></param>
        public MZYYJF(string mzid, string patid)
        {
            PGID = patid;
            MZID = mzid;
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MZYYJF_Load(object sender, EventArgs e)
        {
            string uid = Program.customer.uid;
            string name = Program.customer.user_name;
            DataTable dt = pacu.GetSelectHisCode(uid, name);
            if (dt.Rows.Count > 0)
            {
                this.lbnames.Text = dt.Rows[0]["hisCode"].ToString();
            }
            BindPatInfo();
            BindMZYYType();
            this.RYTime.Enabled = false;
            BindMZYYZTType();
            BindMZKS();
            MZJFDGV();//绑定DGV           
            cmbLBs.Visible = false;
            cmbLBs.SelectedIndexChanged += new EventHandler(cmbLBs_SelectedIndexChanged);
            cmbKS.Visible = false;
            cmbKS.SelectedIndexChanged += new EventHandler(cmbKS_SelectedIndexChanged);
            cmbXMMC.Visible = false;
            cmbXMMC.SelectedIndexChanged += new EventHandler(cmbXMMC_SelectedIndexChanged);
            cmbGG.Visible = false;
            cmbGG.SelectedIndexChanged += new EventHandler(cmbGG_SelectedIndexChanged);
            //cmbDW.Visible = false;
            //cmbDW.SelectedIndexChanged += new EventHandler(cmbDW_SelectedIndexChanged);
            dgvMZYYJF.Controls.Add(cmbLBs);
            dgvMZYYJF.Controls.Add(cmbKS);
            dgvMZYYJF.Controls.Add(cmbXMMC);
            dgvMZYYJF.Controls.Add(cmbGG);
            //dgvMZYYJF.Controls.Add(cmbDW);
            dgvMZYYJF.CellValueChanged += new DataGridViewCellEventHandler(dgvMZYYJF_CellValueChanged);
            this.cmbKS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbKS_KeyDown);
            this.cmbKS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbKS_KeyPress);
            this.cmbXMMC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbXMMC_KeyDown);
            this.cmbXMMC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbXMMC_KeyPress);
        }
        /// <summary>
        /// 绑定麻醉用药类别
        /// </summary>
        private void BindMZYYType()
        {
            DataTable dt = pacu.GetSelectMZZLB();
            cmbLBs.DataSource = dt;
            cmbLBs.DisplayMember = "LBname";
            //DataTable dt = HisHelp.GetYP();
            //cmbLB.DataSource = dt;
            //cmbLB.DisplayMember = "type_on_an";
        }
        /// <summary>
        /// 绑定麻醉组套
        /// </summary>
        private void BindMZKS()
        {
            DataTable dt = pacu.GetSelectMZZTtype();
            cmbZT.DataSource = dt;
            cmbZT.ValueMember = "id";
            cmbZT.DisplayMember = "name";
        }
        /// <summary>
        /// 科室绑定
        /// </summary>
        private void BindMZYYZTType()
        {
            DataTable dt = pacu.GetSelectYYKS();
            cmbKS.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.cmbKS.Items.Add(dt.Rows[i][0]);
            }
            //cmbKS.DataSource = dt;
            //cmbKS.DisplayMember = "KSname";
        }
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void XMMCBind(string LB)
        {
            DataTable dt;
            if (cmbLBs.Text.Trim() == "西药")
                dt = HisHelp.JianSuoType(LB);
            else
                dt = HisHelp.JianSuoTypeALL(LB);
            cmbXMMC.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbXMMC.Items.Add(dr[0].ToString());
            }
        }
        /// <summary>
        /// 绑定规格
        /// </summary>
        /// <param name="LB"></param>
        /// <param name="item_name"></param>
        private void GGBind(string LB, string item_name)
        {
            DataTable dt;
            if (cmbLBs.Text.Trim() == "西药")
                dt = HisHelp.GetAdims_SelectGG(LB, item_name);
            else
                dt = HisHelp.GetAdims_SelectGGALL(LB, item_name);
            cmbGG.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbGG.Items.Add(dr[0].ToString());
            }
            //cmbGG.DataSource = dt1;
            //cmbGG.DisplayMember = "item_spec";             
        }

        /// <summary>
        /// 绑定dgv
        /// </summary>
        private void MZJFDGV()
        {
            DataTable dt = pacu.GetSelectMZJF(tbZhuyuanNo.Text,MZID);
            dgvMZYYJF.DataSource = dt;
        }
        /// <summary>
        /// 保存患者的基本信息
        /// </summary>
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = DAL.GetALLPAIBAN(PGID);
            if (dt.Rows.Count > 0)
            {
                tbZhuyuanNo.Text = dt.Rows[0]["PatZhuYuanid"].ToString();
                tbPatname.Text = dt.Rows[0]["Patname"].ToString();
                tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
                tbSex.Text = dt.Rows[0]["Patsex"].ToString();
                tbAge.Text = dt.Rows[0]["Patage"].ToString();
            }

        }

        /// <summary>
        /// 药品类别
        /// </summary>
        private void cmbLBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMZYYJF.CurrentCell.Value = cmbLBs.Text.ToString();
            cmbLBs.Visible = false;
        }
        /// <summary>
        /// 科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKS_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMZYYJF.CurrentCell.Value = cmbKS.Text.ToString();
            cmbKS.Visible = false;
        }
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbXMMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMZYYJF.CurrentCell.Value = cmbXMMC.Text.ToString();
            cmbXMMC.Visible = false;
            if (isXM)
            {
                string LB = dgvMZYYJF.CurrentRow.Cells["expense_item_class"].Value.ToString();
                DataTable dtYY;
                if (cmbLBs.Text.Trim() == "西药")
                    dtYY = HisHelp.GetAdims_SelectMZYY(LB, cmbXMMC.Text);
                else
                    dtYY = HisHelp.GetAdims_SelectMZYYALL(LB, cmbXMMC.Text);
                cmbGG.Text = "";
                if (dtYY.Rows.Count > 1)
                {
                    GGBind(LB, cmbXMMC.Text);
                    isGG = true;
                }
                else if (dtYY.Rows.Count == 1)
                {
                    dgvMZYYJF.CurrentRow.Cells["item_spec"].Value = dtYY.Rows[0]["item_spec"].ToString();
                    dgvMZYYJF.CurrentRow.Cells["Signer"].Value = dtYY.Rows[0]["unit"].ToString();
                    dgvMZYYJF.CurrentRow.Cells["DJ"].Value = dtYY.Rows[0]["jiage"].ToString();
                    dgvMZYYJF.CurrentRow.Cells["Sl"].Value = "1";
                    string item_code = dtYY.Rows[0]["item_code"].ToString();//项目代码
                    string Ybbz = dtYY.Rows[0]["ybbz"].ToString();//医保标志
                    string FitemID = dtYY.Rows[0]["FitemID"].ToString();//项目标准号
                    string id = dgvMZYYJF.CurrentRow.Cells["id"].Value.ToString();
                    string FBatchID = dtYY.Rows[0]["FBatchID"].ToString();
                    pacu.UpdateMZZJF(id, cmbXMMC.Text, dgvMZYYJF.CurrentRow.Cells["item_spec"].Value.ToString(), dgvMZYYJF.CurrentRow.Cells["Signer"].Value.ToString(), dgvMZYYJF.CurrentRow.Cells["DJ"].Value.ToString(), item_code, Ybbz, FitemID, FBatchID);
                    MZJFDGV();
                }

                isXM = false;
            }
        }
        /// <summary>
        /// 规格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbGG_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgvMZYYJF.CurrentCell.Value = cmbGG.Text.ToString();
            cmbGG.Visible = false;
            //if (cmbGG.Text != "")
            //{
            //    if (cmbLBs.Text.Trim() == "西药")
            //        dtGG = HisHelp.GetAdims_SelectDW(cmbLBs.Text, cmbXMMC.Text, cmbGG.Text);
            //    else
            //        dtGG = HisHelp.GetAdims_SelectDWALL(cmbLBs.Text, cmbXMMC.Text, cmbGG.Text);
            //    cmbDW.Text = "";
            //    if (dtGG.Rows.Count > 1)
            //    {
            //        cmbDW.DataSource = dtGG;
            //        cmbDW.DisplayMember = "unit";
            //    }
            //    else
            //    {
            //        cmbDW.Enabled = false;
            //    }
            //}
        }
        ///// <summary>
        ///// 单位
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void cmbDW_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dgvMZYYJF.CurrentCell.Value = cmbDW.Text.ToString();
        //    cmbGG.Visible = false;
        //}
        ///// <summary>
        ///// 单元格验证时发生
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dgvMZYYJF_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        //{
        //    //if (e.ColumnIndex == 3)
        //    //{

        //    //}
        //    //else if (e.ColumnIndex == 5)
        //    //{
        //    //}
        //}


        private void dgvMZYYJF_CellValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvMZYYJF.Rows.Count > 0)
                {
                    int Indes = dgvMZYYJF.CurrentCell.ColumnIndex;
                    if (Indes == 3)
                    {
                        string id = dgvMZYYJF.CurrentRow.Cells["id"].Value.ToString();
                        string LB = dgvMZYYJF.CurrentRow.Cells["expense_item_class"].Value.ToString();
                        DataTable dtYY;
                        if (cmbLBs.Text.Trim() == "西药")
                        {
                            //if (dtGG.Rows.Count > 1)
                            //{
                            //    dtYY = HisHelp.GetAdims_SelectrowsMZYYDW(LB, cmbXMMC.Text, cmbGG.Text, cmbDW.Text);
                            //}
                            //else
                            //{
                            dtYY = HisHelp.GetAdims_SelectrowsMZYY(LB, cmbXMMC.Text, cmbGG.Text);
                            //}

                        }
                        else
                        {
                            //if (dtGG.Rows.Count > 1)
                            //{
                            //    dtYY = HisHelp.GetAdims_SelectrowsMZYYDWALL(LB, cmbXMMC.Text, cmbGG.Text, cmbDW.Text);
                            //}
                            //else
                            //{
                            dtYY = HisHelp.GetAdims_SelectrowsMZYYALL(LB, cmbXMMC.Text, cmbGG.Text);
                            //}

                        }
                        //dgvMZYYJF.CurrentRow.Cells["Signer"].Value = dtYY.Rows[0]["unit"].ToString();
                        //dgvMZYYJF.CurrentRow.Cells["DJ"].Value = dtYY.Rows[0]["jiage"].ToString();
                        string item_code = dtYY.Rows[0]["item_code"].ToString();//项目代码
                        string Ybbz = dtYY.Rows[0]["ybbz"].ToString();//医保标志
                        string FitemID = dtYY.Rows[0]["FitemID"].ToString();//项目标准号
                        string FBatchID = dtYY.Rows[0]["FBatchID"].ToString();
                        pacu.UpdateMZZJF(id, dgvMZYYJF.CurrentRow.Cells["item_name"].Value.ToString(), cmbGG.Text, dtYY.Rows[0]["unit"].ToString(), dtYY.Rows[0]["jiage"].ToString(), item_code, Ybbz, FitemID, FBatchID);
                        MZJFDGV();
                    }
                    else if (Indes == 2)
                    {

                    }
                    else if (Indes == 5)
                    {
                        string id = dgvMZYYJF.CurrentRow.Cells["id"].Value.ToString();
                        double ShuL = Convert.ToDouble(dgvMZYYJF.CurrentRow.Cells["Amount"].Value.ToString());
                        double Danjia = Convert.ToDouble(dgvMZYYJF.CurrentRow.Cells["DJ"].Value.ToString());
                        //应收费用
                        double Sum = ShuL * Danjia;
                        //dgvMZYYJF.CurrentRow.Cells["Expense"].Value = Sum;
                        //dgvMZYYJF.CurrentRow.Cells["Charges"].Value = Sum;
                        pacu.UpdateMZZJFS(id, ShuL.ToString(), Sum.ToString(), Sum.ToString());
                        MZJFDGV();
                    }
                    else
                    {
                        string patID = dgvMZYYJF.CurrentRow.Cells["id"].Value.ToString();
                        int ColIndex = dgvMZYYJF.CurrentCell.ColumnIndex;
                        string RoomName = dgvMZYYJF.Columns[dgvMZYYJF.CurrentCell.ColumnIndex].Name;
                        string Value_Info = dgvMZYYJF.CurrentCell.Value.ToString();
                        pacu.GetUpdateMZZJF(RoomName, Value_Info, patID);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("操作失败！");
                MZJFDGV();
            }
        }
        /// <summary>
        /// 单元格双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMZYYJF_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMZYYJF.Rows.Count > 0)
            {
                int ColIndex = dgvMZYYJF.CurrentCell.ColumnIndex;

                if (ColIndex == 1)
                {
                    cmbLBs.Visible = false;
                    cmbKS.Visible = false;
                    cmbXMMC.Visible = false;
                    cmbGG.Visible = false;
                    cmbDW.Visible = false;
                    Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                    cmbLBs.Left = rect.Left;
                    cmbLBs.Top = rect.Top;
                    cmbLBs.Width = rect.Width;
                    cmbLBs.Height = rect.Height;
                    cmbLBs.Visible = true;
                    cmbLBs.Text = "";
                    cmbLBs.DroppedDown = true;
                }
                else if (ColIndex == 2)
                {
                    XMMCBind(dgvMZYYJF.CurrentRow.Cells["expense_item_class"].Value.ToString());
                    cmbLBs.Visible = false;
                    cmbKS.Visible = false;
                    cmbXMMC.Visible = false;
                    cmbGG.Visible = false;
                    cmbDW.Visible = false;
                    Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                    cmbXMMC.Left = rect.Left;
                    cmbXMMC.Top = rect.Top;
                    cmbXMMC.Width = rect.Width;
                    cmbXMMC.Height = rect.Height;
                    cmbXMMC.Visible = true;
                    cmbXMMC.Text = "";
                    cmbXMMC.DroppedDown = true;
                    isXM = true;
                    rows = dgvMZYYJF.CurrentCell.RowIndex;
                }
                else if (ColIndex == 3)
                {
                    if (isGG && rows == dgvMZYYJF.CurrentCell.RowIndex)
                    {
                        cmbLBs.Visible = false;
                        cmbKS.Visible = false;
                        cmbXMMC.Visible = false;
                        cmbGG.Visible = false;
                        cmbDW.Visible = false;
                        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                        cmbGG.Left = rect.Left;
                        cmbGG.Top = rect.Top;
                        cmbGG.Width = rect.Width;
                        cmbGG.Height = rect.Height;
                        cmbGG.Visible = true;
                        cmbGG.Text = "";
                        cmbGG.DroppedDown = true;
                        isGG = false;
                    }
                }
                //else if (ColIndex == 4)
                //{
                //        cmbLBs.Visible = false;
                //        cmbKS.Visible = false;
                //        cmbXMMC.Visible = false;
                //        cmbGG.Visible = false;
                //        cmbDW.Visible = false;
                //        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                //        cmbDW.Left = rect.Left;
                //        cmbDW.Top = rect.Top;
                //        cmbDW.Width = rect.Width;
                //        cmbDW.Height = rect.Height;
                //        cmbDW.Visible = true;
                //        cmbDW.Text = "";
                //        cmbDW.DroppedDown = true;                       
                //}
                else if (ColIndex == 10)
                {
                    cmbLBs.Visible = false;
                    cmbKS.Visible = false;
                    cmbXMMC.Visible = false;
                    cmbGG.Visible = false;
                    cmbDW.Visible = false;
                    Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                    cmbKS.Left = rect.Left;
                    cmbKS.Top = rect.Top;
                    cmbKS.Width = rect.Width;
                    cmbKS.Height = rect.Height;
                    cmbKS.Visible = true;
                    cmbKS.Text = "";
                    cmbKS.DroppedDown = true;
                }
                else if (ColIndex == 11)
                {
                    cmbLBs.Visible = false;
                    cmbKS.Visible = false;
                    cmbXMMC.Visible = false;
                    cmbGG.Visible = false;
                    cmbDW.Visible = false;
                    Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                    cmbKS.Left = rect.Left;
                    cmbKS.Top = rect.Top;
                    cmbKS.Width = rect.Width;
                    cmbKS.Height = rect.Height;
                    cmbKS.Visible = true;
                    cmbKS.Text = "";
                    cmbKS.DroppedDown = true;
                }
                else if (ColIndex == 12)
                {
                    cmbLBs.Visible = false;
                    cmbKS.Visible = false;
                    cmbXMMC.Visible = false;
                    cmbGG.Visible = false;
                    cmbDW.Visible = false;
                    Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                    cmbLBs.Left = rect.Left;
                    cmbLBs.Top = rect.Top;
                    cmbLBs.Width = rect.Width;
                    cmbLBs.Height = rect.Height;
                    cmbLBs.Visible = true;
                    cmbLBs.Text = "";
                    cmbLBs.DroppedDown = true;
                }
                else
                {
                    cmbLBs.Visible = false;
                    cmbKS.Visible = false;
                    cmbXMMC.Visible = false;
                    cmbGG.Visible = false;
                    cmbDW.Visible = false;
                }


            }
        }
        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvMZYYZT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvMZYYJF.Rows.Count > 0)
            //{
            //    int ColIndex = dgvMZYYJF.CurrentCell.ColumnIndex;

            //    if (ColIndex == 1)
            //    {
            //        cmbLBs.Visible = false;
            //        cmbKS.Visible = false;
            //        cmbXMMC.Visible = false;
            //        cmbGG.Visible = false;
            //        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
            //        cmbLBs.Left = rect.Left;
            //        cmbLBs.Top = rect.Top;
            //        cmbLBs.Width = rect.Width;
            //        cmbLBs.Height = rect.Height;
            //        cmbLBs.Visible = true;
            //        cmbLBs.Text = "";
            //        cmbLBs.DroppedDown = true;
            //    }
            //    else if (ColIndex == 2)
            //    {
            //        XMMCBind(dgvMZYYJF.CurrentRow.Cells["expense_item_class"].Value.ToString());
            //        cmbLBs.Visible = false;
            //        cmbKS.Visible = false;
            //        cmbXMMC.Visible = false;
            //        cmbGG.Visible = false;
            //        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
            //        cmbXMMC.Left = rect.Left;
            //        cmbXMMC.Top = rect.Top;
            //        cmbXMMC.Width = rect.Width;
            //        cmbXMMC.Height = rect.Height;
            //        cmbXMMC.Visible = true;
            //        cmbXMMC.Text = "";
            //        cmbXMMC.DroppedDown = true;
            //        isXM = true;
            //        rows = dgvMZYYJF.CurrentCell.RowIndex;
            //    }
            //    else if (ColIndex == 3)
            //    {
            //        if (isGG && rows == dgvMZYYJF.CurrentCell.RowIndex)
            //        {
            //            cmbLBs.Visible = false;
            //            cmbKS.Visible = false;
            //            cmbXMMC.Visible = false;
            //            cmbGG.Visible = false;
            //            Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
            //            cmbGG.Left = rect.Left;
            //            cmbGG.Top = rect.Top;
            //            cmbGG.Width = rect.Width;
            //            cmbGG.Height = rect.Height;
            //            cmbGG.Visible = true;
            //            cmbGG.Text = "";
            //            cmbGG.DroppedDown = true;
            //            isGG = false;
            //        }
            //    }
            //    else if (ColIndex == 10)
            //    {
            //        cmbLBs.Visible = false;
            //        cmbKS.Visible = false;
            //        cmbXMMC.Visible = false;
            //        cmbGG.Visible = false;
            //        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
            //        cmbKS.Left = rect.Left;
            //        cmbKS.Top = rect.Top;
            //        cmbKS.Width = rect.Width;
            //        cmbKS.Height = rect.Height;
            //        cmbKS.Visible = true;
            //        cmbKS.Text = "";
            //        cmbKS.DroppedDown = true;
            //    }
            //    else if (ColIndex == 11)
            //    {
            //        cmbLBs.Visible = false;
            //        cmbKS.Visible = false;
            //        cmbXMMC.Visible = false;
            //        cmbGG.Visible = false;
            //        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
            //        cmbKS.Left = rect.Left;
            //        cmbKS.Top = rect.Top;
            //        cmbKS.Width = rect.Width;
            //        cmbKS.Height = rect.Height;
            //        cmbKS.Visible = true;
            //        cmbKS.Text = "";
            //        cmbKS.DroppedDown = true;
            //    }
            //    else if (ColIndex == 12)
            //    {
            //        cmbLBs.Visible = false;
            //        cmbKS.Visible = false;
            //        cmbXMMC.Visible = false;
            //        cmbGG.Visible = false;
            //        Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
            //        cmbLBs.Left = rect.Left;
            //        cmbLBs.Top = rect.Top;
            //        cmbLBs.Width = rect.Width;
            //        cmbLBs.Height = rect.Height;
            //        cmbLBs.Visible = true;
            //        cmbLBs.Text = "";
            //        cmbLBs.DroppedDown = true;
            //    }
            //    else
            //    {
            //        cmbLBs.Visible = false;
            //        cmbKS.Visible = false;
            //        cmbXMMC.Visible = false;
            //        cmbGG.Visible = false;
            //    }              

            //}

        }

        /// <summary>
        /// 组套的使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBagUse_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = pacu.GetAdims_SelectYYJF(cmbZT.Text);
                Dictionary<string, string> MZYY = new Dictionary<string, string>();
                foreach (DataRow dr in dt.Rows)
                {
                    MZYY.Clear();
                    MZYY.Add("illman_id", tbZhuyuanNo.Text);
                    MZYY.Add("this_id", "1");
                    DataTable dt1 = pacu.GetAdims_SelectJF(tbZhuyuanNo.Text);
                    int expense_item_no = Convert.ToInt32(dt1.Rows[0][0].ToString());
                    expense_item_no++;
                    MZYY.Add("expense_item_no", expense_item_no.ToString());
                    MZYY.Add("expense_item_class", dr["LB"].ToString());
                    MZYY.Add("item_name", dr["XMMC"].ToString());
                    MZYY.Add("item_code", dr["item_code"].ToString());
                    MZYY.Add("item_spec", dr["GG"].ToString());
                    MZYY.Add("Amount", dr["SL"].ToString());
                    MZYY.Add("Signer", dr["DW"].ToString());
                    //DataTable dt2 = pacu.GetAdims_SelectKS("麻醉科");
                    MZYY.Add("open_office", dr["KDKS"].ToString());
                    MZYY.Add("exec_office", dr["ZXKS"].ToString());
                    MZYY.Add("expense", dr["YSFY"].ToString());
                    MZYY.Add("charges", dr["SSFY"].ToString());
                    MZYY.Add("billing_datetime", DateTime.Now.ToString());
                    MZYY.Add("operator_no", lbnames.Text);
                    MZYY.Add("dj", dr["DJ"].ToString());
                    MZYY.Add("sl", "1");
                    MZYY.Add("zycost", dr["ZYFYLB"].ToString());
                    MZYY.Add("ybbz", dr["ybbz"].ToString());
                    MZYY.Add("FItemID", dr["FItemID"].ToString());
                    MZYY.Add("FBatchID", dr["FBatchID"].ToString());
                    MZYY.Add("doctor", txtYS.Text);
                    MZYY.Add("FIsIns", "0");
                    MZYY.Add("mzid",MZID);
                    int flag = pacu.InsertMZYYJF(MZYY);
                    //if (flag == 0)
                    //{
                    //    MessageBox.Show("保存失败");
                    //}
                    //else
                    //{
                    //    MessageBox.Show("保存成功");

                    //}


                }
                MZJFDGV();
            }
            catch (Exception)
            {

                MessageBox.Show("保存失败！");
            }
        }

        //private void dgvMZYYJF_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (this.dgvMZYYJF.CurrentCell.ColumnIndex == 10||this.dgvMZYYJF.CurrentCell.ColumnIndex==11)
        //    {
        //    this.cmbKS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbKS_KeyDown);
        //    this.cmbKS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbKS_KeyPress);
        //    }
        //    else
        //    {
        //        this.cmbKS.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.cmbKS_KeyDown);
        //        this.cmbKS.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.cmbKS_KeyPress);
        //    }
        //}
        /// <summary>
        /// 首次按事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = pacu.GetSelectSXKS(cmbKS.Text);
                cmbKS.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.cmbKS.Items.Add(dt.Rows[i][0]);
                }
                cmbLBs.Visible = false;
                cmbKS.Visible = false;
                Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                cmbKS.Left = rect.Left;
                cmbKS.Top = rect.Top;
                cmbKS.Width = rect.Width;
                cmbKS.Height = rect.Height;
                cmbKS.Visible = true;
                cmbKS.Text = "";
                cmbKS.DroppedDown = true;
                //cmbKS.DataSource = dt;
                //cmbKS.DisplayMember = "KSname"; 


            }
        }
        /// <summary>
        /// 释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbKS_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }
        /// <summary>
        /// 首次按事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbXMMC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt;
                if (cmbLBs.Text.Trim() == "西药")
                    dt = HisHelp.JianSuoType(dgvMZYYJF.CurrentRow.Cells["expense_item_class"].Value.ToString(), cmbXMMC.Text);
                else
                    dt = HisHelp.JianSuoTypeALL(dgvMZYYJF.CurrentRow.Cells["expense_item_class"].Value.ToString(), cmbXMMC.Text);
                cmbXMMC.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.cmbXMMC.Items.Add(dt.Rows[i][0]);
                }
                cmbLBs.Visible = false;
                cmbKS.Visible = false;
                cmbXMMC.Visible = false;
                Rectangle rect = dgvMZYYJF.GetCellDisplayRectangle(dgvMZYYJF.CurrentCell.ColumnIndex, dgvMZYYJF.CurrentCell.RowIndex, false);
                cmbXMMC.Left = rect.Left;
                cmbXMMC.Top = rect.Top;
                cmbXMMC.Width = rect.Width;
                cmbXMMC.Height = rect.Height;
                cmbXMMC.Visible = true;
                cmbXMMC.Text = "";
                cmbXMMC.DroppedDown = true;
                //cmbKS.DataSource = dt;
                //cmbKS.DisplayMember = "KSname";
                isXM = true;
                rows = dgvMZYYJF.CurrentCell.RowIndex;
            }
        }
        /// <summary>
        /// 释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbXMMC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiJFAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, string> JFAdd = new Dictionary<string, string>();
                JFAdd.Add("illman_id", tbZhuyuanNo.Text);
                JFAdd.Add("this_id", "1");
                DataTable dt1 = pacu.GetAdims_SelectJF(tbZhuyuanNo.Text);
                int expense_item_no = Convert.ToInt32(dt1.Rows[0][0].ToString());
                expense_item_no++;//流水号
                JFAdd.Add("expense_item_no", expense_item_no.ToString());
                JFAdd.Add("billing_datetime", DateTime.Now.ToString());
                JFAdd.Add("operator_no", lbnames.Text);
                JFAdd.Add("Sl", "1");
                JFAdd.Add("Doctor", txtYS.Text);
                JFAdd.Add("FisIns", "0");
                JFAdd.Add("mzid",MZID);
                int flag = pacu.AddMZYYJF(JFAdd);
                if (flag > 0)
                {
                    MZJFDGV();
                }
                else
                {
                    MessageBox.Show("添加失败！");
                    MZJFDGV();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("添加失败！");
                MZJFDGV();
            }
        }
        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiJFDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMZYYJF.Rows.Count > 0)
                {
                    if (MessageBox.Show(" 您确定要删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        string id = dgvMZYYJF.CurrentRow.Cells["id"].Value.ToString();
                        int result = pacu.DeleteMZZJF(id);
                        if (result > 0)
                        {
                            MZJFDGV();
                        }
                        else
                        {
                            MessageBox.Show("删除失败！");
                            MZJFDGV();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("删除失败！");
                MZJFDGV();
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }
        /// <summary>
        /// 打印计费单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintResult_Click(object sender, EventArgs e)
        {
            if (dgvMZYYJF.Rows.Count > 0)
            {
                //PrintDataGridView.Print(dgvMZYYJF, "昌吉回族自治州人民医院手术通知单", "手术日期"+DateTime.Now.ToString(), "1");
                string name = tbPatname.Text;
                string age = tbAge.Text;
                string sex = tbSex.Text;
                string ZYH = tbZhuyuanNo.Text;
                string ks = tbBingqu.Text;
                string title2 = "姓名： " + name + "     年龄： " + age + "    性别： " + sex + "  科室：  " + ks + "    住院号： " + ZYH + "  费用合计: " + txtSumSSFY.Text;
                PrintJF.Print(dgvMZYYJF, "昌吉回族自治州人民医院麻醉用药计费单", title2, 1);

            }
        }
        /// <summary>
        /// 合计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSum_Click(object sender, EventArgs e)
        {
            int Hj = 0;
            decimal YSFY = 0;
            decimal SSFY = 0;
            //合计
            decimal SunYSFY = 0;
            decimal SunSSFY = 0;
            if (dgvMZYYJF.Rows.Count > 0)
            {
                for (; Hj < dgvMZYYJF.Rows.Count; Hj++)
                {
                    if (dgvMZYYJF.Rows[Hj].Cells["Expense"].Value.ToString() != "")
                    {
                        YSFY = Convert.ToDecimal(dgvMZYYJF.Rows[Hj].Cells["Expense"].Value.ToString());
                        SunYSFY = SunYSFY + YSFY;
                    }
                    if (dgvMZYYJF.Rows[Hj].Cells["Charges"].Value.ToString() != "")
                    {
                        SSFY = Convert.ToDecimal(dgvMZYYJF.Rows[Hj].Cells["Charges"].Value.ToString());
                        SunSSFY = SunSSFY + SSFY;
                    }
                }
                this.txtSumYSFY.Text = SunYSFY.ToString();
                this.txtSumSSFY.Text = SunSSFY.ToString();
            }
        }
        /// <summary>
        /// 保存到麻醉药房
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            if (dgvMZYYJF.Rows.Count > 0)
            {
                if (DialogResult.OK == MessageBox.Show("请确认清楚后在保存？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    for (int i = 0; i < dgvMZYYJF.Rows.Count; i++)
                    {
                        string id = dgvMZYYJF.Rows[i].Cells[0].Value.ToString();
                        DataTable dt = pacu.GetAdims_SelectJFID(id);
                        Dictionary<string, string> MZYYJF = new Dictionary<string, string>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            MZYYJF.Clear();
                            MZYYJF.Add("illman_id", dr["illman_id"].ToString());
                            MZYYJF.Add("this_id", dr["this_id"].ToString());
                            MZYYJF.Add("expense_item_no", dr["expense_item_no"].ToString());
                            MZYYJF.Add("expense_item_class", dr["expense_item_class"].ToString());
                            MZYYJF.Add("item_name", dr["item_name"].ToString());
                            MZYYJF.Add("item_code", dr["item_code"].ToString());
                            MZYYJF.Add("item_spec", dr["item_spec"].ToString());
                            MZYYJF.Add("Amount", dr["Amount"].ToString());
                            MZYYJF.Add("Signer", dr["Signer"].ToString());
                            MZYYJF.Add("open_office", dr["open_office"].ToString());
                            MZYYJF.Add("exec_office", dr["exec_office"].ToString());
                            MZYYJF.Add("Expense", dr["Expense"].ToString());
                            MZYYJF.Add("Charges", dr["Charges"].ToString());
                            MZYYJF.Add("billing_datetime", dr["billing_datetime"].ToString());
                            MZYYJF.Add("operator_no", dr["operator_no"].ToString());
                            MZYYJF.Add("rcpt_no", dr["rcpt_no"].ToString());
                            MZYYJF.Add("Dj", dr["Dj"].ToString());
                            MZYYJF.Add("Sl", dr["Sl"].ToString());
                            MZYYJF.Add("Zycost", dr["Zycost"].ToString());
                            MZYYJF.Add("Yzbz", dr["Yzbz"].ToString());
                            MZYYJF.Add("Zzy", dr["Zzy"].ToString());
                            MZYYJF.Add("Zzsj", dr["Zzsj"].ToString());
                            MZYYJF.Add("Ybbz", dr["Ybbz"].ToString());
                            MZYYJF.Add("total_flag", dr["total_flag"].ToString());
                            MZYYJF.Add("yizhu_no", dr["yizhu_no"].ToString());
                            MZYYJF.Add("txm_bs", dr["txm_bs"].ToString());
                            MZYYJF.Add("FitemID", dr["FitemID"].ToString());
                            MZYYJF.Add("FbatchID", dr["FbatchID"].ToString());
                            MZYYJF.Add("FinPrice", dr["FinPrice"].ToString());
                            MZYYJF.Add("Doctor", dr["Doctor"].ToString());
                            MZYYJF.Add("FisIns", dr["FisIns"].ToString());
                            MZYYJF.Add("Fopssource", dr["Fopssource"].ToString());
                            MZYYJF.Add("Fopscheduleid", dr["Fopscheduleid"].ToString());
                            int flag = pacu.InsertMZYYJFID(MZYYJF);
                        }
                    }
                }
                button1.Enabled = false;
            }

        }

    }
}
