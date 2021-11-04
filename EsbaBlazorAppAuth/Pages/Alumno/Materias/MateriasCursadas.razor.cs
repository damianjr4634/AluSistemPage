using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsbaBlazorAppAuth.Data;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using Radzen;

namespace EsbaBlazorAppAuth.Pages.Alumno.Materias
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
            public DateTime? fecha { set; get; }
            public string anual { set; get; } = "";
            public int cutuco { set; get; }
            public string htmlcolor { set; get; } = "white";
            public string htmlfontcolor { set; get; } = "black";
            public string vencim { set; get; } = "";
            public bool permiso { set; get;}
            public bool inscripcion { set; get; }
        }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                using (var dbContext = new ApplicationDbContext())
                {
                    _materiasCursadas = await dbContext.QueryAsync<MateriaCursadaDto>(@$"select cuatrim, codmat, descripci, condicion, cuaanio, 
                                                                                        nota, fecha, anual, cutuco, vencim, htmlcolor, htmlfontcolor,
                                                                                        permiso, inscripcion
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
                
                args.Attributes.Add("style", $"background-color: {(args.Data.htmlcolor)};");            
            }

        }

        void PedirPermiso(string aCodMat){
            
        }
    }
}