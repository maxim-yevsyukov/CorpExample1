using Newtonsoft.Json;

namespace CorpExample1.Models
{
    public class Phone
    {
        [JsonIgnore]
        public int _Id { get; set; }
        public string Primary { get; set; }
        public string Mobile { get; set; }
    }

    public class AgentDetail
    {
        public int _Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int Tier { get; set; }
        public Phone Phone { get; set; }
    }
}
