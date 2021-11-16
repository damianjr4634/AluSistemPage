using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data.Tablas
{
    [Table("ESTADOCIVIL_WEB")]
    public class EstadoCivil {
        [Key]
        [Column("ID")]
        public string id  {get; set;} = default!;
        [Column("DESCRIPCION")]
        public string descripcion  {get; set;} = default!;
    }

}    