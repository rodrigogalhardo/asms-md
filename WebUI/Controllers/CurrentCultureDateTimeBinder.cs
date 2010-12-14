using System;
using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class CurrentCultureDateTimeBinder : IModelBinder
    {
        private const string PARSE_ERROR = "\"{0}\" is not a valid date";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == null) return null;

            var date = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            if (String.IsNullOrEmpty(date))
                return null;

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, bindingContext.ValueProvider.GetValue(bindingContext.ModelName));

            try
            {
                return DateTime.Parse(date);
            }
            catch (Exception)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, String.Format(PARSE_ERROR, bindingContext.ModelName));
                return null;
            }
        }
    }
}