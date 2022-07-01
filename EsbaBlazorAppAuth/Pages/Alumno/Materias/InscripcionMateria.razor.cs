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
    public partial class InscripcionMateria : _BasePage
    {

        [Parameter]
        public string MateriaId { set; get; } = "";
        [Parameter] 
        public EventCallback OnClose { get; set; }
        [Parameter]
        public bool Visible { get; set; } = false;
        [Parameter]
        public AlumnoCarrera Carrera { set; get; } = default!;
        [Inject]
        public IEmailSender _emailSender {get; set;} = default!;
        private bool _add;
        private bool busy = false;
        private string _cuatrimestreAnio = "";

        public class MateriaInscripcion
        {
            public string? MATERIA { get; set; }
            public int FERRCOD { get; set; }
            public string? FERRWEB { get; set; }
        }

        public class Turnos
        {
            public string Id { get; set; } = default!;
            public string Name { get; set; } = default!;
        }

        private List<Turnos> _turnos = new List<Turnos>();
        public InscripcionesMaterias? _inscripcion;

        public MateriaInscripcion? _materiaInscripcionValida;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            string query = "";

            if (firstRender)
            {
                try
                {
                    _turnos.Add(new Turnos { Id = "1", Name = "Mañana" });
                    _turnos.Add(new Turnos { Id = "2", Name = "Tarde" });
                    _turnos.Add(new Turnos { Id = "4", Name = "Noche" });
                    
            
                    _add = false;

                    using (var dbContext = await appSession.DbContextCreate())
                    {
                        _materiaInscripcionValida = await dbContext.QuerySingleOrDefaultAsync<MateriaInscripcion>(@$"select FERRCOD, FERRWEB, MATERIA from ALUMNOS A, XXX_INSC_VALMAT(A.cod_alu, '{Carrera.IdCarrera}', '{MateriaId}', 'I') WHERE A.INDICE={Carrera.IdAlumno}");
                        _inscripcion = await dbContext.InscripcionesMaterias.Where(b => b.AlumnoId==Carrera.IdAlumno && b.CarreraId==Carrera.IdCarrera && b.MateriaId == MateriaId && b.Estado=="PENDIENTE").FirstOrDefaultAsync();
                        _cuatrimestreAnio = await dbContext.QuerySingleOrDefaultAsync<string>(@$"select FCUATRIM from XXX_NUMCUATANIO('{Carrera.IdCarrera}')");
                    }
                   
                    if (_inscripcion == null)
                    {
                        _add = true;
                        _inscripcion = new InscripcionesMaterias();
                        _inscripcion.Id = 0;
                        _inscripcion.AlumnoId = Carrera.IdAlumno;
                        _inscripcion.CarreraId = Carrera.IdCarrera;
                        _inscripcion.FechaInscripcion = DateTime.Now;
                        _inscripcion.MateriaId = MateriaId;
                        _inscripcion.Turno = "";
                        _inscripcion.Estado = "PENDIENTE";
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
            if (_inscripcion != null)
            { 
                try
                {

                    using (var dbContext = await appSession.DbContextCreate())
                    {

                        if (_add)
                        {
                            if (_inscripcion.Turno == null)
                            {
                                throw new Exception("Seleccione una turno");
                            }
                            
                            dbContext.InscripcionesMaterias.Add(_inscripcion);
                        }
                        else
                        {
                           
                            if (_inscripcion.Turno == null)
                            {
                                throw new Exception("Seleccione una turno");
                            }
                            
                            dbContext.InscripcionesMaterias.Update(_inscripcion);
                        }

                        await dbContext.SaveChangesAsync();
                        
                        await _emailSender.SendEmailAsync(
                            appSession.UserEmail,                        
                            "Inscripcion Cursada",
                            @$"
                            <span style=""font-size:12pt;"">
                            Inscripcion a la cursada de la materia <b>{_materiaInscripcionValida!.MATERIA}</b>. <br>
                            <b>Alumno</b>: {Carrera.NombreAlumno} <br>
                            <b>Carrera</b>: {Carrera.NombreCarrera} <br>                       
                            <b>Cuatrimestre/Año:</b> {_cuatrimestreAnio.Substring(0,1)+"/"+_cuatrimestreAnio.Substring(1,2)} <br>
                            <b>Turno</b>: {(_turnos.Find(x => x.Id == _inscripcion.Turno) ?? new Turnos()).Name} <br><br>
                            </span>
                            <span style=""font-size:15pt;color:red"">
                                <b>IMPORTANTE</b>:<br>
                                * De corresponder, abonar el/los permiso/s de examen/es<br>
                                * La inscripción a la/s materia/s es PROVISORIA hasta corroborar situación administrativa y académica<br>
                            </span>  
                            ");
                        
                    }

                    toastService.ShowSuccess("Grabado");
                    await DialogClose();
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

        private async Task EliminarClick()
        {
            if (_inscripcion != null) 
            {    
                try
                {

                    using (var dbContext = await appSession.DbContextCreate())
                    {

                     
                        dbContext.InscripcionesMaterias.Remove(_inscripcion);
                    

                        await dbContext.SaveChangesAsync();
                    }
                    _add = true;
                    _inscripcion = new InscripcionesMaterias();
                    toastService.ShowSuccess("Inscripcion eliminada");
                    await DialogClose();
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

        private async Task DialogClose()
        {
            if (OnClose.HasDelegate)
            {
                await OnClose.InvokeAsync();
            }
        }

    }
}