Shader "Custom/Color"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _Diffuse_Color("Diffuse_Color", Color) = (1,1,1,1)
        _Diffuse_power("Diffuse strength",Range(0,10)) = 1
        _Diffuse_extent("Diffuse extent",Range(1,3)) = 1
        _Emission_power("Emission strength",Range(0.1,50)) = 1
        _Occlusion_power("Occlusion strength",Range(0.1,50)) = 1
        _Scroll_Speed_X("X scroll speed",Range(0,10)) = 1
        _Scroll_Speed_Y("Y scroll speed",Range(0,10)) = 1



        _MainTex ("Albedo (RGB)", 2D) = "white" {}
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
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _Diffuse_Color;
        float _Diffuse_power;
        float _Diffuse_extent;
        float _Emission_power;
        float  _Occlusion_power;
        float  _Scroll_Speed_X;
        float  _Scroll_Speed_Y;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed2 scrollUV = IN.uv_MainTex;
            float x_scroll = _Scroll_Speed_X * _Time;
            float y_scroll = _Scroll_Speed_Y * _Time;
            scrollUV += float2(x_scroll, y_scroll);
            fixed4 c = tex2D(_MainTex, scrollUV)*(_Color/_Diffuse_extent+(_Diffuse_Color*_Diffuse_power));
            o.Albedo = c.rgb;
            o.Emission=c.rgb/_Emission_power;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            o.Occlusion = _Occlusion_power;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
