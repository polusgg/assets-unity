Shader "Unlit/ReactorPulseShader" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_Speed ("Speed", Float) = 1
	}
	SubShader {
		LOD 100
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			LOD 100
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			GpuProgramID 6879
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
					out vec2 vs_TEXCOORD0;
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
						vec4 _Color;
						float _Speed;
					};
					layout(std140) uniform UnityPerCamera {
						vec4 _Time;
						vec4 unused_1_1[8];
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					float u_xlat5;
					vec2 u_xlat6;
					vec2 u_xlat9;
					float u_xlat13;
					void main()
					{
					    u_xlat0.xyz = _Color.xyz;
					    u_xlat1.xy = vs_TEXCOORD0.xy * vec2(2.0, 2.0) + vec2(-1.0, -1.0);
					    u_xlat1.x = dot(u_xlat1.xy, u_xlat1.xy);
					    u_xlat1.x = sqrt(u_xlat1.x);
					    u_xlat5 = _Speed * _Time.w;
					    u_xlat5 = u_xlat5 * 1.25;
					    u_xlat5 = sin(u_xlat5);
					    u_xlat2 = vec4(u_xlat5) * vec4(0.5, 0.100000001, 0.200000003, 0.200000003) + vec4(0.5, -0.0500000007, -0.100000001, 0.5);
					    u_xlat1.y = u_xlat5 * 0.100000001 + 0.800000012;
					    u_xlat1.x = u_xlat1.x + u_xlat2.y;
					    u_xlat9.x = u_xlat1.x + -0.0199999996;
					    u_xlat13 = (-u_xlat2.y) + u_xlat2.z;
					    u_xlat6.xy = vec2(u_xlat13) + vec2(0.25, 0.230000004);
					    u_xlat9.x = u_xlat9.x / u_xlat6.y;
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat3.x = float(-1.0);
					    u_xlat3.y = float(-1.0);
					    u_xlat3.z = float(-1.0);
					    u_xlat3.w = u_xlat2.x * -0.350000024;
					    u_xlat3 = u_xlat3 + vec4(1.0, 0.800000012, 0.0, 0.0);
					    u_xlat3 = u_xlat9.xxxx * u_xlat3 + vec4(1.0, 1.0, 1.0, 1.0);
					    u_xlat0.w = u_xlat2.x * -0.350000024 + 1.0;
					    u_xlat0 = u_xlat0 + (-u_xlat3);
					    u_xlat9.x = (-u_xlat6.x) + u_xlat2.w;
					    u_xlat13 = u_xlat1.x + (-u_xlat6.x);
					    u_xlat9.x = u_xlat13 / u_xlat9.x;
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0 = u_xlat9.xxxx * u_xlat0 + u_xlat3;
					    u_xlat3 = (-u_xlat0) + vec4(0.25, 0.230000004, 0.5, 0.5);
					    u_xlat9.xy = (-u_xlat2.ww) + u_xlat1.xy;
					    u_xlat9.x = u_xlat9.x / u_xlat9.y;
					    u_xlat9.x = clamp(u_xlat9.x, 0.0, 1.0);
					    u_xlat0 = u_xlat9.xxxx * u_xlat3 + u_xlat0;
					    u_xlat1.x = (-u_xlat1.y) + u_xlat1.x;
					    u_xlat5 = (-u_xlat1.y) + 0.949999988;
					    u_xlat5 = u_xlat1.x / u_xlat5;
					    u_xlat5 = clamp(u_xlat5, 0.0, 1.0);
					    u_xlat1.x = u_xlat1.x * 1000000.0;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat2.xyz = _Color.xyz;
					    u_xlat2.w = 0.5;
					    u_xlat2 = (-u_xlat0) + u_xlat2;
					    u_xlat0 = u_xlat1.xxxx * u_xlat2 + u_xlat0;
					    u_xlat2.xyz = _Color.xyz;
					    u_xlat2.w = 0.0;
					    u_xlat2 = (-u_xlat0) + u_xlat2;
					    u_xlat0 = vec4(u_xlat5) * u_xlat2 + u_xlat0;
					    u_xlat1 = texture(_MainTex, vs_TEXCOORD0.xy);
					    SV_Target0 = u_xlat0 * u_xlat1;
					    return;
					}"
				}
			}
		}
	}
}