using UnityEngine;

namespace Assets.Editor {
    public static class ColorExtensions {
        public static string ToRgba(this Color32 color) => $"{color.r:X2}{color.g:X2}{color.b:X2}{color.a:X2}";
    }
}