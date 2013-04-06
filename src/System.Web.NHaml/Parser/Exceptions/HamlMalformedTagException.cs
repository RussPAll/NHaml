using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Exceptions
{
    [Serializable]
    public class HamlParserMalformedTagException : HamlParserException
    {
        public HamlParserMalformedTagException(string message, HamlSourceFileMetrics metrics)
            : base(string.Format("Malformed tag at {0} : {1}", metrics, message))
        { }

        public HamlParserMalformedTagException(string message, int lineNo)
            : base(string.Format("Malformed tag at line {0} : {1}", lineNo, message))
        { }
    }
}
