using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Transformer.Api.Models;

namespace TransformerService.Tests.IntegrationTests;

public class TransformControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TransformControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Transform_ValidRequest_ReturnsTransformedResults()
    {
        // Arrange
        var request = new TransformRequest
        {
            Elements = new List<ElementModel>
            {
                new ElementModel
                {
                    Value = "Text with numbers 12345 and letters.",
                    Transformers = new List<TransformerModel>
                    {
                        new TransformerModel
                        {
                            GroupId = "Group1",
                            TransformerId = "RegexRemoval",
                            Parameters = new Dictionary<string, string>
                            {
                                { "regex", "\\d+" }
                            }
                        },
                        new TransformerModel
                        {
                            GroupId = "Group1",
                            TransformerId = "RegexReplacement",
                            Parameters = new Dictionary<string, string>
                            {
                                { "regex", "letters" },
                                { "replacement", "characters" }
                            }
                        }
                    }
                }
            }
        };

        var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/transform", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var transformResponse = JsonConvert.DeserializeObject<TransformResponse>(responseString);

        Assert.NotNull(transformResponse);
        Assert.Single(transformResponse.Results);
        Assert.Equal("Text with numbers  and characters.", transformResponse.Results[0].TransformedValue);
    }

    [Fact]
    public async Task Transform_InvalidModel_ReturnsBadRequest()
    {
        // Arrange
        var requestContent = new StringContent("{}", Encoding.UTF8, "application/json"); // Empty request

        // Act
        var response = await _client.PostAsync("/api/transform", requestContent);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}