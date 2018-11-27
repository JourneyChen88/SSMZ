/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 id
      ,mzjldid
      ,CreateTime
      ,NIBPS
      ,NIBPD
      ,NIBPM
      ,RRC
      ,HR
      ,Pulse
      ,SpO2
      ,ETCO2
      ,TEMP
      ,ARTS
      ,ARTD
      ,ARTM
      ,ICO2
      ,CVP
  FROM HeYiAMIS_YC.dbo.Adims_MonitorRecord
  
  
  insert into Adims_MonitorRecord (
'560','	2014-03-19 08:22:00.000	','130','	71','	101','	19','	58	','58	','100','	0','
	-32764','	NULL','	NULL','	NULL','	NULL','	NULL') 
  
  Select * From Adims_MonitorRecord 
  Where CreateTime between '2014-03-19 08:22:00.000'AND '2014-03-19 10:22:00.000'  
  And DateDiff(mi,'2014-03-19 08:22:00.000',CreateTime) % 2=0  Order By id 
 
 update   Adims_MonitorRecord  set mzjldid=533 where mzjldid=241
 
 select MAX(id) from Adims_Mzjld