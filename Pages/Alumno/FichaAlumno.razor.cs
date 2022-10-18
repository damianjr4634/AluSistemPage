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
        public AlumnoCarrera _carrera = new AlumnoCarrera(); 
        private int _alumnoSelectedId;
        private EditContext editContext;    
        public string _photo {get; set;} = default!;
        protected override async Task OnAfterRenderAsync(bool firstRender)        
        {
            
            if (firstRender)
            {    
                try
                {
                    if (appSession.Carreras == null || appSession.Carreras.Count == 0)
                    {
                        await appSession.LoadInformationUser();
                    }
                   
                    if (appSession.Carreras != null & appSession!.Carreras!.Count != 0)
                    {
                        _carrera = appSession!.Carreras[0];
                    }
                    await LoadInfo();
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

        private async Task LoadInfo()
        {
            string query = "";
            try
            {
                if (appSession.Carreras == null || appSession.Carreras.Count == 0)
                {
                    await appSession.LoadInformationUser();
                }
                
                _alumnoSelectedId = _carrera.IdAlumno;

                _add = false;
                using (var dbContext = await appSession.DbContextCreate())
                {    
                    _listSexo = await dbContext.Sexo.ToListAsync();
                    _listEstadoCivil = await dbContext.EstadoCivil.ToListAsync();               

                    _alumno = await dbContext.Alumnos.Where(r => r.Id == _carrera.IdAlumno).SingleOrDefaultAsync(); 
                    if (_alumno == null) {
                        _add = true;
                        query = @$"select INDICE AS ID, NOM_APE AS NOMBRE, APELLIDO, COD_ALU AS CODIGOALUMNO, CARRE AS CARRERAID,
                                            SEXO, NACIONAL AS NACIONALIDAD, EST_CIV AS ESTADOCIVIL, FEC_NAC AS FECHANACIMIENTO,
                                            LUG_NAC AS LUGARNACIMIENTO, PCIA_NAC AS PROVINCIANACIMIENTO, DOMI AS DOMICILIO,
                                            LOCALI AS LOCALIDAD, COD_POS AS CODIGOPOSTAL, TELE AS TELEFONO, MAIL, CELULAR,
                                            'S' AS CAMBIO, CURRENT_TIMESTAMP AS ULTIMAACTUALIZACION
                                    from ALUMNOS WA
                                    WHERE INDICE = {_carrera.IdAlumno}";
                        
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

        private async void carreraOnChange(AlumnoCarrera _value)
        {
            //_carreraSelectedIndex = appSession.Carreras.FindIndex(x => x.IdCarrera == (string)value);
            //_carrera = appSession.Carreras[_carreraSelectedIndex];
            _carrera = _value;
            await LoadInfo();
        }
    }
}