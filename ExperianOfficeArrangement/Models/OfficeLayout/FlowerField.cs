using ExperianOfficeArrangement.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    [SymbolIdentifier(IDENTIFIER)]
    public class FlowerField : InteriorField
    {
        public const char IDENTIFIER = 'F';

        public override char MapSymbol
        {
            get
            {
                return IDENTIFIER;
            }
        }

        public override bool CanHoldObject(InteriorObjectBase obj)
        {
            return obj is Flower;
        }
    }
}
