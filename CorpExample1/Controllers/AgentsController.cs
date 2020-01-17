using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CorpExample1.Models;

namespace CorpExample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentContext _context;

        public AgentsController(AgentContext context)
        {
            _context = context;
        }

        // GET: api/Agents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAgents()
        {
            return await _context.Agents.ToListAsync();
        }

        // GET: api/Agents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Agent>> GetAgent(int id)
        {
            var agent = await _context.Agents.FindAsync(id);

            if (agent == null)
            {
                return NotFound();
            }

            return agent;
        }

        // POST: api/Agents
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Agent>> PostAgent(AgentDetail agentDetail)
        {
            Agent agent = new Agent() { Name = agentDetail.Name, Tier = agentDetail.Tier };
            _context.Agents.Add(agent);
            _context.AgentDetails.Add(agentDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAgent", new { id = agent._Id }, agent);
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e._Id == id);
        }
    }
}
