
using System.IO;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using UnityEngine;

namespace Editor.Accounts {
    public class BundleS3Client {
        private AmazonS3Client client;
        public const string BundleBucket = "polusgg-assetbundles";
        public const string ThumbnailBucket = "polusgg-cosmetics-assets";
        public const string BundleLocation = "https://client-assetbundles.polus.gg";
        public const string ThumbnailLocation = "https://polusgg-cosmetics-assets.nyc3.digitaloceanspaces.com";

        public BundleS3Client(string endpoint) {
            client = new AmazonS3Client(new BasicAWSCredentials(AccountMenu.Save.S3Key, AccountMenu.Save.S3Secret), new AmazonS3Config {
                ServiceURL = "https://nyc3.digitaloceanspaces.com"
            });
        }

        public static string FormatName(string bundle, string cosmetic) => $"{bundle}/{cosmetic}";
        public static string FormatUrl(string endpoint, string bundle, string cosmetic) => $"{endpoint}/{FormatName(bundle, cosmetic)}";

        public static async Task<PutObjectResponse> Upload(BundleS3Client client, string bucket, string location, FileStream stream) {
            PutObjectResponse por = await client.client.PutObjectAsync(new PutObjectRequest {
                BucketName = bucket,
                Key = location,
                InputStream = stream,
                CannedACL = S3CannedACL.PublicRead
            });
            Debug.Log(por.HttpStatusCode);
            return por;
        }
    }
}