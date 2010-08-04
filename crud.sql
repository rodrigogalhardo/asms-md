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
description nvarchar(200),
nocontest bit not null default(0)
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
description nvarchar(300)
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
stateId int references dossierStates(id) not null,
disqualified bit default(0) not null
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

create table coefficientvalues
(
dossierId int references dossiers(id),
coefficientId int references coefficients(id),
value money not null,
unique(dossierId, coefficientId)
)

create table fpis
(
measuresetId int references measuresets(id),
measureId int references measures(id),
"month" int not null,
amount money not null,
unique(measuresetId, measureId, "month")
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
insert dossierStates values(3, 'are_coeficienti')



insert fieldsetStates values(1, 'inregistrat')
insert fieldsetStates values(2, 'are_campuri')
insert fieldsetStates values(3, 'are_indicatori')
insert fieldsetStates values(4, 'are_coeficienti')
insert fieldsetStates values(5, 'activ')
insert fieldsetStates values(6, 'dezactivat')


insert states values(1, 'inregistrat')
insert states values(2, 'activ')
insert states values(3, 'dezactivat')

insert districts(name, code) values('Balti','BL')
insert districts(name, code) values('Briceni','BR')
insert districts(name, code) values('Donduseni','DN')
insert districts(name, code) values('Drochia','DR')
insert districts(name, code) values('Edinet','ED')
insert districts(name, code) values('Falesti','FL')
insert districts(name, code) values('Floresti','FR')
insert districts(name, code) values('Glodeni','GD')
insert districts(name, code) values('Ocnita','OC')
insert districts(name, code) values('Rascani','RS')
insert districts(name, code) values('Sangerei','SN')
insert districts(name, code) values('Soroca','BL')
insert districts(name, code) values('Anenii Noi','AN')
insert districts(name, code) values('Calarasi','cL')
insert districts(name, code) values('Criuleni','CR')
insert districts(name, code) values('Ialoveni','IL')
insert districts(name, code) values('Nisporeni','NS')
insert districts(name, code) values('Orhei','OR')
insert districts(name, code) values('Rezina','RZ')
insert districts(name, code) values('Straseni','ST')
insert districts(name, code) values('Soldanesti','SD')
insert districts(name, code) values('Telenesti','TL')
insert districts(name, code) values('Ungheni','UN')
insert districts(name, code) values('Basarabeasca','BS')
insert districts(name, code) values('Cahul','CH')
insert districts(name, code) values('Cantemir','CT')
insert districts(name, code) values('Causeni','CS')
insert districts(name, code) values('Cimislia','CM')
insert districts(name, code) values('Leova','LV')
insert districts(name, code) values('Stefan Voda','SV')
insert districts(name, code) values('Taraclia','TR')
insert districts(name, code) values('UTA Gagauzia','GE')

insert areas(name) values('Nord')
insert areas(name) values('Centru')
insert areas(name) values('Sud')

insert perfecters(name) values('ACSA')
insert perfecters(name) values('AGROINFORM')
insert perfecters(name) values('UNIAGROPROTECT')
insert perfecters(name) values('FNFM')
insert perfecters(name) values('DIRECTIE AGRICOLA RAIONALA')
insert perfecters(name) values('SOLICITANTUL')
insert perfecters(name) values('Altele')


insert companytypes(name) values('Cooperativa de productie')
insert companytypes(name) values('Cooperativa de intreprinzator')
insert companytypes(name) values('Gospodarie Taraneasca')
insert companytypes(name) values('Intreprindere Individual')
insert companytypes(name) values('Societate pe Actiuni')
insert companytypes(name) values('Societate in Comandita')
insert companytypes(name) values('Societate in Nume Colectiv')
insert companytypes(name) values('Societate cu Raspundere Limitata')

--1
insert fields(name, description) values('Locuri de munca create aditional in urma investitiei', 'Daca se creaza: 0 locuri de munca - se indica valoare "0"; de la 1 pana la 5 locuri de munca - se indica valoarea "0.2"; de la 6 pana la 10 locuri de munca - se indica valoarea "0.3"; peste 10 locuri de munca - se indica valoarea "0.5"')
--2
insert fields(name, description) values('Valoare investitiei (in MDL)', 'Cifra respectiva se selecteaza din punctul 5 al business planului "Finantarea Proiectului Investitional", si reprezinta bugetul investitiei, cu TVA. Suma data se indica in MDL')
--3
insert fields(name, description) values('Venituri din vanzari (sau vanzari nete, in MDL)', 'Suma data o gasim in tabelul "Raportul privind rezultatele financiare" la rubrica "Total pentru I an"')
--4
insert fields(name, description) values('Alte venituri operationale (in MDL)', 'Suma data o gasim in tabelul "Raportul privind rezultatele financiare" la rubrica "Total pentru I an"')
--5
insert fields(name, description) values('Costul vanzarilor (in MDL)', 'Suma data o gasim in tabelul "Raportul privind rezultatele financiare" la rubrica "Total pentru I an"')
--6
insert fields(name, description) values('Cheltuieli comerciale (in MDL)', ' Suma data o gasim in tabelul "Raportul privind rezultatele financiare" la rubrica "Total pentru I an"')
--7
insert fields(name, description) values('Cheltuieli generale si administrative (in MDL)', 'Suma data o gasim in tabelul "Raportul privind rezultatele financiare" la rubrica "Total pentru I an"')
--8
insert fields(name, description) values('Alte cheltuieli operationale (in MDL)', 'Suma data o gasim in tabelul "Raportul privind rezultatele financiare" la rubrica "Total pentru I an"')
--9
insert fields(name, description) values('Total intrari de mijloace banesti in trimestrul I, anul I (in MDL)', 'Suma o gasim in tabelul "Flux de numerar implicat in proiect" la rubrica "Trimestrul I, anul I"')
--10
insert fields(name, description) values('Total intrari de mijloace banesti in trimestrul II, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" la rubrica "Trimestrul II, anul I"')
--11
insert fields(name, description) values('Total intrari de mijloace banesti in trimestrul III, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" la rubrica "Trimestrul III, anul I"')
--12
insert fields(name, description) values('Total intrari de mijloace banesti in trimestrul IV, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" la rubrica "Trimestrul IV, anul I"')
--13
insert fields(name, description) values('Total intrari de mijloace banesti in anul II (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" la rubrica "Anul II"')
--14
insert fields(name, description) values('Total intrari de mijloace banesti in anul III (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" la rubrica "Anul III"')
--15
insert fields(name, description) values('Fluxul net pentru trimestrul I, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" (Mijloace banesti la sfarsitul perioadei de gestiune)')
--16
insert fields(name, description) values('Fluxul net pentru trimestrul II, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" (Mijloace banesti la sfarsitul perioadei de gestiune)')
--17
insert fields(name, description) values('Fluxul net pentru trimestrul III, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" (Mijloace banesti la sfarsitul perioadei de gestiune)')
--18
insert fields(name, description) values('Fluxul net pentru trimestrul IV, anul I (in MDL)', 'Suma data o gasim in tabelul "Flux de numerar implicat in proiect" (Mijloace banesti la sfarsitul perioadei de gestiune)')
--19
insert fields(name, description) values('Datorii pe termen scurt (in MDL)', 'Suma data o gasim in Bilantul contabil si include: dobanzi, plati leasing, rambursarea datoriilor')
--20
insert fields(name, description) values('Total datorii pe termen lung (in MDL)', 'Suma data o gasim in Bilantul contabil')
--21
insert fields(name, description) values('Total Active (in MDL)', 'Suma data o gasim in Bilantul Contabil')
--22
insert fields(name, description) values('Profitul net pentru anul I (in MDL)', 'Suma data o gasim in Raportul privind rezultatele financiare')
--23
insert fields(name, description) values('Profitul net pentru anul II (in MDL)', 'Suma data o gasim in Raportul privind rezultatele financiare')
--24
insert fields(name, description) values('Profitul net pentru anul III (in MDL)', 'Suma data o gasim in Raportul privind rezultatele financiare')
--25
insert fields(name, description) values('Total active materiale pe termen lung (in MDL)', 'Suma data o gasim in Bilantul contabil')


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


insert measures(name,description) values('Masura 1','Stimularea creditarii producatorilor agricoli de catre bancile comerciale')
insert measures(name,description, nocontest) values('Masura 2','Stimularea mecanismului de asigurare in agricultura',1)
insert measures(name,description) values('Masura 3','Subventionarea investitiilor pentru plantatiile multianuale')
insert measures(name,description) values('Masura 4','Subventionarea investitiilor pentru producerea legumelor pentru teren protejat')
insert measures(name,description) values('Masura 5','Subventionarea investitiilor pentru procurarea tehnicii si utilajului agricol precum si a echipamentului de irigare')
insert measures(name,description) values('Masura 6','Sustinerea promovarii si dezvoltarii agriculturii ecologice')
insert measures(name,description) values('Masura 7','Stimularea investitiilor in utilarea si renovarea tehnologica a fermelor zootehnice')
insert measures(name,description, nocontest) values('Masura 8','Stimularea procurarii animalelor de prasila si mentinerea fondului lor genetic',1)
insert measures(name,description) values('Masura 9','Stimularea investitiilor in dezvoltarea infrastructurii posrecoltare si procesare')
insert measures(name,description) values('Masura 10','Subventionarea producatorilor agricoli pentru compensarea cheltuielilor energetice la irigare')



insert measuresets(name,enddate,stateId) values('setul de masuri pe anul 2010', '12/31/2010',2)
insert measuresetsmeasures(measureId, measuresetId) values(1, 1)
insert measuresetsmeasures(measureId, measuresetId) values(2, 1)
insert measuresetsmeasures(measureId, measuresetId) values(3, 1)
insert measuresetsmeasures(measureId, measuresetId) values(4, 1)
insert measuresetsmeasures(measureId, measuresetId) values(5, 1)
insert measuresetsmeasures(measureId, measuresetId) values(6, 1)
insert measuresetsmeasures(measureId, measuresetId) values(7, 1)
insert measuresetsmeasures(measureId, measuresetId) values(8, 1)
insert measuresetsmeasures(measureId, measuresetId) values(9, 1)
insert measuresetsmeasures(measureId, measuresetId) values(10, 1)
