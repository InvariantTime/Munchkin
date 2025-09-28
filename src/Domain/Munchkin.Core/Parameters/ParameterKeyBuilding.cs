using Munchkin.Core.Entities;

namespace Munchkin.Core.Parameters;

public static class ParameterKey
{
    public static ParameterKey<T> CreateKey<T>(string id, string display)
    {
        return new ParameterKey<T>(id, display);
    }
}
