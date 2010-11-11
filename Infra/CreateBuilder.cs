using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using MRGSP.ASMS.Infra.Dto;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class EditBuilder<TEntity, TInput> : IEditBuilder<TEntity, TInput> 
        where TEntity : Entity, new()
        where TInput : EntityInput, new()
    {
        private readonly IRepo<TEntity> repo;

        public EditBuilder(IRepo<TEntity> repo)
        {
            this.repo = repo;
        }

        public TInput BuildInput(TEntity entity)
        {
            var o = new TInput();
            o.InjectFrom(entity);
            return o;
        }

        public TEntity BuildEntity(TInput input)
        {
            var o = repo.Get(input.Id);
            if(o == null) throw new AsmsEx("aceasta entitate nu exista");
            o.InjectFrom(input);
            return o;
        }

        public TInput RebuildInput(TInput input)
        {
            return BuildInput(BuildEntity(input));
        }
    }

    public interface IEditBuilder<TEntity, TInput> : IBuilder<TEntity, TInput>
    {
    }

    public interface ICreateBuilder<TEntity, TInput> : IBuilder<TEntity, TInput>{}

    public class CreateBuilder<TEntity, TInput> : ICreateBuilder<TEntity, TInput>
        where TInput : new()
        where TEntity : new()
    {
        public TInput BuildInput(TEntity entity)
        {
            var input = new TInput();
            input.InjectFrom(entity);
            return MakeInput(input, entity);
        }

        public TEntity BuildEntity(TInput input)
        {
            var entity = new TEntity();
            entity.InjectFrom(input);
            return MakeEntity(entity, input);
        }
       
        public TInput RebuildInput(TInput input)
        {
            return BuildInput(BuildEntity(input));
        }

        protected virtual TInput MakeInput(TInput input, TEntity entity)
        {
            return input;
        }

        protected virtual TEntity MakeEntity(TEntity entity, TInput input)
        {
            return entity;
        }
    }
}