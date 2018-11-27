using adims_DAL;
using adims_MODEL;
using Adims_Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class OperDoctorUpdate : Form
    {
        public OperDoctorUpdate()
        {
            InitializeComponent();
        }

        private void OperDoctorUpdate_Load(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindGridView();
        }

        private void BindGridView()
        {
            DataTable dt = PaibanDal.GetOsAndSSYS();
            var list = ListHelper<UpdDoctorModel>.ConvertToList(dt);
            dgvChoice.DataSource = list;
        }

        private void TaskMethod()
        {
            var list = dgvChoice.DataSource as List<UpdDoctorModel>;
            int i = 0;
            foreach (var item in list)
            {
                if (!item.SSYS.IsNullOrEmpty())
                {
                    string sql = string.Empty;
                    var spit = item.SSYS.Split('、');
                    if (spit.Count() == 1)
                    {
                        sql = $@"UPDATE  Adims_OTypesetting SET 
                                            OS='{spit[0]}'
                                    WHERE PatId='{item.PatID}'";
                        PaibanDal.ExecuteUpdate(sql);
                    }
                    if (spit.Count() == 2)
                    {
                        sql = $@"UPDATE  Adims_OTypesetting SET 
                                            OS='{spit[0]}',OA1='{spit[1]}'
                                                    WHERE PatId='{item.PatID}'";
                        PaibanDal.ExecuteUpdate(sql);
                    }
                    if (spit.Count() == 3)
                    {
                        sql = $@"UPDATE  Adims_OTypesetting SET 
                                        OS='{spit[0]}',OA1='{spit[1]}',OA2='{spit[2]}'
                                          WHERE PatId='{item.PatID}'";
                        PaibanDal.ExecuteUpdate(sql);

                    }
                    if (spit.Count() >= 4)
                    {
                        sql = $@"UPDATE  Adims_OTypesetting SET 
                                        OS='{spit[0]}',OA1='{spit[1]}',OA2='{spit[2]}',OA3='{spit[3]}'
                                          WHERE PatId='{item.PatID}'";
                        PaibanDal.ExecuteUpdate(sql);
                    }

                }
                else
                {
                    string sql = $@"UPDATE  Adims_Mzjld SET 
                                            SSYS='{item.OS}'
                                             WHERE PatId='{item.PatID}'";
                    PaibanDal.ExecuteUpdate(sql);
                }
                i++;
            }
            BindGridView();
            MessageBox.Show("修正完毕");
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "正在修正中。。。，请不要关闭界面";
            try
            {
                TaskMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }    
            
            //Task t3 = new Task(TaskMethod);
            //t3.Start();
           
          
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
