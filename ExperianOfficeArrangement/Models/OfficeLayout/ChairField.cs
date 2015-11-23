using ExperianOfficeArrangement.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    [SymbolIdentifier(IDENTIFIER)]
    public class ChairField : FurnitureField
    {
        public const char IDENTIFIER = 'C';

        public override char MapSymbol
        {
            get
            {
                return IDENTIFIER;
            }
        }

        public override bool CanHoldObject(InteriorObjectBase obj)
        {
            return obj is Chair;
        }
    }
}
