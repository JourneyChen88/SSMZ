use HeYiAMIS
--当前数据库创建角色 
exec sp_addrole 'djpsRead'
--分配视图权限 GRANT SELECT  ON veiw TO [角色] 

--指定视图列表
GRANT SELECT ON  V_JC_MZ TO djpsRead
 
go
--添加只允许访问指定视图的用户: exec sp_addlogin '登录名','密码','默认数据库名' 
exec sp_addlogin 'djps','djps','HeYiAMIS'

--此处可能执行不了，要求密码强度，那就自己手工创建
go
--添加只允许访问指定视图的用户到rCRM角色中: exec sp_adduser '登录名','用户名','角色' 
exec sp_adduser 'djps','djps','djpsRead'