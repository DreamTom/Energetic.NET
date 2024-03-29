﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public abstract class BaseAuditableResponse
    {
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedTime { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedTime { get; set; }
    }
}
