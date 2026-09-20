using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.Api.UcQuery;

public static class InventoryNumberTest
{
    [Fact]
    public static void GivenValue_WhenConstructing_ThenConstructed()
    {
        var value = ThreadLocalRandom.NextUcAlphaOrDigitString(10);

        var sut = new InventoryNumber(value);

        sut.Value.ShouldBe(value);
        (sut with { }).ShouldBe(sut);
    }
}
