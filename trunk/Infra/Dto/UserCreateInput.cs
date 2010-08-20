using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MRGSP.ASMS.Core.Model;
using MvcContrib.UI.InputBuilder.Attributes;

namespace MRGSP.ASMS.Infra.Dto
{
    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class UserCreateInput
    {
        [LoginUnique]
        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        [Label("Parola dorita")]
        [PartialView("Password")]
        public string Password { get; set; }

        [Req]
        [Label("Confirmati Parola")]
        [PartialView("Password")]
        public string ConfirmPassword { get; set; }

        [Label("Roluri")]
        public object Roles { get; set; }
    }

    public class ErrorDisplay
    {
        public string Message { get; set; }
    }

    public class FpiInput
    {
        public int MeasuresetId { get; set; }
        public int MeasureId { get; set; }
        public int Month { get; set; }

        [Req]
        public decimal Amount { get; set; }
    }

    public class DisqualifyInput
    {
        public int DossierId { get; set; }
        [Req]
        public string Reason { get; set; }
    }

    public class DropDownInput
    {
        public string Label { get; set; }
        public object Value { get; set; }
        public string Name { get; set; }
    }

    public class FarmersInput
    {
        public int FarmerId { get; set; }
    }

    public class AddressInput : FarmersInput
    {
        public object DistrictId { get; set; }
        [Req]
        public string Locality { get; set; }
        public string Zip { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
       
    }

    public class EmailInput : FarmersInput
    {
        [Req]
        public string Address { get; set; }

      
    }

    public class PhoneInput : FarmersInput
    {
        [Req]
        public string Number { get; set; }

        public PhoneType Type { get; set; }
    }

    public class BankCreateInput
    {
        [Req]
        [Label("Denumirea bancii")]
        public string Name { get; set; }

        [Req]
        [BankCodeUnique]
        [Label("Cod bancar")]
        public string Code { get; set; }
    }

    [IndicatorFormulaCorrect]
    public class IndicatorInput
    {
        public int FieldsetId { get; set; }

        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        public string Formula { get; set; }
    }

    [CoefficientFormulaCorrect]
    public class CoefficientInput
    {
        public int FieldsetId { get; set; }

        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        public string Formula { get; set; }
    }

    public class MeasuresetInput
    {
        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        [Label("Anul")]
        [Range(2009, 2100)]
        public int Year { get; set; }
    }

    public class FieldsetInput
    {
        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        [Label("Data de sfarsit")]
        public DateTime EndDate { get; set; }

    }

    public class FieldsInput
    {
        public int FieldsetId { get; set; }
        public IEnumerable<Field> Fields { get; set; }
    }

    public class PerfecterInput
    {
        [Req]
        [Label("Nume")]
        public string Name { get; set; }
    }

    public class DistrictInput
    {
        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        [Label("Abrevierea")]
        public string Code { get; set; }

    }

    public class FieldInput
    {
        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        [Label("Descrierea")]
        public string Description { get; set; }
    }

    public class FieldInputz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError { get { return !string.IsNullOrEmpty(ErrorMessage); } }
    }

    public class MeasureInput
    {
        [Req]
        [Label("Nume")]
        public string Name { get; set; }

        [Req]
        [Label("Descrierea")]
        public string Description { get; set; }

        [Label("Fara concurs")]
        public bool NoContest { get; set; }
    }

    public class LandOwnerInput
    {
        [Req]
        public string FirstName { get; set; }
        [Req]
        public string LastName { get; set; }
        [Req]
        public string FathersName { get; set; }
        [Req]
        public DateTime DateOfBirth { get; set; }
        [Req]
        public string FiscalCode { get; set; }

        public int FarmerId { get; set; }
    }

    public class OrganizationInput
    {
        [Req]
        public string Name { get; set; }
        [Req]
        public string FiscalCode { get; set; }

        public object OrganizationFormId { get; set; }
        [Req]
        public DateTime RegDate { get; set; }
        [Req]
        public string RegNr { get; set; }
        [Req]
        public string ActivityType { get; set; }

        public int FarmerId { get; set; }
    }

    public class FarmerCreateInput
    {
        [Req]
        [Label("Denumirea")]
        public string Name { get; set; }

        [Req]
        [FarmerCodeUnique]
        [Label("Cod")]
        public string Code { get; set; }

        [Req]
        [Label("Data inregistrarii")]
        [PartialView("DateTime")]
        public DateTime? DateReg { get; set; }

        [Req]
        [Label("Nr de inregistrare")]
        public string NrReg { get; set; }

        public object CompanyTypeId { get; set; }
    }

    public class UserEditInput
    {
        public long Id { get; set; }
        public object Roles { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class ChangePasswordInput
    {
        public long Id { get; set; }

        [Req]
        [Label("Parola dorita")]
        public string Password { get; set; }

        [Req]
        [Label("Confirmati parola")]
        public string ConfirmPassword { get; set; }

    }

    public class SignInInput
    {
        [Req]
        [Label("Nume utilizator")]
        public string Name { get; set; }

        [Req]
        [Label("Parola")]
        public string Password { get; set; }
    }

    public class LookupInfo
    {
        public LookupInfo()
        {
            Choose = true;
        }

        public string For { get; set; }

        public string Title { get; set; }

        public bool Choose { get; set; }

        public string Display { get; set; }
    }
}