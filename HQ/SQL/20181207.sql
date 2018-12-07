
SELECT * FROM Adims_mzjld_Point WHERE mzjldid='5189'
SELECT * FROM Adims_MonitorRecord   WHERE mzjldid='5189'
SELECT  mzjldid, RecordTime, NIBPS, NIBPD, NIBPM, RRC, HR, Pulse, Temp, SpO2, ETCO2, CVP
FROM dbo.Adims_mzjld_Point WHERE mzjldid='5189'

INSERT INTO dbo.Adims_MonitorRecord (mzjldid, CreateTime, NIBPS, NIBPD, NIBPM, RRC, HR, Pulse, SpO2, ETCO2, TEMP, CVP)
SELECT  mzjldid, RecordTime CreateTime, NIBPS, NIBPD, NIBPM, RRC, HR, Pulse,  SpO2, ETCO2, Temp,CVP
FROM dbo.Adims_mzjld_Point WHERE mzjldid='5189'

exec sp_rename 'Adims_MonitorRecord.CreateTime', 'RecordTime'

exec sp_rename 'Adims_MonitorRecord_PACU.CreateTime', 'RecordTime'