using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace _EcsFsm.Components.Stats
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [System.Serializable]
    public struct Health : IComponent
    {
        public float Value;
    }
}