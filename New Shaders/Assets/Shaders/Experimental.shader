Shader "Custom/Experimental"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Emission("Emission", Color) = (1,1,1,1)
        _Normal("Normal", Color) = (1,1,1,1)
        _Tex("texture", 2D) = "white"{}
        _bump("bump", 2D) = "bump"{}

        

    }
    SubShader
    {

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting


        struct Input
        {
            float2 uv_MainTex;
            float2 uv_myBump;
        };

        fixed4 _Color;
        fixed4 _Emission;
        fixed4 _Normal;
        sampler2D _Tex;
        sampler2D _bump;



        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
            // put more per-instance properties here
        void lul(Input IN,inout SurfaceOutputStandard o) {  
           
            fixed4 n = _Normal;    
            fixed4 c = _Color;    
            
            o.Albedo = (tex2D(_Tex, IN.uv_MainTex)).rgba;        
            o.Normal = (tex2D(_bump, IN.uv_myBump));
            // Metallic and smoothness come from slider variables
        }
        void surf (Input IN, inout SurfaceOutputStandard o)
        {  
            lul(IN,o);
        }

      
        ENDCG
    }
    FallBack "Diffuse"
}
