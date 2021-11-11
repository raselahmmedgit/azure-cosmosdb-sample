using Newtonsoft.Json;

namespace lab.LocalCosmosDbApp.EntityModels
{
    public class Persons //: Base
    {
        public Persons()
        {
        }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "firstname")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }

    }
}

