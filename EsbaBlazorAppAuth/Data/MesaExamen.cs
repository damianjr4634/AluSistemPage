using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data
{
    [Table("MESAS_WEB")]
    public class MesaExamen
    {
        [Key]
        [Required]
        [Column("MESA_ID")]     
        public int MesaId { get; set; } = default!;
                
        [Column("LLAMADO")]         
        public int? Llamado { get; set; } = default!;
            
        [Required]
        [Column("CARRERA_ID")] 
        public string CarreraId { get; set; } = default!;
            
        [Column("CUATRIMESTRE")] 
        public string? Cuatrimestre { get; set; } = default!;

        [Required]
        [Column("MATERIA_ID")] 
        public string MateriaId { get; set; } = default!;
        
        [Required]
        [DataType(DataType.Date)]
        [Column("FECHA_EXAMEN")] 
        public DateTime FechaExamen { get; set; } = default!;

        [Column("HORA_EXAMEN")]         
        public int? HoraExamen { get; set; } = default!;  
        
        [Column("COMISION")]         
        public int? Comision { get; set; } = default!;  

        [Column("TIPO_MESA")]         
        public string? TipoMesa { get; set; } = default!;   

        [Column("PERMISO_EXAMEN")]         
        public int? PermisoExamen { get; set; } = default!;   

    }
}

