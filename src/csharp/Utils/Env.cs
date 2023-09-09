using Microsoft.Extensions.Configuration;

public sealed class Env
{
    internal static string Var(string name)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<Env>()
            .Build();

        var value = configuration[name];
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }

        value = Environment.GetEnvironmentVariable(name);
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception($"Secret / Env var not set: {name}");
        }

        return value;
    }
}
