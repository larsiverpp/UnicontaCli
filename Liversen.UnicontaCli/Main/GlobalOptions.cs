using System;
using System.CommandLine;

namespace Liversen.UnicontaCli.Main;

sealed class GlobalOptions
{
    public const string LoginIdOptionName = "--loginId";
    public const string PasswordOptionName = "--password";
    public const string AccessidentityOptionName = "--accessIdentity";
    public const string CompanyidOptionName = "--companyId";

    public Option<string> LoginId { get; } = new(LoginIdOptionName)
    {
        Description = "Login id (user name) for the Uniconta user"
    };

    public Option<string> Password { get; } = new(PasswordOptionName)
    {
        Description = "Password for the Uniconta user"
    };

    public Option<Guid> AccessIdentity { get; } = new(AccessidentityOptionName)
    {
        Description = "Access identity (GUID) for the Uniconta user"
    };

    public Option<int> CompanyId { get; } = new(CompanyidOptionName)
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
