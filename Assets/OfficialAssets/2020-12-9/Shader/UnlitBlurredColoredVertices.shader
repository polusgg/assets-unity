Shader "Unlit/BlurredColoredVertices" {
	Properties {
		_Size ("Size", Range(0, 20)) = 1
	}
	SubShader {
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			Stencil {
				Ref 1
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 38243
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
					in  vec4 in_COLOR0;
					out vec4 vs_COLOR0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
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
					
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					void main()
					{
					    SV_Target0 = vs_COLOR0;
					    return;
					}"
				}
			}
		}
		GrabPass {
		}
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			GpuProgramID 128004
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
					out vec4 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat0.xy = u_xlat0.xy * vec2(1.0, -1.0) + u_xlat0.ww;
					    vs_TEXCOORD0.zw = u_xlat0.zw;
					    vs_TEXCOORD0.xy = u_xlat0.xy * vec2(0.5, 0.5);
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
						vec4 unused_0_0[2];
						vec4 _GrabTexture_TexelSize;
						float _Size;
					};
					uniform  sampler2D _GrabTexture;
					in  vec4 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat6;
					void main()
					{
					    u_xlat0.yw = vs_TEXCOORD0.yy;
					    u_xlat1.x = _GrabTexture_TexelSize.x * _Size;
					    u_xlat2 = u_xlat1.xxxx * vec4(3.0, -4.0, -3.0, -2.0) + vs_TEXCOORD0.xxxx;
					    u_xlat0.xz = u_xlat2.yz;
					    u_xlat0 = u_xlat0 / vs_TEXCOORD0.wwww;
					    u_xlat3 = texture(_GrabTexture, u_xlat0.zw);
					    u_xlat0 = texture(_GrabTexture, u_xlat0.xy);
					    u_xlat3 = u_xlat3 * vec4(0.0900000036, 0.0900000036, 0.0900000036, 0.0900000036);
					    u_xlat0 = u_xlat0 * vec4(0.0500000007, 0.0500000007, 0.0500000007, 0.0500000007) + u_xlat3;
					    u_xlat3.x = u_xlat2.w;
					    u_xlat3.yw = vs_TEXCOORD0.yy;
					    u_xlat6.xy = u_xlat3.xy / vs_TEXCOORD0.ww;
					    u_xlat4 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat4 * vec4(0.119999997, 0.119999997, 0.119999997, 0.119999997) + u_xlat0;
					    u_xlat3.z = (-_GrabTexture_TexelSize.x) * _Size + vs_TEXCOORD0.x;
					    u_xlat6.xy = u_xlat3.zw / vs_TEXCOORD0.ww;
					    u_xlat3 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat3 * vec4(0.150000006, 0.150000006, 0.150000006, 0.150000006) + u_xlat0;
					    u_xlat6.xy = vs_TEXCOORD0.xy / vs_TEXCOORD0.ww;
					    u_xlat3 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat3 * vec4(0.180000007, 0.180000007, 0.180000007, 0.180000007) + u_xlat0;
					    u_xlat3.x = _GrabTexture_TexelSize.x * _Size + vs_TEXCOORD0.x;
					    u_xlat3.yw = vs_TEXCOORD0.yy;
					    u_xlat6.xy = u_xlat3.xy / vs_TEXCOORD0.ww;
					    u_xlat4 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat4 * vec4(0.150000006, 0.150000006, 0.150000006, 0.150000006) + u_xlat0;
					    u_xlat3.z = u_xlat1.x * 2.0 + vs_TEXCOORD0.x;
					    u_xlat2.z = u_xlat1.x * 4.0 + vs_TEXCOORD0.x;
					    u_xlat1.xy = u_xlat3.zw / vs_TEXCOORD0.ww;
					    u_xlat1 = texture(_GrabTexture, u_xlat1.xy);
					    u_xlat0 = u_xlat1 * vec4(0.119999997, 0.119999997, 0.119999997, 0.119999997) + u_xlat0;
					    u_xlat2.yw = vs_TEXCOORD0.yy;
					    u_xlat1 = u_xlat2 / vs_TEXCOORD0.wwww;
					    u_xlat2 = texture(_GrabTexture, u_xlat1.zw);
					    u_xlat1 = texture(_GrabTexture, u_xlat1.xy);
					    u_xlat0 = u_xlat1 * vec4(0.0900000036, 0.0900000036, 0.0900000036, 0.0900000036) + u_xlat0;
					    SV_Target0 = u_xlat2 * vec4(0.0500000007, 0.0500000007, 0.0500000007, 0.0500000007) + u_xlat0;
					    return;
					}"
				}
			}
		}
		GrabPass {
		}
		Pass {
			Tags { "LIGHTMODE" = "ALWAYS" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			GpuProgramID 147908
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
					out vec4 vs_TEXCOORD0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					void main()
					{
					    u_xlat0 = in_POSITION0.yyyy * unity_ObjectToWorld[1];
					    u_xlat0 = unity_ObjectToWorld[0] * in_POSITION0.xxxx + u_xlat0;
					    u_xlat0 = unity_ObjectToWorld[2] * in_POSITION0.zzzz + u_xlat0;
					    u_xlat0 = u_xlat0 + unity_ObjectToWorld[3];
					    u_xlat1 = u_xlat0.yyyy * unity_MatrixVP[1];
					    u_xlat1 = unity_MatrixVP[0] * u_xlat0.xxxx + u_xlat1;
					    u_xlat1 = unity_MatrixVP[2] * u_xlat0.zzzz + u_xlat1;
					    u_xlat0 = unity_MatrixVP[3] * u_xlat0.wwww + u_xlat1;
					    gl_Position = u_xlat0;
					    u_xlat0.xy = u_xlat0.xy * vec2(1.0, -1.0) + u_xlat0.ww;
					    vs_TEXCOORD0.zw = u_xlat0.zw;
					    vs_TEXCOORD0.xy = u_xlat0.xy * vec2(0.5, 0.5);
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
						vec4 unused_0_0[2];
						vec4 _GrabTexture_TexelSize;
						float _Size;
					};
					uniform  sampler2D _GrabTexture;
					in  vec4 vs_TEXCOORD0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					vec4 u_xlat2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec2 u_xlat6;
					void main()
					{
					    u_xlat0.xz = vs_TEXCOORD0.xx;
					    u_xlat1.x = _GrabTexture_TexelSize.y * _Size;
					    u_xlat2 = u_xlat1.xxxx * vec4(-4.0, 3.0, -3.0, -2.0) + vs_TEXCOORD0.yyyy;
					    u_xlat0.yw = u_xlat2.xz;
					    u_xlat0 = u_xlat0 / vs_TEXCOORD0.wwww;
					    u_xlat3 = texture(_GrabTexture, u_xlat0.zw);
					    u_xlat0 = texture(_GrabTexture, u_xlat0.xy);
					    u_xlat3 = u_xlat3 * vec4(0.0900000036, 0.0900000036, 0.0900000036, 0.0900000036);
					    u_xlat0 = u_xlat0 * vec4(0.0500000007, 0.0500000007, 0.0500000007, 0.0500000007) + u_xlat3;
					    u_xlat3.y = u_xlat2.w;
					    u_xlat3.xz = vs_TEXCOORD0.xx;
					    u_xlat6.xy = u_xlat3.xy / vs_TEXCOORD0.ww;
					    u_xlat4 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat4 * vec4(0.119999997, 0.119999997, 0.119999997, 0.119999997) + u_xlat0;
					    u_xlat3.w = (-_GrabTexture_TexelSize.y) * _Size + vs_TEXCOORD0.y;
					    u_xlat6.xy = u_xlat3.zw / vs_TEXCOORD0.ww;
					    u_xlat3 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat3 * vec4(0.150000006, 0.150000006, 0.150000006, 0.150000006) + u_xlat0;
					    u_xlat6.xy = vs_TEXCOORD0.xy / vs_TEXCOORD0.ww;
					    u_xlat3 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat3 * vec4(0.180000007, 0.180000007, 0.180000007, 0.180000007) + u_xlat0;
					    u_xlat3.y = _GrabTexture_TexelSize.y * _Size + vs_TEXCOORD0.y;
					    u_xlat3.xz = vs_TEXCOORD0.xx;
					    u_xlat6.xy = u_xlat3.xy / vs_TEXCOORD0.ww;
					    u_xlat4 = texture(_GrabTexture, u_xlat6.xy);
					    u_xlat0 = u_xlat4 * vec4(0.150000006, 0.150000006, 0.150000006, 0.150000006) + u_xlat0;
					    u_xlat3.w = u_xlat1.x * 2.0 + vs_TEXCOORD0.y;
					    u_xlat2.w = u_xlat1.x * 4.0 + vs_TEXCOORD0.y;
					    u_xlat1.xy = u_xlat3.zw / vs_TEXCOORD0.ww;
					    u_xlat1 = texture(_GrabTexture, u_xlat1.xy);
					    u_xlat0 = u_xlat1 * vec4(0.119999997, 0.119999997, 0.119999997, 0.119999997) + u_xlat0;
					    u_xlat2.xz = vs_TEXCOORD0.xx;
					    u_xlat1 = u_xlat2 / vs_TEXCOORD0.wwww;
					    u_xlat2 = texture(_GrabTexture, u_xlat1.zw);
					    u_xlat1 = texture(_GrabTexture, u_xlat1.xy);
					    u_xlat0 = u_xlat1 * vec4(0.0900000036, 0.0900000036, 0.0900000036, 0.0900000036) + u_xlat0;
					    SV_Target0 = u_xlat2 * vec4(0.0500000007, 0.0500000007, 0.0500000007, 0.0500000007) + u_xlat0;
					    return;
					}"
				}
			}
		}
	}
}