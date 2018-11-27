using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL;
using System.Web.UI.WebControls;
using adims_MODEL;
using Adims_Utility;

namespace main
{
    public partial class SelectSSYS : Form
    {
        public List<OperDoctorModel> DoctotList = new List<OperDoctorModel>();
        WindowsFormsControlLibrary5.UserControl1 u;
        string ysStr;
        string _PatId;
        public SelectSSYS(WindowsFormsControlLibrary5.UserControl1 o, string PatId)
        {
            _PatId = PatId;
            u = o;
            ysStr = o.Controls[0].Text;
            InitializeComponent();
        }
        /// <summary>
        /// 增加已选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            bool isExit = false;
            int i = 0;
            if (dgvChoice.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow dgvrow in dgvChoice.SelectedRows)
                {

                    foreach (var s in DoctotList)
                    {
                        i = 0;
                        if (dgvrow.Cells[1].Value.ToString() == s.DoctorNo)
                        {
                            isExit = true;
                            break;
                        }


                    }
                    if (!isExit)
                    {
                        DoctotList.Add(new OperDoctorModel
                        {
                            DoctorName = dgvrow.Cells[0].Value.ToString(),
                            DoctorNo = dgvrow.Cells[1].Value.ToString()
                        });

                    }

                }
                BinSelectedDgv();
                BindUcSSYS();

            }
        }

        private void BinSelectedDgv()
        {
            List<OperDoctorModel> list = new List<OperDoctorModel>();
            dgvSelected.DataSource = list;
            dgvSelected.DataSource = DoctotList;
            
        }

        private void BindUcSSYS()
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
        /// <summary>
        /// 去除已选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvSelected.CurrentRow != null)
            {
                DoctotList.RemoveAt(dgvSelected.CurrentRow.Index);
                BinSelectedDgv();
                BindUcSSYS();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        adims_DAL.AdimsProvider dal = new AdimsProvider();
        DataTable dtYS = new DataTable();
        private void SelectSSYS_Load(object sender, EventArgs e)
        {

            BindSSYS();
            //绑定已选列表
            DataTable dt = PaibanDal.GetPiabanOsAndNo(_PatId);
            DoctotList = ListHelper<OperDoctorModel>.ConvertToList(dt);
            BinSelectedDgv();
        }
        /// <summary>
        /// 绑定备选列表
        /// </summary>
        private void BindSSYS()
        {
            dtYS = dal.GetShoushuYisheng(tbJiansuo.Text.Trim());
            dgvChoice.DataSource = dtYS;
        }


        private void tbJiansuo_TextChanged(object sender, EventArgs e)
        {
            BindSSYS();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            bool isExit = false;
            int i = 0;
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
                BindUcSSYS();
            }
        }
    }
}
