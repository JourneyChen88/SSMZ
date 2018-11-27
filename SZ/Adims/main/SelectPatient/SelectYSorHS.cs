using adims_MODEL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsControlLibrary5;
using Adims_Utility;
using adims_DAL;

namespace main
{
    public partial class SelectYSorHS : Form
    {
        public List<OperDoctorModel> DoctotList = new List<OperDoctorModel>();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        adims_DAL.AdimsProvider dal = new adims_DAL.AdimsProvider();
        /// <summary>
        /// 1麻醉医生；2巡回护士；3器械护士
        /// </summary>
        int Type = 0;
        WindowsFormsControlLibrary5.UserControl1 u;
        string _PatId;
        string ysStr;
        public SelectYSorHS(int type, WindowsFormsControlLibrary5.UserControl1 o, string patid)
        {
            _PatId = patid;
            Type = type;
            u = o;
            ysStr = o.Controls[0].Text;
            InitializeComponent();
        }

        private void SelectYSorHS_Load(object sender, EventArgs e)
        {
            if (Type == 1)
            {
                groupBox1.Text = "麻醉医生待选列表";

                DataTable dt = PaibanDal.GetPiabanOA(_PatId);
                DoctotList = ListHelper<OperDoctorModel>.ConvertToList(dt);
            }
            else if (Type == 2)
            {
                groupBox1.Text = "护士待选列表";
                DataTable dt = PaibanDal.GetPiabanON(_PatId);
                DoctotList = ListHelper<OperDoctorModel>.ConvertToList(dt);
            }
            else
            { groupBox1.Text = "护士待选列表";
                DataTable dt = PaibanDal.GetPiabanSN(_PatId);
                DoctotList = ListHelper<OperDoctorModel>.ConvertToList(dt);
            }
                

            BindSSYS();
            BinSelectedDgv();
        }
        /// <summary>
        /// 绑定备选列表
        /// </summary>
        private void BindSSYS()
        {
            int userType = 1;
            if (Type==3|| Type == 2)
            {
                userType = 2;
            }
            else
            {
                userType = 1;
            }
            DataTable dt = bll.selectUserName(userType);
            listChoice.DataSource = dt;
            listChoice.DisplayMember = "姓名";
            listChoice.ValueMember = "工号";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnConfig_Click(object sender, EventArgs e)
        {

            bool isExit = false;
            foreach (var s in DoctotList)
            {
                if (s.DoctorName == tbMannul.Text.Trim())
                    isExit = true;

            }
            if (isExit)
                MessageBox.Show("已添加");
            else
            {
                DoctotList.Add(new OperDoctorModel { DoctorName = tbMannul.Text.Trim(), DoctorNo = string.Empty });
                dgvSelected.DataSource = DoctotList.ToList();
                dgvSelected.Refresh();
                BindUcControl();
            }
        }
        private void BindUcControl()
        {
            string qs = string.Empty;
            foreach (var s in DoctotList)
            {
                if (qs == "")
                { qs = s.DoctorName; }
                else
                    qs = qs + "、" + s.DoctorName;
            }
            u.Controls[0].Text = qs;

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            bool isExit = false;
            int i = 0;
            if (listChoice.SelectedItems.Count > 0)
            {
                foreach (DataRowView dgvrow in listChoice.SelectedItems)
                {

                    foreach (var s in DoctotList)
                    {
                        i = 0;
                        if (dgvrow[1].ToString() == s.DoctorNo)
                        {
                            isExit = true;
                            break;
                        }


                    }
                    if (!isExit)
                    {
                        DoctotList.Add(new OperDoctorModel
                        {
                            DoctorName = dgvrow[0].ToString(),
                            DoctorNo = dgvrow[1].ToString()
                        });

                    }

                }
                BinSelectedDgv();
                BindUcControl();

            }

        }
        private void BinSelectedDgv()
        {
            List<OperDoctorModel> list = new List<OperDoctorModel>();
            dgvSelected.DataSource = list;
            dgvSelected.DataSource = DoctotList;

        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dgvSelected.CurrentRow != null)
            {
                DoctotList.RemoveAt(dgvSelected.CurrentRow.Index);
                BinSelectedDgv();
                BindUcControl();
            }
        }

        private void tbJiansuo_TextChanged(object sender, EventArgs e)
        {
            BindSSYS();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}