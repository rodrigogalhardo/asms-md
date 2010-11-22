using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldController : Cruder<Field,FieldInput>
    {
        public FieldController(IRepo<Field> repo, IBuilder<Field, FieldInput> v) : base(repo, v)
        {
        }
    }

    public class MeasureController : Cruder<Measure, MeasureInput>
    {
        public MeasureController(IRepo<Measure> repo, IBuilder<Measure, MeasureInput> v) : base(repo, v)
        {
        }
    }

    public class DistrictController : Cruder<District, DistrictInput>
    {
        public DistrictController(IRepo<District> repo, IBuilder<District, DistrictInput> v) : base(repo, v)
        {
        }
    }

    public class PerfecterController : Cruder<Perfecter, PerfecterInput>
    {
        public PerfecterController(IRepo<Perfecter> repo, IBuilder<Perfecter, PerfecterInput> v) : base(repo, v)
        {
        }
    }

    public class LocalityController : Cruder<Locality, LocalityInput>
    {
        public LocalityController(IRepo<Locality> repo, IBuilder<Locality, LocalityInput> v) : base(repo, v)
        {
        }
    }
}