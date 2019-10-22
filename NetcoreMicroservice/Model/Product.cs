using System;
using System.Collections.Generic;
using System.Text;

namespace NetcoreMicroservice.Model
{
   public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
    }
}
