using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class OrganizationBuilder : CreateBuilder<Organization, OrganizationInput>
    {
        protected override OrganizationInput MakeInput(OrganizationInput input, Organization entity)
        {
            input.InjectFrom<IdToLookup<OrganizationForm>>(entity);
            return input;
        }

        protected override Organization MakeEntity(Organization entity, OrganizationInput input)
        {
            entity.InjectFrom<LookupToInt>(input);
            return entity;
        }
    }
}