using System;
using System.CommandLine;

namespace Liversen.UnicontaCli.Main;

sealed class GlobalOptions
{
    public Option<string> LoginId { get; } = new("--loginId");

    public Option<string> Password { get; } = new("--password");

    public Option<Guid> AccessIdentity { get; } = new("--accessIdentity");

    public Option<int> CompanyId { get; } = new("--companyId");

    public void AddTo(RootCommand rootCommand)
    {
        rootCommand.Add(LoginId);
        rootCommand.Add(Password);
        rootCommand.Add(AccessIdentity);
        rootCommand.Add(CompanyId);
    }
}
