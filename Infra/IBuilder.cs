namespace MRGSP.ASMS.Infra
{
    public interface IBuilder<TEntity, TInput>
    {
        TInput BuildInput(TEntity entity);
        TEntity BuildEntity(TInput input);
        TInput RebuildInput(TInput input);
    }
}