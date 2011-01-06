using System.Web.Mvc;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Service;
using MRGSP.ASMS.Infra;
using MRGSP.ASMS.Infra.Dto;

namespace MRGSP.ASMS.WebUI.Controllers
{
    /// <summary>
    /// generic crud controller for entities where there is no difference between the edit and create view
    /// </summary>
    /// <typeparam name="TEntity">the entity</typeparam>
    /// <typeparam name="TInput"> viewmodel </typeparam>
    public class Cruder<TEntity, TInput> : Crudere<TEntity, TInput, TInput>
        where TInput : EntityEditInput, new()
        where TEntity : Entity, new()
    {
        public Cruder(ICrudService<TEntity> s, IBuilder<TEntity, TInput> v)
            : base(s, v, v)
        {
        }

        protected override string EditView
        {
            get { return "create"; }
        }
    }
}