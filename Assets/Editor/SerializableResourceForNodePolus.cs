using Newtonsoft.Json;

namespace Assets.Editor
{
    public class SerializableForNodePolus {
        public uint AssetBundleId;
        public string Hash;
        public AssetDecl[] Assets;
    }
    
    public enum AssetType {
        Other = 0,
        Audio = 1,
        Hat = 2
    }
    
    public class AssetDecl {
        public AssetType Type;
        public string Path;
        public AssetDetails Details;
    }

    public class AssetDetails { }

    public class AudioDetails : AssetDetails {
        public int Samples;
        public float SampleRate;
    }
}