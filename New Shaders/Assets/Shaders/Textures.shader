Shader "Custom/Textures"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _RimColor ("RimColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _MainBump("Normal", 2D) = "bump" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _slider("Slider",Range(0,10)) = 1
        _rim_slider("RimSlider",Range(0,10)) = 1

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _MainBump;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_MainBump;
            float3 worldRefl; INTERNAL_DATA
            float3 viewerDir;
        };

        half _Glossiness;
        half _Metallic;
        half _slider;
        half _rim_slider;
        fixed4 _Color;
        fixed4 _RimColor;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Normal = UnpackNormal(tex2D(_MainBump, IN.uv_MainBump));
            half dots = _rim_slider*saturate(dot(normalize(IN.viewerDir), o.Normal));
            o.Emission = _RimColor.rgb * dots;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            o.Normal *= float3(_slider, _slider, 1);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
