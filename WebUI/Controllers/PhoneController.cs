using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class PhoneController : FarmersCruder<Phone, PhoneInput>
    {
        public PhoneController(ICreateBuilder<Phone, PhoneInput> builder, IFarmersEntityService<Phone> service) : base(builder, service)
        {
        }
    }
}