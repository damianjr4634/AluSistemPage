#pragma checksum "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/Shared/MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "05aece59e7b654d21cd5dfe402ab600e0f9385e0"
// <auto-generated/>
#pragma warning disable 1591
namespace EsbaBlazorAppIdentity.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.AspNetCore.Identity.UI.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.EntityFrameworkCore;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using EsbaBlazorAppIdentity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/_Imports.razor"
using EsbaBlazorAppIdentity.Shared;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "page");
            __builder.AddAttribute(2, "b-kri6cyjmz1");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "sidebar");
            __builder.AddAttribute(5, "b-kri6cyjmz1");
            __builder.OpenComponent<EsbaBlazorAppIdentity.Shared.NavMenu>(6);
            __builder.CloseComponent();
            __builder.CloseElement();
            __builder.AddMarkupContent(7, "\r\n\r\n    ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "main");
            __builder.AddAttribute(10, "b-kri6cyjmz1");
            __builder.AddMarkupContent(11, "<div class=\"top-row px-4\" b-kri6cyjmz1><a href=\"https://docs.microsoft.com/aspnet/\" target=\"_blank\" b-kri6cyjmz1>About</a></div>\r\n\r\n        ");
            __builder.OpenElement(12, "div");
            __builder.AddAttribute(13, "class", "content px-4");
            __builder.AddAttribute(14, "b-kri6cyjmz1");
            __builder.AddContent(15, 
#nullable restore
#line 14 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/Shared/MainLayout.razor"
             Body

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
