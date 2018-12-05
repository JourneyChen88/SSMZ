using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_MODEL;
using adims_DAL;
using adims_DAL.Flows;

namespace main
{
    public partial class PointManage : Form
    {
        int _MzjldId;
        int type;
        int JHXcvp;
        int JHXqdy;
        int JHXsdz;
        int JHXjsz;
        MzjldDal _MzjldDal = new MzjldDal();
        MzjldPointDal mpdal = new MzjldPointDal();
        //MoniterRecordDal RecordDal = new MoniterRecordDal();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<ZoomRegion> ZoomRegionList = new List<ZoomRegion>();
        public PointManage(int mzid, int type1, int jhxcvp, int jhxqdy, int jhxsdz, int jhxjsz, List<ZoomRegion> ZoomRegionList1)
        {
            ZoomRegionList = ZoomRegionList1;
            type = type1;
            _MzjldId = mzid;
            JHXcvp = jhxcvp;
            JHXqdy = jhxqdy;
            JHXsdz = jhxsdz;
            JHXjsz = jhxjsz;
            InitializeComponent();
        }
        public PointManage(int mzid, int type1, int jhxcvp, int jhxqdy, int jhxsdz, int jhxjsz)
        {
            type = type1;
            _MzjldId = mzid;
            JHXcvp = jhxcvp;
            JHXqdy = jhxqdy;
            JHXsdz = jhxsdz;
            JHXjsz = jhxjsz;
            InitializeComponent();
        }
        private void SljlAddPoint_Load(object sender, EventArgs e)
        {
            this.dtRecordTime.Format = DateTimePickerFormat.Custom;
            this.dtRecordTime.CustomFormat = "yyyy-MM-dd HH:mm";
            DataBind();
            if (type == 1 || type == 0)
            {
                //lbltw.Visible = false;
                //tbtemp.Visible = false;
                //dataGridView1.Columns["temp"].Visible = false;     
            }
            if (JHXcvp == 1)
            {
                lblCVP.Visible = true;
                tbCVP.Visible = true;
                dataGridView1.Columns["cvp"].Visible = true;

            }
            if (JHXqdy == 1)
            {
                lblqdy.Visible = true;
                tbqdy.Visible = true;
                dataGridView1.Columns["qdy"].Visible = true;

            }
            if (JHXsdz == 1)
            {
                lblsdz.Visible = true;
                tbsdz.Visible = true;
                dataGridView1.Columns["sdz"].Visible = true;

            }
            if (JHXjsz == 1)
            {
                lbljsz.Visible = true;
                tbjsz.Visible = true;
                dataGridView1.Columns["jsz"].Visible = true;

            }
        }
        private void DataBind()
        {
            DataTable dt = new DataTable();
            if (type == 0)
            {
                var dtmz = _MzjldDal.GetMzjldByMzjldId(_MzjldId);
                if (dtmz.Rows.Count > 0 && dtmz.Rows[0]["IsZoom"].ToString() == "0")
                {
                    dt = mpdal.GetByMzjldID(_MzjldId);
                }
                else
                {
                    foreach (ZoomRegion ZR in ZoomRegionList)
                    {
                        DataTable dt2 = mpdal.GetByTimeSpan(_MzjldId, ZR.AStartTime, ZR.EndTime, ZR.Interval);
                        if (dt2.Rows.Count > 0)
                        {
                            if (dt != null)
                            {
                                dt.Merge(dt2);
                            }
                            else
                                dt = dt2;
                        }
                    }
                }

            }
            else if (type == 1)
                dt = bll.GetPointPacu(_MzjldId);
            dataGridView1.DataSource = dt.DefaultView;
            dataGridView1.Columns[0].ReadOnly = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.tbNIBPS.Text == "" || tbNIBPD.Text == "" || tbPulse.Text == ""
                || tbRRC.Text == "" || tbSPO2.Text == "")
            {
                MessageBox.Show("所有的监测值不能为空！");
            }
            else
            {
                if (type == 0)
                {
                    if (mpdal.GetSingle(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count == 0)
                    {
                        int result = mpdal.Add(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")), tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text, tbPulse.Text, tbSPO2.Text, tbetco2.Text, tbCVP.Text, tbtemp.Text);
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
                    if (bll.GetPointSinglePACU(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count == 0)
                    {
                        int result = bll.AddPointPACU(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")), tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text, tbPulse.Text, tbSPO2.Text, tbetco2.Text, tbCVP.Text, tbtemp.Text);
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
            //dtRecordTime.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["CreateTime"].Value);
            dtRecordTime.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells["CreateTime"].Value);
            tbNIBPS.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["nibps"].Value);
            tbNIBPD.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["nibpd"].Value);
            tbPulse.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["Pulse"].Value);
            tbRRC.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["rrc"].Value);
            tbSPO2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["spo2"].Value);
            tbetco2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["etco2"].Value);
            tbCVP.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["cvp"].Value);
            tbqdy.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["qdy"].Value);
            tbsdz.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["sdz"].Value);
            tbjsz.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["jsz"].Value);
            tbtemp.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["temp"].Value);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            tbNIBPS.Text = "";
            tbNIBPD.Text = "";
            tbPulse.Text = "";
            tbRRC.Text = "";
            tbSPO2.Text = "";
            tbtemp.Text = "";
            tbCVP.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int flag = 0;
            int flag2 = 0;
            if (type == 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((bool)dataGridView1.Rows[i].Cells["CoDelete"].EditedFormattedValue == true)
                    {
                        //DateTime dt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["CreateTime"].Value);
                        DateTime dt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["CreateTime"].Value);
                        flag2 = mpdal.Delete(_MzjldId, dt);
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
                        DateTime dt = Convert.ToDateTime(dataGridView1.Rows[i].Cells["CreateTime"].Value);
                        flag2 = bll.DeletePointPACU(_MzjldId, dt);
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
            if (this.tbNIBPS.Text == "" || tbNIBPD.Text == "" || tbPulse.Text == ""
                || tbRRC.Text == "" || tbSPO2.Text == "" || tbetco2.Text == "")
            {
                MessageBox.Show("所有的监测值不能为空！");
            }
            else
            {
                if (type == 0)
                {
                    if (mpdal.GetSingle(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count != 0)
                    {
                        int result = mpdal.Update(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text.Trim(), tbPulse.Text, tbSPO2.Text, tbetco2.Text, tbtemp.Text, tbCVP.Text);

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
                    if (bll.GetPointSinglePACU(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm"))).Rows.Count != 0)
                    {
                        int result = bll.UpdatePointPACU(_MzjldId, Convert.ToDateTime(dtRecordTime.Value.ToString("yyyy/MM/dd HH:mm")),
                        tbNIBPS.Text, tbNIBPD.Text, tbRRC.Text.Trim(), tbPulse.Text, tbSPO2.Text, tbetco2.Text, tbtemp.Text, tbCVP.Text, tbqdy.Text, tbsdz.Text, tbjsz.Text);

                        if (result > 0)
                        {
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


    }
}
