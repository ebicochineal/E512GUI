Shader "E512GUI/U" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Alpha ("Alpha", float) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        ZTest Always
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2fo {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
            struct v2fi {
                float2 uv : TEXCOORD0;
                UNITY_VPOS_TYPE vpos : VPOS;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;
            fixed _Alpha;
            
            float _WL;
            float _WU;
            float _WR;
            float _WD;
            int _Clip;
            
            v2fo vert (appdata v) {
                v2fo o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2fi i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                if (col.a < 0.1) { discard; }
                col.rgb *= _Color.rgb;
                col.a = _Alpha;
                
                #if UNITY_UV_STARTS_AT_TOP
                    col.rgb *= fixed4(1, 0.5, 0.5, 1);
                #else
                    col.rgb *= fixed4(0.5, 0.5, 1, 1);
                #endif
                
                return col;
            }
            ENDCG
        }
    }
}
