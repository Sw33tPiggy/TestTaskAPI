using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;
using APITest.Domain.Models.Responses;
using Microsoft.AspNetCore.Http;

public interface IFileService{

        Task<FileAddResponse> AddFiles(User user, IFormFileCollection files);
        Task<FileAddResponse> GetFile(User user, Guid fileId);
    }