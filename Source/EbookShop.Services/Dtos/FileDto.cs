using EbookShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EbookShop.Services.Dtos
{
    public class FileDto
    {
        public string Path { get; set; }
        public FileType FileType { get; set; }
    }
}
