using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTagId : HamlNode
    {
        public HamlNodeTagId(HamlSourceFileMetrics metrics, string tagId)
            : base(metrics, tagId)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
