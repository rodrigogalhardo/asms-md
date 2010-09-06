using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Infra.Dto
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class IndicatorFormulaCorrectAttribute : ValidationAttribute
    {
        private const string Msg = "formula nu este scrisa corect";

        public override string FormatErrorMessage(string name)
        {
            return Msg;
        }
        
        public override bool IsValid(object value)
        {
            var props = TypeDescriptor.GetProperties(value);
            var fieldsetId = (int)props.Find("FieldsetId", true).GetValue(value);
            var formula = (string)props.Find("Formula", true).GetValue(value);
            return (IoC.Resolve<IFormulaValidationService>().IsIndicatorFormulaValidForFieldset(fieldsetId, formula));
        }
    }   
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CoefficientFormulaCorrectAttribute : ValidationAttribute
    {
        private const string Msg = "formula nu este scrisa corect";

        public override string FormatErrorMessage(string name)
        {
            return Msg;
        }
        
        public override bool IsValid(object value)
        {
            var props = TypeDescriptor.GetProperties(value);
            var fieldsetId = (int)props.Find("FieldsetId", true).GetValue(value);
            var formula = (string)props.Find("Formula", true).GetValue(value);
            return (IoC.Resolve<IFormulaValidationService>().IsCoefficientFormulaValidForFieldset(fieldsetId, formula));
        }
    }

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
            return DateTime.TryParse((string)value, out d);
        }
    }

   

    public sealed class ReqAttribute : RequiredAttribute
    {
        public ReqAttribute()
        {
            ErrorMessage = "camp obligatoriu";
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class HiddenReq : ValidationAttribute
    {
        private const string DefaultErrorMessage = "dass ist nich gut";

        public HiddenReq()
            : base(DefaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return DefaultErrorMessage;
        }

        public override bool IsValid(object value)
        {
            if (value == null || (long)value == 0) return false;
            return true;
        }
    }
}