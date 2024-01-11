using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Infrastructure.ConfigOptions
{
    public class AppConfigOptions
    {
        public bool IsDemo { get; set; }

        public List<string> Permissions { get; set; } = [];
    }
}
