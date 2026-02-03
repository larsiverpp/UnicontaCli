using System;
using System.CommandLine;

namespace Liversen.UnicontaCli.Main;

sealed class GlobalOptions
{
    public Option<string> LoginId { get; } = new("--loginId")
    {
        Description = "Login id (user name) for the Uniconta user"
    };

    public Option<string> Password { get; } = new("--password")
    {
        Description = "Password for the Uniconta user"
    };

    public Option<Guid> AccessIdentity { get; } = new("--accessIdentity")
    {
        Description = "Access identity (GUID) for the Uniconta user"
    };

    public Option<int> CompanyId { get; } = new("--companyId")
    {
        Description = "Company id for the Uniconta company"
    };

    public void AddTo(RootCommand rootCommand)
    {
        rootCommand.Add(LoginId);
        rootCommand.Add(Password);
        rootCommand.Add(AccessIdentity);
        rootCommand.Add(CompanyId);
    }
}
