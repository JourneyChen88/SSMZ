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
            if (type == 0)
                dt = bll.GetPoint(mzjldid);
            else if (type == 1)
                dt = bll.GetPointPacu(mzjldid);
            dataGridView1.DataSource = dt.DefaultView;
            dataGridView1.Columns[0].ReadOnly = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
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
            //if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != 8 && e.KeyChar != 13)
            //{
            //    e.Handled = true;
            //}
            bool IsContainsDot = this.tbTemp.Text.Contains(".");
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8) && (e.KeyChar != 46))
            {
                e.Handled = true;
            }
            else if (IsContainsDot && (e.KeyChar == 46)) //如果输入了小数点，并且再次输入 
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
            tbTOF.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["tof"].Value);
            tbBIS.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["bis"].Value);
        }

      
       


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ControlIsEmpty())
            {
                if (type == 0)
                {
                    if (bll.GetPointSingle(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count != 0)
                    {
                        int result = bll.UpdatePoint(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text.Trim(), tbPulse.Text, tbSPO2.Text, tbETCO2.Text, tbTemp.Text, tbTOF.Text, tbBIS.Text);

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
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text.Trim(), tbPulse.Text, tbSPO2.Text, tbETCO2.Text, tbTOF.Text, tbBIS.Text);

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

        private bool ControlIsEmpty()
        {
            if (this.tbNIBPS.Text == "" || tbNIBPD.Text == "" || tbPulse.Text == ""
               || tbRRC.Text == "" || tbSPO2.Text == "" || tbETCO2.Text == "")
            {
                MessageBox.Show("所有的监测值不能为空！");
                return true;
            }
            else
                return false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ControlIsEmpty())
            {
                return;
            }
            if (type == 0)
            {
                if (bll.GetPointSingle(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count == 0)
                {
                    string temp = tbTemp.Text;
                    if (temp.Contains("."))
                    {
                        temp = temp.Substring(0, temp.IndexOf(".") + 2);
                    }
                  string  StrTOF = "";
                    if (tbTOF.Text=="")
                    {
                        StrTOF = "0";
                    }
                    string StrBIS = "";
                    if (tbBIS.Text == "")
                    {
                        StrBIS = "0";
                    }
                    int result = bll.AddPoint(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                    tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text, tbPulse.Text, tbSPO2.Text, tbETCO2.Text, temp,StrTOF,StrBIS);
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
            else
            {
                if (bll.GetPointSinglePacu(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count == 0)
                {
                    int result = bll.AddPointPacu(mzjldid, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                    tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text, tbPulse.Text, tbSPO2.Text, tbETCO2.Text, tbTemp.Text, tbTOF.Text, tbBIS.Text);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            tbNIBPS.Text = "";
            tbNIBPD.Text = "";
            tbPulse.Text = "";
            tbRRC.Text = "";
            tbSPO2.Text = "";
            tbETCO2.Text = "";
            tbTemp.Text = "";
            tbTOF.Text = "";
            tbBIS.Text = "";
        }

    }
}
