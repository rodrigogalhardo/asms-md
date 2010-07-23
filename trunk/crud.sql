use master
go
drop database asms
go
create database asms
go
use asms
create table users
(
id int identity primary key,
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
userId int foreign key references users(id),
roleId int foreign key references roles(id),
unique(userId, roleId)
)

create table districts
(
id int identity primary key,
name nvarchar(20),
code nvarchar(2)
)

create table areas
(
id int identity primary key,
name nvarchar(20)
)

create table perfecters
(
id int identity primary key,
name nvarchar(50)
)

create table companytypes
(
id int identity primary key,
name nvarchar(50)
)
create table states
(
id int primary key,
name nvarchar(20) not null unique,
)

create table measures
(
id int identity primary key,
name nvarchar(30) not null unique,
description nvarchar(100)
)
create table measuresets
(
id int identity primary key,
name nvarchar(30) not null,
enddate date not null,
stateId int not null default(1) references states(id)
)

create table measuresetsmeasures
(
measuresetId int references measuresets(id),
measureId int references measures(id)
)

create table fields
(
id int identity primary key,
name nvarchar(100) not null,
description nvarchar(200)
)

create table fieldsetStates
(
id int primary key,
name nvarchar(16) unique not null
)

create table fieldsets
(
id int identity primary key,
name nvarchar(100) not null,
enddate Date not null,
stateId int not null references fieldsetStates(id)
)

create table fieldsetsfields
(
fieldsetId int references fieldsets(id),
fieldId int references fields(id),
unique(fieldsetId, fieldId)
)

create table dossierStates
(
id int primary key,
name nvarchar(20) not null
)

create table dossiers
(
id int identity primary key,
responsibleId int,
fiscalcode nvarchar(20),
farmerName nvarchar(200) not null,
dateReg Date,
nrReg nvarchar(20),
companyTypeId int,
number int,
activityType nvarchar(20),
areaId int,
districtId int,
county nvarchar(100),
bankcode nvarchar(20),
bankname nvarchar(200),
settlementAccount nvarchar(50),
adminFirstName nvarchar(20),
adminLastName nvarchar(20),
representativeFirstName nvarchar(20),
representativeLastName nvarchar(20),
phone nvarchar(50),
fax nvarchar(50),
mobile nvarchar(50),
friendPhone nvarchar(50),
email nvarchar(max),
proTraining bit,
speciality nvarchar(max),
diplomaIssuer nvarchar(max),
hasContract bit,
contractNumber nvarchar(max),
contractDate Date,
serviceProvider nvarchar(max),
consultantId int,
regDate Date,
measureId int references measures(id) not null,
perfecterId int references perfecters(id) not null,
fieldsetId int references fieldsets(id) not null,
stateId int references dossierStates(id) not null
)

create table fieldvalues 
(
dossierId int references dossiers(id),
fieldId int references fields(id),
value money,
unique(dossierId, fieldId)
)

create table indicators
(
id int identity primary key,
fieldsetId int references fieldsets(id),
name nvarchar(30) not null,
formula nvarchar(max) not null
)

create table indicatorvalues
(
dossierId int references dossiers(id),
indicatorId int references indicators(id),
value money not null,
unique(dossierId, indicatorId)
)

create table coefficients
(
id int identity primary key,
fieldsetId int references fieldsets(id),
name nvarchar(30) not null,
formula nvarchar(max) not null
)

create table coefficientsvalues
(
dossierId int references dossiers(id),
coefficientId int references coefficients(id),
value money not null,
unique(dossierId, coefficientId)
)


go

insert roles values(1, 'admin')
insert roles values(2, 'superuser')
insert roles values(3, 'user')

insert users(name, password) values('admin', '1')
declare @id int
set @id = @@identity;
insert usersroles values(@id, 1)
insert usersroles values(@id, 2)

go

insert dossierStates values(1, 'inregistrat')
insert dossierStates values(2, 'are_indicatori')



insert fieldsetStates values(1, 'inregistrat')
insert fieldsetStates values(2, 'are_campuri')
insert fieldsetStates values(3, 'are_indicatori')
insert fieldsetStates values(4, 'are_coeficienti')
insert fieldsetStates values(5, 'activ')
insert fieldsetStates values(6, 'dezactivat')


insert states values(1, 'inregistrat')
insert states values(2, 'activ')
insert states values(3, 'dezactivat')

insert districts(name, code) values('Drochia','DR');
insert districts(name, code) values('Ialoveni','IL');
insert districts(name, code) values('Balti','BL');

insert areas(name) values('Nord')
insert areas(name) values('Centru')
insert areas(name) values('Sud')

insert perfecters(name) values('ACSA')
insert perfecters(name) values('Solicitantul')
insert perfecters(name) values('Altele')

insert companytypes(name) values('Intreprindere Individuala')
insert companytypes(name) values('Gospodarie Taraneasca')
insert companytypes(name) values('Societate cu Raspundere Limitata')
insert companytypes(name) values('Societate pe Actiuni')

insert fields(name, description) values('locuri de munca', 'lzz')
insert fields(name, description) values('valoarea investitiilor', 'laass')
insert fields(name, description) values('venituri', 'asdsl')
insert fields(name, description) values('costul vanzarilor', 'lasd')
insert fields(name, description) values('cheltuieli', 'lasd')

insert fieldsets(name, stateId, enddate) values('setul pe anul 2010', 5, '12/31/2010')
insert fieldsetsfields(fieldId, fieldsetId) values(1,1)
insert fieldsetsfields(fieldId, fieldsetId) values(2,1)
insert fieldsetsfields(fieldId, fieldsetId) values(3,1)
insert fieldsetsfields(fieldId, fieldsetId) values(4,1)
insert fieldsetsfields(fieldId, fieldsetId) values(5,1)

insert indicators(name,formula, fieldsetId) values('ind1','c1+c2', 1) 
insert indicators(name,formula, fieldsetId) values('ind2','c2+c3', 1)
insert indicators(name,formula, fieldsetId) values('ind3','c3+c4', 1)

insert coefficients(name,formula,fieldsetId) values('coef1','i1+i2',1)
insert coefficients(name,formula,fieldsetId) values('coef2','i2+i3',1)
insert coefficients(name,formula,fieldsetId) values('coef3','i3+i4',1)

insert measures(name,description) values('mas1','m')
insert measures(name,description) values('mas2','m2')
insert measures(name,description) values('mas3','m3')

insert measuresets(name,enddate,stateId) values('setul de masuri pe anul 2010', '12/31/2010',2)
insert measuresetsmeasures(measureId, measuresetId) values(1, 1)
insert measuresetsmeasures(measureId, measuresetId) values(2, 1)
insert measuresetsmeasures(measureId, measuresetId) values(3, 1)




