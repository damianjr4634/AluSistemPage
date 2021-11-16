using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data.Tablas
{
    [Table("SEXO_WEB")]
    public class Sexo {
        [Key]
        [Column("ID")]
        public string id  {get; set;} = default!;
        [Column("DESCRIPCION")]
        public string descripcion  {get; set;} = default!;
    }

}    