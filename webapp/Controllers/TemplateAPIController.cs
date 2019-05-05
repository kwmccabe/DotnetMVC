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
    [Route("api/template")]
    [ApiController]
    public class TemplateAPIController : ControllerBase
    {

        private readonly MvcDbContext _context;

        public TemplateAPIController(MvcDbContext context)
        {
            _context = context;
        }

        // GET: api/template
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateDTO>>> Get()
        {
            var qry = _context.Template
                .AsNoTracking()
                .Include(a => a.Item).ThenInclude(b => b.Owner)
                .OrderBy(a => a.Keyname)
                .Select(a => new TemplateDTO()
                {
                    Id = a.Id,
                    Keyname = a.Keyname,
                    Title = a.Title,
                    Text = a.Text,
                    TemplateStatus = a.TemplateStatus,

                    CreateDate = a.Item.CreateDate,
                    ModificationDate = a.Item.ModificationDate,
                    OwnerName = a.Item.Owner.UserName
                }
                );
            return await qry.ToListAsync();
        }

        // GET api/template/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateDTO>> Get(int id)
        {
            var template = await _context.Template
                .AsNoTracking()
                .Include(a => a.Item).ThenInclude(b => b.Owner)
                .Select(a => new TemplateDTO()
                {
                    Id = a.Id,
                    Keyname = a.Keyname,
                    Title = a.Title,
                    Text = a.Text,
                    TemplateStatus = a.TemplateStatus,
                    CreateDate = a.Item.CreateDate,
                    ModificationDate = a.Item.ModificationDate,
                    OwnerName = a.Item.Owner.UserName
                })
                .FirstOrDefaultAsync(a => a.Id == id);

            if (template == null)
                return NotFound();

            return template;
        }

        // POST api/template
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TemplateDTO>> Post(Template template)
        {
            TryValidateModel(template);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // create Item
            var templateItem = new TemplateItem()
            {
                ItemType = "Template",
                Keyname = template.Keyname,
                OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                CreateDate = DateTime.Now,
                ModificationDate = DateTime.Now
            };
            _context.Add(templateItem);
            await _context.SaveChangesAsync();

            // create Template with new Item.Id
            template.Id = templateItem.Id;
            _context.Add(template);
            await _context.SaveChangesAsync();

            var result = await _context.Template
                .AsNoTracking()
                .Include(a => a.Item).ThenInclude(b => b.Owner)
                .Select(a => new TemplateDTO()
                {
                    Id = a.Id,
                    Keyname = a.Keyname,
                    Title = a.Title,
                    Text = a.Text,
                    TemplateStatus = a.TemplateStatus,
                    CreateDate = a.Item.CreateDate,
                    ModificationDate = a.Item.ModificationDate,
                    OwnerName = a.Item.Owner.UserName
                }
                )
                .FirstOrDefaultAsync(a => a.Id == template.Id);

            return CreatedAtAction(nameof(Get),
                new { id = template.Id }, result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, Template template)
        {
            if (id != template.Id || !TemplateExists(id))
                return BadRequest();

            TryValidateModel(template);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // core item
            var item = await _context.Item.FindAsync(id);
            item.Keyname = template.Keyname;
            item.ModificationDate = DateTime.Now;
            _context.Update(item);

            // template item
            //_context.Entry(template).State = EntityState.Modified;
            _context.Update(template);

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

        private bool TemplateExists(int id)
        {
            return _context.Template.Any(e => e.Id == id);
        }

    }
}
