namespace System.Web.NHaml.Parser.Rules
{
    public class HamlNodeHamlComment : HamlNode
    {
        public HamlNodeHamlComment(IO.HamlLine nodeLine)
            : base(nodeLine, -1)
        { }

        protected override bool IsContentGeneratingTag
        {
            get { return false; }
        }
    }
}
