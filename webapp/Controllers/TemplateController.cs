using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using webapp.Data;
using webapp.Extensions;
using webapp.Models;

namespace webapp.Controllers
{
    [Authorize]
    public class TemplateController : Controller
    {
        private readonly MvcDbContext _context;
        private readonly string searchOptsKey;

        public TemplateController(MvcDbContext context)
        {
            _context = context;
            searchOptsKey = "_template";
        }

        // GET: Template
        public async Task<IActionResult> Index(
            string search = "",
            string status = "",
            string sort = "",
            string order = "",
            int limit = 0,
            int pagenum = 0
            )
        {
            // Init/Update SearchSession
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(searchOptsKey))) {
                HttpContext.Session.Set<SearchSession>(searchOptsKey, new SearchSession());
            }
            var search_opts = HttpContext.Session.Get<SearchSession>(searchOptsKey);

            if (Request.Query.ContainsKey("search")) {
                search_opts.Search = search;
            }
            if (Request.Query.ContainsKey("status")) {
                search_opts.Status = "All";
                if (System.Enum.IsDefined(typeof(TemplateStatus), status))
                {
                    search_opts.Status = status;
                }
            }
            if (Request.Query.ContainsKey("order"))
            {
                if (order.ToLower() == "asc" || order.ToLower() == "desc")
                    { search_opts.Order = order.ToLower(); }
                else
                    { search_opts.Order = (search_opts.Order == "asc") ? "desc" : "asc"; }
            }
            if (Request.Query.ContainsKey("sort") && typeof(Template).GetProperty(sort) != null)
            {
                if (search_opts.Sort != sort)
                {
                    search_opts.Sort = sort;
                    search_opts.Order = "asc";
                }
            }
            if (limit > 0) {
                search_opts.Limit = limit;
            }
            if (pagenum > 0) {
                search_opts.PageNum = pagenum;
            }
            search_opts.Offset = (search_opts.PageNum - 1) * search_opts.Limit;
            //HttpContext.Session.Set<SearchSession>(searchOptsKey, search_opts);

            // Build query
            IOrderedQueryable<Template> qry = _context.Template
                .Include(a => a.Item)
                    .ThenInclude(b => b.Owner)
                .OrderBy(a => a.Keyname);

            if (qry != null && !String.IsNullOrEmpty(search_opts.Search))
            {
                if (search_opts.Search.Length < 3)
                {
                    qry = (IOrderedQueryable<Template>)qry.Where(a =>
                        a.Keyname.StartsWith(search, StringComparison.OrdinalIgnoreCase)
                        || a.Title.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)
                        );
                }
                else
                {
                    qry = (IOrderedQueryable<Template>)qry.Where(a =>
                        a.Keyname.Contains(search, StringComparison.OrdinalIgnoreCase)
                        || a.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                        );
                }
            }
            if (qry != null && !String.IsNullOrEmpty(search_opts.Status) && search_opts.Status != "All")
            {
                qry = (IOrderedQueryable<Template>)qry.Where(c => c.TemplateStatus.Equals(search_opts.Status, StringComparison.OrdinalIgnoreCase));
            }

            // Execute Count()
            search_opts.ItemCount = qry.Count();
            search_opts.PageCount = (int)Math.Ceiling((double)search_opts.ItemCount / search_opts.Limit);

            // Finalize SearchSession
            if (search_opts.Offset > search_opts.ItemCount)
            {
                search_opts.PageNum = 1;
            }
            if (search_opts.PageNum > search_opts.PageCount)
            {
                search_opts.PageNum = search_opts.PageCount;
            }
            search_opts.Offset = (search_opts.PageNum - 1) * search_opts.Limit;

            // Save SearchSession
            HttpContext.Session.Set<SearchSession>(searchOptsKey, search_opts);

            // Add OrderBy
            if (qry != null)
            {
                qry = (search_opts.Order == "desc")
                    ? (IOrderedQueryable<Template>)qry.OrderByDescending(search_opts.Sort)
                    : (IOrderedQueryable<Template>)qry.OrderBy(search_opts.Sort);
            }
            // Add Offset/Limit
            if (qry != null)
            {
                qry = (IOrderedQueryable<Template>)qry
                    .Skip(search_opts.Offset)
                    .Take(search_opts.Limit);
            }

            ViewBag.TemplateStatus = TemplateStatusFilter(search_opts.Status);

            // Return ViewModel
            var viewModel = new TemplateListModel();
            viewModel.Items = await qry.ToListAsync();
            viewModel.Options = search_opts;
            return View(viewModel);
        }

        // GET: Template/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var template = await _context.Template
                .Include(a => a.Content)
                .Include(a => a.Item)
                    .ThenInclude(b => b.Owner)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (template == null)
                return NotFound();

            return View(template);
        }

        // GET: Template/Create
        public IActionResult Create(string keyname = "")
        {
            ViewBag.TemplateStatus = TemplateStatusSelect();

            Template template = new Template();
            template.Keyname = keyname;
            return View(template);
        }

        // POST: Template/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Keyname,Title,Text")] Template template)
        {
            TryValidateModel(template);
            if (ModelState.IsValid)
            {
                // create Item
                var templateItem = new TemplateItem
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

                //return RedirectToAction(nameof(Index));
                return Redirect("/Template/Details/" + template.Id.ToString());
            }

            ViewBag.TemplateStatus = TemplateStatusSelect(template.TemplateStatus);
            return View(template);
        }

        // GET: Template/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var template = await _context.Template
                .Include(a => a.Item)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (template == null)
                return NotFound();

            ViewBag.TemplateStatus = TemplateStatusSelect(template.TemplateStatus);
            return View(template);
        }

        // POST: Template/Edit/5
        // NOTE: update Bind properties list following model changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Keyname,Title,Text")] Template template)
        {
            if (id != template.Id || !TemplateExists(id))
                return NotFound();

            TryValidateModel(template);
            if (ModelState.IsValid)
            {
                // core item
                var item = await _context.Item.FindAsync(id);
                item.Keyname = template.Keyname;
                item.ModificationDate = DateTime.Now;
                _context.Update(item);

                // template item
                _context.Update(template);

                // commit
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
                return Redirect("/Template/Details/" + template.Id.ToString());
            }

            ViewBag.TemplateStatus = TemplateStatusSelect(template.TemplateStatus);
            return View(template);
        }

        // GET: Template/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var template = await _context.Template
                .Include(a => a.Item)
                    .ThenInclude(b => b.Owner)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (template == null)
                return NotFound();

            return View(template);
        }

        // POST: Template/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var template = await _context.Template.FindAsync(id);
            //_context.Template.Remove(template);
            var item = await _context.TemplateItem.FindAsync(id);
            _context.TemplateItem.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Template.Any(e => e.Id == id);
        }

        private SelectList TemplateStatusFilter(object status = null)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "All Templates", Value = "All" },
                    new SelectListItem { Text = "Public Items", Value = "Public" },
                    new SelectListItem { Text = "Private Items", Value = "Private" },
                    new SelectListItem { Text = "Hidden Items", Value = "Hidden" },
                }, "Value", "Text", status);
        }

        private SelectList TemplateStatusSelect(object status = null)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Public", Value = "Public" },
                    new SelectListItem { Text = "Private", Value = "Private" },
                    new SelectListItem { Text = "Hidden", Value = "Hidden" },
                }, "Value", "Text", status);
        }

    }
}
