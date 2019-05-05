using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
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
    public class ItemController : Controller
    {
        private readonly MvcDbContext _context;
        private readonly string searchOptsKey;

        public ItemController(MvcDbContext context)
        {
            _context = context;
            searchOptsKey = "_item";
        }

        // GET: Item
        public async Task<IActionResult> Index(
            string search = "",
            string itemtype = "",
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
            if (Request.Query.ContainsKey("itemtype")) {
                search_opts.ItemType = "All";
                if (System.Enum.IsDefined(typeof(ItemType), itemtype))
                {
                    search_opts.ItemType = itemtype;
                }
            }
            if (Request.Query.ContainsKey("order"))
            {
                if (order.ToLower() == "asc" || order.ToLower() == "desc")
                    { search_opts.Order = order.ToLower(); }
                else
                    { search_opts.Order = (search_opts.Order == "asc") ? "desc" : "asc"; }
            }
            if (Request.Query.ContainsKey("sort") && typeof(Item).GetProperty(sort) != null)
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
            IOrderedQueryable<Item> qry = _context.Item
                    .Include(a => a.Owner)
                    .OrderBy(a => a.Keyname);

            if (qry != null && !String.IsNullOrEmpty(search_opts.Search))
            {
                qry = (search_opts.Search.Length < 3)
                    ? (IOrderedQueryable<Item>)qry.Where(c => c.Keyname.StartsWith(search_opts.Search, StringComparison.OrdinalIgnoreCase))
                    : (IOrderedQueryable<Item>)qry.Where(c => c.Keyname.Contains(search_opts.Search, StringComparison.OrdinalIgnoreCase));
            }
            if (qry != null && !String.IsNullOrEmpty(search_opts.ItemType) && search_opts.ItemType != "All")
            {
                qry = (IOrderedQueryable<Item>)qry.Where(c => c.ItemType.Equals(search_opts.ItemType, StringComparison.OrdinalIgnoreCase));
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
                    ? (IOrderedQueryable<Item>)qry.OrderByDescending(search_opts.Sort)
                    : (IOrderedQueryable<Item>)qry.OrderBy(search_opts.Sort);
            }
            // Add Offset/Limit
            if (qry != null)
            {
                qry = (IOrderedQueryable<Item>)qry
                    .Skip(search_opts.Offset)
                    .Take(search_opts.Limit);
            }

            ViewBag.ItemType = ItemTypeFilter(search_opts.ItemType);

            // Return ViewModel
            var viewModel = new ItemListModel();
            viewModel.Items = await qry.ToListAsync();
            viewModel.Options = search_opts;
            return View(viewModel);
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var item = await _context.Item
                .Include(a => a.Owner)
                .Include(a => a.ItemUsers)
                    .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // GET: Item/Create
        public IActionResult Create()
        {
            ViewBag.ItemType = ItemTypeSelect();
            return View();
        }

        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public RedirectResult Create([Bind("ItemType,Keyname")] Item item)
        {
            return Redirect("/" + item.ItemType + "/Create/?keyname=" + item.Keyname);
        }

/*      [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemType,Keyname,OwnerId")] Item item)
        {
            item.CreateDate = item.ModificationDate = DateTime.Now;
            item.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            TryValidateModel(item);
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return Redirect("/Item/Details/"+ item.Id.ToString());
            }

            ViewBag.ItemType = ItemTypeSelect(item.ItemType);
            return View(item);
        }
*/

        // GET: Item/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Index));

            var item = await _context.Item.FindAsync(id);

            if (item == null)
                return NotFound();

            ViewBag.OwnerId = ItemOwnerSelect(item.OwnerId);
            ViewBag.ItemUsers = ItemUsersSelect(item);
            return View(item);
        }

        // POST: Item/Edit/5
        // NOTE: update Bind properties list following model changes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Keyname,OwnerId")] Item item)
        {
            if (id != item.Id || !ItemExists(id))
                return NotFound();

            TryValidateModel(item);
            if (ModelState.IsValid)
            {
                // core item
                item.ModificationDate = DateTime.Now;
                _context.Update(item);

                // related users
                _context.ItemUser.Where(c => c.ItemId == item.Id)
                        .ToList().ForEach(cu => _context.ItemUser.Remove(cu));

                var ownerId = Request.Form["OwnerId"];
                foreach (var selectId in Request.Form["ItemUsers"])
                {
                    var userId = selectId;
                    var userRole = (userId == ownerId) ? ItemUserRole.Admin : ItemUserRole.Editor;

                    if (!(userId.Length > 0))
                        continue;

                    var itemUser = new ItemUser
                    {
                        ItemId = item.Id,
                        UserId = userId,
                        Role = userRole
                    };
                    _context.ItemUser.Add(itemUser);
                }

                // commit
                await _context.SaveChangesAsync();

                //return RedirectToAction(nameof(Index));
                return Redirect("/Item/Details/" + item.Id.ToString());
            }

            ViewBag.OwnerId = ItemOwnerSelect(item.OwnerId);
            ViewBag.ItemUsers = ItemUsersSelect(item);
            return View(item);
        }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var item = await _context.Item
                .Include(a => a.Owner)
                .Include(a => a.ItemUsers)
                        .ThenInclude(b => b.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }

        private bool KeynameExists(string keyname, int? id = 0)
        {
            if (id != null && id > 0)
                return _context.Item.Any(e => e.Keyname == keyname && e.Id != id);
            else
                return _context.Item.Any(e => e.Keyname == keyname);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyKeyname(string Keyname, int Id)
        {
            //RegularExpression(@"^[a-z]+[a-z0-9-_.]*$")
            //_context.Item.Keyname = Regex.Replace(Keyname.ToLower(), "[^a-z0-9-_.]*", String.Empty);

            if (!Regex.IsMatch(Keyname, @"^[a-z]{1}[a-z0-9-_.]+$"))
                return Json(data: $"Keyname must lowercase, begin with a letter [a-z], and contain [a-z0-9-_.]+");

            if (KeynameExists(Keyname, Id))
                return Json(data: $"An item named '{Keyname}' already exists.");

            return Json(data: true);
        }

        private SelectList ItemTypeFilter(object itemType = null)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "All Items Types", Value = "All" },
                    new SelectListItem { Text = "Content Items", Value = "Content" },
                    new SelectListItem { Text = "Template Items", Value = "Template" },
                }, "Value", "Text", itemType);
        }

        private SelectList ItemTypeSelect(object itemType = null)
        {
            return new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem { Text = "Content", Value = "Content" },
                    new SelectListItem { Text = "Template", Value = "Template" },
                }, "Value", "Text", itemType);
        }

        private SelectList ItemOwnerSelect(object ownerId = null)
        {
            IOrderedQueryable<AppUser> qry = _context.AppUser.OrderBy(a => a.UserName);
            return new SelectList(qry, "Id", "UserName", ownerId);
        }

        private MultiSelectList ItemUsersSelect(Item item = null)
        {
            var itemUsers = (item != null) ? _context.ItemUser.Where(a => a.ItemId.Equals(item.Id)).ToList() : null;
            string[] selected = new string[itemUsers.Count];
            for (int i = 0; i < itemUsers.Count; i++)
                selected[i] = itemUsers[i].UserId;

            IOrderedQueryable<AppUser> qry = _context.AppUser.OrderBy(u => u.UserName);
            return new MultiSelectList(qry, "Id", "UserName", selected);
        }

    }
}
