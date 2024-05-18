using System;
using UnityEngine;

namespace GPUInstancer.CrowdAnimations
{
    public class GPUICrowdOptionalRendererHandler : MonoBehaviour
    {
        private GPUICrowdPrefab _crowPrefab;


        private void Awake()
        {
            _crowPrefab = GetComponent<GPUICrowdPrefab>();
        }

        private void OnEnable()
        {
            if (_crowPrefab.state == PrefabInstancingState.None || _crowPrefab.state == PrefabInstancingState.Disabled)
            {
                _crowPrefab.state = PrefabInstancingState.Instanced;
                _crowPrefab.runtimeData.instanceDataNativeArray[_crowPrefab.gpuInstancerID - 1] = _crowPrefab.GetInstanceTransform().localToWorldMatrix;
                _crowPrefab.runtimeData.transformDataModified = true;
            }
        }

        private void OnDisable()
        {
            if (_crowPrefab.state == PrefabInstancingState.Instanced && _crowPrefab.runtimeData.instanceDataNativeArray.IsCreated)
            {
                _crowPrefab.state = PrefabInstancingState.Disabled;
                _crowPrefab.runtimeData.instanceDataNativeArray[_crowPrefab.gpuInstancerID - 1] = Matrix4x4.zero;
                _crowPrefab.runtimeData.transformDataModified = true;
            }
        }

        internal void SetupOptionalRenderer()
        {
            if (_crowPrefab.state == PrefabInstancingState.Instanced)
            {
                _crowPrefab.runtimeData.instanceDataNativeArray[_crowPrefab.gpuInstancerID - 1] = _crowPrefab.GetInstanceTransform().localToWorldMatrix;
                _crowPrefab.runtimeData.transformDataModified = true;
            }
            else if (_crowPrefab.state == PrefabInstancingState.Disabled)
            {
                _crowPrefab.runtimeData.instanceDataNativeArray[_crowPrefab.gpuInstancerID - 1] = Matrix4x4.zero;
                _crowPrefab.runtimeData.transformDataModified = true;
            }
        }
    }
}
