using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

  
namespace EsbaBlazorAppAuth.Data
{
    [Table("PERMISOS_EXAMENES_WEB")]
    public class PermisoExamen
    {
        [Key]
        [Required]
        [Column("ID")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una mesa de exman")]
        [Column("MESA")]     
        public int Mesa { get; set; } = default!;
                
        [Column("LLAMADO")]         
        public int? Llamado { get; set; } = default!;
        
        [Required]
        [Column("ALUMNO_ID")] 
        public int AlumnoId { get; set; } = default!;
        
        [Required]
        [Column("CARRERA_ID")] 
        public string CarreraId { get; set; } = default!;
            
        [Column("CUATRIMESTRE_TURNO_COMISION")] 
        public string? CuatrimestreTurnoComision { get; set; } = default!;

        [Required]
        [Column("MATERIA_ID")] 
        public string MateriaId { get; set; } = default!;
        
        [Required]
        [DataType(DataType.Date)]
        [Column("FECHA_EXAMEN")] 
        public DateTime FechaExamen { get; set; } = default!;
        
        [Required]
        [DataType(DataType.Date)]
        [Column("FECHA_EMISION")]         
        public DateTime FechaEmision { get; set; } = default!;
                
    }
}

