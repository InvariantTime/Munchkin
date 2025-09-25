using Munchkin.Core.Parameters;

namespace Munchkin.Core.Tests.Parameters;

public class GenericParameterKeyTests
{
    [Fact]
    public void Keys_With_One_Ref_Is_Equal()
    {
        var common = ParameterKey.Create<int>("Param", string.Empty);

        var key1 = common;
        var key2 = common;

        Assert.Equal(key1, key2);
    }

    [Fact]
    public void Keys_With_One_Id_Is_Equal()
    {
        var key1 = ParameterKey.Create<int>("Param1", string.Empty);
        var key2 = ParameterKey.Create<int>("Param1", string.Empty);

        Assert.Equal(key1, key2);
    }

    [Fact]
    public void Keys_With_Seperate_Ids_Is_NotEqual()
    {
        var key1 = ParameterKey.Create<int>("Param1", string.Empty);
        var key2 = ParameterKey.Create<int>("Param2", string.Empty);

        Assert.NotEqual(key1, key2);
    }

    [Fact]
    public void Keys_With_Seperate_DisplayName_And_One_Ids_Is_Equal()
    {
        var key1 = ParameterKey.Create<int>("Param1", "Display 1");
        var key2 = ParameterKey.Create<int>("Param1", "Display 2");

        Assert.Equal(key1, key2);
    }

    [Fact]
    public void Keys_With_One_Id_Have_One_HashCode()
    {
        var key1 = ParameterKey.Create<int>("Param1", "Display 1");
        var key2 = ParameterKey.Create<int>("Param1", "Display 2");

        Assert.Equal(key1.GetHashCode(), key2.GetHashCode());
    }

    [Fact]
    public void Keys_With_Seperate_Ids_Have_Seperate_HashCodes()
    {
        var key1 = ParameterKey.Create<int>("Param1", "Display 1");
        var key2 = ParameterKey.Create<int>("Param2", "Display 2");

        Assert.NotEqual(key1.GetHashCode(), key2.GetHashCode());
    }

    [Fact]
    public void Keys_Dictionary_Test()
    {
        IParameterKey key1 = ParameterKey.Create<int>("Param1", "Display 1");
        IParameterKey key2 = ParameterKey.Create<int>("Param1", "Display 2");

        int value = 12345;

        Dictionary<IParameterKey, int> values = new();
        values.Add(key1, value);

        Assert.Equal(value, values[key2]);
    }

    [Fact]
    public void Keys_Upcasting_Equal_Test()
    {
        IParameterKey key1 = ParameterKey.Create<int>("Param1", "Display 1");
        IParameterKey key2 = ParameterKey.Create<int>("Param1", "Display 2");

        Assert.Equal(key1, key2);
    }

    [Fact]
    public void Keys_Upcasting_NotEqual_Test()
    {
        IParameterKey key1 = ParameterKey.Create<int>("Param1", "Display 1");
        IParameterKey key2 = ParameterKey.Create<int>("Param2", "Display 2");

        Assert.NotEqual(key1, key2);
    }
}