Shader "E512GUI/UnlitTextureColor" {
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
            #pragma target 3.0
            // make fog work
            // #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2fo {
                float2 uv : TEXCOORD0;
                // UNITY_FOG_COORDS(1)
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
            float _MX;// cursor
            float _MY;// cursor
            
            v2fo vert (appdata v) {
                v2fo o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2fi i) : SV_Target {
                if (_Clip > 0) {
                    i.vpos.xy /= _ScreenParams.xy;
                    #if UNITY_UV_STARTS_AT_TOP
                        if (_ProjectionParams.x < 0) { i.vpos.y = 1.0-i.vpos.y; }
                    #else
                        i.vpos.y = 1.0-i.vpos.y;
                    #endif
                    if (i.vpos.x < _WL) { discard; }
                    if (i.vpos.x >= _WR) { discard; }
                    if (i.vpos.y < _WU) { discard; }
                    if (i.vpos.y >= _WD) { discard; }
                }
                
                fixed4 col = tex2D(_MainTex, i.uv);
                if (col.a < 0.1) { discard; }
                col.rgb *= _Color.rgb;
                col.a = _Alpha;
                return col;
            }
            ENDCG
        }
    }
}
