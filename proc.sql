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
drop proc sealFpis
go
--seals fpis from same measure&month&msset before @this one
create proc sealFpis
@fpiId int
as
begin
declare @measuresetId int, @measureId int, @month int;

select @month = "month", @measureId = measureId, @measuresetId = measuresetId from fpis
where id = @fpiId
update fpis set "state" = 2 
where 
measureId = @measureId and
measuresetId = @measuresetId and
"month" < @month and 
"state" = 1
end

go 
drop proc updateToFpi
go
--move dossiers (state = has_coefficients) from same month&measure&msset to @this one
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
--get amount payed for authorized dossiers and agreements for @this fpi
create proc getAmountPayed
@fpiId int
as
select coalesce(SUM(r.Amount),0) from
(select AmountPayed as Amount from dossiers where @fpiId = fpiId and stateId = 6
union
select case when a.Amount > 0 then a.Amount else 0 end as Amount from agreements a where @fpiId = a.fpiId) r

go
drop proc crossDistricMeasure --'01/11/2010', 1
go
create proc crossDistricMeasure  
@date Date,
@measuresetId int
as
select case when d.id is null then 0 else 1 end as value,
dis.name district, m.name measure
from districts dis 
cross join measures m
left outer join dossiers d on d.districtid = dis.id and d.measureId = m.id and d.createdDate <= @date
where m.id in (select msm.measureId from measuresetsmeasures msm where msm.measuresetId = @measuresetId)

go
drop proc crossDistricMeasureAmountPayed --'01/11/2010', 1
go
create proc crossDistricMeasureAmountPayed  
@date Date,
@measuresetId int
as
select case when d.id is null then 0 else d.amountPayed end as value,
dis.name district, m.name measure
from districts dis 
cross join measures m
left outer join dossiers d on d.districtid = dis.id and d.measureId = m.id and d.createdDate <= @date and d.stateId = 6
where m.id in (select msm.measureId from measuresetsmeasures msm where msm.measuresetId = @measuresetId)
union 
select a.amount as amountPayed, dis.name district, m.name measure from agreements a
inner join contracts c on a.contractId = c.id 
inner join dossiers d on c.dossierId = d.id
inner join districts dis on d.districtId = dis.id
inner join measures m on m.id = d.id
where m.id in (select msm.measureId from measuresetsmeasures msm where msm.measuresetId = @measuresetId)
and a.Date <= @date and d.stateId = 6



go
drop proc DossiersByDistrictReport
go
create proc DossiersByDistrictReport
@year int,
@districtId int
as
select m.name measure, fvi.name as farmer, l.name locality, d.amountRequested,
case when d.stateId = 6 then d.amountPayed else 0 end  amountPayed
from dossiers d inner join measures m on d.measureId = m.id
inner join farmerVersionInfos fvi on d.farmerVersionId = fvi.id
inner join localities l on l.id = d.localityId 
where @year = year(d.createdDate) and d.districtId = @districtId

go
drop proc getPreviousFpi
go
create proc getPreviousFpi
@measuresetId int,
@measureId int,
@month int
as
with xx as  (
select * from fpis where measuresetId = @measuresetId and measureId = @measureId and "MONTH" < @month
)
select * from xx  x where x."month" = (select max("month") from xx)


go
drop proc capo
go
create proc capo --null, '2010-1-1', '2010-11-9', null
@measureId int,
@startDate date,
@endDate date,
@poState int
as
select fvi.name, fvi.fiscalCode, c.id as contractNr, c.date contractDate, null as agreementId, null as agreementNr, null agreementDate, d.amountPayed amount, po.Nr poNr, po.date poDate, po.state  poState, po.id poId
from farmerVersionInfos fvi 
inner join dossiers d on d.farmerVersionId = fvi.id
inner join contracts c on c.dossierId = d.id
left join paymentOrders po on po.id = c.paymentOrderId
where (@measureId is null or d.measureId = @measureId)
and c."date" >= @startDate and c."date" <= @endDate
and (@poState is null or @poState = po."state")

union

select fvi.name, fvi.fiscalCode, c.id as contractNr, c.date contractDate, a.id agreementId, a.nr as agreementNr, a.date agreementDate, a.amount, po.Nr poNr, po.date poDate, po.state poState, po.Id poId
from farmerVersionInfos fvi 
inner join dossiers d on d.farmerVersionId = fvi.id
inner join contracts c on c.dossierId = d.id
inner join agreements a on a.contractId = c.id
left join paymentOrders po on po.id = a.paymentOrderId
where (@measureId is null or d.measureId = @measureId)
and a."date" >= @startDate and a."date" <= @endDate
and (@poState is null or @poState = po."state")

go
drop proc operInfo
go
create proc operInfo
@measuresetId int
as
select m.name + '  ' + m.description measure, c12.amountm, c12.amount, dnr.nrdos, d.sumasol, con.nrcon, con.sumacon, payed.payedNr, payed.payedAmount, wait.waitNr, wait.waitAmount
from measures m
left join (select measureId, SUM(amount) amount, SUM(amountm) amountm from fpis group by measureId) c12 on c12.measureId = m.id
left join (select measureId, sum(amountRequested) sumasol from dossiers group by measureId) d on d.measureId = m.id
left join (select measureId, count(id) nrdos from dossiers group by measureId) dnr on dnr.measureId = m.id

left join (select measureId, count(id) nrcon, sum(amount) sumacon from (
select d.measureId, c.id, d.amountPayed amount from contracts c inner join dossiers d on c.dossierId = d.id 
union 
select d.measureId, a.id, a.amount from agreements a inner join contracts c on a.contractId = c.id inner join dossiers d on d.id = c.dossierId
) x group by measureId)con on con.measureId = m.id

left join (select measureId, sum(amount) payedAmount, COUNT(id) payedNr from (
select d.measureId, po.id, a.amount from paymentOrders po inner join agreements a on a.paymentOrderId = po.id inner join contracts c on a.contractId = c.id inner join dossiers d on c.dossierId = d.id
where po.state = 3
union all
select d.measureId, po.id, d.amountPayed amount from paymentOrders po inner join contracts c on c.paymentOrderId = po.Id inner join dossiers d on d.id = c.dossierId
where po.state = 3) x group by measureId) payed on payed.measureId = m.id

left join (select measureId, sum(amount) waitAmount, COUNT(id) waitNr from (
select d.measureId, po.id, a.amount from paymentOrders po inner join agreements a on a.paymentOrderId = po.id inner join contracts c on a.contractId = c.id inner join dossiers d on c.dossierId = d.id
where po.state = 2
union all
select d.measureId, po.id, d.amountPayed amount from paymentOrders po inner join contracts c on c.paymentOrderId = po.Id inner join dossiers d on d.id = c.dossierId
where po.state = 2) x group by measureId) wait on payed.measureId = m.id

where m.id in (select mm.measureId from measuresetsmeasures mm where mm.measuresetId = @measuresetId )

