using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;


namespace EsbaBlazorAppAuth.Pages
{
    public class _BasePage : ComponentBase
    {
        [Inject]
        public IToastService toastService { get; set; } = default!;
        [Inject]
        public NavigationManager navigationManager { get; set; } = default!;
    }
}