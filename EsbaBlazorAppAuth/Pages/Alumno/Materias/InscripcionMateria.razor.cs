using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsbaBlazorAppAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Blazored.Toast.Services;
using FirebirdSql.Data.FirebirdClient;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using Radzen;

namespace EsbaBlazorAppAuth.Pages.Alumno.Materias
{
    public partial class InscripcionMateria : _BasePage
    {
 
        [Parameter]
        public string MateriaId { set; get; } = "";
        private bool _add;
        private bool busy = false;

        public class MateriaInscripcion
        {
            public string? MATERIA { get; set; }
            public int FERRCOD { get; set; }
            public string? FERRWEB { get; set; }           
        }

        public class Inscripcion
        {
            public string? Turno { get; set; }            
        }

        public Inscripcion _inscripcion = new Inscripcion();

        public MateriaInscripcion? _materiaInscripcion;

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
                        _materiaInscripcion = await dbContext.QuerySingleOrDefaultAsync<MateriaInscripcion>(@$"select FERRCOD, FERRWEB, MATERIA from ALUMNOS A, XXX_INSC_VALMAT(A.cod_alu, '{appSession.Carreras[0].Id}', '{MateriaId}', 'I') WHERE A.INDICE={appSession.UserCode}");                        
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
                        /*if (_mesaInscripto == null)
                        {
                            throw new Exception("Seleccione una mesa");
                        }
                        dbContext.PermisosExamen.Add(_permiso);*/
                    }
                    else
                    {
                        //si pasa aca es porque quito el permiso y lo agrego devuelta   
                        /*if (_permiso.Id == 0 && _permisoOrg != null && _permisoOrg.Id != 0)
                        {
                            dbContext.PermisosExamen.Remove(_permisoOrg);
                            if (_mesaInscripto != null)
                            {
                                dbContext.PermisosExamen.Add(_permiso);
                            }
                        }
                        else
                        {
                            dbContext.PermisosExamen.Update(_permiso);
                        }*/
                    }

                    await dbContext.SaveChangesAsync();
                }

                toastService.ShowSuccess("Grabado");
                busy = false;
            }
            catch (Exception err)
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