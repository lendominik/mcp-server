using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole(consoleLogOptions =>
{
    // Configure all logs to go to stderr
    consoleLogOptions.LogToStandardErrorThreshold = LogLevel.Trace;
});

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"hello from C#: {message}";

    [McpServerTool, Description("Reverses the message and echoes it back to the client.")]
    public static string ReverseEcho(string message)
    {
        var charArray = message.ToCharArray();
        Array.Reverse(charArray);
        return $"hello from C#: {new string(charArray)}";
    }

    [McpServerTool, Description("Converts the message to uppercase and echoes it back to the client.")]
    public static string UppercaseEcho(string message) => $"hello from C#: {message.ToUpper()}";
}