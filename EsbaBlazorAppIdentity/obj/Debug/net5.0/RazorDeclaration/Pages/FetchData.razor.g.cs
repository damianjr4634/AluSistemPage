// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace EsbaBlazorAppIdentity.Pages
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
#nullable restore
#line 3 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/Pages/FetchData.razor"
using EsbaBlazorAppIdentity.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/fetchdata")]
    public partial class FetchData : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 39 "/home/damian/git/AluSistemPage/EsbaBlazorAppIdentity/Pages/FetchData.razor"
       
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService.GetForecastAsync(DateTime.Now);
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private WeatherForecastService ForecastService { get; set; }
    }
}
#pragma warning restore 1591