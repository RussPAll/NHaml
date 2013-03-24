namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTagClass : HamlNode
    {
        public HamlNodeTagClass(int sourceFileLineNo, int sourceFileCharIndex, string className)
            : base(sourceFileLineNo, sourceFileCharIndex, className)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
