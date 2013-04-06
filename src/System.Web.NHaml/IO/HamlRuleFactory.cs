using System.Web.NHaml.Parser;

namespace System.Web.NHaml.IO
{
    public static class HamlRuleFactory
    {
        public static HamlRuleEnum ParseHamlRule(ref string content, out int tokenLength)
        {
            tokenLength = 0;
            if (content == "")
            {
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
                return HamlRuleEnum.PlainText;
            }
            if (content.StartsWith("\\\\"))
            {
                return HamlRuleEnum.PlainText;
            }
            if (content.StartsWith("\\#"))
            {
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
                return HamlRuleEnum.Tag;
            }
            if (content.StartsWith("#"))
            {
                return HamlRuleEnum.Tag;
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
                content = content.Substring(1).Trim();
                return HamlRuleEnum.Partial;
            }
            return HamlRuleEnum.PlainText;
        }
    }
}
