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
    public partial class InfoMateria : _BasePage
    {
        [Parameter]
        public string MateriaId { set; get; } = "";
        [Parameter] 
        public EventCallback OnClose { get; set; }
        [Parameter]
        public bool Visible { get; set; } = false;
        [Parameter]
        public string MateriaNombre { get; set; } = "";
        [Parameter]
        public AlumnoCarrera Carrera { set; get; } = default!;
        [Inject]
        public IEmailSender _emailSender {get; set;} = default!;
        private bool busy = false;
        public class FaltasDto 
        {
            public DateTime Fecha {get; set;}
            public string Descri {get; set;} = default!;
        }

        private class NotasDto 
        {
            public double? PrimerNota { get; set; }
            public double? SegundaNota { get; set; }
            public double? TerceraNota { get; set; }
            public double? Recuperatorio { get; set; }
            public double? MarzoNota { get; set; }
            public double? DiciembreNota { get; set; }
            public double? FaltasPrimerBimestre { get; set; }
            public double? FaltasSegundoBimestre { get; set; }
            public double? FaltasTercerBimestre { get; set; }
        }

        private NotasDto _notas = new NotasDto();
        private List<FaltasDto> _faltasFechas = new List<FaltasDto>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            string query = "";

            if (firstRender)
            {
                try
                {
                    using (var dbContext = await appSession.DbContextCreate())
                    {
                        _faltasFechas = await dbContext.QueryAsync<FaltasDto>(@$"select fecha, descri
                                                                                from xxx_faltas_faltas(@carrera,null,113, @codalu,@materia)
                                                                                where cantid<>0
                                                                                ",
                                                                                new
                                                                                {
                                                                                    codalu = Carrera.DocumentoAlumno,
                                                                                    carrera = Carrera.IdCarrera,
                                                                                    materia=MateriaId
                                                                                });

                        _notas.DiciembreNota=99.99;
                        _notas.MarzoNota=99.99;
                        _notas.PrimerNota=99.99;
                        _notas.TerceraNota=99.99;
                        _notas.SegundaNota=99.99;
                        _notas.Recuperatorio=99.99;
                        _notas.FaltasPrimerBimestre=99.99;
                        _notas.FaltasSegundoBimestre=99.99;
                        _notas.FaltasTercerBimestre=99.99;

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
        
        private async Task DialogClose()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
        }

    }
}