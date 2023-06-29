using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Dtos
{
    public class FileDto
    {
        public string MimeType { get; set; } = string.Empty;

        public string OriginalFileName { get; set; } = string.Empty;

        public string UniqueFileName { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;
    }
}
