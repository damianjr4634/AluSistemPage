using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsbaBlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Blazored.Toast.Services;
using FirebirdSql.Data.FirebirdClient;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using Radzen;

namespace EsbaBlazorApp.Pages.Alumno.Materias
{
    public partial class MateriasCursadas : _BasePage
    {
        [Parameter]
        public string CarreraId { set; get; } = "";
        [Parameter]
        public string AlumnoId { set; get; } = "";
        RadzenDataGrid<MateriaCursadaDto> materiasGrid = default!;
        private List<MateriaCursadaDto> _materiasCursadas = new List<MateriaCursadaDto>();
        public class MateriaCursadaDto
        {
            public int cuatrim { set; get; }
            public string codmat { set; get; } = "";
            public string descripci { set; get; } = "";
            public string condicion { set; get; } = "";
            public string cuaanio { set; get; } = "";
            public double nota { set; get; }
            [DataType(DataType.Date)]
            public DateTime fecha { set; get; }
            public string anual { set; get; } = "";
            public int cutuco { set; get; }
            public int color { set; get; }
            public int fontcolor { set; get; }
            public string vencim { set; get; } = "";
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    _materiasCursadas = await dbContext.QueryAsync<MateriaCursadaDto>(@$"select cuatrim, codmat, descripci, condicion, cuaanio, 
                                                                                        nota, fecha, anual, cutuco, vencim, color, fontcolor
                                                                                from xxx_constancia_terciaria(@codalu,@carrera)",
                                                                                new
                                                                                {
                                                                                    codalu = AlumnoId,
                                                                                    carrera = CarreraId
                                                                                });
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

        void CellRender(DataGridCellRenderEventArgs<MateriaCursadaDto> args)
        {
            if (args.Column.Property == "condicion")
            {
                
                args.Attributes.Add("style", $"background-color: #{(args.Data.color.ToString("X").PadRight(6,'0'))}; color: #{(args.Data.fontcolor.ToString("X").PadRight(6,'0'))};");
                /*    
                if (args.Data.condicion == "FINAL")
                {
                    args.Attributes.Add("style", $"background-color: #{(args.Data.color.ToString("X"))}; color: #{(args.Data.fontcolor.ToString("X"))};");
                }
                else if (args.Data.condicion == "ADEUDA")
                {
                    args.Attributes.Add("style", $"background-color: #{(args.Data.color.ToString("X"))}; color: #{(args.Data.fontcolor.ToString("X"))};");
                }
                else if (args.Data.condicion == "CURSANDO")
                {
                    args.Attributes.Add("style", $"background-color: #{(args.Data.color.ToString("X"))}; color: #{(args.Data.fontcolor.ToString("X"))};");
                }
                else
                {
                    args.Attributes.Add("style", $"background-color: white; color: black;");
                }*/
            }

        }
    }
}