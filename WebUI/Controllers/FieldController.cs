using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldController : Cruder<Field,FieldInput>
    {
        public FieldController(IRepo<Field> repo, IBuilder<Field, FieldInput> builder)
            : base(repo, builder)
        {
        }
    }

    public class MeasureController : Cruder<Measure, MeasureInput>
    {
        public MeasureController(IRepo<Measure> repo, IBuilder<Measure, MeasureInput> builder) : base(repo, builder)
        {
        }
    }

    public class DistrictController : Cruder<District, DistrictInput>
    {
        public DistrictController(IRepo<District> repo, IBuilder<District, DistrictInput> builder) : base(repo, builder)
        {
        }
    }

    public class PerfecterController : Cruder<Perfecter, PerfecterInput>
    {
        public PerfecterController(IRepo<Perfecter> repo, IBuilder<Perfecter, PerfecterInput> builder) : base(repo, builder)
        {
        }
    }
}