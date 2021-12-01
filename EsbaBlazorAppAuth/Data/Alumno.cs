using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data
{
    [Table("ALUMNOS_WEB")]
    public class AlumnoWeb
    {
        [Key]
        [Required]
        [Column("ID")] public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Column("NOMBRE")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string Nombre { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un apelido")]
        [Column("APELLIDO")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string Apellido { get; set; } = default!;
        
        [Required]
        [Column("CODIGO_ALUMNO")] 
        public string CodigoAlumno { get; set; } = default!;
        
        [Required]
        [Column("CARRERA_ID")] 
        public string CarreraId { get; set; } = default!;
        
        [Required(ErrorMessage = "Sexo es requerido")]
        [Column("SEXO")] 
        public string Sexo { get; set; } = default!;

        [Required(ErrorMessage = "Debe ingresar una nacionalidad")]
        [Column("NACIONALIDAD")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string Nacionalidad { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un estado civil")]
        [Column("ESTADO_CIVIL")] 
        public string EstadoCivil { get; set; } = default!;

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Debe ingresar una fecha de nacimiento")]
        [Column("FECHA_NACIMIENTO")] 
        public DateTime FechaNacimiento { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un lugar de nacimiento")]
        [Column("LUGAR_NACIMIENTO")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string LugarNacimiento { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar una provincia de nacimiento")]
        [Column("PROVINCIA_NACIMIENTO")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string ProvinciaNacimiento { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un domicilio")]
        [Column("DOMICILIO")] 
        [StringLength(100, ErrorMessage = "Largo maximo 100 caracteres")]
        public string Domicilio { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar una localidad")]
        [Column("LOCALIDAD")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string Localidad { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un codigo postal")]
        [Column("CODIGO_POSTAL")] 
        [StringLength(20, ErrorMessage = "Largo maximo 20 caracteres")]
        public string CodigoPostal { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un telefono")]
        [Column("TELEFONO")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string Telefono { get; set; } = default!;
        
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Debe ingresar un mail")]
        [Column("MAIL")]
        [StringLength(80, ErrorMessage = "Largo maximo 80 caracteres")]
        public string Mail { get; set; } = default!;
        
        [Required(ErrorMessage = "Debe ingresar un numero de celular")]
        [Column("CELULAR")] 
        [StringLength(50, ErrorMessage = "Largo maximo 50 caracteres")]
        public string Celular { get; set; } = default!;
       
        [Column("CAMBIO")] 
        public string Cambio { get; set; } = default!;
        [Column("FOTO_64")] 
        public string FotoBase64 { get; set; } = default!;
        
        [DataType(DataType.DateTime)]
        [Column("ULTIMA_ACTUALIZACION")]
         public DateTime UltimaActualizacion { get; set; } = default!;
    }
}

/*create view alumnos_web ( id, nombre, apellido, codigo_alumno, carrera_id, sexo, nacionalidad,
estado_civil, fecha_nacimiento, lugar_nacimiento, provincia_nacimiento,
domicilio, localidad, codigo_postal, telefono, mail, celular, cambio, ultima_actualizacion )
as
select INDICE, NOMBRE, APELLIDO, COD_ALU, CARRE, SEXO, NACIONAL, EST_CIV, FEC_NAC, LUG_NAC, PCIA_NAC, DOMI,
       LOCALI, COD_POS, TELE, MAIL, CELULAR,
       CAMBIO, ultmod
from WEB_ALUMNOS WA

CREATE TABLE WEB_ALUMNOS (
    NOMBRE VARCHAR(50),
    APELLIDO VARCHAR(50),
    COD_ALU   CHAR(11) NOT NULL,
    CARRE     VARCHAR(6) NOT NULL,
    SEXO      VARCHAR(1),
    NACIONAL  VARCHAR(50),
    EST_CIV   CHAR(1),
    FEC_NAC   DATE,
    LUG_NAC   VARCHAR(50),
    PCIA_NAC  VARCHAR(50),
    DOMI      VARCHAR(100),
    LOCALI    VARCHAR(50),
    COD_POS   NUMERIC(4,0),
    TELE      VARCHAR(50),
    MAIL      VARCHAR(80),
    CELULAR   VARCHAR(20),
    CAMBIO    CHAR(1),
    INDICE    INTEGER NOT NULL,
    ULTMOD    TIMESTAMP
);


ALTER TABLE WEB_ALUMNOS ADD PRIMARY KEY (INDICE);
*/