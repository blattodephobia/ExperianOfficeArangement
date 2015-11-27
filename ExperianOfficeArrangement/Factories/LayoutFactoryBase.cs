using ExperianOfficeArrangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExperianOfficeArrangement.Factories
{
    public abstract class LayoutFactoryBase : IInteriorLayoutFactory
    {
        static LayoutFactoryBase()
        {
            SearchedAssemblies = new HashSet<Assembly>();
            SupportedTypes = new Dictionary<char, Type>();
            SearchAssembly(typeof(LayoutFactoryBase).Assembly);
        }

        protected static readonly int MaxRows = 50;
        protected static readonly int MaxColumns = 50;

        private static readonly HashSet<Assembly> SearchedAssemblies;

        private static readonly Dictionary<char, Type> SupportedTypes;

        public abstract InteriorField[,] GetLayout();

        public virtual string LayoutIdentifier { get; protected set; }

        protected InteriorField CreateField(char symbol)
        {
            try
            {
                Assembly currentAssembly = Assembly.GetCallingAssembly();
                if (!SearchedAssemblies.Contains(currentAssembly)) SearchAssembly(currentAssembly);

                return Activator.CreateInstance(SupportedTypes[symbol]) as InteriorField;
            }
            catch
            {
                return null;
            }
        }

        protected LayoutFactoryBase()
        {
            this.SupportedIdentifiers = SupportedTypes.Keys.ToList();
        }

        private static void SearchAssembly(Assembly assembly)
        {
            SearchedAssemblies.Add(assembly);
            foreach (Type layoutType in (assembly.ExportedTypes ?? Enumerable.Empty<Type>()).Where(t => typeof(InteriorField).IsAssignableFrom(t)))
            {
                foreach (SymbolIdentifierAttribute identifierHelper in layoutType.GetCustomAttributes(typeof(SymbolIdentifierAttribute)).Cast<SymbolIdentifierAttribute>())
                {
                    if (!SupportedTypes.ContainsKey(identifierHelper.Symbol))
                    {
                        SupportedTypes.Add(identifierHelper.Symbol, layoutType);
                    }
                }
            }
        }

        public IReadOnlyList<char> SupportedIdentifiers { get; private set; }
    }
}
