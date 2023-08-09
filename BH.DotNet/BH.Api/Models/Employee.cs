using Newtonsoft.Json;

namespace BH.Api.Models
{
    public class Employee
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Description { get; set; }
    }
}
