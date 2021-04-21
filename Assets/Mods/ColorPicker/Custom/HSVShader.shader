Shader "Unlit/HSVShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Hue ("Hue", Range(0.0, 1.0)) = 0

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

            float _Hue;

            fixed3 hue2rgb(float3 c)
            {
                fixed4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                fixed3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 white = fixed4(1,1,1,1);
                fixed4 black = fixed4(0,0,0,1);

                fixed3 c = hue2rgb(float3(_Hue, 1, 1));
                fixed4 colour = fixed4(c.x, c.y, c.z, 1);
                fixed4 satGrad = lerp(white, colour, i.uv.x);
                fixed4 o = lerp(black, satGrad, i.uv.y);

                col = fixed4(o.x, o.y, o.z, 1);
                return col;
            }
            ENDCG
        }
    }
}
