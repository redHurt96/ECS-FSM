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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DecreaseCooldown))]
    public sealed class DecreaseCooldown : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake() =>
            _filter = World.Filter
                .With<AttackCooldown>()
                .Build();

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref AttackCooldown cooldown = ref entity.GetComponent<AttackCooldown>();
                cooldown.Left -= deltaTime;

                if (cooldown.Left <= 0f)
                    entity.RemoveComponent<AttackCooldown>();
            }
        }
    }
}