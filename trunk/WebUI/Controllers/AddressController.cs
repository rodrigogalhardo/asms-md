using System.Web.Mvc;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    public class AddressController : FarmersCruder<Address, AddressInput>
    {
        private readonly IRepo<AddressInfo> addressInfoRepo;

        public AddressController(IBuilder<Address, AddressInput> v, IFarmersEntityService<Address> service, IRepo<AddressInfo> addressInfoRepo) : base(v, service)
        {
            this.addressInfoRepo = addressInfoRepo;
        }

        public override ActionResult Index(int farmerId)
        {
            return View(addressInfoRepo.GetWhere(new {farmerId}));
        }
    }
}