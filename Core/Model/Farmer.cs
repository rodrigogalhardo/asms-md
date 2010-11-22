using System;

namespace MRGSP.ASMS.Core.Model
{
    public class DossiersByDistrictReport
    {
        public string Measure { get; set; }
        public string Farmer { get; set; }
        public string Locality { get; set; }
        public decimal AmountRequested { get; set; }
        public decimal AmountPayed { get; set; }
    }

    public class Farmer : Entity
    {
        public FarmerType FType { get; set; }
    }

    public class Competitor : EntityWithName
    {
        public string Code { get; set; }
        public decimal AmountPayed { get; set; }
        public decimal Value { get; set; }
        public int FpiId { get; set; }
        public DossierStates StateId { get; set; }
        public bool Disqualified { get; set; }
    }

    public class FarmerInfo : EntityWithName
    {
        public string FiscalCode { get; set; }
        public FarmerType FType { get; set; }
        public int FarmerVersionId { get; set; }
    }

    public class FarmerVersionInfo : EntityWithName
    {
        public int FarmerId { get; set; }
        public string FiscalCode { get; set; }
        public FarmerType FType { get; set; }
    }

    public abstract class FarmersEntity : Entity
    {
        public int FarmerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class FarmerVersion : Entity
    {
        public int FarmerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class LandOwner : Entity
    {
        public int FarmerVersionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FathersName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string FiscalCode { get; set; }
    }

    public class LandOwnerInfo : LandOwner
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int FarmerId { get; set; }
    }

    public class Organization : Entity
    {
        public int FarmerVersionId { get; set; }
        public string Name { get; set; }
        public string FiscalCode { get; set; }
        public int OrganizationFormId { get; set; }
        public DateTime? RegDate { get; set; }
        public string RegNr { get; set; }
        public string ActivityType { get; set; }
    }

    public class OrganizationInfo : Organization
    {
        public string OrganizationForm { get; set; }
        public int FarmerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class Address : FarmersEntity
    {
        public int? DistrictId { get; set; }
        public int? LocalityId { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
    }

    public class AddressInfo : Address
    {
        public string Locality { get; set; }
        public string District { get; set; }
        public string DistrictCode { get; set; }
    }

    public class Phone : FarmersEntity
    {
        public string Number { get; set; }
        public PhoneType Type { get; set; }
    }

    public class Email : FarmersEntity
    {
        public string Address { get; set; }
    }

    public class Contract : Entity
    {
        public DateTime? Date { get; set; }
        public string Account { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public int DossierId { get; set; }
        public string SupportNr { get; set; }
    }


}