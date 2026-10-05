using System.Diagnostics.CodeAnalysis;
using Markdowned.Browser;
using Markdowned.Cli.Commands;
using Markdowned.DevTools;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Cli;

[ExcludeFromCodeCoverage]
internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        using CancellationTokenSource cancellation = new CancellationTokenSource();
        Console.CancelKeyPress += (_, eventArgs) =>
        {
            eventArgs.Cancel = true;
            cancellation.Cancel();
        };

        try
        {
            await foreach (
                IString line in new Command(args).WithCancellation(cancellation.Token)
            )
            {
                Console.WriteLine(line.TextValue);
            }

            return 0;
        }
        catch (Exception error) when (error is ArgumentException or IOException)
        {
            await Console.Error.WriteLineAsync(error.Message);
            return 1;
        }
        catch (BrowserException error)
        {
            await Console.Error.WriteLineAsync(error.Message);
            return 2;
        }
        catch (DevToolsException error)
        {
            await Console.Error.WriteLineAsync(error.Message);
            return 3;
        }
        catch (OperationCanceledException)
        {
            return 130;
        }
    }
}
