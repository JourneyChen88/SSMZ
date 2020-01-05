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
    public partial class JHmzhfjld : Form
    {

        DataTable table1_copy;
        DataTable table1;
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        string ZYNumber1;
        string mzID;
        string a;
        string b;
        string c;
        string d;
        string e1;
        string f1;
        string g;
        string h;
        string i1;
        string j;
        string k;
        string l;
        string m;
        string n;
        string o;
        string p;
        string q;
        string r;
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        public JHmzhfjld(string patID,string mzbh)
        {
            mzID = mzbh;
            ZYNumber1 = patID;
            InitializeComponent();
        }
        //保存
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbPatname.Text == "")
            {
                MessageBox.Show("姓名不能为空!");
                tbPatname.Focus();
                return;
            }
            if (tbSex.Text == "")
            {
                MessageBox.Show("性别不能为空！");
                tbSex.Focus();
                return;
            }
            if (tbAge.Text == "")
            {
                MessageBox.Show("年龄不能为空！");
                tbAge.Focus();
                return;
            }
            if (tbBedNO.Text == "")
            {
                MessageBox.Show("床号不能为空！");
                tbBedNO.Focus();
                return;

            }
            if (tbZhuyuanID.Text == "")
            {
                MessageBox.Show("住院号不能为空！");
                tbZhuyuanID.Focus();
                return;
            }
            if (comboBoxmzys.Text == "")
            {
                MessageBox.Show("麻醉医生不能为空！");
                comboBoxmzys.Focus();
                return;
            }
            if (comboBoxfsys.Text == "")
            {
                MessageBox.Show("复苏医生不能为空！");
                comboBoxfsys.Focus();
                return;
            }
            if (comboBoxhsq.Text == "")
            {
                MessageBox.Show("复苏护士不能为空！");
                comboBoxhsq.Focus();
                return;
            }

            save();
            baichun();
        }
        private void save()
        {
            try
            {
                int result = 0;
                DataTable dt = DAL.Getkcpacu(ZYNumber1);
                if (dt.Rows.Count > 0)
                {
                    int s = DAL.Deletempacu(ZYNumber1);
                }
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value == null)
                            dataGridView1.Rows[i].Cells[j].Value = "";
                    }
                    Dictionary<string, string> list = new Dictionary<string, string>();
                    list.Clear();
                    //  id,zhuyuanhao,shijian,tiwen,yishi,BP,HR,RR,xiyang,SPO2,vt,f,fio2,xitan,tkdx,tkfg,zhent,pangg,bingqingycljl
                    list.Add("zhuyuanhao", ZYNumber1);
                    list.Add("shijian", dataGridView1.Rows[i].Cells[2].Value.ToString());
                    list.Add("tiwen", dataGridView1.Rows[i].Cells[3].Value.ToString());
                    list.Add("yishi", dataGridView1.Rows[i].Cells[4].Value.ToString());
                    list.Add("BP", dataGridView1.Rows[i].Cells[5].Value.ToString());
                    list.Add("HR", dataGridView1.Rows[i].Cells[6].Value.ToString());
                    list.Add("RR", dataGridView1.Rows[i].Cells[7].Value.ToString());
                    list.Add("xiyang", dataGridView1.Rows[i].Cells[8].Value.ToString());
                    list.Add("SPO2", dataGridView1.Rows[i].Cells[9].Value.ToString());
                    list.Add("vt", dataGridView1.Rows[i].Cells[10].Value.ToString());
                    list.Add("f", dataGridView1.Rows[i].Cells[11].Value.ToString());
                    list.Add("fio2", dataGridView1.Rows[i].Cells[12].Value.ToString());
                    list.Add("xitan", dataGridView1.Rows[i].Cells[13].Value.ToString());
                    list.Add("tkdx", dataGridView1.Rows[i].Cells[14].Value.ToString());
                    list.Add("tkfg", dataGridView1.Rows[i].Cells[15].Value.ToString());
                    list.Add("zj", dataGridView1.Rows[i].Cells[16].Value.ToString());
                    list.Add("zhent", dataGridView1.Rows[i].Cells[17].Value.ToString());
                    list.Add("pangg", dataGridView1.Rows[i].Cells[18].Value.ToString());
                    list.Add("bingqingycljl", dataGridView1.Rows[i].Cells[19].Value.ToString());

                    result = DAL.Insertkcpacu(list);
                }
                if (result > 0)
                {
                    //  MessageBox.Show("保存成功");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void baichun()//保存
        {

            Dictionary<string, string> SQFS = new Dictionary<string, string>();
            int result = 0;
            //            select id,zhuyuanhao,zrl,jtl,jiaoti,xxjiang,zcl,nl,yll,qt,rs,cs,csqx,fangs,fangshiqt,zhentengpeifang,zysx,zysxqt,mazuiys,mzfssys,hushi,riqi
            //from kcpacuxinxi
            SQFS.Add("zhuyuanhao", ZYNumber1);
            string AddItem = "";
            SQFS.Add("zrl", textBoxzrl.Text);
            SQFS.Add("jtl", textBoxjt.Text);
            SQFS.Add("jiaoti", textBoxjti.Text);
            SQFS.Add("xxjiang", textBoxxxj.Text);
            SQFS.Add("zcl", textBoxzcl.Text);

            SQFS.Add("nl", textBoxnl.Text);
            SQFS.Add("yll", textBoxyll.Text);
            SQFS.Add("qt", textBoxqt.Text);
            SQFS.Add("rs", textBoxrs.Text);

            SQFS.Add("cs", textBoxchus.Text);
            SQFS.Add("csqx", comboBoxcsqx.Text);
            AddItem = "";
            if (checkBoxjm.Checked) AddItem += "1";
            if (checkBoxymw.Checked) AddItem += "2";
            SQFS.Add("fangs", AddItem);
            SQFS.Add("fangshiqt", textBoxqta.Text);
            SQFS.Add("zhentengpeifang", txtSqyy.Controls[0].Text);

            //            select id,zhuyuanhao,zrl,jtl,jiaoti,xxjiang,zcl,nl,yll,qt,rs,cs,csqx,fangs,fangshiqt,
            //zhentengpeifang,zysx,zysxqt,mazuiys,mzfssys,hushi,riqi
            //from kcpacuxinxi
            AddItem = "";
            if (checkBoxqttcd.Checked) AddItem += "1";
            if (checkBoxhx.Checked) AddItem += "2";
            if (checkBoxsx.Checked) AddItem += "3";
            if (checkBoxex.Checked) AddItem += "4";
            if (checkBoxnzl.Checked) AddItem += "5";
            if (checkBoxgmfy.Checked) AddItem += "6";
            SQFS.Add("zysx", AddItem);
            SQFS.Add("zysxqt", textBoxqzysxt.Text);

            SQFS.Add("mazuiys", comboBoxmzys.Text);
            SQFS.Add("mzfssys", comboBoxfsys.Text);
            SQFS.Add("hushi", comboBoxhsq.Text);
            SQFS.Add("riqi", Convert.ToDateTime(dateTimePicker1.Value.ToString()).ToString("yyyy-MM-dd"));
            if (SaveToolStripMenuItem.Enabled)
            {
                SQFS.Add("IsRead", "0");
            }
            else
            {
                SQFS.Add("IsRead", "1");
            }
            DataTable dt = DAL.Getkcpacuxinxi(ZYNumber1);
            if (dt.Rows.Count > 0)
            {
                result = DAL.Updatemkcpacuxinxi(SQFS);
                this.table1 = DAL.Getkcpacuxinxi(ZYNumber1);
                //string v_conent = this.getEditContent(this.table1_copy.Rows[0], this.table1.Rows[0]);
                //修改后的的内容
            }
            else
                result = DAL.KCInserskcpacuxinxi(SQFS);
            if (result > 0)
            {
                MessageBox.Show("保存成功！");
            }

        }
        //public string getEditContent(DataRow row1, DataRow row2)
        //{
        //    //string v_content = "";
        //    //foreach (DataColumn col in row1.Table.Columns)
        //    //{
        //    //    if (object.Equals(row1[col.ColumnName], row2[col.ColumnName]) == false)
        //    //    {
        //    //        string v_line = "[{0}]\n<原>{1}  <新>{2}\n";
        //    //        v_line = String.Format(v_line, col.ColumnName, row1[col.ColumnName], row2[col.ColumnName]);
        //    //        v_content += v_line;
        //    //    }
        //    //}
        //    //if (v_content == "")
        //    //{
        //    //    return v_content;
        //    //}
        //    //else
        //    //{
        //    //    string aa = "";
        //    //    DAL.updatexiugaijilu(DateTime.Now, Program.customer.user_name, ZYNumber1, aa, v_content, "PACU记录单");
        //    //}
        //    //return v_content;
        //}
        //显示
        private void JHmzhfjld_Load(object sender, EventArgs e)
        {
            Bindmzdcx();
            BindHS1();
            BindMZYY();
            BindMZFUSYY();
            dgvBind();
            BindPatInfo();
            LodSQFS();
           // xiala();
        }
        
        private void Bindmzdcx()
        {
            DataTable dt = new DataTable();
            dt = dal.GetALMZFS(ZYNumber1);
                textBoxmzfangfa.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.textBoxmzfangfa.Items.Add(dt.Rows[i][0]);
                }
        }
        private void BindHS1()
        {
            DataTable dtHS = DAL.GetAll_hushi();
            comboBoxhsq.Items.Clear();
            for (int i = 0; i < dtHS.Rows.Count; i++)
            {
                this.comboBoxhsq.Items.Add(dtHS.Rows[i][0]);
            }
        }
        private void BindMZYY()
        {
            DataTable dtMZYS = DAL.GetAllMZYS();
            comboBoxmzys.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.comboBoxmzys.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        private void BindMZFUSYY()
        {
            DataTable dtMZYS = DAL.GetAllMZYS();
            comboBoxfsys.Items.Clear();
            for (int i = 0; i < dtMZYS.Rows.Count; i++)
            {
                this.comboBoxfsys.Items.Add(dtMZYS.Rows[i][0]);
            }
        }
        private void Bindtjdh()
        {
            DataTable dt = DAL.Getkcpacu(ZYNumber1);
            if (dt.Rows.Count == 0)
            {
                for (int i = 0; i < 15; i++)
                {
                    DAL.Addkcpacu(ZYNumber1);
                }
            }
            //else
            //{
            dgvBind();
            //}

        }
        private void BindPatInfo()
        {
            DataTable dt = new DataTable();
            dt = dal.GetALLPAIBAN(ZYNumber1);
            tbPatname.Text = dt.Rows[0]["Patname"].ToString();
            tbZhuyuanID.Text = dt.Rows[0]["PatZhuYuanID"].ToString();
            tbBedNO.Text = dt.Rows[0]["Patbedno"].ToString();
            //tbShoushuName.Text = dt.Rows[0]["Oname"].ToString();
            //textBoxzd.Text = dt.Rows[0][""].ToString();
            //tbBingqu.Text = dt.Rows[0]["Patdpm"].ToString();
            tbSex.Text = dt.Rows[0]["patsex"].ToString();
            tbAge.Text = dt.Rows[0]["patage"].ToString();
            // tbWeight.Text = dt.Rows[0]["patWeight"].ToString();
        }
        private void LodSQFS()//显示
        {
            DataTable dt = new DataTable();
            dt = DAL.Getkcpacuxinxi(ZYNumber1);

            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                //dateTimePicker1.Text = dr["fashishijian"].ToString();

                //            select id,zhuyuanhao,zrl,jtl,jiaoti,xxjiang,zcl,nl,yll,qt,rs,cs,csqx,fangs,fangshiqt,
                //zhentengpeifang,zysx,zysxqt,mazuiys,mzfssys,hushi,riqi
                //from kcpacuxinxi
                //体位
                textBoxzrl.Text = dr["zrl"].ToString();
                textBoxjt.Text = dr["jtl"].ToString();
                textBoxjti.Text = dr["jiaoti"].ToString();
                textBoxxxj.Text = dr["xxjiang"].ToString();
                textBoxzcl.Text = dr["zcl"].ToString();
                textBoxnl.Text = dr["nl"].ToString();
                textBoxyll.Text = dr["yll"].ToString();
                textBoxqt.Text = dr["qt"].ToString();
                textBoxrs.Text = dr["rs"].ToString();
                textBoxchus.Text = dr["cs"].ToString();
                comboBoxcsqx.Text = dr["csqx"].ToString();


                if (Convert.ToString(dr["fangs"]).Contains("1")) checkBoxjm.Checked = true;
                if (Convert.ToString(dr["fangs"]).Contains("2")) checkBoxymw.Checked = true;
                textBoxqta.Text = dr["fangshiqt"].ToString();
                txtSqyy.Controls[0].Text = dr["zhentengpeifang"].ToString();


                if (Convert.ToString(dr["zysx"]).Contains("1")) checkBoxqttcd.Checked = true;
                if (Convert.ToString(dr["zysx"]).Contains("2")) checkBoxhx.Checked = true;
                if (Convert.ToString(dr["zysx"]).Contains("3")) checkBoxsx.Checked = true;
                if (Convert.ToString(dr["zysx"]).Contains("4")) checkBoxex.Checked = true;
                if (Convert.ToString(dr["zysx"]).Contains("5")) checkBoxnzl.Checked = true;
                if (Convert.ToString(dr["zysx"]).Contains("6")) checkBoxgmfy.Checked = true;


                textBoxqzysxt.Text = dr["zysxqt"].ToString();
                comboBoxmzys.Text = dr["mazuiys"].ToString();
                comboBoxfsys.Text = dr["mzfssys"].ToString();
                comboBoxhsq.Text = dr["hushi"].ToString();
                dateTimePicker1.Text = dr["riqi"].ToString();
                if (dr["IsRead"].ToString() == "1")
                {
                    存档ToolStripMenuItem.Enabled = false;
                    SaveToolStripMenuItem.Enabled = false;
                    dataGridView1.Enabled = false;
                }
                this.table1_copy = DAL.Getkcpacuxinxi(ZYNumber1).Copy();

            }
        }
        private void dgvBind()
        {
            DataTable dt = DAL.Getkcpacu(ZYNumber1);
            dataGridView1.DataSource = dt.DefaultView;
        }
        //存档
        private void 存档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataTable dt = DAL.Getkcpacuxinxi(ZYNumber1);
            if (dt.Rows.Count == 0)
                MessageBox.Show("需要先保存，才能存档！");
            else
            {
                result = DAL.Updatakcpacuxinxiv(ZYNumber1, 1);
                if (result > 0)
                {
                    MessageBox.Show("存档成功！");
                    //isRead = true;
                    存档ToolStripMenuItem.Enabled = false;
                    SaveToolStripMenuItem.Enabled = false;
                    dataGridView1.Enabled = false;
                }
            }
        }
        //解锁
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string biaodanneir = "PACU记录单";
            string a = "1";
            string aa = "4";
            DataTable dt1 = DAL.Getteshenhe(ZYNumber1, biaodanneir, Program.customer.user_name);//判断是否提交过
            if (dt1.Rows.Count > 0)
            {
                if (dt1.Rows[0]["leibie"].ToString() == "0")
                {
                    MessageBox.Show("已提交！");
                }
                else if (dt1.Rows[0]["leibie"].ToString() == "1")
                {
                    if (Program.customer.user_name == dt1.Rows[0]["shenqingren"].ToString() && biaodanneir == dt1.Rows[0]["biandanleixing"].ToString() && a == dt1.Rows[0]["leibie"].ToString() && ZYNumber1 == dt1.Rows[0]["zhuyuanhao"].ToString())//登录人,表单,审核
                    {
                        int result = 0;
                        DataTable dt = DAL.Getkcpacuxinxi(ZYNumber1);
                        if (dt.Rows[0]["isRead"].ToString() == "1")
                        {
                            result = DAL.Updatakcpacuxinxiv(ZYNumber1, 0);
                            if (result > 0)
                            {
                                DAL.Updatemkcpacuxinxi修改4(ZYNumber1, aa, biaodanneir);
                                MessageBox.Show("解锁成功！");
                                SaveToolStripMenuItem.Enabled = true;
                                // isRead = false;
                                存档ToolStripMenuItem.Enabled = true;
                                dataGridView1.Enabled = true;
                            }
                        }
                    }

                }
                else if (dt1.Rows[0]["leibie"].ToString() == "2")
                {
                    MessageBox.Show("审核不通过！");
                }
                else
                {
                    MessageBox.Show("已修改过，禁止再次修改！");
                }
            }
            else
            {
                tijiaoshenhe al = new tijiaoshenhe(ZYNumber1, biaodanneir);
                al.Show();
            }

        }
        //退出
        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //特殊事件登记
        private void 特殊事件登记ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wu f1 = new wu(ZYNumber1, tbPatname.Text, dateTimePicker1.Value);
            f1.ShowDialog();
        }

        private void 添加一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime datet;
            int indexs = 0;
            if (dataGridView1.Rows.Count == 0)  //没数据 
            {
                datet = DateTime.Now;
            }
            else
            {
                try
                {
                    string nyr = dataGridView1.SelectedRows[0].Cells["shijian"].Value.ToString();
                    
                    datet = Convert.ToDateTime(nyr).AddSeconds(1);
                    datet = datet.AddMinutes(1);
                    indexs = dataGridView1.SelectedRows[0].Index;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("没有选择行");
                    return;
                }
            }
            string sj = datet.ToShortTimeString().ToString();

            adims_DAL.AdimsProvider.ddzcbdxz(ZYNumber1, sj);
            dataGridView1.Refresh();
            dataGridView1.ClearSelection();
            dgvBind();
            if (dataGridView1.Rows.Count != 1)
            {
                indexs = indexs + 1;
            }
            else indexs = 0;
            dataGridView1.Rows[indexs].Selected = true;
            dataGridView1.FirstDisplayedScrollingRowIndex = indexs;

            //save();
            //if (dataGridView1.Rows.Count == 0)
            //{
            //    DAL.Addkcpacu(ZYNumber1);
            //    dgvBind();
            //}
            //else if (dataGridView1.Rows.Count > 0)
            //{
            //    if (dataGridView1.CurrentCell.IsInEditMode == false)
            //    {
            //        //int g = dataGridView1.Rows.Count;
            //        //int c = 24 * (g + 1);
            //        //string dtTime = Convert.ToString(c) + "h";
            //        DAL.Addkcpacu(ZYNumber1);
            //        dgvBind();
            //    }
            //    else MessageBox.Show("单元格内不能为编辑状态！");
            //}
        }

        private void 删除一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("已清空");
                return;
            }

            string idno = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            DAL.Deletekcpacu(idno);
            dgvBind();
        }

        private void 复制一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = this.dataGridView1.CurrentRow.Index;
            a = dataGridView1.Rows[i].Cells[2].Value.ToString();
            b = dataGridView1.Rows[i].Cells[3].Value.ToString();
            c = dataGridView1.Rows[i].Cells[4].Value.ToString();
            d = dataGridView1.Rows[i].Cells[5].Value.ToString();
            e1 = dataGridView1.Rows[i].Cells[6].Value.ToString();
            f1 = dataGridView1.Rows[i].Cells[7].Value.ToString();
            g = dataGridView1.Rows[i].Cells[8].Value.ToString();
            h = dataGridView1.Rows[i].Cells[9].Value.ToString();
            i1 = dataGridView1.Rows[i].Cells[10].Value.ToString();
            j = dataGridView1.Rows[i].Cells[11].Value.ToString();
            k = dataGridView1.Rows[i].Cells[12].Value.ToString();
            l = dataGridView1.Rows[i].Cells[13].Value.ToString();
            m = dataGridView1.Rows[i].Cells[14].Value.ToString();
            n = dataGridView1.Rows[i].Cells[15].Value.ToString();
            o = dataGridView1.Rows[i].Cells[16].Value.ToString();
            p = dataGridView1.Rows[i].Cells[17].Value.ToString();
            q = dataGridView1.Rows[i].Cells[18].Value.ToString();
            r = dataGridView1.Rows[i].Cells[19].Value.ToString();
        }

        private void 粘贴一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = this.dataGridView1.CurrentRow.Index;
            dataGridView1.Rows[i].Cells[2].Value = a;
            dataGridView1.Rows[i].Cells[3].Value = b;
            dataGridView1.Rows[i].Cells[4].Value = c;
            dataGridView1.Rows[i].Cells[5].Value = d;
            dataGridView1.Rows[i].Cells[6].Value = e1;
            dataGridView1.Rows[i].Cells[7].Value = f1;
            dataGridView1.Rows[i].Cells[8].Value = g;
            dataGridView1.Rows[i].Cells[9].Value = h;
            dataGridView1.Rows[i].Cells[10].Value = i1;
            dataGridView1.Rows[i].Cells[11].Value = j;
            dataGridView1.Rows[i].Cells[12].Value = k;
            dataGridView1.Rows[i].Cells[13].Value = l;
            dataGridView1.Rows[i].Cells[14].Value = m;
            dataGridView1.Rows[i].Cells[15].Value = n;
            dataGridView1.Rows[i].Cells[16].Value = o;
            dataGridView1.Rows[i].Cells[17].Value = p;
            dataGridView1.Rows[i].Cells[18].Value = q;
            dataGridView1.Rows[i].Cells[19].Value = r;
        }
        //打印
        private void printStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tbPatname.Text == "")
            {
                MessageBox.Show("姓名为空禁止打印!");
                tbPatname.Focus();
                return;
            }
            if (tbSex.Text == "")
            {
                MessageBox.Show("性别为空禁止打印！");
                tbSex.Focus();
                return;
            }
            if (tbAge.Text == "")
            {
                MessageBox.Show("年龄为空禁止打印！");
                tbAge.Focus();
                return;
            }
            if (tbBedNO.Text == "")
            {
                MessageBox.Show("床号为空禁止打印！");
                tbBedNO.Focus();
                return;

            }
            if (tbZhuyuanID.Text == "")
            {
                MessageBox.Show("住院号为空禁止打印！");
                tbZhuyuanID.Focus();
                return;
            }
            if (comboBoxmzys.Text == "")
            {
                MessageBox.Show("麻醉科医生为空禁止打印！");
                comboBoxmzys.Focus();
                return;
            }
            this.printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font zt8 = new Font("宋体", 8);
            Font zt1 = new Font("宋体", 9);
            Font zt9 = new Font("宋体", 9); //字体
            Font ht13 = new Font("黑体", 13);
            Font ht18 = new Font("黑体", 18);
            Pen pblue = new Pen(Brushes.Blue);
            pblue.Width = 2;
            Pen ptp = Pens.Gray;//下划线画笔
            Pen pb1 = new Pen(Brushes.Black);  //普通画笔
            int x = 60, y = 0, y1 = 0; //整体位置
            y = y + 40;
            string title1 = "浙江省金华市中医医院";

            string title2 = "麻  醉  复  苏  记  录  单";
            string title3 = "（此单为麻醉记录单附页、性别、诊断、手术名称详见麻醉记录单）";
            e.Graphics.DrawString(title1, ht13, Brushes.Black, x + 230, y);
            y = y + 30;
            e.Graphics.DrawString(title2, ht18, Brushes.Black, x + 180, y);
            y = y + 30;
            e.Graphics.DrawString(title3, zt1, Brushes.Black, x + 160, y);

            y = y + 45;
            e.Graphics.DrawString("姓名:" + tbPatname.Text, zt9, Brushes.Black, new Point(x + 10, y));
            //e.Graphics.DrawString("性别:" + tbSex.Text, zt9, Brushes.Black, new Point(x + 200, y));
            e.Graphics.DrawString("年龄:" + tbAge.Text, zt9, Brushes.Black, new Point(x + 260, y));
            e.Graphics.DrawString("床号:" + tbBedNO.Text, zt9, Brushes.Black, new Point(x + 360, y));
            e.Graphics.DrawString("住院号:" + tbZhuyuanID.Text, zt9, Brushes.Black, new Point(x + 550, y));
            y = y + 20;
            e.Graphics.DrawString("麻醉方法：" + textBoxmzfangfa.Text, zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawLine(pb1, x, y, x + 670, y);
            e.Graphics.DrawString("一  般  记  录", zt9, Brushes.Black, new Point(x + 40, y + 5));
            e.Graphics.DrawString("特  殊  记  录", zt9, Brushes.Black, new Point(x + 350, y + 5));
            y = y + 25;
            e.Graphics.DrawLine(pb1, x, y, x + 670, y);



            e.Graphics.DrawString("时\n间", zt9, Brushes.Black, new Point(x + 10, y + 15));
            e.Graphics.DrawLine(pb1, x + 40, y, x + 40, y + 585);
            e.Graphics.DrawString("体\n温", zt9, Brushes.Black, new Point(x + 45, y + 15));
            e.Graphics.DrawLine(pb1, x + 70, y, x + 70, y + 585);
            e.Graphics.DrawString("意\n识", zt9, Brushes.Black, new Point(x + 75, y + 15));
            e.Graphics.DrawLine(pb1, x + 110, y, x + 110, y + 585);
            e.Graphics.DrawString("  BP\n(mmHg)", zt9, Brushes.Black, new Point(x + 105, y + 15));
            e.Graphics.DrawLine(pb1, x + 140, y, x + 140, y + 585);
            e.Graphics.DrawString(" HR\n(bmp)", zt9, Brushes.Black, x + 135, y + 15);
            e.Graphics.DrawLine(pb1, x + 170, y, x + 170, y + 585);
            e.Graphics.DrawString(" RR\n(bmp)", zt9, Brushes.Black, x + 165, y + 15);
            e.Graphics.DrawLine(pb1, x + 200, y, x + 200, y + 585);
            e.Graphics.DrawString(" 吸氧\n(L/分)", zt9, Brushes.Black, x + 195, y + 15);
            e.Graphics.DrawLine(pb1, x + 230, y, x + 230, y + 585);
            e.Graphics.DrawString("SpO2\n (%)", zt9, Brushes.Black, x + 235, y + 15);
            e.Graphics.DrawLine(pb1, x + 260, y - 25, x + 260, y + 585);
            e.Graphics.DrawString(" 人工呼吸  ", zt9, Brushes.Black, x + 285, y);
            e.Graphics.DrawLine(pb1, x + 290, y + 15, x + 290, y + 585);
            e.Graphics.DrawString("Vt", zt9, Brushes.Black, x + 265, y + 25);
            e.Graphics.DrawLine(pb1, x + 320, y + 15, x + 320, y + 585);
            e.Graphics.DrawString(" f", zt9, Brushes.Black, x + 285, y + 25);
            e.Graphics.DrawLine(pb1, x + 350, y, x + 350, y + 585);
            e.Graphics.DrawString("FiO2", zt9, Brushes.Black, x + 320, y + 25);
            e.Graphics.DrawLine(pb1, x + 380, y, x + 380, y + 585);
            e.Graphics.DrawString("吸\n痰", zt9, Brushes.Black, x + 350, y + 15);
            e.Graphics.DrawLine(pb1, x + 410, y +15, x + 410, y + 585);
            e.Graphics.DrawString("  瞳孔  ", zt9, Brushes.Black, x + 380, y);
            e.Graphics.DrawLine(pb1, x + 450, y, x + 450, y + 585);
            e.Graphics.DrawString("大\n小", zt9, Brushes.Black, x + 380, y + 15);
            e.Graphics.DrawLine(pb1, x + 490, y, x + 490, y + 585);
            e.Graphics.DrawString("对光\n反应", zt9, Brushes.Black, x + 410, y + 15);
            e.Graphics.DrawLine(pb1, x + 530, y, x + 530, y + 585);
            e.Graphics.DrawString("镇\n静\n评\n分", zt9, Brushes.Black, x + 450, y);
            e.Graphics.DrawLine(pb1, x + 570, y, x + 570, y + 585);
            e.Graphics.DrawString("镇\n痛\n评\n分", zt9, Brushes.Black, x + 490, y);
            e.Graphics.DrawLine(pb1, x + 260, y + 15, x + 350, y + 15);
            e.Graphics.DrawString("膀\n胱\n冲\n洗", zt9, Brushes.Black, x + 530, y);
            e.Graphics.DrawLine(pb1, x + 380, y + 15, x + 450, y + 15);
            e.Graphics.DrawString("病情与处理记录", zt9, Brushes.Black, x + 570, y + 15);


            y = y + 60;
            e.Graphics.DrawLine(pb1, new Point(x, y), new Point(x + 670, y));
            int flag = 0;
            int row = 0;
            int count = Convert.ToInt32(dataGridView1.Rows.Count);
            for (int i = row; i < count; i++)
            {
                flag++;
                for (int j = 0; j < 18; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                        dataGridView1.Rows[i].Cells[j].Value = "";

                }
                e.Graphics.DrawLine(pb1, new Point(x + 30, y), new Point(x + 660, y));
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value.ToString(), zt9, Brushes.Black, x, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value.ToString(), zt8, Brushes.Black, x + 40, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value.ToString(), zt9, Brushes.Black, x + 70, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[5].Value.ToString(), zt9, Brushes.Black, x + 110, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[6].Value.ToString(), zt9, Brushes.Black, x + 140, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[7].Value.ToString(), zt9, Brushes.Black, x + 170, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[8].Value.ToString(), zt9, Brushes.Black, x + 200, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[9].Value.ToString(), zt9, Brushes.Black, x + 230, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[10].Value.ToString(), zt9, Brushes.Black, x + 260, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[11].Value.ToString(), zt9, Brushes.Black, x + 290, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[12].Value.ToString(), zt9, Brushes.Black, x + 320, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[13].Value.ToString(), zt9, Brushes.Black, x + 350, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[14].Value.ToString(), zt9, Brushes.Black, x + 380, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[15].Value.ToString(), zt9, Brushes.Black, x + 410, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[16].Value.ToString(), zt9, Brushes.Black, x + 450, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[17].Value.ToString(), zt9, Brushes.Black, x + 490, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[18].Value.ToString(), zt9, Brushes.Black, x + 530, y - 20 + flag * 35);
                e.Graphics.DrawString(dataGridView1.Rows[i].Cells[19].Value.ToString(), zt9, Brushes.Black, x + 570, y - 20 + flag * 35);


            }
            for (int j = 0; j < 15; j++)
            {
                e.Graphics.DrawLine(Pens.Black, x, y + 35 * (j + 1), x + 670, y + 35 * (j + 1));
            }

            y = y + 605;
            string z, c, v, b, n, m, q, w;
            if (textBoxzrl.Text == "")
            {
                z = "/";
            }
            else
            {
                z = textBoxzrl.Text;
            }
            if (textBoxjt.Text == "")
            {
                c = "/";
            }
            else
            {
                c = textBoxjt.Text;
            }
            if (textBoxjti.Text == "")
            {
                v = "/";
            }
            else
            {
                v = textBoxjti.Text;
            }
            if (textBoxxxj.Text == "")
            {
                b = "/";
            }
            else
            {
                b = textBoxxxj.Text;
            }
            if (textBoxzcl.Text == "")
            {
                n = "/";
            }
            else
            {
                n = textBoxzcl.Text;
            }
            if (textBoxnl.Text == "")
            {
                m = "/";
            }
            else
            {
                m = textBoxnl.Text;
            }
            if (textBoxqt.Text == "")
            {
                q = "/";
            }
            else
            {
                q = textBoxqt.Text;
            }
            if (textBoxyll.Text == "")
            {
                w = "/";
            }
            else
            {
                w = textBoxyll.Text;
            }
            e.Graphics.DrawString("小结:总入量 " + z + " ml     其中:晶体  " + c + " ml  胶体 " + v + " ml    血/血浆 " + b + " ml", zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("     总出量 " + n + " ml     其中:尿量  " + m + " ml     引流量 " + w + " ml   其    它 " + q + " ml", zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("麻醉复苏室评分：入室  " + textBoxrs.Text + " 分     出室  " + textBoxchus.Text + "分             出室后去向： " + comboBoxcsqx.Text, zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 30;
            e.Graphics.DrawString("术后镇痛：方式：", zt9, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 150, y, 10, 10);
            if (checkBoxjm.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 150, y, x + 155, y + 10);
                e.Graphics.DrawLine(pb1, x + 155, y + 10, x + 160, y);
            }
            e.Graphics.DrawString("静脉", zt9, Brushes.Black, new Point(x + 160, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 200, y, 10, 10);
            if (checkBoxymw.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 200, y, x + 205, y + 10);
                e.Graphics.DrawLine(pb1, x + 205, y + 10, x + 210, y);
            }
            e.Graphics.DrawString("硬膜外", zt9, Brushes.Black, new Point(x + 210, y));

            e.Graphics.DrawString("其他：" + textBoxqta.Text, zt9, Brushes.Black, new Point(x + 270, y));
            y = y + 20;
            e.Graphics.DrawString("镇痛配方：" + txtSqyy.Controls[0].Text, zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("注意事项：", zt9, Brushes.Black, new Point(x + 10, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 80, y, 10, 10);
            if (checkBoxqttcd.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 80, y, x + 85, y + 10);
                e.Graphics.DrawLine(pb1, x + 85, y + 10, x + 90, y);
            }
            e.Graphics.DrawString("气道通畅", zt9, Brushes.Black, new Point(x + 90, y));

            e.Graphics.DrawRectangle(Pens.Black, x + 170, y, 10, 10);
            if (checkBoxhx.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 170, y, x + 175, y + 10);
                e.Graphics.DrawLine(pb1, x + 175, y + 10, x + 180, y);
            }
            e.Graphics.DrawString("呼吸循环抑制", zt9, Brushes.Black, new Point(x + 180, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 290, y, 10, 10);
            if (checkBoxsx.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 290, y, x + 295, y + 10);
                e.Graphics.DrawLine(pb1, x + 295, y + 10, x + 300, y);
            }
            e.Graphics.DrawString("苏醒迟缓", zt9, Brushes.Black, new Point(x + 300, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 370, y, 10, 10);
            if (checkBoxex.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 370, y, x + 375, y + 10);
                e.Graphics.DrawLine(pb1, x + 375, y + 10, x + 380, y);
            }
            e.Graphics.DrawString("恶心呕吐", zt9, Brushes.Black, new Point(x + 380, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 450, y, 10, 10);
            if (checkBoxnzl.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 450, y, x + 455, y + 10);
                e.Graphics.DrawLine(pb1, x + 455, y + 10, x + 460, y);
            }
            e.Graphics.DrawString("尿潴留", zt9, Brushes.Black, new Point(x + 460, y));
            e.Graphics.DrawRectangle(Pens.Black, x + 510, y, 10, 10);
            if (checkBoxgmfy.Checked == true)
            {
                e.Graphics.DrawLine(pb1, x + 510, y, x + 515, y + 10);
                e.Graphics.DrawLine(pb1, x + 515, y + 10, x + 520, y);
            }
            e.Graphics.DrawString("过敏反应", zt9, Brushes.Black, new Point(x + 520, y));
            // e.Graphics.DrawString("注意事项：1.气道通畅2.呼吸循环抑制3.苏醒迟缓4.恶心呕吐5.尿潴留6.过敏反应7其他：" + textBoxqzysxt.Text, zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 20;
            e.Graphics.DrawString("其他：" + textBoxqzysxt.Text, zt9, Brushes.Black, new Point(x + 10, y));
            y = y + 20;

            e.Graphics.DrawString("麻醉医师签名" + comboBoxmzys.Text, zt9, Brushes.Black, new Point(x + 10, y));
            //string asd = comboBoxmzys.Text.Trim();
            //if (asd != "")
            //{
            //    try
            //    {
            //        Image temp = new Bitmap(Application.StartupPath + "\\手麻科6\\" + asd + ".gif");
            //        //Graphics g = Graphics.FromImage(temp);
            //        int width = temp.Width;
            //        int height = temp.Height;
            //        Rectangle destRect = new Rectangle(x + 90, y, 200, 40);
            //        e.Graphics.DrawImage(temp, destRect, 0, 0, temp.Width, temp.Height, System.Drawing.GraphicsUnit.Pixel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("图片不在本地中");
            //        return;
            //    }
            //}
            e.Graphics.DrawString("麻醉复苏室医师签名" + comboBoxfsys.Text, zt9, Brushes.Black, new Point(x + 260, y));
            //string asdv = comboBoxfsys.Text.Trim();
            //if (asdv != "")
            //{
            //    try
            //    {
            //        Image temp1 = new Bitmap(Application.StartupPath + "\\手麻科6\\" + asdv + ".gif");
            //        //Graphics g = Graphics.FromImage(temp);
            //        int width = temp1.Width;
            //        int height = temp1.Height;
            //        Rectangle destRect = new Rectangle(x + 370, y, 200, 40);
            //        e.Graphics.DrawImage(temp1, destRect, 0, 0, temp1.Width, temp1.Height, System.Drawing.GraphicsUnit.Pixel);
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("图片不在本地中");
            //        return;
            //    }
            //}
            e.Graphics.DrawString("护士签名" + comboBoxhsq.Text, zt9, Brushes.Black, new Point(x + 530, y));
            y = y + 40;
            e.Graphics.DrawString("日期" + dateTimePicker1.Value.Date.ToString("yyyy年MM月dd日"), zt9, Brushes.Black, new Point(x + 400, y));
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 1.2;
            printPreviewDialog1.WindowState = FormWindowState.Maximized;
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 820, 1120);
        }
        //窗体关闭事件
        private void JHmzhfjld_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBoxButtons messButton = MessageBoxButtons.YesNo;
            //DialogResult dr = MessageBox.Show("是否保存", "退出系统", messButton);

            //if (dr == DialogResult.Yes)//如果点击“确定”按钮
            //{
            //}
            //else//如果点击“取消”按钮
            //{
            //    this.FormClosing -= new FormClosingEventHandler(this.JHmzhfjld_FormClosing);
            //    e.Cancel = true;
            //    return;
            //}
        }

        

        private void textBoxrs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pingfenbiaozhun form = new pingfenbiaozhun();
            form.ShowDialog();
            textBoxrs.Text = form.count.ToString();
        }

        private void textBoxchus_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pingfenbiaozhun form = new pingfenbiaozhun();
            form.ShowDialog();
            textBoxchus.Text = form.count.ToString();
        }

        private void textBoxchus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                string valueType = dataGridView1.Columns[e.ColumnIndex].Name.ToString();
                string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                adims_DAL.AdimsProvider.djtzfs(valueType, value, id);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > -1 && e.ColumnIndex > 0)
            //{
                
            //        if (e.ColumnIndex == 2)
            //        {
            //            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
            //            {
            //                string valueType = dataGridView1.Columns[e.ColumnIndex].Name.ToString();
            //                DateTime dt = DateTime.Now;
            //                string sj = dt.GetDateTimeFormats('g')[0].ToString();//13:21
            //                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = sj;
            //                string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
            //                adims_DAL.AdimsProvider.djtzfs(valueType, value, id);
            //            }
            //        }
                
            //}
        }


        

        
    }
}
