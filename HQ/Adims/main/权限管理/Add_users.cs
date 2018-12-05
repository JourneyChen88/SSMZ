using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;
using adims_BLL;

namespace main.权限管理
{
    public partial class Add_users : Form
    {
        adims_DAL.Dics.UserDal _UserDal = new adims_DAL.Dics.UserDal();
        public Add_users()
        {
            InitializeComponent();
        }
        AdimsController bll = new AdimsController();
        DataTable users = new DataTable();

        private void Add_users_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _UserDal.GetUserAll();
            DataSet pj_set = new DataSet();
            DataTable pj_table = new DataTable();
            pj_set = _UserDal.GetPositionJurisdictionAll();
            pj_table = pj_set.Tables[0];
            foreach (DataRow myrow in pj_table.Rows)
            {
                cmbPosition.Items.Add(myrow[1]);
            }            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim() == "")
            {
                MessageBox.Show("请填写真实姓名！");
                tbName.Focus();
            }else if (tbUid.Text.Trim()=="")
            {
                MessageBox.Show("请填写登陆账号！");
                tbUid.Focus();
            }else if(tbPwd.Text.Trim()=="")
            {
                MessageBox.Show("请填写登陆密码！");
                tbPwd.Focus();
            }
            else
            {
                UserInfo user = new UserInfo();
                user.user_name = tbName.Text.Trim();
                DataTable dt = _UserDal.GetUserSum(user.user_name);
                int i = Convert.ToInt32(dt.Rows[0][0]);
                if (i > 0)
                {
                    MessageBox.Show("用户名不能重复添加！");
                }
                else
                {
                    user.uid = tbUid.Text.Trim();
                    user.password = tbPwd.Text.Trim();
                    user.position = cmbPosition.Text.Trim();
                    if (cmbType.Text.Trim() == "管理员")
                    {
                        user.type = 0;
                    }
                    else if (cmbType.Text.Trim() == "医师")
                    {
                        user.type = 1;
                    }
                    else
                    {
                        user.type = 2;
                    }
                    _UserDal.InsertUser(user);
                    users = _UserDal.GetUserAll();
                    dataGridView1.DataSource = users.DefaultView;
                    MessageBox.Show("用户添加成功！");
                }
            }
        }
        bool update_flag = true;
        private void button2_Click(object sender, EventArgs e)
        { 
                if (dataGridView1.SelectedCells.Count == 0) 
                { MessageBox.Show("请选择要修改的行", "警告"); return; }
                UserInfo user = new UserInfo();
                user.uid = tbUid.Text.Trim();
                user.user_name = tbName.Text.Trim();
                user.password = tbPwd.Text.Trim();
                user.position = cmbPosition.Text.Trim();
                string id1 = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
                if (cmbType.Text.Trim() == "管理员")
                {
                    user.type = 0;
                }
                else if (cmbType.Text.Trim() == "医师")
                {
                    user.type = 1;
                }
                else
                {
                    user.type = 2;
                }
                _UserDal.UpdateUserByID(user,id1);
                dataGridView1.DataSource= _UserDal.GetUserAll();
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) 
            { MessageBox.Show("请选择要删除的用户", "警告"); return; }
            else if (MessageBox.Show("您真的要删除吗？", "警告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int row_index = dataGridView1.SelectedCells[0].RowIndex;
                UserInfo user = new UserInfo();
                user.Id =Convert.ToInt32( dataGridView1.Rows[row_index].Cells[0].Value.ToString());
                _UserDal.DeleteUser(user);
                users = _UserDal.GetUserAll();
                dataGridView1.DataSource = users.DefaultView;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
               tbUid.Text=dataGridView1.CurrentRow.Cells[1].Value.ToString();
               tbPwd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
               tbName.Text=dataGridView1.CurrentRow.Cells[3].Value.ToString();
               cmbPosition.Text=dataGridView1.CurrentRow.Cells[4].Value.ToString();
               if (dataGridView1.CurrentRow.Cells[5].Value.ToString()=="0")
               {
                   cmbType.Text = "管理员";
               }
               if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "1")
               {
                   cmbType.Text = "医师";
               }
               if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "2")
               {
                   cmbType.Text = "护士";
               }
              
            }
        }
    }
}
