using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Editor.Accounts.Api.Request;
using Editor.Accounts.Api.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Editor.Accounts {
    public class OceanClient {
        private AmazonS3Client client;
        public const string BundleBucket = "polusgg-assetbundles";
        public const string ThumbnailBucket = "polusgg-cosmetics-assets";
        public const string BundleLocation = "https://client-assetbundles.polus.gg";
        public const string ThumbnailLocation = "https://cosmetic.asset.polus.gg";
        //bypass the cache?
        // public const string BundleLocation = "https://polusgg-assetbundles.nyc3.digitaloceanspaces.com";
        // public const string ThumbnailLocation = "https://polusgg-cosmetics-assets.nyc3.digitaloceanspaces.com";
        public static readonly Uri DOEndpoint = new Uri("https://api.digitalocean.com/v2/cdn/endpoints");

        public OceanClient() {
            client = new AmazonS3Client(new BasicAWSCredentials(AccountMenu.Save.S3Key, AccountMenu.Save.S3Secret), new AmazonS3Config {
                ServiceURL = "https://nyc3.digitaloceanspaces.com"
            });
        }

        public static string FormatName(string bundle, string cosmetic) => $"{bundle}/{cosmetic}";
        public static string FormatUrl(string endpoint, string bundle, string cosmetic) => $"{endpoint}/{FormatName(bundle, cosmetic)}";

        public static async Task Upload(OceanClient client, string bucket, string location, Stream stream) {
            PutObjectResponse por = await client.client.PutObjectAsync(new PutObjectRequest {
                BucketName = bucket,
                Key = location,
                InputStream = stream,
                CannedACL = S3CannedACL.PublicRead
            });
            Debug.Log(por);
        }

        //DONT USE THIS IT LITERALLY DOES NOT WORK LOL

        public static async Task Purge(string[] files, string[] buckets) {
            HttpClient client = new HttpClient();

            HttpRequestMessage listRequest = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(DOEndpoint, "endpoints"),
            };

            listRequest.Headers.Clear();
            listRequest.Headers.TryAddWithoutValidation("Content-Type", "application/json");
            listRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {AccountMenu.Save.DoPersonalToken}");

            string list = await (await client.SendAsync(listRequest)).Content.ReadAsStringAsync();

            DoListEndpoint listObject = JsonConvert.DeserializeObject<DoListEndpoint>(list);
            // foreach (DoListEndpoint.Endpoint end in listObject.Endpoints) {
                // Debug.Log($"{end.Id} {end.Origin} {end.CustomDomain}");
            // }

            foreach (string bucket in buckets) {
                DoListEndpoint.Endpoint point = listObject.Endpoints.First(x => x.Origin.Contains(bucket) || x.CustomDomain.Contains(bucket));
                // return;
                HttpRequestMessage purgeRequest = new HttpRequestMessage {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(DOEndpoint, $"endpoints/{point.Id}/cache"),
                    Content = new StringContent(JsonConvert.SerializeObject(new DoPurgeEndpoint {
                        Files = files
                    }))
                };
                
                Debug.Log($"requesting {await purgeRequest.Content.ReadAsStringAsync()}");

                purgeRequest.Headers.Clear();
                purgeRequest.Headers.TryAddWithoutValidation("Content-Type", "application/json");
                purgeRequest.Headers.TryAddWithoutValidation("Authorization", $"Bearer {AccountMenu.Save.DoPersonalToken}");

                Debug.Log($"figma balls {purgeRequest.RequestUri}");

                HttpResponseMessage response = await client.SendAsync(purgeRequest);
                string data = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    Debug.Log($"POGGER {response.StatusCode}");
                else Debug.LogError($"FUCK {data}");
            }
        }
    }
}