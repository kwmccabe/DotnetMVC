#pragma checksum "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Admin/Info.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9488c09fe0387851806383e34d3d93130484b8ed"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_Info), @"mvc.1.0.view", @"/Views/Admin/Info.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Admin/Info.cshtml", typeof(AspNetCore.Views_Admin_Info))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9488c09fe0387851806383e34d3d93130484b8ed", @"/Views/Admin/Info.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eab2d8c25d8e30da56957b335f8e50d396d7165a", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_Info : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Admin/Info.cshtml"
  
    ViewData["Title"] = "Info";

#line default
#line hidden
            BeginContext(40, 4, true);
            WriteLiteral("<h1>");
            EndContext();
            BeginContext(45, 17, false);
#line 4 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Admin/Info.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(62, 103, true);
            WriteLiteral("</h1>\r\n\r\n<p>You must be logged in with \"Administrator\" privileges to see this.</p>\r\n\r\n\r\n<p>ASP.NET MVC ");
            EndContext();
            BeginContext(166, 19, false);
#line 9 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Admin/Info.cshtml"
          Write(ViewData["Version"]);

#line default
#line hidden
            EndContext();
            BeginContext(185, 4, true);
            WriteLiteral(" on ");
            EndContext();
            BeginContext(190, 19, false);
#line 9 "/Library/WebServer/dotnet-hub/hello_mvc/webapp/Views/Admin/Info.cshtml"
                                  Write(ViewData["Runtime"]);

#line default
#line hidden
            EndContext();
            BeginContext(209, 7, true);
            WriteLiteral("!</p>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
