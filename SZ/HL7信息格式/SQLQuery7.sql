Select CreateTime,NIBPS,NIBPD,Pulse,RRC,TEMP,SpO2,ETCO2,CVP,NIBPM,HR from Adims_MonitorRecord  Where mzjldid ='534'  and  CreateTime between '2016/8/28 7:50:00' AND '2016/8/30 7:50:00' And DateDiff(mi,'2016/8/28 7:50:00',CreateTime) % 5=0 order by CreateTime ASC