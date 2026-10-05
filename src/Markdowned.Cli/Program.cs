using System.Diagnostics.CodeAnalysis;
using Markdowned.Cli.Commands;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Cli;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        try
        {
            await foreach (IString line in new Command(args))
            {
                Console.WriteLine(line.TextValue);
            }

            return 0;
        }
        catch (IOException error)
        {
            await Console.Error.WriteLineAsync(error.Message);
            return 1;
        }
    }
}
