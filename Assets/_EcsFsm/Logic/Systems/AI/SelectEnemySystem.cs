using _EcsFsm.Components.AI;
using _EcsFsm.Components.Movement;
using _EcsFsm.Components.Stats;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _EcsFsm.Systems.AI
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(SelectEnemySystem))]
    public sealed class SelectEnemySystem : UpdateSystem 
    {
        private Filter _characters;
        private Filter _targets;
    
        public override void OnAwake()
        {
            _characters = World.Filter
                .With<Team>()
                .Without<EnemyReference>()
                .Build();

            _targets = World.Filter
                .With<Team>()
                .With<Position>()
                .Build();
        }

        public override void OnUpdate(float deltaTime) 
        {
            foreach (Entity entity in _characters)
            {
                ref Team team = ref entity.GetComponent<Team>();
                EntityId targetEntityId = SelectTarget(team);
                ref EnemyReference enemyReference = ref entity.AddComponent<EnemyReference>();
                
                enemyReference.EntityId = targetEntityId;
            }
        }

        private EntityId SelectTarget(Team originTeam)
        {
            foreach (Entity entity in _targets)
            {
                ref Team team = ref entity.GetComponent<Team>();

                if (originTeam.Index != team.Index)
                    return entity.ID;
            }

            return EntityId.Invalid;
        }
    }
}