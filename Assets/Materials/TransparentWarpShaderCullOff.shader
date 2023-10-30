Shader "Custom/TransparentWarpShaderCullOff"
{
    Properties {
        _Color ("Color", Color) = (1.000000,1.000000,1.000000,1.000000)
        _MainTex ("Albedo", 2D) = "white" { }

        _Glossiness ("Smoothness", Range(0.000000,1.000000)) = 0.500000
         [NoScaleOffset]_MetallicGlossMap ("Metallic", 2D) = "black" { }

        [Normal] _BumpMap ("Normal Map", 2D) = "bump" { }
        [NoScaleOffset]_OcclusionMap ("Occlusion", 2D) = "white" { }
        _EmissionColor ("Color", Color) = (0.000000,0.000000,0.000000,1.000000)
        [NoScaleOffset]_EmissionMap ("Emission", 2D) = "white" { }

        //_Amount ("Warp Amount", Range(0.000000,1.000000)) = 0.0
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Cull Off
        LOD 200 
        CGPROGRAM

        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows 
        #pragma vertex vert alpha:fade


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

        float _warpVal;
        void vert (inout appdata_full v)
        {
          
          float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
          float dist = length(_WorldSpaceCameraPos.xyz - worldPos.xyz);

          worldPos.y -= _warpVal*(dist*dist)/5000;
          worldPos.x += _warpVal*sin(dist/100)*(dist*dist/2000);


          v.vertex = mul(unity_WorldToObject, worldPos);

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
              o.Emission = _EmissionColor*tex2D(_OcclusionMap, IN.uv_MainTex);
              o.Smoothness = _Glossiness;
              o.Albedo = _Color*tex2D (_MainTex, IN.uv_MainTex).rgb;
              o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
              o.Metallic = tex2D(_MetallicGlossMap, IN.uv_MainTex);
              o.Occlusion = tex2D(_OcclusionMap, IN.uv_MainTex);
        }
        ENDCG
    }
    FallBack "Diffuse"
}



