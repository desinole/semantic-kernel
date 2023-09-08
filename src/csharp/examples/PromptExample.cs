using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SemanticFunctions;

internal static class PromptExample{

    static IKernel kernel;

    static PromptExample(){
        kernel = Kernel.Builder
            .WithOpenAIChatCompletionService(
                PluginConfiguration.OpenAI.ChatModelId,
                PluginConfiguration.OpenAI.ApiKey)
        .Build();
    }
    internal static async Task SinglePromptTemplateAsync(){
        string summarizeBlurbFlex = """
        Summarize the following text in two sentences or less.
        ---Begin Text---
        {{$INPUT}}
        ---End Text---
        """;
    
    
        var myPromptConfig = new PromptTemplateConfig
        {
            Description = "Take an input and summarize it super-succinctly.",
            Completion =
            {
                MaxTokens = 1000,
                Temperature = 0.2,
                TopP = 0.5,
            }
        };

        var myPromptTemplate = new PromptTemplate(
            summarizeBlurbFlex, 
            myPromptConfig, 
            kernel
        );

        var myFunctionConfig = new SemanticFunctionConfig(
            myPromptConfig, 
            myPromptTemplate);
        
        var myFunction = kernel.RegisterSemanticFunction(
            "CustomFunctions", //name of the skill
            "summarizeBlurbFlex", //name of the function
            myFunctionConfig);
        
        var myOutput = await kernel.RunAsync(
            "Microsoft is deepening its strategic relationship with Zebra Technologies Corp., a world leader in innovative digital solutions, including software and hardware such as rugged Android mobile computers for the frontline workforce. The two companies announced a new collaboration to integrate Microsoft Teams and Microsoft Viva, the employee experience platform, with Zebra’s devices and solutions. The goal is to empower millions of frontline workers with better communication, collaboration, learning and well-being tools. According to a recent Work Trend Index Special Report by Microsoft, frontline workers are essential for business continuity and customer satisfaction, but they often face challenges such as lack of access to information, training and recognition. By leveraging the power of Microsoft’s cloud and AI technologies with Zebra’s industry-specific expertise and devices, the partnership aims to address these gaps and help frontline workers achieve more.", 
            myFunction);
        
        Console.WriteLine(myOutput);
    }

    internal static async Task MultiplePromptTemplateAsync(){
        string elevatorPitchFlex = """
        Write me an elevator pitch for {{$COMPANYNAME}} which is a Silicon Valley startup with 
        a focus on {{$SPECIALTY}} and draw similarities with {{$CURRENTUNICORN}}.
        """;

        var myContext = new ContextVariables(); 
        myContext.Set("COMPANYNAME", "FINDIO"); 
        myContext.Set("SPECIALTY", "Lost and Found Services"); 
        myContext.Set("CURRENTUNICORN","Uber"); 

        var myPromptConfig = new PromptTemplateConfig
        {
            Description = "Write a creative elevator pitch.",
            Completion =
            {
                MaxTokens = 1000,
                Temperature = 0.9,
                TopP = 0.5,
            }
        };

        var myPromptTemplate = new PromptTemplate(
            elevatorPitchFlex, 
            myPromptConfig, 
            kernel
        );

        var myFunctionConfig = new SemanticFunctionConfig(
            myPromptConfig, 
            myPromptTemplate);
        
        var myFunction = kernel.RegisterSemanticFunction(
            "TestPluginFlex", //name of the skill
            "elevatorPitchFlex", //name of the function
            myFunctionConfig);
        
        var myOutput = await kernel.RunAsync(
            myFunction,
            myContext);
        
        Console.WriteLine(myOutput);
    }
}