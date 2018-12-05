using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main.数据维护
{
    public partial class keshiManage : Form
    {
        adims_DAL.Dics.DataDicDal _DataDicDal = new adims_DAL.Dics.DataDicDal();
        public keshiManage()
        {
            InitializeComponent();
        }
        PacuDal dal = new PacuDal();
      
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void keshiMC_Load_1(object sender, EventArgs e)
        {
            ShowLoad();
        }
        /// <summary>
        /// 绑定
        /// </summary>
        public void ShowLoad() 
        {
            DataTable dt = _DataDicDal.GetKeshi();
            dgvFangfa.DataSource = dt.DefaultView;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text!="")
            { 
                DataTable dt = _DataDicDal.GetKeshi(this.txtName.Text);
                if (dt.Rows.Count==0)
                {
                    int reault = _DataDicDal.InsertKeshi(this.txtName.Text);
                    if (reault>0)
                    {
                        ShowLoad();
                    }
                    else
                    {
                        MessageBox.Show("添加失败，请重试！");
                    }
                }
                else
                {
                    MessageBox.Show("该科室名称已存在，不能重复添加！");
                }
            }
            else
            {
                MessageBox.Show("科室名称不能为空！");
            }
        }
       
          
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvFangfa.SelectedRows.Count> 0)
            {
                int realut = _DataDicDal.UpdateKeshi(Convert.ToInt32(dgvFangfa.SelectedRows[0].Cells[0].Value),this.txtName.Text);
                if (realut > 0)
                {
                    ShowLoad();
                    
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }
            }
            else
            {
                MessageBox.Show("请选择需要修改的科室名称！");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelecte_Click(object sender, EventArgs e)
        {
            if (dgvFangfa.SelectedRows.Count>0)
            {
                int realut = _DataDicDal.DeleteKeshi(Convert.ToInt32(dgvFangfa.SelectedRows[0].Cells[0].Value));
                if (realut > 0)
                {
                    ShowLoad();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
            else
            {
                MessageBox.Show("请选择需要删除的科室名称！");
            }
            
        }
        /// <summary>
        /// 单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvFangfa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvFangfa.SelectedRows[0].Cells[1].Value.ToString();
         
        }

       
       
   
    }
}
