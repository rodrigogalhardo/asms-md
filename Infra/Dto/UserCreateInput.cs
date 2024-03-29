﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MRGSP.ASMS.Core.Model;
using Omu.Awesome.Mvc;

namespace MRGSP.ASMS.Infra.Dto
{
    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class UserCreateInput
    {
        [LoginUnique]
        [Req]
        [DisplayName("Nume")]
        [StringLength(20)]
        public string Name { get; set; }

        [Req]
        [DisplayName("Parola dorita")]
        [UIHint("Password")]
        [StringLength(32)]
        public string Password { get; set; }

        [Req]
        [DisplayName("Confirmati Parola")]
        [UIHint("Password")]
        [StringLength(32)]
        public string ConfirmPassword { get; set; }

        [DisplayName("Roluri")]
        [UIHint("Lookup")]
        [Lookup(Multiselect = true)]
        [Req]
        public IEnumerable<int> Roles { get; set; }
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
        [DisplayName("Suma AIPA")]
        public decimal Amount { get; set; }
        [Req]
        [DisplayName("Suma Minister")]
        public decimal Amountm { get; set; }
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

    public class ChangeAmountInput
    {
        public int Id { get; set; }
        [Req]
        public decimal Amount { get; set; }
        [Req]
        public decimal Amountm { get; set; }
    }

    public class ChangeAmountPayedInput
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }

    public class CapoViewModel
    {
        public CapoSearchInput SearchForm { get; set; }
        public IEnumerable<Capo> List { get; set; }
    }

    public class CapoSearchInput
    {
        [UIHint("AjaxDropdown")]
        [AjaxDropdown(Controller = "MeasureAjaxDropdown")]
        [DisplayName("Masura")]
        public int? MeasureId { get; set; }

        [DisplayName("Data inceput")]
        public DateTime StartDate { get; set; }

        [DisplayName("Data sfarsit")]
        public DateTime EndDate { get; set; }

        [UIHint("AjaxDropdown")]
        [AjaxDropdown(Controller = "PoStatesAjaxDropdown")]
        [DisplayName("Stare")]
        public int? PoState { get; set; }
    }

    public class AddressInput : FarmersInput
    {
        [UIHint("Lookup")]
        [DisplayName("Raion")]
        [Req]
        public int? DistrictId { get; set; }

        [DisplayName("Localitate")]
        [StringLength(30)]
        [Req]
        [UIHint("Autocomplete")]
        public string Locality { get; set; }

        public int? LocalityId { get; set; }

        [DisplayName("Cod postal")]
        public string Zip { get; set; }

        [DisplayName("Strada")]
        public string Street { get; set; }

        [DisplayName("Bloc")]
        public string House { get; set; }

        [DisplayName("Apartament")]
        public string Apartment { get; set; }

    }

    public class DossiersByDistrictInput
    {
        [Req]
        [DisplayName("An")]
        public int? Year { get; set; }

        [Req]
        [UIHint("AjaxDropdown")]
        [DisplayName("Raion")]
        public int? District { get; set; }
    }

    public class EmailInput : FarmersInput
    {
        [Req]
        [StringLength(50)]
        [DisplayName("Adresa")]
        public string Address { get; set; }
    }


    public class PhoneInput : FarmersInput
    {
        [Req]
        [StringLength(15)]
        [DisplayName("Numar")]
        public string Number { get; set; }

        [UIHint("Enum")]
        [DisplayName("Tip")]
        public PhoneType Type { get; set; }
    }

    public class BankCreateInput
    {
        [Req]
        [DisplayName("Denumirea bancii")]
        public string Name { get; set; }

        [Req]
        [BankCodeUnique]
        [DisplayName("Cod bancar")]
        public string Code { get; set; }
    }

    [IndicatorFormulaCorrect]
    public class IndicatorInput
    {
        public int FieldsetId { get; set; }

        [Req]
        [DisplayName("Nume")]
        public string Name { get; set; }

        [Req]
        public string Formula { get; set; }
    }

    [CoefficientFormulaCorrect]
    public class CoefficientInput
    {
        public int FieldsetId { get; set; }

        [Req]
        [DisplayName("Nume")]
        public string Name { get; set; }

        [Req]
        public string Formula { get; set; }
    }

    public class CrossDistrictMeasureInput
    {
        public CrossDistrictMeasureInput()
        {
            Date = DateTime.Now;
        }

        public int MeasuresetId { get; set; }

        [Req]
        [DisplayName("Pana la")]
        public DateTime Date { get; set; }
    }

    public class MeasuresetInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StringLength(30)]
        public string Name { get; set; }

        [Req]
        [DisplayName("Anul")]
        [MyRange(2009, 2100)]
        public int Year { get; set; }
    }

    public class MeasuresetEditInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StringLength(30)]
        public string Name { get; set; }
    }

    public class FieldsetInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StringLength(100)]
        public string Name { get; set; }

        [Req]
        [DisplayName("Anul")]
        public int Year { get; set; }

    }

    public class FieldsetEditInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StringLength(100)]
        public string Name { get; set; }
    }

    public class FieldsInput
    {
        public int FieldsetId { get; set; }
        public IEnumerable<Field> Fields { get; set; }
    }

    public class PerfecterInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StringLength(50)]
        public string Name { get; set; }
    }

    public class LocalityInput : EntityEditInput
    {
        [Req]
        [DisplayName("Denumire")]
        [StringLength(30)]
        public string Name { get; set; }
    }

    public class DistrictInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StrLen(20)]
        public string Name { get; set; }

        [Req]
        [DisplayName("Abrevierea")]
        [StrLen(2)]
        public string Code { get; set; }

    }

    public class FieldInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        public string Name { get; set; }

        [Req]
        [DisplayName("Descrierea")]
        [UIHint("Text")]
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

    public class MeasureInput : EntityEditInput
    {
        [Req]
        [DisplayName("Nume")]
        [StringLength(30)]
        public string Name { get; set; }

        [Req]
        [DisplayName("Descrierea")]
        [StringLength(200)]
        public string Description { get; set; }

        [DisplayName("Fara concurs")]
        public bool NoContest { get; set; }
    }

    public class LandOwnerInput
    {
        [Req]
        [StringLength(20)]
        [DisplayName("Prenume")]
        public string FirstName { get; set; }

        [Req]
        [StringLength(20)]
        [DisplayName("Nume")]
        public string LastName { get; set; }

        [Req]
        [StringLength(20)]
        [DisplayName("Patronimic")]
        public string FathersName { get; set; }

        [Req]
        [DisplayName("Data nasterii")]
        public DateTime? DateOfBirth { get; set; }

        [Req]
        [StringLength(13, MinimumLength = 13)]
        [DisplayName("Cod fiscal")]
        public string FiscalCode { get; set; }

        public int FarmerId { get; set; }
    }

    public class OrganizationInput
    {
        [Req]
        [StringLength(200)]
        [DisplayName("Denumirea")]
        public string Name { get; set; }

        [Req]
        [StringLength(13, MinimumLength = 13)]
        [DisplayName("Cod fiscal")]
        public string FiscalCode { get; set; }

        [UIHint("AjaxDropdown")]
        [DisplayName("Forma organizatorica")]
        public int? OrganizationFormId { get; set; }

        [Req]
        [DisplayName("Data inregistrarii")]
        public DateTime? RegDate { get; set; }

        [Req]
        [StringLength(20)]
        [DisplayName("Numarul de inregistrare")]
        public string RegNr { get; set; }

        [Req]
        [StringLength(20)]
        [DisplayName("Tipul de activitate")]
        public string ActivityType { get; set; }

        public int FarmerId { get; set; }
    }



    public class UserEditInput : EntityEditInput
    {
        [DisplayName("Roluri")]
        [UIHint("Lookup")]
        [Lookup(Multiselect = true)]
        [Req]
        public IEnumerable<int> Roles { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class ChangePasswordInput
    {
        public int Id { get; set; }

        [Req]
        [DisplayName("Parola dorita")]
        [StringLength(32)]
        public string Password { get; set; }

        [Req]
        [DisplayName("Confirmati parola")]
        [StringLength(32)]
        public string ConfirmPassword { get; set; }

    }

    public class SignInInput
    {
        [Req]
        [DisplayName("Nume utilizator")]
        [StringLength(20)]
        public string Name { get; set; }

        [Req]
        [DisplayName("Parola")]
        [UIHint("Password")]
        [StringLength(32)]
        public string Password { get; set; }
    }

    public class EntityEditInput
    {
        public int Id { get; set; }
    }

    public class PaymentOrderCreateInput : EntityEditInput
    {
        public int Nr { get; set; }
        public DateTime Date { get; set; }
        public int? ContractId { get; set; }
        public int? AgreementId { get; set; }
    }

    public class PaymentOrderEditInput : EntityEditInput
    {
        public int Nr { get; set; }
        public DateTime Date { get; set; }

        [UIHint("AjaxDropdown")]
        [AjaxDropdown(Controller = "PoStateAjaxDropdown")]
        public int State { get; set; }
    }

    public class AgreementInput : EntityEditInput
    {
        public int? ContractId { get; set; }
        
        [DisplayName("Suma")]
        public decimal Amount { get; set; }

        [Req]
        [DisplayName("Data")]
        public DateTime? Date { get; set; }

    }

    public class ContractInput : EntityEditInput
    {
        [DisplayName("Data")]
        public DateTime? Date { get; set; }

        [Req]
        [DisplayName("Cont de decontare")]
        [StringLength(15)]
        public string Account { get; set; }

        [Req]
        [DisplayName("Cod bancar")]
        [StringLength(10)]
        public string BankCode { get; set; }

        [Req]
        [DisplayName("Denumirea bancii/filialei")]
        [StringLength(100)]
        public string BankName { get; set; }


        public int DossierId { get; set; }

        [Req]
        [DisplayName("Nr Sprijin")]
        [StringLength(10)]
        public string SupportNr { get; set; }
    }
}