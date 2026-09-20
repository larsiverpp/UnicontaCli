using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Liversen.UnicontaCli.Api.UcQuery;

public static class UcSessionTest
{
    [Fact]
    [Trait("Category", TestCategory.Unstable)]
    public static Task GivenInvalidCredentials_WhenCreating_ThenError() =>
        Should.ThrowAsync<ArgumentException>(() => UcSession.Create(
            TestData.CompanyIdCredentials().Credentials with
            {
                Password = ThreadLocalRandom.NextUcAlphaOrDigitString(10)
            }));
}
