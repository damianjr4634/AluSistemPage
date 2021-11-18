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
        private AlumnoDto? _alumno;
        public Carrera _carrera = new Carrera(); 
        private class AlumnoDto
        {
           /*[Column("APELLIDO")]*/
            public string Apellido {get; set;} = default!;
            /*[Column("NOMBRE")]*/
            public string Nombre {get; set;} = default!;
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
                        _alumno = await dbContext.QuerySingleOrDefaultAsync<AlumnoDto>("select apellido, nom_ape as nombre from alumnos where indice=@indice",
                            new {
                                indice = appSession.UserCode
                            });
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
    }
}
