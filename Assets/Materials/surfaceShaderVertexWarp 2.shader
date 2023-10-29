Shader "Custom/carSurfaceShaderVertexWarp"
{
    Properties {
        _Color ("Color", Color) = (1.000000,1.000000,1.000000,1.000000)
        _MainTex ("Albedo", 2D) = "white" { }
        //_Cutoff ("Alpha Cutoff", Range(0.000000,1.000000)) = 0.500000
        _Glossiness ("Smoothness", Range(0.000000,1.000000)) = 0.500000
        //_GlossMapScale ("Smoothness Scale", Range(0.000000,1.000000)) = 1.000000
        //[Enum(Metallic Alpha,0,Albedo Alpha,1)]  _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0.000000
        //[Gamma]  _Metallic ("Metallic", Range(0.000000,1.000000)) = 0.000000
         [NoScaleOffset]_MetallicGlossMap ("Metallic", 2D) = "black" { }
        //[ToggleOff]  _SpecularHighlights ("Specular Highlights", Float) = 1.000000
        //[ToggleOff]  _GlossyReflections ("Glossy Reflections", Float) = 1.000000
        //_BumpScale ("Scale", Float) = 1.000000
        [Normal] _BumpMap ("Normal Map", 2D) = "bump" { }
        // _Parallax ("Height Scale", Range(0.005000,0.080000)) = 0.020000
        // _ParallaxMap ("Height Map", 2D) = "black" { }
        [NoScaleOffset]_OcclusionMap ("Occlusion", 2D) = "white" { }
        _EmissionColor ("Color", Color) = (0.000000,0.000000,0.000000,1.000000)
        [NoScaleOffset]_EmissionMap ("Emission", 2D) = "white" { }

        //_Amount ("Warp Amount", Range(0.000000,1.000000)) = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200 
        CGPROGRAM

        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #pragma vertex vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        struct Input 
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 worldPos;
            UNITY_FOG_COORDS(1)
        };

        /*struct v2f {
            float4 tangent : TANGENT;
            //float4 vertex : POSITION;
            float4 pos : SV_POSITION;
            float3 normal : NORMAL;
            float3 texcoord : TEXCOORD0;
        };*/


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v)
        {
          
          /*float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
          float dist = length(_WorldSpaceCameraPos.xyz - worldPos.xyz);

          worldPos.y -= (dist*dist)/5000;

          v.vertex = mul(unity_WorldToObject, worldPos);
          */
        }

        void RGB_to_HSV(float3 In, out float3 Out)
        {
            float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
            float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
            float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
            float D = Q.x - min(Q.w, Q.y);
            float  E = 1e-10;
            Out = float3(abs(Q.z + (Q.w - Q.y)/(6.0 * D + E)), D / (Q.x + E), Q.x);
        }

        void HSV_to_RGB(float3 In, out float3 Out)
        {
            float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
            float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
            Out = In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
        }


        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _MetallicGlossMap;
        sampler2D _OcclusionMap;
        sampler2D _EmissionMap;
        float _Glossiness;
        fixed4 _EmissionColor;
        fixed4 _Color;
        void surf (Input IN, inout SurfaceOutputStandard o) {
              float3 baseWorldPos = unity_ObjectToWorld._m03_m13_m23;

              float3 texCol = tex2D (_MainTex, IN.uv_MainTex).rgb;
              float mask = step(0.1, texCol);
              float3 randCol = float3((baseWorldPos.z % 2.7f)/2.7f, 0.3f, 0.7f);
              HSV_to_RGB(randCol, randCol);


              o.Albedo = mask*texCol + (1.0f-mask)*randCol;
              o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
              o.Metallic = tex2D(_MetallicGlossMap, IN.uv_MainTex);
              o.Occlusion = tex2D(_OcclusionMap, IN.uv_MainTex);
              o.Emission = _EmissionColor*tex2D(_OcclusionMap, IN.uv_MainTex);
              o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}



