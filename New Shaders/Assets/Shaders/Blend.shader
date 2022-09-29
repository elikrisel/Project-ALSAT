Shader "Custom/Blend"
{
    Properties
    {
        _ColorA("ColorA", Color) = (1,1,1,1)
        _ColorB("ColorB", Color) = (1,1,1,1)
        _ColorC("ColorC", Color) = (1,1,1,1)
        _ColorD("ColorD", Color) = (1,1,1,1)

        _MainTexA("Albedo (RGBA)", 2D) = "" {}
        _MainTexB("Albedo (RGBB)", 2D) = "" {}
        _MainTexC("Albedo (RGBC)", 2D) = "" {}
        _MainTexD ("Albedo (RGBD)", 2D) = "" {}  
        _MainTexE("Albedo (RGBD)", 2D) = "" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.5

        sampler2D _MainTexA;
        sampler2D _MainTexB;
        sampler2D _MainTexC;
        sampler2D _MainTexD;
        sampler2D _MainTexE;

        struct Input
        {
            float2 uv_MainTexA;
            float2 uv_MainTexB;
            float2 uv_MainTexC;
            float2 uv_MainTexD;
            float2 uv_MainTexE;


        };

        half _Glossiness;
        half _Metallic;
        fixed4 _ColorA;
        fixed4 _ColorB;
        fixed4 _ColorC;
        fixed4 _ColorD;


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTexA, IN.uv_MainTexA) * _ColorA;
            float4 AData = tex2D(_MainTexA, IN.uv_MainTexD);
            //Get the data from the textures we want to blend
            float4 BTexData = tex2D(_MainTexB, IN.uv_MainTexB);
            float4 CTexData = tex2D(_MainTexC, IN.uv_MainTexC);
            float4 DTexData = tex2D(_MainTexD, IN.uv_MainTexD);
            float4 ETexData = tex2D(_MainTexE, IN.uv_MainTexE);

            float4 finalColor;
            finalColor = lerp(BTexData, CTexData, DTexData.g);
            finalColor = lerp(BTexData, CTexData, DTexData.b);
            finalColor = lerp(BTexData, CTexData, DTexData.a);

            o.Albedo = finalColor.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
