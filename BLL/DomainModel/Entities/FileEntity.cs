﻿using System;

namespace BLL.DomainModel.Entities
{
    public class FileEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public bool IsPublic { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime UploadDate { get; set; }
        public byte[] Data { get; set; }
       // public string Description { get; set; }
    }
}
