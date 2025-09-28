using Munchkin.Core.Entities;

namespace Munchkin.Core.Parameters;

public static class Parameter
{
    public static Parameter<T> CreateParameter<T>(ParameterKey<T> key, T value)
    {
        return new Parameter<T>(key, value);
    }
}