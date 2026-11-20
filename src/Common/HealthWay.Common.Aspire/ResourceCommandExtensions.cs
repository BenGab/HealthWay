using Aspire.Hosting;
using System.Net.Http.Json;
using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;

namespace HealthWay.Common.Aspire;

public static class ResourceCommandExtensions
{
    private const string MessageArgument = "message";

    /// <summary>
    /// Adds a dashboard command that prompts for a message and POSTs it as <c>{ "message": ... }</c>
    /// to <paramref name="path"/> on the resource's HTTP endpoint.
    /// </summary>
    public static IResourceBuilder<T> WithSendMessageCommand<T>(
        this IResourceBuilder<T> builder, string commandName, string displayName, string path, string endpointName = "http")
        where T : IResourceWithEndpoints =>
        builder.WithCommand(
            commandName,
            displayName,
            async context =>
            {
                var message = context.Arguments.GetString(MessageArgument);
                if (string.IsNullOrWhiteSpace(message))
                {
                    return CommandResults.Failure("A message is required.");
                }

                var endpoint = builder.GetEndpoint(endpointName);
                var client = context.Services.GetRequiredService<IHttpClientFactory>().CreateClient();
                using var response = await client.PostAsJsonAsync(
                    new Uri(new Uri(endpoint.Url), path), new { message }, context.CancellationToken);
                var body = await response.Content.ReadAsStringAsync(context.CancellationToken);

                return response.IsSuccessStatusCode
                    ? CommandResults.Success(body)
                    : CommandResults.Failure($"{(int)response.StatusCode} {response.ReasonPhrase}: {body}");
            },
            new CommandOptions
            {
                Arguments =
                [
                    new InteractionInput { Name = MessageArgument, Label = "Message", InputType = InputType.Text, Required = true }
                ]
            });
}
