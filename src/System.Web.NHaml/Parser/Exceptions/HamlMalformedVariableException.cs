using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Exceptions
{
    [Serializable]
    public class HamlParserMalformedVariableException : HamlParserException
    {
        public HamlParserMalformedVariableException(string variable, HamlSourceFileMetrics metrics)
            : base(string.Format("Malformed variable at {0} : {1}", metrics, variable))
        { }
    }
}
