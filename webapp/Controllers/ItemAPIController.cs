using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using webapp.Data;
using webapp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webapp.Controllers
{
    [Produces("application/json")]
    [Route("api/item")]
    [ApiController]
    public class ItemAPIController : ControllerBase
    {

        private readonly MvcDbContext _context;

        public ItemAPIController(MvcDbContext context)
        {
            _context = context;
        }

        // GET: api/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDTO>>> Get()
        {
            var qry = _context.Item
                .AsNoTracking()
                .Include(a => a.Owner)
                .Select(a => new ItemDTO()
                {
                    Id = a.Id,
                    ItemType = a.ItemType,
                    Keyname = a.Keyname,
                    CreateDate = a.CreateDate,
                    ModificationDate = a.ModificationDate,
                    OwnerName = a.Owner.UserName
                }
                );
            return await qry.ToListAsync();
        }

        // GET api/item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> Get(int id)
        {
            var item = await _context.Item
                .AsNoTracking()
                .Include(a => a.Owner)
                //.Include(a => a.ItemUsers).ThenInclude(b => b.User)
                .Select(a => new ItemDTO()
                {
                    Id = a.Id,
                    ItemType = a.ItemType,
                    Keyname = a.Keyname,
                    CreateDate = a.CreateDate,
                    ModificationDate = a.ModificationDate,
                    OwnerName = a.Owner.UserName
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null)
                return NotFound();

            return item;
        }

        // POST api/item
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ItemDTO>> Post(Item item)
        {
            TryValidateModel(item);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            item.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            item.CreateDate = item.ModificationDate = DateTime.Now;
            _context.Add(item);
            await _context.SaveChangesAsync();

            _context.Entry(item).Reference(a => a.Owner).Load();
            var result = new ItemDTO()
            {
                Id = item.Id,
                ItemType = item.ItemType,
                Keyname = item.Keyname,
                CreateDate = item.CreateDate,
                ModificationDate = item.ModificationDate,
                OwnerName = item.Owner.UserName
            };

            return CreatedAtAction(nameof(Get),
                new { id = item.Id }, result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, Item item)
        {
            if (id != item.Id || !ItemExists(id))
                return BadRequest();

            TryValidateModel(item);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            item.ModificationDate = DateTime.Now;
            _context.Update(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Item.FindAsync(id);

            if (item == null)
                return NotFound();

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

    }
}
