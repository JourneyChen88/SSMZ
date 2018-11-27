using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using adims_MODEL;
using adims_DAL;
namespace adims_BLL
{
    public class BLL
    {
        #region <<Members>>

        adims_DAL.DBConn dBConn = new adims_DAL.DBConn();

        #endregion

        #region <<T_SQL>>

        #region <<术前访视>>

        private static readonly string SQL_BEFOREVISIT_GETLIST = " SELECT BeforeVisitID,PatID,Weight,Blood,HeartRate,Pulse,"
            + "Breathing,BT,Awareness,SSMode,HistoryDrugs,IsAnesthesiaHistory,AnesthesiaHistoryRemark,IsAllergyHistory,"
            + "AllergyHistoryRemark,HeadNeck,Dehisce,Tooth,HLAuscultation,ASAClassification,E,MuscleFeeling,FeelingAbnormal,"
            + "MuscleDrop,Other,PeripheralVenous,SpineCondition,HLClassification,LungFunction,ECG,Chest,LiverFunction,"
            + "LiverFunctionRemark,KidneyFunction,KidneyFunctionRemark,Hemoglobin,Erythrocyte,Hematocrit,BTB,FG,APTT,ThrombinDate,"
            + "Potassium,Hyponatremia,SerumChloride,BloodSugar,OtherAbnormal,AProgram,AMethod,ADrug,MProjects,ProblemHandle,ProtectiveMeasures,Physician,AccessDate,"
            + "CreateDate,UpdateDate  FROM Adims_BeforeVisit WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_BEFOREVISIT_COUNT = " SELECT Count(*) FROM Adims_BeforeVisit WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_BEFOREVISIT_INSERT = "INSERT INTO Adims_BeforeVisit(Weight,Blood,HeartRate,Pulse,"
            + "Breathing,BT,Awareness,SSMode,HistoryDrugs,IsAnesthesiaHistory,AnesthesiaHistoryRemark,IsAllergyHistory,"
            + "AllergyHistoryRemark,HeadNeck,Dehisce,Tooth,HLAuscultation,ASAClassification,E,MuscleFeeling,"
            + "FeelingAbnormal,MuscleDrop,Other,PeripheralVenous,SpineCondition,HLClassification,LungFunction,ECG,"
            + "Chest,LiverFunction,LiverFunctionRemark,KidneyFunction,KidneyFunctionRemark,Hemoglobin,Erythrocyte,"
            + "Hematocrit,BTB,FG,APTT,ThrombinDate,Potassium,Hyponatremia,SerumChloride,BloodSugar,OtherAbnormal,AProgram,AMethod,ADrug,MProjects,ProblemHandle,"
            + "ProtectiveMeasures,Physician,AccessDate,PatID,CreateDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',"
            + "'{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',"
            + "'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',"
            + "'{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}',"
            + "'{49}','{50}','{51}','{52}','{53}',GETDATE())";
        private static readonly string SQL_BEFOREVISIT_UPDATE = " UPDATE Adims_BeforeVisit WITH (ROWLOCK) SET Weight = '{0}',"
            + "Blood = '{1}',HeartRate = '{2}',Pulse = '{3}',Breathing = '{4}',BT = '{5}',Awareness = '{6}',"
            + "SSMode = '{7}',HistoryDrugs = '{8}',IsAnesthesiaHistory = '{9}',AnesthesiaHistoryRemark = '{10}',"
            + "IsAllergyHistory = '{11}',AllergyHistoryRemark = '{12}',HeadNeck = '{13}',Dehisce = '{14}',"
            + "Tooth = '{15}',HLAuscultation = '{16}',ASAClassification = '{17}',E = '{18}',MuscleFeeling = '{19}',"
            + "FeelingAbnormal = '{20}',MuscleDrop = '{21}',Other = '{22}',PeripheralVenous = '{23}',"
            + "SpineCondition = '{24}',HLClassification = '{25}',LungFunction = '{26}',ECG = '{27}',Chest = '{28}',"
            + "LiverFunction = '{29}',LiverFunctionRemark = '{30}',KidneyFunction = '{31}',KidneyFunctionRemark = '{32}',"
            + "Hemoglobin = '{33}',Erythrocyte = '{34}',Hematocrit = '{35}',BTB = '{36}',FG = '{37}',APTT = '{38}',"
            + "ThrombinDate = '{39}',Potassium = '{40}',Hyponatremia = '{41}',SerumChloride = '{42}',BloodSugar = '{43}',"
            + "OtherAbnormal = '{44}',AProgram = '{45}',AMethod = '{46}',ADrug = '{47}',MProjects = '{48}',ProblemHandle = '{49}',"
            + "ProtectiveMeasures = '{50}',Physician = '{51}',AccessDate = '{52}',PatID = '{53}',UpdateDate = GETDATE()";

        #endregion

        #region <<术后随访>>

        private static readonly string SQL_AFTERVISIT_COUNT = "SELECT Count(*) FROM Adims_AfterVisit WITH (NOLOCK) WHERE PatID = '{0}'";
        private static readonly string SQL_AFTERVISIT_SELECT = "SELECT AfterVisitID,PatID,EnterBP,EnterHeartRate,EnterSpo2,"
            + "OpenVein,MainDrug,Intubate,AnesthesiaInduction,SZOutAmount,SZInAmount,SBSituation,SBWhereabouts,SZBP,"
            + "SZBPCauses,SZHeartRate,SZHeartRateCauses,SZSpo2,SZSpo2Causes,Nausea1,NauseaValue1,Vomit1,VomitValue1,"
            + "Headache1,HeadacheValue1,WaistlegsPain1,WaistlegsPainValue1,URetention1,URetentionValue1,BPReduce1,"
            + "BPReduceValue1,BPElevated1,BPElevatedValue1,HHyperlipidemia1,HHyperlipidemiaValue1,BSuppression1,"
            + "BSuppressionValue1,ThroatPain1,ThroatPainValue1,LNumbness1,LNumbnessValue1,Other11,Other1Flag1,"
            + "Other1Value1,Other21,Other2Flag1,Other2Value1,Other31,Other3Flag1,Other3Value1,AnalgesicScore1,"
            + "Nausea2,NauseaValue2,Vomit2,VomitValue2,Headache2,HeadacheValue2,WaistlegsPain2,WaistlegsPainValue2,"
            + "URetention2,URetentionValue2,BPReduce2,BPReduceValue2,BPElevated2,BPElevatedValue2,HHyperlipidemia2,"
            + "HHyperlipidemiaValue2,BSuppression2,BSuppressionValue2,ThroatPain2,ThroatPainValue2,LNumbness2,"
            + "LNumbnessValue2,Other12,Other1Flag2,Other1Value2,Other22,Other2Flag2,Other2Value2,Other32,Other3Flag2,"
            + "Other3Value2,AnalgesicScore2,Nausea3,NauseaValue3,Vomit3,VomitValue3,Headache3,HeadacheValue3,"
            + "WaistlegsPain3,WaistlegsPainValue3,URetention3,URetentionValue3,BPReduce3,BPReduceValue3,BPElevated3,"
            + "BPElevatedValue3,HHyperlipidemia3,HHyperlipidemiaValue3,BSuppression3,BSuppressionValue3,ThroatPain3,"
            + "ThroatPainValue3,LNumbness3,LNumbnessValue3,Other13,Other1Flag3,Other1Value3,Other23,Other2Flag3,"
            + "Other2Value3,Other33,Other3Flag3,Other3Value3,AnalgesicScore3  FROM Adims_AfterVisit WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_AFTERVISIT_INSERT = "INSERT INTO Adims_AfterVisit(EnterBP,EnterHeartRate,"
            + "EnterSpo2,OpenVein,MainDrug,Intubate,AnesthesiaInduction,SZOutAmount,SZInAmount,SBSituation,SBWhereabouts,"
            + "SZBP,SZBPCauses,SZHeartRate,SZHeartRateCauses,SZSpo2,SZSpo2Causes,Nausea1,NauseaValue1,Vomit1,VomitValue1,"
            + "Headache1,HeadacheValue1,WaistlegsPain1,WaistlegsPainValue1,URetention1,URetentionValue1,BPReduce1,"
            + "BPReduceValue1,BPElevated1,BPElevatedValue1,HHyperlipidemia1,HHyperlipidemiaValue1,BSuppression1,"
            + "BSuppressionValue1,ThroatPain1,ThroatPainValue1,LNumbness1,LNumbnessValue1,Other11,Other1Flag1,"
            + "Other1Value1,Other21,Other2Flag1,Other2Value1,Other31,Other3Flag1,Other3Value1,AnalgesicScore1,"
            + "Nausea2,NauseaValue2,Vomit2,VomitValue2,Headache2,HeadacheValue2,WaistlegsPain2,WaistlegsPainValue2,"
            + "URetention2,URetentionValue2,BPReduce2,BPReduceValue2,BPElevated2,BPElevatedValue2,HHyperlipidemia2,"
            + "HHyperlipidemiaValue2,BSuppression2,BSuppressionValue2,ThroatPain2,ThroatPainValue2,LNumbness2,"
            + "LNumbnessValue2,Other12,Other1Flag2,Other1Value2,Other22,Other2Flag2,Other2Value2,Other32,Other3Flag2,"
            + "Other3Value2,AnalgesicScore2,Nausea3,NauseaValue3,Vomit3,VomitValue3,Headache3,HeadacheValue3,"
            + "WaistlegsPain3,WaistlegsPainValue3,URetention3,URetentionValue3,BPReduce3,BPReduceValue3,BPElevated3,"
            + "BPElevatedValue3,HHyperlipidemia3,HHyperlipidemiaValue3,BSuppression3,BSuppressionValue3,ThroatPain3,"
            + "ThroatPainValue3,LNumbness3,LNumbnessValue3,Other13,Other1Flag3,Other1Value3,Other23,Other2Flag3,"
            + "Other2Value3,Other33,Other3Flag3,Other3Value3,AnalgesicScore3,PatID,CreateDate) VALUES ('{0}','{1}','{2}',"
            + "'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',"
            + "'{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}',"
            + "'{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}',"
            + "'{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}',"
            + "'{60}','{61}','{62}','{63}','{64}','{65}','{66}','{67}','{68}','{69}','{70}','{71}','{72}','{73}',"
            + "'{74}','{75}','{76}','{77}','{78}','{79}','{80}','{81}','{82}','{83}','{84}','{85}','{86}','{87}',"
            + "'{88}','{89}','{90}','{91}','{92}','{93}','{94}','{95}','{96}','{97}','{98}','{99}','{100}','{101}',"
            + "'{102}','{103}','{104}','{105}','{106}','{107}','{108}','{109}','{110}','{111}','{112}','{113}',GETDATE()) ";
        private static readonly string SQL_AFTERVISIT_UPDATE = "UPDATE Adims_AfterVisit WITH (ROWLOCK)  SET EnterBP = '{0}',"
            + "EnterHeartRate = '{1}',EnterSpo2 = '{2}',OpenVein = '{3}',MainDrug = '{4}',Intubate = '{5}',AnesthesiaInduction = '{6}',"
            + "SZOutAmount = '{7}',SZInAmount = '{8}',SBSituation = '{9}',SBWhereabouts = '{10}',SZBP = '{11}',SZBPCauses = '{12}',"
            + "SZHeartRate = '{13}',SZHeartRateCauses = '{14}',SZSpo2 = '{15}',SZSpo2Causes = '{16}',Nausea1 = '{17}',"
            + "NauseaValue1 = '{18}',Vomit1 = '{19}',VomitValue1 ='{20}',Headache1 = '{21}',HeadacheValue1 = '{22}',"
            + "WaistlegsPain1 ='{23}' ,WaistlegsPainValue1 = '{24}',URetention1 = '{25}',URetentionValue1 = '{26}',"
            + "BPReduce1 = '{27}',BPReduceValue1 = '{28}',BPElevated1 = '{29}',BPElevatedValue1 ='{30}',HHyperlipidemia1 = '{31}',"
            + "HHyperlipidemiaValue1 = '{32}',BSuppression1 = '{33}',BSuppressionValue1 = '{34}',ThroatPain1 = '{35}',"
            + "ThroatPainValue1 = '{36}',LNumbness1 = '{37}',LNumbnessValue1 = '{38}',Other11 = '{39}',Other1Flag1 = '{40}',"
            + "Other1Value1 = '{41}',Other21 = '{42}',Other2Flag1 = '{43}',Other2Value1 = '{44}',Other31 = '{45}',Other3Flag1 = '{46}',"
            + "Other3Value1 = '{47}',AnalgesicScore1 = '{48}',Nausea2 = '{49}',NauseaValue2 = '{50}',Vomit2 = '{51}',VomitValue2 ='{52}',"
            + "Headache2 = '{53}',HeadacheValue2 = '{54}',WaistlegsPain2 = '{55}',WaistlegsPainValue2 = '{56}',URetention2 = '{57}',"
            + "URetentionValue2 = '{58}',BPReduce2 = '{59}',BPReduceValue2 = '{60}',BPElevated2 = '{61}',BPElevatedValue2 = '{62}',"
            + "HHyperlipidemia2 = '{63}',HHyperlipidemiaValue2 = '{64}',BSuppression2 = '{65}',BSuppressionValue2 = '{66}',"
            + "ThroatPain2 = '{67}',ThroatPainValue2 = '{68}',LNumbness2 = '{69}',LNumbnessValue2 = '{70}',Other12 = '{71}',"
            + "Other1Flag2 = '{72}',Other1Value2 ='{73}',Other22 = '{74}',Other2Flag2 = '{75}',Other2Value2 = '{76}',Other32 = '{77}'"
            + ",Other3Flag2 = '{78}',Other3Value2 = '{79}',AnalgesicScore2 = '{80}',Nausea3 = '{81}',NauseaValue3 = '{82}',"
            + "Vomit3 = '{83}',VomitValue3 = '{84}',Headache3 = '{85}',HeadacheValue3 ='{86}',WaistlegsPain3 = '{87}',"
            + "WaistlegsPainValue3 = '{88}',URetention3 = '{89}',URetentionValue3 = '{90}',BPReduce3 = '{91}',BPReduceValue3 = '{92}',"
            + "BPElevated3 = '{93}',BPElevatedValue3 = '{94}',HHyperlipidemia3 = '{95}',HHyperlipidemiaValue3 = '{96}',BSuppression3 = '{97}',"
            + "BSuppressionValue3 = '{98}',ThroatPain3 = '{99}',ThroatPainValue3 = '{100}',LNumbness3 ='{101}',LNumbnessValue3 ='{102}',"
            + "Other13 ='{105}',Other1Flag3 ='{104}',Other1Value3 = '{105}',Other23 = '{106}',Other2Flag3 = '{107}',Other2Value3 = '{108}',"
            + "Other33 = '{109}',Other3Flag3 = '{110}',Other3Value3 ='{111}',AnalgesicScore3 = '{112}',UpdateDate = GETDATE() WHERE PatID = '{113}'";

        #endregion

        #region <<护理记录>>

        private static readonly string SQL_NURSERECORD_GETLIST = "SELECT NurseRecordID,PatID,EnterTime,LeaveTime,PatAbouts,"
            + "AboutsValue,SPostural,PosturalValue,IsInfusion,InfusionSum,IsBlood,BWhole,BPlasma,BPlatelet,Cryoprecipitate,"
            + "BloodValue,SBefore,BeforeValue,SAfter,AfterValue,IsElectricKnife,PasteParts,PastePartsValue,IsTourniquet,"
            + "Position,PositionValue,HeatingDevice,HeatingValue,Indwelling,IsSample,SampleNum,IsSlice,SliceNum,IsDrainage,"
            + "DrainageValue,CreateDate,UpdateDate FROM Adims_NurseRecord WITH (NOLOCK) WHERE {0}";
        private static readonly string SQL_NURSERECORD_COUNT = "SELECT Count(*) FROM Adims_NurseRecord WHERE PatID = '{0}'";
        private static readonly string SQL_NURSERECORD_INSERT = "INSERT INTO Adims_NurseRecord(PatID,EnterTime,LeaveTime,"
            + "PatAbouts,AboutsValue,SPostural,PosturalValue,IsInfusion,InfusionSum,IsBlood,BWhole,"
            + "BPlasma,BPlatelet,Cryoprecipitate,BloodValue,SBefore,BeforeValue,SAfter,AfterValue,"
            + "IsElectricKnife,PasteParts,PastePartsValue,IsTourniquet,Position,PositionValue,"
            + "HeatingDevice,HeatingValue,Indwelling,IsSample,SampleNum,IsSlice,SliceNum,IsDrainage,"
            + "DrainageValue,CreateDate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',"
            + "'{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',"
            + "'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}',GETDATE())";
        private static readonly string SQL_NURSERECORD_UPDATE = "UPDATE Adims_NurseRecord WITH (ROWLOCK)  SET EnterTime = '{0}',"
            + "LeaveTime = '{1}',PatAbouts = '{2}',AboutsValue = '{3}',SPostural = '{4}',PosturalValue = '{5}',"
            + "IsInfusion ='{6}',InfusionSum = '{7}',IsBlood = '{8}',BWhole = '{9}',BPlasma ='{10}',BPlatelet = '{11}',"
            + "Cryoprecipitate = '{12}',BloodValue = '{13}',SBefore = '{14}',BeforeValue = '{15}',SAfter = '{16}',"
            + "AfterValue = '{17}',IsElectricKnife = '{18}',PasteParts = '{19}',PastePartsValue = '{20}',IsTourniquet = '{21}',"
            + "Position = '{22}',PositionValue = '{23}',HeatingDevice = '{24}',HeatingValue = '{25}',Indwelling = '{26}',"
            + "IsSample = '{27}',SampleNum = '{28}',IsSlice = '{29}',SliceNum = '{30}',IsDrainage = '{31}',DrainageValue = '{32}',"
            + "UpdateDate = GETDATE()  WHERE PatID = '{33}'";

        #endregion

        #endregion

        #region <<Methods>>
        #region     //PACU模块中使用的方法
        public int SavaPACU_record(PACU_record record)  //病人恢复信息保存
        {
            return dBConn.ExecuteNonQuery("insert into Adims_PACU_record (patID,visitDate,cautions,observeTime,concious,liquid,urin,cough,stimulate,breathe,duralPlane,limb,recorder) Values('" +
               record.patID + "','" + record.visitDate + "','" + record.cautions + "','" + record.observeTime + "','" + record.concious + "','" + record.liquid + "','" + record.urin + "','" + record.cough + "','" +
               record.stimulate + "','" + record.breathe + "','" + record.duralPlane + "','" + record.limb + "','" + record.recorder + "')"
                );
        }
        public DataSet PACU_table1()
        {
            return dBConn.GetDataSet("select Adims_Otypesetting.patdpm 病区, Adims_Otypesetting.ID 手术单号, Adims_Otypesetting.patname 姓名,Adims_Otypesetting.patsex 性别,Adims_Otypesetting.patage 年龄," +
                 "Adims_Otypesetting.Oname 手术名称,m_mzjld.ssys 手术医生, m_mzjld.szzd 术中诊断,m_mzjld.mzys 麻醉医生, m_mzjld.mzfa 麻醉方式,Adims_Otypesetting.Odate 手术日期,Adims_Otypesetting.patbedno 床号," +
            "m_mzjld.patid  患者ID from Adims_Otypesetting,m_mzjld where Adims_Otypesetting.Patid=m_mzjld.Patid order by Odate DESC");
        }

#endregion
        #region    //药品管理模块中使用的方法
        #region     //手术室药品管理    

        #region    //药品管理

        public DataSet ypxhtj_table()
        {
            return dBConn.GetDataSet("select ypbh 药品编号,ypname 药品名称,ypGg 药品规格,pyzt 拼音字头,dl 毒理,zt 状态,xisl 消耗数量 from ypxhtj");
        }

        public int Add_surgery_stock(surgery_stock stock)
        {
            string com_str = "insert into adims_surgery_stock(surgery_id,medicine_number,medicine_name,phonetic_prefix," +
       "toxicology,state,dosagy_form,specification,count,produce_time,deadline,batch_number,origin_place) values('" +
       stock.surgery_id + "','" + stock.medicine_number + "','" + stock.medicine_name + "','" + stock.phonetic_prefix + "','" +
       stock.toxicology + "','" + stock.state + "','" + stock.dosagy_form + "','" + stock.specification + "','" + stock.count + "','" +
       stock.produce_time + "','" + stock.deadline + "','" + stock.batch_number + "','" + stock.origin_place + "')";
            return dBConn.ExecuteNonQuery(com_str);
        }  
        public int Add_surgery_input(surgery_input input)
        {
            string com_str = "insert into adims_surgery_input(surgery_id,medicine_number,input_count,input_time,confirm_person) values('" +
        input.surgery_id + "','" + input.medicine_number + "','" + input.input_count + "','" + input.input_time + "','" + input.confirm_person + "')";
            return dBConn.ExecuteNonQuery(com_str);
        }
        public int UpdateAdd_surgery_stock(surgery_stock stock) 
       {
        string com_str="select count(*) from adims_surgery_stock where medicine_number='"+stock.medicine_number+"'";
        int count=(int)dBConn.ExecuteScalar(com_str);
        if (0 == count) { if (1 == Add_surgery_stock(stock))return 1; else return 0; }
        else
        {
            com_str = "update adims_surgery_stock set count=count+'" + stock.count + "'" + " where medicine_number='" +
           stock.medicine_number + "'";
            return dBConn.ExecuteNonQuery(com_str);
        }
    }
        public DataSet select_surgery_input()
        {
            string com_str = "select Adims_surgery_input.surgery_id 手术室号,Adims_surgery_input.medicine_number 药品编号," +
                "Adims_surgery_input.input_count 输入数量,Adims_surgery_input.input_time 输入时间,Adims_surgery_input.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from adims_surgery_stock,adims_surgery_input " +
                "where adims_surgery_stock.medicine_number=adims_surgery_input.medicine_number order by Adims_surgery_input.input_time DESC";
            return dBConn.GetDataSet(com_str);
        }
        public int Add_surgery_output(surgery_output output)
        {
            string com_str = "insert into adims_surgery_output(surgery_id,room_id,medicine_number,output_count,confirm_person,output_time) " +
                "values('" + output.surgery_id +"','"+output.room_id+ "','" + output.medicine_number + "','" + output.output_count + "','" + output.confirm_person +
                "','" + output.output_time + "')";
            return dBConn.ExecuteNonQuery(com_str);
        }
        public int UpdateSub_surgery_stock(surgery_output output)
        {
            string com_str = "select count(*) from adims_surgery_stock where medicine_number='" + output.medicine_number + "'";
            int count = (int)dBConn.ExecuteScalar(com_str);
            if (0 == count) { return 0; }
            else
            {
                com_str = "update adims_surgery_stock set count=count-'" + output.output_count + "'" + " where medicine_number='" +
               output.medicine_number + "'";
                return dBConn.ExecuteNonQuery(com_str);
            }
        }
        public DataSet select_surery_output()
        {
            string com_str = "select Adims_surgery_output.surgery_id 手术室号,Adims_surgery_output.room_id 手术间号,Adims_surgery_output.medicine_number 药品编号," +
                "Adims_surgery_output.output_count 输出数量,Adims_surgery_output.output_time 输出时间,Adims_surgery_output.confirm_person 确认人," +
                "medicine_name 药品名称,phonetic_prefix 拼音字头,toxicology 毒理,state 状态," +
                "dosagy_form 剂型,specification 规格,produce_time 生产日期,deadline 有效日期," +
                "batch_number 批次,origin_place 产地 from adims_surgery_stock,adims_surgery_output " +
                "where adims_surgery_stock.medicine_number=adims_surgery_output.medicine_number order by Adims_surgery_output.output_time DESC";
            return dBConn.GetDataSet(com_str);
        }

#endregion

        #endregion

        #region   //手术间药品管理
#endregion
        #region   //病人用药管理
        #endregion
#endregion
        #region <<方法信息>>

        


        /// <summary>
        /// 术前访视病人信息查询
        /// </summary>
        /// <returns></returns>
        public DataSet Sqfs()
        {
            return dBConn.GetDataSet("select patid 病人ID,patname 姓名,patage 年龄,patsex 性别,patdpm 病区,patbedno 床号,"
                + "oname 拟实施手术,pattmd 主要诊断,os 手术医师,oname 手术名字,amethod 麻醉方法,ap1 主麻医师,ap2 副麻医师1,"
                + "ap3 副麻医师2,on1 洗手护士1,on2 洗手护士2,sn1 巡回护士1,sn2 巡回护士2,remarks 备注 from Adims_OTypesetting "
                + "WITH (NOLOCK) where CONVERT(varchar, Odate , 23 ) < ='" + DateTime.Now.Date.ToString("yyyy-MM-dd") + "'");
        }

        /// <summary>
        /// 添加术前访视
        /// </summary>
        /// <param name="sqfs"></param>
        /// <returns></returns>
        public int InsertSqfs(adims_MODEL.sqfs sqfs)
        {
            string sql = "INSERT INTO Adims_Sqfs(patid,weight,xy,xl,mb,hx,tw,ys,zqojz,xtbsjzlyw,"
                + "ssmzs,gms,tjb,kqzk,kqyc,xftz,asa,e,jljgj,gjyc,jljt,qt,wzjm,jzzk,xfgnfj,fgn,xdt,xphxt,ggn,"
                + "sgn,xhdb,hxb,xxbbr,xxxb,fg,aptt,nxmysj,xj,xn,xuelv,xt,xqfx,mzfa,fhcs,fsys,date) VALUES"
                + "('" + sqfs.Patid + "','" + sqfs.Tz + "','" + sqfs.Xy + "','" + sqfs.Xl + "','" + sqfs.Mb
                + "','" + sqfs.Hx + "','" + sqfs.Tw + "','" + sqfs.Ys + "','" + sqfs.Zqojz + "','" + sqfs.Xtbs
                + "','" + sqfs.Ssmzs + "','" + sqfs.Gms + "','" + sqfs.Tjb + "','" + sqfs.Kqzk + "','" + sqfs.Kqyc
                + "','" + sqfs.Xftz + "','" + sqfs.Asa + "','" + sqfs.E + "','" + sqfs.Jljgj + "','" + sqfs.Gjyc
                + "','" + sqfs.Jljt + "','" + sqfs.Qt + "','" + sqfs.Wzjm + "','" + sqfs.Jzzk + "','" + sqfs.Xfgnfj
                + "','" + sqfs.Fgn + "','" + sqfs.Xdt + "','" + sqfs.Xphxt + "','" + sqfs.Ggn + "','" + sqfs.Sgn
                + "','" + sqfs.Xhdb + "','" + sqfs.Hxb + "','" + sqfs.Xxbbr + "','" + sqfs.Xxxb + "','" + sqfs.Fg
                + "','" + sqfs.Aptt + "','" + sqfs.Nxmysj + "','" + sqfs.Xj + "','" + sqfs.Xn + "','" + sqfs.Xuelv
                + "','" + sqfs.Xt + "','" + sqfs.Xqfx + "','" + sqfs.Mzfa + "','" + sqfs.Fhcs + "','" + sqfs.Fsys
                + "','" + sqfs.Date + "')";
            return dBConn.ExecuteNonQuery(sql);
        }


        /// <summary>
        /// 查询手术信息
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public DataSet Osel(string str)
        {
            return dBConn.GetDataSet("select onname 手术名称,olevel 手术等级,ospell 检索拼音 from OperationName where ospell like'%" + str + "%' ");
        }

        /// <summary>
        /// 气体使用情况
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <returns></returns>
        public DataSet qtsy(int mzjldid)
        {
            return dBConn.GetDataSet("select qtname,yl,dw,sytime,jstime,flags from qtuse where mzjldid='" + mzjldid + "'");
        }

        /// <summary>
        /// 已排版对应麻醉医生的患者
        /// </summary>
        /// <param name="ap"></param>
        /// <returns></returns>
        public DataSet xssbr(string ap)
        {
            string sql = "select patid 病人ID,Patname 病人姓名,patsex 性别, patage 年龄,pattmd 主要诊断,"
                + "oname 手术名称,patdpm 科室 from Adims_OTypesetting where ap1='" + ap + "' or ap2='" + ap + "' or ap3='" + ap + "'";
            return dBConn.GetDataSet(sql);
        }

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="pat"></param>
        public void GetPatInfo(int patID, adims_MODEL.paiban pat)
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select patid 病人ID,patname 姓名,patage 年龄,patsex 性别,patdpm 病区,patbedno 床号,"
                + "oname 拟实施手术,pattmd 主要诊断,os 手术医师,amethod 麻醉方法,ap1 主麻医师,ap2 副麻医师1,ap3 副麻医师2,"
                + "asa,asae from Adims_OTypesetting where patid='" + patID + "'");
            pat.Patid = ds.Tables[0].Rows[0][0].ToString();
            pat.Patname = ds.Tables[0].Rows[0][1].ToString();
            pat.Patage = (int)ds.Tables[0].Rows[0][2];
            pat.Patsex = ds.Tables[0].Rows[0][3].ToString();
            pat.Department = ds.Tables[0].Rows[0][4].ToString();
            pat.Bednumber = ds.Tables[0].Rows[0][5].ToString();
            pat.Oname = ds.Tables[0].Rows[0][6].ToString();
            pat.TMD1 = ds.Tables[0].Rows[0][7].ToString();
            pat.Os = ds.Tables[0].Rows[0][8].ToString();
            pat.Amethod = ds.Tables[0].Rows[0][9].ToString();
            pat.Ap1 = ds.Tables[0].Rows[0][10].ToString();
            pat.Ap2 = ds.Tables[0].Rows[0][11].ToString();
            pat.Ap3 = ds.Tables[0].Rows[0][12].ToString();
            pat.Asa = ds.Tables[0].Rows[0][13].ToString();
            pat.Asae = (int)ds.Tables[0].Rows[0][14];
        }

        /// <summary>
        /// 创建麻醉记录单
        /// </summary>
        /// <param name="patid"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public int cmzjld(int patID, bool flags)
        {
            int i;
            object obj = dBConn.ExecuteScalar("select id from m_mzjld where patid='" + patID + "'");
            if (obj == null || flags)
            {
                dBConn.ExecuteNonQuery("INSERT INTO m_mzjld(patid) values('" + patID + "')");
                i = (int)dBConn.ExecuteScalar("select max(id) from m_mzjld where patid='" + patID + "'");
            }
            else
                i = (int)obj;
            return i;
        }

        /// <summary>
        /// 添加气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int addqt(int mzjldid, adims_MODEL.mzqt qt)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO m_qtuse(mzjldid,qtname,yl,dw,sytime,flags) "
                + "values('" + mzjldid + "','" + qt.Qtname + "','" + qt.Yl + "','" + qt.Dw + "','" + qt.Sysj + "',1)");
        }

        /// <summary>
        /// 结束气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int endqt(int mzjldid, adims_MODEL.mzqt qt)
        {
            return dBConn.ExecuteNonQuery("update m_qtuse set jstime='" + qt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and qtname='" + qt.Qtname + "'");
        }

        /// <summary>
        /// 删除气体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="qt"></param>
        /// <returns></returns>
        public int delqt(int mzjldid, adims_MODEL.mzqt qt)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM m_qtuse WHERE mzjldid='" + mzjldid + "'and qtname = '" + qt.Qtname + "' ");
        }

        /// <summary>
        /// 液体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int addyt(int mzjldid, adims_MODEL.mzyt yt)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO m_ytuse(mzjldid,ytname,yl,dw,yyfs,cxyy,sytime,flags) "
                + "values('" + mzjldid + "','" + yt.Ytname + "','" + yt.Yl + "','" + yt.Dw + "','" + yt.Yyfs + "','" + (yt.Cxyy ? 1 : 0) + "','" + yt.Sysj + "','1')");
        }

        /// <summary>
        /// 结束液体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int endyt(int mzjldid, adims_MODEL.mzyt yt)
        {
            return dBConn.ExecuteNonQuery("update m_ytuse set jstime='" + yt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and ytname='" + yt.Ytname + "'");
        }

        /// <summary>
        /// 删除液体
        /// </summary>
        /// <param name="mzjldid"></param>
        /// <param name="yt"></param>
        /// <returns></returns>
        public int delyt(int mzjldid, adims_MODEL.mzyt yt)
        {
            return dBConn.ExecuteNonQuery("DELETE FROM m_ytuse WHERE mzjldid='" + mzjldid + "'and ytname = '" + yt.Ytname + "' ");
        }

        public int addjt(int mzjldid, adims_MODEL.jtytsx jt)//添加晶体胶体输血
        {
            return dBConn.ExecuteNonQuery("INSERT INTO m_jtjtsx(mzjldid,type,name,jl,dw,zrfs,kssj,flags) values('" + mzjldid + "','" + jt.Ytlx + "','" + jt.Name + "','" + jt.Jl + "','" + jt.Dw + "','" + jt.Zrfs + "','" + jt.Kssj + "','1')");
        }

        public int endjt(int mzjldid, adims_MODEL.jtytsx jt)//结束晶体等
        {
            return dBConn.ExecuteNonQuery("update m_jtjtsx set jssj='" + jt.Jssj + "',flags='2' where mzjldid='" + mzjldid + "'and name='" + jt.Name + "' and type='" + jt.Ytlx + "'");
        }

        public int deljt(int mzjldid, adims_MODEL.jtytsx jt)//删除晶体等
        {
            return dBConn.ExecuteNonQuery("DELETE FROM m_jtjtsx WHERE mzjldid='" + mzjldid + "'and name = '" + jt.Name + "' and type='" + jt.Ytlx + "' ");
        }

        public int addpoint(int mzjldid, adims_MODEL.point p)//添加点
        {

            return dBConn.ExecuteNonQuery("INSERT INTO m_point(mzjldid,lx,value,time) values('" + mzjldid + "','" + p.Lx + "','" + p.V + "','" + p.D + "')");
        }

        public int xgqt(int mzjldid, adims_MODEL.mzqt qt)//修改气体
        {
            return dBConn.ExecuteNonQuery("update m_qtuse set sytime='" + qt.Sysj + "' where mzjldid='" + mzjldid + "'and qtname='" + qt.Qtname + "'");
        }

        public int xgyt(int mzjldid, adims_MODEL.mzyt yt)//修改液体
        {
            return dBConn.ExecuteNonQuery("update m_ytuse set sytime='" + yt.Sysj + "' where mzjldid='" + mzjldid + "'and ytname='" + yt.Ytname + "'");
        }

        public int xgjt(int mzjldid, adims_MODEL.jtytsx jt)//修改晶体等
        {
            return dBConn.ExecuteNonQuery("update m_jtjtsx set kssj='" + jt.Kssj + "' where mzjldid='" + mzjldid + "'and name='" + jt.Name + "' and type='" + jt.Ytlx + "'");
        }

        public int xgpoint(int mzjldid, adims_MODEL.point p)//修改点
        {

            return dBConn.ExecuteNonQuery("update m_point set value='" + p.V + "' where mzjldid='" + mzjldid + "'and lx='" + p.Lx + "' and time='" + p.D + "'");
        }

        public int addjhxm(int mzjldid, adims_MODEL.jhxm jh)//添加监护数据
        {
            return dBConn.ExecuteNonQuery("INSERT INTO m_jhxm(mzjldid,lx,value,time) values('" + mzjldid + "','" + jh.Sy + "','" + jh.V + "','" + jh.D + "')");
        }

        public int xgjhsj(int mzjldid, adims_MODEL.jhxm jh)//修改监护数据
        {
            return dBConn.ExecuteNonQuery("update m_jhxm set value='" + jh.V + "' where mzjldid='" + mzjldid + "'and lx='" + jh.Sy + "' and time='" + jh.D + "'");
        }

        public int addclcxqt(int mzjldid, adims_MODEL.clcxqt cl)//添加出量
        {
            return dBConn.ExecuteNonQuery("INSERT INTO m_cl(mzjldid,lx,value,time) values('" + mzjldid + "','" + cl.Lx + "','" + cl.V + "','" + cl.D + "')");
        }

        public int xgclsj(int mzjldid, adims_MODEL.clcxqt cl)//修改出量时间
        {
            return dBConn.ExecuteNonQuery("update m_cl set time='" + cl.D + "' where id='" + cl.Id + "'");
        }

        public int clid(int mzjldid, adims_MODEL.clcxqt cl)//查询出量ID
        {
            return (int)dBConn.ExecuteScalar("select id from m_cl where mzjldid='" + mzjldid + "' and lx='" + cl.Lx + "' and value='" + cl.V + "' and time='" + cl.D + "'");
        }

        public DataSet mzjldcx(adims_MODEL.mzjldcxtj m) //查询麻醉记录单
        {
            return dBConn.GetDataSet("select m_mzjld.id,patname,time,ssmc,mzfa,ap1,patdpm,pattmd,szzd,patsex,patage,os from m_mzjld,Adims_OTypesetting where m_mzjld.patid = Adims_OTypesetting.id");
        }

        public DataSet selqt(int mzjldid)//查找气体
        {

            return dBConn.GetDataSet("select qtname,yl,dw,sytime,jstime,flags from m_qtuse where mzjldid='" + mzjldid + "'");
        }

        public DataSet selyt(int mzjldid)//查找液体
        {
            return dBConn.GetDataSet("select ytname,yl,dw,yyfs,cxyy,sytime,jstime,flags from m_ytuse where mzjldid='" + mzjldid + "'");

        }

        public DataSet seljt(int mzjldid) //查找晶体
        {
            return dBConn.GetDataSet("select type,name,jl,dw,zrfs,kssj,jssj,flags from m_jtjtsx where mzjldid='" + mzjldid + "'");
        }

        public DataSet selpoint(int mzjldid)//查找胶体
        {

            return dBConn.GetDataSet("select lx,value,time from m_point where mzjldid='" + mzjldid + "'");
        }

        public DataSet seljhxm(int mzjldid)//查找监护项目
        {
            return dBConn.GetDataSet("select lx,value,time from m_point where mzjldid='" + mzjldid + "'");
        }

        public DataSet selbr(DateTime d)//查找指定日期的病人姓名
        {

            return dBConn.GetDataSet("select DISTINCT Adims_OTypesetting.patid,Adims_OTypesetting.patname from Adims_OTypesetting,m_mzjld where m_mzjld.patid = Adims_OTypesetting.patid ");//and m_mzjld.time='"+d.Date+"'");
        }

        public void selmzjld(adims_MODEL.shxjmzxx m, int patid) //术后小结麻醉信息
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select top 1 id,ssmc,szzd,mzfa,yssss,jhxm,tw,mzxg from m_mzjld where patid='" + patid + "' order by id desc");
            DataTable dt = ds.Tables[0];
            m.Mzjldid = Convert.ToInt32(dt.Rows[0][0]);
            m.Ssmc = Convert.ToString(dt.Rows[0][1]);
            m.Szzd = Convert.ToString(dt.Rows[0][2]);
            m.Mzfa = Convert.ToString(dt.Rows[0][3]);
            m.Yssss = Convert.ToString(dt.Rows[0][4]);
            m.Jhxm = Convert.ToString(dt.Rows[0][5]);
            m.Tw = Convert.ToString(dt.Rows[0][6]);
            m.Mzxg = Convert.ToString(dt.Rows[0][7]);
        }

        public void seloroomstate(List<adims_MODEL.oroomstate> oroom) //手术室个数
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select oname,state,mzjldid from ssjstate");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                adims_MODEL.oroomstate o = new adims_MODEL.oroomstate();
                o.Name = r[0].ToString();
                o.Ostate = Convert.ToInt32(r[1]);
                o.Mzjldid = r[2].ToString();
                oroom.Add(o);


            }

        }

        public void oroombaseinfo(adims_MODEL.ssqk ssqk) //手术室情况
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select Adims_OTypesetting.patname,m_mzjld.yssss,m_mzks.time,m_mzjld.mzys from Adims_OTypesetting,m_mzjld,m_mzks where m_mzjld.id='" + ssqk.Mzjldid + "'and m_mzjld.patid = Adims_OTypesetting.id and m_mzks.mzjldid=m_mzjld.id and m_mzks.sj=1");
            ssqk.Hzname = ds.Tables[0].Rows[0][0].ToString();
            ssqk.Oname = ds.Tables[0].Rows[0][1].ToString();
            ssqk.Kssj = Convert.ToDateTime(ds.Tables[0].Rows[0][2]);
            ssqk.Mzys = ds.Tables[0].Rows[0][3].ToString();

        }

        public int xgsqzd(int patid, string sqzd) //修改术前诊断
        {
            return dBConn.ExecuteNonQuery("update Adims_OTypesetting set pattmd='" + sqzd + "' where patid='" + patid + "'");
        }

        public int xgszzd(int mzjldid, string szzd)//修改术中诊断
        {
            return dBConn.ExecuteNonQuery("update m_mzjld set szzd='" + szzd + "' where id='" + mzjldid + "'");
        }

        public int xgsqyy(int mzjldid, string sqyy)//修改术前用药
        {
            return dBConn.ExecuteNonQuery("update m_mzjld set sqyy='" + sqyy + "' where id='" + mzjldid + "'");
        }

        public int xghbz(int mzjldid, string hbz)//修改合并症
        {
            return dBConn.ExecuteNonQuery("update m_mzjld set hbz='" + hbz + "' where id='" + mzjldid + "'");
        }

        public int xgmzff(int mzjldid, string mzff)//修改麻醉方法
        {
            return dBConn.ExecuteNonQuery("update m_mzjld set mzfa='" + mzff + "' where id='" + mzjldid + "'");
        }

        public int xgnssss(int patid, string nssss)//修改拟实施手术
        {
            return dBConn.ExecuteNonQuery("update Adims_OTypesetting set oname='" + nssss + "' where patid='" + patid + "'");
        }

        public int xgyssss(int mzjldid, string yssss)//修改已实施手术
        {
            return dBConn.ExecuteNonQuery("update m_mzjld set yssss='" + yssss + "' where id='" + mzjldid + "'");
        }

        public int xgtw(int mzjldid, string tw)//修改体位
        {
            return dBConn.ExecuteNonQuery("update m_mzjld set tw='" + tw + "' where id='" + mzjldid + "'");
        }

        public int clearpaiban(string d)//清空排班
        {
            return dBConn.ExecuteNonQuery("UPDATE Adims_OTypesetting WITH (ROWLOCK) set ap1='',ap2='',ap3='' where CONVERT(varchar, Odate , 23 )='" + d + "'");
        }

        public void quanxian(List<adims_MODEL.quanxian> qx, int zw) //查权限
        {
            DataSet ds = new DataSet();
            ds = dBConn.GetDataSet("select mulu.fml,mulu.zml from mulu,quanxian,zhiwu where quanxian.sy='" + zw + "' and quanxian.muid=mulu.id");
            foreach (DataRow r in ds.Tables[0].Rows)
            {
                adims_MODEL.quanxian t_qx = new adims_MODEL.quanxian();
                t_qx.Ml1 = Convert.ToInt32(r[0]);
                t_qx.Ml2 = Convert.ToInt32(r[1]);
                qx.Add(t_qx);

            }
        }

        public DataSet allzhiwu()
        {

            return dBConn.GetDataSet("select zhiwu from zhiwu");

        }

        public DataSet fquanxian(string zhiwu) //没有的权限
        {
            return dBConn.GetDataSet("select mulu.name from quanxian,mulu,zhiwu where zhiwu.zhiwu='" + zhiwu + "' and zhiwu.id=quanxian.sy and quanxian.muid=mulu.id");

        }

        public void inss(string s, int i1, int i2) //插入新目录  s名字，i1父目录  i2子目录  i1为-1时 为根目录
        {
            dBConn.ExecuteNonQuery("INSERT INTO mulu(name,fml,zml) values('" + s + "','" + i1 + "','" + i2 + "')");

        }

        public int insertfquanxian(string mlname, string zhiwu)//给指定职务插入不可用权限
        {
            return dBConn.ExecuteNonQuery("INSERT INTO quanxian(sy,muid) select zhiwu.id,mulu.id from zhiwu,mulu where zhiwu.zhiwu='" + zhiwu + "' and mulu.name='" + mlname + "'");
        }

        public int delfquanxian(string mlname, string zhiwu)//删除指定职务插入不可用权限
        {
            /*
                      int mlid, zhiwuid;
                 zhiwuid= (int)dBConn.ExecuteScalar("select id from zhiwu where zhiwu='" + zhiwu + "'");
                mlid= (int)dBConn.ExecuteScalar("select id from mulu where name='" + mlname + "'");
                return dBConn.ExecuteNonQuery("DELETE FROM quanxian WHERE sy='"+zhiwuid+"' and muid='"+mlid+"' ");*/
            return dBConn.ExecuteNonQuery("DELETE FROM quanxian WHERE sy=(select id from zhiwu where zhiwu='" + zhiwu + "') and muid=(select id from mulu where name='" + mlname + "')");
        }

        #endregion

        #region <<基础资料>>

        /// <summary>
        /// 获取病人信息
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetPat(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format("select patid 病人ID,patname 姓名,patage 年龄,patsex 性别,patdpm 病区,patbedno 床号,"
                + "oname 拟实施手术,pattmd 主要诊断 FROM Adims_OTypesetting WITH (NOLOCK) where {0}", sqlWhere));
        }

        /// <summary>
        /// 查询工作人员
        /// </summary>
        /// <returns></returns>
        public DataSet GetSsgzry(string sqlWhere)
        {
            return dBConn.GetDataSet("SELECT  name 姓名,gz 职称 from Adims_Ssgzry WITH (NOLOCK) WHERE 1=1 " + sqlWhere + " ORDER BY GZ");
        }

        #endregion

        #region <<智能排班>>

        /// <summary>
        /// 查询手术排班
        /// </summary>
        /// <param name="oroom"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataSet GetOTypesetting(string oroom, string dt)
        {
            string sql = "SELECT oroom 手术间,second 台次, patid 病人ID,patdpm 科室,patname 病人,os 手术医师,oname 手术名字,"
                + "ap1 主麻医师,ap2 副麻医师1,ap3 副麻医师2,on1 洗手护士1,on2 洗手护士2,sn1 巡回护士1,sn2 巡回护士2,SSYJDate 手术预计时间,"
                + "amethod 麻醉方法,patsex 性别,patage 年龄,patbedno 床号,pattmd 术前诊断,remarks 备注 from Adims_OTypesetting "
                + "WITH (NOLOCK) WHERE CONVERT(varchar, Odate , 23 ) = '" + dt + "'";
            if (oroom != "全部手术间")
                sql += " AND oroom = '" + oroom + "'";
            sql += " ORDER BY oroom";
            return dBConn.GetDataSet(sql);
        }

        /// <summary>
        /// 保存排版信息
        /// </summary>
        /// <param name="pb"></param>
        /// <returns></returns>
        public int SaveOTypesetting(adims_MODEL.paiban pb)
        {
            return dBConn.ExecuteNonQuery("INSERT INTO Adims_OTypesetting(patid,oname,oroom,second,odepartment,olevel,"
                + "amethod,gl,jz,ap1,ap2,ap3,aa1,aa2,aa3,os,oa1,oa2,oa3,oa4,tp,on1,on2,sn1,sn2,sn3,remarks,ostate,"
                + "odate)values('" + pb.Patid + "','" + pb.Oname + "','" + pb.Oroom + "','" + pb.Second + "','"
                + pb.Odepartment + "','" + pb.Olevel + "','" + pb.Amethod + "','" + pb.Gl + "','" + pb.Jz + "','"
                + pb.Ap1 + "','" + pb.Ap2 + "','" + pb.Ap3 + "','" + pb.Aa1 + "','" + pb.Aa2 + "','" + pb.Aa3
                + "','" + pb.Os + "','" + pb.Oa1 + "','" + pb.Oa2 + "','" + pb.Oa3 + "','" + pb.Oa4 + "','"
                + pb.Tp + "','" + pb.On1 + "','" + pb.On2 + "','" + pb.Sn1 + "','" + pb.Sn2 + "','" + pb.Sn3
                + "','" + pb.Remarks + "','" + pb.Ostate + "','" + pb.Odate + "')");
        }

        /// <summary>
        /// 更新排班信息
        /// </summary>
        /// <param name="patID"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int UpdateOTypesetting(int patID, string column, string value)
        {
            string sql = "UPDATE Adims_OTypesetting WITH (ROWLOCK) SET " + column + " = '" + value + "' WHERE patid = '" + patID + "'";
            return dBConn.ExecuteNonQuery(sql);
        }

        #endregion

        #region <<术前访视>>

        /// <summary>
        /// 查询术前访视清单
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetBeforeVisitList(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_BEFOREVISIT_GETLIST, sqlWhere));
        }

        /// <summary>
        /// 判断病人访视信息是否存在
        /// </summary>
        /// <param name="patID"></param>
        /// <returns></returns>
        public bool GetBeforeVisitCount(string patID)
        {
            int result = Convert.ToInt32(dBConn.ExecuteScalar(string.Format(SQL_BEFOREVISIT_COUNT, patID)));
            if (result == 1) return true; else return false;
        }

        /// <summary>
        /// 添加术前访视
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertBeforeVisit(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_INSERT, dictionary.Values.ToArray()));
        }

        /// <summary>
        /// 更新术前访视
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int UpdateBeforeVisit(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_BEFOREVISIT_UPDATE, dictionary.Values.ToArray()));
        }

        #endregion

        #region <<护理记录>>

        /// <summary>
        /// 查询护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetNurseRecord(string sqlWhere)
        {
            string sql = string.Format(SQL_NURSERECORD_GETLIST, sqlWhere);
            return dBConn.GetDataTable(sql);
        }

        /// <summary>
        /// 验证是否存在病人护理记录
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public bool GetNurseRecordCount(string patID)
        {
            int result = Convert.ToInt32(dBConn.ExecuteScalar(string.Format(SQL_NURSERECORD_COUNT, patID)));
            if (result == 1) return true; else return false;
        }

        /// <summary>
        /// 添加护理记录
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public int InsertNurseRecord(Dictionary<string, string> dictionary)
        {
            string sql = string.Format(SQL_NURSERECORD_INSERT, dictionary.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新护理记录
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateNurseRecord(Dictionary<string, string> dictionary)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_NURSERECORD_UPDATE, dictionary.Values.ToArray()));
        }

        #endregion

        #region <<术后随访>>

        /// <summary>
        /// 验证病人信息是否存在
        /// </summary>
        /// <param name="patID">病人ID</param>
        /// <returns></returns>
        public bool GetAfterVisitCount(string patID)
        {
            int result = Convert.ToInt32(dBConn.ExecuteScalar(string.Format(SQL_AFTERVISIT_COUNT, patID)));
            if (result == 1) return true; else return false;
        }

        /// <summary>
        /// 查询术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public DataTable GetAfterVisit(string sqlWhere)
        {
            return dBConn.GetDataTable(string.Format(SQL_AFTERVISIT_SELECT, sqlWhere));
        }

        /// <summary>
        /// 添加术后随访
        /// </summary>
        /// <param name="afterVisitList"></param>
        /// <returns></returns>
        public int InsertAfterVisit(Dictionary<string, string> afterVisitList)
        {
            string sql = string.Format(SQL_AFTERVISIT_INSERT, afterVisitList.Values.ToArray());
            return dBConn.ExecuteNonQuery(sql);
        }

        /// <summary>
        /// 更新术后随访
        /// </summary>
        /// <param name="sqlWhere"></param>
        /// <returns></returns>
        public int UpdateAfterVisit(Dictionary<string, string> afterVisitList)
        {
            return dBConn.ExecuteNonQuery(string.Format(SQL_AFTERVISIT_UPDATE, afterVisitList.Values.ToArray()));
        }

        #endregion

        #region <<职务设置>>

        /// <summary>
        /// 查看职务
        /// </summary>
        /// <returns></returns>
        public DataSet selzhiwu()
        {
            return dBConn.GetDataSet("select id,zhiwu from zhiwu");

        }

        /// <summary>
        /// 删除职务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delzhiwu(int id)
        {

            return dBConn.ExecuteNonQuery("DELETE FROM zhiwu WHERE id='" + id + "'");
        }

        /// <summary>
        /// 删除职务
        /// </summary>
        /// <param name="zhiwu"></param>
        /// <returns></returns>
        public int addzhiwu(string zhiwu)
        {

            return dBConn.ExecuteNonQuery("INSERT INTO zhiwu(zhiwu) values('" + zhiwu + "')");
        }

        #endregion

        #region <<用户管理>>

        /// <summary>
        /// 登录判断
        /// </summary>
        /// <param name="zh">用户名</param>
        /// <param name="pw">密码</param>
        /// <returns></returns>
        public bool GetUsers(string zh, string pw)
        {
            string sql = "select Count(*) from Adims_Users WITH (NOLOCK) where User_ID='" + zh + "' and User_pwd='" + pw + "'";
            int result = Convert.ToInt32(dBConn.ExecuteScalar(sql));
            if (result == 1) return true;
            else return false;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="zh"></param>
        /// <returns></returns>
        public DataSet yhxinxi(string zh)
        {
            return dBConn.GetDataSet("select User_Name,1 from Adims_Users where User_ID='" + zh + "'");
        }

        #endregion

        #endregion
    }
}
