using _EcsFsm.Components.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace _EcsFsm.Systems.Core
{
    public sealed class ReachMoveTarget : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<Position> _positions;
        private EcsPool<MoveTarget> _moveTargets;
        private EcsPool<ReachTargetDistance> _reachTargetDistances;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _filter = world
                .Filter<Position>()
                .Inc<MoveTarget>()
                .Inc<ReachTargetDistance>()
                .End();

            _positions = world.GetPool<Position>();
            _moveTargets = world.GetPool<MoveTarget>();
            _reachTargetDistances = world.GetPool<ReachTargetDistance>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref Position positionHook = ref _positions.Get(entity);
                ref MoveTarget target = ref _moveTargets.Get(entity);
                ref ReachTargetDistance distance = ref _reachTargetDistances.Get(entity);

                if (Vector3.Distance(positionHook.Value, target.Position) <= distance.Value)
                    _moveTargets.Del(entity);
            }
        }
    }
}