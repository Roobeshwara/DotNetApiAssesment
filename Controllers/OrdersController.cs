using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Details.Models;
using Microsoft.EntityFrameworkCore;

namespace Order_Details.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderContext _dbContext;

        public OrdersController(OrderContext dbContext)
        {
            _dbContext = dbContext;
        }
          //Get: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_dbContext.Orders == null)
            {
                return NotFound();
            }
            return await _dbContext.Orders.ToListAsync();


        }
        //Get: api/Orders/5
        [HttpGet("{Id})")]
        public async Task<ActionResult<Order>> GetOrder(int Id)
        {
            if (_dbContext.Orders == null)
            {
                return NotFound();
            }
            var order = await _dbContext.Orders.FindAsync(Id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }
        //POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        //PUT: api/Orders/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            if (!CheckOrderStatus(id)){
                return NotFound();
            }

            _dbContext.Entry(order).State = EntityState.Modified;
            try
              {
                  await _dbContext.SaveChangesAsync();
              }
            catch (DbUpdateConcurrencyException)
              {
                 if (!OrderExists(id))
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
        private bool OrderExists(long id)
        {
            return (_dbContext.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        private bool CheckOrderStatus(long id)
        {
            return (_dbContext.Orders?.Any(e => e.Id == id && e.status.ToLower() =="open")).GetValueOrDefault();
        }
        //DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (_dbContext.Orders == null)
            {
                return NotFound();
            }
            if (!CheckOrderStatus(id))
            {
                return NotFound();
            }
            var order = await _dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }




    }
}
