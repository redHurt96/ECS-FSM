using _EcsFsm.Components.AI;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _EcsFsm.Systems.AI
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ClearEnemyReference))]
    public sealed class ClearEnemyReference : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake() =>
            _filter = World.Filter
                .With<EnemyReference>()
                .Build();

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                EnemyReference enemyReference = entity.GetComponent<EnemyReference>();

                if (!World.TryGetEntity(enemyReference.EntityId, out Entity _))
                    entity.RemoveComponent<EnemyReference>();
            }
        }
    }
}