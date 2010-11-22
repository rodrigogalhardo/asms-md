using System.Web;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class DossierBuilder : Builder<Dossier, DossierCreateInput>
    {
        private readonly IUserService userService;
        private readonly ILocalityService localityService;

        public DossierBuilder(IRepo<Dossier> repo, IUserService userService, ILocalityService localityService) : base(repo)
        {
            this.userService = userService;
            this.localityService = localityService;
        }

        protected override void MakeInput(Dossier entity, ref DossierCreateInput input)
        {
            input.LocalityId = entity.LocalityId;

            if (!input.LocalityId.HasValue) return;
            var o = localityService.Get(input.LocalityId.Value);
            if (o != null) input.Locality = o.Name;
        }

        protected override void MakeEntity(ref Dossier e, DossierCreateInput input)
        {
            e.LocalityId = localityService.Resolve(input.LocalityId, input.Locality);
           
            var userName = HttpContext.Current.User.Identity.Name;
            var user = userService.Get(userName);

            e.InjectFrom<LookupToInt>(input);
            //TODO:entity.ResponsibleId = user.Id;
        }
    }
}