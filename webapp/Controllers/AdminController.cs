using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using webapp.Extensions;
using webapp.Models;

namespace webapp.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        public AdminController()
        {
            //HttpContext.Session.LoadAsync();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "IsEdit")]
        [Route("status")]
        [Route("admin/status")]
        public IActionResult Status()
        {
            ViewBag.Message = "A message from the AdminController";
            return View();
        }

        [Authorize(Policy = "IsAdmin")]
        [Route("info")]
        [Route("admin/info")]
        public IActionResult Info()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";

            return View();
        }

        [Route("admin/setsession")]
        public IActionResult SetSession(
            string search = "",
            string sort = "",
            string order = "",
            int limit = 0,
            int page = 0
            )
        {
            HttpContext.Session.LoadAsync();
            var nameKey = "_name";
            var dateKey = "_date";
            var searchKey = "_item";

            // string
            HttpContext.Session.SetString(nameKey, "The Doctor");
            var name = HttpContext.Session.GetString(nameKey);

            // DateTime object
            HttpContext.Session.Set<DateTime>(dateKey, DateTime.Now);
            var date = HttpContext.Session.Get<DateTime>(dateKey);

            // SearchSession object
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(searchKey)))
            {
                HttpContext.Session.Set<SearchSession>(searchKey, new SearchSession());
            }
            var search_opts = HttpContext.Session.Get<SearchSession>(searchKey);

            if (Request.Query.ContainsKey("search"))
            {
                search_opts.Search = search;
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
            if (limit > 0)
            {
                search_opts.Limit = limit;
            }
            if (page > 0)
            {
                search_opts.PageNum = page;
            }
            HttpContext.Session.Set<SearchSession>(searchKey, search_opts);


            return Content("SET: name=" + name + ", date=" + date.ToString() + ", search=" + search_opts.ToString());
        }

        [Route("admin/getsession")]
        public IActionResult GetSession()
        {
            HttpContext.Session.LoadAsync();
            var nameKey = "_name";
            var dateKey = "_date";
            var searchKey = "_item";

            var name = HttpContext.Session.GetString(nameKey);
            var date = HttpContext.Session.Get<DateTime>(dateKey);
            var search = HttpContext.Session.Get<SearchSession>(searchKey);
            return Content("GET: name=" + name + ", date=" + date.ToString() + ", search=" + search.ToString());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
