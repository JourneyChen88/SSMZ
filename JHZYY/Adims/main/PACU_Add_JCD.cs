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
    public partial class PACU_Add_JCD : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        
        string MZID,ID;
        int Type;
        public PACU_Add_JCD(string mzid,string id,int type)
        {
            ID = id;
            Type = type;
            MZID = mzid;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Type==1)
            {
                DateTime dtTime = dtTimeShort.Value;
                bll.AddPACU_DATA(MZID, dtTime.ToLongTimeString());
                this.Close();
            }
            if (Type == 2)
            {
                DateTime dtTime = dtTimeShort.Value;
                bll.UpdatePACU_DATA(ID, dtTime);
                this.Close();
            }
          
        }

        private void PACU_Add_JCD_Load(object sender, EventArgs e)
        {
            this.dtTimeShort.Format = DateTimePickerFormat.Custom;
            this.dtTimeShort.CustomFormat = "yy-MM-dd HH:mm";
            
        }
    }
}