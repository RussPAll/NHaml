namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeTagId : HamlNode
    {
        public HamlNodeTagId(int sourceFileLineNo, int sourceFileCharIndex, string tagId)
            : base(sourceFileLineNo, sourceFileCharIndex, 1, tagId)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
