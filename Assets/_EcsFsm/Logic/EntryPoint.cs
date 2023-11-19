using _EcsFsm.Systems.AI;
using _EcsFsm.Systems.Movement;
using Scellecs.Morpeh;
using UnityEngine;
using static Scellecs.Morpeh.World;
using static UnityEngine.Time;

namespace _EcsFsm
{
    public class EntryPoint : MonoBehaviour
    {
        private SystemsGroup _systems;
        private World _world;

        private void Start()
        {
            _world = Create();
            _systems = _world.CreateSystemsGroup();

            //Movement
            _systems.AddSystem(new Move());
            _systems.AddSystem(new ReachMoveTarget());
                
            //AI
            _systems.AddSystem(new SelectEnemy());
            _systems.AddSystem(new UpdateEnemyPosition());
            _systems.AddSystem(new Attack());
        }

        private void Update() => 
            _world.Update(deltaTime);

        private void OnDestroy() => 
            _world.Dispose();
    }
}