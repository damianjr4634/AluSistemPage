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
        public EsbaBlazorAppAuth.Data.Alumno _alumno = new EsbaBlazorAppAuth.Data.Alumno();
        public List<Sexo> _listSexo = new List<Sexo>();
        public List<EstadoCivil> _listEstadoCivil = new List<EstadoCivil>();
        private bool busy = false;
        private bool _add = false;
        private EditContext editContext;    
        public string _photo {get; set;} = default!;
        protected override async Task OnAfterRenderAsync(bool firstRender)        
        {
            string query = "";

            if (firstRender)
            {    
                try
                {
                    if (appSession.UserCode == 0)
                    {
                        await appSession.LoadInformationUser();
                    }
                    _add = false;
                    using (var dbContext = await appSession.DbContextCreate())
                    {    
                        _listSexo = await dbContext.Sexo.ToListAsync();
                        _listEstadoCivil = await dbContext.EstadoCivil.ToListAsync();               

                        _alumno = await dbContext.Alumnos.Where(r => r.Id == appSession.UserCode).SingleOrDefaultAsync(); 
                        if (_alumno == null) {
                            _add = true;
                            query = @$"select INDICE AS ID, NOM_APE AS NOMBRE, APELLIDO, COD_ALU AS CODIGOALUMNO, CARRE AS CARRERAID,
                                              SEXO, NACIONAL AS NACIONALIDAD, EST_CIV AS ESTADOCIVIL, FEC_NAC AS FECHANACIMIENTO,
                                              LUG_NAC AS LUGARNACIMIENTO, PCIA_NAC AS PROVINCIANACIMIENTO, DOMI AS DOMICILIO,
                                              LOCALI AS LOCALIDAD, COD_POS AS CODIGOPOSTAL, TELE AS TELEFONO, MAIL, CELULAR,
                                              'S' AS CAMBIO, CURRENT_TIMESTAMP AS ULTIMAACTUALIZACION
                                       from ALUMNOS WA
                                       WHERE INDICE = {appSession.UserCode}";
                            
                            _alumno = new EsbaBlazorAppAuth.Data.Alumno();                             
                            _alumno =  await dbContext.QuerySingleOrDefaultAsync<EsbaBlazorAppAuth.Data.Alumno>(query);                      
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

        private async Task HandleValidSubmit()
        {
            busy = true;   
     
            try
            {                               
                using (var dbContext = await appSession.DbContextCreate())
                {
                    if (_add) 
                    {
                        dbContext.Alumnos.Add(_alumno);
                    }
                    else 
                    {
                        dbContext.Alumnos.Update(_alumno);
                    }
                    
                    await dbContext.SaveChangesAsync();
                }

                toastService.ShowSuccess("Grabado");
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

        void OnProgress(UploadProgressArgs args)
        {
           
            if (args.Progress == 100)
            {
                if (!string.IsNullOrEmpty(args.Files.FirstOrDefault()!.Name)) 
                { 
                    _alumno.FotoBase64 = "";   
                    byte[] imageArray = System.IO.File.ReadAllBytes("wwwroot/uploads/"+args.Files.FirstOrDefault()!.Name);
                    _alumno.FotoBase64 = Convert.ToBase64String(imageArray);          
                }
            }
        }

        void OnComplete(UploadCompleteEventArgs args)
        {           
            //args.RawResponse;
            toastService.ShowSuccess("Foto subida");   
        }

        void OnChange(string value)
        {
            _photo = value;     
        }

        void OnError(UploadErrorEventArgs args)
        {
            toastService.ShowError(args.Message);   
        }
    }
}