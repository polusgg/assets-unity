using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Editor;
using Assets.Editor.HatCreator;
using Editor.Accounts.Api;
using Editor.Accounts.Api.Request;
using Editor.Accounts.Api.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnityEditor;
using UnityEngine;

namespace Editor.Accounts {
    public class CosmeticClient {
        public static CosmeticClient Client = new CosmeticClient();
        private readonly HttpClient _client;
        private readonly JsonSerializerSettings _settings;

        public CosmeticClient() {
            _client = new HttpClient {
                BaseAddress = new Uri("http://sanae6.ca:2219/v1/")
                // BaseAddress = new Uri("http://cosmetics.service.polus.gg:2219/v1/")
                // BaseAddress = new Uri("http://159.203.86.28:2219/v1/")
                // BaseAddress = new Uri("http://127.0.0.1:2219/v1/")
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            _settings = new JsonSerializerSettings {
                ContractResolver = new DefaultContractResolver {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        public async Task<GenericCosmeticResponse<uint>> NextCosmeticId() {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, "item/next"),
                Method = HttpMethod.Get
            };

            Debug.Log($"suag9ijgop {new Uri(_client.BaseAddress, "item/next")}");

            HttpResponseMessage response = await _client.SendAsync(request);
            
            Debug.Log($"Ended request {response.StatusCode}");

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<GenericCosmeticResponse<uint>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );
            }

            return null;
        }

        public async Task<GenericCosmeticResponse<bool>> CheckConflicts(ItemCreation itemInfo) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, "item/next"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(itemInfo), Encoding.UTF8, "application/json")
            };

            Debug.Log($"ghandeez nuts fit in yo mouth {new Uri(_client.BaseAddress, "item/next")}");

            HttpResponseMessage response = await _client.SendAsync(request);
            
            Debug.Log($"Ended request {response.StatusCode}");

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<GenericCosmeticResponse<bool>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );
            }

            return null;
        }
        public async Task<GenericCosmeticResponse<uint>> UpdateBundle(CosmeticBundleObject bundle) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, $"bundle/{bundle.SanitizedName}"),
                Method = new HttpMethod("PATCH"),
                Content = new StringContent(JsonConvert.SerializeObject(new BundleUpdate {
                    Name = bundle.Name,
                    Cosmetics = bundle.Cosmetics.Select(b => b.SanitizedName).ToArray(),
                    CoverArt = Uri.EscapeUriString(OceanClient.FormatUrl(OceanClient.ThumbnailLocation, bundle.Name, "cover.png")),
                    Color = bundle.Color.ToRgba(),
                    Description = bundle.Description,
                    Price = int.Parse(bundle.Price.ToString("###.00", CultureInfo.InvariantCulture).Replace(".", "")),
                    ForSale = bundle.ForSale,
                    Recurring = false,
                }), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"nuts {new Uri(_client.BaseAddress, $"bundle/{bundle.SanitizedName}")}");

            HttpResponseMessage response = await _client.SendAsync(request);
            
            Debug.Log($"Ended request {response.StatusCode}");

            Debug.Log(await response.Content.ReadAsStringAsync());

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<GenericCosmeticResponse<uint>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );
            }

            return null;
        }
        public async Task<GenericCosmeticResponse<uint>> UploadBundle(CosmeticBundleObject bundle) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, $"bundle/{bundle.SanitizedName}"),
                Method = HttpMethod.Put,
                Content = new StringContent(JsonConvert.SerializeObject(new BundleCreate {
                    Name = bundle.Name,
                    Cosmetics = bundle.Cosmetics.Select(b => b.SanitizedName).ToArray(),
                    CoverArt = Uri.EscapeUriString(OceanClient.FormatUrl(OceanClient.ThumbnailLocation, bundle.Name, "cover.png")),
                    Color = bundle.Color.ToRgba(),
                    Description = bundle.Description,
                    Price = int.Parse(bundle.Price.ToString("###.00", CultureInfo.InvariantCulture).Replace(".", "")),
                    ForSale = bundle.ForSale,
                    Recurring = false,
                }), Encoding.UTF8, "application/json")
            };
            
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"nuts {new Uri(_client.BaseAddress, $"bundle/{bundle.SanitizedName}")}");

            HttpResponseMessage response = await _client.SendAsync(request);
            
            Debug.Log($"Ended request {response.StatusCode}");

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<GenericCosmeticResponse<uint>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );
            }

            Debug.Log(await response.Content.ReadAsStringAsync());

            return null;
        }
        
        // this doesn't return an actual value, ignore it :)
        public async Task<GenericCosmeticResponse<uint>> UpdateItem(string bundle, CosmeticBundleObject.CosmeticData cosmetic) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, $"item/{cosmetic.SanitizedName}"),
                Method = new HttpMethod("PATCH"),
                Content = new StringContent(JsonConvert.SerializeObject(new ItemUpdate {
                    Name = cosmetic.Name,
                    Author = cosmetic.Author,
                    IngameId = cosmetic.Id,
                    Resource = new Resource {
                        Path = Uri.EscapeUriString(OceanClient.FormatUrl("Cosmetics", bundle, cosmetic.Name)),
                        Space = OceanClient.BundleLocation,
                        Id = cosmetic.Id + 1
                    },
                    ThumbnailUrl = Uri.EscapeUriString(OceanClient.FormatUrl(OceanClient.ThumbnailLocation, bundle, cosmetic.Name)),
                    Type = cosmetic.Type,
                }, new StringEnumConverter(new CapitalCaseNamingStrategy())), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"nuts {new Uri(_client.BaseAddress, $"item/{cosmetic.SanitizedName}")}");

            HttpResponseMessage response = await _client.SendAsync(request);
            
            Debug.Log($"Ended request {response.StatusCode}");

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<GenericCosmeticResponse<uint>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );
            }

            Debug.Log(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task<GenericCosmeticResponse<uint>> UploadItem(string bundle, CosmeticBundleObject.CosmeticData cosmetic) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, $"item/{cosmetic.SanitizedName}"),
                Method = HttpMethod.Put,
                Content = new StringContent(JsonConvert.SerializeObject(new ItemCreation {
                    Id = cosmetic.SanitizedName,
                    Name = cosmetic.Name,
                    Author = cosmetic.Author,
                    InGameId = cosmetic.Id,
                    Resource = new Resource {
                        Path = Uri.EscapeUriString(OceanClient.FormatUrl("Cosmetics", bundle, cosmetic.Name)),
                        Space = OceanClient.BundleLocation,
                        Id = cosmetic.Id + 1
                    },
                    ThumbnailUrl = Uri.EscapeUriString(OceanClient.FormatUrl(OceanClient.ThumbnailLocation, bundle, cosmetic.Name)),
                    Type = cosmetic.Type,
                }, new StringEnumConverter(new CapitalCaseNamingStrategy())), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"sugondese {new Uri(_client.BaseAddress, $"item/{cosmetic.SanitizedName}")}");

            HttpResponseMessage response = await _client.SendAsync(request);
            
            Debug.Log($"Ended request {response.StatusCode}");

            if (response.IsSuccessStatusCode) {
                return JsonConvert.DeserializeObject<GenericCosmeticResponse<uint>>(
                    await response.Content.ReadAsStringAsync(),
                    _settings
                );
            }

            Debug.Log(await response.Content.ReadAsStringAsync());

            return null;
        }
    }
}
