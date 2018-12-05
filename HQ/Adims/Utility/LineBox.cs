using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace adims_Utility
{
    public partial class LineBox : UserControl
    {
        public LineBox()
        {
            InitializeComponent();
            tbValue.Location = new Point(lableName.Location.X + lableName.Width, lableName.Location.Y);
            tbValue.Size = new Size(this.Size.Width - lableName.Width, tbValue.Height);
            tbValue.DoubleClick += new EventHandler(LineBox_DoubleClick);
        }

        /// <summary>
        /// 定义事件
        /// </summary>
        public event EventHandler LineBoxDoubleClick;
        private void LineBox_DoubleClick(object sender, EventArgs e)
        {
            if (LineBoxDoubleClick != null)
                LineBoxDoubleClick(sender, new EventArgs());//把按钮自身作为参数传递
        }
       /// <summary>
       /// 画线
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void LineBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gra = this.CreateGraphics();
            SizeF size = gra.MeasureString(lableName.Text, this.Font);
            int LableWidth = (int)size.Width;
            tbValue.Location = new Point(lableName.Location.X + LableWidth + 10, lableName.Location.Y);
            gra.DrawLine(new Pen(Color.Black, 1), this.tbValue.Location.X - 10, this.tbValue.Location.Y + this.tbValue.Height + 1, this.Width + this.lableName.Width, this.tbValue.Location.Y + this.tbValue.Height + 1);
        }
        /// <summary>
        /// 定义Lable.Text属性
        /// </summary>
        public string LableText
        {
            get { return lableName.Text; }
            set
            {
                lableName.Text = value;
            }
        }
        /// <summary>
        /// 重写Text属性
        /// </summary>
        public override string Text
        {
            get { return tbValue.Text; }
            set { tbValue.Text = value; }
        }

        private void LineBox_Load(object sender, EventArgs e)
        {
            LineBox_Paint(null, null);
        }

        private void lableName_TextChanged(object sender, EventArgs e)
        {
            tbValue.Location = new Point(lableName.Location.X + lableName.Width + 5, lableName.Location.Y);
            tbValue.Size = new Size(this.Size.Width - lableName.Width, tbValue.Height);
            LineBox_Paint(null, null);
        }

        private void LineBox_Click(object sender, EventArgs e)
        {
            tbValue.Focus();
        }

        private void tbValue_Click(object sender, EventArgs e)
        {
            tbValue.Focus();
        }

    }
}
