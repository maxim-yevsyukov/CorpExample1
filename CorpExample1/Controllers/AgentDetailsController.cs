using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CorpExample1.Models;

namespace CorpExample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentDetailsController : ControllerBase
    {
        private readonly AgentContext _context;

        public AgentDetailsController(AgentContext context)
        {
            _context = context;
        }

        // GET: api/AgentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgentDetail>>> GetAgentDetails()
        {
            return await _context.AgentDetails.ToListAsync();
        }

        // GET: api/AgentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AgentDetail>> GetAgentDetail(int id)
        {
            var agentDetail = await _context.AgentDetails.FindAsync(id);

            if (agentDetail == null)
            {
                return NotFound();
            }

            return agentDetail;
        }

        // PUT: api/AgentDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAgentDetail(int id, AgentDetail agentDetail)
        {
            if (id != agentDetail._Id)
            {
                return BadRequest();
            }

            _context.Entry(agentDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool AgentDetailExists(int id)
        {
            return _context.AgentDetails.Any(e => e._Id == id);
        }
    }
}
