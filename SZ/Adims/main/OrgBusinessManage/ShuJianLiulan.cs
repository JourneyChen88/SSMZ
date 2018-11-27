using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace main
{
    public partial class sjll1: Form
    {
        int x = 1, y = 1, HEGHT, WEIGHT,palcount=1;
        string OROOM = "手术间";
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController();
        List<adims_MODEL.oroomstate> oroom = new List<adims_MODEL.oroomstate>();
        public sjll1()
        {
            InitializeComponent();
        }

        private void sjll_Load(object sender, EventArgs e)
        {
            WEIGHT = this.Width / 3;
            HEGHT = this.Height / 4;
            bll.seloroomstate(oroom);
            for (int i = 0; i < oroom.Count; i++)
            {            
                GroupBox newPAL = new GroupBox();
                palcount++;
                newPAL.Width = WEIGHT - 5;
                newPAL.Name = oroom[i].Oname;
                newPAL.Height = HEGHT - 5;
                newPAL.DoubleClick += new EventHandler(newPAL_DoubleClick);
                newPAL.Location = new Point(x, y);
                newPAL.Font=new Font("楷体", 13);
                newPAL.BackColor = Color.LightYellow;
                this.Controls.Add(newPAL);
                newPAL.Text = oroom[i].Oname;
                if (oroom[i].Ostate!=0)
	            {
                     Label lb = new Label();
                     lb.AutoSize = true;
                     lb.Text = "麻醉编号：" + oroom[i].Mzjldid;
                     lb.Font = new Font("楷体", 10);
                     lb.ForeColor = Color.Blue; 
                     newPAL.Controls.Add(lb);                    
                     lb.Location = new Point(10, 30);
                     newPAL.Controls.Add(lb);
                     lb.BringToFront();

                     Label lb2 = new Label();
                     lb2.AutoSize = true; 
                     lb2.Text = "病人编号：" + oroom[i].Patid;
                     lb2.Font = new Font("楷体", 10);
                     lb2.ForeColor = Color.Blue;
                     newPAL.Controls.Add(lb2);
                     lb2.Location = new Point(10, 60);
                     lb2.Visible=true;                     

                     //Label lb3 = new Label();
                     //newPAL.Controls.Add(lb3);
                     //lb3.Font = new Font("楷体", 10);
                     //lb3.ForeColor = Color.Blue;
                     //lb3.Location = new Point(10,75);
                     //lb3.Text = "开始时间：" + DateTime.Now.ToLongTimeString();
	            }
                else
                {
                    Label lb = new Label();
                    lb.AutoSize = true;
                    newPAL.Controls.Add(lb);
                    lb.Text = "当前无手术";
                    lb.Font = new Font("楷体", 13);
                    lb.ForeColor = Color.Red;                   
                    lb.Location = new Point( 10,  50);                    
                }
                x = x + WEIGHT;
                if (x >= (this.Width - 10))
                {
                    x = 1;
                    y = y + HEGHT;
                }
            }

        }
        [DllImport("User32.dll")]
        private static extern IntPtr WindowFromPoint(Point p);
        void newPAL_DoubleClick(object sender, EventArgs e)
        {
            Point p = Cursor.Position;
            IntPtr h = WindowFromPoint(p);
            foreach (Control con in this.Controls)
            {
                if (con.Handle == h)
                {
                    string OROOM = con.Name;                   
                    DataTable dt = bll.slectOroomINFO(OROOM);
                    if (Convert.ToInt32(dt.Rows[0][0]) != 0)
                    {
                        //this.Close();
                        mzjldEdit mzjldform = new mzjldEdit(dt.Rows[0][2].ToString(),OROOM,Convert.ToDateTime(DateTime.Now.ToShortDateString()),Convert.ToInt32(dt.Rows[0][1]),true);
                        mzjldform.ShowDialog();
                    }
                }
            }
        }

        //private void sjll1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
           
        //    if (e.X > 1 && e.X < this.Width / 3 - 5 && e.Y < this.Height / 4 - 5)
        //    {
        //        OROOM = "手术间1";
        //    }
        //    if (e.X > this.Width / 3 && e.X < this.Width * 2 / 3 - 5 && e.Y < this.Height / 4 - 5)
        //    {
        //        OROOM = "手术间2";
        //    }
        //    if (e.X > this.Width * 2 / 3 && e.X < this.Width - 5 && e.Y < this.Height / 4 - 5)
        //    {
        //        OROOM = "手术间3";
        //    }
        //    if (e.X > 1 && e.X < this.Width / 3 - 5 && e.Y > this.Height / 4 && e.Y < this.Height * 2 / 4 - 5)
        //    {
        //        OROOM = "手术间4";
        //    }
        //    if (e.X > this.Width / 3 && e.X < this.Width * 2 / 3 - 5 && e.Y > this.Height / 4 && e.Y < this.Height * 2 / 4 - 5)
        //    {
        //        OROOM = "手术间5";
        //    }
        //    if (e.X > this.Width * 2 / 3 && e.X < this.Width - 5 && e.Y > this.Height / 4 && e.Y < this.Height * 2 / 4 - 5)
        //    {
        //        OROOM = "手术间6";
        //    }
        //    if (e.X > 1 && e.X < this.Width / 3 - 5 && e.Y > this.Height * 2 / 4 && e.Y < this.Height * 3 / 4 - 5)
        //    {
        //        OROOM = "手术间7";
        //    }
        //    if (e.X > this.Width / 3 && e.X < this.Width * 2 / 3 - 5 && e.Y > this.Height * 2 / 4 && e.Y < this.Height * 3 / 4 - 5)
        //    {
        //        OROOM = "手术间8";
        //    }

        // }
         
            
    }
       
}

