using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTagClass : HamlNode
    {
        public HamlNodeTagClass(HamlSourceFileMetrics metrics, string className)
            : base(metrics, className)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
