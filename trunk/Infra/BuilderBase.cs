using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class BuilderBase<TEntity,TInput> : IBuilder<TEntity, TInput> where TInput : new() where TEntity : new()
    {
        public TInput BuildInput(TEntity entity)
        {
            var input = new TInput();
            input.InjectFrom(entity);
            return MakeInput(input, entity);
        }

        public TEntity BuilEntity(TInput input)
        {
            var entity = new TEntity();
            entity.InjectFrom(input);
            return MakeEntity(entity, input);
        }
       
        public TInput RebuildInput(TInput input)
        {
            return BuildInput(BuilEntity(input));
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