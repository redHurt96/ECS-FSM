using _EcsFsm.Systems;
using AB_Utility.FromSceneToEntityConverter;
using Leopotam.EcsLite;
using UnityEngine;

namespace _EcsFsm
{
    public class EntryPoint : MonoBehaviour
    {
        private EcsSystems _systems;
        private EcsWorld _world;

        private void Start()
        {
            _world = new();
            _systems = new(_world);

            _systems
                    
                //Core
                .Add(new MoveSystem())
                .Add(new ReachTargetSystem())
                
                //View
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem())
#endif
                .ConvertScene()
                .Init();
        }

        private void Update() => 
            _systems.Run();

        private void OnDestroy() => 
            _systems.Destroy();
    }
}