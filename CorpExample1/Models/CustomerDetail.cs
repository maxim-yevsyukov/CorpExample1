using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CorpExample1.Models
{
    public class Name
    {
        [JsonIgnore]
        public Guid Guid { get; set; }

        public string First { get; set; }

        public string Last { get; set; }
    }

    public class CustomerDetail : Customer
    {
        public int _Id { get; set; }
        public bool IsActive { get; set; }
        public string Balance { get; set; }
        public int Age { get; set; }
        public string EyeColor { get; set; }
        public Name Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Registered { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<string> Tags { get; set; }


        public CustomerDetail() { }

        public CustomerDetail(JsonReader reader)
            : base(reader) { }
        protected override JToken UpdateFromJsonReader(JsonReader reader)
        {
            var token = base.UpdateFromJsonReader(reader);

            _Id = Int32.Parse(token["agent_id"].ToString());
            IsActive = bool.Parse(token["isActive"].ToString());
            Balance = token["balance"].ToString();
            Age = Int32.Parse(token["agent_id"].ToString());
            EyeColor = token["eyeColor"].ToString();
            Name = new Name() { Guid = Guid, First = FirstName, Last = LastName };
            Company = token["company"].ToString();
            Email = token["email"].ToString();
            Phone = token["phone"].ToString();
            Address = token["address"].ToString();
            Registered = token["registered"].ToString();
            Latitude = token["latitude"].ToString();
            Longitude = token["longitude"].ToString();
            Tags = token["tags"].Select(x => x.ToString()).ToList();

            return token;
        }

    }


    public class CustomerDetailConverter : JsonConverter
    {
        private readonly Type[] _types;

        public CustomerDetailConverter()
        {
            _types = new[] { typeof(CustomerDetail) };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new CustomerDetail(reader);
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
