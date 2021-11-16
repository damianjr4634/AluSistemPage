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

namespace EsbaBlazorAppAuth.Services
{

    // guia de contruccion de clases
    // http://danderson.io/code/c-developer-guidelines/#class-layout
    // https://stackoverflow.com/questions/150479/order-of-items-in-classes-fields-properties-constructors-methods

    sealed public partial class AppSession : IDisposable
    {
        public event Func<Task>? SessionChangedEvent;
        private AuthenticationStateProvider _authenticationStateProvider;
        private UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private bool _loaded;
        private string _userId = "";
        private List<Carrera> _carreras;
        private int _userCode;
        private bool _userIsAdmin;
        private string _userEmail = "";
        private bool _emailConfirmed;
        private string _userName;

        // contructor
        // **********************************************************************************************
        public AppSession(AuthenticationStateProvider authenticationStateProvider, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, SignInManager<IdentityUser> signInManager)
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
        public List<Carrera> Carreras => _carreras;
        public int UserCode => _userCode;
        public string UserName => _userName;
        public bool UserIsAdmin => _userIsAdmin;
        public string UserEmail => _userEmail;
        public bool EmailConfirmed => _emailConfirmed;
        public IHttpContextAccessor ContextAccessor => _contextAccessor;

        public async Task LoadStateAsync()
        {
            if (!_loaded)
            {

                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var appUser = await _userManager.GetUserAsync(authState.User);

                if (appUser != null)
                {
                    _userId = appUser.Id;
                    _userEmail = appUser.Email;
                    _emailConfirmed = appUser.EmailConfirmed;
                    _userName = appUser.UserName;

                }

                _loaded = true;
            }
        }

        public async Task LoadInformationUser()
        {
            using (var dbContext = await DbContextCreate())
            {
                _userCode = await dbContext.QuerySingleValueOrDefaultAsync<int>("select indice from alumnos a where a.mail=@mail and baja=@baja",
                                                                        new
                                                                        {
                                                                            mail = _userEmail,
                                                                            baja = "N"
                                                                        });
                                                                        
                _carreras = await dbContext.QueryAsync<Carrera>($@"select a.carre as id, c.descarre as name
                                                                  from alumnos a
                                                                  join carrera c on c.carre=a.carre 
                                                                  where a.mail=@mail and baja=@baja",
                                                                        new
                                                                        {
                                                                            mail = _userEmail,
                                                                            baja = "N"
                                                                        });
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