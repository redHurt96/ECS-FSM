using _EcsFsm.Components.Core;
using _EcsFsm.Converters.View;
using _EcsFsm.Services;
using Leopotam.EcsLite;

namespace _EcsFsm.Systems.View
{
    public sealed class CreateViewSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<HasView> _viewsPool;
        private IResourcesService _resourcesService;
        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _resourcesService = systems.GetShared<GameServices>().ResourcesService;
            _filter = _world
                .Filter<Position>()
                .Exc<HasView>()
                .End();

            _viewsPool = _world.GetPool<HasView>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                _resourcesService.CreateFor(entity, _world);
                _viewsPool.Add(entity);
            }
        }
    }
}