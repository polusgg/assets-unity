Shader "Unlit/NoShadowShaderHighlight" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_Mask ("Mask", Float) = 8
		_Outline ("Outline", Float) = 0
		_OutlineColor ("OutlineColor", Vector) = (1,1,1,1)
		_AddColor ("AddColor", Vector) = (0,0,0,0)
	}
	SubShader {
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass {
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			Cull Off
			Stencil {
				Ref 2
				Comp Disabled
				Pass Replace
				Fail Keep
				ZFail Keep
			}
			GpuProgramID 31530
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
						vec4 unused_0_2[4];
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
						float _Outline;
						vec4 _OutlineColor;
						vec4 _AddColor;
					};
					uniform  sampler2D _MainTex;
					in  vec2 vs_TEXCOORD0;
					in  vec4 vs_COLOR0;
					layout(location = 0) out vec4 SV_Target0;
					vec4 u_xlat0;
					vec4 u_xlat1;
					float u_xlat2;
					int u_xlati2;
					vec4 u_xlat3;
					vec4 u_xlat4;
					vec4 u_xlat5;
					vec4 u_xlat6;
					float u_xlat9;
					bool u_xlatb9;
					float u_xlat22;
					void main()
					{
					    u_xlat0 = texture(_MainTex, vs_TEXCOORD0.xy);
					    u_xlat1.z = 0.0;
					    u_xlat22 = u_xlat0.w;
					    for(int u_xlati_loop_1 = 1 ; u_xlati_loop_1<4 ; u_xlati_loop_1++)
					    {
					        u_xlat9 = float(u_xlati_loop_1);
					        u_xlat1.xy = vec2(u_xlat9) * _MainTex_TexelSize.yx;
					        u_xlat3 = u_xlat1.zxyz + vs_TEXCOORD0.xyxy;
					        u_xlat4 = texture(_MainTex, u_xlat3.xy);
					        u_xlat5 = (-u_xlat1.zxyz) + vs_TEXCOORD0.xyxy;
					        u_xlat6 = texture(_MainTex, u_xlat5.xy);
					        u_xlat3 = texture(_MainTex, u_xlat3.zw);
					        u_xlat5 = texture(_MainTex, u_xlat5.zw);
					        u_xlat1.x = u_xlat4.w + u_xlat6.w;
					        u_xlat1.x = u_xlat3.w + u_xlat1.x;
					        u_xlat1.x = u_xlat5.w + u_xlat1.x;
					        u_xlat22 = u_xlat1.x + u_xlat22;
					    }
					    u_xlat1.x = _Outline;
					    u_xlat1.x = clamp(u_xlat1.x, 0.0, 1.0);
					    u_xlat1.x = u_xlat1.x * vs_COLOR0.w;
					    u_xlat1.w = u_xlat1.x * u_xlat22;
					    u_xlat2 = u_xlat0.w + 0.100000001;
					    u_xlat2 = roundEven(u_xlat2);
					    u_xlat1.xyz = _OutlineColor.xyz;
					    u_xlat0 = u_xlat0 * vs_COLOR0 + (-u_xlat1);
					    u_xlat0 = vec4(u_xlat2) * u_xlat0 + u_xlat1;
					    SV_Target0.xyz = _AddColor.xyz * vec3(0.300000012, 0.300000012, 0.300000012) + u_xlat0.xyz;
					    SV_Target0.w = u_xlat0.w;
					    return;
					}"
				}
			}
		}
	}
}