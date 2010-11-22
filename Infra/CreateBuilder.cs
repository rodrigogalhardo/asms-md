using System;
using MRGSP.ASMS.Core;
using MRGSP.ASMS.Core.Model;
using MRGSP.ASMS.Core.Repository;
using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class Builder<TEntity, TInput> : IBuilder<TEntity, TInput>
        where TEntity : class, new()
        where TInput : new()
    {
        private readonly IRepo<TEntity> repo;

        public Builder(IRepo<TEntity> repo)
        {
            this.repo = repo;
        }

        public TInput BuildInput(TEntity entity)
        {
            var input = new TInput();
            input.InjectFrom(entity)
                .InjectFrom<NormalToNullables>(entity);
            MakeInput(entity, ref input);
            return input;
        }

        protected virtual void MakeInput(TEntity entity, ref TInput input)
        {
        }

        public TEntity BuildEntity(TInput input, int? id)
        {
            var e = id.HasValue ? repo.Get(id.Value) : new TEntity();
            if (e == null)
                throw new AsmsEx("this entity doesn't exist anymore");

            e.InjectFrom(input)
               .InjectFrom<NullablesToNormal>(input);
            MakeEntity(ref e, input);
            return e;
        }

        protected virtual void MakeEntity(ref TEntity e, TInput input)
        {
        }

        public TInput RebuildInput(TInput input, int? id)
        {
            return BuildInput(BuildEntity(input, id));
        }
    }

    public class NormalToNullables : LoopValueInjection
    {
        protected override bool TypesMatch(Type sourceType, Type targetType)
        {
            var type = Nullable.GetUnderlyingType(targetType);
            if (type == null) return false;
            return sourceType == type;
        }
    }

    //e.g. from int? to int, bool? to bool, DateTime? to DateTime
    public class NullablesToNormal : LoopValueInjection
    {
        protected override bool TypesMatch(Type sourceType, Type targetType)
        {
            var type = Nullable.GetUnderlyingType(sourceType);
            if (type == null) return false;
            return targetType == type;
        }
    } 
}