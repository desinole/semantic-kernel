using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Memory;

internal static class MemoryExample
{    static IKernel kernel;

    static MemoryExample(){
        kernel = Kernel.Builder
            .WithOpenAITextEmbeddingGenerationService(
                PluginConfiguration.OpenAI.EmbeddingModelId, 
                PluginConfiguration.OpenAI.ApiKey)
            .WithMemoryStorage(new VolatileMemoryStore())
        .Build();

    }
    private const string MemoryCollectionName = "SKBooks";

    internal static async Task MemoryExampleAsync()
    {
        Console.WriteLine("Memory Example with books");
        Console.WriteLine("--------------");
        Console.WriteLine("Enter query");
        string query = Console.ReadLine();
        await StoreMemoryAsync(kernel);
        await SearchMemoryAsync(kernel, query);
    }

    private static async Task SearchMemoryAsync(IKernel kernel, string query)
    {
        Console.WriteLine("\nQuery: " + query + "\n");

        var memories = kernel.Memory.SearchAsync(MemoryCollectionName, query, limit: 2, minRelevanceScore: 0.5);

        int i = 0;
        await foreach (MemoryQueryResult memory in memories)
        {
            Console.WriteLine($"Result {++i}:");
            Console.WriteLine("  URL:     : " + memory.Metadata.Id);
            Console.WriteLine("  Title    : " + memory.Metadata.Description);
            Console.WriteLine("  Relevance: " + memory.Relevance);
            Console.WriteLine();
        }

        Console.WriteLine("----------------------");
    }

    private static async Task StoreMemoryAsync(IKernel kernel)
    {
        var samples = SampleData();
        foreach (var entry in samples)
        {
            await kernel.Memory.SaveReferenceAsync(
                collection: MemoryCollectionName,
                externalSourceName: "Books",
                externalId: entry.Key,
                description: entry.Value,
                text: entry.Value);

        }

    }
    private static Dictionary<string, string> SampleData()
    {
        return new Dictionary<string, string>
        {
["Spare"] = "Prince Harry. A memoir by the Duke of Sussex, revealing his personal struggles and insights on his royal life, family, and public service",
["It Starts With Us"] = "Colleen Hoover. A romance novel about a young woman who falls in love with a neurosurgeon, but discovers that he has a dark past that threatens their future",
["Atomic Habits"] = "James Clear. A self-help book that teaches readers how to build good habits and break bad ones using proven strategies and practical advice",
["Verity"] = "Colleen Hoover. A thriller novel about a struggling writer who is hired to finish the books of a bestselling author, but uncovers a disturbing secret in her manuscripts",
["Lessons in Chemistry"] = "Bonnie Garmus. A historical fiction novel set in 1960s America, about a brilliant female scientist who becomes a TV cooking show host and challenges the gender norms of her time",
["If He Has Been with Me"] = "Laura Nowlin. A young adult novel about two childhood friends who grow up together, but drift apart as they face different challenges and choices in their lives",
["A Good Girl’s Guide to Murder"] = "Holly Jackson. A young adult mystery novel about a high school student who investigates a closed murder case for her senior project, but finds herself in danger as she uncovers new clues",
["Twenty Thousand Fleas Under the Sea (Dog Man #11)"] = "Dav Pilkey. A children's humor book about the adventures of Dog Man, a canine superhero who fights crime and makes friends with other animals",
["Oh, the Places You’ll Go!"] = "Dr. Seuss. A children's classic book that encourages readers to explore the world and pursue their dreams with optimism and courage",
["The Very Hungry Caterpillar"] = "Eric Carle. A children's picture book that follows the life cycle of a caterpillar who eats his way through various foods until he becomes a beautiful butterfly"
        };
    }

}
