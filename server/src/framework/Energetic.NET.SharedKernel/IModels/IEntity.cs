﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energetic.NET.SharedKernel.IModels
{
    public interface IEntity
    {
        long Id { get; }

        public void SetId(long id);
    }
}
