alter table [Adims_OTypesetting] add PidInfo nvarchar(500) null
alter table [Adims_OTypesetting] add Pv1Info nvarchar(500) null
alter table [Adims_OTypesetting] add ApplyDate datetime null
alter table [Adims_OTypesetting] add OperNo nvarchar(50) null
alter table [Adims_OTypesetting] add OsNo nvarchar(50) null



select  patzhuyuanid,Ostate,Oroom,Second,* from Adims_OTypesetting  a
inner join Adims_Mzjld b on a.PatZhuYuanID=b.patid where  Ostate=2

select PatZhuYuanID,Ostate from Adims_OTypesetting a
inner join Adims_Mzjld b on a.PatZhuYuanID=b.patid


update Adims_OTypesetting  set Ostate=2 where PatZhuYuanID
in
(select PatZhuYuanID from Adims_OTypesetting a
inner join Adims_Mzjld b on a.PatZhuYuanID=b.patid)

update Adims_OTypesetting  set Ostate=0 where Ostate is null

update Adims_OTypesetting  set Ostate=1 where Oroom is not null or Second is not null

alter table Adims_OTypesetting alter column Ostate int   not null ;

alter table Adims_OTypesetting add default (0) for Ostate with values