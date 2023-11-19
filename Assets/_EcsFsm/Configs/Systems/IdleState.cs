using _EcsFsm.Components.AI;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace _EcsFsm.Configs.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class IdleStateProvider : MonoProvider<IdleState>
    {
    }
}