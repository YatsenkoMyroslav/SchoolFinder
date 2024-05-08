using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolFinder.Common
{
    public class FileBytes
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public byte[]? Data { get; set; } = null;
        public string Extension { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
