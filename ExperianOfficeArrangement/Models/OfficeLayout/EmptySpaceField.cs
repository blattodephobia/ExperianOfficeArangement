using ExperianOfficeArrangement.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Models
{
    [SymbolIdentifier(IDENTIFIER)]
    public class EmptySpaceField : InteriorField
    {
        public const char IDENTIFIER = 'X';

        public override char MapSymbol
        {
            get
            {
                return IDENTIFIER;
            }
        }
    }
}
