﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using adims_DAL; 

namespace main
{
    public partial class MZZJ_Select : Form
    {
        AdimsProvider apro=new AdimsProvider();
        public MZZJ_Select()
        {
            InitializeComponent();
        }

        private void DataBind()
        {
            DataTable dt1 = apro.GetmazuizongjieList(dateTimePicker1.Value.Date.ToString("yyyy-MM-dd"));
            dataGridView1.DataSource = dt1.DefaultView;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string patID = dataGridView1.CurrentRow.Cells["住院号"].Value.ToString();
             //   BLbbsjdan f2 = new BLbbsjdan(patID);
                //BeforeVisit_HS f2 = new BeforeVisit_HS(patID);
                //f2.ShowDialog();
            }
            else MessageBox.Show("请选择病人！");


            if (dataGridView1.Rows.Count== 0)
            {

                MessageBox.Show("请选择病人！");
            }
            else
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                MZZJ_CJ f2 = new MZZJ_CJ(mzjldid, patID);
                f2.ShowDialog();
            }

        }

        private void Mzzongjie_Load(object sender, EventArgs e)
        {
           
            DataBind();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {

                MessageBox.Show("请选择病人！");
            }
            else
            {
                string mzjldid = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                string patID = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string patNAME = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string patSEX = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                string patAGE = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                MZZJ_CJ f2 = new MZZJ_CJ(mzjldid, patID);
                f2.ShowDialog();
            }
        }

      
    }
}