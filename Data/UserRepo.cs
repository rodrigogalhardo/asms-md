using System.Collections.Generic;
using System.Transactions;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;

namespace MRGSP.ASMS.Data
{
    public class UserRepo : Repo<User>, IUserRepo
    {
        public UserRepo(IConnectionFactory connFactory)
            : base(connFactory)
        {

        }

        public override int Insert(User o)
        {
            using (var scope = new TransactionScope())
            {
                var userId = DbUtil.Insert(o, Cs, new[] { "Id", "Roles" });

                foreach (var role in o.Roles)
                    DbUtil.ExecuteNonQuerySp("assignRole", new { userId, roleId = role.Id }, Cs);

                scope.Complete();
                return userId;
            }
        }

        public void ChangeRoles(User o)
        {
            using (var scope = new TransactionScope())
            {
                DbUtil.ExecuteNonQuerySp("clearRoles", new { o.Id }, Cs);

                foreach (var role in o.Roles)
                    DbUtil.ExecuteNonQuerySp("assignRole", new { userId = o.Id, roleId = role.Id }, Cs);
                scope.Complete();
            }
        }

        public IEnumerable<Role> GetRoles(long id)
        {
            return DbUtil.ExecuteReaderSp<Role>("getRolesByUserId", new { id }, Cs);
        }

        public int Count(string name)
        {
            return DbUtil.CountWhere<User>(new { name }, Cs);
        }

        public IEnumerable<Role> GetRoles()
        {
            return DbUtil.GetAll<Role>(Cs);
        }

        public int UpdatePassword(int id, string password)
        {
            return DbUtil.UpdateWhatWhere<User>(new { password }, new { id }, Cs);
        }
    }
}