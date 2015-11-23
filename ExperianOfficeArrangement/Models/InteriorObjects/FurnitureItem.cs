using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    public abstract class FurnitureItem : InteriorObjectBase
    {
        public string BrandName { get; protected set; }

        protected FurnitureItem(string brandName)
        {
            this.BrandName = brandName;
        }
    }
}
