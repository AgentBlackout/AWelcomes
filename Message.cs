using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWelcomes
{
    /// <summary>
    /// Represents a message to be displayed on join.
    /// </summary>
    public class Message
    {
        [JsonProperty("content")]
        public string Content;

        [JsonProperty("period")]
        public int Period;

        [JsonProperty("monospaced")]
        public bool Monospaced;

        public Message() {
            Content = "DEFAULT MESSAGE";
            Period = 2;
        }

        public Message(string message, int period, bool mono = false)
        {
            Content = message;
            Period = period;
            Monospaced = mono;
        }


    }
}
