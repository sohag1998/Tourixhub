﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourixhub.Domain.Entities
{
    public class Report: IEntity<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ReportAt { get; set; }
        public Guid AppUserId { get; set; }
        public Guid PostId { get; set; }

        // Navigation Property
        public AppUser AppUser { get; set; }
        public Post Post { get; set; }
    }
}
