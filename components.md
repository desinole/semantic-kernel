# Component

### Kernel

- Kernel: "The core, center, or essence of an object or system." —Wiktionary
- Kernel refers to an instance of the processing engine that processes an ASK all the way through to fulfillment. 

``` csharp
using Microsoft.SemanticKernel;
var myKernel = Kernel.Builder.Build();
```

There are a variety of things that you can do with myKernel that include:

- Configuring the kernel to use OpenAI or Azure OpenAI
- Sourcing a collection of skills
- Chaining multiple skills' together
- Customize how the kernel works to fit your exact needs

### Planner

- The planner works backwards from a goal that’s provided from a user's ASK.
- The planner has access to either a pre-defined library of pre-made skills and/or a dynamically defined set of skills it is able to fulfill an ASK with confidence.
- The planner calls upon memories to best situate the ASK's context and connectors to call APIs and to leverage other external capabilities.
- The planner will operate within the skills it has available. In the event that a desired skill does not exist, the planner can suggest you to create the skill. Or, depending upon the level of complexity the kernel can help you write the missing skill.


### Skills

- A skill refers to a domain of expertise made available to the kernel as a single function, or as a group of functions related to the skill.

A function is the basic building block for a skill. A function can be expressed as either:

1. an LLM AI prompt — also called a "semantic" function
2. native computer code -- also called a "native" function

- A skill is the container in which functions live.
- Skill is a directory with subdirectories of semantic and native functions
- Native functions are loosely inspired by Azure Functions and exist as individual native skill files.

```
MyAppSource
│
└───MySkillsDirectory
    │
    └─── MySemanticSkill (a directory)
    |   │
    |   └─── MyFirstSemanticFunction (a directory)
    |   └─── MyOtherSemanticFunction (a directory)
    │
    └─── MyNativeSkill.cs (a file)
    └─── MyOtherNativeSkill.cs (a file)
```

### Memories

Memories are like RAM where AI models are like CPU

3 ways SK accesses memories
- KV Pairs: Similar to env variable, convenient lookup
- Local storage: From local file
- Semantic memory search: Access from embeddings

#### How does Semantic Memory Search work?

- Embeddings are a way of representing words or other data as vectors in a high-dimensional space.
- Similar words or data will have similar vectors, and different words or data will have different vectors.
- So basically you take a sentence, paragraph, or entire page of text, and then generate the corresponding embedding vector.
- When a query is performed, the query is transformed to its embedding representation, and then a search is performed through all the existing embedding vectors to find the most similar ones. 

#### Why are embeddings important?

- We need to consider the length of the input text based on the token limit of the model we choose to use
- GPT-4 can handle up to 8,192 tokens per input, while GPT-3 can only handle up to 4,096 tokens.
- Embeddings break down larger text into smaller pieces.
- An embedding vector is like a compressed representation of the text that preserves its meaning and context. 
- Then we can compare the embedding vectors of our summaries with the embedding vector of our prompt and select the most similar ones.


### Connectors