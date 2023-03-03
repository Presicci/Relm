Shader "UI/HealthBar" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _Fill ("Fill", float) = 0
    }
    SubShader {
        Tags { "Queue"="Overlay" }
        LOD 100

        Pass {
            ZTest Off

            CGPROGRAM
            // Use "vert" function as a vertex shader
            #pragma vertex vert
            // Use "frag" function as a pixel (fragment) shader
            #pragma fragment frag

            #pragma multi_compile_instancing


            #include "UnityCG.cginc"

            // Vertex shader inputs
            struct appdata {
                float4 vertex : POSITION;   // Vertex position
                float2 uv : TEXCOORD0;      // Texture coordinate
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            // Vertex shader outputs ("vertex to fragment")
            struct v2f {
                float2 uv : TEXCOORD0;          // Texture coordinate
                float4 vertex : SV_POSITION;    // Clip space position
            };

            // Texture we will sample
            sampler2D _MainTex;
            float4 _MainTex_ST;

            // Instancing
            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(float, _Fill)
            UNITY_INSTANCING_BUFFER_END(Props)

            // Vertex shader
            v2f vert (appdata v) {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);

                float fill = UNITY_ACCESS_INSTANCED_PROP(Props, _Fill);

                o.vertex = UnityObjectToClipPos(v.vertex);

                // generate UVs from fill level (assumed texture is clamped)
                o.uv = v.uv;
                o.uv.x += 0.5 - fill;
                return o;
            }

            // Pixel shader; returns low precision ("fixed4" type) color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target {
                // Sample texture and return it
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}