using Leopotam.EcsLite;

namespace _EcsFsm.Services
{
    public interface IResourcesService
    {
        void CreateFor(int entity, EcsWorld world);
    }
}