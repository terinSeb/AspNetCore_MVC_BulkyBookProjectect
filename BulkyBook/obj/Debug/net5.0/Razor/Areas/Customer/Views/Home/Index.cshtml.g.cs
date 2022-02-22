#pragma checksum "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea1c6f33458c2017733dc944f57d704a3238eccb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Customer_Views_Home_Index), @"mvc.1.0.view", @"/Areas/Customer/Views/Home/Index.cshtml")]
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
#nullable restore
#line 1 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\_ViewImports.cshtml"
using BulkyBook;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\_ViewImports.cshtml"
using BulkyBook.Models.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea1c6f33458c2017733dc944f57d704a3238eccb", @"/Areas/Customer/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"772f79224864f71b3f583e984145299dbe75211c", @"/Areas/Customer/Views/_ViewImports.cshtml")]
    public class Areas_Customer_Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BulkyBook.Models.Product>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row pb-3 backgroundWhite\">\r\n");
#nullable restore
#line 4 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
     foreach (var product in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <div class=""col-lg-3 col-md-6"">
            <div class=""row p-2"">
                <div class=""col-12  p-1"" style=""border:1px solid #008cba; border-radius: 5px;"">
                    <div class=""card"" style=""border:0px;"">
                        <img");
            BeginWriteAttribute("src", " src=\"", 393, "\"", 416, 1);
#nullable restore
#line 10 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
WriteAttributeValue("", 399, product.ImageUrl, 399, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top rounded\" />\r\n                        <div class=\"pl-1\">\r\n                            <p class=\"card-title h5\"><b style=\"color:#2c3e50\">");
#nullable restore
#line 12 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
                                                                         Write(product.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                            <p class=\"card-title text-primary\">by <b>");
#nullable restore
#line 13 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
                                                                Write(product.Author);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                        </div>\r\n                        <div style=\"padding-left:5px;\">\r\n                            <p>List Price: <strike><b");
            BeginWriteAttribute("class", " class=\"", 833, "\"", 841, 0);
            EndWriteAttribute();
            WriteLiteral(">$ ");
#nullable restore
#line 16 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
                                                            Write(product.ListPrice.ToString("0,00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></strike></p>\r\n                        </div>\r\n                        <div style=\"padding-left:5px;\">\r\n                            <p style=\"color:maroon\">As low as: <b");
            BeginWriteAttribute("class", " class=\"", 1053, "\"", 1061, 0);
            EndWriteAttribute();
            WriteLiteral(">$ ");
#nullable restore
#line 19 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
                                                                        Write(product.Price100.ToString("0,00"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></p>\r\n                        </div>\r\n                    </div>\r\n                    <div>\r\n                        DETAILS BUTTON\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 28 "D:\AspNetCoreMVC_BulkyBookProject\BulkyBook\BulkyBook\Areas\Customer\Views\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BulkyBook.Models.Product>> Html { get; private set; }
    }
}
#pragma warning restore 1591
