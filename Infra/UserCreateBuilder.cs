using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class UserCreateBuilder<TInput> : Builder<User, TInput> where TInput : new()
    {
        public UserCreateBuilder(IRepo<User> repo) : base(repo)
        {
        }

        protected override void MakeEntity(ref User e, TInput input)
        {
            e.InjectFrom<LookupToRoles>(input);
        }

        protected override void MakeInput(User entity, ref TInput input)
        {
            input.InjectFrom<RolesToLookup>(entity);
        }
    }
}