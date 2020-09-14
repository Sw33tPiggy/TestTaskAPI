using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;
using APITest.Domain.Services;
using APITest.Domain.Repositories;
using APITest.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace APITest.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _env;
        public FileService(IFileRepository fileRepository, IUserService userService, IWebHostEnvironment env)
        {
            _fileRepository = fileRepository;
            _userService = userService;
            _env = env;
        }


        public async Task<FileAddResponse> AddFiles(string email, IFormFile file)
        {
            var user = await _userService.FindUserByEmailAsync(email);

            FileRecord fileRecord = new FileRecord();

            var filePath = _env.ContentRootPath;
            var docName = Path.GetFileName(file.FileName);
            var fileType = Path.GetExtension(file.FileName);
            if (file != null && file.Length > 0)
            {
                fileRecord.Id = Guid.NewGuid();
                fileRecord.Name = docName;
                fileRecord.Type = fileType;
                fileRecord.Path = Path.Combine(filePath, "Files", fileRecord.Id.ToString() + fileRecord.Type);
                fileRecord.Owner = user;
                fileRecord.OwnerID = user.Id;
                using (var stream = new FileStream(fileRecord.Path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _fileRepository.Add(fileRecord);
                return new FileAddResponse(true, null);
            } else {
                return new FileAddResponse(false, "Something is wrong with your file");
            }

        }

        public async Task<FileGetResponse> GetFile(string email, Guid fileId)
        {
            var user = await _userService.FindUserByEmailAsync(email);
            var useeId = user.Id;
            var fileRecord = await _fileRepository.FindFileByIdAsync(fileId, useeId);
            if(fileRecord == null){
                return new FileGetResponse(false, "Not Found", null);
            }

            return new FileGetResponse(true, null, fileRecord);
        }

        public async Task<IEnumerable<FileRecord>> GetFiles(string email)
        {
            var user = await _userService.FindUserByEmailAsync(email);
            var userId = user.Id;
            var files = await _fileRepository.ListAsync(userId);
            return files;
        }
    }
}