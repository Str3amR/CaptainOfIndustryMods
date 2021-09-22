using Mafi.Collections;
using Mafi.Localization;
using Mafi.Unity.UiFramework;
using Mafi.Unity.UiFramework.Components;
using Mafi.Unity.UserInterface;

namespace CaptainOfIndustryMods.CheatMenu
{
    public class CheatMenuView : WindowView
    {
        private readonly Dict<string, Lyst<CheatItem>> _cheatItems;
        private StackContainer _itemsContainer;

        public CheatMenuView(Dict<string, Lyst<CheatItem>> cheatItems) : base("CheatMenu", noHeader: true)
        {
            _cheatItems = cheatItems;
        }

        protected override void BuildWindowContent()
        {
            // Buttons container
            _itemsContainer = Builder
                .NewStackContainer("Buttons container")
                .SetStackingDirection(StackContainer.Direction.TopToBottom)
                .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                .SetItemSpacing(15f)
                .SetInnerPadding(Offset.Top(20f) + Offset.Bottom(10f))
                //TODO: PutTo require a reference to UnityEngine.CoreModule, probably because of it's IUiElement parameter containing a GameObject property
                .PutTo(GetContentPanel());

            Builder.NewTitle("Title")
                .SetText("Cheat menu")
                .SetPreferredSize()
                .AppendTo(_itemsContainer, offset: Offset.LeftRight(20));

            var largest = 0f;

            foreach (var outer in _cheatItems)
            {
                var innerContainer = Builder
                    .NewStackContainer("Buttons container")
                    .SetStackingDirection(StackContainer.Direction.LeftToRight)
                    .SetSizeMode(StackContainer.SizeMode.StaticDirectionAligned)
                    .SetItemSpacing(15f)
                    .SetInnerPadding(Offset.All(20f))
                    .AppendTo(_itemsContainer);

                foreach (var item in outer.Value)
                    Builder.NewBtn("button")
                        .SetButtonStyle(item.UsingReflection ? Style.Global.DangerBtn : Style.Global.GeneralBtn)
                        .SetText(new LocStrFormatted(item.Title))
                        //TODO: May be a bug, the OnClick method has a parameter of type UnityEngine.AudioSource which require a reference to the unity library
                        .OnClick(item.Action)
                        .AppendTo(innerContainer);

                var width = innerContainer.GetDynamicWidth();
                if (width > largest) largest = width;
            }

            //_itemsContainer.UpdateSizesFromItems();
            SetContentSize(largest, _itemsContainer.GetDynamicHeight());
            PositionSelfToCenter();
        }
    }
}