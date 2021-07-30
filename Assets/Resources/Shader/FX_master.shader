// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "jadeFX/FX_master"
{
	Properties
	{
		[Header(lllllllllllllllllll Cull Mode lllllllllllllllllll)][Enum(UnityEngine.Rendering.CullMode)]_Cullmode("Cull mode", Int) = 0
		[Header(lllllllllllllllllll Blend mode lllllllllllllllllll)][Header()][Enum(UnityEngine.Rendering.BlendMode)]_Blend_Src("Blend_Src", Int) = 0
		[Enum(UnityEngine.Rendering.BlendMode)]_Blend_Dst("Blend_Dst", Int) = 0
		[Header(lllllllllllllllllll Main Texture lllllllllllllllllll)]_MainTex("MainTex", 2D) = "white" {}
		_MainTex_intcustom1z("MainTex_int (custom1 : z)", Float) = 0
		_MainTex_UVscaleoffsetcustom1xyoffsetuv("MainTex_UV scale : offset (custom1 xy : offset uv)", Vector) = (0,0,0,0)
		_MainTex_pannerXYUV("MainTex_panner (XY : UV)", Vector) = (0,0,0,0)
		[Header(lllllllllllllllllll Second Texture lllllllllllllllllll)]_SecondTex("SecondTex", 2D) = "white" {}
		[Toggle(_USESECONDTEXTURE_ON)] _UseSecondTexture("Use Second Texture", Float) = 0
		_SecondTex_UVscaleoffset("SecondTex_UV scale : offset", Vector) = (0,0,0,0)
		_SecondTex_PannerXYUV("SecondTex_Panner (XY : UV)", Vector) = (0,0,0,0)
		_SecondTex_color("SecondTex_color", Color) = (0,0,0,0)
		_SecondTex_int("SecondTex_int", Float) = 0
		[Header(lllllllllllllllllll Third Texture lllllllllllllllllll)]_ThirdTex("ThirdTex", 2D) = "white" {}
		[Toggle(_USETHIRDTEXTURE_ON)] _UseThirdTexture("Use Third Texture", Float) = 0
		_ThirdTex_UVscaleoffset("ThirdTex_UV scale : offset", Vector) = (0,0,0,0)
		_ThirdTex_PannerXYUV("ThirdTex_Panner (XY : UV)", Vector) = (0,0,0,0)
		_ThirdTex_int("ThirdTex_int", Float) = 0
		_ThirdTex_color("ThirdTex_color", Color) = (0,0,0,0)
		[Header(lllllllllllllllllll Distort Texture lllllllllllllllllll)]_DistortTex("DistortTex", 2D) = "white" {}
		[Toggle(_USEDISTORTION_ON)] _UseDistortion("Use Distortion", Float) = 0
		_DistortTex_UVscaleoffset("DistortTex_UV scale : offset", Vector) = (0,0,0,0)
		_DistortTex_PannerXYUV("DistortTex_Panner (XY : UV)", Vector) = (0,0,0,0)
		_Distort_amountcustom1w("Distort_amount (custom1 : w)", Float) = 0
		[Header(lllllllllllllllllll Dissolve Texture lllllllllllllllllll)]_DissolveTex("DissolveTex", 2D) = "white" {}
		[Toggle(_USEDISSOLVE_ON)] _UseDissolve("Use Dissolve", Float) = 0
		_DissolveTex_UVscaleoffsetcustom2xyoffsetuv("DissolveTex_UV scale : offset (custom2 xy : offset uv)", Vector) = (0,0,0,0)
		_DissolveTex_PannerXYUV("DissolveTex_Panner (XY : UV)", Vector) = (0,0,0,0)
		[Toggle(_USEDISSOLVEBURN_ON)] _UseDissolveBurn("Use DissolveBurn", Float) = 0
		[HDR]_DissolveBurnColor("DissolveBurn Color", Color) = (0,0,0,0)
		_DissolveBurnamountcustom2z("Dissolve Burnamount (custom2 : z)", Float) = 0.25
		[Header(lllllllllllllllllll Depth fade lllllllllllllllllll)]_DepthFadeamount("Depth Fade amount", Float) = 0
		[Toggle(_USEDEPTHFADE_ON)] _UseDepthfade("Use Depth fade ", Float) = 0
		[Toggle(_USE_FACE_COLOR_ON)] _use_face_color("use_face_color", Float) = 0
		[Toggle(_REVERSE_FACE_ON)] _reverse_face("reverse_face", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord2( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord3( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull [_Cullmode]
		ZWrite Off
		ZTest LEqual
		Blend [_Blend_Src] [_Blend_Dst] , [_Blend_Src] [_Blend_Dst]
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _USE_FACE_COLOR_ON
		#pragma shader_feature _USEDISTORTION_ON
		#pragma shader_feature _USESECONDTEXTURE_ON
		#pragma shader_feature _USETHIRDTEXTURE_ON
		#pragma shader_feature_local _USEDISSOLVEBURN_ON
		#pragma shader_feature_local _REVERSE_FACE_ON
		#pragma shader_feature_local _USEDEPTHFADE_ON
		#pragma shader_feature_local _USEDISSOLVE_ON
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd vertex:vertexDataFunc 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 uv2_tex4coord2;
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float4 uv3_tex4coord3;
			half ASEVFace : VFACE;
			float4 screenPosition206;
		};

		uniform int _Blend_Src;
		uniform int _Blend_Dst;
		uniform int _Cullmode;
		uniform sampler2D _MainTex;
		uniform half2 _MainTex_pannerXYUV;
		uniform half4 _MainTex_UVscaleoffsetcustom1xyoffsetuv;
		uniform sampler2D _DistortTex;
		uniform half2 _DistortTex_PannerXYUV;
		uniform half4 _DistortTex_UVscaleoffset;
		uniform half _Distort_amountcustom1w;
		uniform half4 _SecondTex_color;
		uniform half _SecondTex_int;
		uniform sampler2D _SecondTex;
		uniform half2 _SecondTex_PannerXYUV;
		uniform half4 _SecondTex_UVscaleoffset;
		uniform half _MainTex_intcustom1z;
		uniform half4 _ThirdTex_color;
		uniform half _ThirdTex_int;
		uniform sampler2D _ThirdTex;
		uniform half2 _ThirdTex_PannerXYUV;
		uniform half4 _ThirdTex_UVscaleoffset;
		uniform sampler2D _DissolveTex;
		uniform half2 _DissolveTex_PannerXYUV;
		uniform half4 _DissolveTex_UVscaleoffsetcustom2xyoffsetuv;
		uniform half _DissolveBurnamountcustom2z;
		uniform half4 _DissolveBurnColor;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform half _DepthFadeamount;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			half3 vertexPos206 = ase_vertex3Pos;
			float4 ase_screenPos206 = ComputeScreenPos( UnityObjectToClipPos( vertexPos206 ) );
			o.screenPosition206 = ase_screenPos206;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half2 appendResult102 = (half2(_MainTex_UVscaleoffsetcustom1xyoffsetuv.x , _MainTex_UVscaleoffsetcustom1xyoffsetuv.y));
			half4 _Custom1 = half4(0,0,0,0);
			half custom1_x17 = ( i.uv2_tex4coord2.x + _Custom1.x );
			half custom1_y21 = ( i.uv2_tex4coord2.y + _Custom1.y );
			half2 appendResult103 = (half2(( _MainTex_UVscaleoffsetcustom1xyoffsetuv.z + custom1_x17 ) , ( _MainTex_UVscaleoffsetcustom1xyoffsetuv.w + custom1_y21 )));
			float2 uv_TexCoord100 = i.uv_texcoord * appendResult102 + appendResult103;
			half2 panner34 = ( 1.0 * _Time.y * _MainTex_pannerXYUV + uv_TexCoord100);
			half2 appendResult139 = (half2(_DistortTex_UVscaleoffset.x , _DistortTex_UVscaleoffset.y));
			half2 appendResult140 = (half2(_DistortTex_UVscaleoffset.z , _DistortTex_UVscaleoffset.w));
			float2 uv_TexCoord142 = i.uv_texcoord * appendResult139 + appendResult140;
			half2 panner143 = ( 1.0 * _Time.y * _DistortTex_PannerXYUV + uv_TexCoord142);
			half custom1_w23 = ( i.uv2_tex4coord2.w + _Custom1.w );
			half temp_output_146_0 = ( 0.01 * ( _Distort_amountcustom1w + custom1_w23 ) );
			half distort155 = (( temp_output_146_0 * -1.0 ) + (( tex2D( _DistortTex, panner143 ).r * temp_output_146_0 ) - 0.0) * (temp_output_146_0 - ( temp_output_146_0 * -1.0 )) / (temp_output_146_0 - 0.0));
			#ifdef _USEDISTORTION_ON
				half2 staticSwitch150 = ( distort155 + panner34 );
			#else
				half2 staticSwitch150 = panner34;
			#endif
			half4 tex2DNode1 = tex2D( _MainTex, staticSwitch150 );
			half2 appendResult113 = (half2(_SecondTex_UVscaleoffset.x , _SecondTex_UVscaleoffset.y));
			half2 appendResult114 = (half2(_SecondTex_UVscaleoffset.z , _SecondTex_UVscaleoffset.w));
			float2 uv_TexCoord111 = i.uv_texcoord * appendResult113 + appendResult114;
			half2 panner84 = ( 1.0 * _Time.y * _SecondTex_PannerXYUV + uv_TexCoord111);
			#ifdef _USESECONDTEXTURE_ON
				half staticSwitch83 = ( _SecondTex_int * tex2D( _SecondTex, panner84 ).r );
			#else
				half staticSwitch83 = 1.0;
			#endif
			half custom1_z22 = ( i.uv2_tex4coord2.z + _Custom1.z );
			half2 appendResult123 = (half2(_ThirdTex_UVscaleoffset.x , _ThirdTex_UVscaleoffset.y));
			half2 appendResult124 = (half2(_ThirdTex_UVscaleoffset.z , _ThirdTex_UVscaleoffset.w));
			float2 uv_TexCoord125 = i.uv_texcoord * appendResult123 + appendResult124;
			half2 panner126 = ( 1.0 * _Time.y * _ThirdTex_PannerXYUV + uv_TexCoord125);
			#ifdef _USETHIRDTEXTURE_ON
				half staticSwitch129 = ( _ThirdTex_int * tex2D( _ThirdTex, panner126 ).r );
			#else
				half staticSwitch129 = 1.0;
			#endif
			half temp_output_131_0 = ( tex2DNode1.r * staticSwitch129 );
			half4 temp_cast_0 = (0.0).xxxx;
			half2 appendResult183 = (half2(_DissolveTex_UVscaleoffsetcustom2xyoffsetuv.x , _DissolveTex_UVscaleoffsetcustom2xyoffsetuv.y));
			half4 _Custom2 = half4(0,0,0,0);
			half custom2_x26 = ( i.uv3_tex4coord3.x + _Custom2.x );
			half custom2_y27 = ( i.uv3_tex4coord3.y + _Custom2.y );
			half2 appendResult184 = (half2(( _DissolveTex_UVscaleoffsetcustom2xyoffsetuv.z + custom2_x26 ) , ( _DissolveTex_UVscaleoffsetcustom2xyoffsetuv.w + custom2_y27 )));
			float2 uv_TexCoord185 = i.uv_texcoord * appendResult183 + appendResult184;
			half2 panner187 = ( 1.0 * _Time.y * _DissolveTex_PannerXYUV + uv_TexCoord185);
			half4 tex2DNode5 = tex2D( _DissolveTex, panner187 );
			half custom2_w25 = ( i.uv3_tex4coord3.w + _Custom2.w );
			half temp_output_195_0 = ( 1.0 - step( tex2DNode5.r , ( 1.0 - custom2_w25 ) ) );
			half DissolveBurn226 = ( temp_output_195_0 - ( 1.0 - step( tex2DNode5.r , ( 1.0 - ( custom2_w25 + ( _DissolveBurnamountcustom2z * -1.0 ) ) ) ) ) );
			#ifdef _USEDISSOLVEBURN_ON
				half4 staticSwitch231 = ( DissolveBurn226 * _DissolveBurnColor );
			#else
				half4 staticSwitch231 = temp_cast_0;
			#endif
			half4 firstoutput49 = ( ( tex2DNode1.r * ( tex2DNode1.r * ( _SecondTex_color * staticSwitch83 ) * ( _MainTex_intcustom1z + custom1_z22 ) * ( _ThirdTex_color * temp_output_131_0 ) ) * i.vertexColor ) + staticSwitch231 );
			half custom2_z28 = ( i.uv3_tex4coord3.z + _Custom2.z );
			half4 temp_output_44_0 = ( custom2_z28 * firstoutput49 );
			half4 switchResult10 = (((i.ASEVFace>0)?(firstoutput49):(temp_output_44_0)));
			half4 switchResult48 = (((i.ASEVFace>0)?(temp_output_44_0):(firstoutput49)));
			#ifdef _REVERSE_FACE_ON
				half4 staticSwitch47 = switchResult48;
			#else
				half4 staticSwitch47 = switchResult10;
			#endif
			#ifdef _USE_FACE_COLOR_ON
				half4 staticSwitch51 = staticSwitch47;
			#else
				half4 staticSwitch51 = firstoutput49;
			#endif
			half alphabeforedissolve180 = saturate( ( saturate( ( tex2DNode1.r * staticSwitch83 ) ) * saturate( temp_output_131_0 ) * saturate( tex2DNode1.r ) ) );
			#ifdef _USEDISSOLVE_ON
				half staticSwitch181 = saturate( ( alphabeforedissolve180 * temp_output_195_0 ) );
			#else
				half staticSwitch181 = alphabeforedissolve180;
			#endif
			half alphaafterdissolve203 = staticSwitch181;
			float4 ase_screenPos206 = i.screenPosition206;
			half4 ase_screenPosNorm206 = ase_screenPos206 / ase_screenPos206.w;
			ase_screenPosNorm206.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm206.z : ase_screenPosNorm206.z * 0.5 + 0.5;
			float screenDepth206 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm206.xy ));
			half distanceDepth206 = abs( ( screenDepth206 - LinearEyeDepth( ase_screenPosNorm206.z ) ) / ( _DepthFadeamount ) );
			#ifdef _USEDEPTHFADE_ON
				half staticSwitch212 = ( alphaafterdissolve203 * saturate( distanceDepth206 ) );
			#else
				half staticSwitch212 = alphaafterdissolve203;
			#endif
			half alphaafterdepthfade213 = staticSwitch212;
			half finalalpha55 = ( i.vertexColor.a * alphaafterdepthfade213 );
			o.Emission = ( staticSwitch51 * finalalpha55 ).rgb;
			o.Alpha = finalalpha55;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18800
-2;95;1693;766;2495.065;-2771.274;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;33;-4067.504,2086.413;Inherit;False;892.2843;1205.692;Set CustomData term;20;16;20;19;18;14;12;17;21;22;23;30;31;32;26;15;13;29;27;28;25;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;157;-6529.023,-963.4285;Inherit;False;2768.405;901.7507;Distort Term;17;151;152;138;139;140;147;141;142;145;143;137;146;144;154;148;155;101;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector4Node;14;-3866.86,2420.83;Inherit;False;Constant;_Custom1;Custom1;7;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-3959.203,2136.413;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;138;-6479.023,-913.4286;Inherit;False;Property;_DistortTex_UVscaleoffset;DistortTex_UV scale : offset;22;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;20;-3615.326,2496.272;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;139;-6023.215,-904.8644;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;140;-6029.215,-741.8635;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;23;-3419.218,2473.103;Inherit;False;custom1_w;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;-3612.326,2281.272;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-3608.443,2154.27;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;147;-5582.155,-315.8265;Inherit;False;Property;_Distort_amountcustom1w;Distort_amount (custom1 : w);24;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;141;-5933.859,-533.1549;Inherit;False;Property;_DistortTex_PannerXYUV;DistortTex_Panner (XY : UV);23;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;151;-5453.958,-177.6791;Inherit;False;23;custom1_w;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;142;-5710.577,-875.0595;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;143;-5494.257,-612.6279;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-3434.218,2263.103;Inherit;False;custom1_y;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;145;-5357.482,-490.056;Inherit;False;Constant;_Float2;Float 2;26;0;Create;True;0;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;17;-3423.421,2150.174;Inherit;False;custom1_x;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;236;-3030.778,-1260.45;Inherit;False;5176.316;2863.127;Main Term;64;107;106;110;121;109;108;124;114;102;113;103;123;100;111;37;86;125;122;156;126;34;84;2;117;149;127;3;130;118;87;128;150;83;1;129;131;97;164;162;161;132;120;180;119;116;135;134;230;133;115;136;228;11;232;88;229;42;231;227;49;204;179;55;215;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;152;-5209.959,-226.6792;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;101;-4086.807,-458.5365;Inherit;False;Property;_MainTex_UVscaleoffsetcustom1xyoffsetuv;MainTex_UV scale : offset (custom1 xy : offset uv);6;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;107;-2971.778,356.0607;Inherit;False;21;custom1_y;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;106;-2980.778,254.0606;Inherit;False;17;custom1_x;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;146;-5037.3,-380.3884;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;137;-5213.267,-718.9995;Inherit;True;Property;_DistortTex;DistortTex;20;1;[Header];Create;True;1;lllllllllllllllllll Distort Texture lllllllllllllllllll;0;0;False;0;False;-1;None;26a11a98f30a028489d0e9a938f6a8f5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;108;-2702.778,100.0606;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;144;-4740.472,-739.5795;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;-4635.344,-363.5349;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;121;-2851.122,1059.403;Inherit;False;Property;_ThirdTex_UVscaleoffset;ThirdTex_UV scale : offset;16;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;109;-2700.778,308.0607;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;110;-2962.471,578.912;Inherit;False;Property;_SecondTex_UVscaleoffset;SecondTex_UV scale : offset;10;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;123;-2395.313,1067.967;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;148;-4419.798,-712.6638;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-4017.503,2763.289;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;15;-3993.783,3069.229;Inherit;False;Constant;_Custom2;Custom2;10;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;102;-2530.366,-61.1214;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;113;-2524.343,484.5275;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;124;-2401.313,1230.968;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;114;-2530.343,647.5275;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;103;-2509.366,164.8786;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;37;-2237.165,185.2257;Inherit;False;Property;_MainTex_pannerXYUV;MainTex_panner (XY : UV);7;1;[Header];Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;111;-2211.705,514.3323;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;86;-2190.513,804.326;Inherit;False;Property;_SecondTex_PannerXYUV;SecondTex_Panner (XY : UV);11;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;125;-2082.675,1097.772;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;122;-2297.957,1438.677;Inherit;False;Property;_ThirdTex_PannerXYUV;ThirdTex_Panner (XY : UV);17;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RegisterLocalVarNode;155;-4004.62,-743.3615;Inherit;False;distort;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-3636.409,2920.105;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-3649.868,2796.297;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;100;-2237.366,-83.1214;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;205;-3101.865,2062.162;Inherit;False;3318.728;1391.71;Dissolve Term;29;216;203;181;202;197;226;165;222;221;195;220;193;196;5;218;217;187;186;185;201;224;184;183;191;219;190;189;188;182;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;26;-3469.219,2779.894;Inherit;False;custom2_x;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;34;-1916.388,76.63971;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;84;-1858.37,650.3101;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;156;-2044.57,-568.0752;Inherit;False;155;distort;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-3458.293,2897.478;Inherit;False;custom2_y;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;126;-1866.357,1360.204;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;188;-2895.508,2756.538;Inherit;False;26;custom2_x;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;149;-1773.783,-571.8914;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;117;-1543.941,434.5414;Inherit;False;Property;_SecondTex_int;SecondTex_int;13;0;Create;True;0;0;0;False;0;False;0;1.19;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-1605.441,625.3887;Inherit;True;Property;_SecondTex;SecondTex;8;1;[Header];Create;True;1;lllllllllllllllllll Second Texture lllllllllllllllllll;0;0;False;0;False;-1;None;b6c4e76394381a3459ab5e9d1dba142f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;182;-3051.865,2510.285;Inherit;False;Property;_DissolveTex_UVscaleoffsetcustom2xyoffsetuv;DissolveTex_UV scale : offset (custom2 xy : offset uv);27;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;189;-2908.508,2885.538;Inherit;False;27;custom2_y;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-1330.309,1011.839;Inherit;False;Property;_ThirdTex_int;ThirdTex_int;18;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-1529.491,1162.06;Inherit;True;Property;_ThirdTex;ThirdTex;14;1;[Header];Create;True;1;lllllllllllllllllll Third Texture lllllllllllllllllll;0;0;False;0;False;-1;None;26a11a98f30a028489d0e9a938f6a8f5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;191;-2669.508,2855.538;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;190;-2676.508,2726.538;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;118;-1154.037,669.6849;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;128;-957.7134,1168.377;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;150;-1442.113,-373.1646;Inherit;False;Property;_UseDistortion;Use Distortion;21;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;130;-947.4559,980.3091;Inherit;False;Constant;_Float1;Float 1;22;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;32;-3636.409,3157.105;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-1146.566,324.695;Inherit;False;Constant;_Float0;Float 0;14;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;25;-3466.769,3138.161;Inherit;False;custom2_w;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;184;-2469.055,2684.849;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;183;-2463.055,2521.848;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;129;-682.9031,1038.588;Inherit;False;Property;_UseThirdTexture;Use Third Texture;15;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;83;-862.4968,607.0989;Inherit;False;Property;_UseSecondTexture;Use Second Texture;9;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;False;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;219;-2034.286,3120.091;Inherit;False;Property;_DissolveBurnamountcustom2z;Dissolve Burnamount (custom2 : z);31;0;Create;True;0;0;0;False;0;False;0.25;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-1081.363,-220.7191;Inherit;True;Property;_MainTex;MainTex;3;1;[Header];Create;True;1;lllllllllllllllllll Main Texture lllllllllllllllllll;0;0;False;0;False;-1;29a20001d1b09a84fa98678f4c74e100;fb975408197ad904c99b43aa808e1abe;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;185;-2150.417,2551.653;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;186;-2365.699,2892.558;Inherit;False;Property;_DissolveTex_PannerXYUV;DissolveTex_Panner (XY : UV);28;0;Create;True;0;0;0;False;0;False;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;201;-1590.56,2885.661;Inherit;False;25;custom2_w;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;131;-265.3199,910.2776;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;-292.3537,498.9862;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;224;-1394.369,3109.909;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;164;376.4764,-288.0263;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;187;-1934.099,2814.085;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;162;11.46118,470.6625;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;161;66.19538,180.2323;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;217;-1269.286,2872.091;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;218;-1120.286,2874.091;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1429.191,2478.329;Inherit;True;Property;_DissolveTex;DissolveTex;25;1;[Header];Create;True;1;lllllllllllllllllll Dissolve Texture lllllllllllllllllll;0;0;False;0;False;-1;None;370802cf1b9b8bd4b98626bfa0a5ebd6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;196;-1276.626,2724.075;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;132;683.7523,-171.1773;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;220;-874.2864,2792.091;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;193;-954.6264,2526.075;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;120;832.9526,-153.0806;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-3607.326,2391.272;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;221;-595.2866,2789.091;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;195;-627.6265,2491.075;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;180;1122.042,-120.4191;Inherit;False;alphabeforedissolve;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;22;-3424.218,2358.103;Inherit;False;custom1_z;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;165;-1598.971,2112.162;Inherit;True;180;alphabeforedissolve;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;222;-327.8597,2736.938;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;116;-798.1322,-381.2494;Inherit;False;Property;_SecondTex_color;SecondTex_color;12;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;119;-942.9725,129.9004;Inherit;False;Property;_MainTex_intcustom1z;MainTex_int (custom1 : z);5;0;Create;True;0;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;135;-968.1389,231.5439;Inherit;False;22;custom1_z;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;226;-22.46968,2767.36;Inherit;False;DissolveBurn;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;197;-988.1026,2296.651;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;134;229.7025,1089.889;Inherit;False;Property;_ThirdTex_color;ThirdTex_color;19;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;228;-1003.518,-1181.736;Inherit;False;226;DissolveBurn;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;235;-3115.383,3499.13;Inherit;False;1771.948;715.4559;Depth fade Term;8;208;207;206;210;211;214;212;213;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SaturateNode;202;-807.03,2303.114;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;136;-640.1389,198.5439;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;133;362.1702,934.2753;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;230;-796.5179,-927.7362;Inherit;False;Property;_DissolveBurnColor;DissolveBurn Color;30;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;3.213544,0,0.3533217,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;-448.8496,3.305345;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;11;-481.1681,-695.4591;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;181;-758.4277,2146.836;Inherit;False;Property;_UseDissolve;Use Dissolve;26;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;207;-3030.383,3714.586;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;208;-3065.383,4098.585;Inherit;False;Property;_DepthFadeamount;Depth Fade amount;32;1;[Header];Create;True;1;lllllllllllllllllll Depth fade lllllllllllllllllll;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;232;-919.8505,-1041.45;Inherit;False;Constant;_Float4;Float 4;36;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;229;-420.5179,-1079.736;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;2.974798,-149.8097;Inherit;False;4;4;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;231;-215.8505,-1210.45;Inherit;False;Property;_UseDissolveBurn;Use DissolveBurn;29;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;13.08786,-747.8835;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DepthFade;206;-2686.11,3827.277;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;203;-390.692,2295.888;Inherit;False;alphaafterdissolve;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;210;-2402.69,3833.005;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;227;224.276,-936.4241;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;211;-2691.221,3554.566;Inherit;False;203;alphaafterdissolve;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-3651.409,3039.105;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;52;203.1519,-2159.897;Inherit;False;1608.933;613.2833;Two-sided term;7;50;44;48;10;47;51;53;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;49;642.5634,-845.5807;Inherit;True;firstoutput;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;214;-2187.834,3709.53;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;28;-3470.135,3027.886;Inherit;False;custom2_z;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;50;256.0367,-1675.798;Inherit;False;49;firstoutput;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;53;256.1062,-2089.434;Inherit;False;28;custom2_z;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;212;-2031.498,3564.333;Inherit;False;Property;_UseDepthfade;Use Depth fade ;33;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;213;-1621.434,3549.13;Inherit;False;alphaafterdepthfade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;572.6719,-2083.323;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;204;1158.667,-472.21;Inherit;False;213;alphaafterdepthfade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SwitchByFaceNode;10;844.2461,-2101.856;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwitchByFaceNode;48;865.6535,-1700.519;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;1513.508,-593.6858;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;47;1121.663,-2109.897;Inherit;False;Property;_reverse_face;reverse_face;35;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;82;-4041.867,3527.891;Inherit;False;503.0002;418.3506;Cull / Blendmode setting term;3;75;81;72;;1,1,1,1;0;0
Node;AmplifyShaderEditor.StaticSwitch;51;1483.927,-1685.007;Inherit;False;Property;_use_face_color;use_face_color;34;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;55;1751.374,-595.8446;Inherit;True;finalalpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;81;-3932.226,3830.242;Inherit;False;Property;_Cullmode;Cull mode;0;2;[Header];[Enum];Create;True;1;lllllllllllllllllll Cull Mode lllllllllllllllllll;0;1;UnityEngine.Rendering.CullMode;True;0;False;0;0;False;0;1;INT;0
Node;AmplifyShaderEditor.IntNode;75;-3763.865,3609.891;Inherit;False;Property;_Blend_Dst;Blend_Dst;2;1;[Enum];Create;True;0;11;One;0;Zero;1;Src Color;2;Src Alpha;3;Dst Color;4;Dst Alpha;5;One Minus Src Color;6;One Minus Src Alpha;7;One Minus Dst Color;8;One Minus Dst Alpha;9;Src Alpha Saturate;10;1;UnityEngine.Rendering.BlendMode;True;0;False;0;10;False;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;215;1977.538,-1149.666;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;99;1988.251,-1355.81;Inherit;False;55;finalalpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;216;-1556.286,2765.091;Inherit;False;Constant;_Float3;Float 3;33;0;Create;True;0;0;0;False;0;False;0.85;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;72;-3988.883,3589.547;Inherit;False;Property;_Blend_Src;Blend_Src;1;2;[Header];[Enum];Create;True;2;lllllllllllllllllll Blend mode lllllllllllllllllll;;11;One;0;Zero;1;Src Color;2;Src Alpha;3;Dst Color;4;Dst Alpha;5;One Minus Src Color;6;One Minus Src Alpha;7;One Minus Dst Color;8;One Minus Dst Alpha;9;Src Alpha Saturate;10;1;UnityEngine.Rendering.BlendMode;True;0;False;0;5;False;0;1;INT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3298.56,316.1684;Half;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;jadeFX/FX_master;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;False;Off;2;False;-1;3;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;True;72;10;True;75;1;0;True;72;0;True;75;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;4;-1;-1;-1;0;False;0;0;True;81;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;12;4
WireConnection;20;1;14;4
WireConnection;139;0;138;1
WireConnection;139;1;138;2
WireConnection;140;0;138;3
WireConnection;140;1;138;4
WireConnection;23;0;20;0
WireConnection;18;0;12;2
WireConnection;18;1;14;2
WireConnection;16;0;12;1
WireConnection;16;1;14;1
WireConnection;142;0;139;0
WireConnection;142;1;140;0
WireConnection;143;0;142;0
WireConnection;143;2;141;0
WireConnection;21;0;18;0
WireConnection;17;0;16;0
WireConnection;152;0;147;0
WireConnection;152;1;151;0
WireConnection;146;0;145;0
WireConnection;146;1;152;0
WireConnection;137;1;143;0
WireConnection;108;0;101;3
WireConnection;108;1;106;0
WireConnection;144;0;137;1
WireConnection;144;1;146;0
WireConnection;154;0;146;0
WireConnection;109;0;101;4
WireConnection;109;1;107;0
WireConnection;123;0;121;1
WireConnection;123;1;121;2
WireConnection;148;0;144;0
WireConnection;148;2;146;0
WireConnection;148;3;154;0
WireConnection;148;4;146;0
WireConnection;102;0;101;1
WireConnection;102;1;101;2
WireConnection;113;0;110;1
WireConnection;113;1;110;2
WireConnection;124;0;121;3
WireConnection;124;1;121;4
WireConnection;114;0;110;3
WireConnection;114;1;110;4
WireConnection;103;0;108;0
WireConnection;103;1;109;0
WireConnection;111;0;113;0
WireConnection;111;1;114;0
WireConnection;125;0;123;0
WireConnection;125;1;124;0
WireConnection;155;0;148;0
WireConnection;30;0;13;2
WireConnection;30;1;15;2
WireConnection;29;0;13;1
WireConnection;29;1;15;1
WireConnection;100;0;102;0
WireConnection;100;1;103;0
WireConnection;26;0;29;0
WireConnection;34;0;100;0
WireConnection;34;2;37;0
WireConnection;84;0;111;0
WireConnection;84;2;86;0
WireConnection;27;0;30;0
WireConnection;126;0;125;0
WireConnection;126;2;122;0
WireConnection;149;0;156;0
WireConnection;149;1;34;0
WireConnection;2;1;84;0
WireConnection;3;1;126;0
WireConnection;191;0;182;4
WireConnection;191;1;189;0
WireConnection;190;0;182;3
WireConnection;190;1;188;0
WireConnection;118;0;117;0
WireConnection;118;1;2;1
WireConnection;128;0;127;0
WireConnection;128;1;3;1
WireConnection;150;1;34;0
WireConnection;150;0;149;0
WireConnection;32;0;13;4
WireConnection;32;1;15;4
WireConnection;25;0;32;0
WireConnection;184;0;190;0
WireConnection;184;1;191;0
WireConnection;183;0;182;1
WireConnection;183;1;182;2
WireConnection;129;1;130;0
WireConnection;129;0;128;0
WireConnection;83;1;87;0
WireConnection;83;0;118;0
WireConnection;1;1;150;0
WireConnection;185;0;183;0
WireConnection;185;1;184;0
WireConnection;131;0;1;1
WireConnection;131;1;129;0
WireConnection;97;0;1;1
WireConnection;97;1;83;0
WireConnection;224;0;219;0
WireConnection;164;0;1;1
WireConnection;187;0;185;0
WireConnection;187;2;186;0
WireConnection;162;0;131;0
WireConnection;161;0;97;0
WireConnection;217;0;201;0
WireConnection;217;1;224;0
WireConnection;218;0;217;0
WireConnection;5;1;187;0
WireConnection;196;0;201;0
WireConnection;132;0;161;0
WireConnection;132;1;162;0
WireConnection;132;2;164;0
WireConnection;220;0;5;1
WireConnection;220;1;218;0
WireConnection;193;0;5;1
WireConnection;193;1;196;0
WireConnection;120;0;132;0
WireConnection;19;0;12;3
WireConnection;19;1;14;3
WireConnection;221;0;220;0
WireConnection;195;0;193;0
WireConnection;180;0;120;0
WireConnection;22;0;19;0
WireConnection;222;0;195;0
WireConnection;222;1;221;0
WireConnection;226;0;222;0
WireConnection;197;0;165;0
WireConnection;197;1;195;0
WireConnection;202;0;197;0
WireConnection;136;0;119;0
WireConnection;136;1;135;0
WireConnection;133;0;134;0
WireConnection;133;1;131;0
WireConnection;115;0;116;0
WireConnection;115;1;83;0
WireConnection;181;1;165;0
WireConnection;181;0;202;0
WireConnection;229;0;228;0
WireConnection;229;1;230;0
WireConnection;88;0;1;1
WireConnection;88;1;115;0
WireConnection;88;2;136;0
WireConnection;88;3;133;0
WireConnection;231;1;232;0
WireConnection;231;0;229;0
WireConnection;42;0;1;1
WireConnection;42;1;88;0
WireConnection;42;2;11;0
WireConnection;206;1;207;0
WireConnection;206;0;208;0
WireConnection;203;0;181;0
WireConnection;210;0;206;0
WireConnection;227;0;42;0
WireConnection;227;1;231;0
WireConnection;31;0;13;3
WireConnection;31;1;15;3
WireConnection;49;0;227;0
WireConnection;214;0;211;0
WireConnection;214;1;210;0
WireConnection;28;0;31;0
WireConnection;212;1;211;0
WireConnection;212;0;214;0
WireConnection;213;0;212;0
WireConnection;44;0;53;0
WireConnection;44;1;50;0
WireConnection;10;0;50;0
WireConnection;10;1;44;0
WireConnection;48;0;44;0
WireConnection;48;1;50;0
WireConnection;179;0;11;4
WireConnection;179;1;204;0
WireConnection;47;1;10;0
WireConnection;47;0;48;0
WireConnection;51;1;50;0
WireConnection;51;0;47;0
WireConnection;55;0;179;0
WireConnection;215;0;51;0
WireConnection;215;1;55;0
WireConnection;0;2;215;0
WireConnection;0;9;99;0
ASEEND*/
//CHKSM=538DAFD312EE27B4618FA72307C99C0500D06EED