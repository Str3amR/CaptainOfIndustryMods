using Mafi.Collections;
using Mafi.Core.Research;

namespace CaptainOfIndustryMods.CheatMenu.CheatProviders
{
    public class ResearchCheatProvider : ICheatProvider
    {
        private readonly ResearchManager _researchManager;

        public ResearchCheatProvider(ResearchManager researchManager)
        {
            _researchManager = researchManager;
        }

        //TODO Something to unlock the entire research tree

        public Lyst<CheatItem> Cheats => new Lyst<CheatItem>
        {
            new CheatItem
            {
                Title = "Finish current research",
                UsingReflection = false,
                // Sometimes the devs make it easier than others!
                Action = () => _researchManager.Cheat_FinishCurrent(),
            },
        };
    }
}