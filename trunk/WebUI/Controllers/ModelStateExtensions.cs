using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelError> GetErrors(this ModelStateDictionary state)
        {
            return state.Values.SelectMany(o => o.Errors);
        }
    }
}