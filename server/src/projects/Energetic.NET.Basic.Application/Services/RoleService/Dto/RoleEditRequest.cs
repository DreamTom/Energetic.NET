﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.RoleService.Dto
{
    public record RoleEditRequest(string Name, string Code, string? Description)
    {
    }
}
