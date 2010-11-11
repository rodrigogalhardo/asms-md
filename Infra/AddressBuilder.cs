using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class AddressBuilder : CreateBuilder<Address, AddressInput>
    {
        protected override Address MakeEntity(Address entity, AddressInput input)
        {
            entity.InjectFrom<LookupToInt>(input);
            return entity;
        }

        protected override AddressInput MakeInput(AddressInput input, Address entity)
        {
            input.InjectFrom<IdToLookup<District>>(entity);
            return input;
        }
    }
}