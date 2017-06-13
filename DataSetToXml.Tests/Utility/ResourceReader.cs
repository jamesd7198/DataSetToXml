using System;
using System.IO;
using System.Reflection;

namespace DataSetToXml.Tests.Utility
{
    internal class ResourceReader
    {
        internal static Stream GetStreamFromResource(string resourceName)
        {
            var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);

            if (stream == null)
            {
                throw new ArgumentException($"Failed to find resource {resourceName}.");
            }

            return stream;
        }
    }
}
