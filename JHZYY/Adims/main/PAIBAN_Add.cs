using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_BLL;
using adims_DAL;
using adims_MODEL;
using Adims_Utility;
using WindowsFormsControlLibrary5;

namespace main
{
    public partial class PAIBAN_Add : Form
    {
        SqlSugarDal dal = new SqlSugarDal();
        adims_DAL.AdimsProvider provideDal = new adims_DAL.AdimsProvider();
        OTypesetting _model = new OTypesetting();
        public PAIBAN_Add(string id)
        {
            InitializeComponent();
            _model = dal.GetPaiban(id.ToInt32());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void PAIBAN_SGShuRu_Load(object sender, EventArgs e)
        {
            BindAllComBox();
            txtStartTime.Controls[0].Text = _model.StartTime;
            txtPatid.Controls[0].Text = _model.PatID;
            txtBednumber.Controls[0].Text = _model.Patbedno;
            txtPatname.Controls[0].Text = _model.Patname;
            txtSex.Controls[0].Text = _model.Patsex;
            txtPatTmd.Controls[0].Text = _model.Pattmd;
            txtPatNation.Controls[0].Text = _model.PatNation;
            txtMZFF.Controls[0].Text = _model.Amethod;
            txtPatdpm.Controls[0].Text = _model.Patdpm;
            txtOname.Controls[0].Text = _model.Oname;
            txtOS.Controls[0].Text = _model.OS;
            txtPatage.Controls[0].Text = _model.Patage;
            txtWeight.Controls[0].Text = _model.PatWeight;
            txtHeight.Controls[0].Text = _model.PatHeight;
            txtIsGeli.Controls[0].Text = _model.IsGeli;
            txtTalkInfo.Controls[0].Text = _model.TalkInfo;

            cmbOroom.Text = _model.Oroom;
            tbSecond.Text = _model.Second;
            cmbAP1.Text = _model.AP1;
            cmbAP2.Text = _model.AP2;
            cmbAp3.Text = _model.AP3;
            cmbSN1.Text = _model.SN1;
            cmbSN2.Text = _model.SN2;
            cmbON1.Text = _model.ON1;
            cmbON2.Text = _model.ON2;


        }

        private void BindAllComBox()
        {
            cmbOroom.Items.Clear();
            DataTable dt = provideDal.GetOROOM();
            cmbOroom.DataSource = dt;
            cmbOroom.ValueMember = "";
            cmbOroom.DisplayMember = "Name";

            DataTable dtHushi = provideDal.GetHushi();
            cmbSN1.DataSource = dtHushi.Copy();
            cmbSN1.ValueMember = "uid";
            cmbSN1.DisplayMember = "user_name";
            cmbSN1.SelectedIndex = -1;

            cmbON1.DataSource = dtHushi.Copy();
            cmbON1.ValueMember = "uid";
            cmbON1.DisplayMember = "user_name";
            cmbON1.SelectedIndex = -1;

            cmbSN2.DataSource = dtHushi.Copy();
            cmbSN2.ValueMember = "uid";
            cmbSN2.DisplayMember = "user_name";
            cmbSN2.SelectedIndex = -1;


            cmbON2.DataSource = dtHushi.Copy();
            cmbON2.ValueMember = "uid";
            cmbON2.DisplayMember = "user_name";
            cmbON2.SelectedIndex = -1;

            DataTable dtMZYS = provideDal.GetMZYS();
            cmbAP1.DataSource = dtMZYS.Copy();
            cmbAP1.ValueMember = "uid";
            cmbAP1.DisplayMember = "user_name";
            cmbAP1.SelectedIndex = -1;


            cmbAP2.DataSource = dtMZYS.Copy();
            cmbAP2.ValueMember = "uid";
            cmbAP2.DisplayMember = "user_name";
            cmbAP2.SelectedIndex = -1;

            cmbAp3.DataSource = dtMZYS.Copy();
            cmbAp3.ValueMember = "uid";
            cmbAp3.DisplayMember = "user_name";
            cmbAp3.SelectedIndex = -1;

        }

        private void cmbAP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.AP1 = cmbAP1.Text;
            _model.AP1No = cmbAP1.SelectedValue.ToStringForce();
        }

        private void cmbAP2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.AP2 = cmbAP2.Text;
            _model.AP2No = cmbAP2.SelectedValue.ToStringForce(); ;
        }

        private void cmbAp3_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.AP3 = cmbAp3.Text;
            _model.AP3No = cmbAp3.SelectedValue.ToStringForce(); ;
        }

        private void cmbSN1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.SN1 = cmbSN1.Text;
            _model.SN1No = cmbSN1.SelectedValue.ToStringForce(); ;
        }

        private void cmbSN2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.SN2 = cmbSN2.Text;
            _model.SN2No = cmbSN2.SelectedValue.ToStringForce(); ;
        }

        private void cmbON1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.ON1 = cmbON1.Text;
            _model.ON1No = cmbON1.SelectedValue.ToStringForce(); ;
        }

        private void cmbON2_SelectedIndexChanged(object sender, EventArgs e)
        {
            _model.ON2 = cmbON2.Text;
            _model.ON2No = cmbON2.SelectedValue.ToStringForce(); ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _model.AP1 = cmbAP1.Text;
            _model.AP1No = cmbAP1.SelectedValue.ToStringForce();
            _model.AP2 = cmbAP2.Text;
            _model.AP2No = cmbAP2.SelectedValue.ToStringForce();

            _model.AP3 = cmbAp3.Text;
            _model.AP3No = cmbAp3.SelectedValue.ToStringForce();

            _model.SN1 = cmbSN1.Text;
            _model.SN1No = cmbSN1.SelectedValue.ToStringForce();

            _model.SN2 = cmbSN2.Text;
            _model.SN2No = cmbSN2.SelectedValue.ToStringForce();
            _model.ON1 = cmbON1.Text;
            _model.ON1No = cmbON1.SelectedValue.ToStringForce();
            _model.ON2 = cmbON2.Text;
            _model.ON2No = cmbON2.SelectedValue.ToStringForce();
            _model.AP1 = cmbAP1.Text;
            _model.AP1No = cmbAP1.SelectedValue.ToStringForce();
            _model.AP2 = cmbAP2.Text;
            _model.AP2No = cmbAP2.SelectedValue.ToStringForce();

            _model.AP3 = cmbAp3.Text;
            _model.AP3No = cmbAp3.SelectedValue.ToStringForce();
            _model.SN1 = cmbSN1.Text;
            _model.SN1No = cmbSN1.SelectedValue.ToStringForce();
            _model.SN2 = cmbSN2.Text;
            _model.SN2No = cmbSN2.SelectedValue.ToStringForce();
            _model.ON1 = cmbON1.Text;
            _model.ON1No = cmbON1.SelectedValue.ToStringForce();
            _model.ON2 = cmbON2.Text;
            _model.ON2No = cmbON2.SelectedValue.ToStringForce(); ;
            _model.Oroom = cmbOroom.Text;
            _model.Second = tbSecond.Text;
            if (_model.Ostate < 1)
            {
                _model.Ostate = 1;
            }

            int RES = dal.UpdatePaiban(_model);
            if (RES > 0)
            {
                string message = Hl7Bll.AppendHL7stringConfig(_model.ID, Program.yh, Program.zhanghao);
                LogHelp.SaveLogHL7(message);
                string HL7IPaddress = ConfigurationManager.AppSettings["HL7IPaddress"];
                string HL7port = ConfigurationManager.AppSettings["HL7port"];
                SenderRoutingLib.SocketSender send = new SenderRoutingLib.SocketSender();
                object objResult;
                int iResult = 0;
                int count = 1;
                if (count < 10)
                {
                    new System.Threading.Thread(o =>
                    {
                        for (int i = 0; i < count; i++)
                        {
                            objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                            string ack = objResult == null ? string.Empty : objResult.ToString();
                            if (ack.Contains("AA"))
                            {
                                iResult++;
                                LogHelp.SaveLogHL7(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                _model.IsSend = 1;
                                dal.UpdatePaiban(_model);
                                // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                            }
                            else
                            {
                                iResult++;
                                LogHelp.SaveLogHL7(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                // SetText(string.Format("\r\n消息处理失败原因：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                            }
                        }
                    }).Start();
                }
                else
                {
                    for (int j = 0; j < 10; j++)
                    {
                        new System.Threading.Thread(o =>
                        {
                            for (int i = 0; i < count / 10; i++)
                            {
                                objResult = send.Send(message, HL7IPaddress, Convert.ToInt32(HL7port));
                                string ack = objResult == null ? string.Empty : objResult.ToString();
                                if (ack.Contains("AA"))
                                {
                                    iResult++;
                                    // SetText(string.Format("\r\n成功条数：{0} \r\n结束时间:{1}", iResult.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                }
                                else
                                {
                                    iResult++;
                                    //SetText(string.Format("\r\n发送失败，错误信息：{0} \r\n结束时间:{1}", ack, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                }
                            }
                        }).Start();
                    }
                }


                this.Close();
            }
            else
            { MessageBox.Show("保存排班失败，请重试！"); }
        }
    }
}

