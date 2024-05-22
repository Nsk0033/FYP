#include "./../../../GPUInstancer/Shaders/Include/GPUInstancerInclude.cginc"

#ifndef GPUICA_SG_INCLUDED
#define GPUICA_SG_INCLUDED
#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
uniform StructuredBuffer<float4x4> gpuiAnimationBuffer;
uniform float totalNumberOfBones;
uniform float4x4 bindPoseOffset;
#endif

void gpuiAnimate_float(float4 GPUI_BONE_INDEX, float4 GPUI_BONE_WEIGHT, float3 GPUI_VERTEX, float3 GPUI_NORMAL, float3 GPUI_TANGENT, out float3 OUT_GPUI_VERTEX, out float3 OUT_GPUI_NORMAL, out float3 OUT_GPUI_TANGENT)
{
#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
    uint addition = gpuiTransformationMatrix[unity_InstanceID] * totalNumberOfBones;

    // required for some shader compilers like PSSL
    float4x4 weightX = float4x4(
            GPUI_BONE_WEIGHT.x, 0, 0, 0,
            0, GPUI_BONE_WEIGHT.x, 0, 0,
            0, 0, GPUI_BONE_WEIGHT.x, 0,
            0, 0, 0, GPUI_BONE_WEIGHT.x
        );
    float4x4 weightY = float4x4(
            GPUI_BONE_WEIGHT.y, 0, 0, 0,
            0, GPUI_BONE_WEIGHT.y, 0, 0,
            0, 0, GPUI_BONE_WEIGHT.y, 0,
            0, 0, 0, GPUI_BONE_WEIGHT.y
        );
    float4x4 weightZ = float4x4(
            GPUI_BONE_WEIGHT.z, 0, 0, 0,
            0, GPUI_BONE_WEIGHT.z, 0, 0,
            0, 0, GPUI_BONE_WEIGHT.z, 0,
            0, 0, 0, GPUI_BONE_WEIGHT.z
        );
    float4x4 weightW = float4x4(
            GPUI_BONE_WEIGHT.w, 0, 0, 0,
            0, GPUI_BONE_WEIGHT.w, 0, 0,
            0, 0, GPUI_BONE_WEIGHT.w, 0,
            0, 0, 0, GPUI_BONE_WEIGHT.w
        );

#ifdef GPUI_CA_BINDPOSEOFFSET
    float4x4 boneMatrix = mul(mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.x], bindPoseOffset), weightX);
    boneMatrix += mul(mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.y], bindPoseOffset), weightY);
    boneMatrix += mul(mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.z], bindPoseOffset), weightZ);
    boneMatrix += mul(mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.w], bindPoseOffset), weightW);
#else
    float4x4 boneMatrix = mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.x], weightX);
    boneMatrix += mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.y], weightY);
    boneMatrix += mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.z], weightZ);
    boneMatrix += mul(gpuiAnimationBuffer[addition + GPUI_BONE_INDEX.w], weightW);
#endif

    float3x3 boneMat3 = (float3x3) boneMatrix;
    float3 posDiff = boneMatrix._14_24_34;
    OUT_GPUI_VERTEX.xyz = mul(boneMat3, GPUI_VERTEX.xyz);
    OUT_GPUI_VERTEX.xyz += posDiff;
    OUT_GPUI_NORMAL.xyz = normalize(mul(boneMat3, GPUI_NORMAL.xyz));
    OUT_GPUI_TANGENT.xyz = normalize(mul(boneMat3, GPUI_TANGENT.xyz));
#else
    OUT_GPUI_VERTEX = GPUI_VERTEX;
    OUT_GPUI_NORMAL = GPUI_NORMAL;
    OUT_GPUI_TANGENT = GPUI_TANGENT;
#endif
}

#endif