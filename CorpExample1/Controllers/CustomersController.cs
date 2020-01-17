using System;
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
    public class CustomersController : ControllerBase
    {
        private readonly AgentContext _context;

        public CustomersController(AgentContext context)
        {
            _context = context;
        }

        // GET: api/Customer/6b71c8c6-f87f-4518-b277-ee81fc9c24ed
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customer>>> GetCustomer(string id)
        {
           List<Customer> ret = null;
            int AgentId;
            if(Int32.TryParse(id, out AgentId)) 
            {
                return await _context.Customers.Where(c => c.Agent_Id == AgentId).ToListAsync();
            }
            else                                    // dealing with guid
            {
                var customerDetail = await _context.CustomerDetails.FindAsync(id);

                if (customerDetail != null)
                    ret = new List<Customer>() { customerDetail };
            }

            if (ret == null)
                return NotFound();
            else
                return ret;
        }

        // POST: api/Customers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDetail customerDetail)
        {
            customerDetail.City = customerDetail.Address.Split(", ")[1];
            customerDetail.State = customerDetail.Address.Split(", ")[2];
            customerDetail.LastName = customerDetail.Name.Last;
            customerDetail.FirstName = customerDetail.Name.First;

            _context.CustomerDetails.Add(customerDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customerDetail.Guid }, customerDetail);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(Guid id)
        {
            return _context.Customers.Any(e => e.Guid == id);
        }
    }
}
