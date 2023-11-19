using _EcsFsm.Components.Movement;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using static UnityEngine.Vector3;

namespace _EcsFsm.Systems.Movement
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(Move))]
    public sealed class Move : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake() =>
            _filter = World.Filter
                .With<Position>()
                .With<MoveTarget>()
                .Build();

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref Position position = ref entity.GetComponent<Position>();
                ref MoveTarget target = ref entity.GetComponent<MoveTarget>();
                ref Speed speed = ref entity.GetComponent<Speed>();
                
                position.Value = MoveTowards(position.Value, target.Position, speed.Value * deltaTime);
            }
        }

        public void Dispose() {}
    }
}