using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class UserBuilder<TInput> : Builder<User, TInput> where TInput : new()
    {
        public UserBuilder(IRepo<User> repo) : base(repo)
        {
        }

        protected override void MakeEntity(ref User e, TInput input)
        {
            e.InjectFrom<IntsToRoles>(input);
        }

        protected override void MakeInput(User entity, ref TInput input)
        {
            input.InjectFrom<RolesToInts>(entity);
        }
    }
}