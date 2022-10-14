Shader "E512GUI/UnlitTextureHexT" {
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
            float _MX;
            float _MY;
            
            
            v2fo vert (appdata v) {
                v2fo o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            float rand (float2 co) { return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453); }
            
            float2 hex (float2 uv, float scale) {
                uv *= scale;
                float3 col = float3(0, 0, 0);
                float2 r = normalize(float2(1.0, 1.73));
                float2 h = r * 0.5;
                float2 a = fmod(uv, r) - h;
                float2 b = fmod(uv - h, r) - h;
                float2 gv = length(a) < length(b) ? a : b;
                float2 id = uv - gv;
                // col.rg = id * 0.2;
                col.rg = id / scale;
                return col.rg;
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
                
                float x = i.vpos.x;
                float y = i.vpos.y;
                x *= _ScreenParams.x / _ScreenParams.y;
                
                if (col.r == 1 && col.g == 1 && col.b == 1) {
                    float2 u = hex(float2(x, y), 32);
                    col.rgb = _Color;
                    if (col.a > 0) {
                        float a = sin(rand(u - 1000) * 32 + _Time.x * 32) * 0.5 + 0.5;
                        a *= (i.vpos.y - _WU) / (_WD-_WU);
                        float2 mu = hex(float2(_MX / _ScreenParams.x * (_ScreenParams.x/_ScreenParams.y), _MY/_ScreenParams.y), 32);
                        float d = distance(mu, u);
                        float b = d < 0.2 ? 1-d*2 : 0.6;
                        col.rgb *= min(a*b+(b-0.5), 1);
                        col.a = min(a*b+(b-0.5), 1);
                    }
                } else {
                    col.rgb = fixed3(1, 1, 1) * _Color.r;
                }
                return col;
            }
            
            ENDCG
        }
    }
}
