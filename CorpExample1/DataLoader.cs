using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CorpExample1.Models;
//using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace CorpExample1
{
    public class DataLoader
    {
        public static void Load(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AgentContext>();
            if (context.AgentDetails.Any())
                return;

            string agentsJson = File.ReadAllText("./wwwroot/data/agents.json");
            string customersJson = File.ReadAllText("./wwwroot/data/customers.json");

            var agents = JsonConvert.DeserializeObject<List<Agent>>(agentsJson);
            var agentDetails = JsonConvert.DeserializeObject <List<AgentDetail>>(agentsJson);
            var customers = JsonConvert.DeserializeObject<List<Customer>>(customersJson, new CustomerConverter());
            var customerDetails = JsonConvert.DeserializeObject<List<CustomerDetail>>(customersJson, new CustomerDetailConverter());

            customerDetails.ForEach(cd =>
            {
                cd.FirstName = customers.First(x => x.Guid == cd.Guid).FirstName;
                cd.LastName = customers.First(x => x.Guid == cd.Guid).LastName;
                cd.City = customers.First(x => x.Guid == cd.Guid).City;
                cd.State = customers.First(x => x.Guid == cd.Guid).State;
            });

            context.Agents.AddRange(agents);
            context.AgentDetails.AddRange(agentDetails);
            context.CustomerDetails.AddRange(customerDetails);

            context.SaveChanges();
        }
    }
}
