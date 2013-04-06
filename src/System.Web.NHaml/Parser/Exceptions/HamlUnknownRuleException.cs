using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Exceptions
{
    [Serializable]
    public class HamlParserUnknownRuleException : HamlParserException
    {
        public HamlParserUnknownRuleException(string ruleValue, HamlSourceFileMetrics metrics)
            : this(ruleValue, metrics, null)
        { }

        private HamlParserUnknownRuleException(string ruleValue, HamlSourceFileMetrics metrics, Exception ex)
            : base(string.Format("Unknown rule '{0}' at {1}", ruleValue, metrics), ex)
        { }
    }
}
