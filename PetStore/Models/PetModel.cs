using Newtonsoft.Json;

namespace PetStore.Models
{
    public class PetModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
