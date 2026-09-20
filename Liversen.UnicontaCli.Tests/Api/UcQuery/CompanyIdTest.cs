using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.Api.UcQuery;

public static class CompanyIdTest
{
    [Fact]
    public static void GivenValue_WhenConstructing_ThenConstructed()
    {
        var value = ThreadLocalRandom.Next();

        new CompanyId(value).Value.ShouldBe(value);
    }
}
