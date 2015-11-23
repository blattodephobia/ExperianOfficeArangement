using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    public abstract class InteriorField
    {
        public abstract char MapSymbol { get; }

        public virtual bool CanHoldObject(InteriorObjectBase obj)
        {
            return false;
        }
    }
}
