use asms
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
@nrReg nvarchar(20),
@companyTypeId bigint
as
insert farmers(code, name, dateReg, nrReg, companyTypeId) values(@code, @name, @dateReg, @nrReg, @companyTypeId)
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

/***************** cases *******************************/
go
create proc insertCase
@responsibleId bigint ,
@code nvarchar(20)  ,
@number int ,
@farmerId bigint ,
@activityType nvarchar(20),
@areaId int,
@districtId int,
@county nvarchar(100),
@bankId int ,
@settlementAccount nvarchar(50),
@adminFirstName nvarchar(20),
@adminLastName nvarchar(20),
@representativeFirstName nvarchar(20),
@representativeLastName nvarchar(20),
@phone nvarchar(50),
@fax nvarchar(50),
@mobile nvarchar(50),
@friendPhone nvarchar(50),
@email nvarchar,
@proTraining bit,
@speciality nvarchar,
@diplomaIssuer nvarchar,
@hasContract bit,
@contractNumber nvarchar,
@contractDate Date,
@serviceProvider nvarchar,
@consultantId nvarchar,
@regDate Date
as
begin tran
insert cases(responsibleId,
farmerId  ,
activityType ,
areaId ,
districtId ,
county,
bankId  ,
settlementAccount ,
adminFirstName ,
adminLastName ,
representativeFirstName ,
representativeLastName ,
phone ,
fax ,
mobile ,
friendPhone ,
email ,
proTraining,
speciality ,
diplomaIssuer ,
hasContract,
contractNumber ,
contractDate,
serviceProvider ,
consultantId,
regDate )

values(
@responsibleId  ,
@farmerId  ,
@activityType ,
@areaId ,
@districtId ,
@county,
@bankId  ,
@settlementAccount ,
@adminFirstName ,
@adminLastName ,
@representativeFirstName ,
@representativeLastName ,
@phone ,
@fax ,
@mobile ,
@friendPhone ,
@email ,
@proTraining,
@speciality ,
@diplomaIssuer ,
@hasContract,
@contractNumber ,
@contractDate,
@serviceProvider ,
@consultantId,
@regDate
)	
select @@IDENTITY
commit

go
/************* fieldset *********************/
create proc getFieldsByFieldsetId
@id int
as
select f.* from fields f inner join fieldsetsfields ff on f.id = ff.fieldId
where ff.fieldsetId = @id 

go
create proc getUnassignedFieldsByFieldsetId
@id int
as
select * from fields f
where f.id not in (select ff.fieldId from fieldsetsfields ff where ff.fieldsetId = @id)

go
create proc assignField
@fieldId int,
@fieldsetId int
as
insert fieldsetsfields values(@fieldsetId, @fieldId)

go
create proc unassignField
@fieldId int,
@fieldsetId int
as
delete from fieldsetsfields 
where @fieldId = fieldId and @fieldsetId = fieldsetId

go
create proc changeFieldsetState
@id int,
@stateId int
as
update fieldsets set stateId = @stateId where id = @id

