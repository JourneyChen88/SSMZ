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
    public partial class addJhxm : Form
    {
        List<string> jhxma;
        List<string> jhxmy;
        int mzjldID;
        int type;
        adims_BLL.AdimsController bll = new adims_BLL.AdimsController(); 
        public addJhxm(List<string>l1,List<string>l2,int mzid,int type1)
        {
            type = type1;
            jhxma = l1;
            jhxmy = l2;
            mzjldID = mzid;
            InitializeComponent();
        }

        private void addjhxm_Load(object sender, EventArgs e)
        {
            foreach (string s in jhxma)
                listBox1.Items.Add(s);
            foreach (string s in jhxmy)
                listBox2.Items.Add(s);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                int i = bll.addJhxm(mzjldID,listBox1.SelectedItem.ToString(),type);
                if (i>0)
                {
                    //jhxma.Remove(listBox1.SelectedItem.ToString());
                    jhxmy.Add(listBox1.SelectedItem.ToString());
                    listBox2.Items.Add(listBox1.SelectedItem.ToString());
                    //listBox1.Items.RemoveAt(listBox1.SelectedIndex);

                }
             }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {
                int i = bll.DeleteJhxm(mzjldID, listBox2.SelectedItem.ToString(),type);
                if (i > 0)
                {
                    jhxmy.Remove(listBox2.SelectedItem.ToString());
                    //jhxma.Add(listBox2.SelectedItem.ToString());
                    //listBox1.Items.Add(listBox2.SelectedItem.ToString());
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex); 
                }           
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
