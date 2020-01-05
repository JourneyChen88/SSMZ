using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


namespace adims_DAL
{
    public class mz
    {
        private DBConn dBConn = new DBConn();
        #region <<麻醉知情同意书>>

        public DataTable GetMazuizhiqingshu(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting where patzhuyuanid='" + zyh + "' order by odate desc";//方法下取sql得值
            return dBConn.GetDataTable(sql);

        }   //查询麻醉数据调用
        public DataTable GetGaoWei()
        {
            string sql = "select name from Gaowei";
            return dBConn.GetDataTable(sql);
        }  //高危因素
        public DataTable GetAllMZYS()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where type='1'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        } // 医师  

        public DataTable gettljhushi()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where type='2'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        } // hushi  
        public DataTable Getpingjia()
        {
            string sql = "select name from pingjia";
            return dBConn.GetDataTable(sql);
        }//评价
        public DataTable GetMazuiFangfa()
        {
            string sql = "select name from Mazuifangfa";
            return dBConn.GetDataTable(sql);
        } //麻醉方法
        public DataTable Getzhiqingtys(string zyh) //查询保存后的麻醉表
        {
            string sql = "select * from mazuizhiqingtys where zhuyuanhao='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int Insertzhiqingtys(Dictionary<string, string> dictionary)//增加
        {
            string _INSERT = @"INSERT INTO [dbo].[mazuizhiqingtys]([IsRead],[ZYNumber],[sex],[keshi],[age],[caozuoshijian],[zhuyuanhao],[jibingjieshao],[mazui],[zhenduan],[tidaifangan],[fanganpingjia],[yishiqianzi],[rqdate],[gaoweiyinsu])
          VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}' )";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int Updatezhiqingtys(Dictionary<string, string> dictionary)//修改
        {
            string _INSERT = @"UPDATE [dbo].[mazuizhiqingtys]
   SET [IsRead]= '{0}',[sex]='{2}',[keshi]='{3}',[age]='{4}',[caozuoshijian]='{5}',[zhuyuanhao]='{6}',[jibingjieshao]='{7}',[mazui]='{8}',[zhenduan]='{9}',[tidaifangan]='{10}',[fanganpingjia]='{11}',[yishiqianzi]='{12}',[rqdate]='{13}',[gaoweiyinsu]='{14}' where [ZYNumber]= '{1}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        #endregion
        #region <<手术患者交接记录单>>
        public DataTable Getjiaojiejilu(string zyh)//查询信息
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanID='" + zyh + "' order by odate desc";
            return dBConn.GetDataTable(sql);
        }
        public DataTable Getsqljiludan(string zyh)//保存记录单
        {
            string sql = "SELECT * from SShhJJJJD where ZhuYuanID=" + zyh;
            return dBConn.GetDataTable(sql);
        }
        public int Insertjiludan(Dictionary<string, string> dictionary)//增加
        {
            string _INSERT = @"INSERT INTO [dbo].[SShhJJJJD]([IsRead],[ZhuYuanID],[name],[keshi],[bedID],[sex],[age],[Hdxu1],[Hdxu2],
          [Hdxu3],[rate1],[rate2],[rate3],[hzshengfen1],[hzshengfen2],[hzshengfen3],[Gms1],[Gms2],[Shyongyao1],[Pfwzx1],[Pfwzx2],
          [Pfwzx3],[GD1],[GD2],[GD3],[Jss1],[Pkpg1],[BL1],[BL2],[BL3],[YLWS1],[YLWS2],[YLWS3],[SHbj1],[Xdwp1],[Xdwp2],[Xdwp3],
          [BZ1],[BZ2],[BZ3],[JIAOKESHI1],[JIAOKESHI2],[JIAOKESHI3],[JIEKESHI1],[JIEKESHI2],[JIEKESHI3],[JIAOQIANMING1],[JIAOQIANMING2],
          [JIAOQIANMING3],[JIEQIANMING1],[JIEQIANMING2],[JIEQIANMING3],[rate])
           VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}',
          '{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}',
          '{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int Updatejiludan(Dictionary<string, string> dictionary)//修改
        {
            string revise = @"UPDATE [dbo].[SShhJJJJD]
           SET [IsRead]='{0}',[name]='{2}',[keshi]='{3}',[bedID]='{4}',[sex]='{5}',[age]='{6}',[Hdxu1]='{7}',[Hdxu2]='{8}',[Hdxu3]='{9}' ,
          [rate1]='{10}' ,[rate2]='{11}',[rate3]='{12}',[hzshengfen1]='{13}',[hzshengfen2]='{14}',[hzshengfen3]='{15}',[Gms1]='{16}',[Gms2]='{17}',[Shyongyao1]='{18}' ,
          [Pfwzx1]='{19}' ,[Pfwzx2]='{20}' ,[Pfwzx3]='{21}',[GD1]='{22}',[GD2]='{23}',[GD3]='{24}' ,[Jss1]='{25}',[Pkpg1]='{26}',[BL1]='{27}',
          [BL2]='{28}',[BL3]='{29}',[YLWS1]='{30}',[YLWS2]='{31}' ,[YLWS3]='{32}',[SHbj1]='{33}',[Xdwp1]='{34}' ,[Xdwp2]='{35}',[Xdwp3]='{36}' ,
          [BZ1]='{37}' ,[BZ2]='{38}' ,[BZ3]='{39}' ,[JIAOKESHI1]='{40}' ,[JIAOKESHI2]='{41}' ,[JIAOKESHI3]='{42}',[JIEKESHI1]='{43}',
          [JIEKESHI2]='{44}',[JIEKESHI3]='{45}',[JIAOQIANMING1]='{46}' ,[JIAOQIANMING2]='{47}',[JIAOQIANMING3]='{48}',[JIEQIANMING1]='{49}',[JIEQIANMING2]='{50}',[JIEQIANMING3]='{51}',[rate]='{52}'
          where [ZhuYuanID]='{1}'";
            string inst = string.Format(revise, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        #endregion
        #region <<体内植入物条码粘贴单>>
        public DataTable GetTiaoma(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanID='" + zyh + "' order by odate desc";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetAllshoushuyishi()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where type='1'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        public DataTable GetAllshoushuhushi()
        {
            string selectAllYS = " SELECT user_name FROM Adims_user where type='2'";
            return dBConn.GetDataTable(string.Format(selectAllYS));
        }
        //保存
        public int InsertTiaomadan(Dictionary<string, string> dictionary)//增加
        {
            string _INSERT = @"INSERT INTO [dbo].[Tmztd]([PatZYid],[name],[sex],[age],[keshi],[Ssrate],[SsMingchen],[TiaoMA],[YSname],[HSname],[IsRead],[rate])
          VALUES( {0}, '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}' )";

            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public DataTable GetTiaomadan(string ZYH)//查询
        {
            string sql = "select * from Tmztd where PatZYid='" + ZYH + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int UpdateTiaomadan(Dictionary<string, string> dictionary)//修改
        {
            string _INSERT = @"UPDATE [dbo].[Tmztd]
   SET [name]='{1}',[sex]='{2}',[age]='{3}',[keshi]='{4}',[Ssrate]='{5}',[SsMingchen]='{6}',[TiaoMA]='{7}',[YSname]='{8}',[HSname]='{9}',[IsRead]='{10}',[rate]='{11}' where [PatZYid]= '{0}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }

        #endregion
        #region<<输血护理记录单>>
        public DataTable GetSXhljilu(string zyh)//显示信息
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanID='" + zyh + "' order by odate desc";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetaboXuexing()//abo血型
        {
            string xuexing = "SELECT name from Xuexing where type='1'";
            return dBConn.GetDataTable(string.Format(xuexing));
            string rhxuexing = "SELECT name from Xuexing where type='2'";
            return dBConn.GetDataTable(string.Format(rhxuexing));
        }
        public DataTable Getrhxuexing()//rh血型
        {
            string rhxuexing = "SELECT name from Xuexing where type='2'";
            return dBConn.GetDataTable(string.Format(rhxuexing));
        }
        //保存
        public int InsertSxhljjd(Dictionary<string, string> dictionary)//增加
        {
            string insert = @"INSERT INTO [dbo].[SSHLjud]([IsRead],[ZhuYuanID],[name],[keshi],[sex],[age],[sxrate],[beginsx],[endinsx],[Binrenxuexing] ,
          [Gxzxuexing] ,[SXfenlei],[SXliang],[kangguominYW],[kgmname],[SXqianTW],[SXqianMB],[SXqianHX],[SXqianXY] ,[SXzhongTW] ,[SXzhongMB] ,[SXzhongHX],[SXzhongXY]
          ,[SXhouTW],[SXhouMB] ,[SXhouHX],[SXhouXY],[SZ15fenqian],[SZ15fenhou],[SZzunyi] ,[SZshunlifou],[BSLbeizhu] ,[MZyisheng] ,[HSqianzi],[qianzirate] ,[SzJJXdID]
          ,[SzJJXdlliang] ,[SzJJzhaungkuang] ,[SzJJZKmiaosu] ,[SzJJjiaohuqianzi] ,[SzJJjieshouqianzi] ,[SxFybeginrate] ,[SxFyendrate] ,[SxFySXrate],[SxFySXxuexing]
          ,[SxFySXID],[SxFySXliang] ,[SxFySXZhengzhuang],[SxFySXCL] ,[SxFySXCLJG],[SxFySXSBao],[SxFySXHLSBaotime],[SxFySXsxSBao] ,[SxFySXsxSBaotime] ,[SxFySXhushiqianzi])
          VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}',
         '{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}',
          '{52}','{53}', '{54}')";
            string inst = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public DataTable GetSxhljjd(string zyh)//查询
        {
            string sql = "select * from SSHLjud where ZhuYuanID='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int UpdateSxhljjd(Dictionary<string, string> dictionary)//修改
        {
            string revise = @"UPDATE [dbo].[SSHLjud]
           SET [IsRead]='{0}',[name]='{2}',[keshi]='{3}',[sex]='{4}',[age]='{5}',[sxrate]='{6}',[beginsx]='{7}',[endinsx]='{8}',[Binrenxuexing]='{9}' ,
          [Gxzxuexing]='{10}' ,[SXfenlei]='{11}',[SXliang]='{12}',[kangguominYW]='{13}',[kgmname]='{14}',[SXqianTW]='{15}',[SXqianMB]='{16}',[SXqianHX]='{17}',[SXqianXY]='{18}' ,
          [SXzhongTW]='{19}' ,[SXzhongMB]='{20}' ,[SXzhongHX]='{21}',[SXzhongXY]='{22}',[SXhouTW]='{23}',[SXhouMB]='{24}' ,[SXhouHX]='{25}',[SXhouXY]='{26}',[SZ15fenqian]='{27}',
          [SZ15fenhou]='{28}',[SZzunyi]='{29}',[SZshunlifou]='{30}',[BSLbeizhu]='{31}' ,[MZyisheng]='{32}',[HSqianzi]='{33}',[qianzirate]='{34}' ,[SzJJXdID]='{35}',[SzJJXdlliang]='{36}' ,
          [SzJJzhaungkuang]='{37}' ,[SzJJZKmiaosu]='{38}' ,[SzJJjiaohuqianzi]='{39}' ,[SzJJjieshouqianzi]='{40}' ,[SxFybeginrate]='{41}' ,[SxFyendrate]='{42}',[SxFySXrate]='{43}',
          [SxFySXxuexing]='{44}',[SxFySXID]='{45}',[SxFySXliang]='{46}' ,[SxFySXZhengzhuang]='{47}',[SxFySXCL]='{48}',[SxFySXCLJG]='{49}',[SxFySXSBao]='{50}',[SxFySXHLSBaotime]='{51}',
          [SxFySXsxSBao]='{52}' ,[SxFySXsxSBaotime]='{53}' ,[SxFySXhushiqianzi]='{54}' where [ZhuYuanID]='{1}'";
            string inst = string.Format(revise, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }


        #endregion
        #region<<自付费用知情同意书>>
        public DataTable GetZFFzhiqingtys(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting where patzhuyuanid='" + zyh + "' order by odate desc";//方法下取sql得值
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetbaocZffzhiqingtys(string zyh) //查询保存后的麻醉表
        {
            string sql = "select * from ZFfzhiqingtys where PatZYid='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int Insertzffzhiqingtys(Dictionary<string, string> dictionary)//增加
        {
            string _INSERT = @"INSERT INTO [dbo].[ZFfzhiqingtys]([IsRead],[PatZYid],[name],[sex],[age],[zubie],[Rlbie] ,[jbzhenduan],[Ywname1],[byx1],
         [Ywname2],[byx2],[phone],[zxkeshi],[yaowudj],[huanzheqianzi] ,[JcXmname],[byx3],[fx1],[xmdj],[xmhzqianzi] ,[Ylxm],[byx4],[shuoming],
         [ylxmdj],[ylxmqianzi],[zhHZqianzi],[yishiqianzi],[huanzherate],[yishirate],[yaoping2danj])
          VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int Updatezffzhiqingtys(Dictionary<string, string> dictionary)//修改
        {
            string _INSERT = @"UPDATE [dbo].[ZFfzhiqingtys]
          SET [IsRead]= '{0}',[name]='{2}',[sex]='{3}',[age]='{4}',[zubie]='{5}',[Rlbie]='{6}',[jbzhenduan]='{7}',[Ywname1]='{8}',[byx1]='{9}',[Ywname2]='{10}',[byx2]='{11}',[phone]='{12}',[zxkeshi]='{13}',[yaowudj]='{14}',
         [huanzheqianzi]= '{15}',[JcXmname]='{16}',[byx3]='{17}',[fx1]='{18}',[xmdj]='{19}',[xmhzqianzi]='{20}',[Ylxm]='{21}',[byx4]='{22}',[shuoming]='{23}',[ylxmdj]='{24}',[ylxmqianzi]='{25}',[zhHZqianzi]='{26}',[yishiqianzi]='{27}',[huanzherate]='{28}',[yishirate]='{29}',[yaoping2danj]='{30}' where [PatZYid]= '{1}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }

        #endregion
        #region<<病理标本送检单>>
        public DataTable GetBlbesjdan(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanID='" + zyh + "' order by odate desc";//方法下取sql得值
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetbcBlbesjdan(string zyh)
        {
            string sql = "select * from BlBbsjdan where ZhuYuanID='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertBlbesjdan(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [dbo].[BlBbsjdan]([IsRead],[ZhuYuanID],[rate],[BLID],[name],[sex],[age],[zubie],[city],[job],[hospital],[kebie],[mzhenid],[shebao],        
                          [Sjcl1],[Sjcl2],[Sjcl3],[SJmd],[SJrq],[Jcfa],[JStz],[bfshsj],[ZLtime],[ZLdaxiao],[Buwei],[ZYqk],[hunfou],[yuejingzhouqi],[mociyuejing],[Qtjiancha],
                          [linchuangzhenduan],[songjianyishi],[fuyan],[qchuojianhaoma],[dtryjianchaju],[ZzKshu],[jianchazhe],[binglizhenduan],[baogaozhe],[baogaotime])
          VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}',
              '{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int UpdateBlbesjdan(Dictionary<string, string> dictionary)//修改
        {
            string _INSERT = @"UPDATE [dbo].[BlBbsjdan]
          SET [IsRead]= '{0}',[rate]='{2}',[BLID]='{3}',[name]='{4}',[sex]='{5}',[age]='{6}',[zubie]='{7}',[city]='{8}',[job]='{9}',[hospital]='{10}',[kebie]='{11}',[mzhenid]='{12}',[shebao]='{13}',[Sjcl1]='{14}',
         [Sjcl2]= '{15}',[Sjcl3]='{16}',[SJmd]='{17}',[SJrq]='{18}',[Jcfa]='{19}',[JStz]='{20}',[bfshsj]='{21}',[ZLtime]='{22}',[ZLdaxiao]='{23}',[Buwei]='{24}',[ZYqk]='{25}',[hunfou]='{26}',[yuejingzhouqi]='{27}',[mociyuejing]='{28}',[Qtjiancha]='{29}'
          ,[linchuangzhenduan]='{30}',[songjianyishi]='{31}',[fuyan]='{32}',[qchuojianhaoma]='{33}',[dtryjianchaju]='{34}',[ZzKshu]='{35}',[jianchazhe]='{36}',[binglizhenduan]='{37}',[baogaozhe]='{38}',[baogaotime]='{39}' where [ZhuYuanID]= '{1}'";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        #endregion
        #region<<手术安全核查表>>
        public DataTable GetShaqhcbiao(string zyh)
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanID='" + zyh + "' order by odate desc";//方法下取sql得值
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetbaocunShaqhcbiao(string zyh)
        {
            string sql = "select * from SSaqhcb where ZhuYuanID='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertShaqhcbiao(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [dbo].[SSaqhcb]([IsRead],[ZhuYuanID],[rate],[keshi],[chaunghao],[name],[shoushuname],[SHqshenfen],[SHqbuwei],[SHqmingcheng],
                          [SHqtongyi],[SHqbiaoshi],[SHqjcwc],[SHqjcjl],[SHqwzxjc],[SHqpfzb],[SHqjmtdjl],[SHqhzgms],[SHqqdhuxizangai],[SHqkjywpsqk],[SHqsqbx],[SHqqita],
                          [shoushuyisheng1],[mazuiyisheng1],[xunhuihushi1],[SHpiqshenfen],[SHpiqshmc],[SHpiqshbw],[SHpiqyscs],[SHpiqmzys],[SHpiqshhs],[SHpiqqita],
                          [mazuiyishi2],[xunhuihushi2],[SHhshenfen],[SHhsumc],[SHhszyy],[SHhsx],[SHhqdyw],[SHhbbqr],[SHhhuanzhexm],[SHhZyID],[SHhWZ],[SHhdmtl],[SHhYlg],
                          [SHhNG],[SHhqitaguanlu],[SHhBRquxiang],[SHhqita],[SHhheduiwanb],[shoushuyisheng3],[mazuiyisheng3],[xunhuihushi3])
                     VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}',
                          '{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}',
                          '{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int UpdateShaqhcbiao(Dictionary<string, string> dictionary)
        {
            string update = @"UPDATE [dbo].[SSaqhcb]
          SET [IsRead]= '{0}',[rate]='{2}',[keshi]='{3}',[chaunghao]='{4}',[name]='{5}',[shoushuname]='{6}',[SHqshenfen]='{7}',[SHqbuwei]='{8}',[SHqmingcheng]='{9}',[SHqtongyi]='{10}',[SHqbiaoshi]='{11}',[SHqjcwc]='{12}',[SHqjcjl]='{13}',[SHqwzxjc]='{14}',
         [SHqpfzb]= '{15}',[SHqjmtdjl]='{16}',[SHqhzgms]='{17}',[SHqqdhuxizangai]='{18}',[SHqkjywpsqk]='{19}',[SHqsqbx]='{20}',[SHqqita]='{21}',[shoushuyisheng1]='{22}',[mazuiyisheng1]='{23}',[xunhuihushi1]='{24}',[SHpiqshenfen]='{25}',[SHpiqshmc]='{26}',[SHpiqshbw]='{27}',[SHpiqyscs]='{28}',[SHpiqmzys]='{29}',
         [SHpiqshhs]= '{30}',[SHpiqqita]='{31}',[mazuiyishi2]='{32}',[xunhuihushi2]='{33}',[SHhshenfen]='{34}',[SHhsumc]='{35}',[SHhszyy]='{36}',[SHhsx]='{37}',[SHhqdyw]='{38}',[SHhbbqr]='{39}',[SHhhuanzhexm]='{40}',[SHhZyID]='{41}',[SHhWZ]='{42}',[SHhdmtl]='{43}',[SHhYlg]='{44}' ,
          [SHhNG]='{45}',[SHhqitaguanlu]='{46}',[SHhBRquxiang]='{47}',[SHhqita]='{48}',[SHhheduiwanb]='{49}',[shoushuyisheng3]='{50}',[mazuiyisheng3]='{51}',[xunhuihushi3]='{52}' where [ZhuYuanID]= '{1}'";
            string inst = string.Format(update, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }

        #endregion
        #region<<手术护理记录单>>
        public DataTable GetShuShlJLDAN(string zyh)//显示
        {
            string sql = "SELECT * from Adims_OTypesetting where PatZhuYuanID='" + zyh + "' order by odate desc";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GEThlqk(string zyh)
        {
            string sql = "select * from SHhljiludan_hlqk where ZhuYuanID='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetallzxdModel()//调用常用包
        {
            string select = "select zxdType from Pingming";
            return dBConn.GetDataTable(string.Format(select));
        }
        public DataTable SelectzxdModel(string zyh)//常用包数据
        {
            string countZxd = "select cybpm,zxdCount,id,zxdType from [Changybao] where [zxdType]='" + zyh + "' order by id ";
            return dBConn.GetDataTable(string.Format(countZxd));
        }
        public DataTable Getchangyongbaobc(string mzjldid, string cyb)//保存常用包
        {
            string sql = "select * from Changyongbaobc where zhuyuanID='" + mzjldid + "'and cyb='" + cyb + "' ";
            return dBConn.GetDataTable(sql);
        }
        public DataTable Getqixiebaobc(string mzjldid, string cyb)//保存常用包
        {
            string sql = "select * from Qixiebaocun where zhuyuanID='" + mzjldid + "'and cyb='" + cyb + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int Deletechangyongbaobc(string mzjldid, string cyb)//删除常用包
        {
            string sql = "delete Changyongbaobc where zhuyuanID='" + mzjldid + "'and cyb='" + cyb + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int DeleteZxdb(string mzjldid)//清空id常用包
        {
            string sql = @"delete from Changyongbaobc where zhuyuanID='" + mzjldid + "'";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Insertzxdqingdian(Dictionary<string, string> dictionary)//添加常用包
        {
            string insert = @"Insert into Changyongbaobc (zhuyuanID,cyb,zxdname,sqqd,gqhd,ghhd,zxdname1,sqqd1,gqhd1,ghhd1) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public DataTable GetBcv(string mzjldid)//
        {
            string sql = @"SELECT [zxdname],[sqqd],[gqhd],[ghhd],[zxdname1],[sqqd1],[gqhd1],[ghhd1] FROM [Changyongbaobc] where zhuyuanID='" + mzjldid + "' order by id";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetQcv(string mzjldid)//
        {
            string sql = @"SELECT [zxdname],[sqqd],[gqhd],[ghhd],[zxdname1],[sqqd1],[gqhd1],[ghhd1],[zxdname2],[sqqd2],[gqhd2],[ghhd2] FROM [Qixiebaocun] where zhuyuanID='" + mzjldid + "' order by id";
            return dBConn.GetDataTable(sql);
        }
        public DataTable GetallzxdModel2()//调用器械包
        {
            string select = "select zxdType from Qixieming";
            return dBConn.GetDataTable(string.Format(select));
        }
        public DataTable SelectzxdModel2(string zyh)//器械包数据
        {
            string countZxd = "select cybpm,zxdCount,id,zxdType from [Changybao] where [zxdType]='" + zyh + "' order by id ";
            return dBConn.GetDataTable(string.Format(countZxd));
        }
        public DataTable GetQixiebaobc(string mzjldid, string cyb)//保存器械包
        {
            string sql = "select * from Qixiebaocun where zhuyuanID='" + mzjldid + "'and cyb='" + cyb + "' ";
            return dBConn.GetDataTable(sql);
        }
        public int Insertzxqidqingdian(Dictionary<string, string> dictionary)//添加器械包
        {
            string insert = @"Insert into Qixiebaocun (zhuyuanID,cyb,zxdname,sqqd,gqhd,ghhd,zxdname1,sqqd1,gqhd1,ghhd1,zxdname2,sqqd2,gqhd2,ghhd2) Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Deleteqixiebaobc(string mzjldid, string cyb)//删除器械包
        {
            string sql = "delete Qixiebaocun where zhuyuanID='" + mzjldid + "'and cyb='" + cyb + "' ";
            return dBConn.ExecuteNonQuery(sql);
        }
        public int Inserthlqk(Dictionary<string, string> dictionary)
        {
            string _INSERT = @"INSERT INTO [dbo].[SHhljiludan_hlqk]([IsRead],[ZhuYuanID],[rsrate],[shenzhi],[Jingmcichuan],[SJingmcichuan],
       [Daoniao],[Pifu],[SHUQIANqianming],[tiwei],[biaobensbdong],[diandao],[fujizhantie],[niaoliang],[shuyeliang],[shuxueliang],
       [hongxibaoxuanye],[xuejiang],[xuexiaoban],[xuexing],[SZqita],[SZqianming],[SHpifuqingkuang],[SHshenzhi],[SHyinliu],[SHbiaobensongbl],
       [SHBBqianming],[SHchurate],[SHshuhousonghui],[SHfbsrate],[SHsonghuiqianming],[SHshqita],[SHyongyao],[SHqianming],[SHwujunbaojiance],
       [SHqixiehecha],[SHzuihouqita],[qixieysqianming],[shoushuysqianming],[xunhuiysqianming],[jiebanhsqianming],[bz])
       VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}',
       '{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}')";
            string inst = string.Format(_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }
        public int Updatehlqk(Dictionary<string, string> dictionary)
        {
            string update = @"UPDATE [dbo].[SHhljiludan_hlqk]
          SET [IsRead]='{0}',[rsrate]='{2}',[shenzhi]='{3}',[Jingmcichuan]='{4}',[SJingmcichuan]='{5}',[Daoniao]='{6}',[Pifu]='{7}',[SHUQIANqianming]='{8}',[tiwei]='{9}',[biaobensbdong]='{10}',[diandao]='{11}',[fujizhantie]='{12}',[niaoliang]='{13}',[shuyeliang]='{14}',
          [shuxueliang]= '{15}',[hongxibaoxuanye]='{16}',[xuejiang]='{17}',[xuexiaoban]='{18}',[xuexing]='{19}',[SZqita]='{20}',[SZqianming]='{21}',[SHpifuqingkuang]='{22}',[SHshenzhi]='{23}',[SHyinliu]='{24}',[SHbiaobensongbl]='{25}',[SHBBqianming]='{26}',[SHchurate]='{27}',[SHshuhousonghui]='{28}',[SHfbsrate]='{29}',
          [SHsonghuiqianming]= '{30}',[SHshqita]='{31}',[SHyongyao]='{32}',[SHqianming]='{33}',[SHwujunbaojiance]='{34}',[SHqixiehecha]='{35}',[SHzuihouqita]='{36}',[qixieysqianming]='{37}',[shoushuysqianming]='{38}',[xunhuiysqianming]='{39}',[jiebanhsqianming]='{40}', [bz]='{41}' where [ZhuYuanID]= '{1}'";
            string inst = string.Format(update, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(inst);
        }

        public int hlqxXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO BJsysshqxqd (binganhao,qxpm,sqqd,gq,gh,qxpm1,sqqd1,gq1,gh1,qxpm2,sqqd2,gq2,gh2)
                              values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//器械新增
        public int hlQXXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE [dbo].[BJsysshqxqd]
   SET [qxpm] = '{1}',[sqqd] = '{2}',[gq] = '{3}',[gh] = '{4}',[qxpm1] = '{5}',[sqqd1] = '{6}'
      ,[gq1] = '{7}',[gh1] = '{8}',[qxpm2] = '{9}',[sqqd2] = '{10}',[gq2] = '{11}',[gh2] = '{12}'
 WHERE binganhao='{0}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//器械修改
        public int hlflXingZeng(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO BJsyflqd (binganhao,flpm,sqqd,gq,gh,flpm1,sqqd1,gq1,gh1)
                              values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }//敷料新增
        public int hlFLXiuGai(Dictionary<string, string> dictionary)
        {
            string ins = @"UPDATE [dbo].[BJsyflqd]
   SET [flpm] = '{1}',[sqqd] = '{2}',[gq] = '{3}',[gh] = '{4}',[flpm1] = '{5}',[sqqd1] = '{6}'
      ,[gq1] = '{7}',[gh1] = '{8}' WHERE [binganhao] = '{0}'";
            string sql1 = string.Format(ins, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }//敷料修改
        #endregion
        #region<<麻醉门诊评估单>>
        public DataTable GetMazuimzpgdan(string zyh)
        {
            string sql = "SELECT * FROM admin_mazuimenzhen where menzhenhaocishu='" + zyh + "' order by dengjishijian desc";
            return dBConn.GetDataTable(sql);
        }
        public DataTable baocunMazuimzpgdan(string zyh)
        {
            string sql = "select * from MZmzpgdan where zhuyuanhao='" + zyh + "'";
            return dBConn.GetDataTable(sql);
        }
        public int InsertMazuimzpgdan(Dictionary<string, string> dictionary)
        {
            string insert = @"INSERT INTO [MZmzpgdan]
           ([zhuyuanhao],[BP],[R],[P],[T],[xuexing],[xinxueguan],[xinxeguanqita]
           ,[feihehuxi],[feihehuxiqita],[bnsz],[bnszqt],[gdcw],[gdcwqt],[shenjing]
           ,[shenjingqita],[xueye],[xueye1],[xueyeqita],[nfbdx],[nfbdxqita]
           ,[jirou],[jirouqita],[jingshen],[jingshenqita],[ck],[chankeqita],[xiyan]
           ,[xiyanqita],[gms],[gmsqita],[jwmzs],[jwmzsqt],[jzs],[jzsqita]
           ,[xzytsyw],[xzytsyw1],[xzytsywqt],[qsqk],[qsqk1],[qsqkqt],[qdtcd],[zhangkou]
           ,[yaci],[mazuiczbw],[xiongpian],[xiongpianqita],[xindiantu],[xdtqt]
           ,[xcg],[xcgqt],[mianyi],[miaoyiqt],[ningxue],[ningxueqt],[shenghua]
           ,[shenghuaqt],[qita],[qita1],[zongtpg],[asafj],[sfbw],[mqczwtjy]
           ,[sqmzysqz],[riqi])
           VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',
           '{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}',
           '{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}',
           '{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}',
           '{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}',
           '{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}',
           '{60}','{61}','{62}','{63}','{64}')";
            string sql = string.Format(insert, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        public int UpdateMazuimzpgdan(Dictionary<string, string> dictionary)
        {
            string sql = @"UPDATE [MZmzpgdan]
   SET [BP] = '{1}',[R] = '{2}',[P] = '{3}',[T] = '{4}',[xuexing] ='{5}'
      ,[xinxueguan] = '{6}',[xinxeguanqita] ='{7}',[feihehuxi] = '{8}'
      ,[feihehuxiqita] = '{9}',[bnsz] = '{10}',[bnszqt] = '{11}',[gdcw] ='{12}'
      ,[gdcwqt] ='{13}',[shenjing] = '{14}',[shenjingqita] = '{15}',[xueye] = '{16}'
      ,[xueye1] ='{17}',[xueyeqita] = '{18}',[nfbdx] = '{19}',[nfbdxqita] = '{20}'
      ,[jirou] = '{21}',[jirouqita] = '{22}',[jingshen] = '{23}',[jingshenqita] = '{24}'
      ,[ck] = '{25}',[chankeqita] = '{26}',[xiyan] ='{27}',[xiyanqita] ='{28}'
      ,[gms] = '{29}',[gmsqita] = '{30}',[jwmzs] = '{31}',[jwmzsqt] = '{32}'
      ,[jzs] = '{33}',[jzsqita] = '{34}' ,[xzytsyw] = '{35}',[xzytsyw1] = '{36}'
      ,[xzytsywqt] = '{37}',[qsqk] = '{38}',[qsqk1] = '{39}',[qsqkqt] = '{40}'
      ,[qdtcd] = '{41}',[zhangkou] = '{42}',[yaci] = '{43}',[mazuiczbw] = '{44}'
      ,[xiongpian] = '{45}',[xiongpianqita] = '{46}',[xindiantu] = '{47}'
      ,[xdtqt] = '{48}',[xcg] = '{49}',[xcgqt] ='{50}',[mianyi] = '{51}'
      ,[miaoyiqt] = '{52}',[ningxue] = '{53}',[ningxueqt] = '{54}',[shenghua] = '{55}'
      ,[shenghuaqt] = '{56}',[qita] = '{57}',[qita1] = '{58}',[zongtpg] = '{59}'
      ,[asafj] = '{60}',[sfbw] = '{61}',[mqczwtjy] = '{62}',[sqmzysqz] = '{63}'
      ,[riqi] = '{64}' WHERE [zhuyuanhao] = '{0}'";
            string sql1 = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql1);
        }

        public int UpdateMazuimzpgdantizhong(string tizhong, string menzhenhaocishu)
        {
            string sql = @"UPDATE [admin_mazuimenzhen]
   SET [tizhong] ='" + tizhong + "'  WHERE [menzhenhaocishu] = '" + menzhenhaocishu + "'";
           // string sql1 = string.Format(sql, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }
        #endregion


        
    }
}
