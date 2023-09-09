using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;

// Load Config Values from User Secrets
IConfigurationRoot configRoot = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly)
    .AddUserSecrets<Env>()
    .Build();
PluginConfiguration.Initialize(configRoot);

// instantiate the kernel with open ai chat completion service (gpt3)
IKernel kernel = Kernel.Builder
            .WithOpenAIChatCompletionService(
                PluginConfiguration.OpenAI.ChatModelId,
                PluginConfiguration.OpenAI.ApiKey)
        .Build();

Console.WriteLine("Profession:");
var profession = Console.ReadLine();

Console.WriteLine("Expertise:");
var expertise = Console.ReadLine();

string prompt = $"Your profession is a {profession} and you are an expert in {expertise}.";

IChatCompletion chatGPT = kernel.GetService<IChatCompletion>();
var chat = (OpenAIChatHistory)chatGPT.CreateNewChat(prompt);

Console.WriteLine("What are you interested in learning about?");
var question = Console.ReadLine();

chat.AddUserMessage(question);

string reply = await chatGPT.GenerateMessageAsync(chat, new ChatRequestSettings());
chat.AddAssistantMessage(reply);

Console.WriteLine("Chat output:");
foreach (var message in chat.Messages)
{
    Console.WriteLine($"{message.Role}: {message.Content}");
}
