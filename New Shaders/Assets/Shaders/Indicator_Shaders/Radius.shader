Shader "Custom/Radius"
{
    Properties
    {
       [PerRendererData] _Center("Center", Vector) = (200,0,200,0)
       [PerRendererData] _Center_second("Center_second", Vector) = (200,0,200,0)


        _Radius("Radius", Range(0,10)) = 0.5
        _Emission("Emission", Range(0,10)) = 0
        _RadiusWidth("Radius Width", Range(0,100)) = 0.5
        _RadiusColor("RadiusColor", Color) = (1,1,1,1)
        _RadiusColor_second("RadiusColor_second", Color) = (1,1,1,1)
        _Color("Color", Color) = (1,1,1,1)
        _DiffColor("DiffuseColor", Color) = (1,1,1,1)

        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent"
            "IgnoreProjector" = "True"
            "Queue" = "Transparent" }
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
                float3 worldPos;
            };
            float3 _Center;
            float3 _Center_second;

            float _RadiusWidth;
            float _Radius;
            float _Emission;
            float _Radius_second;

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;
            fixed4 _RadiusColor;
            fixed4 _RadiusColor_second;
            fixed4 _DiffColor;



            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                float d = distance(_Center,IN.worldPos);
                float d2 = distance(_Center_second, IN.worldPos);
                _Radius = 0.5;
                _Radius_second = 0.5;
                if (d < _Radius )
                {
                    o.Albedo = _RadiusColor.rgb;

                }
                else  if (d2 < (_Radius_second ))
                {
                    o.Albedo = _RadiusColor_second.rgb;

                }
                else {
                    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                    o.Albedo = c.rgb;
                    o.Emission = c.rgb * _Emission;
                }

                // Albedo comes from a texture tinted by color

                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
            }
            ENDCG
        }
            FallBack "Diffuse"
}
