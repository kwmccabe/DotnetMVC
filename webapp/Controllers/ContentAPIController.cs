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
    [Route("api/content")]
    [ApiController]
    public class ContentAPIController : ControllerBase
    {

        private readonly MvcDbContext _context;

        public ContentAPIController(MvcDbContext context)
        {
            _context = context;
        }

        // GET: api/content
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentDTO>>> Get()
        {
            var qry = _context.Content
                .AsNoTracking()
                .Include(a => a.Template)
                .Include(a => a.Item).ThenInclude(b => b.Owner)
                .OrderBy(a => a.Keyname)
                .Select(a => new ContentDTO()
                {
                    Id = a.Id,
                    Keyname = a.Keyname,
                    Title = a.Title,
                    Text = a.Text,
                    ContentStatus = a.ContentStatus,

                    TemplateId = a.Template.Id,
                    TemplateKeyname = a.Template.Keyname,

                    CreateDate = a.Item.CreateDate,
                    ModificationDate = a.Item.ModificationDate,
                    OwnerName = a.Item.Owner.UserName
                }
                );
            return await qry.ToListAsync();
        }

        // GET api/content/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContentDTO>> Get(int id)
        {
            var content = await _context.Content
                .AsNoTracking()
                .Include(a => a.Template)
                .Include(a => a.Item).ThenInclude(b => b.Owner)
                .Select(a => new ContentDTO()
                {
                    Id = a.Id,
                    Keyname = a.Keyname,
                    Title = a.Title,
                    Text = a.Text,
                    ContentStatus = a.ContentStatus,
                    TemplateId = a.Template.Id,
                    TemplateKeyname = a.Template.Keyname,
                    CreateDate = a.Item.CreateDate,
                    ModificationDate = a.Item.ModificationDate,
                    OwnerName = a.Item.Owner.UserName
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (content == null)
                return NotFound();

            return content;
        }

        // POST api/content
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ContentDTO>> Post(Content content)
        {
            TryValidateModel(content);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // create Item
            var contentItem = new ContentItem
            {
                ItemType = "Content",
                Keyname = content.Keyname,
                OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now
            };
            _context.Add(contentItem);
            await _context.SaveChangesAsync();

            // create Content with new Item.Id
            content.Id = contentItem.Id;
            _context.Add(content);
            await _context.SaveChangesAsync();

            var result = await _context.Content
                .AsNoTracking()
                .Include(a => a.Template)
                .Include(a => a.Item).ThenInclude(b => b.Owner)
                .Select(a => new ContentDTO()
                {
                    Id = a.Id,
                    Keyname = a.Keyname,
                    Title = a.Title,
                    Text = a.Text,
                    ContentStatus = a.ContentStatus,
                    TemplateId = a.Template.Id,
                    TemplateKeyname = a.Template.Keyname,
                    CreateDate = a.Item.CreateDate,
                    ModificationDate = a.Item.ModificationDate,
                    OwnerName = a.Item.Owner.UserName
                }
                )
                .FirstOrDefaultAsync(a => a.Id == content.Id);

            return CreatedAtAction(nameof(Get),
                new { id = content.Id }, result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, Content content)
        {
            if (id != content.Id || !ContentExists(id))
                return BadRequest();

            TryValidateModel(content);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // core item
            var item = await _context.Item.FindAsync(id);
            item.Keyname = content.Keyname;
            item.ModificationDate = DateTime.Now;
            _context.Update(item);

            // content item
            //_context.Entry(content).State = EntityState.Modified;
            _context.Update(content);

            // commit
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

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.Id == id);
        }

    }
}
