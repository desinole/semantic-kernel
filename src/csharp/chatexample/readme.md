# Simple ChatGPT example

### Prereqs

Sign up for an OpenAI account and get an API key from the portal.

Add packages
```dotnetcli
dotnet add package Microsoft.SemanticKernel --prerelease
```

Add reference to Utils project
```dotnetcli
dotnet add reference ..\Utils\Utils.csproj
```

Add dotnet secrets
```dotnetcli
dotnet user-secrets init
dotnet user-secrets set "OpenAI:ChatModelId" "gpt-3.5-turbo"
dotnet user-secrets set "OpenAI:EmbeddingModelId" "text-embedding-ada-002"
dotnet user-secrets set "OpenAI:ApiKey" "openai api key from portal"
```