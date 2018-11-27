CREATE VIEW [dbo].[V_NIS_OPERATION_S]
AS
SELECT     dbo.Adims_OTypesetting.PatID AS PATIENT_ID, dbo.Adims_OTypesetting.Oname AS OPER_NAME, dbo.Adims_Mzjld.id AS OPER_ID, '' AS VISIT_ID, '' AS OPER_CODE, 
                      dbo.Adims_OTypesetting.Pattmd AS OPER_DIAGNOSIS, dbo.Adims_OTypesetting.OS + dbo.Adims_OTypesetting.OsNo AS SURGEON, dbo.Adims_OTypesetting.OA1 AS FIRSTAS, 
                      dbo.Adims_Mzjld.mzys AS ANESTHESIA, dbo.Adims_OTypesetting.Amethod AS ANESTHESIA_METHOD, dbo.Adims_Mzjld.sskssj AS START_DATE_TIME, 
                      dbo.Adims_Mzjld.ssjssj AS END_DATE_TIME, dbo.Adims_Mzjld.ASA, dbo.Adims_Mzjld.CutType AS WOUND_GRADE, dbo.Adims_OTypesetting.Oroom AS OPER_ROOM, '' AS NNIS, '' AS HEAL, 
                      '' AS LOCATION, '' AS EMBED, '' AS ENDOSCOPIC,  CASE dbo.Adims_Mzjld.ASAE  WHEN 1 then '急诊' else '择期' end AS OPER_TYPE
                      , dbo.Adims_Mzjld.ChuXue AS BLOOD_OUT, '' AS BLOOD_IN
FROM         dbo.Adims_Mzjld INNER JOIN
                      dbo.Adims_OTypesetting ON dbo.Adims_Mzjld.id = dbo.Adims_OTypesetting.ID

GO


CREATE VIEW [dbo].[V_NIS_OPERATION_B]
AS
SELECT     dbo.Adims_OTypesetting.PatID AS PATIENT_ID, dbo.Adims_OTypesetting.Oname AS OPER_NAME, dbo.Adims_Mzjld.id AS OPER_ID, '' AS VISIT_ID, '' AS OPER_CODE, 
                      dbo.Adims_OTypesetting.Pattmd AS OPER_DIAGNOSIS, dbo.Adims_OTypesetting.OS + dbo.Adims_OTypesetting.OsNo AS SURGEON, dbo.Adims_OTypesetting.OA1 AS FIRSTAS, 
                      dbo.Adims_Mzjld.mzys AS ANESTHESIA, dbo.Adims_OTypesetting.Amethod AS ANESTHESIA_METHOD, dbo.Adims_Mzjld.sskssj AS START_DATE_TIME, 
                      dbo.Adims_Mzjld.ssjssj AS END_DATE_TIME, dbo.Adims_Mzjld.ASA, dbo.Adims_Mzjld.CutType AS WOUND_GRADE, dbo.Adims_OTypesetting.Oroom AS OPER_ROOM, '' AS NNIS, '' AS HEAL, 
                      '' AS LOCATION, '' AS EMBED, '' AS ENDOSCOPIC,  CASE dbo.Adims_Mzjld.ASAE  WHEN 1 then '急诊' else '择期' end AS OPER_TYPE
                      , dbo.Adims_Mzjld.ChuXue AS BLOOD_OUT, '' AS BLOOD_IN
FROM         dbo.Adims_Mzjld INNER JOIN
                      dbo.Adims_OTypesetting ON dbo.Adims_Mzjld.id = dbo.Adims_OTypesetting.ID

GO


CREATE VIEW [dbo].[V_NIS_OPERATION_P]
AS
SELECT     dbo.Adims_OTypesetting.PatID AS PATIENT_ID, dbo.Adims_OTypesetting.Oname AS OPER_NAME, dbo.Adims_Mzjld.id AS OPER_ID, '' AS VISIT_ID, '' AS OPER_CODE, 
                      dbo.Adims_OTypesetting.Pattmd AS OPER_DIAGNOSIS, dbo.Adims_OTypesetting.OS + dbo.Adims_OTypesetting.OsNo AS SURGEON, dbo.Adims_OTypesetting.OA1 AS FIRSTAS, 
                      dbo.Adims_Mzjld.mzys AS ANESTHESIA, dbo.Adims_OTypesetting.Amethod AS ANESTHESIA_METHOD, dbo.Adims_Mzjld.sskssj AS START_DATE_TIME, 
                      dbo.Adims_Mzjld.ssjssj AS END_DATE_TIME, dbo.Adims_Mzjld.ASA, dbo.Adims_Mzjld.CutType AS WOUND_GRADE, dbo.Adims_OTypesetting.Oroom AS OPER_ROOM, '' AS NNIS, '' AS HEAL, 
                      '' AS LOCATION, '' AS EMBED, '' AS ENDOSCOPIC,  CASE dbo.Adims_Mzjld.ASAE  WHEN 1 then '急诊' else '择期' end AS OPER_TYPE
                      , dbo.Adims_Mzjld.ChuXue AS BLOOD_OUT, '' AS BLOOD_IN,  case  dbo.Adims_OTypesetting.Ostate when 1 then '手术未做' when 2 then '手术已完成' end as OPER_STATUS
FROM         dbo.Adims_Mzjld INNER JOIN
                      dbo.Adims_OTypesetting ON dbo.Adims_Mzjld.id = dbo.Adims_OTypesetting.ID

GO


CREATE VIEW [dbo].[V_NIS_OPERATION_EVENT]
AS

SELECT id AS EVENTS_ID ,name as EVENTS_NAME, time as EVENTS_TIME,'' as EVENTS_TYPE
,'' as ADMINISTRATION
FROM Adims_Szsj

GO