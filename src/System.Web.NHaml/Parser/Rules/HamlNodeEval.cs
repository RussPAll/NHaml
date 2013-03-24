using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeEval : HamlNode
    {
        public HamlNodeEval(HamlLine line)
            : base(line, -1) { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
