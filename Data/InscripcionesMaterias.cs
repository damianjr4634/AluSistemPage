using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

  
namespace EsbaBlazorAppAuth.Data
{
    [Table("INSCRIPCIONES_MATERIAS_WEB")]
    public class InscripcionesMaterias
    {
        [Key]
        [Required]
        [Column("ID")] 
        public int Id { get; set; }

        [Required]
        [Column("ALUMNO_ID")] 
        public int AlumnoId { get; set; } = default!;
        
        [Required]
        [DataType(DataType.Date)]
        [Column("FECHA_INSCRIPCION")] 
        public DateTime FechaInscripcion { get; set; } = default!;

        [Column("TURNO")]         
        public string Turno { get; set; } = default!;
    
        [Required]
        [Column("MATERIA_ID")] 
        public string MateriaId { get; set; } = default!;

        [Required]
        [Column("CARRERA_ID")] 
        public string CarreraId { get; set; } = default!;

        [Column("ESTADO")]         
        public string Estado { get; set; } = default!;
                
    }
}

