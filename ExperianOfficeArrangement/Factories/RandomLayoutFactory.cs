using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperianOfficeArrangement.Models;

namespace ExperianOfficeArrangement.Factories
{
    public class RandomLayoutFactory : LayoutFactoryBase
    {
        public RandomLayoutFactory() :
            this(new Random())
        {
        }

        public RandomLayoutFactory(int seed) :
            this(new Random(seed))
        {
        }

        public override InteriorField[,] GetLayout()
        {
            List<char?> symbolsPool = this.SupportedIdentifiers.Select(c => (char?)c).ToList();
            symbolsPool.Add(null);

            InteriorField[,] result = new InteriorField[rand.Next(1, MaxRows / 5), rand.Next(1, MaxColumns / 5)];
            for (int x = 0; x < result.GetLength(0); x++)
                for (int y = 0; y < result.GetLength(1); y++)
                {
                    char? currentField = symbolsPool[rand.Next(symbolsPool.Count)];
                    if (currentField != null) result[x, y] = this.CreateField(currentField.Value);
                }

            return result;
        }

        protected RandomLayoutFactory(Random rand)
        {
            this.rand = rand;
            this.LayoutIdentifier = "<random>";
        }

        private Random rand;
    }
}
