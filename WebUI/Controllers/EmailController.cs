using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class EmailController : FarmersCruder<Email, EmailInput>
    {
        public EmailController(IBuilder<Email, EmailInput> v, IFarmersEntityService<Email> service) : base(v, service)
        {
        }
    }
}