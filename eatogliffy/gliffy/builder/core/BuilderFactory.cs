using EaToGliffy.Gliffy.Builder.DiagramLinks;
using EaToGliffy.Gliffy.Builder.DiagramObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EaToGliffy.Gliffy.Builder.Core
{
    /// <summary>
    /// FActory to return builder classes out of type strings
    /// </summary>
    public static class BuilderFactory
    {
        private static Dictionary<string, LinkBuilder> linkCatalog = new Dictionary<string, LinkBuilder>();
        private static Dictionary<string, ObjectBuilder> objectCatalog = new Dictionary<string, ObjectBuilder>();


        /// <summary>
        /// Returns with the proper builder instance out of link type
        /// </summary>
        /// <param name="eaLinkType">EA Link Type</param>
        /// <returns>Builder instance or null</returns>
        public static LinkBuilder GetLinkBuilder (string eaLinkType)
        {
            if(!linkCatalog.ContainsKey(eaLinkType)) {
                linkCatalog.Add(eaLinkType, CreateLinkBuilder(eaLinkType));
            }

            return linkCatalog[eaLinkType];
        }

        /// <summary>
        /// Returns with the proper builder instance out of element type
        /// </summary>
        /// <param name="eaLinkType">EA Element Type</param>
        /// <returns>Builder instance or null</returns>
        public static ObjectBuilder GetObjectBuilder(string eaElementType)
        {
            if (!objectCatalog.ContainsKey(eaElementType))
            {
                objectCatalog.Add(eaElementType, CreateObjectBuilder(eaElementType));
            }

            return objectCatalog[eaElementType];
        }

        private static LinkBuilder CreateLinkBuilder(string eaLinkType)
        {
            switch (eaLinkType)
            {
                case "Dependency":
                    return new DependecyBuilder();

                case "Association":
                    return new AssociationBuilder();

                case "Aggregation":
                    return new AggregationBuilder();

                case "Instantiation":
                    return new CompositionBuilder();

                default:
                    return null;
            }
        }

        private static ObjectBuilder CreateObjectBuilder(string eaElementType)
        {
            switch (eaElementType)
            {
                case "Boundary":
                    return new BoundaryBuilder();

                case "Component":
                    return new ComponentBuilder();

                default:
                    return null;
            }
        }
    }
}
