using System.Linq;
using System.Web.NHaml.Parser;

namespace System.Web.NHaml.IO
{
    public class HamlLine
    {
        public HamlLine(string content, HamlRuleEnum hamlRule, HamlSourceFileMetrics metrics = null, string indent = "", bool isInline = false)
        {
            Metrics = metrics ?? new HamlSourceFileMetrics(0, -1, content.Length);
            Content = content;
            Indent = isInline ? "" : indent;
            IndentCount = IsBlankLine(content, hamlRule)
                ? 0
                : GetIndentCount(indent);
            HamlRule = hamlRule;
            IsInline = isInline;
        }

        private static bool IsBlankLine(string content, HamlRuleEnum hamlRule)
        {
            return (hamlRule == HamlRuleEnum.PlainText && string.IsNullOrEmpty(content));
        }

        private int GetIndentCount(string indent)
        {
            var chars = indent.ToArray();
            return chars.Sum(curChar => curChar == '\t' ? 2 : 1);
        }

        public int IndentCount { get; private set; }
        public HamlRuleEnum HamlRule { get; private set; }
        public HamlSourceFileMetrics Metrics { get; private set; }
        public string Indent { get; private set; }
        public string Content { get; set; }
        public bool IsInline { get; private set; }
    }
}
