use asms
go

create proc insertUser
@name nvarchar(20),
@password nvarchar(20)
as
insert users(name, password) values(@name, @password)
select @@identity


go
create proc getRolesByUserId
@id bigint
as
select r.id, r.name from roles r inner join usersroles ur on r.id = ur.roleId inner join users u on u.id = ur.userId where u.id = @id


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


/************* fieldset *********************/
go
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

go
create proc activateFieldset
@id int
as
begin tran
update fieldsets set stateId = 6 where stateId = 5
update fieldsets set stateId = 5 where id = @id
commit

/********************************  measureset  ******************************/
go
create proc getAssignedMeasures
@measuresetId int
as
select m.* from measures m inner join measuresetsmeasures msm on m.id = msm.measureId
where msm.measuresetId = @measuresetId

go
create proc getUnassignedMeasures
@measuresetId int
as
select m.* from measures m
where m.id not in (select msm.measureId from measuresetsmeasures msm where msm.measuresetId = @measuresetId)

go 
create proc assignMeasure
@measureId int,
@measuresetId int
as
insert measuresetsmeasures values(@measuresetId, @measureId)

go
create proc unassignMeasure 
@measureId int,
@measuresetId int
as
delete from measuresetsmeasures where measureId = @measureId and measuresetId = @measuresetId

go
create proc changeMeasuresetState
@id int,
@stateId int
as
update measuresets set stateId = @stateId where id = @id

go
create proc activateMeasureset
@id int
as
begin tran
update measuresets set stateId = 3 where stateId = 2
update measuresets set stateId = 2 where id = @id
commit

go
create proc getMeasures
as
select m.* from measures m inner join measuresetsmeasures msm on m.id = msm.measureId
inner join measuresets ms on msm.measuresetId = ms.id 
where ms.stateId = 2

/**************************** dossier **************************/

go
create proc changeDossierState
@id int,
@stateId int
as
update dossiers set stateid = @stateId where id = @id

go
create proc getDossiers
@measureId int,
@month DateTime
as
select d.* from measures m
inner join dossiers d on d.measureId = m.id
where 
m.id = @measureId
and year(d.dateReg) = year(@month)
and MONTH(d.dateReg) = MONTH(@month)
and d.stateId = 2
and d.disqualified = 0

go

create proc getUsedMeasureIds
@month DateTime
as
select distinct d.measureId from dossiers d
where YEAR(d.dateReg) = YEAR(@month)
and MONTH(d.dateReg) = MONTH(@month)

go 
create proc getIndicatorValues
@measureId int,
@month DateTime
as
select d.id as dossierId, iv.indicatorId, iv.value from measures m
inner join dossiers d on d.measureId = m.id
inner join indicatorvalues iv on iv.dossierId = d.id
where 
m.id = @measureId
and year(d.dateReg) = year(@month)
and MONTH(d.dateReg) = MONTH(@month)
and d.stateId = 2

select * from indicatorvalues
select * from fieldValues


go
use master