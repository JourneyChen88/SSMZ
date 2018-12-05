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
using adims_Utility;

namespace main
{
    public partial class addTsyy : Form
    {
        #region <<Members>>
        YongyaoListDal _YongyaoListDal = new YongyaoListDal();
        int mzjldid;
        List<adims_MODEL.tsyy> tsyy;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        AdimsProvider apro = new AdimsProvider();

        PacuDal Pdal = new PacuDal();

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="t"></param>
        public addTsyy(List<adims_MODEL.tsyy> tsyy1,int id)
        {
            mzjldid = id;
            tsyy = tsyy1;
            InitializeComponent();
          
        }
        public addTsyy(int id)
        {
            mzjldid = id;           
            InitializeComponent();
        }
       
        #region <<Events>>

        

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listYaopin.SelectedIndex != -1)
                tbName.Text = listYaopin.SelectedItem.ToString();           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)//添加
        {
            if (tbName.Text.Trim() != "" && tbYl.Text.Trim() != "")
           {
                adims_MODEL.Yongyao ts = new adims_MODEL.Yongyao();
                ts.YpType = 6;
                ts.KsTime = DateTime.Now;
                ts.JsTime = ts.KsTime; 
                ts.Name = tbName.Text.Trim();
                ts.Yl = Convert.ToDouble(tbYl.Text.Trim());
                ts.Dw = cmbDW1.Text.Trim();
                ts.Yyfs = cmbYYFS1.Text.Trim();
                ts.Y_zb = 370;
                int m = bll.addYongyaoList2(mzjldid, ts);
                if (m > 0) datagridBind();
            }
           else
               MessageBox.Show("药品名和用量不能为空！"); 
        }
        private void button3_Click(object sender, EventArgs e)//删除
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("确定删除?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);
                    int j = bll.deleteYaopinList(mzjldid,ID);
                    if (j > 0) datagridBind();
                }              
            }
            else
                MessageBox.Show("请选择已使用的特殊用药！"); 
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender, e); 
        }

        #endregion

        private void datagridBind()
        {
            
            DataTable dt = _YongyaoListDal.GetYongyaoList(mzjldid, (int)EnumCreator.YongyaoType.其他用药);//6其他用药
            dataGridView1.DataSource = dt.DefaultView;
        }
        adims_DAL.YaopinDal _YaopinDal = new adims_DAL.YaopinDal();
        private void BindYaoPin()
        {
            DataTable dt = _YaopinDal.GetYaoPinByPinyin(tbPinyin.Text.Trim());
            this.listYaopin.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                listYaopin.Items.Add(dt.Rows[i]["ypname"]);
            }
        }
        /// <summary>
        /// 单位
        /// </summary>
        private void BindDW()
        {
            DataTable dtdw = apro.GetAllDW();
            cmbDW1.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.cmbDW1.Items.Add(dtdw.Rows[i][0]);
            }
        }
        /// <summary>
        /// 注入方式
        /// </summary>
        private void BindZRFS()
        {
            DataTable dtdw = apro.GetAllYD_ZRFS();
            cmbYYFS1.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.cmbYYFS1.Items.Add(dtdw.Rows[i][0]);
            }
        }
        private void addtsyy_Load(object sender, EventArgs e)
        {
            BindDW();
            BindZRFS();
            BindYaoPin();
            datagridBind();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbName.Text = dataGridView1.CurrentRow.Cells["name"].Value.ToString();
            tbYl.Text = dataGridView1.CurrentRow.Cells["yl"].Value.ToString();            
        }

        private void tbPinyin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                DataTable dt = _YaopinDal.GetYaoPinByPinyin(tbPinyin.Text.Trim());
                listYaopin.Items.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    listYaopin.Items.Add(dr[1].ToString());
                }
                tbName.Text = "";
            }
        }

        private void tbPinyin_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13); 
        }

       
    }
}
