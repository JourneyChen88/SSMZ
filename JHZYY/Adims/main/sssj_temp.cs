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
    public partial class sssj_temp : Form
    {
        public sssj_temp()
        {
            InitializeComponent();
        }


        private void label25_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sssj_temp_Load(object sender, EventArgs e)
        {
            this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            this.Top = 0;
            timer1.Enabled = true;
        }
        int HR = 60;
        double Spo2 = 90.00;
        int Pulse =60 ;
        int Sys = 90;
        int Dia = 60;
        int Map = 105;
        int Art = 60;
        double Cvp = 0.49;
        private void timer1_Tick(object sender, EventArgs e)
        {
            HR++;
            if (HR == 90) HR = 60;
            Spo2 += 0.5;
            if (Spo2 == 99.5) Spo2 = 90.00;
            Pulse++;
            if (Pulse == 100) Pulse = 60;
            Sys++;
            if (Sys == 139) Sys = 90;
            Dia++;
            if (Dia == 89) Dia = 60;
            Map++;
            if (Map == 185) Map = 105;
            Art++;
            if (Art == 100) Art = 60;
            Cvp+=0.05;
            if (Cvp == 1.18) Cvp = 0.49;
            HR_lbl.Text = HR.ToString();
            Spo2_lbl.Text = Spo2.ToString();
            Pulse_lbl.Text = Pulse.ToString();
            NIBP_lbl.Text = Sys.ToString() + "/" + Dia.ToString();
        //  ART_lbl.Text = Art.ToString();
        //  CVP_lbl.Text = Cvp.ToString();
        }
    }
}
