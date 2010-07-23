using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldController : Cruder<Field,FieldInput>
    {
        public FieldController(IUberRepo<Field> repo, IBuilder<Field, FieldInput> builder)
            : base(repo, builder)
        {
        }
    }

    public class MeasureController : Cruder<Measure, MeasureInput>
    {
        public MeasureController(IUberRepo<Measure> repo, IBuilder<Measure, MeasureInput> builder) : base(repo, builder)
        {
        }
    }

    public class DistrictController : Cruder<District, DistrictInput>
    {
        public DistrictController(IUberRepo<District> repo, IBuilder<District, DistrictInput> builder) : base(repo, builder)
        {
        }
    }

    public class PerfecterController : Cruder<Perfecter, PerfecterInput>
    {
        public PerfecterController(IUberRepo<Perfecter> repo, IBuilder<Perfecter, PerfecterInput> builder) : base(repo, builder)
        {
        }
    }
}