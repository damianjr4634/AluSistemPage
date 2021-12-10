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
        private int? _mesaInscripto = null;
        public PermisoExamen _permiso = new PermisoExamen();
        public PermisoExamen? _permisoOrg;
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

                        _mesas = await dbContext.MesasExamen.Where(r => r.CarreraId == appSession.Carreras[0].Id && r.MateriaId == MateriaId).ToListAsync();

                        _add = true;
                        _permiso = new PermisoExamen();
                        _permisoOrg = null;
                        _mesaInscripto = null;
                        foreach (MesaExamen mesa in _mesas)
                        {
                            if (mesa.PermisoExamen != null)
                            {
                                _add = false;
                                _mesaInscripto = mesa.MesaId;
                                _permiso = await dbContext.PermisosExamen.Where(r => r.Id == mesa.PermisoExamen).SingleOrDefaultAsync();
                                _permisoOrg = _permiso;
                            }
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

        public void AsignarMesa(MesaExamen mesa)
        {
            if (mesa != null)
            {
                _mesaInscripto = mesa.MesaId;
                _permiso.Mesa = _mesaInscripto ?? mesa.MesaId;
                _permiso.AlumnoId = appSession.UserCode;
                _permiso.CarreraId = mesa.CarreraId;
                _permiso.FechaEmision = DateTime.Now;
                _permiso.FechaExamen = mesa.FechaExamen;
                _permiso.Llamado = mesa.Llamado;
                _permiso.MateriaId = mesa.MateriaId;
                _permiso.CuatrimestreTurnoComision = mesa.Cuatrimestre;
            }
        }
        public void QuitarMesa(MesaExamen mesa)
        {
            if (mesa != null)
            {
                _mesaInscripto = null;
                
                _permiso = new PermisoExamen();
                
                /*_permiso.Mesa = _mesaInscripto;
                _permiso.AlumnoId = appSession.UserCode;
                _permiso.CarreraId = mesa.CarreraId;
                _permiso.FechaEmision = DateTime.Now;
                _permiso.FechaExamen = mesa.FechaExamen;
                _permiso.Llamado = mesa.Llamado;
                _permiso.MateriaId = mesa.MateriaId;
                _permiso.CuatrimestreTurnoComision = mesa.Cuatrimestre;*/
            }
        }
    }
}