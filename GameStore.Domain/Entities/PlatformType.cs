﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class PlatformType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
