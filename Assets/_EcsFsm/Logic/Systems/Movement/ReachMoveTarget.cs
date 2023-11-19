using _EcsFsm.Components.Movement;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace _EcsFsm.Systems.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(ReachMoveTarget))]
    public sealed class ReachMoveTarget : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake() =>
            _filter = World.Filter
                .With<Position>()
                .With<MoveTarget>()
                .With<ReachTargetDistance>()
                .Build();

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref Position position = ref entity.GetComponent<Position>();
                ref MoveTarget target = ref entity.GetComponent<MoveTarget>();
                ref ReachTargetDistance distance = ref entity.GetComponent<ReachTargetDistance>();

                if (Vector3.Distance(position.Value, target.Position) <= distance.Value)
                    entity.RemoveComponent<MoveTarget>();
            }
        }

        public void Dispose() {}
    }
}