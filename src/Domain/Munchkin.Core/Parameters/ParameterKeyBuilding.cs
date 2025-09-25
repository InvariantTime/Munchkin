using Munchkin.Core.Entities;

namespace Munchkin.Core.Parameters;

public static class ParameterKey
{
    public static ParameterKey<T> Create<T>(string id, string display)
    {
        return new ParameterKey<T>(id, display);
    }

    /*public static EntityParameterKey<TEntity, TParam> Create<TEntity, TParam>(string id, string display)
        where TEntity : class, IEntity
    {
        return new EntityParameterKey<TEntity, TParam>(id, display);
    }*/
}
