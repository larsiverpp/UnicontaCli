using System;

namespace Liversen.UnicontaCli.Api.UcQuery;

sealed record UcCredentials(
    string LoginId,

    string Password,

    Guid AccessIdentity);
