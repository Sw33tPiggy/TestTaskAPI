using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using APITest.Domain.Services;
using APITest.Domain.Models;
using APITest.Resources;
using APITest.Utils;
using System;
using System.Security.Claims;
using System.IO;
using System.Collections.Generic;

namespace APITest.Controllers
{
    [ApiController]
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _env;
        public FileController(IFileService fileService, IWebHostEnvironment env)
        {
            _fileService = fileService;
            _env = env;
        }

        [Route("/api/myfiles")]
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<FileResource>> GetMyFiles(){
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var files = await _fileService.GetFiles(userId);
            var resources = Mapper.FileRecordsToFileResources(files);
            return resources;
        }

        [Route("/api/files/download")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DownloadFile([FromBody] FileResource fileResource){
            
            Guid Id = fileResource.Id;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _fileService.GetFile(userId, Id);
            if(!response.Success){
                return BadRequest(response.Message);
            }
            
            var fileRecord = response.FileRecord;
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = fileRecord.Name,
                Inline = false
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());

            var path = _env.ContentRootPath;
            var fileReadPath = Path.Combine(path, "Files", fileRecord.Id.ToString() + fileRecord.Type);
            var file = System.IO.File.OpenRead(fileReadPath);
            return File(file, "application/force-download", fileRecord.Name); 
        }

        [Route("/api/files/upload")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadFile(){
            var files = Request.Form.Files;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _fileService.AddFiles(userId, files[0]);

            if(!response.Success){
                return BadRequest(response.Message);
            }
            return Ok();
        }
    }
}