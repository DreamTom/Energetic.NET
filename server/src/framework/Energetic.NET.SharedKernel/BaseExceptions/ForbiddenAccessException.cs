using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.BaseExceptions
{
    public class ForbiddenAccessException : BaseException
    {
        public ForbiddenAccessException() : base("无权限访问")
        {
        }
    }
}
