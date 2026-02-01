using System;

namespace Liversen.UnicontaCli.Api.UcQuery;

record UcCredentials(
    string LoginId,

    string Password,

    Guid AccessIdentity);
