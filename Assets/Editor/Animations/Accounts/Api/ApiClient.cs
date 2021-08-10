using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Editor.Accounts.Api.Request;
using Editor.Accounts.Api.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Editor.Accounts.Api {
    public class ApiClient : IDisposable {
        private readonly HttpClient _client;

        private readonly JsonSerializerSettings _settings;

        public ApiClient() {
            _client = new HttpClient {
                BaseAddress = new Uri("https://account.polus.gg/api/v1/")
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            _settings = new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        public void Dispose() {
            _client?.Dispose();
        }

        internal async Task<GenericResponse<LoginResponse>> LogIn(string email, string password) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, "auth/token"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(new LogInRequest {
                    Email = email,
                    Password = password
                }), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GenericResponse<LoginResponse>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );

            return null;
        }

        internal async Task<GenericResponse<CheckTokenData>> CheckToken(string clientId, string clientToken) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, "auth/check"),
                Method = HttpMethod.Post
            };
            request.Headers.Add("Client-ID", clientId);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", clientToken);

            HttpResponseMessage response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<GenericResponse<CheckTokenData>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );

            return null;
        }
    }
}