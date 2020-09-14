using System.Collections.Generic;
using System;

namespace APITest.Domain.Models{
    public class FileRecord
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type {get; set; }

        public Guid OwnerID { get; set; }
        public User Owner { get; set; }

        public string Path { get; set; }

    }
}