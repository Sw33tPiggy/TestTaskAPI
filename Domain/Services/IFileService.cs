using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;
using APITest.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;
using System;

public interface IFileService{

        Task<FileAddResponse> AddFiles(string email, IFormFile file);
        Task<FileGetResponse> GetFile(string email, Guid fileId);

        Task<IEnumerable<FileRecord>> GetFiles(string email);
    }