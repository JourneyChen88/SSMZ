using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using adims_Utility;

namespace main.药品耗材管理
{
    public partial class YaopinManage : Form
    {
        YaopinDal _YaopinDal = new YaopinDal();
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        string ypLx;
        public string ypName1;
        public string nameSX1;
        public string bagName1;
        public YaopinManage()
        {
            InitializeComponent();
        }

        private void BindYaopin( )
        {
            DataTable dt = _YaopinDal.GetYaoPinByType(cmbYpType.Text.Trim(),"");
            dgvYaowu.DataSource = dt.DefaultView;
        }
        private void BindYaoPinType()
        {
            DataTable dt = _YaopinDal.GetYaoPinType();
            cmbYpType.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                cmbYpType.Items.Add(dr[0].ToString());
            }
            
        }
        private void BindYaoPinBag()
        {
            DataTable dt = _YaopinDal.GetYongYaoBagByBagName(this.cmbBagName.Text.Trim());
            this.dataGridView1.DataSource = dt.DefaultView;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtYPname.Text != "" && cmbYpType.Text != "" && txtNameSuox.Text.Trim()!="")
            {
                int i = 0;
                ypName1 = txtYPname.Text.Trim();
                nameSX1 = txtNameSuox.Text.Trim();
                ypLx = cmbYpType.Text.Trim();
                DataTable dt = _YaopinDal.GetYaoPinByName(ypName1);
                if (dt.Rows.Count == 0)
                {
                    i = _YaopinDal.InsertYaoPin(ypName1, nameSX1, ypLx);                    
                    if (i == 0) MessageBox.Show("添加失败，请重试！");
                    else BindYaopin();
                }
                else
                    MessageBox.Show("同类型药品已存在，不能重复添加！");
                
            }
            else
                MessageBox.Show("药品名和药品类型不能为空！");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dgvYaowu.SelectedRows.Count > 0)
            {
                int flag = 0;
                flag = _YaopinDal.DeleteYaoPin(Convert.ToInt32( dgvYaowu.SelectedRows[0].Cells[0].Value));
                BindYaopin();
                if (flag==0)
                {
                    MessageBox.Show("删除失败，请重试！"); 
                }
            }
            else
                MessageBox.Show("请选择需要删除的药品！"); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvYaowu.SelectedRows.Count > 0)
            {
                int flag = 0;
                int  ID=Convert.ToInt32(dgvYaowu.SelectedRows[0].Cells[0].Value);
                    
                ypName1 = txtYPname.Text.Trim();
                nameSX1 = txtNameSuox.Text.Trim();
                ypLx = cmbYpType.Text.Trim();
                DataTable dt = _YaopinDal.GetYaoPinByName(ypName1);
                if (dt.Rows.Count == 0)
                 {
                     flag = _YaopinDal.UpdateYaoPin(ypName1,nameSX1, ypLx, ID);                    
                     if (flag == 0)
                         MessageBox.Show("修改失败，请重试！");
                     else
                         BindYaopin();
                 }
                 else
                     MessageBox.Show("药品已存在，不能重复添加！");
            }
            else
                MessageBox.Show("请选择需要修改的药品！"); 
        }
        /// <summary>
        /// 单位
        /// </summary>
        private void BindDW()
        {
            DataTable dtdw = DAL.GetAllDW();
            cmbDW.Items.Clear();
            for (int i = 0; i < dtdw.Rows.Count; i++)
            {
                this.cmbDW.Items.Add(dtdw.Rows[i][0]);
            }
        }
        private void YPguanli_Load(object sender, EventArgs e)
        {
            BindDW();
            BindYaopin();
            BindYaoPinBag();
            BindYaoPinType();
            DataTable dt = _YaopinDal.GetYaopinBagAll();
            cmbBagName.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbBagName.Items.Add(dt.Rows[i][0]);
            }
        }

        private void dgvYaowu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtYPname.Text = dgvYaowu.SelectedRows[0].Cells[1].Value.ToString();
            txtNameSuox.Text = dgvYaowu.SelectedRows[0].Cells[3].Value.ToString();
            cmbYpType.Text = dgvYaowu.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void cmbYpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = _YaopinDal.GetYaoPinByType(cmbYpType.Text.Trim(),"");
            dgvYaowu.DataSource = dt.DefaultView;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.tbYL.Text != "" && cmbDW.Text != "" && cmbYaowuName.Text != "" && cmbBagName.Text != "")
            {
                int i = 0;
                bagName1 = cmbBagName.Text.Trim();
                ypName1 = cmbYaowuName.Text.Trim();
                
                int cxyy1=checkBox1.Checked?1:0;
                DataTable dt = _YaopinDal.GetYaoPinBag(ypName1, bagName1);
                if (dt.Rows.Count == 0)
                {
                    i = _YaopinDal.InsertYaoPinBag(bagName1, ypName1, tbYL.Text.Trim(), cmbDW.Text.Trim(), cmbZRFS.Text.Trim(), cxyy1);
                    BindYaoPinBag();
                    if (i == 0)
                    {
                        MessageBox.Show("添加失败，请重试！");
                    }
                }
                else
                    MessageBox.Show("有输入框为空，不能添加到用药包");

            }
            else
                MessageBox.Show("药品名,药品类型,药包名不能为空！");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int flag = 0;
                flag = _YaopinDal.DeleteYaoPinBag(Convert.ToInt32(this.dataGridView1.SelectedRows[0].Cells[0].Value));
                BindYaoPinBag();
                if (flag == 0)
                {
                    MessageBox.Show("删除失败，请重试！");
                }
            }
            else
                MessageBox.Show("请选择需要删除的药品！"); 
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextValueLimit.Text_Value_Limit(sender,e);
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void cmbBagName_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindYaoPinBag();
        }

        private void cmbYaowuName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                cmbYaowuName.Items.Clear();
                DataTable dt = _YaopinDal.GetYaoPinByName(cmbYaowuName.Text.Trim());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbYaowuName.Items.Add(dt.Rows[i]["YPNAME"]);
                }
                cmbYaowuName.Text = "";
                cmbYaowuName.DroppedDown = true;
            }
        }

        private void cmbYaowuName_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)32) || (e.KeyChar == (char)13);
        }
       
    }
}
