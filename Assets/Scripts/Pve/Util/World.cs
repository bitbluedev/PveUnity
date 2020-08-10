using Assets.Scripts.Pve.Handlers;
using Pve.GameEntity;
using Pve.GameEntity.Enemy;
using Pve.Handlers;
using UnityEngine.UI;

namespace Pve.Util
{
    internal static class World
    {
        public static string Text { get; set; }
        public static string UserInput { get; set; }

        static World()
        {
            World.AdventureHandlerInstance = new AdventureHandler();
            World.CombatHandlerInstance = new CombatHandler();
            World.ExitHandlerInstance = new ExitHandler();
            World.InventoryHandlerInstance = new InventoryHandler();
            World.MainHandlerInstance = new MainHandler();
            World.NewGameHandlerInstance = new NewGameHandler();
            World.RestHandlerInstance = new RestHandler();
            World.WorldEventHandlerInstance = new WorldEventHandler();
            World.BlankHandlerInstance = new BlankHandler();
        }

        public static StateHandlerBase CurrentState { get; set; }
        public static bool Exit { get; set; }
        public static Player Player { get; set; }
        public static Enemy Enemy { get; internal set; }

        #region handlers
        public static AdventureHandler AdventureHandlerInstance { get; set; }
        public static CombatHandler CombatHandlerInstance { get; set; }
        public static ExitHandler ExitHandlerInstance { get; set; }
        public static InventoryHandler InventoryHandlerInstance { get; set; }
        public static MainHandler MainHandlerInstance { get; set; }
        public static NewGameHandler NewGameHandlerInstance { get; set; }
        public static RestHandler RestHandlerInstance { get; set; }
        public static WorldEventHandler WorldEventHandlerInstance { get; set; }
        public static BlankHandler BlankHandlerInstance { get; set; }
        #endregion handlers
    }
}