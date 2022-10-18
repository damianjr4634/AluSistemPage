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
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EsbaBlazorAppAuth.Pages.Alumno.Materias
{
    public partial class Inasistencias : _BasePage
    {
        [Parameter]
        public string MateriaId { set; get; } = "";
        [Parameter]
        public EventCallback OnClose { get; set; }
        [Parameter]
        public bool Visible { get; set; } = false;
        [Parameter]
        public AlumnoCarrera Carrera { set; get; } = default!;
        [Parameter]
        public string CuatrimestreAnio { get; set; } = "";
        [Inject]
        public IEmailSender _emailSender { get; set; } = default!;
        private bool busy = false;
        private int SelectedAnioValue;
        public class AniosDto
        {
            public int Anio { get; set; }
        }

        public class FaltasDto
        {
            [DataType(DataType.Date)]
            public DateTime Fecha { get; set; }
            public string Descri { get; set; } = default!;
            public double Cantid { get; set; }
        }
        private List<FaltasDto> _faltasFechas = new List<FaltasDto>();
        private List<AniosDto> _anios = new List<AniosDto>();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            string query = "";

            if (firstRender)
            {
                try
                {
                    using (var dbContext = await appSession.DbContextCreate())
                    {
                        _anios = await dbContext.QueryAsync<AniosDto>(@$" SELECT DISTINCT EXTRACT( YEAR FROM F.FECHA) as anio
                                                                          FROM FALTAS F
                                                                          WHERE F.CARRERA=@carrera AND F.CODALU=@codalu
                                                                          order by 1
                                                                                ",
                                                                                new
                                                                                {
                                                                                    codalu = Carrera.DocumentoAlumno,
                                                                                    carrera = Carrera.IdCarrera
                                                                                });                       
                    }
                    if (_anios.Count > 0)
                    {
                        SelectedAnioValue= _anios[0].Anio;
                        await OnLocalChange();
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

        private async Task OnLocalChange()
        {
            string _cuaanio;
            try
            {
                _cuaanio = "1"+Convert.ToString(SelectedAnioValue-2000);
                using (var dbContext = await appSession.DbContextCreate())
                {
                    _faltasFechas = await dbContext.QueryAsync<FaltasDto>(@$"select fecha, descri, cantid
                                                                                from xxx_faltas_faltas(@carrera,null,@cuaanio, @codalu, null)
                                                                                where cantid<>0
                                                                                order by 1
                                                                                ",
                                                                            new
                                                                            {
                                                                                codalu = Carrera.DocumentoAlumno,
                                                                                carrera = Carrera.IdCarrera,
                                                                                cuaanio = _cuaanio
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
        private async Task DialogClose()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
        }

    }
}