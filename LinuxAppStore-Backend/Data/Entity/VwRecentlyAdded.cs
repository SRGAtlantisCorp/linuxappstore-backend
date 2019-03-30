﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinuxAppStore_Backend.Data.Entity
{
    public class VwRecentlyAdded
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime LastUpdated { get; set; }

    }
}
