using System;
using System.Threading.Tasks;
using EsbaBlazorAppAuth.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EsbaBlazorAppAuth.Services
{
    public class AuthSignInManager<TUser> : SignInManager<IdentityUser> where TUser : class
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IHttpContextAccessor _contextAccessor;
        public AuthSignInManager(
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<IdentityUser>> logger,
            IAuthenticationSchemeProvider schemeProvider,
            IUserConfirmation<IdentityUser> confirmation,
            ApplicationDbContext dbContext
            )
            :base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemeProvider, confirmation)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            
        }
        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool rememberMe, bool shouldLockout)
        {
            var user = UserManager.FindByEmailAsync(userName).Result;
            //SignInResult _res;

            try
            {
                   
                var _carreras = await _dbContext.QueryAsync<AlumnoCarrera>($@"select a.carre as IdCarrera, c.descarre as NombreCarrera, a.indice as IdALumno, a.baja,
                                                                                a.cod_alu as DocumentoAlumno, a.nom_ape||', '||a.apellido as NombreAlumno  
                                                                from alumnos a
                                                                join carrera c on c.carre=a.carre 
                                                                where a.mail=@mail and fusuweb='S'",
                                                                        new
                                                                        {
                                                                            mail = userName,
                                                                            baja = "N"
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

            return await base.PasswordSignInAsync(userName, password, rememberMe, shouldLockout);
       }
    }
}
