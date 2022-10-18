// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace EsbaBlazorAppAuth
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.AspNetCore.Identity.UI.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Radzen;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Radzen.Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Blazored.Toast;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Blazored.Toast.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using Blazored.Toast.Configuration;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using EsbaBlazorAppAuth.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using EsbaBlazorAppAuth.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using EsbaBlazorAppAuth;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/_Imports.razor"
using EsbaBlazorAppAuth.Shared;

#line default
#line hidden
#nullable disable
    public partial class App : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 16 "/home/damian/git/AluSistemPage/EsbaBlazorAppAuth/App.razor"
       
    private int _sessionStateId = -1;

    // la idea es que cuando cambia algo en la sesion se dispare un statehaschanged
    // lo cual hace que todos los componentes subscriptos al cascadingValue se actualicen
    // en verdad podriamos pasar un cascading value al pedo y usar injeccion de dependencia
    // en los componentes, lo unico que hace el cascadingValue es notificar a los componentes
    // que actualicen

    // para que esto funcione hay que pasar el "render-mode" de ServerPrerendered a Server en _host.cshtml
    // https://docs.microsoft.com/en-us/aspnet/core/blazor/hosting-models?view=aspnetcore-3.1
    // esto también tiene la ventaja de que ejecuta una sola vez el OnInitializedAsync.
    protected override async Task OnInitializedAsync()
    {
        await appSession.LoadStateAsync();
    }

    // si por algun motivo no podemos cambiar el "render-mode" a Server,
    // la otra alternativa es acceder en el OnAfterRenderAsync
    // protected async override Task OnAfterRenderAsync(bool firstRender)
    //{
    //    if (firstRender)
    //    {
    //        await appSession.LoadStateAsync();
    //        StateHasChanged();
    //    }
    //}

    private async Task OnSessionUpdate()
    {
        await InvokeAsync( () => {

            StateHasChanged();
        });
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AppSession appSession { get; set; }
    }
}
#pragma warning restore 1591