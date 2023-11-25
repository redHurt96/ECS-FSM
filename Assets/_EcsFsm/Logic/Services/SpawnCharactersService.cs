using _EcsFsm.Components.Movement;
using _EcsFsm.Providers;
using Scellecs.Morpeh;
using UnityEngine;

namespace _EcsFsm.Services
{
    public class SpawnCharactersService : MonoBehaviour
    {
        [SerializeField] private InputService _inputService;
        [SerializeField] private GameObject _prefab;

        private void Start() => 
            _inputService.OnSpawnIntent += Spawn;

        private void OnDestroy() => 
            _inputService.OnSpawnIntent -= Spawn;

        private void Spawn(Vector3 point)
        {
            GameObject instance = Instantiate(_prefab, point, Quaternion.identity);
            ref Position position = ref instance.GetComponent<PositionProvider>().Entity.GetComponent<Position>();
            position.Value = point + Vector3.up * .5f;
        }
    }
}