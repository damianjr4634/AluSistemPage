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

}

/*
create view carreras_web ( id, nombre )
as
select carre, descarre from carrera
*/