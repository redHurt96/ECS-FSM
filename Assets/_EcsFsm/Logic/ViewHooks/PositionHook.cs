using _EcsFsm.Components.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace _EcsFsm.ViewHooks
{
    public class PositionHook : MonoBehaviour
    {
        private int _entityId;
        private EcsPool<Position> _pool;

        public void Setup(int entityId, EcsPool<Position> pool)
        {
            _entityId = entityId;
            _pool = pool;
        }

        private void Update() => 
            transform.position = _pool.Get(_entityId).Value;
    }
}
