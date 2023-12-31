using _EcsFsm.Components.AI;
using _EcsFsm.Components.Movement;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _EcsFsm.Systems.AI
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AddMoveTarget))]
    public sealed class AddMoveTarget : UpdateSystem 
    {
        private Filter _characters;
    
        public override void OnAwake() =>
            _characters = World.Filter
                .With<EnemyReference>()
                .Without<MoveTarget>()
                .Build();

        public override void OnUpdate(float deltaTime) 
        {
            foreach (Entity entity in _characters)
                entity.AddComponent<MoveTarget>();
        }
    }
}