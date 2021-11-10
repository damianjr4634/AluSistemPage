using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data
{
    [Table("CARRERA")]
    public class Carrera
    {
        [Key]
        [Column("CARRE")]
        public string id { get; set; } = default!;
        [Column("DESCARRE")]
        public string name { get; set; } = default!;
    }

}