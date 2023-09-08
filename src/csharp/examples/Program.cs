using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

public static class Program
{
    public static async Task Main()
    {
        // Load configuration from environment variables or user secrets.
        LoadUserSecrets();
        Console.WriteLine("-------");
        Console.WriteLine("Summary:");
        Console.WriteLine("-------");
        await ChatExample.GetSummaryAsync();
        Console.WriteLine("------");
        Console.WriteLine("Topics:");
        Console.WriteLine("------");
        await ChatExample.GetPointsAsync();
        Console.WriteLine("------------------");
        Console.WriteLine("Fun Summary:");
        Console.WriteLine("------------------");
        await ChatExample.GetTwoSentenceSummaryAsync();
        Console.WriteLine("------------------");
        Console.WriteLine("Single Prompt Template:");
        Console.WriteLine("------------------");
        await PromptExample.SinglePromptTemplateAsync();
        Console.WriteLine("------------------");
        Console.WriteLine("Multi Prompt Template:");
        Console.WriteLine("------------------");
        await PromptExample.MultiplePromptTemplateAsync();
        await PlannerExample.SequentialPlanAsync();
        await MemoryExample.MemoryExampleAsync();
    }

    private static void LoadUserSecrets()
    {
        IConfigurationRoot configRoot = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddUserSecrets<Env>()
            .Build();
        PluginConfiguration.Initialize(configRoot);
    }
}

internal sealed class Env
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


