using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using System.Collections.Generic;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly Repository<Order> _orderRepository;

        public OrderController(Repository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(_orderRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Create(Order order)
        {
            _orderRepository.Add(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Order_ID }, order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Order updatedOrder)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return NotFound();

            order.Status = updatedOrder.Status;
            order.OrderTotal = updatedOrder.OrderTotal;
            _orderRepository.Update(order);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return NotFound();

            _orderRepository.Delete(order);
            return NoContent();
        }
    }
}
