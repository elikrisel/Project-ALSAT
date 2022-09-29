Shader "Custom/Hollow"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _SelectColor("SelectColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        _fade_val("Fade value",Range(0,1)) = 1
        _DotProduct("Rim effect", Range(-1,1)) = 0.25
        _Depth("Depth", Range(-5,5)) = 0.25

    }
    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "IgnoreProjector"="False"
            "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert alpha:fade nolighting

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        float _fade_val;
        float _DotProduct;
        float _Depth;
        fixed4 _SelectColor;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 c2 = _SelectColor;
            // Metallic and smoothness come from slider variables
            
            float border = 1-(abs(dot(IN.worldNormal, IN.viewDir)));
            if (border >_Depth) {
                o.Albedo = c2.rgb*_DotProduct;
                o.Alpha = border * _DotProduct;

            }
            else {
                o.Albedo = c.rgb;
                o.Alpha = border;

            }



        }
        ENDCG
    }
    FallBack "Diffuse"
}
