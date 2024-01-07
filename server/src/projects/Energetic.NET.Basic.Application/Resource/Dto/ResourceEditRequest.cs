using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Resource.Dto
{
    public record ResourceEditRequest : ResourceAddRequest
    {
        public ResourceEditRequest(string Name, int DisplayOrder) : base(Name, DisplayOrder)
        {
        }

        public long Id { get; set; }
    }
}
