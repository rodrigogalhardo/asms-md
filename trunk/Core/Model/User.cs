using System;
using System.Collections.Generic;
using System.Linq;

namespace MRGSP.ASMS.Core.Model
{
    
    public class FieldsetDisplay : EntityWithName
    {
        public DateTime EndDate { get; set; }
        public string State { get; set; }
    }    
    
    public class MeasuresetDisplay : EntityWithName
    {
        public DateTime EndDate { get; set; }
        public string State { get; set; }
    }

    public class State : EntityWithName
    {}

    public class FieldsetState: EntityWithName{}

    public class Field : EntityWithName
    {
        public string Description { get; set; }
    }

    public class FieldValue
    {
        public int DossierId { get; set; }
        public int FieldId { get; set; }
        public decimal Value { get; set; }
    }

    public class Fieldset : EntityWithName
    {
        public DateTime EndDate { get; set; }
        public int StateId { get; set; }
    }

    public enum FieldsetStates
    {
        Registered = 1,
        HasFields,
        HasIndicators,
        HasCoefficients,
        Active,
        Inactive
    }

    public enum DossierStates
    {
        Registered = 1,
        HasIndicators = 2,
    }

    public enum States
    {
        Registered = 1,
        Active,
        Inactive
    }

    public class Indicator : EntityWithName
    {
        public int FieldsetId { get; set; }
        public string Formula { get; set; }
    }

    public class IndicatorValue
    {
        public int DossierId { get; set; }
        public int IndicatorId { get; set; }
        public decimal Value { get; set; }
    }

    public class Coefficient : EntityWithName
    {
        public int FieldsetId { get; set; }
        public string Formula { get; set; }
    }

    public class Measure : EntityWithName
    {
        public string Description { get; set; }
    }

    public class Measureset : EntityWithName
    {
        public Measureset()
        {
            StateId = 1;
        }

        public DateTime EndDate { get; set; }
        public int StateId { get; set; }
    }

    public class Entity
    {
        public int Id { get; set; }
    }

    public class EntityWithName : Entity
    {
        public string Name { get; set; }
    }

    public class District : EntityWithName
    {
        public string Code { get; set; }
    }

    public class Perfecter : EntityWithName { }

    public class Area : EntityWithName { }

    public class CompanyType : EntityWithName { }

    public class User : EntityWithName
    {
        public User()
        {
            Roles = Enumerable.Empty<Role>();
        }

        public string Password { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }

    public class Role : EntityWithName
    {

    }

    public class Bank : EntityWithName
    {
        public string Code { get; set; }
    }

    public class Dossier : Entity
    {
        public int ResponsibleId { get; set; }
        public int Number { get; set; }
        public string ActivityType { get; set; }
        public int AreaId { get; set; }
        public int DistrictId { get; set; }
        public string County { get; set; }
        public string SettlementAccount { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string FriendPhone { get; set; }
        public string Email { get; set; }
        public bool ProTraining { get; set; }
        public string Speciality { get; set; }
        public string DiplomaIssuer { get; set; }
        public bool HasContract { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public string ServiceProvider { get; set; }
        public int PerfecterId { get; set; }
        public DateTime DateReg { get; set; }

        public string FiscalCode { get; set; }
        public string NrReg { get; set; }
        public int CompanyTypeId { get; set; }

        public string BankCode { get; set; }
        public string BankName { get; set; }

        public string FarmerName { get; set; }

        public int MeasureId { get; set; }
        public int FieldsetId { get; set; }

        public int StateId { get; set; }
    }

    public class Farmer : EntityWithName
    {
        public string Code { get; set; }
        public DateTime? DateReg { get; set; }
        public string NrReg { get; set; }
        public int CompanyTypeId { get; set; }
    }

    public class Department : EntityWithName
    {

    }
}