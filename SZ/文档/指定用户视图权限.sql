use HeYiAMIS
--��ǰ���ݿⴴ����ɫ 
exec sp_addrole 'djpsRead'
--������ͼȨ�� GRANT SELECT  ON veiw TO [��ɫ] 

--ָ����ͼ�б�
GRANT SELECT ON  V_JC_MZ TO djpsRead
 
go
--���ֻ�������ָ����ͼ���û�: exec sp_addlogin '��¼��','����','Ĭ�����ݿ���' 
exec sp_addlogin 'djps','djps','HeYiAMIS'

--�˴�����ִ�в��ˣ�Ҫ������ǿ�ȣ��Ǿ��Լ��ֹ�����
go
--���ֻ�������ָ����ͼ���û���rCRM��ɫ��: exec sp_adduser '��¼��','�û���','��ɫ' 
exec sp_adduser 'djps','djps','djpsRead'