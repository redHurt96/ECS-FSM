using _EcsFsm.Components.AI;
using _EcsFsm.Components.Core;
using _EcsFsm.Components.Stats;
using Leopotam.EcsLite;

namespace _EcsFsm.Systems.AI
{
    public class SelectEnemy : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _characters;
        private EcsPool<Enemy> _charactersTargets;
        private EcsPool<Team> _teams;
        private EcsPool<MoveTarget> _moveTargets;
        private EcsWorld _world;
        private EcsFilter _targets;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _characters = _world
                .Filter<Team>()
                .Exc<Enemy>()
                .End();

            _targets = _world
                .Filter<Team>()
                .Inc<Position>()
                .End();
            
            _charactersTargets = _world.GetPool<Enemy>();
            _moveTargets = _world.GetPool<MoveTarget>();
            _teams = _world.GetPool<Team>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (int entity in _characters)
            {
                ref Team team = ref _teams.Get(entity);
                int targetEntityId = SelectTarget(team.Index);
                ref Enemy enemy = ref _charactersTargets.Add(entity);
                
                enemy.EntityId = targetEntityId;

                if (!_moveTargets.Has(entity))
                    _moveTargets.Add(entity);
            }
        }

        private int SelectTarget(int teamIndex)
        {
            foreach (int entity in _targets)
            {
                ref Team team = ref _teams.Get(entity);

                if (teamIndex != team.Index)
                    return entity;
            }

            return -1;
        }
    }
}
