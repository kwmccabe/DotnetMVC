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
    public class ContentController : Controller
    {
        private readonly MvcDbContext _context;
        private readonly string searchOptsKey;

        public ContentController(MvcDbContext context)
        {
            _context = context;
            searchOptsKey = "_content";
        }

        // GET: Content
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
                if (System.Enum.IsDefined(typeof(ContentStatus), status))
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
            if (Request.Query.ContainsKey("sort") && typeof(Content).GetProperty(sort) != null)
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
            IOrderedQueryable<Content> qry = _context.Content
                .Include(a => a.Template)
                .Include(a => a.Item)
                    .ThenInclude(b => b.Owner)
                .OrderBy(a => a.Keyname);

            if (qry != null && !String.IsNullOrEmpty(search_opts.Search))
            {
                if (search_opts.Search.Length < 3)
                {
                    qry = (IOrderedQueryable<Content>)qry.Where(a =>
                        a.Keyname.StartsWith(search, StringComparison.OrdinalIgnoreCase)
                        || a.Title.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)
                        );
                }
                else
                {
                    qry = (IOrderedQueryable<Content>)qry.Where(a =>
                        a.Keyname.Contains(search, StringComparison.OrdinalIgnoreCase)
                        || a.Title.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                        );
                }
            }
            if (qry != null && !String.IsNullOrEmpty(search_opts.Status) && search_opts.Status != "All")
            {
                qry = (IOrderedQueryable<Content>)qry.Where(c => c.ContentStatus.Equals(search_opts.Status, StringComparison.OrdinalIgnoreCase));
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
                    ? (IOrderedQueryable<Content>)qry.OrderByDescending(search_opts.Sort)
                    : (IOrderedQueryable<Content>)qry.OrderBy(search_opts.Sort);
            }
            // Add Offset/Limit
            if (qry != null)
            {
                qry = (IOrderedQueryable<Content>)qry
                    .Skip(search_opts.Offset)
                    .Take(search_opts.Limit);
            }

            ViewBag.ContentStatus = ContentStatusFilter(search_opts.Status);

            // Return ViewModel
            var viewModel = new ContentListModel();
            viewModel.Items = await qry.ToListAsync();
            viewModel.Options = search_opts;
            return View(viewModel);
        }

        // GET: Content/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var content = await _context.Content
                .Include(a => a.Template)
                .Include(a => a.Item)
                    .ThenInclude(b => b.Owner)
                //.Include(a => a.Item)
                    //.ThenInclude(b => b.Group)
                //.Include(a => a.Item)
                    //.ThenInclude(b => b.ItemUsers)
                        //.ThenInclude(c => c.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (content == null)
                return NotFound();

            return View(content);
        }

        // GET: Content/Create
        public IActionResult Create(string keyname = "")
        {
            ViewBag.TemplateId = TemplateSelect();
            ViewBag.ContentStatus = ContentStatusSelect();

            Content content = new Content();
            content.Keyname = keyname;
            return View(content);
        }

        // POST: Content/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Keyname,Title,Text,ContentStatus,TemplateId")] Content content)
        {
            TryValidateModel(content);
            if (ModelState.IsValid)
            {
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

                //return RedirectToAction(nameof(Index));
                return Redirect("/Content/Details/" + content.Id.ToString());
            }

            ViewBag.TemplateId = TemplateSelect(content.TemplateId);
            ViewBag.ContentStatus = ContentStatusSelect(content.ContentStatus);
            return View(content);
        }

        // GET: Content/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var content = await _context.Content
                .Include(a => a.Item)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (content == null)
                return NotFound();

            ViewBag.TemplateId = TemplateSelect(content.TemplateId);
            ViewBag.ContentStatus = ContentStatusSelect(content.ContentStatus);
            return View(content);
        }

        // POST: Content/Edit/5
        // NOTE: update Bind properties list following model changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Keyname,Title,Text,ContentStatus,TemplateId")] Content content)
        {
            if (id != content.Id || !ContentExists(id))
                return NotFound();

            TryValidateModel(content);
            if (ModelState.IsValid)
            {
                // core item
                var item = await _context.Item.FindAsync(id);
                item.Keyname = content.Keyname;
                item.ModificationDate = DateTime.Now;
                _context.Update(item);

                // content item
                _context.Update(content);

                // commit
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
                return Redirect("/Content/Details/" + content.Id.ToString());
            }

            ViewBag.TemplateId = TemplateSelect(content.TemplateId);
            ViewBag.ContentStatus = ContentStatusSelect(content.ContentStatus);
            return View(content);
        }

        // GET: Content/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var content = await _context.Content
                .Include(a => a.Item)
                    .ThenInclude(b => b.Owner)
                .Include(a => a.Template)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (content == null)
                return NotFound();

            return View(content);
        }

        // POST: Content/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var content = await _context.Content.FindAsync(id);
            //_context.Content.Remove(content);
            var item = await _context.ContentItem.FindAsync(id);
            _context.ContentItem.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentExists(int id)
        {
            return _context.Content.Any(e => e.Id == id);
        }

        private SelectList ContentStatusFilter(object status = null)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "All Content", Value = "All" },
                    new SelectListItem { Text = "Draft Items", Value = "Draft" },
                    new SelectListItem { Text = "Complete Items", Value = "Complete" },
                    new SelectListItem { Text = "Approved Items", Value = "Approved" },
                    new SelectListItem { Text = "Hidden Items", Value = "Hidden" },
                }, "Value", "Text", status);
        }

        private SelectList ContentStatusSelect(object status = null)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Draft", Value = "Draft" },
                    new SelectListItem { Text = "Complete", Value = "Complete" },
                    new SelectListItem { Text = "Approved", Value = "Approved" },
                    new SelectListItem { Text = "Hidden", Value = "Hidden" },
                }, "Value", "Text", status);
        }

        private SelectList TemplateSelect(object templateId = null)
        {
            IOrderedQueryable<Template> qry = _context.Template
                .Include(a => a.Item)
                .OrderBy(a => a.Keyname);
            return new SelectList(qry, "Id", "Keyname", templateId);
        }

    }
}
