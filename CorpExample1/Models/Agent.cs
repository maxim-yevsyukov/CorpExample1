using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CorpExample1.Models
{
    public class Agent
    {
        public int _Id { get; set; }
        public string Name { get; set; }
        public int Tier { get; set; }
    }
}
