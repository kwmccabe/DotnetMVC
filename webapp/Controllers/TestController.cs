using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using webapp.Extensions;
using webapp.Models;
using webapp.Util;

namespace webapp.Controllers
{

    [AllowAnonymous]
    public class TestController : Controller
    {

        public IActionResult Index()
        {
            ViewBag.Message = "A message from the TestController";
            return View();
        }

        [Route("test/itemsjs")]
        public IActionResult ItemsJS()
        {
            return View();
        }

        [Route("test/xpath")]
        public IActionResult XPath( string file = "test.xml", string xpath = "/nodes/node")
        {
            var filepath = "wwwroot/xml/" + file;
            //var xpath = "/nodes/node";

            ViewBag.nodes = XmlUtil.GetDocumentNodes(filepath, xpath);
            ViewData["nodes_ul"] = XmlUtil.NodesToUL(ViewBag.nodes.Clone());

            if (ViewBag.nodes.Count == 0)
            {
                ViewData["nodes_ul"] = "<p>NO DATA for Select(" + xpath + ")</p>";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
