
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Plugins;

// instantiate the config builder and load the configuration from user secrets
var config = new ConfigurationBuilder()
//    .AddEnvironmentVariables()
    .AddUserSecrets(typeof(Program).GetTypeInfo().Assembly)
    .AddUserSecrets<Env>()
    .Build();
// Initialize the plugin configuration class to load the config values from user secrets
PluginConfiguration.Initialize(config);

//instantiate a kernelbuilder object in semantic kernel
var kernelBuilder = Kernel.CreateBuilder();

//register chat completion service to kernelbuilder
kernelBuilder.Services.AddOpenAIChatCompletion(
    Models.GPT3Model,
    PluginConfiguration.OpenAI.ApiKey);

//add plugins to kernelbuilder
kernelBuilder.Plugins.AddFromType<MusicPlayerPlugin>();

//instantiate a kernel object in semantic kernel
var kernel = kernelBuilder.Build();

//create chat history
var chatHistory = new ChatHistory();

// get chat completion service
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

while (true){
    // get user input
    Console.Write("You: ");
    var userInput = Console.ReadLine();
    chatHistory.AddUserMessage(userInput);
    // create openai prompt execution settings object and enable auto function calling
    var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings();
    openAIPromptExecutionSettings.ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions;
    // Get the response from the AI
    var result = await chatCompletionService.GetChatMessageContentAsync(
        chatHistory,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Print the results
    Console.WriteLine("Assistant: " + result);

    // Add the message from the agent to the chat history
    chatHistory.AddMessage(result.Role, result.Content);
}