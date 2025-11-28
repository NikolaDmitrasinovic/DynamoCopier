using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: dotnet run -- <SourceTable> <TargetTable>");
            return;
        }

        var sourceTable = args[0];
        var targetTable = args[1];

        var regionName = Enviroment.GetEnviromentVariable("AWS_REGION") ?? "us-east-1";
        var region = RegionEndpoint.GetBySystemName(regionName);

        Console.WriteLine($"Region: {region.SystemName}");

        using var client = new AmazonDynamoDBClient(region);

        try
        {

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error copying table:");
            Console.ResetColor();
            Console.WriteLine(ex);
        }
    }

    private static IEnumerable<List<Dictionary<string, AttributeValue>>> Chunk(List<Dictionary<string, AttributeValue>> items, int size)
    {
        for (int i= 0; i < items.Count; i += size)
            yield return items.GetRange(i, Math.Min(size, items.Count - i));
    }
}