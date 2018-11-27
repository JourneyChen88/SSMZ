using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace main.OrgBusinessManage
{
    public partial class Fsjwfct : Form
    {

        string TreenodeText = "";
        public Fsjwfct(string str)
        {
            InitializeComponent();
            TreenodeText = str;
        }


        //<summary>
        //增加
        //</summary>
        //<param name="sender"></param>
        //<param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
             string text = this.textBox1.Text;
            switch (TreenodeText)
            {
                case "证书名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhengshu");//判断是否已经添加此证书名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "zhengshu");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengshu");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("证书名称不能为空");
                    }

                    break;
                case "职务名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhiwu");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "zhiwu");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhiwu");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("证书名称不能为空");
                    }

                    break;
                case "职称名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhicheng");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "zhicheng");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhicheng");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("证书名称不能为空");
                    }

                    break;
                case "专业名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhuanye");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "zhuanye");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhuanye");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("专业名称不能为空");
                    }

                    break;

                case "语种名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yuzhong");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "yuzhong");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yuzhong");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("语种名称不能为空");
                    }

                    break;
                case "学历名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "xueli");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "xueli");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xueli");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("学历名称不能为空");
                    }

                    break;
                case "奖惩名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "jiangcheng");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "jiangcheng");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jiangcheng");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("奖惩名称不能为空");
                    }

                    break;
                case "床号":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "chuanghao");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "chuanghao");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("chuanghao");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("床号名称不能为空");
                    }

                    break;
                case "科室名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "keshi");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "keshi");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("keshi");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("科室名称不能为空");
                    }

                    break;
                case "病区名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "bingqu");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "bingqu");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingqu");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("病区名称不能为空");
                    }

                    break;
                case "手术间名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "shoushujian");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "shoushujian");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shoushujian");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("手术间名称不能为空");
                    }

                    break;
                case "麻醉大类":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuidalei");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuidalei");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuidalei");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉大类不能为空");
                    }

                    break;
                case "麻醉平面":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuipingmian");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuipingmian");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuipingmian");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉平面不能为空");
                    }

                    break;
                case "麻醉范围":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuifanwei");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuifanwei");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuifanwei");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉范围不能为空");
                    }

                    break;
                case "麻醉效果":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuixiaoguo");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuixiaoguo");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuixiaoguo");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉效果不能为空");
                    }

                    break;
                case "麻醉气体":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuiqiti");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuiqiti");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiqiti");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉气体不能为空");
                    }

                    break;
                case "麻醉顾虑":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuigulu");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuigulu");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuigulu");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉顾虑不能为空");
                    }

                    break;
                case "麻醉问题":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuiwenti");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuiwenti");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiwenti");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉问题不能为空");
                    }

                    break;
                case "麻醉适应症":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuishiyingzheng");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "mazuishiyingzheng");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuishiyingzheng");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉适应症不能为空");
                    }

                    break;
                case "病因":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "bingying");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "bingying");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingying");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("病因不能为空");
                    }

                    break;
                case "病种":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "bingzhong");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "bingzhong");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingzhong");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("病种不能为空");
                    }

                    break;
                case "ASA":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "ASA");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "ASA");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("ASA");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ASA不能为空");
                    }

                    break;
                case "疾病症状":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "jibingzhengzhuang");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "jibingzhengzhuang");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jibingzhengzhuang");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("疾病症状不能为空");
                    }

                    break;
                case "用药效果":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yongyaoxiaoguo");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "yongyaoxiaoguo");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yongyaoxiaoguo");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("用药效果不能为空");
                    }

                    break;
                case "精神状态":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "jingshengzhuangtai");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "jingshengzhuangtai");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jingshengzhuangtai");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("精神状态不能为空");
                    }

                    break;
                case "系统疾病":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "xitongjibing");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "xitongjibing");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xitongjibing");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("系统疾病不能为空");
                    }

                    break;
                case "活动能力":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "huodongnengli");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "huodongnengli");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("huodongnengli");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("活动能力不能为空");
                    }

                    break;
                case "药品名称":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinmingcheng");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "yaopinmingcheng");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinmingcheng");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("药品名称不能为空");
                    }

                    break;
                case "药品规格":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinguige");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "yaopinguige");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinguige");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("药品规格不能为空");
                    }

                    break;
                case "药品产地":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinchandi");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "yaopinchandi");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinchandi");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("药品产地不能为空");
                    }

                    break;
                case "药品剂型":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinjixing");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "yaopinjixing");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinjixing");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("药品剂型不能为空");
                    }

                    break;
                case "整量单位":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhengliangdanwei");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "zhengliangdanwei");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengliangdanwei");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("整量单位不能为空");
                    }

                    break;
                case "散量单位":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "sanliangdanwei");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "sanliangdanwei");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("sanliangdanwei");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("散量单位不能为空");
                    }

                    break;
                case "毒理类别":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "dulileibie");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "dulileibie");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("dulileibie");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("毒理类别不能为空");
                    }

                    break;
                case "物理状态":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "wulizhuangtai");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "wulizhuangtai");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("wulizhuangtai");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("物理状态不能为空");
                    }

                    break;
                case "最小剂量单位":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zuixiaojiliangdanwei");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "zuixiaojiliangdanwei");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zuixiaojiliangdanwei");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("最小剂量单位不能为空");
                    }

                    break;
                case "诱导用药":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "youdaoyongyao");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "youdaoyongyao");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("youdaoyongyao");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("诱导用药不能为空");
                    }

                    break;
                case "术前用药":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "shuqianyongyao");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "shuqianyongyao");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shuqianyongyao");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("术前用药不能为空");
                    }

                    break;
                case "给药方式":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "geiyaofangshi");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "geiyaofangshi");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("geiyaofangshi");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("给药方式不能为空");
                    }

                    break;
                case "过敏药物":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "guominyaowu");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "guominyaowu");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("guominyaowu");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("过敏药物不能为空");
                    }

                    break;
                case "麻醉医生":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "Adims_SurgeryStaff WHERE PostType = 0");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "Adims_SurgeryStaff WHERE PostType = 0");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("Adims_SurgeryStaff WHERE PostType = 0");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("麻醉医生不能为空");
                    }

                    break;
                case "护士":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "hushi");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "hushi");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("hushi");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("护士不能为空");
                    }

                    break;
                case "请假类型":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "qingjialeixing");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "qingjialeixing");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("qingjialeixing");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请假类型不能为空");
                    }

                    break;
                case "排班时间":

                    if (!string.IsNullOrEmpty(text))
                    {
                        bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "shijian");//判断是否已经添加此职务名称
                        if (IsHaveName)
                        {
                            MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "保存失败");
                        }
                        else
                        {
                            adims_BLL.AdimsController.AddData1(text, "shijian");
                            this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shijian");
                            MessageBox.Show(this.textBox1.Text + "添加成功");
                        }
                    }
                    else
                    {
                        MessageBox.Show("排班时间不能为空");
                    }

                    break;

            }
            IsEnabled();

        }

        private void Fsjwfct_Load(object sender, EventArgs e)
        {

            switch (TreenodeText)
            {
                case "证书名称":
                    {
                        this.label1.Text = "证书名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengshu");
                        this.dataGridView1.Columns[0].HeaderText = "证书编号";
                        this.dataGridView1.Columns[1].HeaderText = "证书名称";


                    }
                    break;
                case "职务名称":
                    {
                        this.label1.Text = "职务名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhiwu");
                        this.dataGridView1.Columns[0].HeaderText = "职务名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "职务名称";
                    }
                    break;
                case "职称名称":
                    {
                        this.label1.Text = "职称名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhicheng");
                        this.dataGridView1.Columns[0].HeaderText = "职称名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "职称名称";
                    }
                    break;
                case "专业名称":
                    {
                        this.label1.Text = "专业名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhuanye");
                        this.dataGridView1.Columns[0].HeaderText = "专业名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "专业名称";
                    }
                    break;
                case "语种名称":
                    {
                        this.label1.Text = "语种名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yuzhong");
                        this.dataGridView1.Columns[0].HeaderText = "语种名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "语种名称";
                    }
                    break;
                case "学历名称":
                    {
                        this.label1.Text = "学历名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xueli");
                        this.dataGridView1.Columns[0].HeaderText = "学历名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "学历名称";
                    }
                    break;
                case "奖惩名称":
                    {
                        this.label1.Text = "奖惩名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jiangcheng");
                        this.dataGridView1.Columns[0].HeaderText = "奖惩名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "奖惩名称";
                    }
                    break;
                case "床号":
                    {
                        this.label1.Text = "床号";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("chuanghao");
                        this.dataGridView1.Columns[0].HeaderText = "床号编号";
                        this.dataGridView1.Columns[1].HeaderText = "床号";
                    }
                    break;
                case "科室名称":
                    {
                        this.label1.Text = "科室名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("keshi");
                        this.dataGridView1.Columns[0].HeaderText = "科室名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "科室名称";
                    }
                    break;
                case "病区名称":
                    {
                        this.label1.Text = "病区名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingqu");
                        this.dataGridView1.Columns[0].HeaderText = "病区名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "病区名称";
                    }
                    break;
                case "手术间名称":
                    {
                        this.label1.Text = "手术间名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shoushujian");
                        this.dataGridView1.Columns[0].HeaderText = "手术间名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "手术间名称";
                    }
                    break;
                case "麻醉大类":
                    {
                        this.label1.Text = "麻醉大类";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuidalei");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉大类编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉大类";
                    }
                    break;
                case "麻醉平面":
                    {
                        this.label1.Text = "麻醉平面";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuipingmian");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉平面编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉平面";
                    }
                    break;
                case "麻醉范围":
                    {
                        this.label1.Text = "麻醉范围";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuifanwei");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉范围编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉范围";
                    }
                    break;
                case "麻醉效果":
                    {
                        this.label1.Text = "麻醉效果";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuixiaoguo");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉效果编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉效果";
                    }
                    break;
                case "麻醉气体":
                    {
                        this.label1.Text = "麻醉气体";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiqiti");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉气体编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉气体";
                    }
                    break;
                case "麻醉顾虑":
                    {
                        this.label1.Text = "麻醉顾虑";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuigulu");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉顾虑编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉顾虑";
                    }
                    break;
                case "麻醉问题":
                    {
                        this.label1.Text = "麻醉问题";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiwenti");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉问题编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉问题";
                    }
                    break;
                case "麻醉适应症":
                    {
                        this.label1.Text = "麻醉适应症";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuishiyingzheng");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉适应症编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉适应症";
                    }
                    break;
                case "病因":
                    {
                        this.label1.Text = "病因";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingying");
                        this.dataGridView1.Columns[0].HeaderText = "病因编号";
                        this.dataGridView1.Columns[1].HeaderText = "病因";
                    }
                    break;
                case "病种":
                    {
                        this.label1.Text = "病种";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingzhong");
                        this.dataGridView1.Columns[0].HeaderText = "病种编号";
                        this.dataGridView1.Columns[1].HeaderText = "病种";
                    }
                    break;
                case "ASA":
                    {
                        this.label1.Text = "ASA";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("ASA");
                        this.dataGridView1.Columns[0].HeaderText = "ASA编号";
                        this.dataGridView1.Columns[1].HeaderText = "ASA";
                    }
                    break;
                case "疾病症状":
                    {
                        this.label1.Text = "疾病症状";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jibingzhengzhuang");
                        this.dataGridView1.Columns[0].HeaderText = "疾病症状编号";
                        this.dataGridView1.Columns[1].HeaderText = "疾病症状";
                    }
                    break;
                case "用药效果":
                    {
                        this.label1.Text = "用药效果";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yongyaoxiaoguo");
                        this.dataGridView1.Columns[0].HeaderText = "用药效果编号";
                        this.dataGridView1.Columns[1].HeaderText = "用药效果";
                    }
                    break;
                case "精神状态":
                    {
                        this.label1.Text = "精神状态";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jingshengzhuangtai");
                        this.dataGridView1.Columns[0].HeaderText = "精神状态编号";
                        this.dataGridView1.Columns[1].HeaderText = "精神状态";
                    }
                    break;
                case "系统疾病":
                    {
                        this.label1.Text = "系统疾病";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xitongjibing");
                        this.dataGridView1.Columns[0].HeaderText = "系统疾病编号";
                        this.dataGridView1.Columns[1].HeaderText = "系统疾病";
                    }
                    break;
                case "活动能力":
                    {
                        this.label1.Text = "活动能力";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("huodongnengli");
                        this.dataGridView1.Columns[0].HeaderText = "活动能力编号";
                        this.dataGridView1.Columns[1].HeaderText = "活动能力";
                    }
                    break;
                case "药品名称":
                    {
                        this.label1.Text = "药品名称";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinmingcheng");
                        this.dataGridView1.Columns[0].HeaderText = "药品名称编号";
                        this.dataGridView1.Columns[1].HeaderText = "药品名称";
                    }
                    break;
                case "药品规格":
                    {
                        this.label1.Text = "药品规格";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinguige");
                        this.dataGridView1.Columns[0].HeaderText = "药品规格编号";
                        this.dataGridView1.Columns[1].HeaderText = "药品规格";
                    }
                    break;
                case "药品产地":
                    {
                        this.label1.Text = "药品产地";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinchandi");
                        this.dataGridView1.Columns[0].HeaderText = "药品产地编号";
                        this.dataGridView1.Columns[1].HeaderText = "药品产地";
                    }
                    break;
                case "药品剂型":
                    {
                        this.label1.Text = "药品剂型";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinjixing");
                        this.dataGridView1.Columns[0].HeaderText = "药品剂型编号";
                        this.dataGridView1.Columns[1].HeaderText = "药品剂型";
                    }
                    break;
                case "整量单位":
                    {
                        this.label1.Text = "整量单位";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengliangdanwei");
                        this.dataGridView1.Columns[0].HeaderText = "整量单位编号";
                        this.dataGridView1.Columns[1].HeaderText = "整量单位";
                    }
                    break;
                case "散量单位":
                    {
                        this.label1.Text = "散量单位";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("sanliangdanwei");
                        this.dataGridView1.Columns[0].HeaderText = "散量单位编号";
                        this.dataGridView1.Columns[1].HeaderText = "散量单位";
                    }
                    break;
                case "毒理类别":
                    {
                        this.label1.Text = "毒理类别";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("dulileibie");
                        this.dataGridView1.Columns[0].HeaderText = "毒理类别编号";
                        this.dataGridView1.Columns[1].HeaderText = "毒理类别";
                    }
                    break;
                case "物理状态":
                    {
                        this.label1.Text = "物理状态";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("wulizhuangtai");
                        this.dataGridView1.Columns[0].HeaderText = "物理状态编号";
                        this.dataGridView1.Columns[1].HeaderText = "物理状态";
                    }
                    break;
                case "最小剂量单位":
                    {
                        this.label1.Text = "最小剂量单位";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zuixiaojiliangdanwei");
                        this.dataGridView1.Columns[0].HeaderText = "最小剂量单位编号";
                        this.dataGridView1.Columns[1].HeaderText = "最小计量单位";
                    }
                    break;
                case "诱导用药":
                    {
                        this.label1.Text = "诱导用药";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("youdaoyongyao");
                        this.dataGridView1.Columns[0].HeaderText = "诱导用药编号";
                        this.dataGridView1.Columns[1].HeaderText = "诱导用药";
                    }
                    break;
                case "术前用药":
                    {
                        this.label1.Text = "术前用药";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shuqianyongyao");
                        this.dataGridView1.Columns[0].HeaderText = "术前用药编号";
                        this.dataGridView1.Columns[1].HeaderText = "术前用药";
                    }
                    break;
                case "给药方式":
                    {
                        this.label1.Text = "给药方式";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("geiyaofangshi");
                        this.dataGridView1.Columns[0].HeaderText = "给药方式编号";
                        this.dataGridView1.Columns[1].HeaderText = "给药方式";
                    }
                    break;
                case "过敏药物":
                    {
                        this.label1.Text = "给药方式";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("guominyaowu");
                        this.dataGridView1.Columns[0].HeaderText = "过敏药物编号";
                        this.dataGridView1.Columns[1].HeaderText = "过敏药物";
                    }
                    break;
                case "麻醉医生":
                    {
                        this.label1.Text = "麻醉医生";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("Adims_SurgeryStaff WHERE PostType = 0");
                        this.dataGridView1.Columns[0].HeaderText = "麻醉医生编号";
                        this.dataGridView1.Columns[1].HeaderText = "麻醉医生";
                    }
                    break;
                case "护士":
                    {
                        this.label1.Text = "护士";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("hushi");
                        this.dataGridView1.Columns[0].HeaderText = "护士编号";
                        this.dataGridView1.Columns[1].HeaderText = "护士";
                    }
                    break;
                case "请假类型":
                    {
                        this.label1.Text = "请假类型";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("qingjialeixing");
                        this.dataGridView1.Columns[0].HeaderText = "请假类型编号";
                        this.dataGridView1.Columns[1].HeaderText = "请假类型";
                    }
                    break;
                case "排班时间":
                    {
                        this.label1.Text = "排班时间";
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shijian");
                        this.dataGridView1.Columns[0].HeaderText = "排班时间编号";
                        this.dataGridView1.Columns[1].HeaderText = "排班时间";
                    }
                    break;
            }
            IsEnabled();

        }



        //<summary>
        //修改
        //</summary>
        //<param name="sender"></param>
        //<param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            string SelectID = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            switch (TreenodeText)
            {
                case "证书名称":
                    {

                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhengshu");//判断是否已经添加此证书名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "zhengshu");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengshu");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("证书名称不能为空");
                        }

                    }
                    break;
                case "职务名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhiwu");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "zhiwu");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhiwu");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("职务名称不能为空");
                        }

                    }
                    break;
                case "职称名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhicheng");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "zhicheng");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhicheng");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("职称名称不能为空");
                        }

                    }
                    break;
                case "专业名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhuanye");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "zhuanye");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhuanye");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("专业名称不能为空");
                        }

                    }
                    break;
                case "语种名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yuzhong");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "yuzhong");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yuzhong");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("语种名称不能为空");
                        }

                    }
                    break;
                case "学历名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "xueli");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "xueli");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xueli");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("学历名称不能为空");
                        }

                    }
                    break;
                case "奖惩名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "jiangcheng");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "jiangcheng");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jiangcheng");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("奖惩名称不能为空");
                        }

                    }
                    break;
                case "床号":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "chuanghao");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "chuanghao");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("chuanghao");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("床号不能为空");
                        }

                    }
                    break;
                case "科室名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "keshi");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "keshi");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("keshi");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("科室名称不能为空");
                        }

                    }
                    break;
                case "病区名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "bingqu");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "bingqu");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingqu");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("病区名称不能为空");
                        }

                    }
                    break;
                case "手术间名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "shoushujian");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "shoushujian");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shoushujian");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("手术间名称不能为空");
                        }

                    }
                    break;
                case "麻醉大类":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuidalei");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuidalei");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuidalei");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉大类不能为空");
                        }

                    }
                    break;
                case "麻醉平面":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuipingmian");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuipingmian");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuipingmian");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉平面不能为空");
                        }

                    }
                    break;
                case "麻醉范围":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuifanwei");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuifanwei");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuifanwei");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉范围不能为空");
                        }

                    }
                    break;
                case "麻醉效果":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuixiaoguo");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuixiaoguo");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuixiaoguo");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉效果不能为空");
                        }

                    }
                    break;
                case "麻醉气体":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuiqiti");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuiqiti");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiqiti");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉气体不能为空");
                        }

                    }
                    break;
                case "麻醉顾虑":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuigulu");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuigulu");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuigulu");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉顾虑不能为空");
                        }

                    }
                    break;
                case "麻醉问题":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuiwenti");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuiwenti");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiwenti");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉问题不能为空");
                        }

                    }
                    break;
                case "麻醉适应症":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "mazuishiyingzheng");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "mazuishiyingzheng");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuishiyingzheng");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉适应症不能为空");
                        }

                    }
                    break;
                case "病因":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "bingying");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "bingying");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingying");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("病因不能为空");
                        }

                    }
                    break;
                case "病种":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "bingzhong");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "bingzhong");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingzhong");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("病种不能为空");
                        }

                    }
                    break;
                case "ASA":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "ASA");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "ASA");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("ASA");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("ASA不能为空");
                        }

                    }
                    break;
                case "疾病症状":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "jibingzhengzhuang");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "jibingzhengzhuang");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jibingzhengzhuang");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("疾病症状不能为空");
                        }

                    }
                    break;
                case "用药效果":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yongyaoxiaoguo");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "yongyaoxiaoguo");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yongyaoxiaoguo");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("用药效果不能为空");
                        }

                    }
                    break;
                case "精神状态":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "jingshengzhuangtai");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "jingshengzhuangtai");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jingshengzhuangtai");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("精神状态不能为空");
                        }

                    }
                    break;
                case "系统疾病":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "xitongjibing");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "xitongjibing");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xitongjibing");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("系统疾病不能为空");
                        }

                    }
                    break;
                case "活动能力":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "huodongnengli");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "huodongnengli");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("huodongnengli");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("活动能力不能为空");
                        }

                    }
                    break;
                case "药品名称":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinmingcheng");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "yaopinmingcheng");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinmingcheng");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("药品名称不能为空");
                        }

                    }
                    break;
                case "药品规格":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinguige");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "yaopinguige");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinguige");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("药品规格不能为空");
                        }

                    }
                    break;
                case "药品产地":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinchandi");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "yaopinchandi");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinchandi");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("药品产地不能为空");
                        }

                    }
                    break;
                case "药品剂型":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "yaopinjixing");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "yaopinjixing");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinjixing");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("药品剂型不能为空");
                        }

                    }
                    break;
                case "整量单位":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zhengliangdanwei");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "zhengliangdanwei");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengliangdanwei");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("整量单位不能为空");
                        }

                    }
                    break;
                case "散量单位":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "sanliangdanwei");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "sanliangdanwei");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("sanliangdanwei");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("散量单位不能为空");
                        }

                    }
                    break;
                case "毒理类别":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "dulileibie");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "dulileibie");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("dulileibie");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("毒理类别不能为空");
                        }

                    }
                    break;
                case "物理状态":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "wulizhuangtai");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "wulizhuangtai");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("wulizhuangtai");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("物理状态不能为空");
                        }

                    }
                    break;
                case "最小剂量单位":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "zuixiaojiliangdanwei");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "zuixiaojiliangdanwei");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zuixiaojiliangdanwei");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("最小剂量单位不能为空");
                        }

                    }
                    break;
                case "诱导用药":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "youdaoyongyao");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "youdaoyongyao");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("youdaoyongyao");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("诱导用药不能为空");
                        }

                    }
                    break;
                case "术前用药":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "shuqianyongyao");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "shuqianyongyao");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shuqianyongyao");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("术前用药不能为空");
                        }

                    }
                    break;
                case "给药方式":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "geiiyaofangshi");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "geiiyaofangshi");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("geiiyaofangshi");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("给药方式不能为空");
                        }

                    }
                    break;
                case "过敏药物":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "guominyaowu");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "guominyaowu");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("guominyaowu");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("过敏药物不能为空");
                        }

                    }
                    break;
                case "麻醉医生":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "Adims_SurgeryStaff WHERE PostType = 0");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "Adims_SurgeryStaff WHERE PostType = 0");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("Adims_SurgeryStaff WHERE PostType = 0");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("麻醉医生不能为空");
                        }

                    }
                    break;
                case "护士":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "hushi");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "hushi");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("hushi");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("护士不能为空");
                        }

                    }
                    break;
                case "请假类型":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "qingjialeixing");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "qingjialeixing");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("qingjialeixing");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("请假类型为空");
                        }

                    }
                    break;
                case "排班时间":
                    {
                        if (!string.IsNullOrEmpty(text))
                        {
                            bool IsHaveName = adims_BLL.AdimsController.IsHaveName(this.textBox1.Text, "shijian");//判断是否已经添加此职务名称
                            if (IsHaveName)
                            {
                                MessageBox.Show(this.textBox1.Text + "  已经存在" + "\n" + "修改失败");
                            }
                            else
                            {
                                adims_BLL.AdimsController.UpdateData1(SelectID, text, "shijian");
                                this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shijian");
                                MessageBox.Show("添加成功");
                            }
                        }
                        else
                        {
                            MessageBox.Show("排班时间为空");
                        }

                    }
                    break;

            }
            IsEnabled();
        }


        //<summary>
        //判断datagridview里面是否有选中项
        //</summary>
        //<returns></returns>
        public bool IsSelectItem()
        {
            int count = this.dataGridView1.SelectedRows.Count;
            if (count < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.textBox1.Text = this.dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
        }



        //<summary>
        //删除
        //</summary>
        //<param name="sender"></param>
        //<param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult d = MessageBox.Show("删除后将无法恢复，是否决定删除", "消息提示", MessageBoxButtons.YesNo);
            if (d == DialogResult.Yes)
            {
                string SelectID = this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                ;

                switch (TreenodeText)//刷新datagridview的数据源
                {
                    case "证书名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "zhengshu");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengshu");
                        break;
                    case "职务名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "zhiwu");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhiwu");
                        break;
                    case "职称名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "zhicheng");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhicheng");
                        break;
                    case "专业名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "zhuanye");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhuanye");
                        break;
                    case "语种名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "yuzhong");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yuzhong");
                        break;
                    case "学历名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "xueli");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xueli");
                        break;
                    case "奖惩名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "jiangcheng");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jiangcheng");
                        break;
                    case "床号":
                        adims_BLL.AdimsController.DeleteData(SelectID, "chuanghao");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("chuanghao");
                        break;
                    case "科室名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "keshi");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("keshi");
                        break;
                    case "病区名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "bingqu");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingqu");
                        break;
                    case "手术间名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "shoushujian");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shoushujian");
                        break;
                    case "麻醉大类":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuidalei");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuidalei");
                        break;
                    case "麻醉平面":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuipingmian");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuipingmian");
                        break;
                    case "麻醉范围":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuifanwei");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuifanwei");
                        break;
                    case "麻醉效果":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuixiaoguo");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuixiaoguo");
                        break;
                    case "麻醉气体":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuiqiti");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiqiti");
                        break;
                    case "麻醉顾虑":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuigulu");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuigulu");
                        break;
                    case "麻醉问题":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuiwenti");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuiwenti");
                        break;
                    case "麻醉适应症":
                        adims_BLL.AdimsController.DeleteData(SelectID, "mazuishiyingzheng");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("mazuishiyingzheng");
                        break;
                    case "病因":
                        adims_BLL.AdimsController.DeleteData(SelectID, "bingying");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingying");
                        break;
                    case "病种":
                        adims_BLL.AdimsController.DeleteData(SelectID, "bingzhong");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("bingzhong");
                        break;
                    case "ASA":
                        adims_BLL.AdimsController.DeleteData(SelectID, "ASA");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("ASA");
                        break;
                    case "疾病症状":
                        adims_BLL.AdimsController.DeleteData(SelectID, "jibingzhengzhuang");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jibingzhengzhuang");
                        break;
                    case "用药效果":
                        adims_BLL.AdimsController.DeleteData(SelectID, "yongyaoxiaoguo");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yongyaoxiaoguo");
                        break;
                    case "精神状态":
                        adims_BLL.AdimsController.DeleteData(SelectID, "jingshengzhuangtai");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("jingshengzhuangtai");
                        break;
                    case "系统疾病":
                        adims_BLL.AdimsController.DeleteData(SelectID, "xitongjibing");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("xitongjibing");
                        break;
                    case "活动能力":
                        adims_BLL.AdimsController.DeleteData(SelectID, "huodongnengli");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("huodongnengli");
                        break;
                    case "药品名称":
                        adims_BLL.AdimsController.DeleteData(SelectID, "yaopinmingcheng");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinmingcheng");
                        break;
                    case "药品规格":
                        adims_BLL.AdimsController.DeleteData(SelectID, "yaopinguige");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinguige");
                        break;
                    case "药品产地":
                        adims_BLL.AdimsController.DeleteData(SelectID, "yaopinchandi");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinchandi");
                        break;
                    case "药品剂型":
                        adims_BLL.AdimsController.DeleteData(SelectID, "yaopinjixing");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("yaopinjixing");
                        break;
                    case "整量单位":
                        adims_BLL.AdimsController.DeleteData(SelectID, "zhengliangdanwei");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zhengliangdanwei");
                        break;
                    case "散量单位":
                        adims_BLL.AdimsController.DeleteData(SelectID, "sanliangdanwei");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("sangliangdanwei");
                        break;
                    case "毒理类别":
                        adims_BLL.AdimsController.DeleteData(SelectID, "dulileibie");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("dulileibie");
                        break;
                    case "物理状态":
                        adims_BLL.AdimsController.DeleteData(SelectID, "wulizhuangtai");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("wulizhuangtai");
                        break;
                    case "最小剂量单位":
                        adims_BLL.AdimsController.DeleteData(SelectID, "zuixiaojiliangdanwei");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("zuixiaojiliangdanwei");
                        break;
                    case "诱导用药":
                        adims_BLL.AdimsController.DeleteData(SelectID, "youdaoyongyao");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("youdaoyongyao");
                        break;
                    case "术前用药":
                        adims_BLL.AdimsController.DeleteData(SelectID, "shuqianyongyao");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shuqianyongyao");
                        break;
                    case "给药方式":
                        adims_BLL.AdimsController.DeleteData(SelectID, "geiyaofangshi");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("geiyaofangshi");
                        break;
                    case "过敏药物":
                        adims_BLL.AdimsController.DeleteData(SelectID, "guominyaowu");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("guominyaowu");
                        break;
                    case "麻醉医生":
                        adims_BLL.AdimsController.DeleteData(SelectID, "Adims_SurgeryStaff WHERE PostType = 0");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("Adims_SurgeryStaff WHERE PostType = 0");
                        break;
                    case "护士":
                        adims_BLL.AdimsController.DeleteData(SelectID, "hushi");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("hushi");
                        break;
                    case "请假类型":
                        adims_BLL.AdimsController.DeleteData(SelectID, "qingjialeixing");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("qingjialeixing");
                        break;
                    case "排班时间":
                        adims_BLL.AdimsController.DeleteData(SelectID, "shijian");
                        this.dataGridView1.DataSource = adims_BLL.AdimsController.getData("shijian");
                        break;




                }
                MessageBox.Show("删除成功");
            }
            else if (d == DialogResult.No)
            {
                MessageBox.Show("取消删除");
            }
            IsEnabled();

        }


        //<summary>
        //控制修改删除控件是否能用
        //</summary>
        public void IsEnabled()
        {
            if (IsSelectItem())
            {
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
            }
        }

    }
}
