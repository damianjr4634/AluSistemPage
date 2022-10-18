using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorApp.Data
{
    [Table("CARRE_GRP")]
    public class CarreraGrupos {
        [Key]
        [Column("FCODIGO")]
        public string id  {get; set;} = default!;
        [Column("FDESCRI")]
        public string name  {get; set;} = default!;
    }

}    