using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.SemanticFunctions;
using Microsoft.SemanticKernel.SkillDefinition;
using Microsoft.SemanticKernel.Skills.Core;

internal static class PlannerExample{

    static IKernel kernel;

    static PlannerExample(){
        kernel = Kernel.Builder
            .WithOpenAITextEmbeddingGenerationService(
                PluginConfiguration.OpenAI.ChatModelId,
                PluginConfiguration.OpenAI.ApiKey)
        .Build();
    }
    internal static async Task SequentialPlanAsync(){
        IDictionary<string, ISKFunction> mathSkill = 
            kernel.ImportSkill(new MathSkill());
        IDictionary<string, ISKFunction> timeSkill = 
            kernel.ImportSkill(new TimeSkill());
        
        var planner = new SequentialPlanner(kernel);
        var ask = "If the current day is the weekend, add 1 to starting amount, else add 100 to starting amount. Starting amount is 1000. What is the total?";
        try
        {
            var plan = await planner.CreatePlanAsync(ask);
            Console.WriteLine(plan.ToJson());
            SKContext context = await plan.InvokeAsync();
            Console.WriteLine(context.Result);
        }
        catch (SKException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}