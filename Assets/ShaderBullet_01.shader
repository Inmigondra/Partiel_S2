// Shader created with Shader Forge v1.03 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.03;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:2,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:811,x:33253,y:32629,varname:node_811,prsc:2|emission-3241-OUT,voffset-9185-OUT;n:type:ShaderForge.SFN_ComponentMask,id:8218,x:31992,y:32739,varname:node_8218,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-2138-XYZ;n:type:ShaderForge.SFN_Lerp,id:3241,x:33041,y:32700,varname:node_3241,prsc:2|A-8763-RGB,B-2177-RGB,T-4440-OUT;n:type:ShaderForge.SFN_Color,id:8763,x:32654,y:32475,ptovrint:False,ptlb:node_8763,ptin:_node_8763,varname:node_8763,prsc:2,glob:False,c1:0.2378893,c2:0.342298,c3:0.6470588,c4:1;n:type:ShaderForge.SFN_Color,id:2177,x:32654,y:32644,ptovrint:False,ptlb:node_2177,ptin:_node_2177,varname:node_2177,prsc:2,glob:False,c1:0.6397059,c2:0.07525951,c3:0.5774221,c4:1;n:type:ShaderForge.SFN_Clamp01,id:4440,x:32861,y:32798,varname:node_4440,prsc:2|IN-7186-OUT;n:type:ShaderForge.SFN_Sin,id:3574,x:32606,y:32775,varname:node_3574,prsc:2|IN-4889-OUT;n:type:ShaderForge.SFN_Multiply,id:4889,x:32447,y:32911,varname:node_4889,prsc:2|A-9641-OUT,B-828-OUT,C-2620-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9641,x:32203,y:32676,ptovrint:False,ptlb:node_9641,ptin:_node_9641,varname:node_9641,prsc:2,glob:False,v1:3;n:type:ShaderForge.SFN_RemapRange,id:7186,x:32737,y:32932,varname:node_7186,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-3574-OUT;n:type:ShaderForge.SFN_Tau,id:2620,x:32386,y:33086,varname:node_2620,prsc:2;n:type:ShaderForge.SFN_Add,id:828,x:32222,y:32911,varname:node_828,prsc:2|A-8218-OUT,B-5266-TSL,C-2138-W;n:type:ShaderForge.SFN_Time,id:5266,x:31884,y:33022,varname:node_5266,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9185,x:33096,y:32987,varname:node_9185,prsc:2|A-1931-OUT,B-4440-OUT,C-1323-OUT;n:type:ShaderForge.SFN_NormalVector,id:1931,x:32705,y:33116,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:1323,x:32868,y:33309,ptovrint:False,ptlb:node_1323,ptin:_node_1323,varname:node_1323,prsc:2,glob:False,v1:0.3;n:type:ShaderForge.SFN_FragmentPosition,id:2138,x:31700,y:32765,varname:node_2138,prsc:2;proporder:8763-2177-9641-1323;pass:END;sub:END;*/

Shader "Shader Forge/ShaderBullet_01" {
    Properties {
        _node_8763 ("node_8763", Color) = (0.2378893,0.342298,0.6470588,1)
        _node_2177 ("node_2177", Color) = (0.6397059,0.07525951,0.5774221,1)
        _node_9641 ("node_9641", Float ) = 3
        _node_1323 ("node_1323", Float ) = 0.3
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                float4 unity_DynamicLightmapST;
            #endif
            uniform float4 _node_8763;
            uniform float4 _node_2177;
            uniform float _node_9641;
            uniform float _node_1323;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
                UNITY_FOG_COORDS(3)
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD4;
                #else
                    float3 shLight : TEXCOORD4;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_5266 = _Time + _TimeEditor;
                float3 node_4440 = saturate((sin((_node_9641*(mul(_Object2World, v.vertex).rgb.rgb+node_5266.r+mul(_Object2World, v.vertex).a)*6.28318530718))*0.5+0.5));
                v.vertex.xyz += (v.normal*node_4440*_node_1323);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_5266 = _Time + _TimeEditor;
                float3 node_4440 = saturate((sin((_node_9641*(i.posWorld.rgb.rgb+node_5266.r+i.posWorld.a)*6.28318530718))*0.5+0.5));
                float3 emissive = lerp(_node_8763.rgb,_node_2177.rgb,node_4440);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCollector"
            Tags {
                "LightMode"="ShadowCollector"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCOLLECTOR
            #define SHADOW_COLLECTOR_PASS
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcollector
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                float4 unity_DynamicLightmapST;
            #endif
            uniform float _node_9641;
            uniform float _node_1323;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_COLLECTOR;
                float4 posWorld : TEXCOORD5;
                float3 normalDir : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD8;
                #else
                    float3 shLight : TEXCOORD8;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_5266 = _Time + _TimeEditor;
                float3 node_4440 = saturate((sin((_node_9641*(mul(_Object2World, v.vertex).rgb.rgb+node_5266.r+mul(_Object2World, v.vertex).a)*6.28318530718))*0.5+0.5));
                v.vertex.xyz += (v.normal*node_4440*_node_1323);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_COLLECTOR(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                SHADOW_COLLECTOR_FRAGMENT(i)
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Cull Off
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            // Dithering function, to use with scene UVs (screen pixel coords)
            // 3x3 Bayer matrix, based on https://en.wikipedia.org/wiki/Ordered_dithering
            float BinaryDither3x3( float value, float2 sceneUVs ) {
                float3x3 mtx = float3x3(
                    float3( 3,  7,  4 )/10.0,
                    float3( 6,  1,  9 )/10.0,
                    float3( 2,  8,  5 )/10.0
                );
                float2 px = floor(_ScreenParams.xy * sceneUVs);
                int xSmp = fmod(px.x,3);
                int ySmp = fmod(px.y,3);
                float3 xVec = 1-saturate(abs(float3(0,1,2) - xSmp));
                float3 yVec = 1-saturate(abs(float3(0,1,2) - ySmp));
                float3 pxMult = float3( dot(mtx[0],yVec), dot(mtx[1],yVec), dot(mtx[2],yVec) );
                return round(value + dot(pxMult, xVec));
            }
            uniform float4 _TimeEditor;
            float4 unity_LightmapST;
            #ifdef DYNAMICLIGHTMAP_ON
                float4 unity_DynamicLightmapST;
            #endif
            uniform float _node_9641;
            uniform float _node_1323;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                #ifndef LIGHTMAP_OFF
                    float4 uvLM : TEXCOORD4;
                #else
                    float3 shLight : TEXCOORD4;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = mul(_Object2World, float4(v.normal,0)).xyz;
                float4 node_5266 = _Time + _TimeEditor;
                float3 node_4440 = saturate((sin((_node_9641*(mul(_Object2World, v.vertex).rgb.rgb+node_5266.r+mul(_Object2World, v.vertex).a)*6.28318530718))*0.5+0.5));
                v.vertex.xyz += (v.normal*node_4440*_node_1323);
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.screenPos = o.pos;
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
/////// Vectors:
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
