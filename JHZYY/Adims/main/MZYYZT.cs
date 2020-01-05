using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using main.科室事物管理;
using adims_DAL;
using adims_BLL;

namespace main
{
    public partial class MZYYZT : Form
    {
        adims_DAL.HisDB_Help HisHelp = new adims_DAL.HisDB_Help();
        PACU_DAL dal = new PACU_DAL();
        DataTable dtYY;
        DataTable dtGG;//单位
        public MZYYZT()
        {
            InitializeComponent();
        }
      
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MZYYZT_Load(object sender, EventArgs e)
        {         
            BindMZYYZTType();//麻醉用药计费组套
            BindMZYYType();//麻醉用药类别
            BindDGV();
            cmbDW.Enabled = false;
            cmbGG.Enabled = false;
            this.cmbZT.SelectedIndexChanged += new System.EventHandler(this.cmbZT_SelectedIndexChanged);
            this.listXMMC.SelectedIndexChanged += new System.EventHandler(this.listXMMC_SelectedIndexChanged);
            this.cmbYpType.SelectedIndexChanged += new System.EventHandler(this.cmbYpType_SelectedIndexChanged);
            
        }
        /// <summary>
        /// 绑定麻醉组套
        /// </summary>
        private void BindMZYYZTType()
        {
            DataTable dt = dal.GetSelectMZZTtype();
            cmbZT.DataSource = dt;
            cmbZT.ValueMember = "id";
            cmbZT.DisplayMember = "name";
        }
        /// <summary>
        /// 绑定麻醉用药类别
        /// </summary>
        private void BindMZYYType()
        {
            DataTable dt = dal.GetSelectMZZLB();
            cmbYpType.DataSource = dt;
            cmbYpType.DisplayMember = "LBname";
            //DataTable dt = HisHelp.GetYP();
            //cmbYpType.DataSource = dt;
            //cmbYpType.DisplayMember = "type_on_an";

        }
        /// <summary>
        /// 绑定dgv
        /// </summary>
        private void BindDGV()
        {
            DataTable dt = dal.GetAdims_SelectDGV(cmbZT.Text);
            dgvMZYYZT.DataSource = dt;
        }

        private void 组套名称管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateSetup f1 = new DateSetup();
            f1.Show();
            
        }
        /// <summary>
        /// 首次按事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt;
                if (cmbYpType.Text.Trim() == "西药")
                    dt = HisHelp.JianSuoType(cmbYpType.Text.Trim(), tbPinyin.Text.Trim());
                else
                    dt = HisHelp.JianSuoTypeALL(cmbYpType.Text.Trim(), tbPinyin.Text.Trim());
                listXMMC.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    listXMMC.Items.Add(dr[0].ToString());
                }
                txtitem_name.Text = "";

            }
        }
        /// <summary>
        /// 释放事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

        private void listXMMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbGG.Enabled = true;
            cmbDW.Text = "";
            if (listXMMC.SelectedIndex != -1)
                txtitem_name.Text = listXMMC.SelectedItem.ToString();
            if (cmbYpType.Text.Trim() == "西药")
                dtYY = HisHelp.GetAdims_SelectMZYY(cmbYpType.Text, txtitem_name.Text);
            else
                dtYY = HisHelp.GetAdims_SelectMZYYALL(cmbYpType.Text, txtitem_name.Text);
            cmbGG.Text = "";
            if (dtYY.Rows.Count > 1)
            {
                DataTable dt1;
                if (cmbYpType.Text.Trim() == "西药")
                    dt1 = HisHelp.GetAdims_SelectGG(cmbYpType.Text, txtitem_name.Text);
                else
                    dt1 = HisHelp.GetAdims_SelectGGALL(cmbYpType.Text, txtitem_name.Text);
                cmbGG.DataSource = dt1;
                cmbGG.DisplayMember = "item_spec";
                this.cmbGG.SelectedIndexChanged += new System.EventHandler(this.cmbGG_SelectedIndexChanged);
                cmbDW.Enabled = true;
            }
            else
            {
                cmbGG.Enabled = false;
                cmbDW.Enabled = false;
                this.cmbGG.SelectedIndexChanged -= new System.EventHandler(this.cmbGG_SelectedIndexChanged);
            }
        }
        /// <summary>
        /// 增加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {            
            try
            {
                if (listXMMC.Text != "")
                {
                    DataTable dt;
                    if (cmbYpType.Text.Trim() == "西药")
                    {
                        if (dtYY.Rows.Count > 1)
                        {
                            if (dtGG.Rows.Count > 1)
                                dt = HisHelp.GetAdims_SelectrowsMZYYDW(cmbYpType.Text, txtitem_name.Text, cmbGG.Text, cmbDW.Text);
                            else
                                dt = HisHelp.GetAdims_SelectrowsMZYY(cmbYpType.Text, txtitem_name.Text, cmbGG.Text);
                        }
                        else
                        {
                            dt = HisHelp.GetAdims_SelectMZYY(cmbYpType.Text, txtitem_name.Text);
                        }
                    }
                    else
                    {
                        if (dtYY.Rows.Count > 1)
                        {
                            if (dtGG.Rows.Count > 1)
                                dt = HisHelp.GetAdims_SelectrowsMZYYDWALL(cmbYpType.Text, txtitem_name.Text, cmbGG.Text, cmbDW.Text);
                            else
                                dt = HisHelp.GetAdims_SelectrowsMZYYALL(cmbYpType.Text, txtitem_name.Text, cmbGG.Text);
                        }
                        else
                        {
                            dt = HisHelp.GetAdims_SelectMZYYALL(cmbYpType.Text, txtitem_name.Text);
                        }
                    }
                  
                    string item_code = dt.Rows[0]["item_code"].ToString();//项目代码
                    string item_spec = dt.Rows[0]["item_spec"].ToString();//规格
                    string unit = dt.Rows[0]["unit"].ToString();//单位
                    string type_inpatient_receip = dt.Rows[0]["type_inpatient_receipt"].ToString();//核算类别
                    string ybbz = dt.Rows[0]["ybbz"].ToString();//医保标志
                    double jiage = Convert.ToDouble(dt.Rows[0]["jiage"]);//单价
                    string FSpell = dt.Rows[0]["FSpell"].ToString();//拼音码
                    string type = dt.Rows[0]["item_type"].ToString();//类别（A）
                    string FItemID = dt.Rows[0]["FItemID"].ToString();//项目标准号
                    string FBatchID = dt.Rows[0]["FBatchID"].ToString();//
                    //计算费用     
                    int SL = Convert.ToInt32(numSL.Text);
                    double YSFY = jiage * SL;
                    Dictionary<string, string> MZYY = new Dictionary<string, string>();
                    MZYY.Add("ZT_id", cmbZT.SelectedValue.ToString());
                    MZYY.Add("LB", cmbYpType.Text);
                    MZYY.Add("GG", txtitem_name.Text);
                    MZYY.Add("XMMC", item_spec);
                    MZYY.Add("DW", unit);
                    MZYY.Add("SL", numSL.Text);
                    MZYY.Add("DJ", jiage.ToString());
                    MZYY.Add("F", "1");
                    MZYY.Add("YSFY", YSFY.ToString());
                    MZYY.Add("SSFY", YSFY.ToString());
                    MZYY.Add("KDKS", "麻醉科");
                    MZYY.Add("ZXKS", "麻醉科");
                    MZYY.Add("ZYFYLB", cmbYpType.Text);
                    MZYY.Add("item_code", item_code);
                    MZYY.Add("ybbz", ybbz);
                    MZYY.Add("FSpell", FSpell);
                    MZYY.Add("expense_item_class", type);
                    MZYY.Add("FItemID", FItemID);
                    MZYY.Add("FBatchID", FBatchID);
                    int flag = dal.InsertMZYYZT(MZYY);
                    if (flag == 0)
                    {
                        MessageBox.Show("保存失败");
                    }
                    else
                    {
                        MessageBox.Show("保存成功");
                        BindDGV();
                    }

                }
                else
                {
                    MessageBox.Show("项目名称不能为空！");
                }
               
            }
            catch (Exception)
            {

                MessageBox.Show("增加失败！");
            }
        }

        private void cmbZT_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDGV();
        }
        /// <summary>
        /// 刷新麻醉药品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string IPaddress = "132.147.160.5";
            bool flag = DataValid.PingHost(IPaddress, 1000);
            if (flag == true)
            {
               // HismzypBind();              
                HisMzLBBind();
                HisMzKSBind();
                BindMZYYType();
                BindMZYYZTType();
            }
            else MessageBox.Show("HIS数据库未连接，请检查网络");
        }
        #region 麻醉药品名称
        ///// <summary>
        ///// 麻醉药品名称
        ///// </summary>
        //private void HismzypBind() {
        //    //try
        //    //{
        //        DataTable dt1 = HisHelp.GetXM();
        //        List<string> HISinfo = new List<string>();
        //        int result = 0;
        //        foreach (DataRow dr in dt1.Rows)
        //        {
        //            HISinfo.Clear();
        //            HISinfo.Add(dr["FItemID"].ToString());
        //            HISinfo.Add(dr["item_code"].ToString());
        //            HISinfo.Add(dr["item_name"].ToString());
        //            if (dr["item_spec"].ToString().Contains("'"))
        //            {
        //                string spec = dr["item_spec"].ToString().Replace("'", ".");
        //                HISinfo.Add(spec);                   
        //            }
        //            else
        //            {
        //                HISinfo.Add(dr["item_spec"].ToString());
        //            }

        //            HISinfo.Add(dr["unit"].ToString());
        //            HISinfo.Add(dr["item_type"].ToString());
        //            HISinfo.Add(dr["type_inpatient_receipt"].ToString());
        //            HISinfo.Add(dr["type_on_an"].ToString());
        //            HISinfo.Add(dr["ybbz"].ToString());
        //            HISinfo.Add(dr["jiage"].ToString());
        //            HISinfo.Add(dr["FSpell"].ToString());
        //            DataTable dt = dal.GetSelectKS(dr["FItemID"].ToString());
        //            if (dt.Rows.Count == 0)
        //                result = dal.InsertMZYYXM(HISinfo);
        //        }
        //    //}
        //    //catch (Exception)
        //    //{

        //    //    MessageBox.Show("刷新失败");
        //    //}

        //}
        #endregion
        private void HisMzLBBind()
        {
            DataTable dt1 = HisHelp.GetLB();
            List<string> HISinfo = new List<string>();
            int result = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                HISinfo.Clear();
                HISinfo.Add(dr["number"].ToString());
                HISinfo.Add(dr["an_cost_type_code"].ToString());
                HISinfo.Add(dr["an_cost_type_name"].ToString());
                HISinfo.Add(dr["spell_header"].ToString());
                DataTable dt = dal.GetSelectLB(dr["number"].ToString());
                if (dt.Rows.Count == 0)
                    result = dal.InsertLB(HISinfo);
            }            
        }
        /// <summary>
        /// 科室
        /// </summary>
        private void HisMzKSBind() 
        {
            DataTable dt1 = HisHelp.GetKS();
            List<string> HISinfo = new List<string>();
            int result = 0;
            foreach (DataRow dr in dt1.Rows)
            {
                HISinfo.Clear();
                HISinfo.Add(dr["FItemID"].ToString());
                HISinfo.Add(dr["office_code"].ToString());
                HISinfo.Add(dr["office_name"].ToString());
                HISinfo.Add(dr["spell_header"].ToString());
                DataTable dt = dal.GetSelectKS(dr["FItemID"].ToString());
                if (dt.Rows.Count == 0)
                    result = dal.InsertMZYYKS(HISinfo);
            }
            MessageBox.Show("刷新成功");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMZYYZT.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        int m = dal.DelectMZYYZT(Convert.ToInt32(dgvMZYYZT.SelectedRows[0].Cells[0].Value));
                        if (m > 0)
                        {
                            BindDGV();
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("请选中要删除的项！");               
            }          
        
        }
        /// <summary>
        /// 药品类别改变是发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void cmbYpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt;
            if (cmbYpType.Text.Trim() == "西药")
            {
                dt = HisHelp.GetAdims_SelectLB(cmbYpType.Text.Trim());
            }
            else
            {
                dt = HisHelp.GetAdims_SelectLBALL(cmbYpType.Text.Trim());
            }
            listXMMC.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                listXMMC.Items.Add(dr[0].ToString());
            }
            txtitem_name.Text = "";
        }

        private void cmbGG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGG.Text != "")
            {              
               
                if (cmbYpType.Text.Trim() == "西药")
                    dtGG = HisHelp.GetAdims_SelectDW(cmbYpType.Text, txtitem_name.Text, cmbGG.Text);
                else
                    dtGG = HisHelp.GetAdims_SelectDWALL(cmbYpType.Text, txtitem_name.Text, cmbGG.Text);
                cmbDW.Text = "";
                if (dtGG.Rows.Count > 1)
                {
                    cmbDW.DataSource = dtGG;
                    cmbDW.DisplayMember = "unit";
                }
                else
                {
                    cmbDW.Enabled = false;
                }
            }
        }
        /// <summary>
        /// 组套管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            DateSetup maintenance = new DateSetup();
            maintenance.Show();
        }

      

    }
}
