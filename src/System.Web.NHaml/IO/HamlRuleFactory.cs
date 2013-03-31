using System.Web.NHaml.Parser;

namespace System.Web.NHaml.IO
{
    public static class HamlRuleFactory
    {
        public static HamlRuleEnum ParseHamlRule(ref string content, out int tokenLength)
        {
            if (content == "")
            {
                tokenLength = 0;
                return HamlRuleEnum.PlainText;
            }

            if (content.StartsWith("!!!"))
            {
                tokenLength = 3;
                content = content.Substring(3);
                return HamlRuleEnum.DocType;
            }
            if (content.StartsWith("-#"))
            {
                tokenLength = 2;
                content = content.Substring(2);
                return HamlRuleEnum.HamlComment;
            }
            if (content.StartsWith("#{"))
            {
                tokenLength = 0;
                return HamlRuleEnum.PlainText;
            }
            if (content.StartsWith("\\\\"))
            {
                tokenLength = 0;
                return HamlRuleEnum.PlainText;
            }
            if (content.StartsWith("\\#"))
            {
                tokenLength = 0;
                return HamlRuleEnum.PlainText;
            }
            if (content.StartsWith("%"))
            {
                tokenLength = 1;
                content = content.Substring(1);
                return HamlRuleEnum.Tag;
            }
            if (content.StartsWith("."))
            {
                tokenLength = 0;
                return HamlRuleEnum.DivClass;
            }
            if (content.StartsWith("#"))
            {
                tokenLength = 0;
                return HamlRuleEnum.DivId;
            }
            if (content.StartsWith("/"))
            {
                tokenLength = 1;
                content = content.Substring(1);
                return HamlRuleEnum.HtmlComment;
            }
            if (content.StartsWith("="))
            {
                tokenLength = 1;
                content = content.Substring(1);
                return HamlRuleEnum.Evaluation;
            }
            if (content.StartsWith("-"))
            {
                tokenLength = 1;
                content = content.Substring(1);
                return HamlRuleEnum.Code;
            }
            if (content.StartsWith(@"\"))
            {
                tokenLength = 1;
                content = content.Substring(1);
                return HamlRuleEnum.PlainText;
            }
            if (content.StartsWith("_"))
            {
                tokenLength = 1;
                while (char.IsWhiteSpace(content[tokenLength]))
                    tokenLength++;

                content = content.Substring(tokenLength).Trim();
                return HamlRuleEnum.Partial;
            }
            tokenLength = 0;
            return HamlRuleEnum.PlainText;
        }
    }
}
