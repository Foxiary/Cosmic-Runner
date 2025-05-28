Shader "Custom/UIOutlineGlow"
{
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1) // Màu chính của UI
        _OutlineColor ("Outline Color", Color) = (1,1,1,1) // Màu outline
        _Outline ("Outline Width", Range(0.0, 0.1)) = 0.05 // Độ dày outline
        _GlowColor ("Glow Color", Color) = (1, 1, 0, 1) // Màu phát sáng
        _GlowIntensity ("Glow Intensity", Range(0.0, 1.0)) = 0.5 // Độ mạnh của phát sáng
    }
    SubShader {
        Tags { "Queue"="Overlay" "IgnoreProjector"="True" "RenderType"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            Name "OUTLINE"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _OutlineColor;
            float _Outline;
            float4 _GlowColor;
            float _GlowIntensity;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Lấy màu từ texture và áp dụng màu chính
                fixed4 mainColor = tex2D(_MainTex, i.uv) * _Color;

                // Áp dụng outline bằng cách pha màu outline vào các cạnh
                fixed4 outline = _OutlineColor * (1.0 - smoothstep(0.0, _Outline, length(fwidth(i.uv))));

                // Áp dụng hiệu ứng phát sáng
                fixed4 glow = _GlowColor * _GlowIntensity;

                // Kết hợp màu chính, outline và glow
                return mainColor + outline + glow;
            }
            ENDCG
        }
    }
    FallBack "UI/Default"
}
