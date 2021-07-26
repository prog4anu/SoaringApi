using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoaringApi.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SoaringApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UploadController : Controller
    {
        private readonly SDCMSApiContext _context;

        public UploadController(SDCMSApiContext context)
        {
            _context = context;
        }

        //Get api/upload/mainlogo
        [HttpGet("{uploadlogo}")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadLogo()
        {
            try
            {
                //var file = Request.Form.Files[0];
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = Path.Combine("Logo");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
