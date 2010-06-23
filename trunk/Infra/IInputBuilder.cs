namespace MRGSP.ASMS.Infra
{
    public interface IBuilder<TEntity, TInput>
    {
        TInput BuildInput(TEntity entity);
        TEntity BuilEntity(TInput input);
        TInput RebuildInput(TInput input);
    }
}