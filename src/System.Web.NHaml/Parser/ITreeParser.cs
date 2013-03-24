using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser
{
    public interface ITreeParser
    {
        HamlDocument ParseHamlFile(HamlFile hamlFile);
    }
}
