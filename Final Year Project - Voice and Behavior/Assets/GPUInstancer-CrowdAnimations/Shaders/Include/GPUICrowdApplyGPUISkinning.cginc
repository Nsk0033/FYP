
sampler2D _gpuiAnimationTexture;

float4x4 getBoneMatrixFromTexture(int frameIndex, float boneIndex, float totalNumberOfBones, float animationTextureSizeX, float animationTextureSizeY, 
	float4x4 bindPoseOffset)
{
	uint textureIndex = frameIndex * totalNumberOfBones + boneIndex;

	float4 texIndex;
	texIndex.x = ((textureIndex % animationTextureSizeX) + 0.5) / animationTextureSizeX;
	texIndex.y = ((floor(textureIndex / animationTextureSizeX) * 4) + 0.5) / animationTextureSizeY;
	texIndex.z = 0;
	texIndex.w = 0;
	float offset = 1.0f / animationTextureSizeY;

	float4x4 boneMatrix;
	boneMatrix._11_21_31_41 = tex2Dlod(_gpuiAnimationTexture, texIndex);
	texIndex.y += offset;
	boneMatrix._12_22_32_42 = tex2Dlod(_gpuiAnimationTexture, texIndex);
	texIndex.y += offset;
	boneMatrix._13_23_33_43 = tex2Dlod(_gpuiAnimationTexture, texIndex);
	texIndex.y += offset; 
	boneMatrix._14_24_34_44 = tex2Dlod(_gpuiAnimationTexture, texIndex);
	return mul(boneMatrix, bindPoseOffset);
	//return float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
}

void applyGPUISkinning_float(float4 GPUIBoneIndex, float4 GPUIBoneWeight, float3 vertex, float3 normal, float3 tangent, float frameIndex, float numBones, 
					   float animationTextureSizeX, float animationTextureSizeY,  float4x4 bindPoseOffset,
					   out float3 OUT_vertex, out float3 OUT_normal, out float3 OUT_tangent)
{
	float4 boneIndexes = float4(GPUIBoneIndex.x, GPUIBoneIndex.y, GPUIBoneIndex.z, GPUIBoneIndex.w);
	float4 boneWeights = float4(GPUIBoneWeight.x, GPUIBoneWeight.y, GPUIBoneWeight.z, GPUIBoneWeight.w);
    
	uint frameStart = floor(frameIndex);
	uint frameEnd = ceil(frameIndex);
	float progress = frac(frameIndex);
		
	float4x4 boneMatrix = mul(getBoneMatrixFromTexture(frameStart, boneIndexes.x, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.x * (1.0 - progress));
	boneMatrix += mul(getBoneMatrixFromTexture(frameStart, boneIndexes.y, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.y * (1.0 - progress));
	boneMatrix += mul(getBoneMatrixFromTexture(frameStart, boneIndexes.z, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.z * (1.0 - progress));
	boneMatrix += mul(getBoneMatrixFromTexture(frameStart, boneIndexes.w, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.w * (1.0 - progress));

	boneMatrix += mul(getBoneMatrixFromTexture(frameEnd, boneIndexes.x, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.x * progress);
	boneMatrix += mul(getBoneMatrixFromTexture(frameEnd, boneIndexes.y, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.y * progress);
	boneMatrix += mul(getBoneMatrixFromTexture(frameEnd, boneIndexes.z, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.z * progress);
	boneMatrix += mul(getBoneMatrixFromTexture(frameEnd, boneIndexes.w, numBones, animationTextureSizeX, animationTextureSizeY, bindPoseOffset), boneWeights.w * progress);

	float3x3 boneMat3 = (float3x3) boneMatrix;
	float3 posDiff = boneMatrix._14_24_34;
	OUT_vertex.xyz = mul(boneMat3, vertex.xyz);
	OUT_vertex.xyz += posDiff;
	OUT_normal.xyz = normalize(mul(boneMat3, normal.xyz));
	OUT_tangent.xyz = normalize(mul(boneMat3, tangent.xyz));

}

