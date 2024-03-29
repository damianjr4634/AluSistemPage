using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EsbaBlazorAppAuth.Data;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using EsbaBlazorAppAuth.Areas.Identity.Data;

namespace EsbaBlazorAppAuth.Services
{

    // guia de contruccion de clases
    // http://danderson.io/code/c-developer-guidelines/#class-layout
    // https://stackoverflow.com/questions/150479/order-of-items-in-classes-fields-properties-constructors-methods

    sealed public partial class AppSession : IDisposable
    {
        public event Func<Task>? SessionChangedEvent;
        private AuthenticationStateProvider _authenticationStateProvider;
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private bool _loaded;
        private string _userId = "";
        private List<AlumnoCarrera> _carreras;
        //private int _userCode;
        private bool _userIsAdmin;
        private string _userEmail = "";
        private bool _emailConfirmed;
        private string _userName;
        private string _userType;

        // contructor
        // **********************************************************************************************
        public AppSession(AuthenticationStateProvider authenticationStateProvider, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager)
        {
            _contextAccessor = httpContextAccessor;
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // implementamos DisposeAsync
        // https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync#disposeasync-and-disposeasynccore
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        // properties
        // **********************************************************************************************
        public bool Loaded => _loaded;
        public string UserId => _userId;
        public List<AlumnoCarrera> Carreras => _carreras;
        //public int UserCode => _userCode;
        public string UserName => _userName;
        public bool UserIsAdmin => _userIsAdmin;
        public string UserEmail => _userEmail;
        public string UserType => _userType;
        public bool EmailConfirmed => _emailConfirmed;
        public IHttpContextAccessor ContextAccessor => _contextAccessor;

        public async Task LoadStateAsync()
        {
            if (!_loaded)
            {

                /*var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var appUser = await _userManager.GetUserAsync(authState.User);

                if (appUser != null)
                {
                    _userId = appUser.Id;
                    _userEmail = appUser.Email;
                    _emailConfirmed = appUser.EmailConfirmed;
                    _userName = appUser.UserName;

                }
                */
                _loaded = true;
            }
        }

        public async Task LoadUser()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var appUser = await _userManager.GetUserAsync(authState.User);

            if (appUser != null)
            {
                _userId = appUser.Id;
                _userEmail = appUser.Email;
                _emailConfirmed = appUser.EmailConfirmed;
                _userName = appUser.UserName;
                _userType = appUser.AccountType ?? "A";

            }
        }
        public async Task LoadInformationUser()
        {
            //if ((_loaded || _userCode == 0) && !string.IsNullOrEmpty(_userEmail)) 
            if (string.IsNullOrEmpty(_userEmail))
            {
                await LoadUser();
            }

            if (!string.IsNullOrEmpty(_userEmail))
            {
                try
                {
                    using (var dbContext = await DbContextCreate())
                    {
                        _carreras = await dbContext.QueryAsync<AlumnoCarrera>($@"select ferrmsg, cod_alu as DocumentoAlumno, id_alumno as IdAlumno, NOMBRE as NombreAlumno,
                                                       carre as IdCarrera, distancia as Adistancia, descarre as NombreCarrera, baja, NOMBRE||' '||descarre as AlumnoApellidoCarrera,
                                                       tipo_carrera as TipoCarrera
                                                from WEB_NET_LOGIN(@mail, @tipo, 1)",
                                                                                new
                                                                                {
                                                                                    mail = _userEmail,
                                                                                    tipo = _userType
                                                                                });
                    }
                }
                catch (Exception err)
                {
                    throw;
                }
            }
        }
        public async Task<ApplicationDbContext> DbContextCreate()
        {
            var dbContext = new ApplicationDbContext();
            dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            await dbContext.Database.OpenConnectionAsync();
            return dbContext;
        }
    }
}