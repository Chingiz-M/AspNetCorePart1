﻿using AspNetCoreProject.Domain.Entities.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreProject.Domain.Entities.Base
{
    class Entity : IEntity
    {
        public int ID { get; set; }
    }
}
