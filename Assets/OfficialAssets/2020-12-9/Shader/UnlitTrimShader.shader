Shader "Unlit/TrimShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Percent ("Percent Offset X", Float) = 0
		_PercentY ("Percent Offset Y", Float) = 0
		_NormalizedUvs ("NormUvs", Vector) = (0,1,0,0)
		_Mask ("Mask", Float) = 8
	}
	SubShader {
		Tags { "QUEUE" = "Transparent" "RenderType" = "Trimmed" }
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Trimmed" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Cull Off
			Stencil {
				Ref 2
				Comp Disabled
				Pass Replace
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 36209
			Program "vp" {
				SubProgram "d3d11 " {
					"vs_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform VGlobals {
						vec4 unused_0_0[2];
						vec4 _MainTex_ST;
						vec4 unused_0_2[2];
					};
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_1_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_2_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_2_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					in  vec4 in_COLOR0;
					out vec2 vs_TEXCOORD0;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    vs_COLOR0 = in_COLOR0;
					    return;
					}"
				}
			}
			Program "fp" {
				SubProgram "d3d11 " {
					"ps_4_0
					
					#version 330
					#extension GL_ARB_explicit_attrib_location : require
					#extension GL_ARB_explicit_uniform_location : require
					
					#define HLSLCC_ENABLE_UNIFORM_BUFFERS 1
					#if HLSLCC_ENABLE_UNIFORM_BUFFERS
					#define UNITY_UNIFORM
					#else
					#define UNITY_UNIFORM uniform
					#endif
					#define UNITY_SUPPORTS_UNIFORM_LOCATION 1
					#if UNITY_SUPPORTS_UNIFORM_LOCATION
					#define UNITY_LOCATION(x) layout(location = x)
					#define UNITY_BINDING(x) layout(binding = x, std140)
					#else
					#define UNITY_LOCATION(x)
					#define UNITY_BINDING(x) layout(std140)
					#endif
					layout(std140) uniform PGlobals {
						vec4 unused_0_0[3];
						float _Percent;
						float _PercentY;
						vec4 _NormalizedUvs;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					float u_xlat1;
					bool u_xlatb1;
					float u_xlat4;
					bool u_xlatb4;
					void main()
					{
					    u_xlat0.x = _Percent * _NormalizedUvs.y;
					    u_xlat0.y = (-_PercentY) * _NormalizedUvs.w;
					    u_xlat0.xy = (-u_xlat0.xy) + vs_TEXCOORD0.xy;
					    u_xlatb4 = u_xlat0.x<_NormalizedUvs.x;
					    if(((int(u_xlatb4) * int(0xffffffffu)))!=0){discard;}
					    u_xlat4 = _NormalizedUvs.y + _NormalizedUvs.x;
					    u_xlatb4 = u_xlat4<u_xlat0.x;
					    if(((int(u_xlatb4) * int(0xffffffffu)))!=0){discard;}
					    u_xlatb4 = u_xlat0.y<_NormalizedUvs.z;
					    if(((int(u_xlatb4) * int(0xffffffffu)))!=0){discard;}
					    u_xlat4 = _NormalizedUvs.w + _NormalizedUvs.z;
					    u_xlatb4 = u_xlat4<u_xlat0.y;
					    if(((int(u_xlatb4) * int(0xffffffffu)))!=0){discard;}
					    u_xlat0 = texture(_MainTex, u_xlat0.xy);
					    u_xlat1 = u_xlat0.w + -0.349999994;
					    u_xlatb1 = u_xlat1<0.0;
					    if(((int(u_xlatb1) * int(0xffffffffu)))!=0){discard;}
					    SV_Target0 = u_xlat0 * vs_COLOR0;
					    return;
					}"
				}
			}
		}
	}
}