using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeCode : HamlNode
    {
        public HamlNodeCode(HamlLine line)
            : base(line, -1) { }

        protected override bool IsContentGeneratingTag
        {
            get { return false; }
        }
    }
}
