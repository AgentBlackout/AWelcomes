using Atlas;
using Harmony;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AWelcomes
{
    [MetadataAttribute(
        "Welcome Messages",
        "Displays a welcome message for users",
        "AgentBlackout", "Ashe"
    )]
    public class WelcomesMod : Mod<WelcomeSettings>
    {
        private readonly HarmonyInstance _harmony; //Harmony for patching methods

        public static WelcomesMod Instance;

        public WelcomesMod(ModLoadInfo data) : base(data)
        {
            Instance = this;
            _harmony = HarmonyInstance.Create(Id);

            //Add a hook to the end of the authentication method so
            //we only try to add the rainbow controller after we're
            //sure that the user has a uid set.
            _harmony.Patch(
                AccessTools.Method(
                    typeof(ServerRoles), nameof(ServerRoles.CallCmdServerSignatureComplete)
                    ),
                null,
                new HarmonyMethod(
                    AccessTools.Method(typeof(WelcomesMod), nameof(CallCmdServerSignatureCompletePostFix))
                    )
            );
        }

        /// <summary>
        /// Attempts to add a welcome controller to a player.
        /// </summary>
        /// <param name="player"></param>
        public void AddWelcomeController(GameObject player)
        {
            var component = player.GetComponent<WelcomeController>();

            if (component != null) return;
            //Component already exists on the player, they already have a RainbowTag.

            var messages = Settings.GetMessagesForPlayer(player);

            if (messages.Count() == 0) return;

            player.AddComponent<WelcomeController>();
            player.GetComponent<WelcomeController>().Messages = messages;
        }

        /// <summary>
        /// Callback for patching
        /// </summary>
        /// <param name="__instance"></param>
        private static void CallCmdServerSignatureCompletePostFix(ServerRoles __instance)
        {
            Instance.AddWelcomeController(__instance.gameObject);
        }

        /// <summary>
        /// Gets called when the Mod unloads. Cancels any on going welcomes.
        /// </summary>
        protected override void Destroy()
        {
            foreach (var player in PlayerManager.players)
            {
                Object.Destroy(player.GetComponent<WelcomeController>());
            }

            base.Destroy();
        }
    }
}
