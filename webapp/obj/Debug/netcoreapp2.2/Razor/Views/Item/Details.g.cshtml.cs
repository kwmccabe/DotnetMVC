#pragma checksum "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "aad5079c0638cd3cea4e149f5111e26495810c77"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Item_Details), @"mvc.1.0.view", @"/Views/Item/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Item/Details.cshtml", typeof(AspNetCore.Views_Item_Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/_ViewImports.cshtml"
using webapp;

#line default
#line hidden
#line 2 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/_ViewImports.cshtml"
using webapp.Extensions;

#line default
#line hidden
#line 3 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/_ViewImports.cshtml"
using webapp.Models;

#line default
#line hidden
#line 4 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/_ViewImports.cshtml"
using System.Xml.XPath;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"aad5079c0638cd3cea4e149f5111e26495810c77", @"/Views/Item/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eab2d8c25d8e30da56957b335f8e50d396d7165a", @"/Views/_ViewImports.cshtml")]
    public class Views_Item_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<webapp.Models.Item>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(26, 1, true);
            WriteLiteral("\n");
            EndContext();
#line 3 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
  
    ViewData["Title"] = "'" + Model.Keyname + "' - item details";

#line default
#line hidden
            BeginContext(98, 5, true);
            WriteLiteral("\n<h1>");
            EndContext();
            BeginContext(104, 40, false);
#line 7 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
Write(Html.DisplayFor(model => model.ItemType));

#line default
#line hidden
            EndContext();
            BeginContext(144, 157, true);
            WriteLiteral(" Item</h1>\n<nav aria-label=\"breadcrumb\">\n  <ol class=\"breadcrumb\">\n    <li class=\"breadcrumb-item\"><a href=\"/\">Home</a></li>\n    <li class=\"breadcrumb-item\">");
            EndContext();
            BeginContext(301, 31, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad5079c0638cd3cea4e149f5111e26495810c775353", async() => {
                BeginContext(323, 5, true);
                WriteLiteral("Items");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(332, 65, true);
            WriteLiteral("</li>\n    <li class=\"breadcrumb-item active\" aria-current=\"page\">");
            EndContext();
            BeginContext(397, 93, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad5079c0638cd3cea4e149f5111e26495810c776766", async() => {
                BeginContext(447, 39, false);
#line 12 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                                                                                                       Write(Html.DisplayFor(model => model.Keyname));

#line default
#line hidden
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 12 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                                                                                     WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(490, 107, true);
            WriteLiteral("</li>\n  </ol>\n</nav>\n<hr size=\"1\" />\n\n<div>\n    <dl class=\"row\">\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(598, 40, false);
#line 20 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayFor(model => model.ItemType));

#line default
#line hidden
            EndContext();
            BeginContext(638, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(696, 156, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad5079c0638cd3cea4e149f5111e26495810c779837", async() => {
                BeginContext(778, 17, true);
                WriteLiteral("\n                ");
                EndContext();
                BeginContext(796, 39, false);
#line 24 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
           Write(Html.DisplayFor(model => model.Keyname));

#line default
#line hidden
                EndContext();
                BeginContext(835, 13, true);
                WriteLiteral("\n            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 23 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                   WriteLiteral(Model.ItemType);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 23 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                                                                       WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(852, 58, true);
            WriteLiteral("\n        </dd>\n\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(911, 41, false);
#line 29 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Owner));

#line default
#line hidden
            EndContext();
            BeginContext(952, 64, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            <text>");
            EndContext();
            BeginContext(1017, 46, false);
#line 32 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
             Write(Html.DisplayFor(model => model.Owner.UserName));

#line default
#line hidden
            EndContext();
            BeginContext(1063, 2, true);
            WriteLiteral(" [");
            EndContext();
            BeginContext(1066, 43, false);
#line 32 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                                                              Write(Html.DisplayFor(model => model.Owner.Email));

#line default
#line hidden
            EndContext();
            BeginContext(1109, 66, true);
            WriteLiteral("]</text>\n        </dd>\n\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(1176, 46, false);
#line 36 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.CreateDate));

#line default
#line hidden
            EndContext();
            BeginContext(1222, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(1281, 42, false);
#line 39 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayFor(model => model.CreateDate));

#line default
#line hidden
            EndContext();
            BeginContext(1323, 57, true);
            WriteLiteral("\n        </dd>\n        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(1381, 52, false);
#line 42 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ModificationDate));

#line default
#line hidden
            EndContext();
            BeginContext(1433, 58, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            ");
            EndContext();
            BeginContext(1492, 48, false);
#line 45 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayFor(model => model.ModificationDate));

#line default
#line hidden
            EndContext();
            BeginContext(1540, 16, true);
            WriteLiteral("\n        </dd>\n\n");
            EndContext();
#line 48 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
     if (Model.ItemUsers?.Count > 0)
    {

#line default
#line hidden
            BeginContext(1599, 42, true);
            WriteLiteral("        <dt class=\"col-sm-2\">\n            ");
            EndContext();
            BeginContext(1642, 58, false);
#line 51 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
       Write(Html.DisplayNameFor(model => model.ItemUsers.First().User));

#line default
#line hidden
            EndContext();
            BeginContext(1700, 63, true);
            WriteLiteral("\n        </dt>\n        <dd class=\"col-sm-10\">\n            <ul>\n");
            EndContext();
#line 55 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
             foreach (var item in Model.ItemUsers.Select((val, idx) => new { val, idx }))
            {

#line default
#line hidden
            BeginContext(1867, 20, true);
            WriteLiteral("                <li>");
            EndContext();
            BeginContext(1888, 46, false);
#line 57 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
               Write(Html.DisplayFor(val => item.val.User.UserName));

#line default
#line hidden
            EndContext();
            BeginContext(1934, 2, true);
            WriteLiteral(" (");
            EndContext();
            BeginContext(1937, 37, false);
#line 57 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                                                                Write(Html.DisplayFor(val => item.val.Role));

#line default
#line hidden
            EndContext();
            BeginContext(1974, 7, true);
            WriteLiteral(")</li>\n");
            EndContext();
#line 58 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
            }

#line default
#line hidden
            BeginContext(1995, 31, true);
            WriteLiteral("            <ul>\n        </dd>\n");
            EndContext();
#line 61 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
    }

#line default
#line hidden
            BeginContext(2032, 28, true);
            WriteLiteral("\n    </dl>\n</div>\n<div>\n    ");
            EndContext();
            BeginContext(2060, 59, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad5079c0638cd3cea4e149f5111e26495810c7718312", async() => {
                BeginContext(2106, 9, true);
                WriteLiteral("Edit Item");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 66 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                           WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2119, 7, true);
            WriteLiteral(" |\n    ");
            EndContext();
            BeginContext(2126, 63, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad5079c0638cd3cea4e149f5111e26495810c7720595", async() => {
                BeginContext(2174, 11, true);
                WriteLiteral("Delete Item");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 67 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Item/Details.cshtml"
                             WriteLiteral(Model.Id);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2189, 7, true);
            WriteLiteral(" |\n    ");
            EndContext();
            BeginContext(2196, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "aad5079c0638cd3cea4e149f5111e26495810c7722883", async() => {
                BeginContext(2218, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2234, 8, true);
            WriteLiteral("\n</div>\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<webapp.Models.Item> Html { get; private set; }
    }
}
#pragma warning restore 1591
