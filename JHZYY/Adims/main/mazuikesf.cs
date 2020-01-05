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
    public partial class mazuikesf : Form
    {
        adims_BLL.YXL_BLL cll = new adims_BLL.YXL_BLL();
        adims_DAL.mz dal = new adims_DAL.mz();
        adims_BLL.mz bll = new adims_BLL.mz();
        public mazuikesf()
        {
            InitializeComponent();
        }

        private void mazuikesf_Load(object sender, EventArgs e)
        {
            infochangfuliao();
        }

        private void infochangfuliao()
        {
            try
            {
                DataTable dt = cll.shoushufei("3");
                if (dt.Rows.Count == 0)
                {
                    DataTable dtr = cll.shoushufei("1");
                    for (int i = 0; i < dtr.Rows.Count; i++)
                    {
                        if (i >= datagrid.Rows.Count)
                        {
                            datagrid.Rows.Add();
                        }
                        datagrid.Rows[i].Cells[0].Value = dtr.Rows[i]["ITEM_NAME"];
                        datagrid.Rows[i].Cells[1].Value = dtr.Rows[i]["ITEM_SPEC"];
                        datagrid.Rows[i].Cells[2].Value = dtr.Rows[i]["UNITS"];
                        datagrid.Rows[i].Cells[3].Value = dtr.Rows[i]["AMOUNT"];
                        
                    }
                    datagrid.AllowUserToAddRows = false;
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i >= datagrid.Rows.Count)
                        {
                            datagrid.Rows.Add();
                        }
                      datagrid.Rows[i].Cells[4].Value = dt.Rows[i]["ITEM_NAME"];
                        datagrid.Rows[i].Cells[5].Value = dt.Rows[i]["ITEM_SPEC"];
                        datagrid.Rows[i].Cells[6].Value = dt.Rows[i]["UNITS"];
                        datagrid.Rows[i].Cells[7].Value = dt.Rows[i]["AMOUNT"];
                    }
                    datagrid.AllowUserToAddRows = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "止血带包清点加载失败");
            }
        }//敷料加载
    }
}
