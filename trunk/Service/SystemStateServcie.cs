using System;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Service;

namespace MRGSP.ASMS.Service
{
    public class SystemStateServcie : ISystemStateServcie
    {
        private readonly IFieldsetService fsService;
        private readonly IMeasuresetService mService;

        public SystemStateServcie(IFieldsetService fsService, IMeasuresetService mService)
        {
            this.fsService = fsService;
            this.mService = mService;
        }

        public void AssureAbilityToCreateDossier()
        {
            var fs = fsService.GetActive();
            if (fs == null) throw new AsmsEx("la moment nu exista nici un set de campuri activ");
            if(fs.EndDate < DateTime.Now.AddDays(-1)) throw new AsmsEx("termenul setului de campuri activ a expirat");

            var m = mService.GetActive();
            if (m == null) throw new AsmsEx("la moment nu exista nici un set de masuri activ");
            if (m.Year != DateTime.Now.Year) throw new AsmsEx("setului de masuri activ nu este pentru anul curent");
        }
    }
}