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
    public partial class wu : Form
    {
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string zyh = "";
        string name1 = "";
        DateTime? shiian1;
        public wu(string patID, string name, DateTime shijian)
        {
            zyh = patID;
            name1 = name;
            shiian1 = shijian;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> tsqk = new Dictionary<string, string>();
            int result = 0;
            try
            {

                string AddItem = "";
                tsqk.Add("zhuyuanhao", zyhao.Text);
                tsqk.Add("xingmingname", brname.Text);
                tsqk.Add("shiian", Convert.ToDateTime(dateme.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
                AddItem = "";
                if (fswyqysza.Checked) AddItem += "1";
                tsqk.Add("fswyqysza", AddItem);
                AddItem = "";
                if (cxybhdd.Checked) AddItem += "1";
                tsqk.Add("cxybhdjd", AddItem);
                AddItem = "";
                if (qsmzsycxyw.Checked) AddItem += "1";
                tsqk.Add("qsmzycxy", AddItem);
                AddItem = "";
                if (wywxhxdgz.Checked) AddItem += "1";
                tsqk.Add("ywxwyyfhxdz", AddItem);
                AddItem = "";
                if (mzywsw.Checked) AddItem += "1";
                tsqk.Add("mzywsw", AddItem);
                AddItem = "";
                if (qtfyqsj.Checked) AddItem += "1";
                tsqk.Add("qtfyqsj", AddItem);

                AddItem = "";
                if (shsw1.Checked) AddItem += "1";
                tsqk.Add("shsw1", AddItem);
                AddItem = "";
                if (shsw2.Checked) AddItem += "1";
                tsqk.Add("shsw2", AddItem);
                AddItem = "";
                if (shsw3.Checked) AddItem += "1";
                tsqk.Add("shsw3", AddItem);
                AddItem = "";
                if (shsw4.Checked) AddItem += "1";
                tsqk.Add("shsw4", AddItem);
                AddItem = "";
                if (shsw5.Checked) AddItem += "1";
                tsqk.Add("shsw5", AddItem);
                AddItem = "";
                if (mzkssswks.Checked) AddItem += "1";
                tsqk.Add("mzkssswks", AddItem);
                AddItem = "";
                if (rpacucg3xs.Checked) AddItem += "1";
                tsqk.Add("rpacu3xs", AddItem);

                AddItem = "";
                if (pacudtw.Checked) AddItem += "1";
                tsqk.Add("pacudtw", AddItem);
                AddItem = "";
                if (fjheccg.Checked) AddItem += "1";
                tsqk.Add("fjheccg", AddItem);
                AddItem = "";
                if (mz24xssw.Checked) AddItem += "1";
                tsqk.Add("mz24xssw", AddItem);
                AddItem = "";
                if (mz24xsxtzt.Checked) AddItem += "1";
                tsqk.Add("mz24xsxzzt", AddItem);
                AddItem = "";
                if (mzqjgmfy.Checked) AddItem += "1";
                tsqk.Add("mzqgmfy", AddItem);
                AddItem = "";
                if (zgnmzyzbfz.Checked) AddItem += "1";
                tsqk.Add("zgnmzhyzsjbfz", AddItem);
                AddItem = "";
                if (szzxjmccyzbfz.Checked) AddItem += "1";
                tsqk.Add("szzxjmccyzbfz", AddItem);
                AddItem = "";
                if (mzhxfhm.Checked) AddItem += "1";
                tsqk.Add("mzxfhm", AddItem);
                AddItem = "";
                if (fjhzricu.Checked) AddItem += "1";
                tsqk.Add("fjhzricu", AddItem);
                AddItem = "";
                if (qmcghsysy.Checked) AddItem += "1";
                tsqk.Add("qmcghsysy", AddItem);
                AddItem = "";
                if (checkBoxwu.Checked) AddItem += "1";
                tsqk.Add("wu", AddItem);
                DataTable dt = DAL.gettsqkdj(zyhao.Text);
                if (dt.Rows.Count > 0)
                    result = DAL.UpdatemaTsmztj(tsqk);
                else
                    result = DAL.InsertTsmztj(tsqk);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");

                }
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        private void wu_Load(object sender, EventArgs e)
        {
            zyhao.Text = zyh;
            brname.Text = name1;
            dateme.Value = shiian1 == null ? DateTime.Now : (DateTime)shiian1;
            xianshi();
        }
        private void xianshi()
        {

            DataTable dt = new DataTable();
            dt = DAL.gettsqkdj(zyhao.Text);
            if (dt.Rows.Count > 0)
            {

                DataRow dr = dt.Rows[0];
                dateme.Text = dr["shiian"].ToString();
                if (Convert.ToString(dr["fswyqysza"]).Contains("1")) fswyqysza.Checked = true;
                if (Convert.ToString(dr["cxybhdjd"]).Contains("1")) cxybhdd.Checked = true;
                if (Convert.ToString(dr["qsmzycxy"]).Contains("1")) qsmzsycxyw.Checked = true;
                if (Convert.ToString(dr["ywxwyyfhxdz"]).Contains("1")) wywxhxdgz.Checked = true;
                if (Convert.ToString(dr["mzywsw"]).Contains("1")) mzywsw.Checked = true;
                if (Convert.ToString(dr["qtfyqsj"]).Contains("1")) qtfyqsj.Checked = true;

                if (Convert.ToString(dr["shsw1"]).Contains("1")) shsw1.Checked = true;
                if (Convert.ToString(dr["shsw2"]).Contains("1")) shsw2.Checked = true;
                if (Convert.ToString(dr["shsw3"]).Contains("1")) shsw3.Checked = true;
                if (Convert.ToString(dr["shsw4"]).Contains("1")) shsw4.Checked = true;
                if (Convert.ToString(dr["shsw5"]).Contains("1")) shsw5.Checked = true;
                if (Convert.ToString(dr["mzkssswks"]).Contains("1")) mzkssswks.Checked = true;
                if (Convert.ToString(dr["rpacu3xs"]).Contains("1")) rpacucg3xs.Checked = true;
                if (Convert.ToString(dr["pacudtw"]).Contains("1")) pacudtw.Checked = true;
                if (Convert.ToString(dr["fjheccg"]).Contains("1")) fjheccg.Checked = true;
                if (Convert.ToString(dr["mz24xssw"]).Contains("1")) mz24xssw.Checked = true;
                if (Convert.ToString(dr["mz24xsxzzt"]).Contains("1")) mz24xsxtzt.Checked = true;
                if (Convert.ToString(dr["mzqgmfy"]).Contains("1")) mzqjgmfy.Checked = true;
                if (Convert.ToString(dr["zgnmzhyzsjbfz"]).Contains("1")) zgnmzyzbfz.Checked = true;
                if (Convert.ToString(dr["szzxjmccyzbfz"]).Contains("1")) szzxjmccyzbfz.Checked = true;
                if (Convert.ToString(dr["mzxfhm"]).Contains("1")) mzhxfhm.Checked = true;

                if (Convert.ToString(dr["fjhzricu"]).Contains("1")) fjhzricu.Checked = true;
                if (Convert.ToString(dr["qmcghsysy"]).Contains("1")) qmcghsysy.Checked = true;

                if (Convert.ToString(dr["wu"]).Contains("1")) checkBoxwu.Checked = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Dictionary<string, string> tsqk = new Dictionary<string, string>();
            int result = 0;
            try
            {

                string AddItem = "";
                tsqk.Add("zhuyuanhao", zyhao.Text);
                tsqk.Add("xingmingname", brname.Text);
                tsqk.Add("shiian", Convert.ToDateTime(dateme.Value.ToString()).ToString("yyyy-MM-dd HH:mm"));
                AddItem = "";
                if (fswyqysza.Checked) AddItem += "1";
                tsqk.Add("fswyqysza", AddItem);
                AddItem = "";
                if (cxybhdd.Checked) AddItem += "1";
                tsqk.Add("cxybhdjd", AddItem);
                AddItem = "";
                if (qsmzsycxyw.Checked) AddItem += "1";
                tsqk.Add("qsmzycxy", AddItem);
                AddItem = "";
                if (wywxhxdgz.Checked) AddItem += "1";
                tsqk.Add("ywxwyyfhxdz", AddItem);
                AddItem = "";
                if (mzywsw.Checked) AddItem += "1";
                tsqk.Add("mzywsw", AddItem);
                AddItem = "";
                if (qtfyqsj.Checked) AddItem += "1";
                tsqk.Add("qtfyqsj", AddItem);

                AddItem = "";
                if (shsw1.Checked) AddItem += "1";
                tsqk.Add("shsw1", AddItem);
                AddItem = "";
                if (shsw2.Checked) AddItem += "1";
                tsqk.Add("shsw2", AddItem);
                AddItem = "";
                if (shsw3.Checked) AddItem += "1";
                tsqk.Add("shsw3", AddItem);
                AddItem = "";
                if (shsw4.Checked) AddItem += "1";
                tsqk.Add("shsw4", AddItem);
                AddItem = "";
                if (shsw5.Checked) AddItem += "1";
                tsqk.Add("shsw5", AddItem);
                AddItem = "";
                if (mzkssswks.Checked) AddItem += "1";
                tsqk.Add("mzkssswks", AddItem);
                AddItem = "";
                if (rpacucg3xs.Checked) AddItem += "1";
                tsqk.Add("rpacu3xs", AddItem);

                AddItem = "";
                if (pacudtw.Checked) AddItem += "1";
                tsqk.Add("pacudtw", AddItem);
                AddItem = "";
                if (fjheccg.Checked) AddItem += "1";
                tsqk.Add("fjheccg", AddItem);
                AddItem = "";
                if (mz24xssw.Checked) AddItem += "1";
                tsqk.Add("mz24xssw", AddItem);
                AddItem = "";
                if (mz24xsxtzt.Checked) AddItem += "1";
                tsqk.Add("mz24xsxzzt", AddItem);
                AddItem = "";
                if (mzqjgmfy.Checked) AddItem += "1";
                tsqk.Add("mzqgmfy", AddItem);
                AddItem = "";
                if (zgnmzyzbfz.Checked) AddItem += "1";
                tsqk.Add("zgnmzhyzsjbfz", AddItem);
                AddItem = "";
                if (szzxjmccyzbfz.Checked) AddItem += "1";
                tsqk.Add("szzxjmccyzbfz", AddItem);
                AddItem = "";
                if (mzhxfhm.Checked) AddItem += "1";
                tsqk.Add("mzxfhm", AddItem);
                AddItem = "";
                if (fjhzricu.Checked) AddItem += "1";
                tsqk.Add("fjhzricu", AddItem);
                AddItem = "";
                if (qmcghsysy.Checked) AddItem += "1";
                tsqk.Add("qmcghsysy", AddItem);
                AddItem = "";
                if (checkBoxwu.Checked) AddItem += "1";
                tsqk.Add("wu", AddItem);
                DataTable dt = DAL.gettsqkdj(zyhao.Text);
                if (dt.Rows.Count > 0)
                    result = DAL.UpdatemaTsmztj(tsqk);
                else
                    result = DAL.InsertTsmztj(tsqk);
                if (result > 0)
                {
                    MessageBox.Show("保存成功！");

                }
                else
                    MessageBox.Show("保存失败！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "保存出现异常，请检查！");
            }
        }

        private void exitStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
