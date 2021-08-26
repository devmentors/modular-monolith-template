using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Modular.Bootstrapper;
using Xunit;

namespace Modular.Shared.Tests
{
    [Collection("tests")]
    public abstract class WebApiTestBase : IDisposable, IClassFixture<WebApplicationFactory<Startup>>
    {
        private static readonly JsonSerializerOptions SerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {new JsonStringEnumConverter()}
        };
        
        private string _route;

        protected void SetPath(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                _route = string.Empty;
                return;
            }

            if (route.StartsWith("/"))
            {
                route = route.Substring(1, route.Length - 1);
            }

            if (route.EndsWith("/"))
            {
                route = route.Substring(0, route.Length - 1);
            }

            _route = $"{route}/";
        }

        protected static T Map<T>(object data) => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(data, SerializerOptions), SerializerOptions);

        protected Task<HttpResponseMessage> GetAsync(string endpoint)
            => Client.GetAsync(GetEndpoint(endpoint));

        protected async Task<T> GetAsync<T>(string endpoint)
            => await ReadAsync<T>(await GetAsync(endpoint));

        protected Task<HttpResponseMessage> PostAsync<T>(string endpoint, T command)
            => Client.PostAsync(GetEndpoint(endpoint), GetPayload(command));

        protected Task<HttpResponseMessage> PutAsync<T>(string endpoint, T command)
            => Client.PutAsync(GetEndpoint(endpoint), GetPayload(command));

        protected Task<HttpResponseMessage> DeleteAsync(string endpoint)
            => Client.DeleteAsync(GetEndpoint(endpoint));

        protected Task<HttpResponseMessage> SendAsync(string method, string endpoint)
            => SendAsync(GetMethod(method), endpoint);

        protected Task<HttpResponseMessage> SendAsync(HttpMethod method, string endpoint)
            => Client.SendAsync(new HttpRequestMessage(method, GetEndpoint(endpoint)));
        
        protected void Authenticate(Guid userId)
        {
            var jwt = AuthHelper.GenerateJwt(userId);
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        }

        private static HttpMethod GetMethod(string method)
            => method.ToUpperInvariant() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => null
            };

        private string GetEndpoint(string endpoint) => $"{_route}{endpoint}";

        private static StringContent GetPayload(object value)
            => new(JsonSerializer.Serialize(value, SerializerOptions), Encoding.UTF8, "application/json");

        protected static async Task<T> ReadAsync<T>(HttpResponseMessage response)
            => JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), SerializerOptions);

        #region Arrange

        protected readonly HttpClient Client;
        private readonly WebApplicationFactory<Startup> _factory;

        protected WebApiTestBase(WebApplicationFactory<Startup> factory, string environment = "test")
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment(environment);
                builder.ConfigureServices(ConfigureServices);
            });
            Client = _factory.CreateClient();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        public virtual void Dispose()
        {
        }

        #endregion
    }
}