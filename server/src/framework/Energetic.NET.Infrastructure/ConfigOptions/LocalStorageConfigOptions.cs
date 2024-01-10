using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Infrastructure.ConfigOptions
{
    public class LocalStorageConfigOptions
    {
        public required string SavePath { get; set; }

        public long MaxFileSize { get; set; }
    }
}
