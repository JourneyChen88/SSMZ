///************************************
///Updated By        : Senvi
///************************************

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
    public partial class SelectDoctor : Form
    {
        #region <<Members>>

        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int dr, dc;
        DataGridView dgv;

        #endregion

        #region <<Constructors>>

        public SelectDoctor(DataGridView d, int r, int c)
        {
            dgv = d;
            dr = r;
            dc = c;
            InitializeComponent();
        }

        #endregion

        #region <<Events>>

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selaa_Load(object sender, EventArgs e)
        {
            string sqlWhere = string.Empty;
            if (dc == 15 || dc == 13 || dc == 14 || dc == 2 || dc == 3 || dc == 4 || dc == 1 || dc == 5)
                sqlWhere = " PostType = 0 ";
            else if (dc == 17 || dc == 16)
                sqlWhere = " PostType = 1 ";
            else if (dc == 18 || dc == 18)
                sqlWhere = " PostType = 2 ";
            DataTable dt = bll.GetSurgeryStaff(sqlWhere);
            foreach (DataRow dr in dt.Rows)
            {
                int result = Convert.ToInt32(dr["PostType"]);
                switch (result)
                {
                    case 0:
                        dr["PostType"] = "麻醉医师";
                        break;
                    case 1:
                        dr["PostType"] = "洗手护士";
                        break;
                    case 2:
                        dr["PostType"] = "巡回护士";
                        break;
                }
            }
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            dataGridView1.DataSource = dt.DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MZH_ZhengTong mm = new MZH_ZhengTong();
            int result = 0;
            switch (dc)
            {
                case 2:
                    dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
                    this.Close();
                    break;
                case 3:
                   dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
                   this.Close();
                   break;
                case 4:
                    dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
                    this.Close();
                    break;
                case 5:
                    dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
                    this.Close();
                    break;

                case 1:
                    dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
                    this.Close();
                    break;
                case 18:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "ap1", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
                case 13:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "ap2", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
                case 14:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "ap3", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
                case 15:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "on1", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
                case 16:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "on2", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
                case 17:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "sn1", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
                case 19:
                    result = bll.UpdateOTypesetting(Convert.ToString(dgv.Rows[dr].Cells[2].Value), "sn2", Convert.ToString(dataGridView1.SelectedCells[1].Value));
                    break;
            }
            if (result == 1)
            {
                dgv.Rows[dr].Cells[dc].Value = dataGridView1.SelectedCells[1].Value;
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
