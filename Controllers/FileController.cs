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

namespace APITest.Controllers
{
    [ApiController]
    public class FileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        public FileController(IUserService userService, IWebHostEnvironment env)
        {
            _userService = userService;
            _env = env;
        }

        [Route("/api/myfiles")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetMyFiles(){
            return Ok("kok");
        }

        [Route("/api/files/download")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DownloadFile(/*[FromBody] int Id*/){
            
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.FindUserByEmailAsync(userId);
            var files = user.Files;
            
            return Ok("kok");
        }

        [Route("/api/files/upload")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UploadFile(){
            var file = Request.Form.Files;
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.FindUserByEmailAsync(userId);
            FileRecord fileRecord  = new FileRecord();

            var filePath = _env.ContentRootPath;
            var docName = Path.GetFileName(file.FileName);
            var fileType = Path.GetExtension(file.FileName);
            if (file != null && file.Length > 0){
                fileRecord.Id = Guid.NewGuid();
                fileRecord.Name = docName;
                fileRecord.Type = fileType;
                fileRecord.Path = Path.Combine(filePath, "Files", fileRecord.Id.ToString() + fileRecord.Type);
                fileRecord.Owner = user;
                fileRecord.OwnerID = user.Id;
                using (var stream = new FileStream(fileRecord.Path, FileMode.Create)){
                    await file.CopyToAsync(stream);
                }

                //_context.Add(fileDetail);
            } else {
                return BadRequest();
            }
            return Ok("kok");
        }
    }
}