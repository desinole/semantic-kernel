using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
using Microsoft.SemanticKernel.Skills.Core;

internal static class ChatExample{

    static IKernel kernel;

    static ChatExample(){
        kernel = Kernel.Builder
            .WithOpenAIChatCompletionService(
                PluginConfiguration.OpenAI.ChatModelId,
                PluginConfiguration.OpenAI.ApiKey)
        .Build();
    }
    internal static async Task GetSummaryAsync(){

        IDictionary<string, ISKFunction> fileIOSkill =
            kernel.ImportSkill(new FileIOSkill());
        SKContext fileIOresult = await kernel.RunAsync(
            "resources/chatexample.txt", // name of the file to read
            fileIOSkill["Read"]); // Read the file using fileioskill "Read" function
        
        string chatText = fileIOresult.Result;

        IDictionary<string, ISKFunction> conversationSummarySkill =
            kernel.ImportSkill(new ConversationSummarySkill(kernel));

        SKContext result = await kernel.RunAsync(
            chatText, // text of the file
            conversationSummarySkill["SummarizeConversation"]); // Get the topics using conversation summary skill "GetConversationTopics" function
        Console.WriteLine(result.Result);
    }
    internal static async Task GetPointsAsync(){

        var fileIOSkill =
            kernel.ImportSkill(new FileIOSkill());
        IDictionary<string, ISKFunction> conversationSummarySkill =
            kernel.ImportSkill(new ConversationSummarySkill(kernel));

        SKContext result = await kernel.RunAsync(
            "resources/chatexample.txt", // name of the file to read
            fileIOSkill["Read"], // Read the file using fileioskill "Read" function
            conversationSummarySkill["GetConversationTopics"]); // Get the topics using conversation summary skill "GetConversationTopics" function
        Console.WriteLine(result.Result);
    }

    internal static async Task GetTwoSentenceSummaryAsync()
    {
        var fileIOSkill =
            kernel.ImportSkill(new FileIOSkill());
        SKContext fileIOresult = await kernel.RunAsync(
            "resources/chatexample.txt", // name of the file to read
            fileIOSkill["Read"]); // Read the file using fileioskill "Read" function
        
        string chatText = fileIOresult.Result;

        var summarizeSkill = kernel.ImportSemanticSkillFromDirectory(
            "skills",
            "Summarize");
        SKContext result = await kernel.RunAsync(
            chatText, // text of the file
            summarizeSkill["FunSummary"]); // Get the topics using conversation summary skill "GetConversationTopics" function
        Console.WriteLine(result.Result);

    }
}