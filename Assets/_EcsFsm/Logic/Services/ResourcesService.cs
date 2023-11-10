using _EcsFsm.Components.Core;
using _EcsFsm.ViewHooks;
using Leopotam.EcsLite;
using UnityEngine;
using static UnityEngine.Object;
using static UnityEngine.Resources;

namespace _EcsFsm.Services
{
    public class ResourcesService : IResourcesService
    {
        private PositionHook _characterResource;

        public void CreateFor(int entity, EcsWorld world)
        {
            _characterResource ??= Load<PositionHook>("CharacterView");

            EcsPool<Position> pool = world.GetPool<Position>();
            Vector3 position = pool.Get(entity).Value;
            
            Instantiate(_characterResource, position, Quaternion.identity)
                .Setup(entity, pool);
        }
    }
}
