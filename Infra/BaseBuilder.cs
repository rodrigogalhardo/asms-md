﻿using Omu.ValueInjecter;

namespace MRGSP.ASMS.Infra
{
    public class BaseBuilder<TEntity,TInput> : IBuilder<TEntity, TInput> where TInput : new() where TEntity : new()
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