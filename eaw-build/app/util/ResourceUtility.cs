using System.IO;
using System.Linq;
using System.Reflection;

namespace eaw.build.app.util
{
    internal static class ResourceUtility
    {
        private static readonly Assembly ASSEMBLY = Assembly.GetExecutingAssembly();

        internal static string GetResourceByFileName(string name)
        {
            return ASSEMBLY.GetManifestResourceNames()
                .Single(str => str.EndsWith(name));
        }

        internal static Stream GetResourceAsStreamByFileName(string name)
        {
            return ASSEMBLY.GetManifestResourceStream(GetResourceByFileName(name));
        }
    }
}
