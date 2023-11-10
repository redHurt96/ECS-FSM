using _EcsFsm.Systems.Core;
using _EcsFsm.Systems.View;
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
            _systems = new(_world, new GameServices());

            _systems
                //Core
                .Add(new MoveSystem())
                .Add(new ReachTargetSystem())
                
                //View
                .Add(new CreateViewSystem())
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