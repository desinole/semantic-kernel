# Local Development

### Required Tools

[Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- [Install](https://go.microsoft.com/fwlink/?linkid=2174087)

- Create a folder (c:\temp\sk) and open a command prompt and navigate to that folder cd c:\temp\sk

- Run the following command to create a new console project
```dotnetcli
dotnet new console
```

Instantiate the kernel and run it
```dotnetcli
dotnet add package Microsoft.SemanticKernel --prerelease
```

```csharp
using Microsoft.SemanticKernel;

// Set Simple kernel instance
IKernel kernel_1 = KernelBuilder.Create();
```
Add built in skills
```dotnetcli
using Microsoft.SemanticKernel.Skills.Core;
```

```csharp
// Load native skill
var text = kernel.ImportSkill(new TextSkill());

SKContext result = await kernel.RunAsync("    i n f i n i t e     s p a c e     ",
    text["TrimStart"],
    text["TrimEnd"],
    text["Uppercase"]);

Console.WriteLine(result);
```

Overall
Create kernel 
outline to add add model (brains)
add plugin - native and llm prompts

Pre-requisites
Create Azure Open AI endpoint and chatgpt deployment

Create project
dotnet new console
add following packages
Microsoft.Extensions.Configuration
Microsoft.Extensions.Configuration.Abstractions
Microsoft.Extensions.Configuration.Binder
Microsoft.Extensions.Configuration.EnvironmentVariables
Microsoft.Extensions.Configuration.Json
Microsoft.Extensions.Configuration.UserSecrets
Microsoft.Extensions.DependencyInjection
System.Linq.Async

Review PluginConfiguration.cs


[Secret Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets)
to avoid the risk of leaking secrets into the repository, branches and pull requests.
You can also use environment variables if you prefer.

To set your secrets with Secret Manager (inside project folder):

dotnet user-secrets init

then add azure open ai secrets
dotnet user-secrets set "AzureOpenAI:DeploymentName" "..."
dotnet user-secrets set "AzureOpenAI:Endpoint" "https://... .openai.azure.com/"
dotnet user-secrets set "AzureOpenAI:ApiKey" "..."

dotnet add package Microsoft.SemanticKernel --prerelease

review chatexample.cs which shows how to use built-in fileio and summarize skill along with chatgpt

use writerskill to rewrite Lincoln's Gettysburg address in modern English using chatgpt
Take the output and summarize it using the built-in summarization skill
Shows planner with sequential

Prep work
setup up class that connects to azure open ai endpoint and initializes kernel


We will show how to use built-in skills, how to use chat-gpt to read links and summarize it, and how to use the planner to create a sequential pipeline of skills.

1. Summarize text block with built-in text skills
dotnet new console
dotnet add package Microsoft.SemanticKernel --prerelease

2. Summarize text block with plugin

1. Use chat-gpt to read a link and summarize it with built-in skills
- orchestrate with planner
1. Use chat-gpt to read multiple links from memory and summarize them with built-in skills
- Show different memory stores like redis https://github.com/microsoft/semantic-kernel/tree/main/dotnet/src/Connectors/Connectors.Memory.Redis
- add another item and see the memory expand
1. Use chat-gpt to read multiple links from memory and summarize them with built-in skills and recommend additional reading sources
- Show how to use planner to create a sequential pipeline of skills
