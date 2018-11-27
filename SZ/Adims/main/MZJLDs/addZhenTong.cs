using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using System.Collections;

namespace main
{
    public partial class addZhenTong : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        private List<adims_MODEL.ZhenTongYao> ztyList = new List<adims_MODEL.ZhenTongYao>();//镇痛药
        int mzjldid;

        ArrayList sss = new ArrayList();
        public addZhenTong(int mzjldid1)
        {
            mzjldid = mzjldid1;
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
                tbName.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text != "" && tbYL.Text != "")
            {

                adims_MODEL.ZhenTongYao ZTY = new adims_MODEL.ZhenTongYao();
                ZTY.Name = tbName.Text.Trim();
                ZTY.Yl = Convert.ToDouble(tbYL.Text.Trim());
                ZTY.Dw = cmbDW.Text.Trim();
                //DataTable dt = bll.getMZZTY(mzjldid);
                //if (dt.Rows.Count==0)
                //{
                int m = bll.addMZZTY(mzjldid, ZTY);
                if (m != 1)
                    MessageBox.Show("添加失败");
                else
                {
                    //DataTable dt1 = bll.getMZZTY(mzjldid);
                    //ZTY.Id = Convert.ToInt32(dt1.Rows[0]["id"]);
                    //ztyList.Add(ZTY);
                    datagridBind();
                }
                //}
                //else
                //    MessageBox.Show(ZTY.Name+"已存在，不能重复添加");

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedCells.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                    int i = bll.deleteMZZTY(ID);
                    datagridBind();
                    if (i > 0)
                    {
                        datagridBind();
                    }
                }
                else
                    MessageBox.Show("请选择要删除的镇痛药！");
            }
        }
        private void datagridBind()
        {
           
            DataTable dt = bll.getMZZTY(mzjldid);
            if (dt.Rows.Count >=0)
            {
                dataGridView1.DataSource = dt;
            }
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("镇痛药");
            this.listBox1.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listBox1.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void addZhenTong_Load(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            DataTable dt = dal.GetAdims_ALLBagName();//绑定用药包
            cmbYB.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbYB.Items.Add(dt.Rows[i][0]);
            }
            BindYaoPin();
            datagridBind();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = dal.GetYongYaoBag(this.cmbYB.Text.Trim());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sss.Count < 11 || (sss.Count == 11 && sss.Contains(dt.Rows[i]["ypname"].ToString())))
                {
                    int m = 0;
                    adims_MODEL.mzyt yt1 = new adims_MODEL.mzyt();
                    yt1.Ytname = dt.Rows[i]["ypname"].ToString();
                    yt1.Yl = Convert.ToDouble(dt.Rows[i]["yl"]);
                    yt1.Dw = Convert.ToString(dt.Rows[i]["dw"]);
                    m = bll.addZTB(mzjldid, yt1);
                    if (m > 0 && !sss.Contains(yt1.Ytname))
                    {
                        sss.Add(yt1.Ytname);
                        datagridBind();
                    }
                }
                else
                {
                    MessageBox.Show("镇痛药标记数超标," + dt.Rows[i]["ypname"].ToString() + " 未添加成功！");
                }
            }
        }
    }
}
