using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.CGG
{
    public partial class SHHLJLdan : Form
    {
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();
        adims_DAL.mz dal = new adims_DAL.mz();
        adims_BLL.mz bll = new adims_BLL.mz();
        string patID;
        int result = 0;
        public SHHLJLdan(string patID1)
        {
            patID = patID1; 
            InitializeComponent();
        }

        private void SHHLJLdan_Load(object sender, EventArgs e)
        {
            info();
            BindMZfangshi();
            Changyongbao();
            Qixiebao();
            infochangyongbao();
            infoqixiebao();
         
        }
        #region<<显示信息>>
        private void info()//基础信息
        {
            try
            {
                DataTable dt1 = dal.GetShuShlJLDAN(patID);
                if (dt1.Rows.Count > 0)
                {
                    DataRow dr1 = dt1.Rows[0];
                    tbPatname.Text = dr1["PatName"].ToString();
                    tbMingzu.Text = dr1["PatNation"].ToString();
                    tbSex.Text = dr1["Patsex"].ToString();
                    tbAge.Text = dr1["Patage"].ToString();
                    tbZhuyuanID.Text = dr1["PatID"].ToString();
                    tbKeshi.Text = dr1["Patdpm"].ToString();
                    tbBedNO.Text = dr1["Patbedno"].ToString();
                    tbSZZD.Text = dr1["Pattmd"].ToString();
                    tbShoushuName.Text = dr1["Oname"].ToString();
                    tbGuominshi.Text = dr1["asa"].ToString();
                    cmbMZFF.Text = dr1["Amethod"].ToString();
                    textBox2.Text = dr1["PatWeight"].ToString();
                    cmbOroom.Text = dr1["Oroom"].ToString();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            DataTable dt2 = dal.GEThlqk(patID);
            if (dt2.Rows.Count > 0)
            {
                DataRow dr2 = dt2.Rows[0];
                dtEnterTime.Text = dr2["rsrate"].ToString();
                cmbShenzhi.Text = dr2["shenzhi"].ToString();

            }
            infochangqixie();
            infochangfuliao();
            
            
        }
       //器械显示
        private void infochangqixie()
        {
            try
            {
                DataTable dt = cll.BCHqixiefuliao(patID);
                if (dt.Rows.Count == 0)
                {
                    DataTable dtr = cll.BCHqixiefuliao("1");
                    for (int i = 0; i < dtr.Rows.Count; i++)
                    {
                        if (i >= dgvNurseRecord.Rows.Count)
                        {
                            dgvNurseRecord.Rows.Add();
                        }
                        dgvNurseRecord.Rows[i].Cells[0].Value = dtr.Rows[i]["qxpm"];
                        dgvNurseRecord.Rows[i].Cells[1].Value = dtr.Rows[i]["sqqd"];
                        dgvNurseRecord.Rows[i].Cells[2].Value = dtr.Rows[i]["gq"];
                        dgvNurseRecord.Rows[i].Cells[3].Value = dtr.Rows[i]["gh"];
                        dgvNurseRecord.Rows[i].Cells[4].Value = dtr.Rows[i]["qxpm1"];
                        dgvNurseRecord.Rows[i].Cells[5].Value = dtr.Rows[i]["sqqd1"];
                        dgvNurseRecord.Rows[i].Cells[6].Value = dtr.Rows[i]["gq1"];
                        dgvNurseRecord.Rows[i].Cells[7].Value = dtr.Rows[i]["gh1"];
                        dgvNurseRecord.Rows[i].Cells[8].Value = dtr.Rows[i]["qxpm2"];
                        dgvNurseRecord.Rows[i].Cells[9].Value = dtr.Rows[i]["sqqd2"];
                        dgvNurseRecord.Rows[i].Cells[10].Value = dtr.Rows[i]["gq2"];
                        dgvNurseRecord.Rows[i].Cells[11].Value = dtr.Rows[i]["gh2"];
                    }
                    dgvNurseRecord.AllowUserToAddRows = false;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= dgvNurseRecord.Rows.Count)
                        {
                            dgvNurseRecord.Rows.Add();
                        }
                        dgvNurseRecord.Rows[i].Cells[0].Value = dt.Rows[i]["qxpm"];
                        dgvNurseRecord.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                        dgvNurseRecord.Rows[i].Cells[2].Value = dt.Rows[i]["gq"];
                        dgvNurseRecord.Rows[i].Cells[3].Value = dt.Rows[i]["gh"];
                        dgvNurseRecord.Rows[i].Cells[4].Value = dt.Rows[i]["qxpm1"];
                        dgvNurseRecord.Rows[i].Cells[5].Value = dt.Rows[i]["sqqd1"];
                        dgvNurseRecord.Rows[i].Cells[6].Value = dt.Rows[i]["gq1"];
                        dgvNurseRecord.Rows[i].Cells[7].Value = dt.Rows[i]["gh1"];
                        dgvNurseRecord.Rows[i].Cells[8].Value = dt.Rows[i]["qxpm2"];
                        dgvNurseRecord.Rows[i].Cells[9].Value = dt.Rows[i]["sqqd2"];
                        dgvNurseRecord.Rows[i].Cells[10].Value = dt.Rows[i]["gq2"];
                        dgvNurseRecord.Rows[i].Cells[11].Value = dt.Rows[i]["gh2"];
                    }
                    dgvNurseRecord.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "器械清点加载失败");
            }
        }//器械加载
        private void infochangfuliao()
        {
            try
            {
                DataTable dt = cll.qixiefuliao1(patID);
                if (dt.Rows.Count == 0)
                {
                    DataTable dtr = cll.qixiefuliao1("1");
                    for (int i = 0; i < dtr.Rows.Count; i++)
                    {
                        if (i >= dgvZxd.Rows.Count)
                        {
                            dgvZxd.Rows.Add();
                        }
                        dgvZxd.Rows[i].Cells[0].Value = dtr.Rows[i]["flpm"];
                        dgvZxd.Rows[i].Cells[1].Value = dtr.Rows[i]["sqqd"];
                        dgvZxd.Rows[i].Cells[2].Value = dtr.Rows[i]["gq"];
                        dgvZxd.Rows[i].Cells[3].Value = dtr.Rows[i]["gh"];
                        dgvZxd.Rows[i].Cells[4].Value = dtr.Rows[i]["flpm1"];
                        dgvZxd.Rows[i].Cells[5].Value = dtr.Rows[i]["sqqd1"];
                        dgvZxd.Rows[i].Cells[6].Value = dtr.Rows[i]["gq1"];
                        dgvZxd.Rows[i].Cells[7].Value = dtr.Rows[i]["gh1"];
                    }
                    dgvZxd.AllowUserToAddRows = false;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= dgvZxd.Rows.Count)
                        {
                            dgvZxd.Rows.Add();
                        }
                        dgvZxd.Rows[i].Cells[0].Value = dt.Rows[i]["flpm"];
                        dgvZxd.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                        dgvZxd.Rows[i].Cells[2].Value = dt.Rows[i]["gq"];
                        dgvZxd.Rows[i].Cells[3].Value = dt.Rows[i]["gh"];
                        dgvZxd.Rows[i].Cells[4].Value = dt.Rows[i]["flpm1"];
                        dgvZxd.Rows[i].Cells[5].Value = dt.Rows[i]["sqqd1"];
                        dgvZxd.Rows[i].Cells[6].Value = dt.Rows[i]["gq1"];
                        dgvZxd.Rows[i].Cells[7].Value = dt.Rows[i]["gh1"];
                    }
                    dgvZxd.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "止血带包清点加载失败");
            }
        }//敷料加载
        //麻醉方式
        private void BindMZfangshi()
        {
            DataTable dtMZfs = dal.GetMazuiFangfa();
            cmbMZFF.Items.Clear();
            foreach (DataRow dr in dtMZfs.Rows)//循环dt表里数组行。
            {
                cmbMZFF.Items.Add(dr["name"].ToString());
            }

        }
        #endregion
        #region<<保存信息>>
        private void baocun()
        {
            Dictionary<string, string> SQF = new Dictionary<string, string>();
            if (btnSave.Enabled)
            {
                SQF.Add ("IsRead","0");
            }
            else
            {
                SQF.Add("IsRead", "1");
            }
            SQF.Add("ZhuYuanID",patID);
            SQF.Add("rsrate", dtEnterTime.Text);
            SQF.Add("shenzhi", cmbShenzhi.Text);
            string AddItem = "";
            AddItem = "";
            if (checkBox1.Checked) AddItem += "有";
            if (checkBox2.Checked) AddItem += "无";
            SQF.Add("Jingmcichuan", AddItem);
            string AddItem1 = "";
            AddItem1 = "";
            if (checkBox4.Checked) AddItem1 += "有";
            if (checkBox3.Checked) AddItem1 += "无";
            SQF.Add("SJingmcichuan", AddItem1);

            string AddItem2= "";
            AddItem2 = "";
            if (checkBox6.Checked) AddItem2 += "有";
            if (checkBox5.Checked) AddItem2 += "无";
            SQF.Add("Daoniao", AddItem2);

            string AddItem3 = "";
            AddItem3 = "";
            if (checkBox7.Checked) AddItem3 += "完整";
            if (checkBox8.Checked) AddItem3 += "其他" + textBox1.Text.Trim();
            SQF.Add("Pifu", AddItem3);

            SQF.Add("SHUQIANqianming", textBox3.Text);

            //string AddItem4 = "";
            //AddItem4 = "";
            //if (checkBox7.Checked) AddItem4 += "完整";
            //if (checkBox8.Checked) AddItem4 += "其他" + textBox1.Text.Trim();
            //SQF.Add("SHUQIANqianming", AddItem4);

            string AddItem5 = "";
            AddItem5 = "";
            if (checkBox10.Checked) AddItem5 += "仰卧位";
            if (checkBox9.Checked) AddItem5 += "侧卧位";
            if (checkBox12.Checked) AddItem5 += "俯卧位";
            if (checkBox11.Checked) AddItem5 += "截石位";
            if (checkBox13.Checked) AddItem5 += "坐位";
            if (checkBox41.Checked) AddItem5 += "其他" + comboBox1.Text.Trim();
            SQF.Add("tiwei", AddItem5);

            string AddItem6 = "";
            AddItem6 = "";
            if (checkBox16.Checked) AddItem6 += "无标本";
            if (checkBox15.Checked) AddItem6 += "已送";
            if (checkBox14.Checked) AddItem6 += "未送";
            SQF.Add("biaobensbdong", AddItem6);

            string AddItem7 = "";
            AddItem7 = "";
            if (checkBox18.Checked) AddItem7+= "是";
            if (checkBox17.Checked) AddItem7 += "否";
            SQF.Add("diandao", AddItem7);

            string AddItem8 = "";
            AddItem8 = "";
            if (checkBox21.Checked) AddItem8 += "大腿";
            if (checkBox20.Checked) AddItem8 += "小腿";
            if (checkBox19.Checked) AddItem8 += "手臂";
            if (checkBox22.Checked) AddItem8 += "臀";
            SQF.Add("fujizhantie", AddItem8);

            SQF.Add("niaoliang", comboBox8.Text);
            SQF.Add("shuyeliang",tbShuye.Text);
            SQF.Add("shuxueliang", comboBox7.Text);
            SQF.Add("hongxibaoxuanye",textBox5.Text);
            SQF.Add("xuejiang", textBox6.Text);
            SQF.Add("xuexiaoban", textBox7.Text);



            string AddItem9 = "";
            AddItem9 = "";
            if (checkBox24.Checked) AddItem9 += "阳性";
            if (checkBox23.Checked) AddItem9 += "阴性";
            SQF.Add(comboBox2.Text + "xuexing", AddItem9);

            SQF.Add("SZqita", comboBox3.Text);
            SQF.Add("SZqianming", textBox8.Text);

            string AddItem10 = "";
            AddItem10 = "";
            if (checkBox26.Checked) AddItem10 += "完整";
            if (checkBox25.Checked) AddItem10 += "同术前";
            SQF.Add("SHpifuqingkuang", AddItem10);

            string AddItem11 = "";
            AddItem11 = "";
            if (checkBox28.Checked) AddItem11 += "清醒";
            if (checkBox27.Checked) AddItem11 += "未清醒";
            SQF.Add("SHshenzhi", AddItem11);

            string AddItem12 = "";
            AddItem12 = "";
            if (checkBox30.Checked) AddItem12 += "有";
            if (checkBox29.Checked) AddItem12 += "无";
            SQF.Add("SHyinliu", AddItem12);

            string AddItem13 = "";
            AddItem13 = "";
            if (checkBox32.Checked) AddItem13 += "无标本";
            if (checkBox31.Checked) AddItem13 += "已送";
            if (checkBox33.Checked) AddItem13 += "未送" + textBox9.Text;
            SQF.Add("SHbiaobensongbl", AddItem13);

            SQF.Add("SHBBqianming", textBox10.Text);
            SQF.Add("SHchurate", Convert.ToDateTime(dtLeaveTime.Value.ToString()).ToString("HH:mm:ss"));

            string AddItem14 = "";
            AddItem14 = "";
            if (checkBox36.Checked) AddItem14 += "病房";
            if (checkBox35.Checked) AddItem14 += "ICU";
            if (checkBox37.Checked) AddItem14 += "EICU";
            if (checkBox34.Checked) AddItem14 += "PACU";
            SQF.Add("SHshuhousonghui", AddItem14);
            SQF.Add("SHfbsrate", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("HH:mm:ss"));
            SQF.Add("SHsonghuiqianming", textBox11.Text);
            SQF.Add("SHshqita", textBox12.Text);
            SQF.Add("SHyongyao", textBox13.Text);
            SQF.Add("SHqianming", textBox14.Text);

            string AddItem15 = "";
            AddItem15 = "";
            if (checkBox38.Checked) AddItem15 += "合格";
            SQF.Add("SHwujunbaojiance", AddItem15);


            string AddItem16 = "";
            AddItem16 = "";
            if (checkBox39.Checked) AddItem16 += "术前完整";
            if (checkBox40.Checked) AddItem16 += "术后完整";
            SQF.Add("SHqixiehecha", AddItem16);
            SQF.Add("SHzuihouqita",textBox15.Text);

            SQF.Add("qixieysqianming", cmbQXHS.Text);
            SQF.Add("shoushuysqianming", comboBox4.Text);
            SQF.Add("xunhuiysqianming", comboBox5.Text);
            SQF.Add("jiebanhsqianming", comboBox6.Text);
            SQF.Add("bz", tbRemarkLast.Text);

            DataTable dt = dal.GEThlqk(patID);
            if (dt.Rows.Count > 0)
            {
                result = dal.Updatehlqk(SQF);
                MessageBox.Show("修改成功");
            }
            else
            {
                result = dal.Inserthlqk(SQF);
                MessageBox.Show("保存成功！");
            }




        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (cmbShenzhi.Text == "")
            //{
            //    MessageBox.Show("神志不能为空");
            //    cmbShenzhi.Focus();
            //    return;
            //}
            //if (textBox3.Text == "")
            //{
            //    MessageBox.Show("术前签名不能为空");
            //    textBox3.Focus();
            //    return;
            //}
            //if (tbShuye.Text == "")
            //{
            //    MessageBox.Show("输液量不能为空");
            //    tbShuye.Focus();
            //    return;
            //}
            //if (comboBox7.Text == "")
            //{
            //    MessageBox.Show("自血体不能为空");
            //    comboBox7.Focus();
            //    return;
            //}
            //if (textBox5.Text == "")
            //{
            //    MessageBox.Show("红细胞不能为空");
            //    textBox5.Focus();
            //    return;
            //}
            //if (textBox6.Text == "")
            //{
            //    MessageBox.Show("血浆不能为空");
            //    textBox6.Focus();
            //    return;
            //}
            //if (textBox7.Text == "")
            //{
            //    MessageBox.Show("血小板不能为空");
            //    textBox7.Focus();
            //    return;
            //}
            //if (comboBox2.Text == "")
            //{
            //    MessageBox.Show("术中血型不能为空");
            //    comboBox2.Focus();
            //    return;
            //}
            //if (textBox8.Text == "")
            //{
            //    MessageBox.Show("术中签名不能为空");
            //    textBox8.Focus();
            //    return;
            //}
            //if (textBox10.Text == "")
            //{
            //    MessageBox.Show("标本送病理签名不能为空");
            //    textBox10.Focus();
            //    return;
            //}
            //if (textBox11.Text == "")
            //{
            //    MessageBox.Show("术后签名不能为空");
            //    textBox11.Focus();
            //    return;
            //}
            //if (textBox13.Text == "")
            //{
            //    MessageBox.Show("手术用药不能为空");
            //    textBox13.Focus();
            //    return;
            //}
            //if (textBox14.Text == "")
            //{
            //    MessageBox.Show("签名不能为空");
            //    textBox14.Focus();
            //    return;
            //}
            //if (cmbQXHS.Text == "")
            //{
            //    MessageBox.Show("器械护士不能为空");
            //    cmbQXHS.Focus();
            //    return;
            //}
            //if (comboBox4.Text == "")
            //{
            //    MessageBox.Show("手术医师不能为空");
            //    comboBox4.Focus();
            //    return;
            //}
            //if (comboBox5.Text == "")
            //{
            //    MessageBox.Show("巡回护士不能为空");
            //    comboBox5.Focus();
            //    return;
            //}
            //if (comboBox6.Text == "")
            //{
            //    MessageBox.Show("接班护士不能为空");
            //    comboBox6.Focus();
            //    return;
            //}
            
            baocun();
            Getchangyongbaobaocun();
            GetQixiebaobaocun();
            Getfuliaobaocun();
            
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }      
        #region<<常用包>>
        private void btnzxdConfig_Click(object sender, EventArgs e)//常用包确认
        {
            try
            {
                DataTable dt = dal.SelectzxdModel(comZxdmb.Text);
               
                    if (dt.Rows.Count % 2 == 0)//双数
                    {
                        int j = dt.Rows.Count / 2;
                        dgvZxd.Rows.Clear();
                        dgvZxd.Rows.Add(j);

                        for (int i = 0; i < j; i++)
                        {
                            dgvZxd.Rows[i].Cells[0].Value = dt.Rows[i][0];
                            //  dgvZxd.Rows[i].Cells[1].Value = dt.Rows[i][1];

                        }

                        for (int i = 0; i < dt.Rows.Count - j; i++)
                        {
                            dgvZxd.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
                            //  dgvZxd.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
                        }
                    }
                    else
                    {
                        int j = dt.Rows.Count / 2;
                        dgvZxd.Rows.Clear();
                        dgvZxd.Rows.Add(j + 1);

                        for (int i = 0; i < j; i++)
                        {
                            dgvZxd.Rows[i].Cells[0].Value = dt.Rows[i][0];
                            //   dgvZxd.Rows[i].Cells[1].Value = dt.Rows[i][1];

                        }

                        for (int i = 0; i < dt.Rows.Count - j; i++)
                        {
                            dgvZxd.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
                            // dgvZxd.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
                        }

                    }



                }
            
            catch (Exception)
            {
                MessageBox.Show("注（选择不能为空）");
            }

        }  
        private void Getchangyongbaobaocun()//保存
        {
            DataTable dt = dal.Getchangyongbaobc(patID, comZxdmb.Text);

            if (dt.Rows.Count > 0)
            {
                int zxd = dal.Deletechangyongbaobc(patID, comZxdmb.Text);
            }
            try
            {
                for (int i = 0; i < dgvZxd.Rows.Count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (dgvZxd.Rows[i].Cells[j].Value == null)
                            dgvZxd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    int result = 0;
                    zxdList.Add("zhuyuanID", patID);
                    zxdList.Add("cyb", comZxdmb.Text);
                    zxdList.Add("zxdname", dgvZxd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", dgvZxd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gqhd", dgvZxd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("ghhd", dgvZxd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("zxdname1", dgvZxd.Rows[i].Cells[4].Value.ToString());
                    zxdList.Add("sqqd1", dgvZxd.Rows[i].Cells[5].Value.ToString());
                    zxdList.Add("gqhd1", dgvZxd.Rows[i].Cells[6].Value.ToString());
                    zxdList.Add("ghhd1", dgvZxd.Rows[i].Cells[7].Value.ToString());
                    result = dal.Insertzxdqingdian(zxdList);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "保存失败");
            }
        }
        private void infochangyongbao()
        {
            try
            {
                DataTable dt = dal.GetBcv(patID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i >= dgvZxd.Rows.Count)
                    {
                        dgvZxd.Rows.Add();
                    }
                    dgvZxd.Rows[i].Cells[0].Value = dt.Rows[i]["zxdname"];
                    dgvZxd.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                    dgvZxd.Rows[i].Cells[2].Value = dt.Rows[i]["gqhd"];
                    dgvZxd.Rows[i].Cells[3].Value = dt.Rows[i]["ghhd"];
                    dgvZxd.Rows[i].Cells[4].Value = dt.Rows[i]["zxdname1"];
                    dgvZxd.Rows[i].Cells[5].Value = dt.Rows[i]["sqqd1"];
                    dgvZxd.Rows[i].Cells[6].Value = dt.Rows[i]["gqhd1"];
                    dgvZxd.Rows[i].Cells[7].Value = dt.Rows[i]["ghhd1"];
                }
                dgvZxd.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "止血带包清点加载失败");
            }
        }
        private void Changyongbao()
        {
           
                DataTable dt = dal.SelectzxdModel(comZxdmb.Text);
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count % 2 == 0)//双数
                    {
                        int j = dt.Rows.Count / 2;
                        dgvZxd.Rows.Clear();
                        dgvZxd.Rows.Add(j);

                        for (int i = 0; i < j; i++)
                        {
                            dgvZxd.Rows[i].Cells[0].Value = dt.Rows[i][0];
                            dgvZxd.Rows[i].Cells[1].Value = dt.Rows[i][1];

                        }

                        for (int i = 0; i < dt.Rows.Count - j; i++)
                        {
                            dgvZxd.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
                            dgvZxd.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
                        }
                    }
                    else
                    {
                        int j = dt.Rows.Count / 2;
                        dgvZxd.Rows.Clear();
                        dgvZxd.Rows.Add(j + 1);

                        for (int i = 0; i < j; i++)
                        {
                            dgvZxd.Rows[i].Cells[0].Value = dt.Rows[i][0];
                            dgvZxd.Rows[i].Cells[1].Value = dt.Rows[i][1];

                        }

                        for (int i = 0; i < dt.Rows.Count - j; i++)
                        {
                            dgvZxd.Rows[i].Cells[4].Value = dt.Rows[i + j][0];
                            dgvZxd.Rows[i].Cells[5].Value = dt.Rows[i + j][1];
                        }

                    }

                }
        }
        private void btnZxdQc_Click(object sender, EventArgs e)//清空
         {
             if (MessageBox.Show("是否清空此人的常用包", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
             {
                 int a = dal.DeleteZxdb(patID);
                 dgvZxd.Rows.Clear();
             }
             else
                 return;
         }
        private void button2_Click(object sender, EventArgs e)
         {
             try
             {
                 string jinggao = "";
                 int fla = 0;//清点成功标志
                 int a1, a2, a3, a5, a6, a7;
                 for (int i = 0; i < dgvZxd.Rows.Count; i++)
                 {
                     for (int j = 0; j < dgvZxd.Columns.Count; j++)
                     {
                         dgvZxd.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                         dgvZxd.Rows[i].Cells[4].Style.ForeColor = Color.Black;

                     }
                     if (dgvZxd.Rows[i].Cells[1].Value == "")
                         a1 = 0;
                     else a1 = Convert.ToInt32(dgvZxd.Rows[i].Cells[1].Value);
                     if (dgvZxd.Rows[i].Cells[2].Value == "")
                         a2 = 0;
                     else a2 = Convert.ToInt32(dgvZxd.Rows[i].Cells[2].Value);
                     if (dgvZxd.Rows[i].Cells[3].Value == "")
                         a3 = 0;
                     else a3 = Convert.ToInt32(dgvZxd.Rows[i].Cells[3].Value);

                     if (dgvZxd.Rows[i].Cells[5].Value == "")
                         a5 = 0;
                     else a5 = Convert.ToInt32(dgvZxd.Rows[i].Cells[5].Value);
                     if (dgvZxd.Rows[i].Cells[6].Value == "")
                         a6 = 0;
                     else a6 = Convert.ToInt32(dgvZxd.Rows[i].Cells[6].Value);
                     if (dgvZxd.Rows[i].Cells[7].Value == "")
                         a7 = 0;
                     else a7 = Convert.ToInt32(dgvZxd.Rows[i].Cells[7].Value);

                     if (a1 != a2 || a1 != a3)
                     {
                         fla++;
                         jinggao = jinggao + dgvZxd.Rows[i].Cells[0].Value.ToString() + "\n";
                         dgvZxd.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                     }
                     if (a5 != a6 || a5 != a7)
                     {
                         fla++;
                         jinggao = jinggao + dgvZxd.Rows[i].Cells[4].Value.ToString() + "\n";
                         dgvZxd.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                     }

                 }
                 if (fla == 0)
                     MessageBox.Show("清点成功！");
                 else
                     MessageBox.Show(jinggao + "数量不正确");

             }
             catch (Exception ex)
             {

                 MessageBox.Show(ex.ToString(), "请填写完整的术前、关前、关后、缝合皮肤核对信息！");
             }
         }  
        #endregion
        #region<<器械包>>
        private void btnConfig_Click(object sender, EventArgs e)//器械包确认
        {
            try
            {
                DataTable dt = dal.SelectzxdModel2(cmdQxmb.Text);
                //int dtcount = dt.Rows.Count;
                //int j = dt.Rows.Count / 3;
                //dgvNurseRecord.Rows.Clear();
                //dgvNurseRecord.Rows.Add(j + 1);
                //for (int i = 0; i < dtcount; i++)
                //{
                //    if (i % 3 == 0)
                //    {
                //        dgvNurseRecord.Rows[i / 3].Cells[0].Value = dt.Rows[i][0];
                //        dgvNurseRecord.Rows[i / 3].Cells[1].Value = dt.Rows[i][1];
                //    }
                //    else if (i % 3 == 1)
                //    {
                //        dgvNurseRecord.Rows[i / 3].Cells[4].Value = dt.Rows[i][0];
                //        dgvNurseRecord.Rows[i / 3].Cells[5].Value = dt.Rows[i][1];
                //    }
                //    else if (i % 3 == 2)
                //    {
                //        dgvNurseRecord.Rows[i / 3].Cells[8].Value = dt.Rows[i][0];
                //        dgvNurseRecord.Rows[i / 3].Cells[9].Value = dt.Rows[i][1];
                //    }
                //}
                int dtcount = dt.Rows.Count;
                int j = dt.Rows.Count / 3;//三列
                dgvNurseRecord.Rows.Clear();
                dgvNurseRecord.Rows.Add(j + 1);
                int a = 0;
                int c1 = 0, c2 = 1;
                for (int i = 0; i < dtcount; i++)
                {

                    if (i == 11)
                    {
                        c1 = 4; c2 = 5; a = 0;
                    }
                    else if (i == 22)
                    {
                        c1 = 8; c2 = 9; a = 0;
                    }
                    dgvNurseRecord.Rows[a].Cells[c1].Value = dt.Rows[i][0];
                   // dgvNurseRecord.Rows[a].Cells[c2].Value = dt.Rows[i][1];
                    a++;

                }
            }

            catch (Exception)
            {
                MessageBox.Show("注（选择不能为空）");
            }

            
        }
        private void GetQixiebaobaocun()//器械保存
        {
            try
            {
                DataTable b = cll.BCHqixiefuliao(patID);

                if (b.Rows.Count <= 0)
                {
                    for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
                                dgvNurseRecord.Rows[i].Cells[j].Value = "";
                        }
                        Dictionary<string, string> zxdList = new Dictionary<string, string>();
                        zxdList.Clear();
                        int result = 0;
                        zxdList.Add("binganhao", patID);
                        zxdList.Add("qxpm", dgvNurseRecord.Rows[i].Cells[0].Value.ToString());
                        zxdList.Add("sqqd", dgvNurseRecord.Rows[i].Cells[1].Value.ToString());
                        zxdList.Add("gq", dgvNurseRecord.Rows[i].Cells[2].Value.ToString());
                        zxdList.Add("gh", dgvNurseRecord.Rows[i].Cells[3].Value.ToString());
                        zxdList.Add("qxpm1", dgvNurseRecord.Rows[i].Cells[4].Value.ToString());
                        zxdList.Add("sqqd1", dgvNurseRecord.Rows[i].Cells[5].Value.ToString());
                        zxdList.Add("gq1", dgvNurseRecord.Rows[i].Cells[6].Value.ToString());
                        zxdList.Add("gh1", dgvNurseRecord.Rows[i].Cells[7].Value.ToString());
                        zxdList.Add("qxpm2", dgvNurseRecord.Rows[i].Cells[8].Value.ToString());
                        zxdList.Add("sqqd2", dgvNurseRecord.Rows[i].Cells[9].Value.ToString());
                        zxdList.Add("gq2", dgvNurseRecord.Rows[i].Cells[10].Value.ToString());
                        zxdList.Add("gh2", dgvNurseRecord.Rows[i].Cells[11].Value.ToString());
                        result = dal.hlqxXingZeng(zxdList);
                    }
                }
                else
                {
                    for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
                    {
                        for (int j = 0; j < 12; j++)
                        {
                            if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
                                dgvNurseRecord.Rows[i].Cells[j].Value = "";
                        }
                        Dictionary<string, string> zxdList = new Dictionary<string, string>();
                        zxdList.Clear();
                        int result = 0;
                        zxdList.Add("binganhao", patID);
                        zxdList.Add("qxpm", dgvNurseRecord.Rows[i].Cells[0].Value.ToString());
                        zxdList.Add("sqqd", dgvNurseRecord.Rows[i].Cells[1].Value.ToString());
                        zxdList.Add("gq", dgvNurseRecord.Rows[i].Cells[2].Value.ToString());
                        zxdList.Add("gh", dgvNurseRecord.Rows[i].Cells[3].Value.ToString());
                        zxdList.Add("qxpm1", dgvNurseRecord.Rows[i].Cells[4].Value.ToString());
                        zxdList.Add("sqqd1", dgvNurseRecord.Rows[i].Cells[5].Value.ToString());
                        zxdList.Add("gq1", dgvNurseRecord.Rows[i].Cells[6].Value.ToString());
                        zxdList.Add("gh1", dgvNurseRecord.Rows[i].Cells[7].Value.ToString());
                        zxdList.Add("qxpm2", dgvNurseRecord.Rows[i].Cells[8].Value.ToString());
                        zxdList.Add("sqqd2", dgvNurseRecord.Rows[i].Cells[9].Value.ToString());
                        zxdList.Add("gq2", dgvNurseRecord.Rows[i].Cells[10].Value.ToString());
                        zxdList.Add("gh2", dgvNurseRecord.Rows[i].Cells[11].Value.ToString());
                        result = dal.hlQXXiuGai(zxdList);
                    }
                }

            }
            catch (Exception)
            {
                MessageBox.Show("注（选择不能为空）");
            }
                
            
                
            
        }
        private void Getfuliaobaocun()//敷料保存
        {
            DataTable q = cll.qixiefuliao1(patID);
            if (q.Rows.Count <= 0)
            {
                for (int i = 0; i < dgvZxd.Rows.Count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (dgvZxd.Rows[i].Cells[j].Value == null)
                            dgvZxd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    int result = 0;
                    zxdList.Add("binganhao", patID);
                    zxdList.Add("flpm", dgvZxd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", dgvZxd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gq", dgvZxd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("gh", dgvZxd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("flpm1", dgvZxd.Rows[i].Cells[4].Value.ToString());
                    zxdList.Add("sqqd1", dgvZxd.Rows[i].Cells[5].Value.ToString());
                    zxdList.Add("gq1", dgvZxd.Rows[i].Cells[6].Value.ToString());
                    zxdList.Add("gh1", dgvZxd.Rows[i].Cells[7].Value.ToString());
                    result = dal.hlflXingZeng(zxdList);
                }
            }
            else
            {
                for (int i = 0; i < dgvZxd.Rows.Count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (dgvZxd.Rows[i].Cells[j].Value == null)
                            dgvZxd.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> zxdList = new Dictionary<string, string>();
                    zxdList.Clear();
                    int result = 0;
                    zxdList.Add("binganhao", patID);
                    zxdList.Add("flpm", dgvZxd.Rows[i].Cells[0].Value.ToString());
                    zxdList.Add("sqqd", dgvZxd.Rows[i].Cells[1].Value.ToString());
                    zxdList.Add("gq", dgvZxd.Rows[i].Cells[2].Value.ToString());
                    zxdList.Add("gh", dgvZxd.Rows[i].Cells[3].Value.ToString());
                    zxdList.Add("flpm1", dgvZxd.Rows[i].Cells[4].Value.ToString());
                    zxdList.Add("sqqd1", dgvZxd.Rows[i].Cells[5].Value.ToString());
                    zxdList.Add("gq1", dgvZxd.Rows[i].Cells[6].Value.ToString());
                    zxdList.Add("gh1", dgvZxd.Rows[i].Cells[7].Value.ToString());
                    result = dal.hlFLXiuGai(zxdList);
                }
            }
        }
        #region
        //private void GetQixiebaobaocun()//保存
        //{
        //    DataTable dt = dal.GetQixiebaobc1(patID, cmdQxmb.Text);
        //    if (dt.Rows.Count > 0)
        //    {
        //        int zxd = dal.Deleteqixiebaobc(patID, cmdQxmb.Text);
        //    }
        //    try
        //    {
        //        for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < 12; j++)
        //            {
        //                if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
        //                    dgvNurseRecord.Rows[i].Cells[j].Value = "";
        //            }
        //            Dictionary<string, string> zxdList = new Dictionary<string, string>();
        //            zxdList.Clear();
        //            int result = 0;
        //            zxdList.Add("binganhao", patID);
        //            //zxdList.Add("cyb", cmdQxmb.Text);
        //            zxdList.Add("qxpm", dgvNurseRecord.Rows[i].Cells[0].Value.ToString());
        //            zxdList.Add("sqqd", dgvNurseRecord.Rows[i].Cells[1].Value.ToString());
        //            zxdList.Add("gq", dgvNurseRecord.Rows[i].Cells[2].Value.ToString());
        //            zxdList.Add("gh", dgvNurseRecord.Rows[i].Cells[3].Value.ToString());
        //            zxdList.Add("qxpm1", dgvNurseRecord.Rows[i].Cells[4].Value.ToString());
        //            zxdList.Add("sqqd1", dgvNurseRecord.Rows[i].Cells[5].Value.ToString());
        //            zxdList.Add("gq1", dgvNurseRecord.Rows[i].Cells[6].Value.ToString());
        //            zxdList.Add("gh1", dgvNurseRecord.Rows[i].Cells[7].Value.ToString());
        //            zxdList.Add("qxpm2", dgvNurseRecord.Rows[i].Cells[8].Value.ToString());
        //            zxdList.Add("sqqd2", dgvNurseRecord.Rows[i].Cells[9].Value.ToString());
        //            zxdList.Add("gq2", dgvNurseRecord.Rows[i].Cells[10].Value.ToString());
        //            zxdList.Add("gh2", dgvNurseRecord.Rows[i].Cells[11].Value.ToString());
        //            result = dal.Insertzxqidqingdian(zxdList);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.Message, "保存失败");
        //    }
        //}
        #endregion
        private void infoqixiebao()//信息
        {
            try
            {
                DataTable dt = dal.GetQcv(patID);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i >= dgvNurseRecord.Rows.Count)
                    {
                        dgvNurseRecord.Rows.Add();
                    }
                    dgvNurseRecord.Rows[i].Cells[0].Value = dt.Rows[i]["zxdname"];
                    dgvNurseRecord.Rows[i].Cells[1].Value = dt.Rows[i]["sqqd"];
                    dgvNurseRecord.Rows[i].Cells[2].Value = dt.Rows[i]["gqhd"];
                    dgvNurseRecord.Rows[i].Cells[3].Value = dt.Rows[i]["ghhd"];
                    dgvNurseRecord.Rows[i].Cells[4].Value = dt.Rows[i]["zxdname1"];
                    dgvNurseRecord.Rows[i].Cells[5].Value = dt.Rows[i]["sqqd1"];
                    dgvNurseRecord.Rows[i].Cells[6].Value = dt.Rows[i]["gqhd1"];
                    dgvNurseRecord.Rows[i].Cells[7].Value = dt.Rows[i]["ghhd1"];
                    dgvNurseRecord.Rows[i].Cells[8].Value = dt.Rows[i]["zxdname2"];
                    dgvNurseRecord.Rows[i].Cells[9].Value = dt.Rows[i]["sqqd2"];
                    dgvNurseRecord.Rows[i].Cells[10].Value = dt.Rows[i]["gqhd2"];
                    dgvNurseRecord.Rows[i].Cells[11].Value = dt.Rows[i]["ghhd2"];
                }
                dgvNurseRecord.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "止血带包清点加载失败");
            }
        }
        private void Qixiebao()
         {
             cmdQxmb.Items.Clear();
             DataTable dt = dal.GetallzxdModel2();
             for (int i = 0; i < dt.Rows.Count; i++)
             {
                 cmdQxmb.Items.Add(dt.Rows[i][0]);
             }
         }
        private void btnQxQc_Click(object sender, EventArgs e)//清空
        {
            if (MessageBox.Show("是否清空此人的常用包", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int a = dal.Deleteqixiebaobc(patID, cmdQxmb.Text);
                dgvNurseRecord.Rows.Clear();
            }
            else
                return;
        }
        private void button1_Click(object sender, EventArgs e)
        { 
            
            try
            {
                string jinggao = "";
                int flag = 0;//清点成功标志
                int a1 = 0, a2 = 0, a3 = 0, a4 = 0, a5 = 0, a6 = 0, a7 = 0, a8 = 0, a9 = 0, a10 = 0, a11 = 0;
                for (int i = 0; i < dgvNurseRecord.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvNurseRecord.Columns.Count; j++)
                    {
                        dgvNurseRecord.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                        dgvNurseRecord.Rows[i].Cells[4].Style.ForeColor = Color.Black;
                        dgvNurseRecord.Rows[i].Cells[8].Style.ForeColor = Color.Black;
                    }
                    if (dgvNurseRecord.Rows[i].Cells[1].Value == "")
                        a1 = 0;
                    else a1 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[1].Value);

                    if (dgvNurseRecord.Rows[i].Cells[2].Value == "")
                        a2 = 0;
                    else a2 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[2].Value);
                    if (dgvNurseRecord.Rows[i].Cells[3].Value == "")
                        a3 = 0;
                    else a3 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[3].Value);
                    if (dgvNurseRecord.Rows[i].Cells[5].Value == "")
                        a5 = 0;
                    else a5 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[5].Value);
                    if (dgvNurseRecord.Rows[i].Cells[6].Value == "")
                        a6 = 0;
                    else a6 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[6].Value);
                    if (dgvNurseRecord.Rows[i].Cells[7].Value == "")
                        a7 = 0;
                   else a7 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[7].Value);
                    if (dgvNurseRecord.Rows[i].Cells[9].Value == "")
                        a9 = 0;
                    else a9 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[9].Value);
                    if (dgvNurseRecord.Rows[i].Cells[9].Value == "0")
                    {
                        a9 = int.Parse("╲");
                    }

                    if (dgvNurseRecord.Rows[i].Cells[10].Value == "")
                        a10 = 0;
                    else a10 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[10].Value);
                    if (dgvNurseRecord.Rows[i].Cells[11].Value == "")
                        a11 = 0;
                    else a11 = Convert.ToInt32(dgvNurseRecord.Rows[i].Cells[11].Value);
                    if (a1 != a2 || a1 != a3)
                    {
                        flag++;
                        jinggao = jinggao + dgvNurseRecord.Rows[i].Cells[0].Value.ToString() + "\n";
                        dgvNurseRecord.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                    }
                    if (a5 != a6 || a5 != a7)
                    {
                        flag++;
                        jinggao = jinggao + dgvNurseRecord.Rows[i].Cells[4].Value.ToString() + "\n";
                        dgvNurseRecord.Rows[i].Cells[4].Style.ForeColor = Color.Red;
                    }
                    if (a9 != a11 || a9 != a10)
                    {
                        flag++;
                        jinggao = jinggao + dgvNurseRecord.Rows[i].Cells[8].Value.ToString() + "\n";
                        dgvNurseRecord.Rows[i].Cells[8].Style.ForeColor = Color.Red;
                    }
                }
                if (flag == 0)
                    MessageBox.Show("清点成功！");
                else
                    MessageBox.Show(jinggao + "数量不正确");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex+"请填写完整的术前、关前、关后核对信息！");
            }

        }
      #endregion
        #region<<打印>>
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
        private void printDocument1_BeginPrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;//打印
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            //           //printDocument1.DefaultPageSettings.PaperSize =
            //    new System.Drawing.Printing.PaperSize("K16", 740, 1020);
            printDocument1.DefaultPageSettings.PaperSize =
                       new System.Drawing.Printing.PaperSize("A4", 820, 1160);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int row = 0;
            Font zt9 = new Font("宋体", 9);//字体
            Font zt10 = new Font("宋体", 10);
            Font zt11 = new Font("宋体", 12);
            Font ht13 = new Font("黑体", 13);
            Font ht18 = new Font("黑体", 18);
            Pen pblue = new Pen(Brushes.Blue);
            pblue.Width = 2;
            Pen ptp = Pens.Gray;//下划线画笔
            Pen pb1 = new Pen(Brushes.Black);  //普通画笔
            Pen pb2 = new Pen(Brushes.Black, 3);
            Brush bp1 = Brushes.Black;
            int y = 40; int x = 35; int y1 = 0; //整体位置
            y = y + 20;
            string title1 = " 新疆医科大学第五附属医院";
            string title2 = " 手 术 护 理 记 录 单 ";
            e.Graphics.DrawString(title1, ht13, Brushes.Black, x + 230, y);
            y = y + 25;
            e.Graphics.DrawString(title2, ht18, Brushes.Black, x + 205, y);

            y = y + 40;
            int Y_Line = y + 20;
            e.Graphics.DrawString("日期:" + dtVisitDate.Value.ToString("yyyy-MM-dd"), zt10, Brushes.Black, x , y + 5);
            e.Graphics.DrawLine(ptp, new Point(35 + x, Y_Line), new Point(130 + x, Y_Line));
            e.Graphics.DrawString("姓名:" + tbPatname.Text, zt10, Brushes.Black, x + 130, y + 5);
            e.Graphics.DrawLine(ptp, new Point(165 + x, Y_Line), new Point(263 + x, Y_Line));
            e.Graphics.DrawString("民族:" + tbMingzu.Text, zt10, Brushes.Black, x + 263, y + 5);
            e.Graphics.DrawLine(ptp, new Point(300 + x, Y_Line), new Point(380 + x, Y_Line));
            e.Graphics.DrawString("性别: " + tbSex.Text, zt10, Brushes.Black, x + 380, y + 5);
            e.Graphics.DrawLine(ptp, new Point(417 + x, Y_Line), new Point(447 + x, Y_Line));
            e.Graphics.DrawString("年龄: " + tbAge.Text, zt10, Brushes.Black, x + 447, y + 5);
            e.Graphics.DrawLine(ptp, new Point(484 + x, Y_Line), new Point(524 + x, Y_Line));
            e.Graphics.DrawString("住院号:" + tbZhuyuanID.Text, zt10, Brushes.Black, x + 524, y + 5);
            e.Graphics.DrawLine(ptp, new Point(570 + x, Y_Line), new Point(660 + x, Y_Line));
            e.Graphics.DrawString("床号:" + tbBedNO.Text, zt10, Brushes.Black, x + 660, y + 5);
            e.Graphics.DrawLine(ptp, new Point(697 + x, Y_Line), new Point(725 + x, Y_Line));
            y = y + 20;
            e.Graphics.DrawString("科室:" + tbKeshi.Text, zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawLine(ptp, new Point(35 + x, y + 20), new Point(160 + x, y + 20));
            e.Graphics.DrawString("术前诊断:" + tbSZZD.Text, zt10, Brushes.Black, x + 160, y + 5);
            e.Graphics.DrawLine(ptp, new Point(220 + x, y + 20), new Point(400 + x, y + 20));
            e.Graphics.DrawString("手术名称:" + tbShoushuName.Text, zt10, Brushes.Black, x + 400, y + 5);
            e.Graphics.DrawLine(ptp, new Point(460 + x, y + 20), new Point(725 + x, y + 20));
            y = y + 20;
            e.Graphics.DrawString("药物过敏史:" + tbGuominshi.Text, zt10, Brushes.Black, x, y + 5);
            e.Graphics.DrawLine(ptp, new Point(80 + x, y + 20), new Point(180 + x, y + 20));
            e.Graphics.DrawString("麻醉方式:" + cmbMZFF.Text, zt10, Brushes.Black, x + 180, y + 5);
            e.Graphics.DrawLine(ptp, new Point(240 + x, y + 20), new Point(400 + x, y + 20));
            e.Graphics.DrawString("体重:" + textBox2.Text, zt10, Brushes.Black, x + 400, y + 5);
            e.Graphics.DrawString("千克（推入）", zt10, Brushes.Black, x + 500, y + 5);
            e.Graphics.DrawLine(ptp, new Point(437 + x, y + 20), new Point(500 + x, y + 20));
            e.Graphics.DrawString("手术间: " + cmbOroom.Text, zt10, Brushes.Black, x + 590, y + 5);
            e.Graphics.DrawString("室", zt10, Brushes.Black, x + 690, y + 5);
            e.Graphics.DrawLine(ptp, new Point(645 + x, y + 20), new Point(690 + x, y + 20));        
            y = y + 25; y1 = y;
            e.Graphics.DrawLine(pb1, x, y, x + 725, y);//最上面的横线
            e.Graphics.DrawLine(pb1, x , y +250, x + 725, y + 250);
            //左右竖线
            e.Graphics.DrawLine(pb1, x , y, x , y + 923);
            e.Graphics.DrawLine(pb1, x + 35, y, x + 35, y + 250);
            e.Graphics.DrawLine(pb1, x + 725, y, x + 725, y + 923);


            e.Graphics.DrawString("护", zt11, Brushes.Black, x + 5, y + 55);
            e.Graphics.DrawString("理", zt11, Brushes.Black, x + 5, y + 90);
            e.Graphics.DrawString("情", zt11, Brushes.Black, x + 5, y + 125);
            e.Graphics.DrawString("况", zt11, Brushes.Black, x + 5, y + 160);
          
            e.Graphics.DrawString("术前:", zt11, Brushes.Black, x + 40, y + 8 );
            e.Graphics.DrawString("入室时间: ", zt10, Brushes.Black, x +90, y + 10);
            e.Graphics.DrawLine(ptp, new Point(150 + x, y + 25), new Point(240 + x, y + 25));
            e.Graphics.DrawString("神志: " + cmbShenzhi.Text.Trim(), zt10, Brushes.Black, x + 240, y + 10);
            e.Graphics.DrawLine(ptp, new Point(280 + x, y + 25), new Point(360 + x, y + 25));
            e.Graphics.DrawString("静脉穿刺:□有□无 深静脉穿刺:□有□无 导尿:□是□否", zt10, Brushes.Black, new Point(x + 360, y + 10));
            if (checkBox1.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 423, y + 10));
            if (checkBox2.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 451, y + 10));
            if (checkBox4.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 561, y + 10));
            if (checkBox3.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 591, y + 10));
            if (checkBox6.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 660, y + 10));
            if (checkBox5.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 688, y + 10));
            y = y + 20;
            e.Graphics.DrawString("皮肤情况:□完整 □其他 " + textBox1.Text.Trim(), zt10, Brushes.Black, new Point(x + 90, y + 10));
            if (checkBox7.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 152, y + 10));
            if (checkBox8.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 200, y + 10));
            e.Graphics.DrawLine(ptp, new Point(250 + x, y + 25), new Point(580 + x, y + 25));
            e.Graphics.DrawString("签名：" + textBox3.Text.Trim(), zt10, Brushes.Black, new Point(x + 580, y + 10));
            e.Graphics.DrawLine(ptp, new Point(620 + x, y + 25), new Point(715 + x, y + 25));
            y = y + 25;
            e.Graphics.DrawString("术中:", zt11, Brushes.Black, x + 40, y + 8);
            e.Graphics.DrawString("体位:□仰卧位□侧卧位□俯卧位□截石位□坐位□其他 " + comboBox1.Text.Trim(), zt10, Brushes.Black, x + 90, y + 10);
            if (checkBox10.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 125, y + 10));
            if (checkBox9.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 180, y + 10));
            if (checkBox12.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 235, y + 10));
            if (checkBox11.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 290, y + 10));
            if (checkBox13.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 348, y + 10));
            if (checkBox41.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 390, y + 10));
            e.Graphics.DrawLine(ptp, new Point(440 + x, y + 25), new Point(500 + x, y + 25));
            e.Graphics.DrawString("标本送冰冻:□无标本□已送□未送", zt10, Brushes.Black, x + 500, y + 10);
            if (checkBox16.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 577, y + 10));
            if (checkBox15.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 631, y + 10));
            if (checkBox14.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 675, y + 10));
            y = y + 20;
            e.Graphics.DrawString("使用电刀:□是□否  负极粘贴位置:□大腿□小腿□手臂□臀 ", zt10, Brushes.Black, x + 40, y + 10);
            if (checkBox18.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 103, y + 10));
            if (checkBox17.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 131, y + 10));
            if (checkBox21.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 262, y + 10));
            if (checkBox20.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 305, y + 10));
            if (checkBox19.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 347, y + 10));
            if (checkBox22.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 389, y + 10));
            e.Graphics.DrawString("尿量: " + comboBox8.Text.Trim(), zt10, Brushes.Black, x + 430, y + 10);
            e.Graphics.DrawLine(ptp, new Point(462 + x, y + 25), new Point(535 + x, y + 25));
            e.Graphics.DrawString("毫升", zt10, Brushes.Black, x + 535, y + 10);
            e.Graphics.DrawString("输液量: " + tbShuye.Text.Trim(), zt10, Brushes.Black, x + 570, y + 10);
            e.Graphics.DrawLine(ptp, new Point(617 + x, y + 25), new Point(689 + x, y + 25));
            e.Graphics.DrawString("毫升", zt10, Brushes.Black, x + 690, y + 10);
            y = y + 20;
            e.Graphics.DrawString("输血量:自体血" + comboBox7.Text.Trim(), zt10, Brushes.Black, x + 40, y + 10);
            e.Graphics.DrawLine(ptp, new Point(137 + x, y + 25), new Point(190 + x, y + 25));
            e.Graphics.DrawString("毫升", zt10, Brushes.Black, x + 190, y + 10);
            e.Graphics.DrawString("红细胞悬液: " + textBox5.Text.Trim(), zt10, Brushes.Black, x + 230, y + 10);
            e.Graphics.DrawLine(ptp, new Point(310 + x, y + 25), new Point(390 + x, y + 25));
            e.Graphics.DrawString("毫升", zt10, Brushes.Black, x + 390, y + 10);
            e.Graphics.DrawString("血浆: " + textBox6.Text.Trim(), zt10, Brushes.Black, x + 430, y + 10);
            e.Graphics.DrawLine(ptp, new Point(462 + x, y + 25), new Point(535 + x, y + 25));
            e.Graphics.DrawString("毫升", zt10, Brushes.Black, x + 535, y + 10);
            e.Graphics.DrawString("血小板: " + textBox7.Text.Trim(), zt10, Brushes.Black, x + 570, y + 10);
            e.Graphics.DrawLine(ptp, new Point(617 + x, y + 25), new Point(689 + x, y + 25));
            e.Graphics.DrawString("单位", zt10, Brushes.Black, x + 690, y + 10);
            y = y + 20;
            e.Graphics.DrawString("血型: " + comboBox2.Text.Trim(), zt10, Brushes.Black, x + 40, y + 10);
            e.Graphics.DrawLine(ptp, new Point(77 + x, y + 25), new Point(159 + x, y + 25));
            e.Graphics.DrawString("型 (RH:□阳性□阴性)", zt10, Brushes.Black, x + 159, y + 10);
            if (checkBox24.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 210, y + 10));
            if (checkBox23.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 253, y + 10));
            e.Graphics.DrawString("其他: " + comboBox3.Text.Trim(), zt10, Brushes.Black, x + 320, y + 10);
            e.Graphics.DrawLine(ptp, new Point(356 + x, y + 25), new Point(520 + x, y + 25));
            e.Graphics.DrawString("签名：" + textBox3.Text.Trim(), zt10, Brushes.Black, new Point(x + 550, y + 10));
            e.Graphics.DrawLine(ptp, new Point(590 + x, y + 25), new Point(685 + x, y + 25));
            y = y + 25;
            e.Graphics.DrawString("术毕:", zt11, Brushes.Black, x + 40, y + 8);
            e.Graphics.DrawString("皮肤情况: □完整 □同术前 见备注          神志：□清醒 □未清醒   引流：□有 □无" + comboBox1.Text.Trim(), zt10, Brushes.Black, x + 90, y + 10);
            if (checkBox26.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 160, y + 10));
            if (checkBox25.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 209, y + 10));
            if (checkBox28.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 426, y + 10));
            if (checkBox27.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 473, y + 10));
            if (checkBox30.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 590, y + 10));
            if (checkBox29.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 628, y + 10));
            y = y + 20;
            e.Graphics.DrawString("标本送病理: ", zt10, Brushes.Black, x + 40, y + 10);
            e.Graphics.DrawString("□无标本 □已送 □未送(原因: " + textBox9.Text.Trim(), zt10, Brushes.Black, x + 129, y + 10);
            e.Graphics.DrawLine(ptp, new Point(327 + x, y + 25), new Point(415 + x, y + 25));
            if (checkBox32.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 130, y + 10));
            if (checkBox31.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 192, y + 10));
            if (checkBox33.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 243, y + 10));
            e.Graphics.DrawString("签名: " + textBox10.Text.Trim(), zt10, Brushes.Black, x + 420, y + 10);
            e.Graphics.DrawLine(ptp, new Point(452 + x, y + 25), new Point(550 + x, y + 25));
            e.Graphics.DrawString(")" + textBox10.Text.Trim(), zt10, Brushes.Black, x + 550, y + 10);
            e.Graphics.DrawString("出室时间：", zt10, Brushes.Black, new Point(x + 560, y + 10));
            e.Graphics.DrawLine(ptp, new Point(630 + x, y + 25), new Point(705 + x, y + 25));

            y = y + 25;
            e.Graphics.DrawString("术后送回: ", zt11, Brushes.Black, x + 40, y + 8);
            e.Graphics.DrawString("□病房□ICU□EICU□PACU(返病室时间: " + dateTimePicker1.Text, zt10, Brushes.Black, x + 122, y + 10);
            e.Graphics.DrawLine(ptp, new Point(377 + x, y + 25), new Point(445 + x, y + 25));
            if (checkBox36.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 121, y + 10));
            if (checkBox35.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 163, y + 10));
            if (checkBox37.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 203, y + 10));
            if (checkBox34.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 243, y + 10));
            e.Graphics.DrawString("签名: " + textBox11.Text.Trim(), zt10, Brushes.Black, x + 450, y + 10);
            e.Graphics.DrawLine(ptp, new Point(482 + x, y + 25), new Point(580 + x, y + 25));
            e.Graphics.DrawString(")", zt10, Brushes.Black, x + 580, y + 10);
            e.Graphics.DrawString("其他：" + textBox12.Text.Trim(), zt10, Brushes.Black, new Point(x + 590, y + 10));
            e.Graphics.DrawLine(ptp, new Point(630 + x, y + 25), new Point(705 + x, y + 25));
            y = y + 25;
           
            string str = "";
            int a = textBox13.TextLength / 40;
            for (int i = 0; i < textBox13.TextLength / 40; i++)
            {
                if (i < a)
                {
                    str += textBox13.Text.Substring(i * 40, 40) + Environment.NewLine;
                }
                else
                {
                    str += textBox13.Text.Substring(i * 40);
                }
            }
              e.Graphics.DrawString("手术用药: " + str,zt11, Brushes.Black, x + 40, y + 10);
              e.Graphics.DrawLine(ptp, new Point(120 + x, y + 25), new Point(705 + x, y + 25));
              y = y + 20;
              e.Graphics.DrawLine(ptp, new Point(50 + x, y + 25), new Point(575 + x, y + 25));
              e.Graphics.DrawString("签名：" + textBox14.Text.Trim(), zt10, Brushes.Black, new Point(x + 580, y + 10));
              e.Graphics.DrawLine(ptp, new Point(620 + x, y + 25), new Point(705 + x, y + 25));
             y = y + 25;
             e.Graphics.DrawString("无菌包监测:    □合格", zt10, Brushes.Black, x  , y + 10);
             if (checkBox38.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 106, y + 10));
             e.Graphics.DrawString("器械核查:  □术前完整  □术后完整", zt10, Brushes.Black, x + 180, y + 10);
             if (checkBox39.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 258, y + 10));
             if (checkBox40.Checked) e.Graphics.DrawString("✔", zt10, Brushes.Black, new Point(x + 340, y + 10));
             //e.Graphics.DrawString("其他：" + textBox15.Text.Trim(), zt10, Brushes.Black, new Point(x + 430, y + 10));
             //e.Graphics.DrawLine(ptp, new Point(470 + x, y + 25), new Point(595 + x, y + 25));
             y = y + 25;
             #region<<常用包>>
             //品名行
             e.Graphics.DrawLine(ptp, new Point(x , y), new Point(725 + x, y));
             e.Graphics.DrawLine(ptp, new Point(x + 100, y), new Point(x + 100, y + 275));
             e.Graphics.DrawLine(ptp, new Point(x + 180, y), new Point(x + 180, y + 275));
             e.Graphics.DrawLine(ptp, new Point(x + 270, y), new Point(x + 270, y + 275));
             e.Graphics.DrawLine(ptp, new Point(x + 360, y), new Point(x + 360, y + 275));
             e.Graphics.DrawLine(ptp, new Point(x + 450, y), new Point(x + 450, y + 275));
             e.Graphics.DrawLine(ptp, new Point(x + 540, y), new Point(x + 540, y + 275));
             e.Graphics.DrawLine(ptp, new Point(x + 630, y), new Point(x + 630, y + 275));
            //e.Graphics.DrawLine(ptp, new Point(x + 720, y), new Point(x + 720, y + 5 * (10 * 2)));
            // e.Graphics.DrawLine(ptp, new Point(x , y + 5 * (10 * 2)), new Point(675 + x, y + 5 * (10 * 2)));
             //表头列表
             e.Graphics.DrawString(dgvZxd.Columns[0].HeaderText, zt9, Brushes.Black, x + 35, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[1].HeaderText, zt9, Brushes.Black, x + 110, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[2].HeaderText, zt9, Brushes.Black, x + 200, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[3].HeaderText, zt9, Brushes.Black, x + 285, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[4].HeaderText, zt9, Brushes.Black, x + 390, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[5].HeaderText, zt9, Brushes.Black, x + 465, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[6].HeaderText, zt9, Brushes.Black, x + 560, y + 5);
             e.Graphics.DrawString(dgvZxd.Columns[7].HeaderText, zt9, Brushes.Black, x + 650, y + 5);
             //品名信息
             int flag = 0;
             int count = Convert.ToInt32(dgvZxd.Rows.Count);
             for (int i = row; i < count; i++)
             {
                 flag++;
                 for (int j = 0; j < 8; j++)
                 {
                     if (dgvZxd.Rows[i].Cells[j].Value == null)
                         dgvZxd.Rows[i].Cells[j].Value = "";
                 }

                 //e.Graphics.DrawLine(Pens.Black, x, y + 25 * flag, x + 635, y + 25 * flag);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[0].Value.ToString(), zt9, Brushes.Black, x + 25, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 130, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 220, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 305, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[4].Value.ToString(), zt9, Brushes.Black, x + 380, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 485, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 580, y + 5 + flag * 25);
                 e.Graphics.DrawString(dgvZxd.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 670, y + 5 + flag * 25);
             }
             for (int j = 0; j < 11; j++)
             {
                 e.Graphics.DrawLine(Pens.Black, x , y + 25 * (j + 1), x + 725, y + 25 * (j + 1));
             }
             #endregion
             //画器械
             y = y + 285; y1 = y;
             e.Graphics.DrawLine(Pens.Black, new Point(x, y - 3), new Point(x + 725, y - 3));
             e.Graphics.DrawLine(ptp, new Point(x + 80, y - 3), new Point(x + 80, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 140, y - 3), new Point(x + 140, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 200, y - 3), new Point(x + 200, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 258, y - 3), new Point(x + 258, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 316, y - 3), new Point(x + 316, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 374, y - 3), new Point(x + 374, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 432, y - 3), new Point(x + 432, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 485, y - 3), new Point(x + 485, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 547, y - 3), new Point(x + 547, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 605, y - 3), new Point(x + 605, y + 10 * (15 * 2)));
             e.Graphics.DrawLine(ptp, new Point(x + 663, y - 3), new Point(x + 663, y + 10 * (15 * 2)));
             //e.Graphics.DrawLine(ptp, new Point(x, y + 5 * (10 * 2)), new Point(635 + x, y + 5 * (10 * 2)));

             //表头
             e.Graphics.DrawString(dgvNurseRecord.Columns[0].HeaderText, zt9, Brushes.Black, x + 30, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[1].HeaderText, zt9, Brushes.Black, x + 87, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[2].HeaderText, zt9, Brushes.Black, x + 145, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[3].HeaderText, zt9, Brushes.Black, x + 203, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[4].HeaderText, zt9, Brushes.Black, x + 271, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[5].HeaderText, zt9, Brushes.Black, x + 319, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[6].HeaderText, zt9, Brushes.Black, x + 377, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[7].HeaderText, zt9, Brushes.Black, x + 432, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[8].HeaderText, zt9, Brushes.Black, x + 503, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[9].HeaderText, zt9, Brushes.Black, x + 551, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[10].HeaderText, zt9, Brushes.Black, x + 609, y + 5);
             e.Graphics.DrawString(dgvNurseRecord.Columns[11].HeaderText, zt9, Brushes.Black, x + 667, y + 5);

             int b = Convert.ToInt32(dgvNurseRecord.Rows.Count);
             int result = 0;
             for (int i = row; i < b; i++)
             {
                 result++;
                 for (int j = 0; j < 12; j++)
                 {
                     if (dgvNurseRecord.Rows[i].Cells[j].Value == null)
                         dgvNurseRecord.Rows[i].Cells[j].Value = "";
                     
                 }
                 //数据
                 //e.Graphics.DrawLine(Pens.Black, x - 20, y + 20 * result, x + 675, y + 20 * result);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[0].Value.ToString(), zt9, Brushes.Black, x, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[1].Value.ToString(), zt9, Brushes.Black, x + 103, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x + 166, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[3].Value.ToString(), zt9, Brushes.Black, x + 209, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[4].Value.ToString(), zt9, Brushes.Black, x + 272, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 335, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 398, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 458, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[8].Value.ToString(), zt9, Brushes.Black, x + 484, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 570, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 630, y + 5 + result * 25);
                 e.Graphics.DrawString(dgvNurseRecord.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 685, y + 5 + result * 25);
             }
             for (int j = 0; j < 12; j++)
             {
                 e.Graphics.DrawLine(Pens.Black, x , y + 25 * (j + 1), x + 725, y + 25 * (j + 1));
             }
             y = y + 303;
             e.Graphics.DrawString("器械护士:", zt11, Brushes.Black, x + 10, y + 5);
             e.Graphics.DrawString("手术医师:", zt11, Brushes.Black, x + 190, y + 5);
             e.Graphics.DrawString("巡回护士:", zt11, Brushes.Black, x + 380, y + 5);
             e.Graphics.DrawString("接班护士:", zt11, Brushes.Black, x + 555, y + 5);
             e.Graphics.DrawLine(pb1, x, y + 30, x + 725, y + 30);
             y = y + 25;
             e.Graphics.DrawString("备注  " + tbRemarkLast.Text, zt11, Brushes.Black, x + 10 , y + 15 );
             e.Graphics.DrawLine(pb1, x, y + 40, x + 725, y + 40);

        }
        #endregion

       












       



    }   
    
}
