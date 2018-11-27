select xhhs,sn1,qxhs,on1,mzys,ap1,AP2,ap3,Otime,A.patid from Adims_OTypesetting as A 
INNER JOIN Adims_Mzjld AS B ON A.PatID=B.patid 
where Otime<'2018-06-03'  and  charindex('、',qxhs)>0

select xhhs,qxhs,mzys,Otime,charindex('、',mzys) from Adims_Mzjld 
where charindex('、',mzys)>1 and Otime<'2016-06-03'

update A 
set AP1= SUBSTRING(mzys,0,charindex('、',mzys)),
AP2= SUBSTRING(mzys,charindex('、',mzys)+1,len(mzys)),
ON1=REPLACE(qxhs,'、',''),SN1=REPLACE(xhhs,'、','')
from Adims_OTypesetting A 
inner join Adims_Mzjld B on A.PatID=B.patid where Otime<'2016-06-03'


update Adims_OTypesetting  set on1=REPLACE(on1,'涙','') 

select distinct  on1 from Adims_OTypesetting


update A 
set on1= SUBSTRING(on1,0,charindex('、',on1))
from Adims_OTypesetting A where  charindex('、',on1)>0