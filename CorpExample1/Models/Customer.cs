using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace CorpExample1.Models
{
    public class Customer
    {
        public Guid Guid { get; set; }
        public int Agent_Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string City { get; set; }
        public string State { get; set; }


        public Customer() { }

        public Customer(JsonReader reader)
        {
            UpdateFromJsonReader(reader);
        }

        protected virtual JToken UpdateFromJsonReader(JsonReader reader)
        {
            var token = JToken.ReadFrom(reader);

            Guid = new Guid(token["guid"].ToString());
            Agent_Id = Int32.Parse(token["agent_id"].ToString());
            LastName = token["name"]["last"].ToString();
            FirstName = token["name"]["first"].ToString();
            City = token["address"].ToString().Split(", ")[1];
            State = token["address"].ToString().Split(", ")[2];

            return token;
        }
    }

    public class CustomerConverter : JsonConverter
    {
        private readonly Type[] _types;

        public CustomerConverter()
        {
            _types = new[] { typeof(Customer) };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new Customer(reader);
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}
