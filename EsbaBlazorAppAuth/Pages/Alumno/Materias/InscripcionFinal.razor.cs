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
    public partial class InscripcionFinal : _BasePage
    {
        [Parameter]
        public string MateriaId { set; get; } = "";
        private string _nombreMateria = "";
        private bool busy = false;
        private bool _add = false;
        public PermisoExamen _permiso = new PermisoExamen();
        public List<MesaExamen> _mesas = new List<MesaExamen>();
        RadzenDataGrid<MesaExamen> mesasGrid = default!;
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
                        //_listSexo = await dbContext.Sexo.ToListAsync();
                        //_listEstadoCivil = await dbContext.EstadoCivil.ToListAsync();               
                        _nombreMateria = await dbContext.QuerySingleValueOrDefaultAsync<string>(@$"select m.descripci from materias m where m.codcarre='{appSession.Carreras[0].Id}' and m.codmateri='{MateriaId}'");

                        _permiso = await dbContext.PermisosExamen.Where(r => r.AlumnoId == appSession.UserCode && r.MateriaId == MateriaId).SingleOrDefaultAsync();
                        if (_permiso == null)
                        {
                            _add = true;

                            _permiso = new PermisoExamen();
                        }
                        _mesas = await dbContext.MesasExamen.Where(r => r.CarreraId == appSession.Carreras[0].Id && r.MateriaId == MateriaId).ToListAsync();
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
                        dbContext.PermisosExamen.Add(_permiso);
                    }
                    else
                    {
                        dbContext.PermisosExamen.Update(_permiso);
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