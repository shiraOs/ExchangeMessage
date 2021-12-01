using System;
using System.Text.Json.Serialization;

namespace Sender
{
    public class MessageDetail
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        [JsonIgnore]
        public int Age { get; set; }

        public string Profession { get; set; }
    }
}
