using _EcsFsm.Components.AI;
using _EcsFsm.Components.Core;
using _EcsFsm.Components.Stats;
using Leopotam.EcsLite;
using static UnityEngine.Mathf;
using static UnityEngine.Vector3;

namespace _EcsFsm.Systems.AI
{
    public class Attack : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<Enemy> _enemies;
        private EcsPool<MoveTarget> _moveTargets;
        private EcsPool<Position> _positions;
        private EcsPool<Components.Stats.Attack> _attacks;
        private EcsPool<Health> _healths;
        private EcsPool<AttackCooldown> _cooldowns;
        private EcsFilter _targets;

        public void Init(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();

            _filter = world
                .Filter<Enemy>()
                .Inc<MoveTarget>()
                .Inc<Components.Stats.Attack>()
                .Exc<AttackCooldown>()
                .End();

            _attacks = world.GetPool<Components.Stats.Attack>();
            _healths = world.GetPool<Health>();
            _cooldowns = world.GetPool<AttackCooldown>();
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
                ref Components.Stats.Attack attack = ref _attacks.Get(entity);
                ref Position position = ref _positions.Get(entity);
                ref Position enemyPosition = ref _positions.Get(enemy.EntityId);
                ref Health enemyHealth = ref _healths.Get(enemy.EntityId);

                if (Distance(position.Value, enemyPosition.Value) < attack.Range)
                {
                    enemyHealth.Value = Max(enemyHealth.Value - attack.Damage, 0f);
                    ref AttackCooldown cooldown = ref _cooldowns.Add(entity);
                    cooldown.Left = attack.Cooldown;
                }
                
                moveTarget.Position = enemyPosition.Value;
            }
        }
    }
}