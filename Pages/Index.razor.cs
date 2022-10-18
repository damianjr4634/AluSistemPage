using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using Radzen;
using EsbaBlazorAppAuth.Data;

namespace EsbaBlazorAppAuth.Pages
{
    public partial class Index : _BasePage
    {      
       
        private AlumnoDto? _alumno = new AlumnoDto();
        
        public AlumnoCarrera _carrera = new AlumnoCarrera(); 
        private int _alumnoSelectedId;
        private string _messageError = "";

        private class AlumnoDto
        {          
            public string Apellido {get; set;} = default!;            
            public string Nombre {get; set;} = default!;
            public string ConstanciaAnalitico {get; set;} = default!;
            [DataType(DataType.Date)] public DateTime? FechaContanciaTituloTramite {get; set;}
            public string ConstanciaTituloTramite {get; set;} = default!;
            [DataType(DataType.Date)] public DateTime? FechaAptoFisico {get; set;}
            public string AptoFisico {get; set;} = default!;
            public string Foto {get; set;} = default!;
            public string PartidaNacimiento {get; set;} = default!;
            public string FotocopiaNominaPase {get; set;} = default!;
            public string Documento {get; set;} = default!;

        }

        private async Task LoadInfo()
        {
            try
            {
                if (appSession.Carreras == null || appSession.Carreras.Count == 0)
                {
                    await appSession.LoadInformationUser();
                }
                
                _alumnoSelectedId = _carrera.IdAlumno;

                using (var dbContext = await appSession.DbContextCreate())
                {
                    _alumno = await dbContext.QuerySingleOrDefaultAsync<AlumnoDto>(@$"select trim(apellido) as apellido, trim(nom_ape) as nombre,
                                                                                                iif(CTT='*','S','N') as ConstanciaTituloTramite,
                                                                                                FAPTFIS as AptoFisico, FAPTFEC as FechaAptoFisico,
                                                                                                FFOTO as foto ,FPARNAC as PartidaNacimiento, 
                                                                                                NOMIPASE as FotocopiaNominaPase,DNI as Documento,
                                                                                                CA as ConstanciaAnalitico
                                                                                        from alumnos 
                                                                                        where indice=@indice",
                        new {
                            indice = _carrera.IdAlumno
                        });
                }

                //_alumno.FechaAptoFisico.Value.AddMonths(3) > DateTime.Now
                
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
                        await LoadInfo();
                    }
                    else
                    {
                        _messageError="No esta habilitado para ver informacion en esta pagina. Solicite acceso";
                    }
                    
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

        private async void carreraOnChange(AlumnoCarrera _value)
        {
            //_carreraSelectedIndex = appSession.Carreras.FindIndex(x => x.IdCarrera == (string)value);
            //_carrera = appSession.Carreras[_carreraSelectedIndex];
            _carrera = _value;
            await LoadInfo();
        }
    }
}
