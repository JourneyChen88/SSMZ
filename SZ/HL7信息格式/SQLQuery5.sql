/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [id]
      ,[patid]
      ,[Otime]
      ,[jcsjjg]
      ,[Ssname]
      ,[SsAge]
      ,[SsSex]
      ,[Height]
      ,[Weight]
      ,[sqyy]
      ,[tsbq]
      ,[sqzd]
      ,[nssss]
      ,[jhxm]
      ,[mzxg]
      ,[xuexing]
      ,[tiwen]
      ,[ASA]
      ,[ASAE]
      ,[isJizhen]
      ,[SQJinshi]
      ,[QiekouType]
      ,[QiekouCount]
      ,[jkkssj]
      ,[jkjssj]
      ,[jkValue]
      ,[fzkssj]
      ,[fzjssj]
      ,[fzValue]
      ,[mzkssj]
      ,[mzjssj]
      ,[sskssj]
      ,[ssjssj]
      ,[CGSJ]
      ,[BGSJ]
      ,[mzydy]
      ,[tw]
      ,[mzfa]
      ,[szzd]
      ,[ssss]
      ,[ShoushuFS]
      ,[MazuiFS]
      ,[ssys]
      ,[mzys]
      ,[qxhs]
      ,[xhhs]
      ,[ChuXue]
      ,[Niaoliang]
      ,[brqx]
      ,[time]
      ,[flags]
      ,[Rsssxy]
      ,[Mb]
      ,[Hx]
      ,[Xx]
      ,[Cg]
      ,[Sssj]
      ,[Mzsj]
      ,[Nl]
      ,[sjzz]
      ,[cc1]
      ,[zg1]
      ,[cc2]
      ,[zg2]
      ,[twjc]
      ,[qita]
      ,[sumcl]
      ,[sxl]
      ,[dxJ]
      ,[jtl]
      ,[zrl]
      ,[shixueliang]
  FROM [HeYiAMIS_ZJ].[dbo].[Adims_Mzjld]
  
  update  [Adims_Mzjld] set otime='2016-08-26 08:00:00.000' where id=533