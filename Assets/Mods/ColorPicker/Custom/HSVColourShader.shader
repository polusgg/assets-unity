Shader "Unlit/HSVColourShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HSV ("Hue/Sat/Val", Vector) = (0,0,0,0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 _HSV;

            fixed3 hue2rgb(fixed3 c)
            {
                fixed4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed3 pc = hue2rgb(fixed3(_HSV.x, _HSV.y, _HSV.z));
                fixed3 hs = hue2rgb(fixed3(_HSV.x, _HSV.y * 1.229362f, _HSV.z * 0.6993335f));

                fixed cf = step(i.uv.x, i.uv.y);
                fixed3 shape = fixed3(cf, cf, cf);

                fixed4 o = fixed4((pc.x * cf) + (hs.x * (1-cf)), (pc.y * cf) + (hs.y * (1-cf)), (pc.z * cf) + (hs.z * (1-cf)), 1);
                return o;
            }
            ENDCG
        }
    }
}
