using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Password { get; set; }

        [Req]
        [Label("Confirmati Parola")]
        public string ConfirmPassword { get; set; }

        [Label("Roluri")]
        public object Roles { get; set; }
    }

    public class ErrorDisplay
    {
        public string Message { get; set; }
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
        [Label("Data de sfarsit")]
        public DateTime EndDate { get; set; }

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