1. Create new console app

2. Add SK reference to project

``` xml
  <ItemGroup>
    <PackageReference Include="Microsoft.SemanticKernel" Version="0.12.207.1-preview" />
  </ItemGroup>
```

3. Insantiate kernel

``` csharp
var myKernel = Kernel.Builder.Build();
```

4. Add CoreSkills reference

``` csharp
using Microsoft.SemanticKernel.CoreSkills;
```

5. Import a CoreSkill (review list of coreskills)

``` csharp
myKernel.ImportSkill(new ConversationSummarySkill(), "Summarize");
```

