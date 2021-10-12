using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesWebAPI.Models;

namespace SalesWebAPI.Controllers
{
    [Route("api/[controller]")] // configurable - http://localhost:00000/api/customers  (leave out the word controllers)
    [ApiController] // this is going to be a controller without its own ui, it will interact with the web to display the data - sending and receiving JSON data
    public class CustomersController : ControllerBase
    {
        private readonly AppDbContext _context; //defined to view so someone else doesn't delete or modify

        public CustomersController(AppDbContext context) // this is the constructor - the runtime passes the dbcontext to the constructor, we save it in the private read only variable, thats it
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet] // returns different kinds of data if something goes wrong
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]  // one of the things that has to make the HTTP method combined with url has to unique: 1 get method: get all, this method, if they are same, url must be different so it knows what method to execute!!!
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id); // this whole thing is get by PK, then find async, it returns. if we dont find it(customer null) can return NotFound (JSON stuff)

            if (customer == null)
            {
                return NotFound();
            }

            return customer; // obviously if found, return customer
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
