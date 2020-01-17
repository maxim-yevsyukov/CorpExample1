using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CorpExample1.Models;

namespace CorpExample1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : ControllerBase
    {
        private readonly AgentContext _context;

        public CustomerDetailsController(AgentContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDetail>> GetCustomerDetail(Guid id)
        {
            var customerDetail = await _context.CustomerDetails.FindAsync(id);

            if (customerDetail == null)
            {
                return NotFound();
            }

            return customerDetail;
        }

        // PUT: api/CustomerDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerDetail(Guid id, CustomerDetail customerDetail)
        {
            if (id != customerDetail.Guid)
            {
                return BadRequest();
            }

            _context.Entry(customerDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDetailExists(id))
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


        private bool CustomerDetailExists(Guid id)
        {
            return _context.CustomerDetails.Any(e => e.Guid == id);
        }
    }
}
