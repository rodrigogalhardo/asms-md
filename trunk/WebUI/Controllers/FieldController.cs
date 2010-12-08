using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class FieldController : Cruder<Field,FieldInput>
    {
        public FieldController(ICrudService<Field> s, IBuilder<Field, FieldInput> v) : base(s, v)
        {
        }
    }

    public class MeasureController : Cruder<Measure, MeasureInput>
    {
        public MeasureController(ICrudService<Measure> s, IBuilder<Measure, MeasureInput> v) : base(s, v)
        {
        }
    }

    public class DistrictController : Cruder<District, DistrictInput>
    {
        public DistrictController(ICrudService<District> s, IBuilder<District, DistrictInput> v) : base(s, v)
        {
        }
    }

    public class PerfecterController : Cruder<Perfecter, PerfecterInput>
    {
        public PerfecterController(ICrudService<Perfecter> s, IBuilder<Perfecter, PerfecterInput> v) : base(s, v)
        {
        }
    }

    public class LocalityController : Cruder<Locality, LocalityInput>
    {
        public LocalityController(ICrudService<Locality> s, IBuilder<Locality, LocalityInput> v) : base(s, v)
        {
        }
    }
}