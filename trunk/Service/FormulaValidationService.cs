using System.Text.RegularExpressions;
using ILCalc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class FormulaValidationService : IFormulaValidationService
    {
        private readonly IFieldRepo fieldRepo;
        private readonly IRepo<Indicator> iRepo;

        public FormulaValidationService(IFieldRepo fieldRepo, IRepo<Indicator> iRepo)
        {
            this.fieldRepo = fieldRepo;
            this.iRepo = iRepo;
        }

        public bool IsIndicatorFormulaValidForFieldset(int fieldsetId, string formula)
        {
            var fields = fieldRepo.GetAssigned(fieldsetId);
            var calc = new CalcContext<decimal>();

            foreach (var field in fields)
            {
                calc.Constants.Add("c" + field.Id, 1);
            }

            try
            {
                calc.Validate(formula);
                return true;
            }
            catch (SyntaxException)
            {
                return false;
            }
        }

        public bool IsCoefficientFormulaValidForFieldset(int fieldsetId, string formula)
        {
            var re = new Regex(@"suma\(i\d+\)");
            formula = formula.Replace(" ", "");
            var mc = re.Matches(formula);

            foreach (Match mt in mc)
            {
                formula = formula.Replace(mt.Value, mt.Value.Replace("suma(","").Replace(")",""));
            }

            var indicators = iRepo.GetWhere(new { fieldsetId });
            var calc = new CalcContext<decimal>();
            
            foreach (var i in indicators)
            {
                calc.Constants.Add("i" + i.Id, 1);
            }

            try
            {
                calc.Validate(formula);
                return true;
            }
            catch (SyntaxException)
            {
                return false;
            }
        }
    }
}