
EXEC  sp_rename 'Adims_BeforeShfs_YS' ,'Adims_AfterVisit_YS' ;
EXEC  sp_rename 'Adims_Mzzj_CJ' ,'Adims_AnesthesiaSummary' ;
EXEC  sp_rename 'Adims_SHZT_YS' ,'Adims_AfterAnalgesia' ;
EXEC  sp_rename 'Adims_SHZTGCZB_YS' ,'Adims_AfterAnalgesiaDetail' ;
EXEC  sp_rename 'Adims_OTypesetting' ,'Adims_OperSchedule' ;
EXEC  sp_rename 'Adims_SSSZR' ,'Adims_OperImplant' ;
EXEC  sp_rename 'Adims_AfterVisit_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterVisit_YS.PayTime' ,'Odate' , 'COLUMN';
EXEC  sp_rename 'Adims_BeforeVisit_HS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_BeforeVisit_YS.PayTime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_BeforeVisit_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_MZZQTYS_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterAnalgesia.ZYNumber' ,'PatId' , 'COLUMN'; 
EXEC  sp_rename 'Adims_ZTZLZQTYS_YS.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_OperImplant.ZYNumber' ,'PatId' , 'COLUMN' ;
EXEC  sp_rename 'Adims_BeforeVisit_HS.sstiem' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AnesthesiaSummary.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_MZZQTYS_YS.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_NurseRecord_HQ.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_PACU.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_SHZT_YS.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_OperImplant.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterVisit_YS.Mzjld' ,'MzjldID' , 'COLUMN'; 
EXEC  sp_rename 'Adims_AfterAnalgesiaDetail.Paytime' ,'VisitTime' , 'COLUMN' 
EXEC  sp_rename 'Adims_ZTZLZQTYS_YS.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterAnalgesia.Paytime' ,'Odate' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterAnalgesia.mzjld' ,'MzjldID' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterAnalgesiaDetail.mzjld' ,'MzjldID' , 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterVisit_YS.mzjid' ,'MzjldID', 'COLUMN' ;
EXEC  sp_rename 'Adims_AfterAnalgesiaDetail.mzjld' ,'VisitDate' , 'Odate' ;

alter  table Adims_BeforeVisit_YS add PatZhuYuanID nvarchar(100)
update Adims_BeforeVisit_YS set PatZhuYuanID=PatId
alter  table Adims_BeforeVisit_HS add PatZhuYuanID nvarchar(100)
update Adims_BeforeVisit_HS set PatZhuYuanID=PatId
alter  table Adims_Mzjld add PatZhuYuanID nvarchar(100)
update Adims_Mzjld set PatZhuYuanID=PatId
alter  table Adims_MZZQTYS_YS add PatZhuYuanID nvarchar(100)
update Adims_MZZQTYS_YS set PatZhuYuanID=PatId
alter  table Adims_AfterAnalgesia add PatZhuYuanID nvarchar(100)
update Adims_AfterAnalgesia set PatZhuYuanID=PatId
alter  table Adims_AfterVisit_YS add PatZhuYuanID nvarchar(100)
update Adims_AfterVisit_YS set PatZhuYuanID=PatId
alter  table Adims_AnesthesiaSummary add PatZhuYuanID nvarchar(100)
update Adims_AnesthesiaSummary set PatZhuYuanID=PatId
alter  table Adims_ZTZLZQTYS_YS add PatZhuYuanID nvarchar(100)
update Adims_ZTZLZQTYS_YS set PatZhuYuanID=PatId

