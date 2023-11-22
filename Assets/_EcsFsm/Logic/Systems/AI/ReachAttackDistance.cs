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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ReachAttackDistance))]
    public sealed class ReachAttackDistance : UpdateSystem 
    {
        private Filter _characters;
    
        public override void OnAwake() =>
            _characters = World.Filter
                .With<EnemyReference>()
                .With<Position>()
                .With<Attack>()
                .With<MoveTarget>()
                .Without<NearToAttack>()
                .Build();

        public override void OnUpdate(float deltaTime) 
        {
            foreach (Entity entity in _characters)
            {
                ref MoveTarget moveTarget = ref entity.GetComponent<MoveTarget>();
                ref Position position = ref entity.GetComponent<Position>();
                ref Attack attack = ref entity.GetComponent<Attack>();

                if (Vector3.Distance(moveTarget.Position, position.Value) < attack.Range)
                    entity.AddComponent<NearToAttack>();
            }
        }
    }
}