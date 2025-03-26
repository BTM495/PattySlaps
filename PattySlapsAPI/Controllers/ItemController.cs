using Microsoft.AspNetCore.Mvc;
using PattySlaps.Data;
using System.Collections.Generic;

namespace PattySlaps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly Repository<Item> _itemRepository;

        public ItemController(Repository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // ✅ Get all items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            var items = _itemRepository.GetAll();
            return Ok(items);
        }

        // ✅ Get a single item by ID
        [HttpGet("{id}")]
        public ActionResult<Item> GetById(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // ✅ Add a new item
        [HttpPost]
        public ActionResult<Item> Create(Item item)
        {
            _itemRepository.Add(item);
            return CreatedAtAction(nameof(GetById), new { id = item.ItemID }, item);
        }

        // ✅ Update an existing item
        [HttpPut("{id}")]
        public IActionResult Update(int id, Item updatedItem)
        {
            var item = _itemRepository.GetById(id);
            if (item == null) return NotFound();

            // Update fields
            item.Name = updatedItem.Name;
            item.CountType = updatedItem.CountType;
            item.Price = updatedItem.Price;
            item.StockQuantity = updatedItem.StockQuantity;

            _itemRepository.Update(item);
            return NoContent();
        }

        // ✅ Delete an item
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item == null) return NotFound();

            _itemRepository.Delete(item);
            return NoContent();
        }
    }
}
