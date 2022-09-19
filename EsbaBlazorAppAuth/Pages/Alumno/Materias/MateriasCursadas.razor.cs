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
        //public AlumnoCarrera _carrera = new AlumnoCarrera();   
        //public string _alumnoId = default!;
        public string _carreraNombre = default!;
        public bool _inscFinal = false;
        public bool _inscMateria = false;
        public string _materiaFinal = "";
        public string _materiaInscripcion = "";
        public string _infoMateriaId = "";
        public bool _infoViewMateria = false;
        public string _infoDesMateria = "";
        public AlumnoCarrera _carrera = new AlumnoCarrera();
        private int _alumnoSelectedId;
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
            public bool permiso { set; get; }
            public bool inscripcion { set; get; }
            public bool tienepermiso { set; get; }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    if (appSession.Carreras == null || appSession.Carreras.Count == 0)
                    {
                        await appSession.LoadInformationUser();
                    }

                    if (appSession.Carreras != null & appSession!.Carreras!.Count != 0)
                    {
                        _carrera = appSession!.Carreras[0];
                    }
                    await LoadMaterias();
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

        protected async Task LoadMaterias()
        {
            try
            {
                using (var dbContext = await appSession.DbContextCreate())
                {
                    _alumnoSelectedId = _carrera.IdAlumno;

                    /*_alumnoId = await dbContext.QuerySingleValueOrDefaultAsync<string>(@$"select cod_alu 
                                                                                from alumnos a                                                                             
                                                                                where a.indice=@codalu",
                                                                                new
                                                                                {
                                                                                    codalu = _carrera.IdAlumno                                                                                   
                                                                                });*/

                    _materiasCursadas = await dbContext.QueryAsync<MateriaCursadaDto>(@$"select cuatrim, codmat, descripci, condicion, cuaanio, 
                                                                                        nota, fecha, anual, cutuco, vencim, htmlcolor, htmlfontcolor,
                                                                                        permiso, inscripcion, tienepermiso
                                                                                from xxx_constancia_terciaria(@codalu,@carrera)",
                                                                                new
                                                                                {
                                                                                    codalu = _carrera.DocumentoAlumno,
                                                                                    carrera = _carrera.IdCarrera
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

        void CellRender(DataGridCellRenderEventArgs<MateriaCursadaDto> args)
        {
            if (args.Column.Property == "condicion")
            {

                args.Attributes.Add("style", $"background-color: {(args.Data.htmlcolor)};");
            }

        }

        void PedirPermiso(string aCodMat)
        {

        }

        private async Task InscripcionMateriaClose()
        {
            _materiaInscripcion = "";
            _inscMateria = false;
            await LoadMaterias();
        }
        private async Task InscripcionFinalClose()
        {
            _materiaFinal = "";
            _inscFinal = false;
            await LoadMaterias();
        }

        private async Task InfoMateriaClose()
        {
            _infoMateriaId = "";
            _infoDesMateria = "";
            _infoViewMateria  = false;
        }

        private async void carreraOnChange(AlumnoCarrera _value)
        {
            //_carreraSelectedIndex = appSession.Carreras.FindIndex(x => x.IdCarrera == (string)value);
            //_carrera = appSession.Carreras[_carreraSelectedIndex];
            _carrera = _value;
            await LoadMaterias();
        }
    }

}