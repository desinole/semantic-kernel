using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

public sealed class PluginConfiguration
{
    private IConfigurationRoot _configRoot;
    private static PluginConfiguration? s_instance;

    private PluginConfiguration(IConfigurationRoot configRoot)
    {
        this._configRoot = configRoot;
    }

    public static void Initialize(IConfigurationRoot configRoot)
    {
        s_instance = new PluginConfiguration(configRoot);
    }

    public static OpenAIConfig OpenAI => LoadSection<OpenAIConfig>();
    public static AzureOpenAIConfig AzureOpenAI => LoadSection<AzureOpenAIConfig>();

    private static T LoadSection<T>([CallerMemberName] string? caller = null)
    {
        if (s_instance == null)
        {
            throw new InvalidOperationException(
                "TestConfiguration must be initialized with a call to Initialize(IConfigurationRoot) before accessing configuration values.");
        }

        if (string.IsNullOrEmpty(caller))
        {
            throw new ArgumentNullException(nameof(caller));
        }
        return s_instance._configRoot.GetSection(caller).Get<T>() ??
            throw new Exception("Could not load configuration section " + caller);
    }

    public class OpenAIConfig
    {
        public string ChatModelId { get; set; }
        public string EmbeddingModelId { get; set; }
        public string ApiKey { get; set; }
    }

    public class AzureOpenAIConfig
    {
        public string DeploymentName { get; set; }
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
    }

}