using System;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Data;
using NUnit.Framework;

namespace MRGSP.ASMS.Tests
{
    [TestFixture]
    public class DossierRepoTest : BaseRepoTest
    {
        readonly DossierRepo repo = new DossierRepo(new ConnectionFactory());

        [Test]
        public void Insert()
        {
            var d = new Dossier { AdminFirstName = "athene", Name = "jora", DateReg = DateTime.Now, HasContract = true };
            
            var id = repo.Insert(d);
            var dos = repo.Get(id);

            dos.AdminFirstName.IsEqualTo(d.AdminFirstName);
            dos.Name.IsEqualTo(d.Name);
            dos.HasContract.IsEqualTo(true);
            dos.DateReg.Date.IsEqualTo(DateTime.Now.Date);
        }
    }
}