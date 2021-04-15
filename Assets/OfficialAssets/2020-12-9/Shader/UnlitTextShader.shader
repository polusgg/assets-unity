Shader "Unlit/TextShader" {
	Properties {
		_MainTex ("Font Texture", 2D) = "white" {}
		_InputTex ("Input Texture", 2D) = "white" {}
		_ColorTex ("Input Colors", 2D) = "white" {}
		_Mask ("Mask", Float) = 0
	}
	SubShader {
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Stencil {
				Ref 1
				Comp Disabled
				Pass Keep
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 54090
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
					layout(std140) uniform UnityPerDraw {
						mat4x4 unity_ObjectToWorld;
						vec4 unused_0_1[7];
					};
					layout(std140) uniform UnityPerFrame {
						vec4 unused_1_0[17];
						mat4x4 unity_MatrixVP;
						vec4 unused_1_2[2];
					};
					in  vec4 in_POSITION0;
					in  vec2 in_TEXCOORD0;
					out vec2 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    vs_TEXCOORD0.xy = in_TEXCOORD0.xy;
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    gl_Position = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
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
						vec4 _InputTex_TexelSize;
						vec4 unused_0_2;
					};
					uniform  sampler2D _InputTex;
					uniform  sampler2D _ColorTex;
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					bool u_xlatb0;
					vec4 u_xlat1;
					bvec2 u_xlatb1;
					vec2 u_xlat2;
					vec2 u_xlat4;
					bool u_xlatb4;
					void main()
					{
					    u_xlat0 = texture(_InputTex, vs_TEXCOORD0.xy);
					    u_xlat2.xy = u_xlat0.xx * vec2(256.0, 4096.0);
					    u_xlatb4 = u_xlat2.y>=(-u_xlat2.y);
					    u_xlat4.xy = (bool(u_xlatb4)) ? vec2(16.0, 0.0625) : vec2(-16.0, -0.0625);
					    u_xlat2.x = u_xlat4.y * u_xlat2.x;
					    u_xlat2.x = fract(u_xlat2.x);
					    u_xlat0.y = u_xlat2.x * u_xlat4.x;
					    u_xlat0.x = u_xlat0.x * 16.0 + 1.0;
					    u_xlat0.xy = floor(u_xlat0.xy);
					    u_xlat0.x = (-u_xlat0.x) * 0.0625 + 1.0;
					    u_xlat4.xy = vs_TEXCOORD0.xy * _InputTex_TexelSize.zw;
					    u_xlatb1.xy = greaterThanEqual(u_xlat4.xyxx, (-u_xlat4.xyxx)).xy;
					    u_xlat1.x = (u_xlatb1.x) ? float(1.0) : float(-1.0);
					    u_xlat1.y = (u_xlatb1.y) ? float(1.0) : float(-1.0);
					    u_xlat4.xy = u_xlat4.xy * u_xlat1.xy;
					    u_xlat4.xy = fract(u_xlat4.xy);
					    u_xlat4.xy = u_xlat4.xy * u_xlat1.xy;
					    u_xlat4.x = u_xlat4.x * 0.0625;
					    u_xlat1.x = u_xlat0.y * 0.0625 + u_xlat4.x;
					    u_xlat1.y = u_xlat4.y * 0.0625 + u_xlat0.x;
					    u_xlat0 = texture(_MainTex, u_xlat1.xy);
					    u_xlatb0 = 0.00100000005<u_xlat0.w;
					    if(u_xlatb0){
					        u_xlat1 = texture(_ColorTex, u_xlat1.xy);
					        SV_Target0.xyz = u_xlat1.xyz;
					        SV_Target0.w = u_xlat0.w;
					        return;
					    }
					    if((int(0xFFFFFFFFu))!=0){discard;}
					    SV_Target0 = vec4(0.0, 0.0, 0.0, 0.0);
					    return;
					}"
				}
			}
		}
	}
}