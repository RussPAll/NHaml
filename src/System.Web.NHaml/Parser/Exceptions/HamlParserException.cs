namespace System.Web.NHaml.Parser.Exceptions
{
    [Serializable]
    public class HamlParserException : Exception
    {
        protected HamlParserException(string message, Exception exception)
            : base(message, exception)
        { }

        protected HamlParserException(string message)
            : base(message)
        { }
    }
}