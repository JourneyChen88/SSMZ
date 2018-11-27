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
    public partial class XiugaiYuanYin : Form
    {
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        int mzjldid, lx,xgqvalue, xghvalue;//麻醉记录单ID，检测类型，修改前值，修改后值
        DateTime xgdtime,dtnow;//监测点点时间，日志增加时间
        public XiugaiYuanYin(int mzid,int LX,int XGQZ,int XGHZ,DateTime XGDTIME,DateTime DTIME)
        {
            mzjldid = mzid;
            lx = LX;
            xgqvalue = XGQZ;
            xghvalue = XGHZ;
            xgdtime = XGDTIME;
            dtnow = DTIME;

            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            adims_MODEL.point point1=new adims_MODEL.point();
            point1.V=xghvalue;
            point1.D=xgdtime;
            point1.Lx = lx;
            int result1 = 0, result2 = 0;
            string xgyy=comboBox1.Text;
            result1 = bll.addSljlLog(mzjldid,lx,xgyy,xgqvalue,xghvalue,xgdtime,dtnow);
            result2 = bll.xgpoint(mzjldid, point1);
            if (result1 > 0 && result2 > 0)
            {
                MessageBox.Show("修改日志添加成功！");
                this.Close();
            }
            else
                MessageBox.Show("修改日志添加不成功，请重新添加！");
        }

       
    }
}
