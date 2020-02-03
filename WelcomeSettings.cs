using Atlas.Settings.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AWelcomes
{
    public class WelcomeSettings : ModSettings
    {
        [JsonProperty("message_map")]
        public Dictionary<string, Message> MessageMap { get; set; }

        [JsonProperty("role_map")]
        public Dictionary<string, string[]> RoleMessageMap { get; set; }

        public WelcomeSettings()
        {
            MessageMap = new Dictionary<string, Message>();
            RoleMessageMap = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Get messages for a given role name
        /// </summary>
        /// <param name="roleName">Role name as displayed in-game</param>
        /// <returns></returns>
        public IEnumerable<Message> GetMessagesForRole(string roleName)
        {
            string[] messageNames;
            if (!RoleMessageMap.TryGetValue(roleName, out messageNames)) return Enumerable.Empty<Message>();

            var messages = new List<Message>();
            foreach (var messageName in messageNames)
            {
                if (!MessageMap.TryGetValue(messageName, out var message))
                {
                    WelcomesMod.Instance.Warn($"Role {roleName} has a message called {messageName} which is invalid");
                    continue;
                }
                messages.Add(message);
            }

            return messages;
        }

        /// <summary>
        /// Get a list of messages for a given player.
        /// </summary>
        /// <param name="player">Player object </param>
        /// <returns></returns>
        public IEnumerable<Message> GetMessagesForPlayer(GameObject player)
        {
            var refHub = ReferenceHub.GetHub(player);

            var role = refHub.serverRoles.NetworkMyText.ToLower();

            var defaultMessages = GetGlobalMessages();

            return GetMessagesForRole(role).Concat(defaultMessages);

        }

        /// <summary>
        /// Gets the special messages for every user who joins.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Message> GetGlobalMessages() {
            //Any messages for '*' will be given to everyone.
            return GetMessagesForRole("*");
        }
    }
}