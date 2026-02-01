using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Shouldly;
using Uniconta.DataModel;
using Xunit;

namespace Liversen.UnicontaCli.InventoryStockStatus.Get;

public sealed class HelperTest : Test.Context
{
    public HelperTest()
        : base(Test.Services.CreateServices)
    {
    }

    Helper Sut => Resolve<Helper>();

    Api.UcQuery.TestClient UcClient => Resolve<Api.UcQuery.TestClient>();

    [Fact]
    public async Task GivenNothing_WhenGetting_ThenEmpty()
    {
        var collection = await Sut.Get(TestData.RandomLocalDate());

        collection.Items.ShouldBeEmpty();
    }

    [Fact]
    public async Task GivenSingleStatus_WhenGetting_ThenSingleItemWithFractionZero()
    {
        var valueAt = TestData.RandomLocalDate();
        var status = UcClient.AddStatus(Api.UcQuery.TestData.RandomInventoryStockStatus(date: valueAt));

        var collection = await Sut.Get(valueAt);

        collection.Items.ShouldHaveSingleItem().ShouldBe(new(
            InventoryNumber: status.InventoryNumber,
            Name: status.Name,
            Quantity: status.Quantity,
            FullValue: status.CostValue,
            LastMovement: null,
            Fraction: 0));
    }

    [Fact]
    public async Task GivenSingleStatusAndRecentTransaction_WhenGetting_ThenSingleItemWithFractionOne()
    {
        var valueAt = TestData.RandomLocalDate();
        var status = UcClient.AddStatus(Api.UcQuery.TestData.RandomInventoryStockStatus(date: valueAt));
        var transaction = UcClient.AddTransaction(Api.UcQuery.TestData.RandomInventoryTransaction(
            inventoryNumber: status.InventoryNumber,
            date: valueAt,
            movementType: RandomMovementType()));

        var collection = await Sut.Get(valueAt);

        collection.Items.ShouldHaveSingleItem().ShouldBe(new(
            InventoryNumber: status.InventoryNumber,
            Name: status.Name,
            Quantity: status.Quantity,
            FullValue: status.CostValue,
            LastMovement: transaction.Date,
            Fraction: 1));
    }

    [Fact]
    public async Task GivenSingleStatusAndRecentTransactionWithExcludedMovementType_WhenGetting_ThenSingleItemWithFractionZero()
    {
        var valueAt = TestData.RandomLocalDate();
        var status = UcClient.AddStatus(Api.UcQuery.TestData.RandomInventoryStockStatus(date: valueAt));
        UcClient.AddTransaction(Api.UcQuery.TestData.RandomInventoryTransaction(
            inventoryNumber: status.InventoryNumber,
            date: valueAt,
            movementType: InvMovementType.Opening));

        var collection = await Sut.Get(valueAt);

        collection.Items.ShouldHaveSingleItem().ShouldBe(new(
            InventoryNumber: status.InventoryNumber,
            Name: status.Name,
            Quantity: status.Quantity,
            FullValue: status.CostValue,
            LastMovement: null,
            Fraction: 0));
    }

    [Fact]
    public async Task GivenStatuses_WhenGetting_Thenordered()
    {
        var valueAt = TestData.RandomLocalDate();
        var status1 = UcClient.AddStatus(Api.UcQuery.TestData.RandomInventoryStockStatus(inventoryNumber: new("ZOO"), date: valueAt));
        var status2 = UcClient.AddStatus(Api.UcQuery.TestData.RandomInventoryStockStatus(inventoryNumber: new("FOO"), date: valueAt));

        var collection = await Sut.Get(valueAt);

        collection.Items.Select(x => x.InventoryNumber).ToImmutableArray().ShouldBe(
            [status2.InventoryNumber, status1.InventoryNumber]);
    }

    static InvMovementType RandomMovementType() =>
        TestData.RandomArrayValue(Helper.IncludedMovementTypes);
}
