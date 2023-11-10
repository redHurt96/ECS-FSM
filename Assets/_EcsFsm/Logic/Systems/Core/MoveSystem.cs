using _EcsFsm.Components.Core;
using Leopotam.EcsLite;
using static UnityEngine.Time;
using static UnityEngine.Vector3;

namespace _EcsFsm.Systems.Core
{
    public sealed class MoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<Position> _positions;
        private EcsPool<MoveTarget> _moveTargets;
        private EcsPool<Speed> _speeds;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _filter = world
                .Filter<Position>()
                .Inc<MoveTarget>()
                .End();

            _positions = world.GetPool<Position>();
            _moveTargets = world.GetPool<MoveTarget>();
            _speeds = world.GetPool<Speed>();
        }

        public void Run(IEcsSystems systems)
        {
            float time = deltaTime;
            
            foreach (int entity in _filter)
            {
                ref Position positionHook = ref _positions.Get(entity);
                ref MoveTarget target = ref _moveTargets.Get(entity);
                ref Speed speed = ref _speeds.Get(entity);

                positionHook.Value = MoveTowards(positionHook.Value, target.Position, speed.Value * time);
            }
        }
    }
}