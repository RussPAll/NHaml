namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeHtmlComment : HamlNode
    {
        public HamlNodeHtmlComment(IO.HamlLine nodeLine)
            : base(nodeLine, -1)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return true; }
        }
    }
}
