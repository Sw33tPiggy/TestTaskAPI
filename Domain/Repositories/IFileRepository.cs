using System.Collections.Generic;
using System.Threading.Tasks;
using APITest.Domain.Models;
using System;

namespace APITest.Domain.Repositories{
    public interface  IFileRepository{
        Task<IEnumerable<FileRecord>> ListAsync(Guid userId);
        Task<FileRecord> FindFileByIdAsync(Guid Id, Guid UserId);

        void Add(FileRecord fileRecord);
    }
}