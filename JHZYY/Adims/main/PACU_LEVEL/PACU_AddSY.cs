using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.PACU_LEVEL
{
    public partial class PACU_AddSY : Form
    {

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();       
        int mzjldid,RowIndex,ColumIndex;
        string ID, RoomName, Value_Info;
        public PACU_AddSY(int mzid, string id, string ziduanNAME, string value)
        {
            RoomName = ziduanNAME; ID = id;
            mzjldid = mzid; Value_Info = value;
            InitializeComponent();
        }
        adims_DAL.PACU_DAL dal = new adims_DAL.PACU_DAL();
        private void BindYaoPin()
        {
            DataTable dt = dal.GetAdims_YaoPinByType("输液", cmbName.Text.Trim());
            this.cmbName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbName.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        private void BindShuyeList()
        {
            DataTable dtSX = bll.selectSY_pacu(mzjldid);
            dataGridView1.DataSource = dtSX.DefaultView;
           
        }
        private void PACU_AddSY_Load(object sender, EventArgs e)
        {
            BindYaoPin();
            BindShuyeList();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            adims_BLL.DataValid.Text_Value_Limit(sender,e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                adims_MODEL.shuye jtytsx = new adims_MODEL.shuye();
                jtytsx.Bz = 1;
                jtytsx.Name = cmbName.Text;
                jtytsx.Jl = Convert.ToInt32(textBox1.Text);
                jtytsx.Dw = cmbDW.Text;
                jtytsx.Zrfs = cmbZRFS.Text;
                jtytsx.Kssj = DateTime.Now;
                int q = bll.addshuyePACU(mzjldid, jtytsx);
                if (Value_Info.IsNullOrEmpty())
                    Value_Info = jtytsx.Name + jtytsx.Jl.ToString() + jtytsx.Dw + jtytsx.Zrfs;
                else
                    Value_Info = Value_Info+"+"+jtytsx.Name + jtytsx.Jl.ToString() + jtytsx.Dw + jtytsx.Zrfs;
             
                
                bll.UpdatePACU_Data(ID, RoomName, Value_Info);
                if (q > 0)
                    BindShuyeList();
                else
                    MessageBox.Show("添加失败，请重试。");
            }
            else
            {
                MessageBox.Show("剂量不能为空！");
            }
        }
      

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int q = bll.delshuyePACU(mzjldid, Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                MessageBox.Show("删除成功！");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
