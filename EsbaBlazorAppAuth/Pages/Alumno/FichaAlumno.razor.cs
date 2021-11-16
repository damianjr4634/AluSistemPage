using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsbaBlazorAppAuth.Data;
using EsbaBlazorAppAuth.Data.Tablas;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Blazored.Toast.Services;
using FirebirdSql.Data.FirebirdClient;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using Radzen;

namespace EsbaBlazorAppAuth.Pages.Alumno
{
    public partial class FichaAlumno : _BasePage
    {       
        public string _carreraId = default!;       
        public AlumnoWeb _alumno = new AlumnoWeb();
        public List<Sexo> _listSexo = new List<Sexo>();
        public List<EstadoCivil> _listEstadoCivil = new List<EstadoCivil>();
        public bool busy = false;
        private EditContext editContext;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {    
                try
                {
                    if (appSession.UserCode == 0)
                    {
                        await appSession.LoadInformationUser();
                    }
                    
                    using (var dbContext = await appSession.DbContextCreate())
                    {    
                        _listSexo = await dbContext.Sexo.ToListAsync();
                        _listEstadoCivil = await dbContext.EstadoCivil.ToListAsync();               

                        _alumno = await dbContext.AlumnosWeb.Where(r => r.Id == appSession.UserCode).SingleOrDefaultAsync(); 
                        if (_alumno == null) {
                            _alumno = await dbContext.AlumnosWeb.Where(r => r.Id == appSession.UserCode).SingleOrDefaultAsync();                             
                            _alumno = new AlumnoWeb();
                            _alumno.Id = appSession.UserCode; 
                            _alumno.CodigoAlumno =  await dbContext.QuerySingleValueOrDefaultAsync<string>("select cod_alu from alumnos a where a.indice=@id",
                                                                        new
                                                                        {
                                                                            id = appSession.UserCode                                                                            
                                                                        });                      
                        }
                    }                                    
                    StateHasChanged();
                }
                catch (Exception err)
                {
                    if (err.InnerException != null && err.InnerException.Message != "")
                    {
                        toastService.ShowError(err.InnerException.Message);
                    }
                    else
                    {
                        toastService.ShowError(err.Message);
                    }
                }        
            }
        }

        private void HandleValidSubmit()
        {
            busy = true;   
     
            try
            {               
                if (editContext != null && editContext.Validate())
                {
            
                   
                }
                busy = false; 
            }
            catch( Exception err)
            {
                busy = false; 
                if (err.InnerException != null && err.InnerException.Message != "")
                {
                    toastService.ShowError(err.InnerException.Message);
                }
                else
                {
                    toastService.ShowError(err.Message);
                }
            }

        }
    }
}