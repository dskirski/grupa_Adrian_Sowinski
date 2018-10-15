using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Core.DataModels
{
    public class FilePath
    {
        [Key]
        public int FilePathId { get; set; }
        /// <summary>
        /// The original file name
        /// </summary>
        [StringLength(255), Required]
        public string FileName { get; set; }
        /// <summary>
        /// File location in storage system. Name of the file must be unique in order to prevent of having two files with the same name in the same directory. 
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Enum property for filtering file type.
        /// </summary>
        public FileType FileType { get; set; }


        public Ebook Ebook { get; set; }

    }

    public enum FileType
    {
        EBOOK_EPUB,
        EBOOK_MOBI,
        EBOOK_PDF

    }
}
