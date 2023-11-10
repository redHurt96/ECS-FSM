using _EcsFsm.Components.AI;
using _EcsFsm.Components.Core;
using _EcsFsm.Components.Stats;
using _EcsFsm.Systems.Movement;
using Leopotam.EcsLite;

namespace _EcsFsm.Systems.AI
{
    public class UpdateEnemyPosition : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<Enemy> _enemies;
        private EcsPool<MoveTarget> _moveTargets;
        private EcsPool<Position> _positions;
        private EcsFilter _targets;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _filter = world
                .Filter<Enemy>()
                .Inc<MoveTarget>()
                .End();

            _enemies = world.GetPool<Enemy>();
            _moveTargets = world.GetPool<MoveTarget>();
            _positions = world.GetPool<Position>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _filter)
            {
                ref Enemy enemy = ref _enemies.Get(entity);
                ref MoveTarget moveTarget = ref _moveTargets.Get(entity);
                ref Position enemyPosition = ref _positions.Get(enemy.EntityId);

                moveTarget.Position = enemyPosition.Value;
            }
        }
    }
}