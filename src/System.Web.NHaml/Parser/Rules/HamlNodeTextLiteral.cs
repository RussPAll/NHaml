using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTextLiteral : HamlNode
    {
        public HamlNodeTextLiteral(HamlSourceFileMetrics metrics, string content)
            : base(metrics, content)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
