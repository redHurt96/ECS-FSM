using _EcsFsm.Components.Movement;
using _EcsFsm.Providers;
using Scellecs.Morpeh;
using UnityEngine;
using static UnityEngine.Input;
using static UnityEngine.Physics;

namespace _EcsFsm.Services
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        
        private Camera _camera;

        private void Start() => 
            _camera = Camera.main;

        private void Update()
        {
            if (GetMouseButtonDown(0)
                && Raycast(_camera.ScreenPointToRay(mousePosition), out RaycastHit hit)
                && hit.collider.CompareTag("Ground")) 
                Spawn(hit.point);
        }

        private void Spawn(Vector3 point)
        {
            GameObject instance = Instantiate(_prefab, point, Quaternion.identity);
            ref Position position = ref instance
                .GetComponent<PositionProvider>()
                .Entity
                .GetComponent<Position>();
            
            position.Value = point + Vector3.up * .5f;
        }
    }
}
