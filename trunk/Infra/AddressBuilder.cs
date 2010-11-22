using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.Infra
{
    public class AddressBuilder : Builder<Address, AddressInput>
    {
        private ILocalityService s;
        public AddressBuilder(IRepo<Address> repo, ILocalityService s) : base(repo)
        {
            this.s = s;
        }

        protected override void MakeEntity(ref Address e, AddressInput input)
        {
            e.LocalityId = s.Resolve(input.LocalityId, input.Locality);

            
        }

        protected override void MakeInput(Address e, ref AddressInput input)
        {
            input.LocalityId = e.LocalityId;

            if (!input.LocalityId.HasValue) return;
            var o = s.Get(input.LocalityId.Value);
            if (o != null) input.Locality = o.Name;
            
        }
    }
}