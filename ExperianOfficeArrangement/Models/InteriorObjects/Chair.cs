using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    public class Chair : FurnitureItem
    {
        public Chair(string brandName = null) :
            base(brandName)
        {
        }
    }
}
