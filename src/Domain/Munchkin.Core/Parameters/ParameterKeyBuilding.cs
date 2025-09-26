using Munchkin.Core.Entities;

namespace Munchkin.Core.Parameters;

public static class ParameterKey
{
    public static ParameterKey<T> CreateKey<T>(string id, string display)
    {
        return new ParameterKey<T>(id, display);
    }

    public static EntityParameterKey<TEntity, T> CreateKey<TEntity, T>(string id, string display)
        where TEntity : IEntity
    {
        return new EntityParameterKey<TEntity, T>(id, display);
    }
}
