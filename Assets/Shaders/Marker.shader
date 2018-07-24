// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Marker" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		
		_MarkerTex("Marker Texture", 2D) = "white" {}
		_MarkerPos ("Marker Position", Vector) = (0, 0, 0, 0)
		_MarkerDist ("Marker Distance", Range(0, 100)) = 5
		_MarkerColor ("Marker Color", Color) = (1,1,1,1)

		_EdgeColor("Edge Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		sampler2D _MarkerTex;
		float4 _MarkerPos;
		float _MarkerDist;
		fixed4 _MarkerColor;

		fixed4 _EdgeColor;

		void surf (Input IN, inout SurfaceOutputStandard o) {

			// MainTexture
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;

			// distance
			float dist = distance(_MarkerPos, IN.worldPos);
			float power = min(dist / _MarkerDist, 1);

			// Marker fill
			float x = _Time.y;
			fixed4 fill = tex2D(_MarkerTex, float2(x + power, 0)) * power;
			fill *= _MarkerColor;


			// Marker edge
			// 外側の境界は若干アンチエイリアスをかける感じで
			float edge = smoothstep(0.9, 0.99, power) - smoothstep(0.99, 0.999, power);

			// 最後にそれらをブレンド
			o.Albedo = fill * (1 - power) + c.rgb * power + _EdgeColor * edge;

			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
