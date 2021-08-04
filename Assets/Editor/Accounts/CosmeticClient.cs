using System;
using System.IO;
using System.Linq;
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
                BaseAddress = new Uri("http://127.0.0.1:2219/v1/")
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
        public async Task<GenericCosmeticResponse<uint>> UpdateBundle(CosmeticBundleObject bundle) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, $"bundle/{bundle.Name}"),
                Method = new HttpMethod("PATCH"),
                Content = new StringContent(JsonConvert.SerializeObject(new BundleUpdate {
                    Name = bundle.Name,
                    Cosmetics = bundle.Cosmetics.Select(b => b.SanitizedName).ToArray(),
                    CoverArt = BundleS3Client.FormatUrl(BundleS3Client.ThumbnailLocation + "/CoverArt", bundle.Name, Path.GetFileName(AssetDatabase.GetAssetPath(bundle.CoverArt))),
                    Color = bundle.Color.ToRgba(),
                    Description = bundle.Description,
                    Id = bundle.LowerName,
                    Price = bundle.Price,
                    ForSale = bundle.ForSale
                }), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"nuts {new Uri(_client.BaseAddress, $"bundle/{bundle.Name}")}");

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
        public async Task<GenericCosmeticResponse<uint>> UploadBundle(CosmeticBundleObject bundle) {
            HttpRequestMessage request = new HttpRequestMessage {
                RequestUri = new Uri(_client.BaseAddress, $"bundle/{bundle.Name}"),
                Method = HttpMethod.Put,
                Content = new StringContent(JsonConvert.SerializeObject(new BundleCreate {
                    Name = bundle.Name,
                    Cosmetics = bundle.Cosmetics.Select(b => b.SanitizedName).ToArray(),
                    CoverArt = BundleS3Client.FormatUrl(BundleS3Client.ThumbnailLocation + "/CoverArt", bundle.Name, Path.GetFileName(AssetDatabase.GetAssetPath(bundle.CoverArt))),
                    Color = bundle.Color.ToRgba(),
                    Description = bundle.Description,
                    Id = bundle.LowerName,
                    Price = bundle.Price,
                    ForSale = bundle.ForSale
                }), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"nuts {new Uri(_client.BaseAddress, $"bundle/{bundle.Name}")}");

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
                RequestUri = new Uri(_client.BaseAddress, $"item/{cosmetic.Name}"),
                Method = new HttpMethod("PATCH"),
                Content = new StringContent(JsonConvert.SerializeObject(new ItemUpdate {
                    Name = cosmetic.Name,
                    Resource = new Resource {
                        Path = BundleS3Client.FormatUrl($"{BundleS3Client.BundleLocation}/Cosmetics", bundle, cosmetic.Name),
                        Id = cosmetic.Id + 1
                    },
                    ThumbnailUrl = BundleS3Client.FormatUrl(BundleS3Client.ThumbnailLocation, bundle, Path.GetFileName(AssetDatabase.GetAssetPath(cosmetic.Thumbnail))),
                    Type = cosmetic.Type,
                }, new StringEnumConverter(new CapitalCaseNamingStrategy())), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"nuts {new Uri(_client.BaseAddress, $"item/{cosmetic.Name}")}");

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
                RequestUri = new Uri(_client.BaseAddress, $"item/{cosmetic.Name}"),
                Method = HttpMethod.Put,
                Content = new StringContent(JsonConvert.SerializeObject(new ItemCreation {
                    Id = cosmetic.SanitizedName,
                    Name = cosmetic.Name,
                    IngameId = cosmetic.Id,
                    Resource = new Resource {
                        Path = BundleS3Client.FormatUrl($"{BundleS3Client.BundleLocation}/Cosmetics", bundle, cosmetic.Name),
                        Id = cosmetic.Id + 1
                    },
                    ThumbnailUrl = BundleS3Client.FormatUrl(BundleS3Client.ThumbnailLocation, bundle, Path.GetFileName(AssetDatabase.GetAssetPath(cosmetic.Thumbnail))),
                    Type = cosmetic.Type,
                }, new StringEnumConverter(new CapitalCaseNamingStrategy())), Encoding.UTF8, "application/json")
            };
            request.Headers.TryAddWithoutValidation("Authorization", $"{AccountMenu.Save.ClientToken}:{AccountMenu.Save.ClientId}");

            Debug.Log($"sugondese {new Uri(_client.BaseAddress, $"item/{cosmetic.Name}")}");

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