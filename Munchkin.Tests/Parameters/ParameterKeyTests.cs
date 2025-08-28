using Munchkin.GameModel.Parameters;

namespace Munchkin.Tests.Parameters;

public class ParameterKeyTests
{
    [Fact]
    public void Two_Identical_ParameterKeys_Are_Equal()
    {
        IGameParameterKey parameter1 = new GameParameterKey<int>("PARAM");
        IGameParameterKey parameter2 = new GameParameterKey<int>("PARAM");

        Assert.Equal(parameter1, parameter2);
    }

    [Fact]
    public void Two_Identical_ParameterKeys_Have_Identical_HashCode()
    {
        IGameParameterKey parameter1 = new GameParameterKey<int>("PARAM");
        IGameParameterKey parameter2 = new GameParameterKey<int>("PARAM");

        Assert.Equal(parameter1.GetHashCode(), parameter2.GetHashCode());
    }

    [Fact]
    public void Two_Different_ParameterKeys_Are_Not_Equal()
    {
        IGameParameterKey parameter1 = new GameParameterKey<string>("PARAM");
        IGameParameterKey parameter2 = new GameParameterKey<int>("PARAM");

        Assert.NotEqual(parameter1, parameter2);
    }
}