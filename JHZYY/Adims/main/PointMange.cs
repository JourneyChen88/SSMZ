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
    public partial class PointManage : Form
    {
        int mzjldid;
        int type;

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();

        public PointManage(int mzid, int type1)
        {
            type = type1;
            mzjldid = mzid;
            InitializeComponent();
        }

        private void SljlAddPoint_Load(object sender, EventArgs e)
        {
            this.dtRecordTime.Format = DateTimePickerFormat.Custom;
            this.dtRecordTime.CustomFormat = "yyyy-MM-dd HH:mm";
            DataBind();
        }
        private void DataBind()
        {
            DataTable dt = new DataTable();
            if (type==0)
                dt = bll.GetPoint(mzjldid);
            else if (type == 1)
                dt =bll.GetPointPacu(mzjldid);                
            dataGridView1.DataSource = dt.DefaultView;
            dataGridView1.Columns[0].ReadOnly = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.tbNIBPS.Text.IsNullOrEmpty() || tbNIBPD.Text.IsNullOrEmpty() || tbPulse.Text.IsNullOrEmpty()
                || tbRRC.Text.IsNullOrEmpty() || tbSPO2.Text.IsNullOrEmpty() || tbETCO2.Text.IsNullOrEmpty() || tbTemp.Text.IsNullOrEmpty() || tbBIS.Text.IsNullOrEmpty() || tBCVP.Text.IsNullOrEmpty())
            {
                MessageBox.Show("所有的监测值不能为空！");
            }
            else
            {
                if (type == 0)
                {
                    if (bll.GetPointSingle(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count == 0)
                    {
                        int result = bll.AddPoint(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text, tbPulse.Text, tbSPO2.Text, tbETCO2.Text, tbBIS.Text, tbTemp.Text, tBCVP.Text);
                        if (result > 0)
                        {
                            DataBind();
                        }
                        else
                            MessageBox.Show("添加失败，请重新添加");
                    }
                    else
                        MessageBox.Show("该时间点数据已存在，不能重复添加");

                }
                    //注意这个麻醉记录单里我改过监护数据
                else
                {
                    if (bll.GetPointSinglePacu(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count == 0)
                    {
                        int result = bll.AddPointPacu(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text, tbPulse.Text, tbSPO2.Text, tbETCO2.Text,tbTemp.Text);
                        if (result > 0)
                        {
                            DataBind();
                        }
                        else
                            MessageBox.Show("添加失败，请重新添加");
                    }
                    else
                        MessageBox.Show("该时间点数据已存在，不能重复添加");
                }

            }

        }

        #region//输入数字限制
        private void tbNIBPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
        private void tbRRC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }
        private void tbNIBPD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void tbPulse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void tbSPO2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void tbETCO2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void tbCVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        #endregion

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dtRecordTime.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["RecordTime"].Value);
            tbNIBPS.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["nibps"].Value);
            tbNIBPD.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["nibpd"].Value);
            tbPulse.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Pulse"].Value);
            tbRRC.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["rrc"].Value);
            tbSPO2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["spo2"].Value);
            tbETCO2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["etco2"].Value);
            tbTemp.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["temp"].Value);
            tbBIS.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["BIS"].Value);
            tBCVP.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["CVP"].Value);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            tbNIBPS.Text = "";
            tbNIBPD.Text = "";
            tbPulse.Text = "";
            tbRRC.Text = "";
            tbSPO2.Text = "";
            tbETCO2.Text = "";
            tbTemp.Text = "";
            tbBIS.Text = "";
            tBCVP.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<DataTable> list = new List<DataTable>();            
            DataTable dts = new DataTable();            
            int flag = 0;
            int flag2 = 0;
            if (type == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells["CoDelete"].EditedFormattedValue == true)
                    {
                        DateTime dt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["RecordTime"].Value);
                        flag2 = bll.DeletePoint(mzjldid, dt);
                        if (flag2 > 0)
                            flag++;
                    }
                }
                if (flag > 0)
                {
                    DataBind();
                }
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells["CoDelete"].EditedFormattedValue == true)
                    {
                        DateTime dt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["RecordTime"].Value);
                        flag2 = bll.DeletePointPACU(mzjldid, dt);
                        if (flag2 > 0)
                            flag++;
                    }
                }
                if (flag > 0)
                {
                    DataBind();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.tbNIBPS.Text.IsNullOrEmpty() || tbNIBPD.Text.IsNullOrEmpty() || tbPulse.Text.IsNullOrEmpty()
                || tbRRC.Text.IsNullOrEmpty() || tbSPO2.Text.IsNullOrEmpty() || tbETCO2.Text.IsNullOrEmpty() || tbTemp.Text.IsNullOrEmpty() || tbBIS.Text.IsNullOrEmpty() || tBCVP.Text.IsNullOrEmpty())
            {
                MessageBox.Show("所有的监测值不能为空！");
            }
            else
            {
                if (type==0)
                {
                    if (bll.GetPointSingle(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count != 0)
                    {
                        int result = bll.UpdatePoint(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text.Trim(), tbPulse.Text, tbSPO2.Text, tbETCO2.Text, tbBIS.Text, tbTemp.Text, tBCVP.Text);

                        if (result > 0)
                        {
                            MessageBox.Show("修改成功！");
                            DataBind();
                        }
                        else
                            MessageBox.Show("修改失败，请重试！");
                    }
                    else
                        MessageBox.Show("该时间点数据不存在，请尝试添加");
                }
                else
                {
                    if (bll.GetPointSinglePACU(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count != 0)
                    {
                        int result = bll.UpdatePointPACU(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text.Trim(), tbPulse.Text, tbSPO2.Text, tbETCO2.Text,tbTemp.Text);

                        if (result > 0)
                        {
                            MessageBox.Show("修改成功！");
                            DataBind();
                        }
                        else
                            MessageBox.Show("修改失败，请重试！");
                    }
                    else
                        MessageBox.Show("该时间点数据不存在，请尝试添加");
                }
                
            }
        }

        

        private void tBCVP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

        private void tbBIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            {
                e.Handled = true;
            }
        }

       

       
    }
}
