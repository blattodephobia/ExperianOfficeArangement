using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    public class Table : FurnitureItem
    {
        public Table(string brandName = null) :
            base(brandName)
        {
        }
    }
}
