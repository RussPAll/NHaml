using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Exceptions
{
    [Serializable]
    public class HamlMalformedVariableException : Exception
    {
        public HamlMalformedVariableException(string variable, HamlSourceFileMetrics metrics)
            : base(string.Format("Malformed variable at {0} : {1}", metrics, variable))
        { }
    }
}
