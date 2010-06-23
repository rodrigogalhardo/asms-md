using System.Collections.Generic;
using System.Linq;

namespace MRGSP.ASMS.Core.Model
{
    public class Entity
    {
        public long Id { get; set; }
    }

    public class EntityWithName : Entity
    {
        public string Name { get; set; }
    }

    public class User : EntityWithName
    {
        public User()
        {
            Roles = Enumerable.Empty<Role>();
        }

        public string Password { get; set; }
        public IEnumerable<Role> Roles { get; set; }

    }

    public class Role : EntityWithName
    {
        
    }

    public class Bank : EntityWithName
    {
        public string Code { get; set; }
    }

    public class Case : EntityWithName
    {

    }

    public class Farmer : EntityWithName
    {
        
    }

    public class Department : EntityWithName
    {
        
    }
}