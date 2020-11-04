﻿using System;
using System.Collections.Generic;

namespace SavvyDB.Entities
{
    public partial class Location
    {
        public Location()
        {
            Cart = new HashSet<Cart>();
            Manager = new HashSet<Manager>();
        }

        public int Locationid { get; set; }
        public string Name { get; set; }
        public int? Employees { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Manager> Manager { get; set; }
    }
}