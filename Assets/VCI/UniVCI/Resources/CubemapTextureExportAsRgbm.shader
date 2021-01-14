Shader "Hidden/UniVCI/CubemapConversion/ExportAsRgbm"
{
    Properties
    {
        _MainTex ("Cube", Cube) = "white" {}
        _FaceIndex ("Face Index", int) = 0
        _MipLevel ("Mip Level", int) = 0
    }
    SubShader
    {
		Cull Back
        ZWrite Off
        ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldNormal : TEXCOORD1;
            };

            UNITY_DECLARE_TEXCUBE(_MainTex);
            int _FaceIndex;
            int _MipLevel;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            #include "UnityPBSLighting.cginc"
            #include "./TextureIoUtils.cginc"

            float4 frag (v2f i) : SV_Target
            {
                static const float3 x_axis = float3(1, 0, 0);
                static const float3 y_axis = float3(0, 1, 0);
                static const float3 z_axis = float3(0, 0, 1);
                
                float3 axis_0 = float3(0, 0, -1);
                float3 axis_1 = float3(0, +1, 0);
                float3 axis_2 = float3(+1, 0, 0);
                if (_FaceIndex == 0) // +X
                {
                    axis_0 = -z_axis;
                    axis_1 = +y_axis;
                    axis_2 = +x_axis;
                }
                else if (_FaceIndex == 1) // -X
                {
                    axis_0 = +z_axis;
                    axis_1 = +y_axis;
                    axis_2 = -x_axis;
                }
                else if (_FaceIndex == 2) // +Y
                {
                    axis_0 = +x_axis;
                    axis_1 = -z_axis;
                    axis_2 = +y_axis;
                }
                else if (_FaceIndex == 3) // -Y
                {
                    axis_0 = +x_axis;
                    axis_1 = +z_axis;
                    axis_2 = -y_axis;
                }
                else if (_FaceIndex == 4) // +Z
                {
                    axis_0 = +x_axis;
                    axis_1 = +y_axis;
                    axis_2 = +z_axis;
                }
                else if (_FaceIndex == 5) // -Z
                {
                    axis_0 = -x_axis;
                    axis_1 = +y_axis;
                    axis_2 = -z_axis;
                }

                float2 local_dir = i.uv.xy * 2 - 1;
                float3 world_dir = axis_0 * local_dir.x + axis_1 * local_dir.y + axis_2;

                float4 col = UNITY_SAMPLE_TEXCUBE_LOD(_MainTex, world_dir, _MipLevel);

                return EncodeToRgbm(col);
            }
            ENDCG
        }
    }
}
