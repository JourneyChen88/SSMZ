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
    public partial class qxqdForm : Form
    {

        adims_DAL.AdimsProvider apro = new adims_DAL.AdimsProvider();
        public qxqdForm()
        {
            InitializeComponent();
        }

        private void qxqdForm_Load(object sender, EventArgs e)
        {
            FillDataIn();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (i%2==0)
                {
                     dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
                     dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.Red;
            }
                        
            Bind_comboBox1();
            
        }
        private void Bind_comboBox1()
        {
            DataTable dt = apro.GetallQxModel();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(dt.Rows[i][0]);
            }
        }


        private void FillDataIn()//填入初始数量
        {
            for (int i = 0; i < 20; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[1].Value = "0";
                dataGridView1.Rows[i].Cells[2].Value = "0";
                dataGridView1.Rows[i].Cells[3].Value = "0";
                dataGridView1.Rows[i].Cells[4].Value = "0";
                dataGridView1.Rows[i].Cells[6].Value = "0";
                dataGridView1.Rows[i].Cells[7].Value = "0";
                dataGridView1.Rows[i].Cells[8].Value = "0";
                dataGridView1.Rows[i].Cells[9].Value = "0";
            }

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
          
            if (e.RowIndex<=19&&(e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3 || e.ColumnIndex == 4))
            {
                try
                {
                    // 如果输入的格式化数值是空值执行 
                    if (String.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        
                        MessageBox.Show("输入不能为空！");
                        // 通过事件名柄将输入操作取消 
                        e.Cancel = true;
                    }
                    // 将单元格输入的格式化数值转换成高精度浮点型  
                    else
                    {
                        decimal val = decimal.Parse(e.FormattedValue.ToString());
                    }
                }
                catch
                {
                    MessageBox.Show("输入中有无效字符，只能输入数字！");
                    // 通过事件名柄将输入操作取消 
                    e.Cancel = true;
                }
            }

        }
       
    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 19)
            {
                dataGridView1.Rows[e.RowIndex].ReadOnly = true;
                MessageBox.Show("此行不能编辑");
            }
        }
        int j, k;
        private void button1_Click(object sender, EventArgs e)
        {
             string qxmbm=textBox1.Text;
             
             try
             {
                 for (int i = 0; i < dataGridView1.Rows.Count; i++)
                 {
                     if (dataGridView1.Rows[i].Cells[0].Value != null)
                     {
                         string qzmz = dataGridView1.Rows[i].Cells[0].Value.ToString();
                         int qxCount = Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value);
                         //j = apro.InsertqxqdModel(qzmz, qxmbm, qxCount);
                         
                     }

                 }
                 for (int i = 0; i < dataGridView1.Rows.Count; i++)
                 {
                     if (dataGridView1.Rows[i].Cells[5].Value != null)
                     {
                         string qzmz = dataGridView1.Rows[i].Cells[5].Value.ToString();
                         int qxCount = Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
                         //k = apro.InsertqxqdModel(qzmz, qxmbm, qxCount);
                     }
                 }

             }
             catch (Exception)
             {

                 MessageBox.Show("添加模板失败，请重新添加！");

             }
             if (j>0||k>0)
             {
                 MessageBox.Show("添加模板成功！");
             }
             comboBox1.Items.Clear();
             Bind_comboBox1();
             
            
        }

        private void dategridView1_Fresh()
        {
            string kongge = "";
            for (int i = 0; i < 20; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = kongge;
                dataGridView1.Rows[i].Cells[5].Value = kongge;
            } 
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dategridView1_Fresh();
            DataTable dt=apro.SelectqxmcInmodel(comboBox1.SelectedItem.ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {                                
                if (i<=19)
                {                    
                    dataGridView1.Rows[i].Cells[0].Value = dt.Rows[i][0];                    
                }
                else if (i > 19)
                {                    
                    dataGridView1.Rows[i - 20].Cells[5].Value = dt.Rows[i][0];
                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 5)
            {
                qixieManage selaa = new qixieManage(dataGridView1, e.RowIndex, e.ColumnIndex);
                selaa.ShowDialog();
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            int flag = 0;//清点成功标志
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                int a1 = Convert.ToInt32( dataGridView1.Rows[i].Cells[1].Value);
                int a2 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                int a3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                int a4 = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                int a5 = Convert.ToInt32(dataGridView1.Rows[i].Cells[6].Value);
                int a6 = Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
                int a7 = Convert.ToInt32(dataGridView1.Rows[i].Cells[8].Value);
                int a8 = Convert.ToInt32(dataGridView1.Rows[i].Cells[9].Value);
                if (a1 + a2 != a3 && a1 + a2 != a4)
                { flag++; MessageBox.Show(dataGridView1.Rows[i].Cells[0].Value.ToString() + "数量不对"); }
                else if (a5 + a6 != a7 && a5 + a6 != a8)
                { flag++; MessageBox.Show(dataGridView1.Rows[i].Cells[5].Value.ToString() + "数量不对"); }

                
            }
            if (flag == 0)
                MessageBox.Show("清点成功！");
           
        }

       
    }
}