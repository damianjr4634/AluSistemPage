using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EsbaBlazorAppAuth.Data
{    
    public class AlumnoWeb
    {

        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Apellido { get; set; } = default!;
        public string CodigoAlumno { get; set; } = default!;
        public string CarreraId { get; set; } = default!;
        public string Sexo { get; set; } = default!;
        public string Nacionalidad { get; set; } = default!;
        public string EstadoCivil { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; } = default!;
        public string LugarNacimiento { get; set; } = default!;
        public string ProvinciaNacimiento { get; set; } = default!;
        public string Domicilio { get; set; } = default!;
        public string Localidad { get; set; } = default!;
        public string CodigoPostal { get; set; } = default!;
        public string Telefono { get; set; } = default!;
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; } = default!;
        public string Celular { get; set; } = default!;
        public string Cambio { get; set; } = default!;
        [DataType(DataType.DateTime)]
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