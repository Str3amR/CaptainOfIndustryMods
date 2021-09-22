using Mafi;
using Mafi.Core.Mods;
using Mafi.Core.Prototypes;
using Mafi.Unity;
using UnityEngine;

namespace CaptainOfIndustryMods.CheatMenu
{
    // The IMod implementation must be sealed!
    public sealed class CheatMenu : IMod
    {
        // Name must be alphanumeric
        public string Name => "CheatMenu";
        public bool IsUiOnly => false;

        public void Initialize(DependencyResolver resolver, bool gameWasLoaded)
        {
            var cheatMenuController = resolver.Resolve<CheatMenuController>();

            // Fetch the Input Manager
            var unityInputManager = resolver.Resolve<IUnityInputMgr>();

            // Register a global shortcut for this controller
            unityInputManager.RegisterGlobalShortcut(KeyCode.F8, cheatMenuController);
        }

        public void RegisterPrototypes(ProtoRegistrator registrator)
        {
        }

        public void RegisterDependencies(DependencyResolverBuilder depBuilder, ProtosDb protosDb, bool wasLoaded)
        {
            depBuilder.RegisterAllTypesImplementing<ICheatProvider>(typeof(CheatMenu).Assembly);
            depBuilder.RegisterDependency<CheatMenuController>().AsSelf().AsAllInterfaces();
        }
    }
}