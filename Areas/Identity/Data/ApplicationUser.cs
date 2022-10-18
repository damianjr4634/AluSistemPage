using Microsoft.AspNetCore.Identity;
 
 
namespace EsbaBlazorAppAuth.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? AccountType { get; set; } = default;
    }
}