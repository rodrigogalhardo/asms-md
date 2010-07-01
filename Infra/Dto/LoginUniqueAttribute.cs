using System;
using System.ComponentModel.DataAnnotations;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Infra.Dto
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class LoginUniqueAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "acest login deja exista";

        public LoginUniqueAttribute()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value)) return true;
            return !IoC.Resolve<IUserService>().Exists((string)value);
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class BankCodeUniqueAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "o banca cu asa cod deja exista";

        public BankCodeUniqueAttribute()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value)) return true;
            return !IoC.Resolve<IBankService>().Exists((string)value);
        }
    }

    public sealed class MyDate : ValidationAttribute
    {
        private const string DefaultErrorMessage = "invalid date";

        public MyDate()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }

        public override bool IsValid(object value)
        {
            DateTime d;
            return DateTime.TryParse((string) value, out d);
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class FarmerCodeUniqueAttribute : ValidationAttribute
    {
        private const string DefaultErrorMessage = "o banca cu asa cod deja exista";

        public FarmerCodeUniqueAttribute()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty((string)value)) return true;
            return !IoC.Resolve<IFarmerService>().Exists((string)value);
        }
    }

    public sealed class ReqAttribute : RequiredAttribute
    {
        public ReqAttribute()
        {
            ErrorMessage = "acest camp este obligatoriu";
        }
    }


}