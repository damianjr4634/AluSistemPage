using System;
using System.Threading.Tasks;
using EsbaBlazorAppAuth.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EsbaBlazorAppAuth.Areas.Identity.Data;

namespace EsbaBlazorAppAuth.Services
{
    public class AuthSignInManager<TUser> : SignInManager<ApplicationUser> where TUser : class
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public string? pepe { get; set; }
        public AuthSignInManager(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemeProvider,
            IUserConfirmation<ApplicationUser> confirmation,
            ApplicationDbContext dbContext
            )
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemeProvider, confirmation)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        }
        public async Task<SignInResult> _PasswordSignInAsync(string userName, string password, string accountType, bool isPersistent, bool lockoutOnFailure)
        {
            var user = UserManager.FindByEmailAsync(userName).Result;
            //SignInResult _res;

            try
            {
                var _carreras = await _dbContext.QueryAsync<AlumnoCarrera>($@"
                                                select ferrmsg, cod_alu as DocumentoAlumno, id_alumno as IdAlumno, NOMBRE as NombreAlumno,
                                                       carre as IdCarrera, descarre as NombreCarrera, baja
                                                from WEB_NET_LOGIN(@mail, @tipo, 0)",
                                                    new
                                                    {
                                                        mail = userName,
                                                        tipo = accountType
                                                    });
                
                if (_carreras == null || _carreras.Count == 0)
                {
                    return SignInResult.NotAllowed;
                }

            }
            catch
            {
                return SignInResult.NotAllowed;
            }

            return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }
}
