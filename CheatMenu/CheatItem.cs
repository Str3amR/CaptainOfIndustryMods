using System;

namespace CaptainOfIndustryMods.CheatMenu
{
    public class CheatItem
    {
        public string Title { get; set; }
        public Action Action { get; set; }
        public bool UsingReflection { get; set; }
    }
}