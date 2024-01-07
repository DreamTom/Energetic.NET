using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel
{
    public class TreeResponse
    {
        public long Id { get; set; }

        public string Label { get; set; } = null!;

        public string Value => Id.ToString();

        public long ParentId { get; set; }

        public List<TreeResponse> Children { get; set; } = [];
    }
}
