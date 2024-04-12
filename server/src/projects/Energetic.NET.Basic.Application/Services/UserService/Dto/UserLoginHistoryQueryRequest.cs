using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.Basic.Application.Services.UserService.Dto
{
    public record UserLoginHistoryQueryRequest(int PageNumber, int PageSize) : PaginatedQueryRequest(PageNumber, PageSize)
    {
        public string? LoginAccount { get; set; }

        private DateTime[] _rangeTime = [];

        public DateTime[] RangeTime
        {
            get
            {
                return _rangeTime;
            }
            set
            {
                if (value.Length != 0 && value.Length != 2)
                    throw new ArgumentException($"{nameof(RangeTime)}世界范围不合法");
                _rangeTime = value;
            }
        }
    }
}
