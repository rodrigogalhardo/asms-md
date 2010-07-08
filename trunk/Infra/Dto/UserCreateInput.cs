using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MvcContrib.UI.InputBuilder.Attributes;

namespace MRGSP.ASMS.Infra.Dto
{
    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class UserCreateInput
    {
        [LoginUnique]
        [Req]
        [DisplayName("Nume")]
        public string Name { get; set; }

        [Req]
        [DisplayName("Parola dorita")]
        public string Password { get; set; }

        [Req]
        [DisplayName("Confirmati Parola")]
        public string ConfirmPassword { get; set; }
        
        [DisplayName("Roluri")]
        public object Roles { get; set; }
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

    public class FarmerCreateInput
    {
        [Req]
        [DisplayName("Denumirea")]
        public string Name { get; set; }

        [Req]
        [FarmerCodeUnique]
        [DisplayName("Cod")]
        public string Code { get; set; }

        [Req]
        [DisplayName("Data inregistrarii")]
        [PartialView("DateTime")]
        public DateTime? DateReg { get; set; }

        [Req]
        [DisplayName("Nr de inregistrare")]
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
        [DisplayName("Parola dorita")]
        public string Password { get; set; }

        [Req]
        [DisplayName("Confirmati parola")]
        public string ConfirmPassword { get; set; }

    }

    public class SignInInput
    {
        [Req]
        [DisplayName("Nume utilizator")]
        public string Name { get; set; }

        [Req]
        [DisplayName("Parola")]
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