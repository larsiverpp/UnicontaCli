using System.CommandLine;
using System.Threading;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public class CommandTest : Test.Context
{
    readonly RootCommand sut;

    public CommandTest()
        : base(
            Test.Services.CreateServices,
            Test.CommandHandler<Parameters>.AddService)
    {
        sut = Test.CommandHelper.CreateRootCommand(ServiceProvider);
    }

    [Fact]
    public async Task GivenParameters_WhenInvokingWithoutOptions_ThenInvoked()
    {
        var valueAt = LocalDateConverter.Serialize(TestData.RandomLocalDate());

        await sut.Parse(["inventory-stock-status", "get", valueAt]).InvokeAsync(cancellationToken: CancellationToken.None);

        var parameters = Resolve<Test.CommandHandler<Parameters>>().Parameters;
        parameters.ShouldBe(Parameters.Create(valueAt, false, null, "da-DK"));
    }

    [Fact]
    public async Task GivenParameters_WhenInvokingWithOptions_ThenInvoked()
    {
        var valueAt = LocalDateConverter.Serialize(TestData.RandomLocalDate());

        await sut.Parse(["inventory-stock-status", "get", valueAt, "--csv", "--outputPath", "foo.txt", "--culture", "en-US"]).InvokeAsync(cancellationToken: CancellationToken.None);

        var parameters = Resolve<Test.CommandHandler<Parameters>>().Parameters;
        parameters.ShouldBe(Parameters.Create(valueAt, true, "foo.txt", "en-US"));
    }
}
