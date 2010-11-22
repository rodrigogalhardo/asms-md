using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class OrganizationBuilder : Builder<Organization, OrganizationInput>
    {
        public OrganizationBuilder(IRepo<Organization> repo) : base(repo)
        {
        }

        protected override void MakeInput(Organization entity, ref OrganizationInput input)
        {
            input.InjectFrom<IdToLookup<OrganizationForm>>(entity);
        }

        protected override void MakeEntity(ref Organization e, OrganizationInput input)
        {
            e.InjectFrom<LookupToInt>(input);
        }
    }
}