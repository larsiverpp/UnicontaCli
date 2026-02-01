using System;
using System.Globalization;
using NodaTime;
using Uniconta.DataModel;

namespace Liversen.UnicontaCli.Api.UcQuery;

static class TestData
{
    public const string SecretPrefix = "LIVERSEN_UNICONTACLI";

    /// <summary>
    /// Parses a string with format "companyId#accessIdentity#loginId#password",
    /// e.g. "12345#4B3751FD-79C1-4B5E-989D-A173991F8984#alice#qwerty123".
    /// </summary>
    /// <returns>Company id and credentials.</returns>
    public static (CompanyId CompanyId, UcCredentials Credentials) CompanyIdCredentials()
    {
        var value = TestSecrets.Get(SecretPrefix, "CompanyIdCredentials");
        var (companyId, value2) = Split(value);
        var (accessIdentity, value3) = Split(value2);
        var (loginId, password) = Split(value3);
        var credentials = new UcCredentials(
            LoginId: loginId,
            Password: password,
            AccessIdentity: new(accessIdentity));
        return (new(int.Parse(companyId, CultureInfo.InvariantCulture)), credentials);
    }

    public static InventoryNumber RandomInventoryNumber() =>
        new(ThreadLocalRandom.NextUcAlphaOrDigitString(20));

    public static InventoryStockStatus RandomInventoryStockStatus(
        InventoryNumber? inventoryNumber = null,
        LocalDate? date = null)
    {
        var quantity = ThreadLocalRandom.Next(100);
        var unitPrice = ((decimal)ThreadLocalRandom.Next(10000)) / 100;
        var costValue = Convert.ToDecimal(unitPrice * quantity);
        return new(
            InventoryNumber: inventoryNumber ?? RandomInventoryNumber(),
            Name: UnicontaCli.TestData.RandomName(),
            Date: date ?? UnicontaCli.TestData.RandomLocalDate(),
            Quantity: quantity,
            CostValue: costValue);
    }

    public static InventoryTransaction RandomInventoryTransaction(
        InventoryNumber? inventoryNumber = null,
        LocalDate? date = null,
        InvMovementType? movementType = null) =>
        new(
            InventoryNumber: inventoryNumber ?? RandomInventoryNumber(),
            Date: date ?? UnicontaCli.TestData.RandomLocalDate(),
            Quantity: ThreadLocalRandom.Next(100),
            MovementType: movementType ?? UnicontaCli.TestData.RandomEnumValue<InvMovementType>());

    public static (string Prefix, string Suffix) Split(string value)
    {
        var index = value.IndexOf('#', StringComparison.Ordinal);
        return (value[..index], value[(index + 1)..]);
    }
}
