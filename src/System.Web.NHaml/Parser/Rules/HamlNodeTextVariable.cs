using System.Linq;
using System.Web.NHaml.IO;

namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTextVariable : HamlNode
    {
        public HamlNodeTextVariable(HamlSourceFileMetrics metrics, string content)
            : base(metrics, content)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }

        public string VariableName
        {
            get
            {
                return Content.Substring(2, Content.Length - 3);
            }
        }

        public bool IsVariableViewDataKey()
        {
            return VariableName.All(Char.IsLetterOrDigit);
        }
    }
}
