using System;
using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Physics;

namespace _EcsFsm.Services
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector3> OnSpawnIntent; 

        private Camera _camera;

        private void Start() => 
            _camera = Camera.main;

        private void Update()
        {
            if (GetMouseButtonDown(0)
                && Raycast(_camera.ScreenPointToRay(mousePosition), out RaycastHit hit)
                && hit.collider.CompareTag("Ground")) 
                OnSpawnIntent?.Invoke(hit.point);
        }
    }
}
