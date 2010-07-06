using System;
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
        public long ResponsibleId { get; set; }
        public string Code { get; set; }
        public int Number { get; set; }
        public long FarmerId { get; set; }
        public string ActivityType { get; set; }
        public int Area { get; set; }
        public int District { get; set; }
        public string County { get; set; }
        public long BankId { get; set; }
        public string SettlementAccount { get; set; }
        public string AdminFirstName { get; set; }
        public string AdminLastName { get; set; }
        public string RepresentativeFirstName { get; set; }
        public string RepresentativeLastName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Mobile { get; set; }
        public string FriendPhone { get; set; }
        public string Email { get; set; }
        public bool ProTraining { get; set; }
        public string Speciality { get; set; }
        public string DiplomaIssuer { get; set; }
        public bool HasContract { get; set; }
        public string ContractNumber { get; set; }
        public DateTime ContractDate { get; set; }
        public string ServiceProvider { get; set; }
        public string BusinessPlanHelper { get; set; }
    }

    public class Farmer : EntityWithName
    {
        public string Code { get; set; }
        public DateTime? DateReg { get; set; }
        public string NrReg { get; set; }
        public string OrganizationalForm { get; set; }
    }

    public class Department : EntityWithName
    {

    }
}