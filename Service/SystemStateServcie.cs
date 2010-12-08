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
            fs.IsNull().B("la moment nu exista nici un set de campuri activ");
            (fs.Year != DateTime.Now.Year).B("setul de campuri activ nu este pentru anul curent");

            var m = mService.GetActive();
            m.IsNull().B("la moment nu exista nici un set de masuri activ");
            (m.Year != DateTime.Now.Year).B("setului de masuri activ nu este pentru anul curent");
        }
    }
}