using System;

namespace Liversen.UnicontaCli;

static class TestSecrets
{
    public static string Get(string prefix, string name)
    {
        var environmentVariableName = $"{prefix}_{name}";
        var environmentVariableValue = Environment.GetEnvironmentVariable(environmentVariableName);
        if (environmentVariableValue == null)
        {
            throw new ArgumentException($"Environment variable {environmentVariableName} does not exist");
        }

        return environmentVariableValue;
    }
}
