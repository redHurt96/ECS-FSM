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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AttackSystem))]
    public sealed class AttackSystem : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake() =>
            _filter = World.Filter
                .With<NearToAttack>()
                .With<Attack>()
                .With<EnemyReference>()
                .Without<AttackCooldown>()
                .Build();

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref Attack attack = ref entity.GetComponent<Attack>();
                
                World.TryGetEntity(entity.GetComponent<EnemyReference>().EntityId, out Entity enemy);

                ref Health enemyHealth = ref enemy.GetComponent<Health>();
                enemyHealth.Value -= attack.Damage;

                ref AttackCooldown cooldown = ref entity.AddComponent<AttackCooldown>();
                cooldown.Left = attack.Cooldown;
            }
        }
    }
}