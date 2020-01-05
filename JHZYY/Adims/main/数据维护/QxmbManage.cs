using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;

namespace main
{
    public partial class QxmbManage : Form
    {
        adims_DAL.admin_T_SQL at = new admin_T_SQL();
        public QxmbManage()
        {
            InitializeComponent();
        }
        adims_DAL.AdimsProvider DAL = new adims_DAL.AdimsProvider();
        private void Bind_comboBox1()
        {
            //comboBox1.Items.Clear();
            DataTable dt = DAL.GetallQxModel();
            comboBox1.DataSource = dt;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "qxbType";
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    comboBox1.Items.Add(dt.Rows[i][0]);
            //}
           // comboBox1.SelectedIndex = 0;
        }

        private void QiXieQD_Load(object sender, EventArgs e)
        {
            Bind_comboBox1();
        }
        private void DgvBind() {
            DataTable dt = DAL.SelectqxmcInmodel(comboBox1.Text.Trim());
            dataGridView1.DataSource = dt.DefaultView;
            DataTable dts = DAL.SelectNum(comboBox1.Text.Trim());
            this.lblnum.Text = dts.Rows[0][0].ToString();

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DgvBind();
            //DataTable dt = DAL.SelectqxmcInmodel(comboBox1.Text);
            //int j = dt.Rows.Count;
            //dataGridView1.Rows.Clear();
            //dataGridView1.Rows.Add(j + 1);
            //for (int i = 0; i < j; i++)
            //{
            //    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0];
            //    dataGridView1.Rows[i].Cells[1].Value = dt.Rows[i][1];

            //}


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("确定修改", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(Convert.ToString(textBox2.Text.Trim())))
                {
                    MessageBox.Show("请输入模板器械");
                    this.textBox1.Focus();
                }
                else
                { 
                    //int j = 0, flag = 0;
                    string qxmbm = textBox2.Text.Trim();
                    int flag=0;
                    string cmb = comboBox1.Text.Trim();
                    try
                    {
                        //for (int i = 0; i < dataGridView1.Rows.Count; i++)
                        //{
                        //    if (dataGridView1.Rows[i].Cells[0].Value != null)
                        //    {
                        //        string qzmz = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        //        int qxCount = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                        flag = DAL.SelectqxType(qxmbm,cmb);
                        //        j++;
                        //    }
                        //}
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("修改模板失败！");
                    }
                    if (flag > 0)
                    {
                        MessageBox.Show("修改模板成功！");
                        //at.DeleteqxqdModel(comboBox1.Text.Trim());
                        Bind_comboBox1();
                        this.textBox2.Clear();
                    }

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(textBox1.Text.Trim())))
            {
                MessageBox.Show("请输入模板器械");
                this.textBox1.Focus();
            }
            else
            {
                DataTable dt = DAL.SlectqxModel(textBox1.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("器械模板已存在，请检查！");
                }
                else
                {
                    //int j = 0, flag = 0;
                    int flag = 0;
                    string qxmbm = textBox1.Text.Trim();
                    try
                    {
                    //    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    //    {
                    //        if (dataGridView1.Rows[i].Cells[0].Value != null)
                    //        {
                    //            string qzmz = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    //            int qxCount = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                                flag = DAL.InsertqxqdModel(qxmbm);
                        //        j++;
                        //    }
                        //}
                
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("添加模板失败，请重新添加！");

                    }
                    if (flag > 0)
                    {
                        MessageBox.Show("添加模板成功！");
                        Bind_comboBox1();
                        this.textBox1.Clear();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定删除", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //DataTable table = DAL.SelectqxmType(comboBox1.Text);
                //int qxqd_id=Convert.ToInt32 (table.Rows[0][0].ToString());
                int dts = at.DeleteqxqdModel(Convert.ToInt32(comboBox1.SelectedValue));
                int dt = at.DeleteqxqdType(comboBox1.Text.Trim());
                if (dt > 0)
                {
                    MessageBox.Show("删除成功！");
                    Bind_comboBox1();
                }
                else
                {
                    MessageBox.Show("删除失败！");
                }
            }
        }
        /// <summary>
        /// 添加器械名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Convert.ToString(comboBox1.Text.Trim())))
            {
                MessageBox.Show("请选择模板器械");
                this.textBox1.Focus();
            }
            else
            { 
             int flag = 0;
             flag = DAL.SelectqxqdModel(Convert.ToInt32(comboBox1.SelectedValue));
             if (flag > 0)
             {
                 DgvBind();
             }
             else
             {
                 MessageBox.Show("添加失败！");
             }     
            }
        }
        /// <summary>
        /// 删除器械名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count>0)
                {
                    if (MessageBox.Show(" 您确定要删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int id = Convert.ToInt32(this.dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value);
                        int flag = 0;
                        flag = DAL.DeteletqxqdModel(id);
                       
                        if (flag>0)
                        {
                            DgvBind();
                        }
                        else
                        {
                            MessageBox.Show("删除失败");
                        }  
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("删除失败");
            }
        }
        
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                  DBConn dBConn = new DBConn();
                 int Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                 string Name = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].Name;
                 string value_info = dataGridView1.CurrentCell.Value.ToString();
                 //string Text = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText;
                 int flag = 0;
                 string countQJ = "update Adims_qxqdModel set " + Name + "='" + value_info + "' where id=" + Id + "";
                 flag = dBConn.ExecuteNonQuery(string.Format(countQJ));
                 if (flag>0)
                 {
                     
                 }
                 else
                 {
                     MessageBox.Show("修改失败");
                 }
            }
        }
        /// <summary>
        /// 输入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.ColumnIndex == 2)
                e.Control.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
            else
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);

        }
   
               

    }
}
