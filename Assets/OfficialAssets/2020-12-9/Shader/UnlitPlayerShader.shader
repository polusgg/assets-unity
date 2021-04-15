Shader "Unlit/PlayerShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_BackColor ("Shadow Color", Vector) = (1,0,0,1)
		_BodyColor ("Body Color", Vector) = (1,1,0,1)
		_VisorColor ("VisorColor", Vector) = (0,1,1,1)
		_Outline ("Outline", Range(0, 1)) = 0
		_OutlineColor ("OutlineColor", Vector) = (1,1,1,1)
		_Desaturate ("Desaturate", Range(0, 1)) = 0
	}
	SubShader {
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Cull Off
			GpuProgramID 16931
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
						vec4 unused_0_2[7];
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
						vec4 _MainTex_TexelSize;
						vec4 _BodyColor;
						vec4 _VisorColor;
						vec4 _BackColor;
						float _Outline;
						vec4 _OutlineColor;
						vec4 unused_0_7;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					bool u_xlatb1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec3 u_xlat7;
					bool u_xlatb7;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.x = min(u_xlat0.z, u_xlat0.y);
					    u_xlat1.x = min(u_xlat0.x, u_xlat1.x);
					    u_xlat7.x = max(u_xlat0.z, u_xlat0.y);
					    u_xlat7.x = max(u_xlat0.x, u_xlat7.x);
					    u_xlat1.x = u_xlat1.x / u_xlat7.x;
					    u_xlatb7 = u_xlat7.x<0.00100000005;
					    u_xlat1.x = (-u_xlat1.x) + 1.0;
					    u_xlatb1 = abs(u_xlat1.x)<0.449999988;
					    u_xlatb1 = u_xlatb1 || u_xlatb7;
					    u_xlat7.xyz = u_xlat0.yyy * _VisorColor.xyz;
					    u_xlat7.xyz = _BodyColor.xyz * u_xlat0.xxx + u_xlat7.xyz;
					    u_xlat7.xyz = _BackColor.xyz * u_xlat0.zzz + u_xlat7.xyz;
					    u_xlat7.xyz = clamp(u_xlat7.xyz, 0.0, 1.0);
					    u_xlat0.xyz = (bool(u_xlatb1)) ? u_xlat0.xyz : u_xlat7.xyz;
					    u_xlat1.x = float(0.0);
					    u_xlat1.w = float(0.0);
					    u_xlat1.yz = _MainTex_TexelSize.yx * vec2(2.0, 2.0);
					    u_xlat2 = u_xlat1 + vs_TEXCOORD0.xyxy;
					    u_xlat1 = (-u_xlat1) + vs_TEXCOORD0.xyxy;
					    u_xlat3 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat4 = texture(_MainTex, u_xlat1.xy);
					    u_xlat1 = texture(_MainTex, u_xlat1.zw);
					    u_xlat1.x = u_xlat3.w + u_xlat4.w;
					    u_xlat1.x = u_xlat2.w + u_xlat1.x;
					    u_xlat1.x = u_xlat1.w + u_xlat1.x;
					    u_xlat2.x = float(0.0);
					    u_xlat2.w = float(0.0);
					    u_xlat2.yz = _MainTex_TexelSize.yx;
					    u_xlat3 = u_xlat2 + vs_TEXCOORD0.xyxy;
					    u_xlat2 = (-u_xlat2) + vs_TEXCOORD0.xyxy;
					    u_xlat4 = texture(_MainTex, u_xlat3.xy);
					    u_xlat3 = texture(_MainTex, u_xlat3.zw);
					    u_xlat5 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat7.x = u_xlat4.w + u_xlat5.w;
					    u_xlat7.x = u_xlat3.w + u_xlat7.x;
					    u_xlat7.x = u_xlat2.w + u_xlat7.x;
					    u_xlat7.x = u_xlat0.w + u_xlat7.x;
					    u_xlat1.x = u_xlat1.x + u_xlat7.x;
					    u_xlat2.x = float(0.0);
					    u_xlat2.w = float(0.0);
					    u_xlat2.yz = _MainTex_TexelSize.yx * vec2(3.0, 3.0);
					    u_xlat3 = u_xlat2 + vs_TEXCOORD0.xyxy;
					    u_xlat2 = (-u_xlat2) + vs_TEXCOORD0.xyxy;
					    u_xlat4 = texture(_MainTex, u_xlat3.xy);
					    u_xlat3 = texture(_MainTex, u_xlat3.zw);
					    u_xlat5 = texture(_MainTex, u_xlat2.xy);
					    u_xlat2 = texture(_MainTex, u_xlat2.zw);
					    u_xlat7.x = u_xlat4.w + u_xlat5.w;
					    u_xlat7.x = u_xlat3.w + u_xlat7.x;
					    u_xlat7.x = u_xlat2.w + u_xlat7.x;
					    u_xlat1.x = u_xlat7.x + u_xlat1.x;
					    u_xlat7.x = _Outline;
					    u_xlat7.x = clamp(u_xlat7.x, 0.0, 1.0);
					    u_xlat1.w = u_xlat7.x * u_xlat1.x;
					    u_xlat1.xyz = _OutlineColor.xyz;
					    u_xlat2.x = u_xlat0.w + 0.0500000007;
					    u_xlat2.x = roundEven(u_xlat2.x);
					    u_xlat0 = u_xlat0 + (-u_xlat1);
					    u_xlat0 = u_xlat2.xxxx * u_xlat0 + u_xlat1;
					    SV_Target0 = u_xlat0 * vs_COLOR0;
					    return;
					}"
				}
			}
		}
	}
}