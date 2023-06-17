using CustomerWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }


        [HttpGet("cx")]
        [Authorize]
        public ActionResult<IEnumerable<Customer>> GetCustomer()
        {
            return Ok(_context.Customers);
        }


        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            return Ok(await _context.Customers.FindAsync(customerId));
        }


        [HttpPost("cx")]
        [Authorize(Roles ="Administrator")]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return Ok(customer);
        }


        [HttpPut("cx")]
        [Authorize(Roles = "Administrator,User")]
        public async Task<ActionResult> Update(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
