using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data
{
    public class Carrera
    {
        [Key]
        public string Id { get; set; } = default!;
        public string Nombre { get; set; } = default!;
    }

    public class AlumnoCarrera
    {
        [Key]
        public string IdCarrera { get; set; } = default!;
        public string NombreCarrera { get; set; } = default!;
        public int IdAlumno { get; set; }
        public string DocumentoAlumno { get; set; } = default!; 
        public string Baja { get; set; } = "S";
    }

}

/*
create view carreras_web ( id, nombre )
as
select carre, descarre from carrera
*/