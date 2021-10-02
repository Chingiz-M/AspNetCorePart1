﻿using AspNetCoreProject.Domain.Entities.Base;
using AspNetCoreProject.Domain.Entities.Base.Interfaces;

namespace AspNetCoreProject.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }
        public int? ParentId { get; set; }
    }
}
