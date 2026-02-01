using System.Collections.Immutable;
using System.Globalization;
using System.Threading.Tasks;
using NodaTime;
using Shouldly;
using Uniconta.DataModel;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public sealed class HandlerTest : Test.Context
{
    static readonly LocalDate ValueAt = new(2025, 12, 31);

    static readonly ImmutableArray<Api.UcQuery.InventoryStockStatus> TestStatuses =
    [
        new(new("AB1234"), "Valve", ValueAt, 10, 12),
        new(new("AB123"), "Expensive valve", ValueAt, 5, 120)
    ];

    static readonly ImmutableArray<Api.UcQuery.InventoryTransaction> TestTransactions =
    [
        new(new("AB1234"), new(2025, 1, 1), 42, InvMovementType.Creditor),
        new(new("AB123"), new(2024, 12, 30), 37, InvMovementType.Debtor)
    ];

    public HandlerTest()
        : base(Test.Services.CreateServices)
    {
    }

    ICommandHandler<Parameters> Sut => Resolve<ICommandHandler<Parameters>>();

    Api.UcQuery.TestClient UcClient => Resolve<Api.UcQuery.TestClient>();

    [Fact]
    public async Task GivenStatusesAndTransactions_WhenExecuting_ThenScreenOutput()
    {
        AddTestData();

        await Sut.Run(new(ValueAt: ValueAt, CsvOutput: false, CultureInfo.InvariantCulture));

        var consoleLines = GetConsoleLines();
        consoleLines.ShouldBe(
        [
            "=== InventoryStockStatus 2025-12-31 ===",
            "InventoryNumber Name            Quantity FullValue LastMovement Fraction ReducedValue",
            "AB123           Expensive valve        5    120.00   2024-12-30     0.50        60.00",
            "AB1234          Valve                 10     12.00   2025-01-01     1.00        12.00",
            "=== TOTALS ===                              132.00                              72.00",
            string.Empty
        ]);
    }

    [Fact]
    public async Task GivenStatusesAndTransactions_WhenExecutingWithCsvOutput_ThenCsvOutput()
    {
        AddTestData();

        await Sut.Run(new(ValueAt: ValueAt, CsvOutput: true, CultureInfo.InvariantCulture));

        var consoleLines = GetConsoleLines();
        consoleLines.ShouldBe(
        [
            "=== InventoryStockStatus 2025-12-31 ===",
            "InventoryNumber\tName\tQuantity\tFullValue\tLastMovement\tFraction\tReducedValue",
            "AB123\tExpensive valve\t5\t120.00\t2024-12-30\t0.50\t60.00",
            "AB1234\tValve\t10\t12.00\t2025-01-01\t1.00\t12.00",
            "=== TOTALS ===\t\t\t132.00\t\t\t72.00",
            string.Empty
        ]);
    }

    void AddTestData()
    {
        foreach (var status in TestStatuses)
        {
            UcClient.AddStatus(status);
        }

        foreach (var transaction in TestTransactions)
        {
            UcClient.AddTransaction(transaction);
        }
    }

    ImmutableArray<string> GetConsoleLines() =>
        [..ConsoleOutput.ReplaceLineEndings("\n").Split('\n')];
}
