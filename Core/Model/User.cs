using System;
using System.Collections.Generic;
using System.Linq;

namespace MRGSP.ASMS.Core.Model
{
    public class FieldsetDisplay : EntityWithName
    {
        public int Year { get; set; }
        public string State { get; set; }
    }

    public class MeasuresetDisplay : EntityWithName
    {
        public int Year { get; set; }
        public string State { get; set; }
    }

    public class State : EntityWithName
    { }

    public class FieldsetState : EntityWithName { }

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
        public int Year { get; set; }
        public int StateId { get; set; }
    }

    public enum PoState
    {
        Registered = 1,
        Waiting,
        Payed
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
        HasFieldValues,
        HasIndicators,
        HasCoefficients,
        Winner,
        Authorized,
    }

    public enum FpiState
    {
        Contest = 1,
        Agreement,
        Sealed
    }

    public enum States
    {
        Registered = 1,
        Active,
        Inactive
    }

    public enum FarmerType
    {
        LandOwner = 1,
        Organization
    }

    public enum PhoneType
    {
        Fix = 1,
        Mobil,
        Fax
    }


    public class Indicator : EntityWithName
    {
        public int FieldsetId { get; set; }
        public string Formula { get; set; }
    }

    public class CalcItem : EntityWithName
    {
        public int DossierId { get; set; }
        public decimal Value { get; set; }
    }

    public class FieldValueInfo : CalcItem
    {

    }

    public class IndicatorValueInfo : CalcItem
    {
    }

    public class CoefficientValueInfo : CalcItem
    {
    }

    public class IndicatorValue
    {
        public int DossierId { get; set; }
        public int IndicatorId { get; set; }
        public decimal Value { get; set; }
    }

    public class CoefficientValue
    {
        public int DossierId { get; set; }
        public int CoefficientId { get; set; }
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
        public bool NoContest { get; set; }
    }

    public class Measureset : EntityWithName
    {
        public Measureset()
        {
            StateId = 1;
        }

        public int Year { get; set; }
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

    public class Locality : EntityWithName { }

    public class Perfecter : EntityWithName { }

    public class Area : EntityWithName { }

    public class OrganizationForm : EntityWithName
    {
        public string Abbreviation { get; set; }
    }

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

    public class RankedDossier : Entity
    {
        public decimal Value { get; set; }
        public decimal AmountPayed { get; set; }
    }

    public class Dossier : Entity
    {
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }

        public string FriendPhone { get; set; }

        public bool ProTraining { get; set; }
        public string Speciality { get; set; }
        public string DiplomaIssuer { get; set; }
        public bool HasContract { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public string ServiceProvider { get; set; }
        public int? PerfecterId { get; set; }

        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }


        public int MeasureId { get; set; }
        public int FieldsetId { get; set; }
        public int MeasuresetId { get; set; }

        public DossierStates StateId { get; set; }

        public decimal AmountRequested { get; set; }
        public decimal AmountPayed { get; set; }
        public decimal Value { get; set; }
        public bool Disqualified { get; set; }
        public int FarmerVersionId { get; set; }
        public decimal InvestmentValue { get; set; }

        public int FpiId { get; set; }
        public int? DistrictId { get; set; }
        public int? LocalityId { get; set; }
        public string Code { get; set; }
    }

    public class DossierInfo : EntityWithName
    {
        public DateTime CreatedDate { get; set; }
        public string FiscalCode { get; set; }
        public string Measure { get; set; }
        public DossierStates StateId { get; set; }
        public bool Disqualified { get; set; }
        public int FpiId { get; set; }
    }

    public class Disqualifier : Entity
    {
        public int DossierId { get; set; }
        public string Reason { get; set; }
    }


    /// <summary>
    /// Financial Plan Item
    /// </summary>
    public class Fpi : Entity
    {
        public int MeasuresetId { get; set; }
        public int MeasureId { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public decimal Amountm { get; set; }
        public FpiState State { get; set; }
    }

    public class Department : EntityWithName
    {

    }
}