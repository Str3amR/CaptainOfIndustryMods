using Mafi;
using Mafi.Collections;

namespace CaptainOfIndustryMods.CheatMenu
{
    [MultiDependency]
    public interface ICheatProvider
    {
        Lyst<CheatItem> Cheats { get; }
    }
}