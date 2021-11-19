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
        public Carrera _carrera = new Carrera(); 
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

                    
                     _carrera = appSession.Carreras[0];

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
                                indice = appSession.UserCode
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
        }
    }
}
