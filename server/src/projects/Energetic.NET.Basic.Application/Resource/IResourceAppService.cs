using Energetic.NET.Basic.Application.Resource.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Resource
{
    public interface IResourceAppService
    {
        Task<List<ResourceTreeResponse>> GetResourceTreeAsync();
    }
}
