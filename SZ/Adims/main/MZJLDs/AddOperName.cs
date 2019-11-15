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
using System.Windows.Forms;

namespace main
{
    public partial class AddOperName : Form
    {
        List<PatOperation> PatOperationList = new List<PatOperation>();
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        WindowsFormsControlLibrary5.UserControl1 u;
        int MzjldId;
        public AddOperName(WindowsFormsControlLibrary5.UserControl1 o, int mzid)
        {
            u = o;
            MzjldId = mzid;
            InitializeComponent();
        }
        public AddOperName(WindowsFormsControlLibrary5.UserControl1 o)
        {
            u = o;
            InitializeComponent();
        }
        DB2help HisHelp = new DB2help();
        DataTable OperNameTable = new DataTable();
        adims_DAL.AdimsProvider dal = new AdimsProvider();
        private void osel_Load(object sender, EventArgs e)
        {
            BindDgvSelected();
            OperNameTable = HisHelp.GetHisOperName("");

             dgvOperList.DataSource = OperNameTable.DefaultView;
            dgvOperList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        PatOperationDal _PatOperationDal = new PatOperationDal();
        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvOperList.SelectedRows.Count > 0)
            {
                PatOperation po = new PatOperation();
                po.OperCode = dgvOperList.CurrentRow.Cells[0].Value.ToString();
                po.OperName = dgvOperList.CurrentRow.Cells[1].Value.ToString();
                po.OperLevel = dgvOperList.CurrentRow.Cells[2].Value.ToString();
                po.CutType = dgvOperList.CurrentRow.Cells[3].Value.ToString();


                DataTable dt = _PatOperationDal.GetPatOperationByCode(MzjldId, po.OperCode);
                if (dt.Rows.Count == 0)
                {

                    int res = _PatOperationDal.InsertPatOperation(po, MzjldId);
                    if (res > 0)
                    {
                        BindDgvSelected();
                    }

                }
                else
                {
                    MessageBox.Show("该手术已添加！");
                }


            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() != "")
            {
                string select = string.Format("INPUTSTR like '%{0} %' or 手术名称 like '%{0}%'", textBox1.Text);
                DataRow[] drs = OperNameTable.Select(select);
                dgvOperList.DataSource = Adims_Utility.TypeExtension.ToDataTable(drs);
            }
            else
            {
                dgvOperList.DataSource = OperNameTable;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvSelected.SelectedRows.Count > 0)
            {
                PatOperation po = new PatOperation();
                int id = dgvSelected.CurrentRow.Cells["id"].Value.ToInt32();

                int res = _PatOperationDal.DelPatOperation(id);
                if (res > 0)
                {
                    BindDgvSelected();
                }

            }
        }
        /// <summary>
        /// 绑定已选列表
        /// </summary>

        public void BindDgvSelected()
        {
            DataTable dt = _PatOperationDal.GetPatOperation2(MzjldId);
            dgvSelected.DataSource = dt;
        }

        private void AddOperName_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var item in PatOperationList)
            {
                u.Controls[0].Text += " + " + item.OperName;
            }
        }
    }
}
