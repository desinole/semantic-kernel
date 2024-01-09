using System.Reflection;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// instantiate the config builder and load the configuration from user secrets
var config = new ConfigurationBuilder()
//    .AddEnvironmentVariables()
    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly)
    .AddUserSecrets<Env>()
    .Build();
// Initialize the plugin configuration class to load the config values from user secrets
PluginConfiguration.Initialize(config);
// Instantiate the OpenAI client with the API key from plugin configuration
OpenAIClient client = new OpenAIClient
    (PluginConfiguration.OpenAI.ApiKey);
// Instantiate the OpenAI chat completion service with the GPT-3 model
OpenAIChatCompletionService service 
    = new OpenAIChatCompletionService
        (Models.GPT3Model, 
        client);
//prompt user for expertise
Console.WriteLine("Expertise:");
string expertise = Console.ReadLine();
// create a system prompt with the expertise
string prompt = $"You're an expert in {expertise}.";
ChatHistory history = new ChatHistory();
history.AddSystemMessage(prompt);
//prompt user for question
Console.WriteLine($"Enter your question regarding {expertise}:");
string question = Console.ReadLine();
// add the question to the chat history
history.AddUserMessage(question);
// get the chat message content from the chat completion service and display on console
var reply = service.GetChatMessageContentAsync(history);
Console.WriteLine(reply.Result);
