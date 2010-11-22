using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class PhoneController : FarmersCruder<Phone, PhoneInput>
    {
        public PhoneController(IBuilder<Phone, PhoneInput> v, IFarmersEntityService<Phone> service) : base(v, service)
        {
        }
    }
}