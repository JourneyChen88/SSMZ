using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace main.OrgBusinessManage
{
    public partial class data_maintenance : Form
    {
        public data_maintenance()
        {
            InitializeComponent();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {          
            TreeNode node = e.Node;
            if (node.Nodes.Count == 0)//如果是最后一级的节点才需要将子窗体添加到主窗体里面
            {
                this.panel1.Controls.Clear();//每次点击节点都要刷新主窗体里面装载子窗体的容器
                string TreenodeText = e.Node.Text;
                string TreenodeName = e.Node.Name;
                if (TreenodeName == "")
                { MessageBox.Show("数据库中没有建立相应的表", "警告"); return; }
                data_add f = new data_add(TreenodeText, TreenodeName);//TreenodeText表示是哪一个树节点被单击了
                //AddForm(f);//将数据维护窗体添加的主窗体里面
                f.TopLevel=false;
                panel1.Width=f.Width ;
                panel1.Height=f.Height;
                panel1.Controls.Add(f);
                f.Show();
            }
        }

        private void data_maintenance_Load(object sender, EventArgs e)
        {
            //获取数据表名与维护数据之间的关联文本文件
           /*    string file =  @".\datatable.txt";
               string content = "已经维护的基本数据表的都只包含两个字段：id（int ,not null primarykey）name(varchar(50),null)";
               content += "\r\n";
               content += "维护的数据   表名";
               content += "\r\n";
               foreach (TreeNode mynode in treeView1.Nodes)
               {    foreach(TreeNode node in mynode.Nodes)
                   {
                       if (node.Name != "")
                           content += node.Text + "    " +node.Name+ "\r\n";
                   }
               }
               if (File.Exists(file) == true)
                    {
                    MessageBox.Show("存在此文件!");
                     }
               else            
              {                
                   FileStream myFs = new FileStream(file, FileMode.Create);                
                   StreamWriter mySw = new StreamWriter(myFs);                
                   mySw.Write(content);               
                   mySw.Close();                
                   myFs.Close();               
                   MessageBox.Show("写入成功");}
            */ 
        }
       }
}
