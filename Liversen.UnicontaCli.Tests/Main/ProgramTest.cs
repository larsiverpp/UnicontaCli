using System.Collections.Immutable;
using System.Threading.Tasks;
using NodaTime;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.Main;

public static class ProgramTest
{
    [Fact]
    [Trait("Category", TestCategory.Unstable)]
    public static async Task GivenArguments_WhenExecuting_ThenExecuted()
    {
        var (companyId, credentials) = Api.UcQuery.TestData.CompanyIdCredentials();
        var arguments = ImmutableArray.Create(
            "--loginId",
            credentials.LoginId,
            "--password",
            credentials.Password,
            "--accessIdentity",
            credentials.AccessIdentity.ToString(),
            "--companyId",
            companyId.ToString(),
            "inventory-stock-status",
            "get",
            LocalDateConverter.Serialize(Today()));
        using var console = new TestConsole();

        var exitCode = await Program.Execute(arguments, console);

        exitCode.ShouldBe(0);
        var consoleOutput = console.OutputInner.ToString();
        consoleOutput.ShouldContain("InventoryNumber");
        var consoleError = console.ErrorInner.ToString();
        consoleError.ShouldBeEmpty();
    }

    static LocalDate Today() =>
        SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime.Date;
}
