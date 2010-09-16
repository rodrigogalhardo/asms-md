use asms

go
drop proc getRolesByUserId
go
create proc getRolesByUserId
@id bigint
as
select r.id, r.name from roles r inner join usersroles ur on r.id = ur.roleId inner join users u on u.id = ur.userId where u.id = @id

go
drop proc assignRole
go
create proc assignRole
@userId bigint,
@roleId int
as
insert usersroles values(@userId, @roleId)

go
drop proc clearRoles
go
create proc clearRoles
@id bigint
as
delete from usersroles where userid = @id


/************* fieldset *********************/
go
drop proc getFieldsByFieldsetId
go
create proc getFieldsByFieldsetId
@id int
as
select f.* from fields f inner join fieldsetsfields ff on f.id = ff.fieldId
where ff.fieldsetId = @id 

go
drop proc getUnassignedFieldsByFieldsetId
go
create proc getUnassignedFieldsByFieldsetId
@id int
as
select * from fields f
where f.id not in (select ff.fieldId from fieldsetsfields ff where ff.fieldsetId = @id)

go
drop proc assignField
go
create proc assignField
@fieldId int,
@fieldsetId int
as
insert fieldsetsfields values(@fieldsetId, @fieldId)

go
drop proc unassignField
go
create proc unassignField
@fieldId int,
@fieldsetId int
as
delete from fieldsetsfields 
where @fieldId = fieldId and @fieldsetId = fieldsetId

go
drop proc changeFieldsetState
go
create proc changeFieldsetState
@id int,
@stateId int
as
update fieldsets set stateId = @stateId where id = @id

go
drop proc activateFieldset
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
drop proc getAssignedMeasures
go
create proc getAssignedMeasures
@measuresetId int
as
select m.* from measures m inner join measuresetsmeasures msm on m.id = msm.measureId
where msm.measuresetId = @measuresetId

go
drop proc getUnassignedMeasures
go
create proc getUnassignedMeasures
@measuresetId int
as
select m.* from measures m
where m.id not in (select msm.measureId from measuresetsmeasures msm where msm.measuresetId = @measuresetId)

go
drop proc assignMeasure
go 
create proc assignMeasure
@measureId int,
@measuresetId int
as
insert measuresetsmeasures values(@measuresetId, @measureId)

go
drop proc unassignMeasure 
go
create proc unassignMeasure 
@measureId int,
@measuresetId int
as
delete from measuresetsmeasures where measureId = @measureId and measuresetId = @measuresetId

go
drop proc changeMeasuresetState
go
create proc changeMeasuresetState
@id int,
@stateId int
as
update measuresets set stateId = @stateId where id = @id

go
drop proc activateMeasureset
go
create proc activateMeasureset
@id int
as
begin tran
update measuresets set stateId = 3 where stateId = 2
update measuresets set stateId = 2 where id = @id
commit

go
drop proc getMeasures
go
create proc getMeasures
as
select m.* from measures m inner join measuresetsmeasures msm on m.id = msm.measureId
inner join measuresets ms on msm.measuresetId = ms.id 
where ms.stateId = 2

/**************************** dossier **************************/
go
drop proc getLosers
go
create proc getLosers
@fpiId int
as
select * from competitors where
stateId < 5 and disqualified = 0 and fpiId = @fpiId

go
drop proc closeFpis
go
create proc closeFpis
@fpiId int
as
begin
declare @measuresetId int, @measureId int, @month int;

select @month = "month", @measureId = measureId, @measuresetId = measuresetId from fpis
where id = @fpiId;

update fpis set closed = 1 where 
measureId = @measureId and
measuresetId = @measuresetId and
"month" < @month

end

go 
drop proc updateToFpi
go
create proc updateToFpi
@fpiId int
as
begin
declare @measuresetId int, @measureId int, @month int;

select @month = "month", @measureId = measureId, @measuresetId = measuresetId from fpis
where id = @fpiId;

update dossiers 
set fpiId = @fpiId
where measuresetId = @measuresetId and measureId = @measureId and MONTH(createdDate) < @month and stateId = 4 and disqualified = 0
end

go
drop proc rollbackWinners
go
create proc rollbackWinners
@fpiId int
as
update dossiers 
set stateId = 4
where stateId = 5 
and fpiId = @fpiId
and disqualified = 0

go
drop proc RollbackToIndicators
go
create proc RollbackToIndicators
@fpiId int
as
begin tran

delete from coefficientvalues where dossierId in
(select id from dossiers 
where fpiId = @fpiId and
(stateId = 4 or stateId = 5)
and disqualified = 0 )

update dossiers set stateId = 3 where 
fpiId = @fpiId and
(stateId = 4 or stateId = 5) and
disqualified = 0

commit 

go
drop proc changeDossierState
go
create proc changeDossierState
@id int,
@stateId int
as
update dossiers set stateid = @stateId where id = @id


go
drop proc getDossiersForRanking
go
create proc getDossiersForRanking
@measuresetId int,
@measureId int,
@month int
as
select sum(cv.value) as value, d.id, d.amountPayed
from dossiers d left join coefficientvalues cv on d.id = cv.dossierId
where
d.measureId = @measureId
and d.measuresetId = @measuresetId
and month(d.createdDate) = @month
and d.stateId = 4
and d.disqualified = 0
group by d.Id, d.amountPayed

go
drop proc getDossiers
go
create proc getDossiers
@measuresetId int,
@measureId int,
@month int,
@stateId int = null
as
select d.* from dossiers d
where 
d.measureId = @measureId
and d.measuresetId = @measuresetId
and MONTH(d.createdDate) = @month
and (d.stateId = @stateId or @stateId = null)
and d.disqualified = 0

go
drop proc getUsedMeasureIds
go
create proc getUsedMeasureIds
@month DateTime
as
select distinct d.measureId from dossiers d
where YEAR(d.createdDate) = YEAR(@month)
and MONTH(d.createdDate) = MONTH(@month)

go
drop proc getIndicatorValues
go 
create proc getIndicatorValues
@fpiId int
as
select d.id as dossierId, iv.indicatorId, iv.value from 
dossiers d inner join indicatorvalues iv on iv.dossierId = d.id
where 
d.stateId = 3
and d.fpiId = @fpiId
and d.disqualified = 0


go
drop proc getAmountPayed
go
create proc getAmountPayed
@fpiId int
as
select coalesce(sum(AmountPayed),0) from dossiers 
where @fpiId = fpiId
and stateId = 6

go
