using System.Collections.Generic;
using Mafi;
using Mafi.Collections;
using Mafi.Core.Input;
using Mafi.Unity.InputControl;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UserInterface;

namespace CaptainOfIndustryMods.CheatMenu
{
    public class CheatMenuController : IUnityInputController, IUnityUi
    {
        private readonly DependencyResolver _dependencyResolver;
        private CheatMenuView _view;

        //TODO: Is there a way to directly request all implementations?
        public CheatMenuController(DependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }


        public ControllerConfig Config => new ControllerConfig
        {
            DeactivateOnEscapeKey = false,
            DeactivateOnNonUiClick = false,
            AllowInspectorCursor = false,
            PreventSpeedControl = true,
        };

        public void Activate()
        {
            _view.Show();
        }

        public void Deactivate()
        {
            _view.Hide();
        }

        public bool InputUpdate(IInputScheduler inputScheduler)
        {
            return false;
        }

        public void RegisterUi(UiBuilder builder)
        {
            var cheatProviders = _dependencyResolver.ResolveAll<ICheatProvider>().Implementations;
            _view = new CheatMenuView(cheatProviders
                .Select(x => new KeyValuePair<string, Lyst<CheatItem>>(x.GetType().Name, x.Cheats))
                .ToDict());
            _view.BuildUi(builder);
        }
    }
}