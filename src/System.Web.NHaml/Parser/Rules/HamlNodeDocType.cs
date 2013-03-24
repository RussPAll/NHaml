using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeDocType : HamlNode
    {
        public HamlNodeDocType(HamlLine line)
            : base(line, -1) { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
