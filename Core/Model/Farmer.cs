using System;

namespace MRGSP.ASMS.Core.Model
{
    public class OperInfoReport
    {
        public string Measure { get; set; }
        public decimal? Amountm { get; set; }
        public decimal? Amount { get; set; }
        public int? NrDos { get; set; }
        public decimal? SumaSol { get; set; }
        public int? NrCon { get; set; }
        public decimal? SumaCon { get; set; }
        public int? PayedNr { get; set; }
        public decimal? PayedAmount { get; set; }
        public int? WaitNr { get; set; }
        public decimal? WaitAmount { get; set; }
    }

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
        public int? PaymentOrderId { get; set; }
    }

    public class Agreement : Entity
    {
        public int ContractId { get; set; }
        public decimal Amount { get; set; }
        public byte Nr { get; set; }
        public int? FpiId { get; set; }
        public DateTime? Date { get; set; }
        public int? PaymentOrderId { get; set; }
    }

    public class PaymentOrder : Entity
    {
        public int Nr { get; set; }
        public DateTime Date { get; set; }
        public PoState State { get; set; }
    }

    /// <summary>
    /// Contract Agreement Payment Order
    /// </summary>
    public class Capo
    {
        public string Name { get; set; }
        public string FiscalCode { get; set; }
        public int ContractNr { get; set; }
        public DateTime ContractDate { get; set; }
        public byte? AgreementNr { get; set; }
        public int? AgreementId { get; set; }
        public DateTime? AgreementDate { get; set; }
        public decimal Amount { get; set; }
        public int? PoNr { get; set; }
        public DateTime? PoDate { get; set; }
        public PoState? PoState { get; set; }
        public int? PoId { get; set; }
    }
}