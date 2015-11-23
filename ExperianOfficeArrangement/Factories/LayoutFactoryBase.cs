using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExperianOfficeArrangement.Factories
{
    public abstract class LayoutFactoryBase : IInteriorLayoutFactory
    {
        protected static readonly int MaxRows = 50;
        protected static readonly int MaxColumns = 50;

        public abstract InteriorField[,] GetLayout();

        public virtual string LayoutIdentifier { get; protected set; }

        protected InteriorField CreateField(char symbol)
        {
            try
            {
                return Activator.CreateInstance(this.supportedTypes[symbol]) as InteriorField;
            }
            catch
            {
                return null;
            }
        }

        protected LayoutFactoryBase()
        {
            this.supportedTypes = new Dictionary<char, Type>();

            foreach (Type layoutType in (Assembly.GetAssembly(typeof(LayoutFactoryBase)).ExportedTypes ?? Enumerable.Empty<Type>()).Where(t => typeof(InteriorField).IsAssignableFrom(t)))
            {
                SymbolIdentifierAttribute identifierHelper = layoutType.GetCustomAttribute(typeof(SymbolIdentifierAttribute)) as SymbolIdentifierAttribute;
                if (identifierHelper != null)
                {
                    this.supportedTypes.Add(identifierHelper.Symbol, layoutType);
                }
            }

            this.SupportedIdentifiers = this.supportedTypes.Keys.ToList();
        }

        public IReadOnlyList<char> SupportedIdentifiers { get; private set; }

        private Dictionary<char, Type> supportedTypes;
    }
}
