use master
go
drop database asms
go
create database asms
go
use asms

create table users
(
id bigint identity primary key,
name nvarchar(20) unique not null,
password nvarchar(20) not null
)

create table roles
(
id int primary key,
name nvarchar(20) unique not null
)

create table usersroles
(
userId bigint foreign key references users(id),
roleId int foreign key references roles(id),
unique(userId, roleId)
)

create table farmers
(
id bigint identity primary key,
code nvarchar(20) unique,
name nvarchar(200) not null,
DateReg Date,
NrReg nvarchar(20)
)

create table cases
(
id bigint identity primary key
)

create table banks
(
id bigint identity primary key,
code nvarchar(20) unique,
name nvarchar(200)
)
go

insert roles values(1, 'admin')
insert roles values(2, 'superuser')
insert roles values(3, 'user')

insert users(name, password) values('admin', '1')
declare @id bigint
set @id = @@identity;
insert usersroles values(@id, 1)
insert usersroles values(@id, 2)

go
create proc insertUser
@name nvarchar(20),
@password nvarchar(20)
as
insert users(name, password) values(@name, @password)
select @@identity

go
create proc getUsersCount
as
select count(*) from users

go
create proc getUsersPage
@pageSize int,
@page int
as
with result as(select *, ROW_NUMBER() over(order by id) nr
        from Users
)

select  * 
from    result
where   nr  between ((@page - 1) * @pageSize + 1)
        and (@page * @pageSize) 
        
go
create proc getRolesByUserId
@id bigint
as
select r.id, r.name from roles r inner join usersroles ur on r.id = ur.roleId inner join users u on u.id = ur.userId where u.id = @id

go 
create proc getUser
@id bigint
as
select * from users where id = @id

go
create proc getUsersCountByNamePassword
@name nvarchar(20),
@password nvarchar(20)
as
select count(*) from users where name = @name and password = @password

go
create proc getUsersCountByName
@name nvarchar(20)
as
select count(*) from users where name = @name

go
create proc assignRole
@userId bigint,
@roleId int
as
insert usersroles values(@userId, @roleId)

go
create proc getRoles
as
select id, name from roles

select * from usersroles
select * from users

go
create proc updatePassword
@id bigint,
@password nvarchar(20)
as
update users set password = @password where id = @id

go
create proc clearRoles
@id bigint
as
delete from usersroles where userid = @id

go
create proc getUserByNamePass
@name nvarchar(20),
@password nvarchar(20)
as
select * from users where @name = name 
and @password COLLATE Latin1_General_CS_AS = password COLLATE Latin1_General_CS_AS

/***************** banks **************/

go
create proc insertBank
@code nvarchar(20),
@name nvarchar(200)
as
insert banks(code, name) values(@code, @name)
select @@identity

go
create proc getBanksCountByCode
@code nvarchar(20)
as
select count(*) from banks where code = @code

go
create proc getBanksCount
@code nvarchar(20) = null,
@name nvarchar(200) = null
as
select count(*) from banks
where 
(@code is null or code like '%' + @code + '%') and
(@name is null or name like '%' + @name + '%')

go
create proc getBanksPage
@pageSize int,
@page int,
@code nvarchar(20) = null,
@name nvarchar(200) = null
as
with result as(select *, ROW_NUMBER() over(order by id desc) nr
        from Banks
        where 
        (@code is null or code like '%' + @code + '%') and
		(@name is null or name like '%' + @name + '%')
)

select  * 
from    result
where   nr  between ((@page - 1) * @pageSize + 1)
        and (@page * @pageSize) 

go
create proc deleteBank
@id int
as
delete from banks where id = @id

/**************************  farmers **************************************/

go
create proc insertFarmer
@code nvarchar(20),
@name nvarchar(200),
@dateReg Date,
@nrReg nvarchar(20)
as
insert farmers(code, name, dateReg, nrReg) values(@code, @name, @dateReg, @nrReg)
select @@identity

go
create proc getFarmersCount
@code nvarchar(20) = null,
@name nvarchar(200) = null
as
select count(*) from farmers
where 
(@code is null or code like '%' + @code + '%') and
(@name is null or name like '%' + @name + '%')

go
create proc getFarmersPage
@pageSize int,
@page int,
@code nvarchar(20) = null,
@name nvarchar(200) = null
as
with result as(select *, ROW_NUMBER() over(order by id desc) nr
        from farmers
        where 
        (@code is null or code like '%' + @code + '%') and
		(@name is null or name like '%' + @name + '%')
)

select  * 
from    result
where   nr  between ((@page - 1) * @pageSize + 1)
        and (@page * @pageSize) 


select * from farmers