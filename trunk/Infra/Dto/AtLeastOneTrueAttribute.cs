using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MRGSP.ASMS.Infra.Dto
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]  
    public sealed class AtLeastOneTrueAttribute : ValidationAttribute  
    {  
        private const string DefaultErrorMessage = "selectati macar un rol";

        public AtLeastOneTrueAttribute(params string[] props)
        {
            this.props = props;
        }

        private readonly string[] props;
   
        public override string FormatErrorMessage(string name)  
        {
            return DefaultErrorMessage;
        }  
   
        public override bool IsValid(object value)  
        {  
            var properties = TypeDescriptor.GetProperties(value);  
            return props.Any(p => (bool) properties.Find(p, true).GetValue(value));
        }  
    }
}