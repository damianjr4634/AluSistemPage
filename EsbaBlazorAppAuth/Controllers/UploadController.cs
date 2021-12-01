using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EsbaBlazorAppAuth.Pages.Alumno
{
    public partial class UploadController : Controller    
    {
        private IWebHostEnvironment _hostingEnvironment;

        public UploadController(IWebHostEnvironment environment) {
            _hostingEnvironment = environment;
        }

        [HttpPost("upload/single")]
        public async Task<IActionResult>  Single(IFormFile file)
        {
            try
            {
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                
                if (!Directory.Exists(uploads)) 
                {
                    Directory.CreateDirectory(uploads);
                }

                if (file.Length > 0) {
                    string filePath = Path.Combine(uploads, file.FileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                        await file.CopyToAsync(fileStream);
                    }
                }
                
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/multiple")]
        public IActionResult Multiple(IFormFile[] files)
        {
            try
            {
                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("upload/{id}")]
        public IActionResult Post(IFormFile[] files, int id)
        {
            try
            {
                // Put your code here
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}