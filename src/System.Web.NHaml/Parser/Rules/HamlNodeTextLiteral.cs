namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTextLiteral : HamlNode
    {
        public HamlNodeTextLiteral(int sourceLineNum, int sourceFileCharIndex, string content)
            : base(sourceLineNum, sourceFileCharIndex, 0, content)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
