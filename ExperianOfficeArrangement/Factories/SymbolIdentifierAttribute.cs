using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Factories
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class SymbolIdentifierAttribute : Attribute
    {
        public char Symbol { get; private set; }

        public SymbolIdentifierAttribute(char symbol)
        {
            this.Symbol = symbol;
        }
    }
}
