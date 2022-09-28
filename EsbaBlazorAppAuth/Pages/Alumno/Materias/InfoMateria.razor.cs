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
        public string CuatrimestreAnio { get; set; } = "";
        [Parameter]
        public AlumnoCarrera Carrera { set; get; } = default!;
        [Inject]
        public IEmailSender _emailSender { get; set; } = default!;
        private bool busy = false;

        private class NotasDto
        {
            public double? PrimerNota { get; set; }
            public double? SegundaNota { get; set; }
            public double? TerceraNota { get; set; }
            public double? Recuperatorio { get; set; }
            public double? MarzoNota { get; set; }
            public double? DiciembreNota { get; set; }
            public int? Inasistencias { get; set; }
            public int? Justificadas { get; set; }
            public string? ferrmsg { get; set; }
        }

        private NotasDto _notas = new NotasDto();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {           

            if (firstRender)
            {
                try
                {
                    using (var dbContext = await appSession.DbContextCreate())
                    {
                        _notas = await dbContext.QuerySingleOrDefaultAsync<NotasDto>(@$" Select NOTA1 as primernota, 
                                                                                            NOTA2 as segundanota, 
                                                                                            NOTA3 as tercernota, RECUP as recuperatorio, 
                                                                                            MARZO as marzonota, DICIE as diciembrenota, 
                                                                                            inasi as inasistencias,
                                                                                            justi as justificadas,
                                                                                            FERRMSG
                                                                                       from WEB_NET_NOTASMATERIAS(@IDALUMNO, @CODALU, @IDCARRERA, @IDMATERIA)
                                                                                ",
                                                                                new
                                                                                {
                                                                                    codalu = Carrera.DocumentoAlumno,
                                                                                    idcarrera = Carrera.IdCarrera,
                                                                                    idalumno = Carrera.IdAlumno,
                                                                                    idmateria = MateriaId

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

        private async Task DialogClose()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
        }

    }
}