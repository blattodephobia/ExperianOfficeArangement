using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperianOfficeArrangement.Factories
{
    public interface IInteriorLayoutFactory
    {
        InteriorField[,] GetLayout();

        string LayoutIdentifier { get; }
    }
}
