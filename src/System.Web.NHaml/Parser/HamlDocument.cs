using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser
{
    public class HamlDocument : HamlNode
    {
        public HamlDocument(string fileName)
            : base(new HamlLine(fileName, HamlRuleEnum.Document), -1)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return false; }
        }
    }
}
