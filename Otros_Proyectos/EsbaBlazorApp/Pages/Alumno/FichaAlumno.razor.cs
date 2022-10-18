using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EsbaBlazorApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Blazored.Toast.Services;
using FirebirdSql.Data.FirebirdClient;
using Radzen.Blazor;
using System.ComponentModel.DataAnnotations;
using Radzen;

namespace EsbaBlazorApp.Pages.Alumno
{
    public partial class FichaAlumno : _BasePage
    {
        [Parameter]
        public string CarreraId { set; get; } = "";
        [Parameter]
        public string AlumnoId { set; get; } = "";
    }
}